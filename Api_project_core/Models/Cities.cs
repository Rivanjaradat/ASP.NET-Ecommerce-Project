using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Users> Users { get; set; } = new HashSet<Users>();
        [ForeignKey("Goverments")]
        public int GovermentId { get; set; }
        public Goverments Goverments { get; set; }
        public ICollection<Zones> Zones { get; set; } = new HashSet<Zones>();
        public ICollection<Stores> Stores { get; set; } = new HashSet<Stores>();
    }
}
