using BLL.DTOs;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly IUserServices _userService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserServices userService, ILogger<LoginController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                    return NotFound();

                _logger.LogInformation("User Fetched by Id.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving an User.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to retrieve user with ID: {id}.");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate(string userName, string password)
        {
            try
            {
                var token = _userService.Authenticate(userName, password);
                if (token == null)
                {
                    // Authentication failed
                    return Unauthorized();
                }
                _logger.LogInformation("User Logged In & Token generated.");
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Login an User.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create user.{ex}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserDTO model)
        {
            try
            {
                var user = (User)model;


                _userService.Register(user);
                _logger.LogInformation("User successfully registered.");
                return Ok("User Successfully Registered");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering an User.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create user.{ex}");
            }
        }
    }
}
