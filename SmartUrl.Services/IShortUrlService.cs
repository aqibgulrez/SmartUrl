using SmartUrl.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartUrl.Services
{
    public interface IShortUrlService
    {
        Task<SmartUrlEntity> CreateSmartUrl(string url);
        Task<SmartUrlEntity> GetSmartUrl(string urlKey);
    }
}
