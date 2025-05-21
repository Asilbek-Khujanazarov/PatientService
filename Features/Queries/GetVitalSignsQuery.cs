using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Queries
{
    public class GetVitalSignsQuery : IRequest<VitalSignsDto>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
    }
}