using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CaseStudy.Core.Publisher.RabbitMQ
{
    public class ClientPublisher
    {
        private readonly ClientServices _clientServices;

        public ClientPublisher(ClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        public void Publisher<T>(T message) //Tekli veri aktarımı
        {
            var channel = _clientServices.Connect();
            var bodyString = JsonSerializer.Serialize(message);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties(); //Veri kaybolmasını önler, fiziksel olarak kaydedilmesini sağlar
            properties.Persistent = true;
            channel.BasicPublish(ClientServices.ExchangeName, ClientServices.Routing, basicProperties: properties, body: bodyByte); //İlgili veriyi kuyruğa aktarır
        }


        public void PublisherList<T>(List<T> messages) //Listeyi kuyruğa aktarır
        {
            var channel = _clientServices.Connect();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            foreach (var message in messages)
            {
                var bodyString = JsonSerializer.Serialize(message);
                var bodyByte = Encoding.UTF8.GetBytes(bodyString);

                channel.BasicPublish(ClientServices.ExchangeName, ClientServices.Routing, basicProperties: properties, body: bodyByte);
            }
        }

    }
}