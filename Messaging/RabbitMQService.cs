using RabbitMQ.Client;
using System.Text;

namespace PatientRecovery.Shared.Messaging
{
    public class RabbitMQService : IRabbitMQService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ExchangeName = "patient_recovery";

        public RabbitMQService(string hostName = "localhost")
        {
            var factory = new ConnectionFactory { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Topic);
        }

        public void PublishMessage(string message, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: ExchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: body);
        }

        public void PublishMedicationAdded(string message)
        {
            PublishMessage(message, "medication.added");
        }

        public void PublishVitalSignsAlert(string message)
        {
            PublishMessage(message, "vitalsigns.alert");
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}