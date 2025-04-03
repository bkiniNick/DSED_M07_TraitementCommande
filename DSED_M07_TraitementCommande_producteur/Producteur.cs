using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace DSED_M07_TraitementCommande_producteur
{
    public class Producteur
    {
        static void Main(string[] args)
        {
           
               
                var factory = new ConnectionFactory() { HostName = "localhost" };

                // Utilisation de "using" pour garantir la fermeture de la connexion et du canal
                using (IConnection connection = factory.CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        // 2. Déclarer un Exchange de type "topic"
                        string exchangeName = "m07-commandes";
                        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

                    // 3. Publier des messages avec des routing keys
                    string[] routingKeys = { "commande.placee.normal" , "commande.placee.premium" };
                        foreach (var routingKey in routingKeys)
                        {
                            Commande message = new Commande();
                        string messageJson = JsonSerializer.Serialize(message);

                            var body = Encoding.UTF8.GetBytes(messageJson);

                            // Publier le message avec la routing key
                            channel.BasicPublish(exchange: exchangeName,
                                                 routingKey: routingKey,
                                                 basicProperties: null,
                                                 body: body);

                            Console.WriteLine($"[x] Envoyé : '{message}' avec Routing Key '{routingKey}'");
                        }

                        Console.WriteLine("Messages envoyés. Appuyez sur une touche pour quitter.");
                        Console.ReadLine();
                    }
                }
            }
    }
}
