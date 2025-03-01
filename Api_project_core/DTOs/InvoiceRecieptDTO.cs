using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.DTOs
{
    public class InvoiceRecieptDTO
    {
        public int invoice_Id { get; set; }
        public int customer_Id { get; set; }
        public double total_price { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<InvoiceItemDTO> items { get; set; }

    }

}
