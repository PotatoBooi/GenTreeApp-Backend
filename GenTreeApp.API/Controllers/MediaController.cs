using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Internal;
using GenTreeApp.API.DTOs.Media;
using GenTreeApp.API.Persistence;
using GenTreeApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GenTreeApp.API.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : Controller
    {
        private readonly TreeDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly CloudBlobContainer _container;

        public MediaController(TreeDbContext ctx, IMapper mapper,CloudBlobContainer container)
        {
            _ctx = ctx;
            _mapper = mapper;
            _container = container;
        }
        /// <summary>
        /// Returns media with specified id
        /// </summary>
        /// <param name="id">Id of media</param>
        /// <returns></returns>
        [HttpGet("{id}",Name="GetMedia")]
        public async Task<ActionResult<MediaDto>> Get(Guid id)
        {
            var media = await _ctx.Media.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MediaDto>(media));
        }
        /// <summary>
        /// Uploads media (picture,video).
        /// </summary>
        ///<remarks>Header media-type must be multipart/form-data.
        ///Returns Id of Media for later usage.
        /// </remarks>
        /// <returns>Id of created media</returns>
        
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult<Guid>>Post([FromForm]  IFormFile file)
        {

            if (file.Length == 0)
            {
                return NoContent();
            }

            var media = new Media();

            var blockBlob = _container.GetBlockBlobReference(file.FileName);
            if (file.Length > 0)
            {

                
                using (var fileStream = file.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(fileStream);
                }

                media.Name = blockBlob.Name;
                media.Type = resolveMediaType(file.ContentType);
                media.Url = blockBlob.Uri.ToString();
            }


             await _ctx.Media.AddAsync(media);
             await _ctx.SaveChangesAsync();

           return Ok(new { Id = media.Id });
        
        }
        /// <summary>
        /// Deletes media
        /// </summary>
        /// <param name="id">Id of media</param>
        /// <returns>No content if succefully deleted</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedia(Guid id)
        {
            var media = await _ctx.Media.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            _ctx.Media.Remove(media);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        private MediaType resolveMediaType(string typeString)
        {
            if (typeString.StartsWith("image"))
            {
                return MediaType.Picture;
            }
            else if (typeString.StartsWith("video"))
            {
                return MediaType.Video;
            }
            else
            {
                return MediaType.Unknown;
            }
        }
    }
}
