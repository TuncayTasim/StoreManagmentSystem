using Microsoft.IdentityModel.Tokens;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagmentSystem.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUser(User user)
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

        public async Task<User> UpdateUser(Guid UserId, User newUserInfo)
        {
            var user = await _userRepository.GetUserById(UserId);

            if (user == null)
            {
                return null;
            }

            user.UserName = newUserInfo.UserName;
            user.FirstName = newUserInfo.FirstName;
            user.LastName = newUserInfo.LastName;
            user.RoleId = newUserInfo.RoleId;
            user.Email = newUserInfo.Email;
            user.PhoneNumber = newUserInfo.PhoneNumber;
            user.Note = newUserInfo.Note;

            await _userRepository.UpdateUser(user);
            return user;
        }


        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }


        public string GetToken(User User, string password)
        {
            bool isValid = VerifyPassword(password, User.Password);

            if (!isValid)
            {
                return null;
            }

            var secret = Environment.GetEnvironmentVariable("JWT");
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, User.UserName),
                    new Claim(ClaimTypes.Role, User.RoleId.ToString())
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
    }
}
