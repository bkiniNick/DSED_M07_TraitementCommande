using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using DSED_M07_TraitementCommande_producteur;
namespace DSED_M07_TraitementCommande_facturation
{
    public class Consommateur_Journal
    {
        static void Main(string[] args)
        {
            string[] requetesSujets = { "#" };
            string fileMessage = "m07-journal";    
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                    exchange: "m07-commandes",
                    type: "topic",
                    durable: false,
                    autoDelete: false
                    );
                    channel.QueueDeclare(
                    fileMessage,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: fileMessage, exchange: "m07-commandes",
                        routingKey: requeteSujet);
                    }
                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                    consumateur.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = System.Text.Encoding.UTF8.GetString(body);
                        Commande commande = JsonSerializer.Deserialize<Commande>(message);
                        string nomCommande = "";
                        nomCommande += DateTime.Now.Year.ToString()+ DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_"+ DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_"+"Nouveau" + "_"+commande.Guid+".json";
                        File.AppendAllText(nomCommande.Trim(), $"{message}{Environment.NewLine}");
                        Console.WriteLine($"Message reçu  {message}");
                    };
                    channel.BasicConsume(queue: fileMessage,
                    autoAck: true,
                    consumer: consumateur);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
