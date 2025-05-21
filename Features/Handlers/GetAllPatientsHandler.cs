using MediatR;
using AutoMapper;
using PatientRecovery.PatientService.Repository;
using PatientRecovery.PatientService.Features.Queries;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Handlers
{
    public class GetAllPatientsHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientDto>>
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPatientsHandler(IPatientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }
    }
}