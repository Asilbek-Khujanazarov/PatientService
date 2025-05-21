using MediatR;

namespace PatientRecovery.PatientService.Features.Commands
{
    public class DeletePatientCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}