using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class Zones
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Users> Users { get; set; } = new HashSet<Users>();
        

      
        [ForeignKey(nameof(Goverments))]
        public int Gov_Id { get; set; }
        public Goverments Goverments { get; set; }

        [ForeignKey(nameof(Cities))]
        public int City_Id { get; set; }
        public Cities Cities { get; set; }

   
        public ICollection<Stores> Stores { get; set; } = new HashSet<Stores>();

    }
}
