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

        public UserServices(IUserRepo userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string Login(string username, string password)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user == null) { return null; }
            var token = GenerateToken(user.UserName);
            return token;
        }
        public string GenerateToken(string username)
        {



            var tokenHandler = new JwtSecurityTokenHandler(); //create and validate jwt
            var key = Encoding.ASCII.GetBytes("Thisismysecretkey!!!"); // converted into a byte array
            //creates a SecurityTokenDescriptor object to specify the claims like the user's name, expiration time, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                 // Add additional claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Set token expiration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;




            
        }

        //Kept this method as public due to Testing requirements
        public string CreatePasswordHash(string password)
        {
            return password;
        }

        public string Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUserName(username);
            if (user == null || !VerifyPasswordHash(password, user.Password))
            {
                // Authentication failed
                return null;
            }

            return GenerateToken(user.UserName);
        }
        private bool VerifyPasswordHash(string password, string existingPassword)
        {
            return password == existingPassword;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _userRepository.GetAllUsers().Select(u => (UserDTO)u);
        }

        public UserDTO GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return null;
            return (UserDTO)user;
        }

        public bool Register(UserDTO userDto)
        {
            var existingUser = _userRepository.GetUserByUserName(userDto.UserName);
            if (existingUser != null)
            {
                return false; // User already exists
            }

            var user = new User
            {
                UserId = userDto.UserId,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                IsAdmin = userDto.IsAdmin
                // Additional properties
            };

            _userRepository.CreateUser(user);
            return true;
        }


    }
}

