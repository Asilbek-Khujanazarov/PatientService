using Microsoft.AspNetCore.Mvc;
using MediatR;
using PatientRecovery.PatientService.DTOs;
using PatientRecovery.PatientService.Features.Queries;
using PatientRecovery.PatientService.Features.Commands;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PatientRecovery.PatientService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PatientsController> _logger;
        public PatientsController(IMediator mediator, ILogger<PatientsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            var query = new GetAllPatientsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> Get(Guid id)
        {
            var query = new GetPatientQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(CreatePatientCommand command)
        {
            try
            {
                // User ID'ni olish
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst("sub")?.Value;
if (string.IsNullOrEmpty(userId))
{
    return Unauthorized("User not authenticated");
}

                // Command'ga UserID'ni qo'shish
                command.UserId = userId;

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating patient");
                return BadRequest(new { message = ex.Message });
            }
        }

        // Foydalanuvchiga tegishli bemorlarni olish
        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetMyPatients()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }

            var query = new GetPatientsByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> Update(Guid id, [FromBody] UpdatePatientCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePatientCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}/vital-signs")]
        public async Task<ActionResult<IEnumerable<VitalSignsDto>>> GetVitalSigns(Guid id)
        {
            var query = new GetPatientVitalSignsQuery { PatientId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}/medications")]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> GetMedications(Guid id)
        {
            var query = new GetPatientMedicationsQuery { PatientId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}