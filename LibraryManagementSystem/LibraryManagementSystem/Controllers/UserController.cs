using BLL.DTOs;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserServices userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("GetCustomers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAllUsers()
        {
            try
            {
                var Customers = _userService.GetUsers();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Customers Fetched.");
                return Ok(Customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving Customers.");
                return StatusCode(500);
            }
        }

        [HttpGet("GetCustomerById")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var customer = _userService.GetUserById(id);
                if (customer == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Customer are fetched by ID");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a Customer by ID.");
                return StatusCode(500);
            }
        }

        [HttpPost("CreateCustomer")]
        public IActionResult CreateUser(UserDTO user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!_userService.Register(user))
                {
                    ModelState.AddModelError("", "Customer is not Created [CONTOLLER]");
                    return StatusCode(500, ModelState);
                }
                _logger.LogInformation("Customer is Created");
                return Ok("Customer Successfully Created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a Customer.");
                return StatusCode(500);
            }
        }
    }
}
