
using LSB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSB.Repository
{
    public class TransactionRepository
    {
        public TransactionRepository()
        {

        }

        public async Task<List<Transaction>> GetTransactionsByUserId(string userId)
        {

            return new List<Transaction>();

        }

        public async Task<Transaction> GetTransactionById(string transactionId)
        {

            return new Transaction();
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            return transaction;
        }

        public async Task<Transaction> UpdateTransaction(string transactionId, Transaction transaction)
        {
            return transaction;
        }

        public async Task DeleteTransaction(string transactionId)
        {
            return;
        }



    }

}
