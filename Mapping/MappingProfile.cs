using AutoMapper;
using PatientRecovery.PatientService.DTOs;
using PatientRecovery.PatientService.Models;
using PatientRecovery.PatientService.Features.Commands;
using PatientRecoverySystem.PatientService.Features.Commands;

namespace PatientRecovery.PatientService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()

        {
            CreateMap<CreatePatientCommand, Patient>()
           .ForMember(dest => dest.Status,
                     opt => opt.MapFrom(src => src.Status ?? "Active"))
           .ForMember(dest => dest.AdmissionDate,
                     opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Patient, PatientDto>();
            CreateMap<UpdatePatientCommand, Patient>();
            CreateMap<VitalSigns, VitalSignsDto>();
            CreateMap<CreateVitalSignsCommand, VitalSigns>();
            CreateMap<Medication, MedicationDto>();
            CreateMap<CreateMedicationCommand, Medication>();
            CreateMap<UpdateMedicationCommand, Medication>();
        }
    }
}