using Microsoft.EntityFrameworkCore;
using PatientRecovery.PatientService.Data;
using PatientRecovery.PatientService.Models;

namespace PatientRecovery.PatientService.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;

        public PatientRepository(PatientDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.VitalSigns)
                .Include(p => p.Medications)
                .ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(Guid id)
        {
            return await _context.Patients
                .Include(p => p.VitalSigns)
                .Include(p => p.Medications)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<VitalSigns>> GetPatientVitalSignsAsync(Guid patientId)
        {
            return await _context.VitalSigns
                .Where(v => v.PatientId == patientId)
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Medication>> GetPatientMedicationsAsync(Guid patientId)
        {
            return await _context.Medications
                .Where(m => m.PatientId == patientId)
                .OrderByDescending(m => m.StartDate)
                .ToListAsync();
        }
    }
}