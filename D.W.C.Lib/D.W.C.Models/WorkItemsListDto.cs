using Newtonsoft.Json;

namespace DevWorkCalc.D.W.C.Models
{
    public class WorkItemsListDto
    {
        [JsonProperty("workItemRelations")]
        public List<WorkItemRelationDto> WorkItemRelations { get; set; }
    }

    public class WorkItemRelationDto
    {
        public string SprintId { get; set; }

        [JsonProperty("target")]
        public RelationTargetDto Target { get; set; }
    }

    public class RelationTargetDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}