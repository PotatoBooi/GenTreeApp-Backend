using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenTreeApp.API.DTOs.Person;
using GenTreeApp.API.DTOs.Relation;
using GenTreeApp.API.DTOs.Tree;
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
    public class TreesController : ControllerBase
    {
        private readonly TreeDbContext _ctx;
        private readonly IMapper _mapper;

        public TreesController(TreeDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns List of Trees
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<TreeGetDto>> GetTrees()
        {
            var tree = await _ctx.Trees
                .Include(p => p.Persons)
                .ThenInclude(d => d.Details)
                .ThenInclude(e => e.Events)
                .Include(p => p.Persons)
                .ThenInclude(d => d.Details)
                .ThenInclude(e => e.Media)
                .Include(p => p.Persons)
                .ThenInclude(d => d.Details)
                .ThenInclude(e => e.Comments)
                .Include(r => r.Persons).ThenInclude(r => r.Relations1)
                .Include(r => r.Persons).ThenInclude(r => r.Relations2)
                .ToListAsync();
            var mappedList = _mapper.Map<List<TreeGetDto>>(tree);
            return Ok(mappedList);
        }
        /// <summary>
        /// Returns tree with specified id
        /// </summary>
        /// <param name="id">Id of Tree</param>
        /// <returns>tree</returns>
        [HttpGet("{id}",Name = "GetTree")]
        public async Task<ActionResult<TreeGetDto>> GetTree(Guid id)
        {
            var tree = await _ctx.Trees.Where(t => t.Id == id)
                .Include(p => p.Persons)
                .ThenInclude(d => d.Details)
                .ThenInclude(e => e.Events)
                .Include(p => p.Persons)
                .ThenInclude(d => d.Details)
                .ThenInclude(e => e.Media)
                .Include(p => p.Persons)
                .ThenInclude(d => d.Details)
                .ThenInclude(e => e.Comments)
                .Include(r => r.Persons).ThenInclude(r => r.Relations1)
                .Include(r => r.Persons).ThenInclude(r => r.Relations2)
                .FirstAsync();

            var mappedTree = _mapper.Map<TreeGetDto>(tree);
            
//            foreach (var treePerson in tree.Persons)
//            {
//                List<RelationDto> joinedRelation = new List<RelationDto>();
//                foreach (var relation in treePerson.Relations1)
//                {
//                    joinedRelation.Add(_mapper.Map<RelationDto>(relation));
//                }
//                foreach (var relation in treePerson.Relations2)
//                {
//                    joinedRelation.Add(_mapper.Map<RelationDto>(relation));
//                }
//
//                
//            }
           
            return Ok(mappedTree);
        }
        /// <summary>
        /// Returns list of Trees related to User
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        [HttpGet("user/{userId}", Name = "GetUsersTrees")]
        public async Task<ActionResult<List<TreeGetDto>>> GetUsersTrees(Guid userId)
        {
            var tree = await _ctx.UserTrees.Where(u => u.UserId == userId).Include(t=>t.Tree).Select(t=>t.Tree).ToListAsync();

            var mappedTree = _mapper.Map<List<TreeGetDto>>(tree);

            //            foreach (var treePerson in tree.Persons)
            //            {
            //                List<RelationDto> joinedRelation = new List<RelationDto>();
            //                foreach (var relation in treePerson.Relations1)
            //                {
            //                    joinedRelation.Add(_mapper.Map<RelationDto>(relation));
            //                }
            //                foreach (var relation in treePerson.Relations2)
            //                {
            //                    joinedRelation.Add(_mapper.Map<RelationDto>(relation));
            //                }
            //
            //                
            //            }

            return Ok(mappedTree);
        }

        /// <summary>
        /// Gets list of all persons in tree
        /// </summary>
        /// <param name="id">Id of Tree</param>
        /// <returns>List of persons</returns>
        [HttpGet("{id}/persons",Name="GetPersonsForTree")]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersonsForTree(Guid id)
        {

            var persons = await _ctx.Persons.Where(t => t.Tree.Id == id)
                .Include(d=>d.Details)
                .ThenInclude(e=>e.Events)
                .Include(d => d.Details)
                .ThenInclude(e => e.Comments)
                .Include(d => d.Details)
                .ThenInclude(e => e.Media)
                .Include(r => r.Relations1)
                .Include(r => r.Relations2)
                .ToListAsync();
            var mapped = _mapper.Map<List<PersonDto>>(persons);
            return Ok(mapped);
        }

        /// <summary>
        /// Adds Person to specified Tree and returns its Id 
        /// 
        /// </summary>
        /// <param name="id">Id of Tree</param>
        /// <param name="person">Person object for creation</param>
        /// <returns></returns>
        [HttpPost("{id}/persons")]
        public async Task<ActionResult> AddPersonToTree(Guid id,[FromBody] PersonCreationDto person)
        {
            var tree = await _ctx.Trees.FindAsync(id);
            if (tree == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mappedPerson = _mapper.Map<Person>(person);
            mappedPerson.Tree = tree;
            await _ctx.Persons.AddAsync(mappedPerson);
            await _ctx.SaveChangesAsync();
            var creationResult = _mapper.Map<PersonDto>(mappedPerson);
            return CreatedAtRoute("GetPersonsForTree", new {Id = creationResult.Id});

        }
        /// <summary>
        /// Adds Tree and returns Id for later edition
        /// </summary>
        /// <param name="tree">Object for Tree creation</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddTree([FromBody] TreeCreationDto tree)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model State");
            }

            var addTree = _mapper.Map<Tree>(tree);
            await _ctx.Trees.AddAsync(addTree);
            await _ctx.SaveChangesAsync();

            return CreatedAtRoute("GetTree", new {treeId = addTree.Id});
//            var addTree = new Tree();
//            var addPersons = new List<Person>();
//            foreach (var personCreation in tree.Persons)
//            {
//                addPersons.Add(new Person
//                {
//                    Details = new Details
//                {
//                        Name = personCreation.Name,
//                        Surname = personCreation.Surname,
//                        Sex = (personCreation.Sex.ToLower()=="male")?Sex.Male : Sex.Female,
//                        
//                },
//
//                });
//            }

        }
        /// <summary>
        /// Adds owner to Tree
        /// </summary>
        /// <remarks>This must be done after Tree creation</remarks>
        /// <param name="treeId">Id of Tree</param>
        /// <param name="userId">Id of User</param>
        /// <returns></returns>
        [HttpPost("{treeId}/{userId}")]
        public async Task<ActionResult> AddTreeOwner(Guid treeId, Guid userId)
        {
            var tree = await _ctx.Trees.FindAsync(treeId);
            var user = await _ctx.Users.FindAsync(userId);
            if (tree == null || user == null)
            {
                return NotFound("Tree or User not found.");
            }

            _ctx.UserTrees.Add(new UserTree {Tree = tree, User = user});
            await _ctx.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Changes Tree
        /// </summary>
        /// <param name="id">Id of Tree</param>
        /// <param name="treeUpdated">Object for Tree edition</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTree(Guid id,[FromBody]TreeCreationDto treeUpdated)
        {
            var tree = await _ctx.Trees.FindAsync(id);
            if (tree == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            tree.Name = treeUpdated.Name;
             _ctx.Trees.Update(tree);
            await _ctx.SaveChangesAsync();

            return Ok();
        }
        /// <summary>
        /// Deletes Tree
        /// </summary>
        /// <param name="id">Id of Tree</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTree(Guid id)
        {

            var tree = await _ctx.Trees.FindAsync(id);
            if (tree == null)
            {
                return NotFound();
            }

            _ctx.Trees.Remove(tree);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

    }
}