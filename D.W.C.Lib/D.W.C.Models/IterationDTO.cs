using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.W.C.Lib.D.W.C.Models
{
    public class IterationsListdto
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("value")]
        public List<Iterationdto> Value { get; set; }
    }
    public class Iterationdto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("attributes")]
        public IterationAttributes Attributes { get; set; }
    }

    public class IterationAttributes
    {
        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("finishDate")]
        public DateTime? FinishDate { get; set; }
    }
}
