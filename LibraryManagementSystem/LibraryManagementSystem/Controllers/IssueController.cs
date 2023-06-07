using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueServices _issueService;
        private readonly ILogger<IssueController> _logger;

        public IssueController(IIssueServices issueService, ILogger<IssueController> logger)
        {
            _issueService = issueService;
            _logger = logger;
        }

        [HttpGet("GetIssue")]
        public IActionResult GetIssue()
        {
            try
            {
                var Issues = _issueService.GetIssues();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Issues Fetched.");
                return Ok(Issues);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving Issues.");
                return StatusCode(500);
            }
        }

        [HttpGet("GetIssueById")]
        public IActionResult GetIssueById(int id)
        {
            try
            {
                var issue = _issueService.GetIssue(id);
                if (issue == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("issue is fetched by ID");
                return Ok(issue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving an issue by ID.");
                return StatusCode(500);
            }
        }

        [HttpPost("AddIssue")]
        public IActionResult CreateBooking(IssueDTO issueDto)
        {
            try
            {
                if (issueDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!_issueService.AddIssue(issueDto))
                {
                    ModelState.AddModelError("", "Issuing is not Added [CONTOLLER]");
                    return StatusCode(500, ModelState);
                }
                _logger.LogInformation("Issue is Created");
                return Ok("Issue Successfully Created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an Issue.");
                return StatusCode(500);
            }
        }

        /*[HttpPut("UpdateIssue")]
        public IActionResult UpdateIssue(int id, IssueDTO issue)
        {
            try
            {
                if (id != issue.Id)
                {
                    return BadRequest();
                }

                _issueService.UpdateIssue(id, issue);
                _logger.LogInformation("Booking is Created");

                return Ok("Booking Successfully Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a Booking.");
                return StatusCode(500);
            }
        }*/

        [HttpDelete("DeleteIssue")]
        public IActionResult DeleteIssue(int id)
        {
            try
            {
                var issue = _issueService.GetIssue(id);

                if (issue == null)
                {
                    return NotFound();
                }
                _issueService.DeleteIssue(id);
                _logger.LogInformation("Issue is Deleted");

                return Ok("Issue Successfully Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an Issue.");
                return StatusCode(500);
            }
        }
    }
}
