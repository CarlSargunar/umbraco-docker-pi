using RabbitMQ.Client;
using System.Text;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbDockPi.Controllers
{
    public class RMQController : UmbracoApiController
    {
        public RMQController()
        {

        }

        public Microsoft.AspNetCore.Mvc.RedirectResult SendMessage(string name)
        {
            try
            {
                // TODO : Put the hostname in Config
                // var factory = new ConnectionFactory() { HostName = "192.168.0.144" };
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = $"Hey {name}! Happy 24 Days of Umbraco. #h5yr!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                }
            }
            catch
            {
                
            }

            // Being Lazy :-)
            return Redirect("/thank-you/");
        }

    }
}
