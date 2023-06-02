using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.TransactionDtos
{
    public class GetTransactionDto :BaseTransactionDto
    {
        public Guid ID { get; set; }
    }
}
