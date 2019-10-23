using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUrl.Entities.Domain
{
    public class SmartUrlEntity
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        [Required(ErrorMessage = "Please Enter Url")]
        [Url(ErrorMessage = "Please Enter Valid Url")]
        [Display(Name = "Url")]
        public string Url { get; set; }
        [JsonIgnore]
        public string UrlHash { get; set; }
        [JsonIgnore]
        public string UrlKey { get; set; }
        [JsonProperty(PropertyName = "shorturl")]
        public string ShortUrl { get; set; }
        [NotMapped]
        public bool IsSuccess { get; set; }
        [NotMapped]
        public bool IsShortUrl { get; set; }
    }
}
