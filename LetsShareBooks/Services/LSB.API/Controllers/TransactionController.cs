using LSB.Models;
using LSB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LSB.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private TransactionRepository _transactionRepository;

        public TransactionController(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        // GET: api/<TransactionController>
        [HttpGet, Route("api/transactions/{transactionId}")]
        public async Task<IActionResult> GetById(string transactionId)
        {
            var transaction = _transactionRepository.GetTransactionById(transactionId);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        // GET api/<TransactionController>/5
        [HttpGet, Route("api/transactions/user/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(string userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserId(userId);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        // POST api/<TransactionController>
        [HttpPost, Route("api/transactions")]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] Transaction transaction)
        {
            var transactionCreated = await _transactionRepository.CreateTransaction(transaction);
            if (transactionCreated == null)
            {
                return NotFound();
            }
            return Ok(transactionCreated);
        }

        // PUT api/<TransactionController>/5
        [HttpPut, Route("api/transactions/{transactionId}")]
        public async Task<IActionResult> UpdateTransactionAsync(string transactionId, [FromBody] Transaction transaction)
        {
            var transactionUpdated = await _transactionRepository.UpdateTransaction(transactionId, transaction);
            if (transactionUpdated == null)
            {
                return NotFound();
            }
            return Ok(transactionUpdated);
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete, Route("api/transactions/{transactionId}")]
        public async Task<IActionResult> DeleteTransactionAsync(string transactionId)
        {
            await _transactionRepository.DeleteTransaction(transactionId);
            return Ok();
        }
    }
}
