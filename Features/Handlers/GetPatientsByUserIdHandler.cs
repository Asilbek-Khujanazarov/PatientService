using AutoMapper;
using MediatR;
using PatientRecovery.PatientService.DTOs;
using PatientRecovery.PatientService.Repository;

public class GetPatientsByUserIdHandler : IRequestHandler<GetPatientsByUserIdQuery, IEnumerable<PatientDto>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientsByUserIdHandler(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetPatientsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetByUserIdAsync(request.UserId);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }
}