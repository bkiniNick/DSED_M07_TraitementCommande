using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using DSED_M07_TraitementCommande_producteur;
using System.Text.Json;

namespace DSED_M07_TraitementCommande_Expedition
{
    public  class Consommateur_Expedition
    {
        static void Main(string[] args)
        {
            string[] requetesSujets = { "commande.placee.*" };
            string fileMessage = "m07-preparation-expedition";
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

                    {
                        EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                        consumateur.Received += (model, ea) =>
                        {

                            if (ea.RoutingKey == "commande.placee.premium")
                            {
                                byte[] body = ea.Body.ToArray();
                                string message = System.Text.Encoding.UTF8.GetString(body);
                                Commande commande = JsonSerializer.Deserialize<Commande>(message);
                                Console.WriteLine($"commande Premium ");
                                foreach (Article a in commande.Articles)
                                {
                                    Console.WriteLine($"article:  {a.name} qte {a.quantite}");
                                }

                            }
                            else
                            {
                                byte[] body = ea.Body.ToArray();
                                string message = System.Text.Encoding.UTF8.GetString(body);
                                Commande commande = JsonSerializer.Deserialize<Commande>(message);
                                Console.WriteLine($"commande Normale ");
                                foreach (Article a in commande.Articles)
                                {
                                    Console.WriteLine($"article:  {a.name} qte {a.quantite}");
                                }

                            }
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
}
