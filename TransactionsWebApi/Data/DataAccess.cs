using TransactionsWebApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransactionsWebApi.Data
{
    public class DataAccess
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private const string ConnectionString = "mongodb://192.168.99.100:27017";

        public DataAccess()
        {
            _client = new MongoClient(ConnectionString);
            _database = _client.GetDatabase("money");
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            var collection = _database.GetCollection<Transaction>("transactions");
            var filter = new BsonDocument();
            var result = await collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransaction(FilterDefinition<Transaction> filter)
        {
            var collection = _database.GetCollection<Transaction>("transactions");
            var result = await collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetBalance(FilterDefinition<Transaction> filter)
        {
            var collection = _database.GetCollection<Transaction>("transactions");
            var result = await collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<Transaction> InsertOne(Transaction transaction)
        {
            var collection = _database.GetCollection<Transaction>("transactions");
            await collection.InsertOneAsync(transaction);
            return transaction;
        }
        public async Task<IEnumerable<Transaction>> InsertMany(IEnumerable<Transaction> transactions)
        {
            var collection = _database.GetCollection<Transaction>("transactions");
            await collection.InsertManyAsync(transactions);
            return transactions;
        }
    }
}