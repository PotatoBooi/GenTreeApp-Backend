using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenTreeApp.API.Controllers
{

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

        [HttpGet("{id}",Name = "GetPerson")]
        public async Task<ActionResult<PersonDto>> GetPersonById(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(e => e.Events)
                .Include(d => d.Details)
                .ThenInclude(m => m.Media)
                .Include(d => d.Details)
                .ThenInclude(c => c.Comments)
                .Include(r => r.Relations1)
                .Include(r => r.Relations2).FirstAsync();
            if (person == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PersonDto>(person));

        }

        [HttpGet("{id}/media", Name = "GetPersonMedia")]
        public async Task<ActionResult<List<MediaDto>>> GetPersonMedia(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(m => m.Media).FirstAsync();
            if (person == null)
            {
                return NotFound();
            }

            var media = person.Details.Media;
            return Ok(_mapper.Map<List<MediaDto>>(media));

        }
        [HttpGet("{id}/events", Name = "GetPersonEvents")]
        public async Task<ActionResult<List<EventDto>>> GetPersonEvents(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(e => e.Events).FirstAsync();
            if (person == null)
            {
                return NotFound();
            }

            var events = person.Details.Events;
            return Ok(_mapper.Map<List<EventDto>>(events));

        }

        [HttpGet("{id}/comments", Name = "GetPersonComments")]
        public async Task<ActionResult<List<CommentDto>>> GetPersonComments(Guid id)
        {
            var person = await _ctx.Persons.Where(p => p.Id == id)
                .Include(d => d.Details)
                .ThenInclude(c => c.Comments).FirstAsync();
            if (person == null)
            {
                return NotFound();
            }

            var comments = person.Details.Comments;
            return Ok(_mapper.Map<List<CommentDto>>(comments));

        }
        [HttpGet("{id}/relations", Name = "GetPersonRelations")]
        public async Task<ActionResult<List<RelationDto>>> GetPersonRelations(Guid id)
        {
            var person = await _ctx.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            var relations = await _ctx.Relations.Where(p => (p.Person.Id == id) || (p.SecondPerson.Id == id)).Include(p=>p.SecondPerson).ToListAsync();

            return Ok(_mapper.Map<List<RelationDto>>(relations));
        }

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
            mappedPerson.Tree.Id = treeId;
            await _ctx.Persons.AddAsync(mappedPerson);
            await _ctx.SaveChangesAsync();
            var personReturned = _mapper.Map<PersonDto>(mappedPerson);

            return CreatedAtRoute("GetPerson", new {Id = personReturned.Id});

        }

        [HttpPost("{id}/events")]
        public async Task<ActionResult> AddEventToPerson(Guid id,[FromBody] EventCreationDto eventCreation)
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

            var newEvent = _mapper.Map<Event>(eventCreation);
            //TODO edit this after db migration(relational fix)
            await _ctx.Events.AddAsync(newEvent);
            await _ctx.SaveChangesAsync();
            return CreatedAtRoute("GetPersonEvents", new {Id = newEvent.Id});
        }
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
            //TODO edit this after db migration(relational fix)
            await _ctx.Comments.AddAsync(newComment);
            await _ctx.SaveChangesAsync();
            return CreatedAtRoute("GetPersonComments", new { Id = newComment.Id });
        }
        [HttpPost("{id}/media")]
        public async Task<ActionResult> AddMediaToPerson(Guid id, [FromBody] MediaCreationDto media)
        {
            //todo change this after media upload is implemented
            var person = await _ctx.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newMedia = _mapper.Map<Media>(media);
            //TODO edit this after db migration(relational fix)
            await _ctx.Media.AddAsync(newMedia);
            await _ctx.SaveChangesAsync();
            return CreatedAtRoute("GetPersonComment", new { Id = newMedia.Id });
        }

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


            //            var relation = new RelationCreationDto
            //            {
            //                FirstPersonId = personId,
            //                SecondPersonId = secondPersonId
            //            };
            //            var relationToSend = _mapper.Map<Relation>(relation);
            await _ctx.AddAsync(relationToSend);
            await _ctx.SaveChangesAsync();

            return CreatedAtRoute("GetRelations", new {Id = relationToSend.Id});
        }

        //ogarnij to dzbanie
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson(Guid id,[FromBody] PersonCreationDto personUpdated)
        {
            var person = await _ctx.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            if (!personUpdated.Name.IsNullOrEmpty())
            {

            }
            if (!personUpdated.Surname.IsNullOrEmpty())
            {

            }
            if (!personUpdated.Sex.IsNullOrEmpty())
            {

            }


            return Ok(_mapper.Map<PersonDto>(person));

        }

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