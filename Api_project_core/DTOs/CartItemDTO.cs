using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.DTOs
{
    public class CartItemDTO
    {
        public  int ItemCode {get;set;}
     
        public int UnitCode {get;set;}
        public double Quantity {get;set;}
        public int Store_Id {get;set;}


    }
}
