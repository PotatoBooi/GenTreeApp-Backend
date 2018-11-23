using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Models;
using Microsoft.Ajax.Utilities;

namespace GenTreeApp_API.Controllers
{
    public class TreesController : ApiController
    {
        private readonly ITreeRepository _treeRepo;

        public TreesController(ITreeRepository treeRepo)
        {
            _treeRepo = treeRepo;
        }


        // GET: api/Trees
        public async Task<IEnumerable<Tree>> Get()
        {
            var result = await _treeRepo.GetAllAsync();
            return result;
        }

        // GET: api/Trees/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Trees
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Trees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Trees/5
        public void Delete(int id)
        {
        }
    }
}
