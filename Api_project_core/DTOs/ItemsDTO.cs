using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Api_project_core.DTOs
{
    public class ItemsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public List <string> ItemUnits { get; set; }
        public List <string> Stores{ get; set; }



    }
}
