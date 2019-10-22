using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SmartUrl.Entities.Domain
{
    public class SmartUrlEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; }
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
    }
}
