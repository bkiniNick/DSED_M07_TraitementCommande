using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED_M07_TraitementCommande_producteur
{
    public class Commande
    {
        public Guid Guid { get; set; }
        public string ClientName { get; set; }
        public List<Article> Articles { get; set; }


        public Commande()
        {
            Guid = Guid.NewGuid();
            ClientName = "";
            Articles = new List<Article>();

        }

        public Commande (string clientName, List<Article> articles)
        {
            Guid = Guid.NewGuid();
            ClientName = clientName;
            Articles = articles;
        }   
    }
}
