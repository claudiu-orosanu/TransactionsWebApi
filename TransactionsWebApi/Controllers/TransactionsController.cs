using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using TransactionsWebApi.Data;
using TransactionsWebApi.Models;

namespace TransactionsWebApi.Controllers
{
    public class TransactionsController : ApiController
    {
        DataAccess dataAccess;

        public TransactionsController()
        {
            dataAccess = new DataAccess();
        }

        [HttpGet]
        [Route("transactions")]
        public async Task<IEnumerable<Transaction>> Get()
        {
            var transactions = await dataAccess.GetTransactions();
            return transactions;
        }

        [HttpGet]
        [Route("transactions")]
        public async Task<IHttpActionResult> Get(int user, int day, int threshold)
        {
            var filter = CreateFilterByUserDayThreshold(user, day, threshold);
            var transactions = await dataAccess.GetTransaction(filter);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        private FilterDefinition<Transaction> CreateFilterByUserDayThreshold(int user, int day, int threshold)
        {
            var builder = Builders<Transaction>.Filter;
            var filter1 = builder.Eq("sender", user) | builder.Eq("receiver", user);
            var filter2 = builder.Gt("sum", threshold);
            var filter3 = builder.Eq("timestamp", day);
            var filter = filter1 & filter2 & filter3;
            return filter;
        }

        [HttpGet]
        [Route("balance")]
        public async Task<IHttpActionResult> GetBalance(int user, int since, int until)
        {
            var filter = CreateFilterByUserSinceUntil(user, since, until);
            var transactions = await dataAccess.GetBalance(filter);
            if (transactions == null)
            {
                return NotFound();
            }

            var balance = CalculateBalance(user, transactions);
            return Ok(balance);
        }

        private FilterDefinition<Transaction> CreateFilterByUserSinceUntil(int user, int since, int until)
        {
            var builder = Builders<Transaction>.Filter;
            var filter1 = builder.Eq("sender", user) | builder.Eq("receiver", user);
            var filter2 = builder.Gt("timestamp", since);
            var filter3 = builder.Lt("timestamp", until);
            var filter = filter1 & filter2 & filter3;
            return filter;
        }

        private int CalculateBalance(int user, IEnumerable<Transaction> transactions)
        {
            int balance = 0;
            foreach (var t in transactions)
            {
                if (user == t.Sender)
                    balance -= t.Sum;
                else if (user == t.Receiver)
                    balance += t.Sum;
            }

            return balance;
        }

        [HttpPost]
        [Route("transactions")]
        public async Task<IHttpActionResult> Post([FromBody]Transaction transaction)
        {
            var result = await dataAccess.Create(transaction);
            return Ok(result);
        }
    }
}
