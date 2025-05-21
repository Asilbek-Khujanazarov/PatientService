namespace PatientRecovery.PatientService.Models
{
    public class Medication
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Instructions { get; set; }
        public string Status { get; set; }
        public virtual Patient Patient { get; set; }
    }
}