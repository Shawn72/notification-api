using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Interfaces;
using NotificationApi.Models;
using System;
using System.Threading.Tasks;

namespace NotificationApi.Controllers
{
    [ApiController]
    [Route("api/statementconfig")]
    public class StatementConfigController : ControllerBase
    {
        private readonly IStatementConfig _repo;

        public StatementConfigController(IStatementConfig repo) //create constructor
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult> GetStatementConfig()
        {
            try
            {
                return Ok(await _repo.GetAllCustomerStatConfig());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("createstatconfig")]
        public async Task<IActionResult> CreateStatementConfig([FromBody] IndividualStatementConfig statementConfig)
        {
            if (statementConfig == null)
                return BadRequest();

            await _repo.CreateCustomerStatConfig(statementConfig);

            return Created("", statementConfig);
        }

        [HttpGet("{id}")]
        public IActionResult GetStatementConfig(string id)
        {
            var statement = _repo.GetCustomerStatConfig(id);
            return Ok(statement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatementConfig(string id, [FromBody] IndividualStatementConfig statconfig)
        {
            // additional product and model validation checks

            var dbStatconfig = _repo.GetCustomerStatConfig(id);
            if (dbStatconfig == null)
                return NotFound();

            await _repo.UpdateCustomerStatConfig(statconfig, dbStatconfig);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatementConfig(string id)
        {
            var stat = _repo.GetCustomerStatConfig(id);
            if (stat == null)
                return NotFound();

            await _repo.DeleteCustomerStatConfig(stat);

            return Ok();
        }
    }
}
