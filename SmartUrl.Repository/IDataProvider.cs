using SmartUrl.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartUrl.Repository
{
    public interface IDataProvider
    {
        Task Add(SmartUrlEntity smartUrl);
        Task<SmartUrlEntity> GetSmartUrl(int id);
        Task<SmartUrlEntity> GetSmartUrlByKey(string urlKey);
        Task<SmartUrlEntity> GetSmartUrlByHash(string urlHash);
    }
}
