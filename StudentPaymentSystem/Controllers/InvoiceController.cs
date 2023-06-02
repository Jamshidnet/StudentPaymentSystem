using Application.DTO.InvoiceDtos;
using Application.Interfaces;
using Application.ResponseModel;
using Domein.Models;
using Microsoft.AspNetCore.Mvc;
using StudentPaymentSystem.Controllers;

namespace InvoicePaymentSystem.Controllers
{
    public class InvoiceController : ApiController<Invoice>
    {
        private readonly IInvoiceRepository invoiceService;
        public InvoiceController(IInvoiceRepository _invoiceService)
        {
            invoiceService = _invoiceService;
        }

        [HttpGet]
        [Route("[action]")]
        //  [Authorize(Roles = "InvoiceGet")]
        public async Task<ActionResult<ResponseCore<GetInvoiceDto>>> GetById(Guid id)
        {
            Invoice? invoice = await invoiceService.GetByIdAsync(id);

            if (invoice == null)
            {
                return NotFound(new ResponseCore<Invoice?>(false, id + " not found!"));
            }
            GetInvoiceDto mappedRole = _mapper.Map<GetInvoiceDto>(invoice);
            return Ok(new ResponseCore<GetInvoiceDto?>(mappedRole));
        }


        [HttpGet]
        [Route("[action]")]
        //  [Authorize(Roles = "InvoiceGetAll")]
        public ActionResult<ResponseCore<GetInvoiceDto>> GetAllInvoices(int page = 1, int pageSize = 10)
        {
            IEnumerable<Invoice>? roles = invoiceService.GetAllAsync(x => true);

            IEnumerable<GetInvoiceDto> mappedRoles = _mapper.Map<IEnumerable<GetInvoiceDto>>(roles);

            return Ok(new ResponseCore<IEnumerable<GetInvoiceDto>>(mappedRoles));
        }

        [HttpPost]
        [Route("[action]")]
        //  [Authorize(Roles = "InvoiceCreate")]
        public async Task<ActionResult<ResponseCore<GetInvoiceDto>>> Create([FromBody] CreateInvoiceDto invoice)
        {

            var mappedInvoice = _mapper.Map<Invoice>(invoice);
            var createdInvoice = await invoiceService.CreateAsync(mappedInvoice);
            if (createdInvoice is null || mappedInvoice is null) return BadRequest();

            var GetInvoice = _mapper.Map<GetInvoiceDto>(createdInvoice);
            return Ok(new ResponseCore<GetInvoiceDto>(GetInvoice));
        }

        [HttpPut]
        [Route("[action]")]
        //  [Authorize(Roles = "InvoiceUpdate")]
        public async Task<ActionResult<ResponseCore<GetInvoiceDto>>> Update([FromBody] UpdateInvoiceDto entity)
        {
            var mappedInvoice = _mapper.Map<Invoice>(entity);
            var updatedInvoice = await invoiceService.UpdateAsync(mappedInvoice);
            if (updatedInvoice is null || mappedInvoice is null) return BadRequest();

            var getRole = _mapper.Map<GetInvoiceDto>(updatedInvoice);
            return Ok(new ResponseCore<GetInvoiceDto>(getRole));

        }

        [HttpDelete]
        [Route("[action]")]
        //[Authorize(Roles = "InvoiceDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            return await invoiceService.DeleteAsync(Id) ?
           Ok(new ResponseCore<bool>(true))
         : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
