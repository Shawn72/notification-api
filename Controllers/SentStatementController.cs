using NotificationApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using NotificationApi.Models;

namespace NotificationApi.Controllers
{
    [ApiController]
    [Route("api/sentstatement")]
    public class SentStatementController : ControllerBase
    {
        private readonly ISentStatement _repo;

        public SentStatementController(ISentStatement repo) //create constructor
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult> GetSentStatement()
        {
            try
            {
                return Ok(await _repo.GetAllSentStatement());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("createsentstatement")]
        public async Task<IActionResult> CreateStatementConfig([FromBody] SentStatement sentStatement)
        {
            if (sentStatement == null)
                return BadRequest();

            await _repo.CreateSentStatement(sentStatement);

            return Created("", sentStatement);
        }

        [HttpGet("{id}")]
        public IActionResult GetSentStatement(string id)
        {
            var statement = _repo.GetSentStatement(id);
            return Ok(statement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSentStatement(string id, [FromBody] SentStatement sentStatement)
        {
            // additional product and model validation checks

            var dbSentStatement = _repo.GetSentStatement(id);
            if (dbSentStatement == null)
                return NotFound();

            await _repo.UpdateSentStatetement(sentStatement, dbSentStatement);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSentStatement(string id)
        {
            var stat = _repo.GetSentStatement(id);
            if (stat == null)
                return NotFound();

            await _repo.DeleteSentStatement(stat);

            return Ok();
        }
    }
}
