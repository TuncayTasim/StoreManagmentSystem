using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.UserModels;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{UserId}")]
        public async Task<ActionResult<UserModelWithRole>> GetUserById(Guid UserId)
        {
            return await _userService.GetModelById(UserId);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserModelWithPass user)
        {
            await _userService.AddUser(user);
            await _userService.RequestEmailConfirm(user.Email);
            return Ok("User successfully added.");
            
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{UserId}")]
        public async Task<ActionResult> DeleteUser(Guid UserId)
        {
            var userToDelate = await _userService.DeleteUser(UserId);
            if (userToDelate == null)
            {
                return NotFound($"User with ID {UserId} was not found.");
            }

            return Ok($"User with ID: {UserId} successfully deleted.");
        }


        [Authorize(Roles = "Admin,Salesman")]
        [HttpPut("{UserId}")]
        public async Task<ActionResult> UpdateUser(Guid UserId, UserModelToChange newUserInfo)
        {
            var updatedUser = await _userService.UpdateUser(UserId, newUserInfo);

            if (updatedUser == null)
            {
                return NotFound($"User with ID {UserId} was not found.");
            }

            return Ok(updatedUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRole/{UserId}")]
        public async Task<ActionResult> UpdateUserRole(Guid UserId, string newRole)
        {
            var updatedUser = await _userService.UpdateUserRole(UserId, newRole);

            if (updatedUser == null)
            {
                return NotFound($"User with ID {UserId} was not found.");
            }

            return Ok("Role successfully updated.");
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<User>> Login(string email, string loginPassword)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound($"User with this email was not found");
            }

            var token = _userService.GetJWTToken(user, loginPassword);

            if (token == null)
                return BadRequest(new { message = "User name or password is incorrect" });

            return Ok(token);
        }

        [Authorize(Roles = "Admin,Salesman")]
        [HttpPut("UpdatePassword")]
        public async Task<ActionResult> UpdateUserPassword(string email, string currentPass, string newUserPassword)
        {
            var userToChangePass = await _userService.GetUserByEmail(email);
            
            if (userToChangePass == null)
            {
                return NotFound($"User with this email was not found");
            }
            var newPassUser = await _userService.UpdateUserPassword(userToChangePass, currentPass, newUserPassword);
            if (newPassUser == null)
            {
                return Unauthorized($"Тhe entered current passoword is not correct");
            }
            return Ok("Password successfully updated.");
            
        }


        [HttpPost("RequestResetPassword")]
        public async Task<IActionResult> RequestReset(string email)
        {
            var link = await _userService.RequestPasswordReset(email);

            if (link == null)
                return NotFound("User not found.");

            return Ok("Reset link sent to your email.");
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string token, string newPassword)
        {
            var userToChangePass = await _userService.GetUserByToken(token);

            if (userToChangePass == null)
            {
                return NotFound($"User with this token was not found!");
            }

            var userWithNewPassword = await _userService.ResetPassword(userToChangePass, newPassword);

            return Ok("Password successfully reset.");
        }

        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var userToChangeStatus = await _userService.GetUserByToken(token);

            if (userToChangeStatus == null)
            {
                return NotFound($"User with this token was not found!");
            }

            var userWithNewPassword = await _userService.ChangeStatus(userToChangeStatus, "Active");

            if (userWithNewPassword == null)
            {
                return NotFound($"Invalid status for user!");
            }

            return Ok("E-mail successfully confirmed.");
        }
    }
}
