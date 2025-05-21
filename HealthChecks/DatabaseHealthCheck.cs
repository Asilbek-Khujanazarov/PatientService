using Microsoft.Extensions.Diagnostics.HealthChecks;
using PatientRecovery.PatientService.Data;

namespace PatientRecovery.PatientService.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly PatientDbContext _dbContext;

        public DatabaseHealthCheck(PatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Database.CanConnectAsync(cancellationToken);
                return HealthCheckResult.Healthy("Database is healthy");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database is unhealthy", ex);
            }
        }
    }
}