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
        public IActionResult GetAllCustomers()
        {
            try
            {
                var users = _userService.GetUsers();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Users Fetched.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving Users.");
                return StatusCode(500);
            }
        }

        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var customer = _userService.GetUserById(id);
                if (customer == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("User are fetched by ID");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a Customer by ID.");
                return StatusCode(500);
            }
        }

        [HttpPost("CreateCustomer")]
        public IActionResult AddUser(UserDTO user)
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
                if (!_userService.AddUser(user))
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

        [HttpPut("UpdateCustomer")]
        public IActionResult UpdateUser(int id, User user)
        {
            try
            {
                if (id != user.UserId)
                {
                    return BadRequest();
                }

                _userService.UpdateUser(id, user);
                _logger.LogInformation("User is Created");

                return Ok("Customer Successfully Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a Customer.");
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }
                _userService.DeleteUser(id);
                _logger.LogInformation("Customer is Deleted");

                return Ok("Customer Successfully Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a Customer.");
                return StatusCode(500);
            }
        }
    }
}
