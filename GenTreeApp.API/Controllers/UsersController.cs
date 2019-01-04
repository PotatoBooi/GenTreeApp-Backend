using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GenTreeApp.API.DTOs.Users;
using GenTreeApp.API.DTOs.Users.Login;
using GenTreeApp.API.DTOs.Users.Register;
using GenTreeApp.API.Helpers;
using GenTreeApp.API.Persistence;
using GenTreeApp.API.Services;
using GenTreeApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenTreeApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly TreeDbContext _ctx;

        public UsersController(IUserService userService, IOptions<AppSettings> appSettings,TreeDbContext ctx)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _ctx = ctx;
        }


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

       

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] UserLoginDto userDto)
        {

            var user = _userService.Authenticate(userDto.Name, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("ApiUser","") 
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
               
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Name,
                Token = tokenString
            });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserRegisterDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name
            };
            try
            {
                // save 
                _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("avatar")]
        public async Task<ActionResult> AddAvatar([FromBody] AddAvatarToUserDto request)
        {
            var user = _userService.GetById(request.UserId);
            var media = await _ctx.Media.FindAsync(request.MediaId);
            if (user == null || media == null)
            {
                return NotFound();
            }

            if (media.Type != MediaType.Picture)
            {
                return BadRequest();
            }

            media.User = user;
            media.Type = MediaType.Avatar;

            _ctx.Media.Update(media);
            await _ctx.SaveChangesAsync();
            return Ok(new {mediaId = media.Id});
        }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
