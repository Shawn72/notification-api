using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System;
using System.Threading.Tasks;

namespace NotificationApi.Controllers
{   

    [ApiController]
    [Route("api/customerclaimalert")]
    public class CustomerClaimAlertController : ControllerBase
    {
        private readonly ICustomerClaimAlert _repo;

        public CustomerClaimAlertController(ICustomerClaimAlert repo) //create constructor
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerClaimAlert()
        {
            try
            {
                return Ok(await _repo.GetAllCustomerClaimAlert());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("createcustclaimalert")]
        public async Task<IActionResult> CreateCustomerClaimAlert([FromBody] CustomerClaimAlert claim)
        {
            if (claim == null)
                return BadRequest();
            await _repo.CreateCustomerClaimAlert(claim);
            return Created("", claim);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerClaimAlert(int id)
        {
            var calert = _repo.GetCustomerClaimAlert(id);
            return Ok(calert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerClaimAlert(int id, [FromBody] CustomerClaimAlert claimAlert)
        {
            // additional product and model validation checks

            var dbClaim = _repo.GetCustomerClaimAlert(id);
            if (dbClaim == null)
                return NotFound();
            await _repo.UpdateCustomerClaimAlert(claimAlert, dbClaim);
            return Ok();
        }

        [HttpPut("sqlupdate")]
        public async Task<IActionResult> UpdateCustomerClaimAlertBySQl(int id, [FromBody] CustomerClaimAlert claimAlert)
        {
            // additional product and model validation checks

            var dbClaim = _repo.GetCustomerClaimAlert(id);
            if (dbClaim == null)
                return NotFound();
            await _repo.UpdateClaimAlertBySQL(claimAlert, dbClaim);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerClaimAlert(int id)
        {
            var alertc = _repo.GetCustomerClaimAlert(id);
            if (alertc == null)
                return NotFound();
            await _repo.DeleteCustomerClaimAlert(alertc);
            return Ok();
        }
    }
}
