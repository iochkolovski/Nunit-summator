using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TestProjectSoftUni.Models
{
    public class Issue
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
