using MediatR;
using AutoMapper;
using PatientRecovery.PatientService.Repository;
using PatientRecovery.PatientService.Features.Queries;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Handlers
{
    public class GetPatientHandler : IRequestHandler<GetPatientQuery, PatientDto>
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public GetPatientHandler(IPatientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<PatientDto>(patient);
        }
    }
}