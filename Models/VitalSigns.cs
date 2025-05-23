using System.ComponentModel.DataAnnotations.Schema;

namespace PatientRecovery.PatientService.Models
{
    public class VitalSigns
    {
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Temperature { get; set; }
        public int HeartRate { get; set; }
        public int BloodPressureSystolic { get; set; }
        public int BloodPressureDiastolic { get; set; }
        public int RespiratoryRate { get; set; }
        public int OxygenSaturation { get; set; }
        public DateTime RecordedAt { get; set; }
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}