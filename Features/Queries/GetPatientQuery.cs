using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Queries
{
    public class GetPatientQuery : IRequest<PatientDto>
    {
        public Guid Id { get; set; }
    }
}