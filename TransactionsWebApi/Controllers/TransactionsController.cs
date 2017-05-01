using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using TransactionsWebApi.Data;
using TransactionsWebApi.Models;
using MongoDB.Bson;

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
        public async Task<IEnumerable<Transaction>> Get()
        {
            var transactions = await dataAccess.GetTransactions();
            return transactions;
        }

        [HttpGet]
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
        
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Transaction transaction)
        {
            var result = await dataAccess.InsertOne(transaction);
            return Ok(result);
        }
    }
}
