using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Commands
{
    public class CreateMedicationCommand : IRequest<MedicationDto>
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Instructions { get; set; }
    }
}