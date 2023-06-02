using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.InvoiceDtos
{
    public  class UpdateInvoiceDto : BaseInvoiceDto
    {
        public Guid ID { get; set; }
    }
}
