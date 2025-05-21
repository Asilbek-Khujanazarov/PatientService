using Microsoft.AspNetCore.Mvc;
using MediatR;
using PatientRecovery.PatientService.DTOs;
using PatientRecovery.PatientService.Features.Queries;
using PatientRecovery.PatientService.Features.Commands; 

namespace PatientRecovery.PatientService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<ActionResult<PatientDto>> Create([FromBody] CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
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