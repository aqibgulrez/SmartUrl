using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUrl.Web.Models
{
    public class HomeCreateResponseModel
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsShortUrl { get; set; }
    }
}
