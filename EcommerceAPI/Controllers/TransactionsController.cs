using EcommerceAPI.Models;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [SwaggerResponse(200, "The list of transactions was successfully returned.", typeof(List<Transaction>))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "The specified transaction was found and returned.", typeof(Transaction))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        [SwaggerResponse(404, "The specified transaction was not found.")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction is null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        [SwaggerResponse(201, "The transaction was successfully created.", typeof(Transaction))]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.")]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Create([FromBody] Transaction transaction)
        {
            try
            {
                await _transactionService.AddTransactionAsync(transaction);

                return CreatedAtAction("GetById", new { id = transaction.Id }, transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(204, "The specified transaction was successfully updated.")]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.")]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Update(int id, [FromBody] Transaction transaction)
        {
            try
            {
                transaction.Id = id;
                await _transactionService.UpdateTransactionAsync(transaction);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "The specified transaction was successfully deleted.")]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.")]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _transactionService.DeleteTransactionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}