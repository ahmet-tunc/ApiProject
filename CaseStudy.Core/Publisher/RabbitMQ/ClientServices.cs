using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.Publisher.RabbitMQ
{
    public class ClientServices : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "PdfDirectExchange";
        public static string Routing = "pdf-creator-route"; // Bunlar farklı bir konumda tutulmalı
        public static string QueueName = "pdf-creator";

        private readonly ILogger<ClientServices> _logger;

        public ClientServices(ConnectionFactory connectionFactory, ILogger<ClientServices> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            //_channel.IsOpen == true
            if (_channel is { IsOpen: true }) //İlgili kanalın varlığına dair kontrol
            {
                return _channel;
            }
            _channel = _connection.CreateModel(); //yeni kanal oluştur
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false); //Direct exchange tipine sahip | routing-key bazlı veri gönderimi
            _channel.QueueDeclare(QueueName, true, false, false, null); //Kuyruk ismi verildi
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: Routing);
            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close(); //kanalı kapat ve yok et
            _channel?.Dispose();
            _connection?.Close(); //bağlantıyı kapat ve yok et
            _connection?.Dispose();
            _logger.LogInformation("RabbitMQ ile bağlantı koptu...");
        }
    }
}
