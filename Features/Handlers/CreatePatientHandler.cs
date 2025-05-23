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
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public CreatePatientHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<Patient>(request);

            // Default status qo'shamiz
            patient.Status = "Active";  // Yoki boshqa default qiymat
            patient.AdmissionDate = DateTime.UtcNow;  // AdmissionDate ham kerak
            patient.UserId = request.UserId;

            var createdPatient = await _patientRepository.CreateAsync(patient);
            return _mapper.Map<PatientDto>(createdPatient);
        }
    }
}