using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.W.C.Lib.D.W.C.Models
{
    public class WorkItemDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public int? ApiId { get; set; }

        public int? Rev { get; set; }

        public string? AreaPath { get; set; }

        public string? TeamProject { get; set; }

        public string? IterationPath { get; set; }

        public string? WorkItemType { get; set; }

        public string? State { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Title { get; set; }

        public string? BoardColumn { get; set; }

        public DateTime? ActivatedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public string? DisplayName { get; set; }
    }
}