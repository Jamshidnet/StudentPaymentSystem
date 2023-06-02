using Application.DTO.PaymentDtos;
using Application.Interfaces;
using Application.ResponseModel;
using Domein.Models;
using Microsoft.AspNetCore.Mvc;
using StudentPaymentSystem.Controllers;

namespace PaymentPaymentSystem.Controllers
{
    public class PaymentController : ApiController<Payment>
    {
        private readonly IPaymentRepository paymentService;
        public PaymentController(IPaymentRepository _paymentService)
        {
            paymentService = _paymentService;
        }

        [HttpGet]
        [Route("[action]")]
        //  [Authorize(Roles = "PaymentGet")]
        public async Task<ActionResult<ResponseCore<GetPaymentDto>>> GetById(Guid id)
        {
            Payment? payment = await paymentService.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound(new ResponseCore<Payment?>(false, id + " not found!"));
            }
            GetPaymentDto mappedRole = _mapper.Map<GetPaymentDto>(payment);
            return Ok(new ResponseCore<GetPaymentDto?>(mappedRole));
        }


        [HttpGet]
        [Route("[action]")]
        //  [Authorize(Roles = "PaymentGetAll")]
        public ActionResult<ResponseCore<GetPaymentDto>> GetAllPayments(int page = 1, int pageSize = 10)
        {
            IEnumerable<Payment>? roles = paymentService.GetAllAsync(x => true);

            IEnumerable<GetPaymentDto> mappedRoles = _mapper.Map<IEnumerable<GetPaymentDto>>(roles);

            return Ok(new ResponseCore<IEnumerable<GetPaymentDto>>(mappedRoles));
        }

        [HttpPost]
        [Route("[action]")]
        //  [Authorize(Roles = "PaymentCreate")]
        public async Task<ActionResult<ResponseCore<GetPaymentDto>>> Create([FromBody] CreatePaymentDTO payment)
        {

            var mappedPayment = _mapper.Map<Payment>(payment);
            var createdPayment = await paymentService.CreateAsync(mappedPayment);
            if (createdPayment is null || mappedPayment is null) return BadRequest();

            var GetPayment = _mapper.Map<GetPaymentDto>(createdPayment);
            return Ok(new ResponseCore<GetPaymentDto>(GetPayment));
        }

        [HttpPut]
        [Route("[action]")]
        //  [Authorize(Roles = "PaymentUpdate")]
        public async Task<ActionResult<ResponseCore<GetPaymentDto>>> Update([FromBody] UpdatePaymentDto entity)
        {
            var mappedPayment = _mapper.Map<Payment>(entity);
            var updatedPayment = await paymentService.UpdateAsync(mappedPayment);
            if (updatedPayment is null || mappedPayment is null) return BadRequest();

            var getRole = _mapper.Map<GetPaymentDto>(updatedPayment);
            return Ok(new ResponseCore<GetPaymentDto>(getRole));

        }

        [HttpDelete]
        [Route("[action]")]
        //[Authorize(Roles = "PaymentDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            return await paymentService.DeleteAsync(Id) ?
           Ok(new ResponseCore<bool>(true))
         : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
