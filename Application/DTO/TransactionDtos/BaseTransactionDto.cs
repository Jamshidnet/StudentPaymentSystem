using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.TransactionDtos
{
    public  class BaseTransactionDto
    {
        [Required]
        public Guid StudentID { get; set; }

        [Required]
        public Guid CourseID { get; set; }

        public DateTime TransactionDate { get; set; }

        public string? TransactionType { get; set; }
        [Required]

        public decimal Amount { get; set; }
    }
}
