using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using NotificationApi.Paging;
using System;
using System.Threading.Tasks;

namespace NotificationApi.Controllers
{
    [ApiController]
    [Route("api/customeralert")]
    public class CustomerAlertController : ControllerBase
    {       
        private readonly ICustomerAlert _repo;
        private ILoggerManager _logger;
        public CustomerAlertController(ILoggerManager logger,  ICustomerAlert repo) //create constructor
        {
            _repo = repo;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllCustomerAlert()
        {
            try
            {
                return Ok(await _repo.GetAllCustomerAlert());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("createcustomeralert")]
        public async Task<IActionResult> CreateCustomerAlert([FromBody] CustomerAlertModel alert)
        {
            if (alert == null)
                return BadRequest();

            await _repo.CreateCustomerAlert(alert);

            return Created("", alert);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerAlert(Int64 id)
        {
            var calert = _repo.GetCustomerAlert(id);
            return Ok(calert);
        }
               

        [HttpGet("GetByScheme/{schemeName}")]
        public IActionResult GetCustomerAlertBySchemeName(string schemeName)
        {
            try
            {
                var calert = _repo.GetCustomerAlertbySchemeName(schemeName); 
                return Ok(calert);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("totalpages")]
        public IActionResult GetTotalPages()
        {
            var ttPges = _repo.TotalAlertPages();
            return Ok(ttPges);
        }


        [HttpGet("GetAllAlerts")]
        public IActionResult GetPagedCustomerAlerts([FromQuery] PagingParameters pgParameter)
        {
          try 
            { 
            var alerts = _repo.GetCustomerAlertPagedlist(pgParameter);

            var metadata = new
            {
                alerts.TotalCount,
                alerts.PageSize,
                alerts.CurrentPage,
                alerts.TotalPages,
                alerts.HasNext,
                alerts.HasPrevious
            };

             Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

             _logger.LogInfo($"Returned {alerts.TotalCount} alerts from database.");
             return Ok(alerts);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
               
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAlert(int id, [FromBody] CustomerAlertModel cstAlert)
        {
            // additional product and model validation checks

            var dbAlert = _repo.GetCustomerAlert(id);
            if (dbAlert == null)
                return NotFound();

            await _repo.UpdateCustomerAlert(cstAlert, dbAlert);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAlert(int id)
        {
            var alertc = _repo.GetCustomerAlert(id);
            if (alertc == null)
                return NotFound();

            await _repo.DeleteCustomerAlert(alertc);

            return Ok();
        }

    }
}
