using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransactionsWebApi.Models
{
    public class Transaction
    {
        public ObjectId Id { get; set; }

        [BsonElement("sender")]
        public int Sender { get; set; }

        [BsonElement("receiver")]
        public int Receiver { get; set; }

        [BsonElement("timestamp")]
        public int Timestamp { get; set; }

        [BsonElement("sum")]
        public int Sum { get; set; }
    }
}