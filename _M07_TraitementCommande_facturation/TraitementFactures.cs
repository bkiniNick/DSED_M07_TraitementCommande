using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSED_M07_TraitementCommande_producteur;


namespace _M07_TraitementCommande_facturation
{
    public static  class TraitementFactures
    {
        public static string FacturerCommande(Commande commande)
        {
            decimal total = 0;
            decimal totalAvecTaxes = 0;

            foreach (Article article in commande.Articles)
            {
                total += article.prix * article.quantite;
            }
            totalAvecTaxes = 14.995m * total / 100 + total;
            return $"Facture sans taxes  pour {commande.ClientName} : {total} $ \n Facture avec taxes  pour {commande.ClientName} : {totalAvecTaxes} $" ;
        }
        public static string FacturerCommandePremium(Commande commande)
        {
            decimal total = 0;
            decimal totalAvecTaxes = 0;

            foreach (Article article in commande.Articles)
            {
                total += article.prix * article.quantite;
            }
            total -= 5 * total / 100;
            totalAvecTaxes -= 5 * total / 100; 
            totalAvecTaxes = 14.995m * total / 100 + total;
            
            return $"Facture sans taxes  pour {commande.ClientName} : {total} $ \n Facture avec taxes  pour {commande.ClientName} : {totalAvecTaxes} $";
        }




    }
}
