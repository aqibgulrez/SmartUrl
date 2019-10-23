using MongoDB.Driver;
using SmartUrl.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUrl.Repository.Mongo
{
    public class MongoApiContext
    {
        private readonly IMongoDatabase _mongoDb;
        public MongoApiContext()
        {
            var client = new MongoClient(AppConfiguration.GetConfiguration("ServerName"));
            _mongoDb = client.GetDatabase(AppConfiguration.GetConfiguration("DatabaseName"));
        }
        public IMongoCollection<SmartUrlEntity> SmartUrl
        {
            get
            {
                return _mongoDb.GetCollection<SmartUrlEntity>(AppConfiguration.GetConfiguration("CollectionName"));
            }
        }
    }

}
