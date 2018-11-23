using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Models;

namespace GenTreeApp_API.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [ResponseType(typeof(List<User>))]
        public async Task<IHttpActionResult> Get()
        {
            var list = await _userRepository.GetAllAsync();
            return Ok(list);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> Get(Guid id)
        {


            IEnumerable<User> user = await _userRepository.GetBy(u => u.UUID == id);

            if (!user.Any())
            {
                return NotFound();
            }

            return Ok(user.First());
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.Create(user);
            await _userRepository.SaveAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.UUID }, user);

        }

        // PUT: api/Users/5
        public void Put(Guid id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            IEnumerable<User> lookup = await _userRepository.GetBy(u => u.UUID == id);
            if (!lookup.Any())
            {
                return NotFound();
            }

            User user = lookup.First();
            _userRepository.Delete(user);
            await _userRepository.SaveAsync();
            return Ok(user);
        }
    }
}
