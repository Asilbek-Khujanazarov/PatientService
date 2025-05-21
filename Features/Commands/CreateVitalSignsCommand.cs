using System;
using MediatR;
using PatientRecovery.PatientService.DTOs;

namespace PatientRecoverySystem.PatientService.Features.Commands
{
    public class CreateVitalSignsCommand : IRequest<VitalSignsDto>
    {
        public Guid PatientId { get; set; }
        public decimal Temperature { get; set; }
        public int HeartRate { get; set; }
        public int BloodPressureSystolic { get; set; }
        public int BloodPressureDiastolic { get; set; }
        public int RespiratoryRate { get; set; }
        public decimal OxygenSaturation { get; set; }
        public string Notes { get; set; }
    }
}