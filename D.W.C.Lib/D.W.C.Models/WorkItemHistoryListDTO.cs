using Newtonsoft.Json;

namespace D.W.C.Lib.D.W.C.Models
{
    public class WorkItemHistoryListDto
    {
        [JsonProperty("count")]
        public int? count { get; set; }

        [JsonProperty("value")]
        public List<WorkItemHistorydto>? Value { get; set; }
    }

    public class WorkItemHistorydto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("workItemId")]
        public int workItemId { get; set; }

        [JsonProperty("rev")]
        public int Rev { get; set; }

        [JsonProperty("fields")]
        public Fieldsdto? Fields { get; set; }
    }

    public class Fieldsdto
    {
        [JsonProperty("System.ChangedDate")]
        public ChangeDated? System_ChangedDate { get; set; }

        [JsonProperty("System.BoardColumn")]
        public BoardColumnd? System_BoardColumn { get; set; }

        public string RequiredProperty { get; set; }
    }

    public class ChangeDated
    {
        [JsonProperty("oldValue")]
        public DateTime? OldValue { get; set; }

        [JsonProperty("newValue")]
        public DateTime? NewValue { get; set; }
    }

    public class BoardColumnd
    {
        [JsonProperty("oldValue")]
        public string? OldValue { get; set; }

        [JsonProperty("newValue")]
        public string? NewValue { get; set; }
    }
}