using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Models
{
    public class Users : IdentityUser<int>
    {
        [ForeignKey(nameof(Goverments))]
        public int Gov_Id { get; set; }

        public Goverments Goverments { get; set; }
        [ForeignKey(nameof(Cities))]
        public int City_Id { get; set; }
        public Cities Cities { get; set; }
        [ForeignKey(nameof(Zones))]
        public int Zone_Id { get; set; }
        public Zones Zones { get; set; }
        [ForeignKey(nameof(Classifications))]
        public int Class_Id { get; set; }
        public Classifications Classifications { get; set; }
        public ICollection<CustomerStores> CustomerStores { get; set; } = new HashSet<CustomerStores>();


    }
}
