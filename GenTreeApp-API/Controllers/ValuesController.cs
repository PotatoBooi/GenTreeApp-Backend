using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Models;
using GenTreeApp_API.MockObject;
using Microsoft.Ajax.Utilities;

namespace GenTreeApp_API.Controllers
{
    public class ValuesController : ApiController
    {
        private List<Tree> trees;
        private readonly ITreeRepository _treeRepository;
        public ValuesController(ITreeRepository treeRepository)
        {
          
            _treeRepository = treeRepository;
            
        }

    
        // GET api/values
        //async version of get
        //public async Task< IEnumerable<Tree>> Get()
        //{

        //    return await _treeRepository.GetAllAsync();
            
            
        //}
        public IEnumerable<Tree> Get()
        {
            return new List<Tree>();
        }
      

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
