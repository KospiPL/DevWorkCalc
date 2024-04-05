using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.W.C.Lib.D.W.C.Models
{
    public class WorkItemHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public int? ApiId { get; set; }
        public int? Rev { get; set; }
        public DateTime? OldValueDate { get; set; }
        public DateTime? NewValueDate { get; set; }
        public string? OldValueColumn { get; set; }
        public string? NewValueColumn { get; set; }
    }
}