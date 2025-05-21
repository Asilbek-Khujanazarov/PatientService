using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Queries
{
    public class GetPatientMedicationsQuery : IRequest<IEnumerable<MedicationDto>>
    {
        public Guid PatientId { get; set; }
    }
}