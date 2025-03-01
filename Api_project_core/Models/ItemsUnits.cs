using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class ItemsUnits
    {
        [ForeignKey(nameof(Items))]
        public int Item_Id { get; set; }

        [ForeignKey(nameof(Units))]
        public int Unit_Id { get; set; }
        public int Factor { get; set; }
        public Items Items { get; set; }
        public Units Units { get; set; }


    }
}
