using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Interfaces
{
   public  interface IUnitOfWork
    {
        IAuthRepository authRepository { get; }
        IInvoiceRepository invoiceRepository { get; }
        ITemsIRepository itemRepository { get; }
        ICartRepository cartRepository { get; }
        Task <int> SaveAsync();


    }
}
