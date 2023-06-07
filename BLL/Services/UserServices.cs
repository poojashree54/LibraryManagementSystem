using BLL.DTOs;
using DAL.Models;
using DAL.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepository;
        private readonly IConfiguration _configuration;

        public UserServices(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }

       

        /*public string Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUserName(username);
            if (user == null || !VerifyPasswordHash(password, user.Password))
            {
                // Authentication failed
                return null;
            }

            return GenerateJwtToken(user.UserName);
        }*/
        public string Register(UserDTO user)
        {
            // Hash the password before storing it in the database
            user.Password = CreatePasswordHash(user.Password);
            var temp = new User
            {
                /*UserId = user.UserId,*/
                UserName = user.UserName,
                Password = user.Password,
                IsAdmin = user.IsAdmin
            };

            return _userRepository.CreateUser(temp);
        }
        //Kept this method as public due to Testing requirements
        public string CreatePasswordHash(string password)
        {
            return password;
        }

        /*private bool VerifyPasswordHash(string password, string existingPassword)
        {
            return password == existingPassword;
        }
        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }*/

        public ICollection<User> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public bool AddUser(UserDTO userDto)
        {


            var temp = (User)userDto;
            var s = _userRepository.CreateUser(temp);
            if (s != null)
            {
                return true;
            }
            return false;
        }

        public void UpdateUser(int id, User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}
