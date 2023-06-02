using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AUserDtos
{
    public  class UpdateAUserDto : CreateAUserDto
    {
        public Guid ID { get; set; }
    }
}
