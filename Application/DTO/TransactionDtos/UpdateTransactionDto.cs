using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.TransactionDtos
{
    public class UpdateTransactionDto : BaseTransactionDto
    {
        [Required]
        public Guid ID { get; set; }
         
    }
}
