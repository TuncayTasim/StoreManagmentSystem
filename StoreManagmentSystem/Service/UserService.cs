using Microsoft.IdentityModel.Tokens;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Help;
using StoreManagmentSystem.Helpers;
using StoreManagmentSystem.Models;
using StoreManagmentSystem.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StoreManagmentSystem.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModelWithPass> AddUser(UserModelWithPass user)
        {
            user.Password = HashPassword(user.Password);
            await _userRepository.AddUser(user);
            return user;
        }

        public async Task<User> DeleteUser(Guid UserId)
        {
            var user = await _userRepository.GetUserById(UserId);

            if (user == null)
            {
                return null;
            }

            await _userRepository.DeleteUser(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(Guid UserId)
        {
            return await _userRepository.GetUserById(UserId);
        }
        public async Task<User> GetUserByEmail(string Email)
        {
            return await _userRepository.GetUserByEmail(Email);
        }

        public async Task<User> GetUserByToken(string Token)
        {
            return await _userRepository.GetUserByToken(Token);
        }

        public async Task<UserModelWithRole> GetModelById(Guid UserId)
        {
            return await _userRepository.GetModelById(UserId);
        }

        public async Task<UserModelWithRole> UpdateUser(Guid UserId, UserModelToChange newUserInfo)
        {
            var user = await _userRepository.GetUserById(UserId);

            if (user == null)
            {
                return null;
            }

            user.UserName = newUserInfo.UserName;
            user.FirstName = newUserInfo.FirstName;
            user.LastName = newUserInfo.LastName;
            user.Email = newUserInfo.Email;
            user.PhoneNumber = newUserInfo.PhoneNumber;
            user.Note = newUserInfo.Note;

            await _userRepository.UpdateUser(user);
            var userModel = await _userRepository.GetModelById(UserId);
            return userModel;
        }


        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }


        public string GetJWTToken(User User, string password)
        {
            bool isValid = VerifyPassword(password, User.Password);

            if (!isValid)
            {
                return null;
            }

            var secret = Environment.GetEnvironmentVariable("JWT");
            var key = Encoding.ASCII.GetBytes(secret);

            var roleName = ((UserRole)User.RoleId).ToString();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, User.UserName),
                    new Claim(ClaimTypes.Role, roleName)
                }),

                Expires = DateTime.UtcNow.AddMinutes(5),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

        public async Task<User> UpdateUserPassword(User userToUpdate, string currentPass, string newUserPassword)
        {
            bool isValid = VerifyPassword(currentPass, userToUpdate.Password);

            if (!isValid)
            {
                return null;
            }

            userToUpdate.Password = HashPassword(newUserPassword);


            await _userRepository.UpdateUser(userToUpdate);
            return userToUpdate;
        }

        public async Task<User> UpdateUserRole(Guid UserId, string newRole)
        {
            var user = await _userRepository.GetUserById(UserId);

            if (user == null)
            {
                return null;
            }

            if (!Enum.TryParse<UserRole>(newRole, ignoreCase: true, out var role))
            {
                return null;
            }
            user.RoleId = (int)role;

            await _userRepository.UpdateUser(user);
            return user;
        }

        public async Task<string> RequestPasswordReset(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                return null;

            string token = TokenGenerator.GenerateToken();
            user.ActionToken = token;
            await _userRepository.UpdateUser(user);

            var resetLink = $"https://myfrontend.com/reset-password:email={email}&token={token}";

            await EmailSender.SendEmail(
                email,
                "Reset Password",
                $"Click here to reset your password:\n{resetLink}"
            );

            return resetLink;
        }

        public async Task<string> RequestEmailConfirm(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                return null;

            string token = TokenGenerator.GenerateToken();
            user.ActionToken = token;
            await _userRepository.UpdateUser(user);

            var resetLink = $"https://myfrontend.com/confirm-email:email={email}&token={token}";

            await EmailSender.SendEmail(
                email,
                "Confirm e-mail",
                $"Click here to confirm your e-mail:\n{resetLink}"
            );

            return resetLink;
        }
        public async Task<User> ResetPassword(User user, string newPassword)
        {
            user.Password = HashPassword(newPassword);
            user.ActionToken = "";

            await _userRepository.UpdateUser(user);
            return user;
        }

        public async Task<User> ChangeStatus(User user, string newStatus)
        {

            if (!Enum.TryParse<UserStatus>(newStatus, ignoreCase: true, out var status))
            {
                return null;
            }

            user.StatusId = (int)status;
            user.ActionToken = "";

            await _userRepository.UpdateUser(user);
            return user;
        }

    }
}
