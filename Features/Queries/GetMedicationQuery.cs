using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Queries
{
    public class GetMedicationQuery : IRequest<MedicationDto>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
    }
}