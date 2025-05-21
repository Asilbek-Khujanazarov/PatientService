using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Queries
{
    public class GetAllPatientsQuery : IRequest<IEnumerable<PatientDto>>
    {
    }
}