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
            CreateMap<Patient, PatientDto>();
            CreateMap<CreatePatientCommand, Patient>();
            CreateMap<UpdatePatientCommand, Patient>();
            CreateMap<VitalSigns, VitalSignsDto>();
            CreateMap<CreateVitalSignsCommand, VitalSigns>();
            CreateMap<Medication, MedicationDto>();
            CreateMap<CreateMedicationCommand, Medication>();
            CreateMap<UpdateMedicationCommand, Medication>();
        }
    }
}