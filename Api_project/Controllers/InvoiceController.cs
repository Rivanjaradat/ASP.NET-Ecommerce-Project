using Api_project.HelperFunctions;
using Api_project_core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        //private readonly IInvoiceRepository invoiceRepository;

        public InvoiceController( IUnitOfWork unitOfWork  /*IInvoiceRepository invoiceRepository*/)
        {
            UnitOfWork = unitOfWork;
            //this.invoiceRepository = invoiceRepository;
        }

        public IUnitOfWork UnitOfWork { get; }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoiceAsync()
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
           
                var userId = ExtractClaims.ExtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid token");
                }
                var result = await UnitOfWork.invoiceRepository.CreateInvoiceAsync(userId.Value);
                if (result.StartsWith( "Invoice created successfully"))
                {
                    return Ok(result);
                }
                return BadRequest(result);

            
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetInvoiceReciept(int invoice_Id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }
            var userId = ExtractClaims.ExtractUserId(token);
            if (!userId.HasValue)
            {
                return Unauthorized("Invalid token");
            }
            var result = await UnitOfWork.invoiceRepository.GetInvoiceReciept(userId.Value, invoice_Id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Invoice not found");
        }

    }
}
