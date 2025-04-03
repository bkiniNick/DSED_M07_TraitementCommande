using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED_M07_TraitementCommande_producteur
{
    public  class Article
    {
        public Guid Guid { get; set; }
        public string name { get; set; }
        public decimal prix { get; set; }
        public int quantite { get; set; }

        public Article() {
        Guid= Guid.NewGuid();
        }



    }
}
