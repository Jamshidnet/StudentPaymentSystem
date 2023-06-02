using System.ComponentModel.DataAnnotations;

namespace Application.DTO.CourseDtos
{
    public class UpdateCourseDto : BaseCourseDto
    {
        public Guid ID { get; set; }
    }
}
