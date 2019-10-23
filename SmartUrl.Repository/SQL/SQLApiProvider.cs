using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartUrl.Entities.Domain;

namespace SmartUrl.Repository.SQL
{
    public class SQLApiProvider : IDataProvider
    {
        private readonly SQLApiContext _db;
 
        public SQLApiProvider(SQLApiContext db)
        {
            _db = db;
        }
 
        public async Task Add(SmartUrlEntity smartUrl)
        {
            _db.Add(smartUrl);
            await _db.SaveChangesAsync();
        }

        public async Task<SmartUrlEntity> GetSmartUrlAsync(int id)
        {
            return await _db.SmartUrl.Where(at => at.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SmartUrlEntity> GetSmartUrlByHashAsync(string urlHash)
        {
            return await _db.SmartUrl.Where(at => at.UrlHash == urlHash).FirstOrDefaultAsync();
        }

        public async Task<SmartUrlEntity> GetSmartUrlByKey(string urlKey)
        {
            return await _db.SmartUrl.Where(at => at.UrlKey == urlKey).FirstOrDefaultAsync();
        }

        public async Task<SmartUrlEntity> GetSmartUrlByShortAsync(string shortUrl)
        {
            return await _db.SmartUrl.Where(at => at.ShortUrl == shortUrl).FirstOrDefaultAsync();
        }
    }
}
