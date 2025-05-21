namespace PatientRecovery.Shared.Messaging
{
    public interface IRabbitMQService
    {
        void PublishMessage(string message, string routingKey);
        void PublishMedicationAdded(string message);
        void PublishVitalSignsAlert(string message);
    }
}