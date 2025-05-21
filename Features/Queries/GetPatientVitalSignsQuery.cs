using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Queries
{
    public class GetPatientVitalSignsQuery : IRequest<IEnumerable<VitalSignsDto>>
    {
        public Guid PatientId { get; set; }
    }
}