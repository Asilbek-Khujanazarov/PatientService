using Microsoft.EntityFrameworkCore;
using PatientRecovery.PatientService.Models;

namespace PatientRecovery.PatientService.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<VitalSigns> VitalSigns { get; set; }
        public DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Patient -> VitalSigns relation
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.VitalSigns)
                .WithOne(v => v.Patient)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient -> Medications relation
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Medications)
                .WithOne(m => m.Patient)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // VitalSigns configuration
            modelBuilder.Entity<VitalSigns>()
                .Property(v => v.Temperature)
                .HasPrecision(5, 2);  // 123.45 format

            // Medications configuration
            modelBuilder.Entity<Medication>()
                .Property(m => m.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<Medication>()
                .Property(m => m.Dosage)
                .HasMaxLength(50);
        }
    }
}