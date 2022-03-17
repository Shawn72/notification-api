using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System;
using System.Threading.Tasks;

namespace NotificationApi.Controllers
{ 
    [ApiController]
    [Route("api/customeralertsetting")]
    public class CustomerAlertSettingController : ControllerBase
    {
        private readonly ICustomerAlertSettings _repo;

        public CustomerAlertSettingController(ICustomerAlertSettings repo) //create constructor
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerAlertSetting()
        {
            try
            {
                return Ok(await _repo.GetAllCustomerAlertSetting());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("createcustalertsetting")]
        public async Task<IActionResult> CreateCustomerAlertSetting([FromBody] CustomerAlertSetting setting)
        {
            if (setting == null)
                return BadRequest();

            await _repo.CreateCustomerAlertSetting(setting);

            return Created("", setting);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerAlertSetting(int id)
        {
            var calert = _repo.GetCustomerAlertSetting(id);
            return Ok(calert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAlertSetting(int id, [FromBody] CustomerAlertSetting alertSetting)
        {
            // additional product and model validation checks

            var dbSetting = _repo.GetCustomerAlertSetting(id);
            if (dbSetting == null)
                return NotFound();

            await _repo.UpdateCustomerAlertSetting(alertSetting, dbSetting);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAlertSetting(int id)
        {
            var alertc = _repo.GetCustomerAlertSetting(id);
            if (alertc == null)
                return NotFound();

            await _repo.DeleteCustomerAlertSetting(alertc);

            return Ok();
        }
    }
}
