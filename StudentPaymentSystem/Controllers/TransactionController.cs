using Application.DTO.TransactionDtos;
using Application.Interfaces;
using Application.ResponseModel;
using Domein.Models;
using Microsoft.AspNetCore.Mvc;
using StudentPaymentSystem.Controllers;

namespace TransactionPaymentSystem.Controllers
{
    public class TransactionController :ApiController<Transaction>
    {
        private readonly ITransactionRepository transactionService;
        public TransactionController(ITransactionRepository _transactionService)
        {
            transactionService = _transactionService;
        }

        [HttpGet]
        [Route("[action]")]
        //  [Authorize(Roles = "TransactionGet")]
        public async Task<ActionResult<ResponseCore<GetTransactionDto>>> GetById(Guid id)
        {
            Transaction? transaction = await transactionService.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound(new ResponseCore<Transaction?>(false, id + " not found!"));
            }
            GetTransactionDto mappedRole = _mapper.Map<GetTransactionDto>(transaction);
            return Ok(new ResponseCore<GetTransactionDto?>(mappedRole));
        }


        [HttpGet]
        [Route("[action]")]
        //  [Authorize(Roles = "TransactionGetAll")]
        public ActionResult<ResponseCore<GetTransactionDto>> GetAllTransactions(int page = 1, int pageSize = 10)
        {
            IEnumerable<Transaction>? roles = transactionService.GetAllAsync(x => true);

            IEnumerable<GetTransactionDto> mappedRoles = _mapper.Map<IEnumerable<GetTransactionDto>>(roles);

            return Ok(new ResponseCore<IEnumerable<GetTransactionDto>>(mappedRoles));
        }

        [HttpPost]
        [Route("[action]")]
        //  [Authorize(Roles = "TransactionCreate")]
        public async Task<ActionResult<ResponseCore<GetTransactionDto>>> Create([FromBody] CreateTransactionDto transaction)
        {

            var mappedTransaction = _mapper.Map<Transaction>(transaction);
            var createdTransaction = await transactionService.CreateAsync(mappedTransaction);
            if (createdTransaction is null || mappedTransaction is null) return BadRequest();

            var GetTransaction = _mapper.Map<GetTransactionDto>(createdTransaction);
            return Ok(new ResponseCore<GetTransactionDto>(GetTransaction));
        }

        [HttpPut]
        [Route("[action]")]
        //  [Authorize(Roles = "TransactionUpdate")]
        public async Task<ActionResult<ResponseCore<GetTransactionDto>>> Update([FromBody] UpdateTransactionDto entity)
        {
            var mappedTransaction = _mapper.Map<Transaction>(entity);
            var updatedTransaction = await transactionService.UpdateAsync(mappedTransaction);
            if (updatedTransaction is null || mappedTransaction is null) return BadRequest();

            var getRole = _mapper.Map<GetTransactionDto>(updatedTransaction);
            return Ok(new ResponseCore<GetTransactionDto>(getRole));

        }

        [HttpDelete]
        [Route("[action]")]
        //[Authorize(Roles = "TransactionDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            return await transactionService.DeleteAsync(Id) ?
           Ok(new ResponseCore<bool>(true))
         : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
