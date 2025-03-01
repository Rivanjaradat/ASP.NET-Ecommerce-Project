using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class Units
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ItemsUnits> ItemsUnits { get; set; } = new HashSet<ItemsUnits>();

    }
}
