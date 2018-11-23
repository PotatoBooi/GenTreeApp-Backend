using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Models;
using Microsoft.Ajax.Utilities;

namespace GenTreeApp_API.Controllers
{
    public class TreesController : ApiController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public TreesController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/Trees
        public async Task<IHttpActionResult> Get()
        {
            var result = await _repositoryWrapper.TreeRepository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Trees/5
        public async Task<IHttpActionResult> Get(Guid id)
        {
            IEnumerable<Tree> tree = await _repositoryWrapper.TreeRepository.GetBy(u => u.UUID == id);

            if (!tree.Any())
            {
                return NotFound();
            }

            return Ok(tree.First());
        }

        // POST: api/Trees
        public async Task<IHttpActionResult> Post(Tree tree)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repositoryWrapper.TreeRepository.Create(tree);
            await _repositoryWrapper.TreeRepository.SaveAsync();

            return CreatedAtRoute("DefaultApi", new { id = tree.UUID }, tree);
        }

        // PUT: api/Trees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Trees/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        [Route("api/trees/{id}/relations")]
        public async Task<IHttpActionResult> GetRelations(Guid id)
        {
            IEnumerable<Tree> tree = await _repositoryWrapper.TreeRepository.GetBy(u => u.UUID == id);

            if (!tree.Any())
            {
                return NotFound();
            }

            return Ok(tree.First().Relations);
        }

        [HttpGet]
        [Route("api/trees/persons")]
        public async Task<IHttpActionResult> GetPersons()
        {
            var result = await _repositoryWrapper.PersonRepository.GetAllAsync();
            return Ok(result);
        }

        //        [HttpPut]
        //        [Route("api/trees/{treeId}/person/{personId}/relation/{relationId}")]
        //        public async Task<IHttpActionResult> PutRelationToPerson(Guid treeId, Guid personId, Guid relationId)
        //        {
        //            IEnumerable<Tree> tree = await _repositoryWrapper.TreeRepository.GetBy(u => u.UUID == treeId);
        //            var person = await _repositoryWrapper.PersonRepository.GetBy(u => u.UUID == personId);
        //            var relation = await _repositoryWrapper.RelationRepository.GetBy(u => u.UUID == relationId);
        //            if (!tree.Any() || !person.Any() || !relation.Any()) 
        //            {
        //                return NotFound();
        //            }
        //
        //          
        //            var updatedPerson = person.First();
        //            updatedPerson.RelationList.Add(relation.First());
        //            _repositoryWrapper.PersonRepository.Update(updatedPerson);
        //            await _repositoryWrapper.PersonRepository.SaveAsync();
        //
        //            return Ok(updatedPerson);
        //        }


    }
}
