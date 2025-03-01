using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.DTOs
{
    public   class UserCartItemDTO
    {
        public string name { get; set; }
        public double price { get; set; }
        public string itemUnit { get; set; }
        public double quantity { get; set; }

    }
}
