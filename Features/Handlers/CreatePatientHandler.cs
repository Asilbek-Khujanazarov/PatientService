using MediatR;
using AutoMapper;
using PatientRecovery.PatientService.Models;
using PatientRecovery.PatientService.Repository;
using PatientRecovery.PatientService.Features.Commands;
using PatientRecoverySystem.PatientService.Features.Commands;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecovery.PatientService.Features.Handlers
{
    public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, PatientDto>
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public CreatePatientHandler(IPatientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<Patient>(request);
            var createdPatient = await _repository.CreateAsync(patient);
            return _mapper.Map<PatientDto>(createdPatient);
        }
    }
}