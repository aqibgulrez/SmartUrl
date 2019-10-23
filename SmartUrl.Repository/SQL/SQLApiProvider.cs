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

        public async Task<SmartUrlEntity> GetSmartUrl(int id)
        {
            return await _db.SmartUrl.SingleOrDefaultAsync(at => at.Id == id);
        }

        public async Task<SmartUrlEntity> GetSmartUrlByHash(string urlHash)
        {
            return await _db.SmartUrl.SingleOrDefaultAsync(at => string.Equals(at.UrlHash, urlHash, StringComparison.InvariantCulture));
        }

        public async Task<SmartUrlEntity> GetSmartUrlByKey(string urlKey)
        {
            var url = await _db.SmartUrl.SingleOrDefaultAsync(at => string.Equals(at.UrlKey, urlKey, StringComparison.InvariantCulture));
            return url;
        }
    }
}
