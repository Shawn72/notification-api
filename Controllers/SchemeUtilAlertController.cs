using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using NotificationApi.Paging;
using System;
using System.Threading.Tasks;

namespace NotificationApi.Controllers
{
    [ApiController]
    [Route("api/schemeutilalert")]
    public class SchemeUtilAlertController : ControllerBase
    {
        private readonly ISchemeUtilAlert _repo;

        public SchemeUtilAlertController(ISchemeUtilAlert repo) //create constructor
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult> GetSchemeUtilAlert()
        {
            try
            {
                return Ok(await _repo.GetAllSchemeUtilAlert());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("createschemeutilalert")]
        public async Task<IActionResult> CreateSchemeUtilAlertt([FromBody] SchemeUtilAlertModel alert)
        {
            if (alert == null)
                return BadRequest();

            await _repo.CreateSchemeUtilAlert(alert);

            return Created("", alert);
        }

        [HttpGet("{id}")]
        public IActionResult GetSchemeUtilAlert(int id)
        {
            var calert = _repo.GetSchemeUtilAlert(id);
            return Ok(calert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchemeUtilAlertt(int id, [FromBody] SchemeUtilAlertModel schmAlert)
        {
            // additional product and model validation checks

            var dbAlert = _repo.GetSchemeUtilAlert(id);
            if (dbAlert == null)
                return NotFound();

            await _repo.UpdateSchemeUtilAlert(schmAlert, dbAlert);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchemeUtilAlert(int id)
        {
            var alertc = _repo.GetSchemeUtilAlert(id);
            if (alertc == null)
                return NotFound();

            await _repo.DeleteSchemeUtilAlert(alertc);

            return Ok();
        }
    }
}
