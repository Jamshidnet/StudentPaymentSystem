using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.InvoiceDtos
{
    public  class BaseInvoiceDto
    {
        [Required]
        public Guid StudentID { get; set; }

        [Required]
        public Guid CourseID { get; set; }

        public DateTime IssueDate { get; set; }


        public decimal TotalAmount { get; set; }
    }
}
