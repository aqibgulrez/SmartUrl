using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUrl.Web.Models
{
    public class HomeCreateRequestModel
    {
        [Required(ErrorMessage = "Please Enter Url")]
        [Url(ErrorMessage = "Please Enter Valid Url")]
        [Display(Name = "Url")]
        public string Url { get; set; }
    }
}
