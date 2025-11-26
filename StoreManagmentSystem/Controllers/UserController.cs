using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Service;

namespace StoreManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{UserId}")]

        public async Task<ActionResult<User>> GetUserById(Guid UserId)
        {
            return await _userService.GetUserById(UserId);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            await _userService.AddUser(user);
            return Ok();
        }

        [HttpDelete("{UserId}")]
        public async Task<ActionResult> DeleteUser(Guid UserId)
        {
            var userToDelate = await _userService.DeleteUser(UserId);
            if (userToDelate == null)
            {
                return NotFound($"User with ID {UserId} was not found.");
            }

            return NoContent();
        }

        [HttpPut("{UserId}")]

        public async Task<ActionResult> UpdateUser(Guid UserId, User newUserInfo)
        {
            var updatedUser = await _userService.UpdateUser(UserId, newUserInfo);

            if (updatedUser == null)
            {
                return NotFound($"User with ID {UserId} was not found.");
            }

            return Ok(updatedUser);
        }
    }
}
