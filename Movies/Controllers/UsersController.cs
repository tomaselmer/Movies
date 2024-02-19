using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Movies.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class UsersController : ControllerBase
        {
            private readonly IUserService _userService;

            public UsersController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpGet("{userId}")]
            public async Task<ActionResult<User>> GetUserById(int userId)
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }

            [HttpPost]
            public async Task<IActionResult> CreateUser(User user)
            {
                await _userService.CreateUser(user);
                return Ok();
            }

            [HttpPut("{userId}")]
            public async Task<IActionResult> UpdateUser(int userId, User user)
            {
                if (userId != user.Id)
                {
                    return BadRequest();
                }

                await _userService.UpdateUser(user);
                return Ok();
            }

            [HttpDelete("{userId}")]
            public async Task<IActionResult> DeleteUser(int userId)
            {
                await _userService.DeleteUser(userId);
                return Ok();
            }
        }
}
