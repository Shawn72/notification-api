using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System;
using System.Threading.Tasks;

namespace NotificationApi.Controllers
{

    [ApiController]
    [Route("api/customerfullutilshare")]
    public class CustomerFullUtilShareController : Controller
    {
        private readonly ICustomerFullUtilShare _repo;

        public CustomerFullUtilShareController(ICustomerFullUtilShare repo) //create constructor
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult> GetCustomerFullUtilShare()
        {
            try
            {
                return Ok(await _repo.GetAllCustomerFullUtilShare());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("createfullutilshare")]
        public async Task<IActionResult> CreateCustomerFullUtilShare([FromBody] CustomerFullUtilShareSetting util)
        {
            if (util == null)
                return BadRequest();

            await _repo.CreateCustomerFullUtilShare(util);

            return Created("", util);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerFullUtilShare(string id)
        {
            var cutil = _repo.GetCustomerFullUtilShare(id);
            return Ok(cutil);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerFullUtilShare(string id, [FromBody] CustomerFullUtilShareSetting util)
        {
            // additional product and model validation checks

            var dbUtil = _repo.GetCustomerFullUtilShare(id);
            if (dbUtil == null)
                return NotFound();

            await _repo.UpdateCustomerFullUtilShare(util, dbUtil);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerFullUtilShare(string id)
        {
            var cutil = _repo.GetCustomerFullUtilShare(id);
            if (cutil == null)
                return NotFound();

            await _repo.DeleteCustomerFullUtilShare(cutil);

            return Ok();
        }


    }
}
