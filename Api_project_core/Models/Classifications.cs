using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class Classifications
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Users> Users { get; set; } = new HashSet<Users>();

    }
}
