using SmartUrl.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartUrl.Services
{
    public interface IShortUrlService
    {
        Task<SmartUrlEntity> CreateSmartUrl(string url, string scheme, string host, string pathBase);
        Task<SmartUrlEntity> GetSmartUrl(string urlKey);
    }
}
