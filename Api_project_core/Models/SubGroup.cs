using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class SubGroup
    {
        public int Id { get; set; }
        public double Price { get; set; }
      
        public MainGroup MainGroup { get; set; }
        [ForeignKey(nameof(MainGroup))]
        public int MainGroupId { get; set; }
        public ICollection<Items> Items { get; set; } = new HashSet<Items>();
        public ICollection<SubGroup2> SubGroup2 { get; set; } = new HashSet<SubGroup2>();
    }
}
