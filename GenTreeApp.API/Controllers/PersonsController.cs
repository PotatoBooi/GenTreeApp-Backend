using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Internal;
using GenTreeApp.API.DTOs.Comment;
using GenTreeApp.API.DTOs.Event;
using GenTreeApp.API.DTOs.Media;
using GenTreeApp.API.DTOs.Person;
using GenTreeApp.API.DTOs.Relation;
using GenTreeApp.API.Persistence;
using GenTreeApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenTreeApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly TreeDbContext _ctx;
        private readonly IMapper _mapper;

        public PersonsController(TreeDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns Person with specified id
        /// </summary>
        /// <param name="id">Id of person</param>
        /// <returns></returns>
        [HttpGet("{id}",Name = "GetPerson")]
        public async Task<ActionResult<PersonDto>> GetPersonById(Guid id)
        {
            var person = await _ctx.Persons.Where(p=>p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(e => e.Events)
                .Include(d => d.Details)
                .ThenInclude(m => m.Media)
                .Include(d => d.Details)
                .ThenInclude(c => c.Comments)
                .Include(r => r.Relations1)
                .Include(r => r.Relations2)
                .Where(p=>p.Id == id).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PersonDto>(person));

        }
        /// <summary>
        /// Returns list of Media related to Person 
        /// </summary>
        /// <param name="id">Id of Person</param>
        /// <returns></returns>
        [HttpGet("{id}/media", Name = "GetPersonMedia")]
        public async Task<ActionResult<List<MediaDto>>> GetPersonMedia(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(m => m.Media).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            var media = person.Details.Media;
            return Ok(_mapper.Map<List<MediaDto>>(media));

        }
        /// <summary>
        /// Returns Avatar related to Person
        /// </summary>
        /// <param name="id">Id of Person</param>
        /// <returns></returns>
        [HttpGet("{id}/media/avatar", Name = "GetPersonAvatar")]
        public async Task<ActionResult<List<MediaDto>>> GetPersonAvatar(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(m => m.Media).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            var media = person.Details.Media.Where(t=>t.Type == MediaType.Avatar);
            return Ok(_mapper.Map<List<MediaDto>>(media));

        }
        [HttpPost("{id}/media/avatar/file", Name = "GetPersonAvatarFile")]
        public async Task<ActionResult<List<MediaDto>>> GetPersonAvatarFile(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(m => m.Media).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            var media = person.Details.Media.Where(t => t.Type == MediaType.Avatar);
            return Ok(_mapper.Map<List<MediaDto>>(media));

        }
        /// <summary>
        /// Returns list of Events related to Person
        /// </summary>
        /// <param name="id">Id of Person</param>
        /// <returns></returns>
        [HttpGet("{id}/events", Name = "GetPersonEvents")]
        public async Task<ActionResult<List<EventDto>>> GetPersonEvents(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(e => e.Events).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            var events = person.Details.Events;
            return Ok(_mapper.Map<List<EventDto>>(events));

        }
        /// <summary>
        /// Returns list of Comments related to Person
        /// </summary>
        /// <param name="id">Id of Person</param>
        /// <returns></returns>
        [HttpGet("{id}/comments", Name = "GetPersonComments")]
        public async Task<ActionResult<List<CommentDto>>> GetPersonComments(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(c => c.Comments).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            var comments = person.Details.Comments;
            return Ok(_mapper.Map<List<CommentDto>>(comments));

        }
        /// <summary>
        /// Returns list of Relations related to Person
        /// </summary>
        /// <param name="id">Id of Person</param>
        /// <returns></returns>
        [HttpGet("{id}/relations", Name = "GetPersonRelations")]
        public async Task<ActionResult<List<RelationDto>>> GetPersonRelations(Guid id)
        {
            var person = await _ctx.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            var relations = await _ctx.Relations
                .Where(p => (p.Person.Id == id) || (p.SecondPerson.Id == id))
                .Include(p=>p.SecondPerson).ToListAsync();

            return Ok(_mapper.Map<List<RelationDto>>(relations));
        }
        /// <summary>
        /// Adds Person to specified Tree
        /// </summary>
        /// <remarks>Relations and Media must be added separately</remarks>
        /// <param name="treeId">Id of Tree</param>
        /// <param name="person">Person object for sending data</param>
        /// <returns></returns>
        [HttpPost("{treeId}")]
        public async Task<ActionResult> AddPerson(Guid treeId, [FromBody] PersonCreationDto person)
        {
            var tree = await _ctx.Trees.FindAsync(treeId);
            if (tree == null)
            {
                return NotFound("Tree not Found");
            
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mappedPerson = _mapper.Map<Person>(person);
            mappedPerson.Tree = tree;
            await _ctx.Persons.AddAsync(mappedPerson);
            await _ctx.SaveChangesAsync();
            var personReturned = _mapper.Map<PersonDto>(mappedPerson);

            return Ok(new {Id = personReturned.Id});

        }
        /// <summary>
        /// Adds event to specified Person
        /// </summary>
        /// <remarks>Possible Event types:
        /// -Birth
        /// -Death
        /// -Wedding
        /// -Divorce
        /// </remarks>
        /// <param name="id">Id of Person</param>
        /// <param name="eventCreation">Object for creating an Event</param>
        /// <returns></returns>
        [HttpPost("{id}/events")]
        public async Task<ActionResult> AddEventToPerson(Guid id,[FromBody] EventCreationDto eventCreation)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id).Include(d => d.Details).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var newEvent = _mapper.Map<Event>(eventCreation);
            //TODO edit this after db migration(relational fix)
            newEvent.Details = person.Details;
            await _ctx.Events.AddAsync(newEvent);
            await _ctx.SaveChangesAsync();
            return Ok(new {Id = newEvent.Id});
        }
        /// <summary>
        /// Adds comment to specified Person
        /// </summary>
        /// <param name="id">If of Person</param>
        /// <param name="comment">Object for creating Comment</param>
        /// <returns></returns>
        [HttpPost("{id}/comments")]
        public async Task<ActionResult> AddCommentToPerson(Guid id, [FromBody] CommentCreationDto comment)
        {
            var person = await _ctx.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var newComment = _mapper.Map<Comment>(comment);

            newComment.Details = person.Details;
            await _ctx.Comments.AddAsync(newComment);
            await _ctx.SaveChangesAsync();
            return Ok( new { Id = newComment.Id });
        }
        /// <summary>
        /// Adds picture or video to person
        /// </summary>
        /// <param name="id">Id of Person</param>
        /// <param name="media">Object which contains mediaId</param>
        /// <returns></returns>
        [HttpPost("{id}/media")]
        public async Task<ActionResult> AddMediaToPerson(Guid id, [FromBody] MediaCreationDto media)
        {
            //todo change this after media upload is implemented
            var person = await _ctx.Persons.Where(p=>p.Id == id).Include(d=>d.Details).SingleOrDefaultAsync();
            var mediaToAdd = await _ctx.Media.FindAsync(media.MediaId);
            if (person == null || mediaToAdd == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            mediaToAdd.Details = person.Details;
            _ctx.Media.Update(mediaToAdd);
            await _ctx.SaveChangesAsync();
            return Ok();

        }
        /// <summary>
        /// Sets avatar for person with given id and mediaId to set
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        [HttpPut("{id}/media/{mediaId}/avatar")]
        public async Task<ActionResult> SetPersonAvatar(Guid id,Guid mediaId)
        {
            //todo change this after media upload is implemented
            var person = await _ctx.Persons.Where(p => p.Id == id).Include(d => d.Details).SingleOrDefaultAsync();
            var mediaToAdd = await _ctx.Media.FindAsync(mediaId);
            if (person == null || mediaToAdd == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (mediaToAdd.Type != MediaType.Picture)
            {
                return BadRequest();
            }
            var currentAvatar = await _ctx.Media.Include(d => d.Details)
                .Where(d => (d.Details.Id == person.DetailsId) && (d.Type == MediaType.Avatar))
                .FirstAsync();

            if (currentAvatar != null)
            {
                currentAvatar.Type = MediaType.Picture;
                _ctx.Update(currentAvatar);
            }

            mediaToAdd.Type = MediaType.Avatar;
            _ctx.Update(mediaToAdd);


            await _ctx.SaveChangesAsync();
            return Ok($"Changed avatar for person with id {id}");

        }
        /// <summary>
        /// Adds Relation to person
        /// </summary>
        /// <remarks>Relation types: Child, Marriage, Sibling</remarks>
        /// <param name="relation">Object which contains id of 2 Persons to create Relation</param>
        /// <returns></returns>
        [HttpPost("relations")]
        public async Task<ActionResult> AddRelationToPerson([FromBody] RelationCreationDto relation)
        {


            var person = await _ctx.Persons.FindAsync(relation.FirstPersonId);
            var secondPerson = await _ctx.Persons.FindAsync(relation.SecondPersonId);
            if (person == null || secondPerson == null)
            {
                return NotFound();
            }
            var relationToSend = _mapper.Map<Relation>(relation);
            await _ctx.AddAsync(relationToSend);
            await _ctx.SaveChangesAsync();

            return Ok();
        }
        /// <summary>
        /// Updates person 
        /// </summary>
        /// <remarks>only possible to edit name and surname</remarks>
        /// <param name="id">Id of Person</param>
        /// <param name="personUpdated">Object for Person edit</param>
        /// <returns></returns>

        //ogarnij to dzbanie
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson(Guid id,[FromBody] PersonCreationDto personUpdated)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id).Include(d => d.Details).SingleOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            if (!personUpdated.Name.IsNullOrEmpty())
            {
                person.Details.Name = personUpdated.Name;
            }
            if (!personUpdated.Surname.IsNullOrEmpty())
            {
                person.Details.Surname = personUpdated.Name;
            }

            _ctx.Update(person);
            await _ctx.SaveChangesAsync();
            return Ok(_mapper.Map<PersonDto>(person));

        }
        /// <summary>
        /// Deletes Person
        /// </summary>
        /// <param name="id">Id of Person</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(Guid id)
        {
            var person = await _ctx.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _ctx.Persons.Remove(person);
            await _ctx.SaveChangesAsync();

            return NoContent();

        }

    }
    }