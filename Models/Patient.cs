using PatientRecovery.Shared.Models;

namespace PatientRecovery.PatientService.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }
        public string MedicalHistory { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string Status { get; set; }
        public virtual ICollection<VitalSigns> VitalSigns { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }
    }
}