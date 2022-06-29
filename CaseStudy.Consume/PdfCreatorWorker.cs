using CaseStudy.Consume.Constant;
using CaseStudy.Consume.Services;
using CaseStudy.Core.Helpers;
using CaseStudy.Core.SharedModels;
using CaseStudy.Entities.Concrete.ApiResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CaseStudy.Consume
{
    public class PdfCreatorWorker : BackgroundService
    {
        private readonly ILogger<PdfCreatorWorker> _logger;
        private readonly ClientService _clientService;
        private readonly IConfiguration _configuration;
        private readonly string _getFileDataUrl;
        private readonly string _pdfPath;
        private IDictionary<string, string> _parameters;

        private IModel _channel;
        public PdfCreatorWorker(ILogger<PdfCreatorWorker> logger, ClientService clientService, IConfiguration configuration)
        {
            _logger = logger;
            _clientService = clientService;
            _configuration = configuration;
            _getFileDataUrl = _configuration.GetSection("GetFileData").Value;
            _pdfPath = _configuration.GetSection("PdfPath").Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(Messages.StartQueue, ClientService.QueueName);
            _channel = _clientService.Connect();
            _channel.BasicQos(0, 1, false); //Kuyruktaki mesajların, subscriberlara kaçar kaçar gideceği
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel); //İlgili kanal için tüketici oluşturur
            _channel.BasicConsume(ClientService.QueueName, false, consumer); //Mesaj subscriberlara aktarıldığında, mesajı alıp almadığımıza dair bildirim vermek amacıyla kullanırız. Buna göre siler veya bildirim bekler
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }


        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var document = JsonSerializer.Deserialize<PdfFile>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            try
            {
                _logger.LogInformation(Messages.DocumentInfo, new object[] { document.DocumentName, document.Guid });
                _parameters = new Dictionary<string,string>{{ "filename", document.DocumentName }};
                var file = await ApiHelper<FileData>.GetDataByFilter(_getFileDataUrl, _parameters);

                if (file.Success)
                {
                    //byte[] fileContent = Convert.FromBase64String(file.Data);
                    File.WriteAllBytes( _pdfPath + document.Guid + ".pdf", file.Data);
                }

                //PdfFileList.PdfList.Add(new PdfFile
                //{
                //    DocumentName = document.DocumentName,
                //    Guid = document.Guid
                //});

                _channel.BasicAck(@event.DeliveryTag, false); //Kuyruktan mesajın silinmesini sağlar
                await Task.Delay(3500);
            }
            catch (Exception)
            {
                _logger.LogError(Messages.DocumentInfo,document.DocumentName,document.Guid);
            }
        }

    }
}
