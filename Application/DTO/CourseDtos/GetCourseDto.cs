using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CourseDtos
{
    public  class GetCourseDto :BaseCourseDto
    {
        public Guid ID { get; set; }
    }
}
