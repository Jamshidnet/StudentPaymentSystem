using System.ComponentModel.DataAnnotations;

namespace Application.DTO.StudentDtos
{
    public class UpdateStudentDto : BaseStudentDto
    {
        public Guid ID { get; set; }
    }
}
