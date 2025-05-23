using MediatR;
using PatientRecovery.PatientService.DTOs;

public class GetPatientsByUserIdQuery : IRequest<IEnumerable<PatientDto>>
{
    public string UserId { get; set; }
}