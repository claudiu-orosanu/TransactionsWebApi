using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using TransactionsWebApi.Data;
using TransactionsWebApi.Models;
using MongoDB.Bson;

namespace TransactionsWebApi.Controllers
{
    public class InsertDataController : ApiController
    {
        DataAccess dataAccess;

        public InsertDataController()
        {
            dataAccess = new DataAccess();
        }

        [HttpPost]
        public async Task<IHttpActionResult> InsertInitialData()
        {
            var result = await InsertData();
            return Ok(result);
        }

        private async Task<IEnumerable<Transaction>> InsertData()
        {
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Sender=1,
                    Receiver=2,
                    Timestamp=1,
                    Sum=5000
                },
                new Transaction
                {
                    Sender=1,
                    Receiver=3,
                    Timestamp=2,
                    Sum=4000
                },
                new Transaction
                {
                    Sender=2,
                    Receiver=3,
                    Timestamp=3,
                    Sum=2000
                },
                new Transaction
                {
                    Sender=3,
                    Receiver=4,
                    Timestamp=4,
                    Sum=3000
                },
                new Transaction
                {
                    Sender=4,
                    Receiver=5,
                    Timestamp=5,
                    Sum=1500
                },
                new Transaction
                {
                    Sender=5,
                    Receiver=1,
                    Timestamp=6,
                    Sum=1000
                },
                new Transaction
                {
                    Sender=4,
                    Receiver=1,
                    Timestamp=7,
                    Sum=1600
                },
                new Transaction
                {
                    Sender=2,
                    Receiver=6,
                    Timestamp=8,
                    Sum=1200
                },
                new Transaction
                {
                    Sender=3,
                    Receiver=6,
                    Timestamp=9,
                    Sum=700
                },
                new Transaction
                {
                    Sender=5,
                    Receiver=8,
                    Timestamp=10,
                    Sum=4000
                },
                new Transaction
                {
                    Sender=8,
                    Receiver=7,
                    Timestamp=11,
                    Sum=2300
                },
                new Transaction
                {
                    Sender=2,
                    Receiver=7,
                    Timestamp=12,
                    Sum=1300
                },
                new Transaction
                {
                    Sender=7,
                    Receiver=4,
                    Timestamp=13,
                    Sum=2000
                },
                new Transaction
                {
                    Sender=5,
                    Receiver=9,
                    Timestamp=14,
                    Sum=5000
                },
                new Transaction
                {
                    Sender=7,
                    Receiver=5,
                    Timestamp=15,
                    Sum=1000
                },
                new Transaction
                {
                    Sender=9,
                    Receiver=1,
                    Timestamp=16,
                    Sum=4000
                },
                new Transaction
                {
                    Sender=8,
                    Receiver=5,
                    Timestamp=17,
                    Sum=2700
                },
                new Transaction
                {
                    Sender=4,
                    Receiver=10,
                    Timestamp=18,
                    Sum=2200
                },
                new Transaction
                {
                    Sender=6,
                    Receiver=10,
                    Timestamp=19,
                    Sum=1400
                },
                new Transaction
                {
                    Sender=10,
                    Receiver=2,
                    Timestamp=20,
                    Sum=3000
                },
            };

            await dataAccess.InsertMany(transactions);
            return transactions;
        }
    }
}
