using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Commands
{
    public class UpdateMedicationCommand : IRequest<MedicationDto>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime? EndDate { get; set; }
        public string Instructions { get; set; }
        public string Status { get; set; }
    }
}