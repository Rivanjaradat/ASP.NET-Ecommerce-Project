using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class InvItemStores
    {

        [ForeignKey(nameof(Items))]
        public int Item_Id { get; set; }
        [ForeignKey(nameof(Store))]
        public int Store_Id { get; set; }
        public Items Items { get; set; }
        public Stores Store { get; set; }
        public double Balance { get; set; }
        public int Factor { get; set; }
        public double ReservedQuantity { get; set; }
        public DateTime LastUpdated { get; set; }


    } 
}
