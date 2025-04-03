using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using DSED_M07_TraitementCommande_producteur;
using System.Text.Json;

namespace _M07_TraitementCommande_facturation
{
    public  class Consommateur_Facturation
    {
        static void Main(string[] args)
        {
            string[] requetesSujets = { "commande.placee.*" };
            string fileMessage = "m07-facturation";
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
                                string contenu = TraitementFactures.FacturerCommandePremium(commande);
                                string nomCommande = "";
                                nomCommande += DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + "ReferenceCommande_Facture.json";
                                File.AppendAllText(nomCommande.Trim(), $"{contenu}{Environment.NewLine}");
                                Console.WriteLine($"Message reçu  {message}");
                            }
                            else
                            {
                                byte[] body = ea.Body.ToArray();
                                string message = System.Text.Encoding.UTF8.GetString(body);
                                Commande commande = JsonSerializer.Deserialize<Commande>(message);
                                string contenu = TraitementFactures.FacturerCommande(commande);
                                string nomCommande = "";
                                nomCommande += DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + "ReferenceCommande_Facture.json";
                                File.AppendAllText(nomCommande.Trim(), $"{contenu}{Environment.NewLine}");
                                Console.WriteLine($"Message reçu  {message}");
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
