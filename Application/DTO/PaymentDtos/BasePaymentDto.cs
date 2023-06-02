using Domein.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PaymentDtos
{
    public  class BasePaymentDto 
    {
        [Required]
        public Guid StudentID { get; set; }
        [Required]
        public Guid CourseID { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
