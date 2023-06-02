using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CourseDtos
{
    public class BaseCourseDto
    {
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        public string? Instructor { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
