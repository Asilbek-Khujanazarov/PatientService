using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Commands
{
    public class UpdatePatientCommand : IRequest<PatientDto>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }
        public string Status { get; set; }
    }
}