using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.StudentDtos
{
    public class BaseStudentDto
    {
        [Required(ErrorMessage = "You must input name to create student. ")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters. ")]
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email input")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid Input phone style ")]
        public string? PhoneNumber { get; set; }

        public bool EnrollmentStatus { get; set; } = true;

        public string? Address { get; set; }
    }
}
