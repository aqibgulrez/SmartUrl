using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SmartUrl.Entities.Domain;

namespace SmartUrl.Repository.Mongo
{
    public class MongoApiProvider : IDataProvider
    {
        MongoApiContext db = new MongoApiContext();

        public async Task Add(SmartUrlEntity smartUrl)
        {
            try
            {
                await db.SmartUrl.InsertOneAsync(smartUrl);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SmartUrlEntity> GetSmartUrlByHash(string urlHash)
        {
            try
            {
                FilterDefinition<SmartUrlEntity> filter = Builders<SmartUrlEntity>.Filter.Eq("UrlHash", urlHash);
                return await db.SmartUrl.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<SmartUrlEntity> GetSmartUrlByKey(string urlKey)
        {
            try
            {
                FilterDefinition<SmartUrlEntity> filter = Builders<SmartUrlEntity>.Filter.Eq("UrlKey", urlKey);
                return await db.SmartUrl.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<SmartUrlEntity> GetSmartUrl(string id)
        {
            try
            {
                FilterDefinition<SmartUrlEntity> filter = Builders<SmartUrlEntity>.Filter.Eq("Id", id);
                return await db.SmartUrl.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SmartUrlEntity>> GetSmartUrls()
        {
            try
            {
                return await db.SmartUrl.Find(_ => true).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
