using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Data;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		public static string AuthenticationName = "UserAuth";
		private readonly TasksAPIContext _context;

        public UsersController(TasksAPIContext context)
        {
            _context = context;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("adduser")]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var member = await _context.User.FirstOrDefaultAsync(u => u.Username == user.Username);
			if (member != null)
			{
				return BadRequest("Username does already exist");
			}

			_context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }


		// POST: api/Users
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("login")]
		public async Task<ActionResult<User>> Login(User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var member = await _context.User.FirstOrDefaultAsync(u => u.Username == user.Username);
			if (member == null)
			{
                return NotFound("Username does not exist");
			}
			else
			{
				if (member.Password != user.Password)
				{
                    return NotFound("Password is not correct");
				}
				else
				{
					Claim[] claims =
					{
                        new Claim(nameof(Models.User.Id), member.Id.ToString(), ClaimValueTypes.Integer),
						new Claim(nameof(Models.User.Username), member.Username)
					};
					ClaimsIdentity identity = new ClaimsIdentity(claims, AuthenticationName);
					ClaimsPrincipal principal = new ClaimsPrincipal(identity);
					AuthenticationProperties authenticationProperties = new AuthenticationProperties()
					{
						IsPersistent = true,
					};
					await HttpContext.SignInAsync(AuthenticationName, principal, authenticationProperties);
					return Ok();
				}
			}
		}

		[HttpGet("logout")]
		public async Task<OkResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return Ok();
		}
    }
}
