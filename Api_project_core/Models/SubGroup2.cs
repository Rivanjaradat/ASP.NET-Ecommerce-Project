using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class SubGroup2
    {
        public int Id { get; set; }
        public string Name { get; set; }
       public ICollection<Items> Items { get; set; } = new HashSet<Items>();
        public MainGroup MainGroup { get; set; }
        [ForeignKey(nameof(MainGroup))]
        public int MainGroupId { get; set; }
        public SubGroup SubGroup { get; set; }
        [ForeignKey(nameof(SubGroup))]
        public int SubGroupId { get; set; }
    }
}
