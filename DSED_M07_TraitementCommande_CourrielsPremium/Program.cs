using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using DSED_M07_TraitementCommande_producteur;

namespace DSED_M07_TraitementCommande_CourrielsPremium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Déclaration des sujets à écouter dans l'échange RabbitMQ
            string[] requetesSujets = { "commande.placee.premium" };

            // Nom de la file d'attente pour les messages reçus
            string fileMessage = "m07-courriel-premium";

            // Création d'une connexion RabbitMQ vers le serveur local
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

            // Établissement de la connexion avec le serveur RabbitMQ
            using (IConnection connection = factory.CreateConnection())
            {
                // Ouverture d'un canal de communication avec RabbitMQ
                using (IModel channel = connection.CreateModel())
                {
                    // Déclaration de l'échange "m07-commandes" de type "topic"
                    channel.ExchangeDeclare(
                        exchange: "m07-commandes",
                        type: "topic",
                        durable: false,
                        autoDelete: false
                    );

                    // Déclaration de la file d'attente pour stocker les messages premium
                    channel.QueueDeclare(
                        fileMessage,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    // Liaison de la file d'attente à l'échange avec les sujets spécifiés
                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: fileMessage, exchange: "m07-commandes",
                            routingKey: requeteSujet);
                    }

                    // Création d'un consommateur d'événements RabbitMQ
                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);

                    // Gestion des messages reçus par le consommateur
                    consumateur.Received += (model, ea) =>
                    {
                        // Récupération du corps du message
                        byte[] body = ea.Body.ToArray();

                        // Conversion du message en chaîne UTF-8
                        string message = System.Text.Encoding.UTF8.GetString(body);

                        // Désérialisation du message JSON en objet Commande
                        Commande commande = JsonSerializer.Deserialize<Commande>(message);

                        // Affichage de la commande reçue dans la console
                        Console.WriteLine($"Commande premium -> {commande.Guid}");
                    };

                    // Abonnement à la file d'attente pour consommer les messages automatiquement
                    channel.BasicConsume(
                        queue: fileMessage,
                        autoAck: true, // Accusé de réception automatique
                        consumer: consumateur
                    );

                    // Indication à l'utilisateur que le programme est en attente de messages
                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine(); // Attente d'une entrée utilisateur avant fermeture
                }
            }
        }
    }
}