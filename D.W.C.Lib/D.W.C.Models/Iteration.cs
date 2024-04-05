using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevWorkCalc.D.W.C.Models
{
    public class Iteration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ApiId { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }
    }
}