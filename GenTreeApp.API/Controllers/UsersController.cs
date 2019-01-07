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
using Microsoft.EntityFrameworkCore;
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




        /// <summary>
        /// Authenticates User (Login)
        /// </summary>
        /// <remarks>This is used for logging.
        /// Returns Id, Username and Bearer Token.
        /// Token must be in Authorization header when every other controller is used.
        /// Usage:
        /// 
        /// [{"key":"Authorization","value":"Bearer eyJhbGciOiJIUzI1NiIs...."} ]
        ///
        /// Sample response:
        ///
        /// 
        /// {
     ///   "id": "df4cab02-8062-4586-3d83-08d6728edcaf",
     ///   "username": "admin123",
    ///    "token": "eyJhbGciOiJIUzIGNh"
    /// }
    /// </remarks>
    /// <param name="userDto">Object containing Login and Password</param>
    /// <returns></returns>

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
        /// <summary>
        /// Registers user 
        /// </summary>
        /// <param name="userDto">Object containing Login and Password </param>
        /// <returns></returns>
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
        /// <summary>
        /// Adds avatar to user
        /// </summary>
        /// <remarks>Media must be of type Picture</remarks>
        /// <param name="request">Contains Id of User and Id of Media</param>
        /// <returns></returns>
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
            var currentAvatar = await _ctx.Media.Include(d => d.User)
                .Where(d => (d.User.Id == user.AvatarId) && (d.Type == MediaType.Avatar))
                .SingleOrDefaultAsync();

            if (currentAvatar != null)
            {
                currentAvatar.Type = MediaType.Picture;
                _ctx.Update(currentAvatar);
            }
            media.User = user;
            media.Type = MediaType.Avatar;

            _ctx.Media.Update(media);
            await _ctx.SaveChangesAsync();
            return Ok(new {mediaId = media.Id});
        }

    }
}
