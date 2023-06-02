using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Models
{
    public class Invoice : BaseAuditableEntity
    {
        public Guid StudentID { get; set; }


        public Guid CourseID { get; set; }

        public DateTime IssueDate { get; set; }


        public decimal TotalAmount { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
    }
}
