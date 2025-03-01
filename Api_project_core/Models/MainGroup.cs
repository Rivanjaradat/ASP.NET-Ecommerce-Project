using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class MainGroup
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public ICollection<Items> Items { get; set; } = new HashSet<Items>();
        public ICollection<SubGroup> SubGroup { get; set; } = new HashSet<SubGroup>();
        public ICollection<SubGroup2> SubGroup2 { get; set; } = new HashSet<SubGroup2>();


    }
}
