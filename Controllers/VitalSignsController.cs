using Microsoft.AspNetCore.Mvc;
using MediatR;
using PatientRecovery.PatientService.DTOs;
using PatientRecovery.PatientService.Features.Commands;
using PatientRecovery.PatientService.Features.Queries;
using PatientRecovery.Shared.Messaging;
using PatientRecoverySystem.PatientService.Features.Commands;

namespace PatientRecovery.PatientService.Controllers
{
    [ApiController]
    [Route("api/patients/{patientId}/vital-signs")]
    public class VitalSignsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRabbitMQService _messageBus;

        public VitalSignsController(IMediator mediator, IRabbitMQService messageBus)
        {
            _mediator = mediator;
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<ActionResult<VitalSignsDto>> Create(Guid patientId, [FromBody] CreateVitalSignsCommand command)
        {
            command.PatientId = patientId;
            var result = await _mediator.Send(command);

            // Check vital signs for alerts
            if (IsVitalSignsAlert(result))
            {
                _messageBus.PublishVitalSignsAlert(System.Text.Json.JsonSerializer.Serialize(result));
            }

            return CreatedAtAction(nameof(Get), new { patientId, id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VitalSignsDto>> Get(Guid patientId, Guid id)
        {
            var query = new GetVitalSignsQuery { PatientId = patientId, Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        private bool IsVitalSignsAlert(VitalSignsDto vitalSigns)
        {
            return vitalSigns.Temperature > 38.5m ||
                   vitalSigns.Temperature < 35.0m ||
                   vitalSigns.HeartRate > 100 ||
                   vitalSigns.HeartRate < 60 ||
                   vitalSigns.BloodPressureSystolic > 140 ||
                   vitalSigns.BloodPressureSystolic < 90 ||
                   vitalSigns.OxygenSaturation < 95;
        }
    }
}