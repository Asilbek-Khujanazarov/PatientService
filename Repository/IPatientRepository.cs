using PatientRecovery.PatientService.Models;

namespace PatientRecovery.PatientService.Repository
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(Guid id);
        Task<Patient> CreateAsync(Patient patient);
        Task<Patient> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<VitalSigns>> GetPatientVitalSignsAsync(Guid patientId);
        Task<IEnumerable<Medication>> GetPatientMedicationsAsync(Guid patientId);
    }
}