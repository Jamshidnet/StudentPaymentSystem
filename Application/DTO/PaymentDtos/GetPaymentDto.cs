﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PaymentDtos
{
    public class GetPaymentDto : BasePaymentDto
    {
        public Guid ID { get; set; }
    }
}
