using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using TransactionsWebApi.Data;
using TransactionsWebApi.Models;
using MongoDB.Bson;

namespace TransactionsWebApi.Controllers
{
    public class BalanceController : ApiController
    {
        DataAccess dataAccess;

        public BalanceController()
        {
            dataAccess = new DataAccess();
        }

        [HttpGet]
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
            var filter2 = builder.Gte("timestamp", since);
            var filter3 = builder.Lte("timestamp", until);
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
    }
}
