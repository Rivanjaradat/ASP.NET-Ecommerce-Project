using Api_project_core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Interfaces
{
   public interface IInvoiceRepository
    {
        Task<string> CreateInvoiceAsync(int customer_Id);
        Task<InvoiceRecieptDTO> GetInvoiceReciept(int customerId,int invoice_Id);
    }
}
