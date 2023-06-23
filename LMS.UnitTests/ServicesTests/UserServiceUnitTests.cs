using BLL.DTOs;
using BLL.Services;
using DAL.Models;
using DAL.Repository;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.UnitTests.ServicesTests
{
    [TestFixture]
    public class UserServiceUnitTests
    {
        private Mock<IUserRepo> _userRepositoryMock;
        private Mock<IConfiguration> _configurationMock;
        private UserServices _userService;

        [SetUp]
        public void Setup()
        {
            // Creating mock instances for dependencies
            _userRepositoryMock = new Mock<IUserRepo>();
            _configurationMock = new Mock<IConfiguration>();

            // Initializing the UserService with the mock dependencies
            _userService = new UserServices(_userRepositoryMock.Object, _configurationMock.Object);
        }

        

        [Test]
        public void Authenticate_WithValidCredentials_ReturnsJwtToken()
        {
            // Arrange
            var username = "testuser";
            var password = "password";

            // Create a mock user with matching username and password
            var mockUser = new User
            {
                UserName = username,
                Password = _userService.CreatePasswordHash(password)
            };
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns(mockUser);

            // Mock the JWT configuration values
            var validKey = Encoding.ASCII.GetBytes("Jwt:Key");
            _configurationMock.Setup(config => config["Jwt:Key"]).Returns(Convert.ToBase64String(validKey));
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("your_issuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("your_audience");

            // Act
            var jwtToken = _userService.Authenticate(username, password);

            // Assert
            Assert.That(jwtToken, Is.Not.Null);
            Assert.That(jwtToken, Is.Not.Empty);
        }


        [Test]
        public void Authenticate_WithInvalidCredentials_ReturnsNull()
        {
            // Arrange
            var username = "testuser";
            var password = "password";

            // Mock the GetUserByUsername method to return null, simulating invalid credentials
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns((User)null);

            // Act
            var jwtToken = _userService.Authenticate(username, password);

            // Assert
            Assert.That(jwtToken, Is.Null);
        }

        [Test]
        public void Register_ValidUser_ReturnsUserId()
        {
            // Arrange
            var newUser = new UserDTO
            {
                UserId=1,
                UserName = "johndoe",
                Password = "password",
                IsAdmin = true
            };

            // Mock the CreateUser method to return a userId
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>())).Returns("Token");

            // Act
            var result = _userService.Register(newUser);

            // Assert
            Assert.IsTrue(result);
        }

        /*[Test]
        public void CreateUser_WhenValidUser_ReturnsToken()
        {
            // Arrange
            User user = new User
            {
                UserId=1,
                UserName = "testuser",
                Password = "password",
                IsAdmin=true
                
            };
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>())).Returns("");

            // Act
            string result = _userService.Register(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(user.Token, result);
        }*/
    }
}
