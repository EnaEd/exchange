using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Exchange.Mobile.Core.Models.GooglesModels
{
    public class GooglePlace : BaseModel
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Raw { get; set; }
        [JsonProperty("address_components")]
        public List<AddressComponent> Addresses { get; set; } = new List<AddressComponent>();
        //public GooglePlace()
        //{
        //}
        public GooglePlace(JObject jsonObject)
        {
            Name = (string)jsonObject["result"]["name"];
            Latitude = (double)jsonObject["result"]["geometry"]["location"]["lat"];
            Longitude = (double)jsonObject["result"]["geometry"]["location"]["lng"];
            Addresses = JsonConvert.DeserializeObject<List<AddressComponent>>(jsonObject["result"]["address_components"].ToString());
            Raw = jsonObject.ToString();
        }
    }
    public class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
        [JsonProperty("types")]
        public List<string> Types { get; set; } = new List<string>();
    }
}
