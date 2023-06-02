using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.StudentDtos
{
    public class GetStudentDto : BaseStudentDto
    {
        public Guid ID { get; set; }
    }
}
