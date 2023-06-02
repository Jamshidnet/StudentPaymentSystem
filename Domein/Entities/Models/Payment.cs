using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Domein.States;

namespace Domein.Models
{
    public class Payment : BaseAuditableEntity
    {

        public Guid StudentID { get; set; }


        public Guid CourseID { get; set; }
         


        public DateTime PaymentDate { get; set; }


        public PaymentMethod PaymentMethod { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
    }
}
