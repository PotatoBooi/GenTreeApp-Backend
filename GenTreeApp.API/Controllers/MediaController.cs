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

namespace GenTreeApp.API.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly TreeDbContext _ctx;
        private readonly IMapper _mapper;
        public MediaController(TreeDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

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
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult<Guid>>Post([FromForm]  IFormFile file)
        {

            var media = new Media();
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                    media.Content = s;
                    media.Type = resolveMediaType(file.ContentType);
                    media.Name = file.FileName;
                }
            }

            if (media.Content.IsNullOrEmpty())
            {
                return NoContent();
            }
            await _ctx.Media.AddAsync(media);
            await _ctx.SaveChangesAsync();

            return Ok(new { Id = media.Id });
        }

        [HttpDelete("id")]
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
