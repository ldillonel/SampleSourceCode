using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyTool.Database;
using SurveyTool.Extensions;
using SurveyTool.Models;

namespace SurveyTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SurveyToolDbContextBase _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _loginManager;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>();

        public IConfiguration configuration { get; }

        public AuthController(
            IConfiguration iConfig, 
            SurveyToolDbContextBase context, 
            UserManager<User> userManager, 
            SignInManager<User> loginManager)
        {
            configuration = iConfig;
            _context = context;
            _users = _context.Users.ToList();
            _userManager = userManager;
            _loginManager = loginManager;
        }

        // GET: api/auth/user/5
        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private User GetUserByName(string userName)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            return user;
        }

        // api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody]UserCredentials credentials)
        {
            var user = this.Authenticate(credentials.Username, credentials.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // api/auth/register
        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody]UserCredentials credentials)
        {
            // Create new user base info
            User user = new User
            {
                UserName = credentials.Username,
                Token = "",
                Role = "admin"
            };
            // Pass credentials to initialize and hash password
            IdentityResult result = _userManager.CreateAsync(user, credentials.Password).Result;
            if (result.Succeeded) {
                _userManager.AddToRoleAsync(user, "admin").Wait();
                return user;
            }

            return null;
        }

        // api/auth/password
        [HttpPost("password")]
        public async Task<ActionResult<User>> Password([FromBody]UserCredentials[] credentials)
        {
            if (credentials == null || credentials.Length < 2)
                return BadRequest(new { message = "Change password does not have old and new credentials" });

            // Parameter order: [old credentials, new credentials]
            UserCredentials oldCredentials = credentials[0];
            UserCredentials newCredentials = credentials[1];
            
            // Attempt to auth with old credentials
            var user = this.Authenticate(oldCredentials.Username, oldCredentials.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            // Replace old user credentials with new
            var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this._userManager.ResetPasswordAsync(user, token, newCredentials.Password); 

            // Login with new credentials
            user = this.Authenticate(newCredentials.Username, newCredentials.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        public User Authenticate(string username, string password)
        {
            var result = _loginManager.PasswordSignInAsync(username, password, false, false);

            if (result.Result.Succeeded)
            {
                User user = _users.SingleOrDefault(x => x.UserName == username);

                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                string appSettingsSecret = this.configuration.GetValue<string>("AppSettings:Secret");
                var key = Encoding.ASCII.GetBytes(appSettingsSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user;
            }

            return null;
        }
    }
}