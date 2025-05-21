using Microsoft.AspNetCore.Mvc;
using MediatR;
using PatientRecovery.PatientService.Features.Commands;
using PatientRecovery.PatientService.Features.Queries;
using PatientRecovery.PatientService.DTOs;
using PatientRecovery.Shared.Messaging;

namespace PatientRecovery.PatientService.Controllers
{
    [ApiController]
    [Route("api/patients/{patientId}/medications")]
    public class MedicationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRabbitMQService _messageBus;

        public MedicationsController(IMediator mediator, IRabbitMQService messageBus)
        {
            _mediator = mediator;
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<ActionResult<MedicationDto>> Create(Guid patientId, [FromBody] CreateMedicationCommand command)
        {
            command.PatientId = patientId;
            var result = await _mediator.Send(command);

            // Notify about new medication
            _messageBus.PublishMedicationAdded(System.Text.Json.JsonSerializer.Serialize(result));

            return CreatedAtAction(nameof(Get), new { patientId, id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationDto>> Get(Guid patientId, Guid id)
        {
            var query = new GetMedicationQuery { PatientId = patientId, Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicationDto>> Update(Guid patientId, Guid id, [FromBody] UpdateMedicationCommand command)
        {
            if (id != command.Id || patientId != command.PatientId)
                return BadRequest();

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid patientId, Guid id)
        {
            var command = new DeleteMedicationCommand { PatientId = patientId, Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}