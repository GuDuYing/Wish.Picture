using Newtonsoft.Json;

namespace Wish.Picture.Web.Models
{
    public class UploadResult
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}