using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenTreeApp.API.DTOs.Person;
using GenTreeApp.API.DTOs.Tree;
using GenTreeApp.API.Persistence;
using GenTreeApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenTreeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreesController : ControllerBase
    {
        private TreeDbContext _ctx;
        private readonly IMapper _mapper;

        public TreesController(TreeDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Tree>> GetTree([FromBody] Guid id)
        {
            return await _ctx.Trees.FindAsync(id);
        }
        [HttpGet("{id}/persons")]
        public ActionResult<IEnumerable<PersonWithDetailsDto>> GetPersonsForTree([FromBody] Guid id)
        {
            return new List<PersonWithDetailsDto>();
        }
    

        [HttpGet]
        public ActionResult<IEnumerable<TreeCreationDto>> GetTrees()
        {
            return new List<TreeCreationDto>
            {
                new TreeCreationDto
                {
                    Persons = new List<PersonCreationDto>
                    {
                        new PersonCreationDto
                        {
                            Name = "jacek",
                            Surname = "uryga",
                            Sex = "Jakaś"
                        }
                    }
                }
            };
        }

        [HttpPost]
        public async Task<ActionResult> AddTree([FromBody] TreeCreationDto tree)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model State");
            }

            var addTree = _mapper.Map<Tree>(tree);
            var trees = await _ctx.Trees.ToListAsync();

            return Ok();
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
        [HttpPut("{treeId}/{userId}")]
        public async Task<ActionResult> AddTreeOwner([FromBody] Guid treeId, Guid userId)
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

    }
}