using MediatR;

namespace PatientRecovery.PatientService.Features.Commands
{
    public class DeleteMedicationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
    }
}