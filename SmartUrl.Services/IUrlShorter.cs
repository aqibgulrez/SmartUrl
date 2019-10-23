using SmartUrl.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartUrl.Services
{
    public interface IUrlShorter
    {
        Task<string> GetSmartUrl(SmartUrlEntity objSmartUrlEntity);
    }
}
