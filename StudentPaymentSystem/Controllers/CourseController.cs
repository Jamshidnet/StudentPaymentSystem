using Application.DTO.CourseDtos;
using Application.Interfaces;
using Application.ResponseModel;
using Domein.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPaymentSystem.Controllers;

namespace CoursePaymentSystem.Controllers
{
    public class CourseController : ApiController<Course>
    {
        private readonly ICourseRepository courseService;
        public CourseController(ICourseRepository _courseService)
        {
            courseService = _courseService;
        }

        [HttpGet]
        [Route("[action]")]
         [Authorize(Roles = "CourseGet")]
        public async Task<ActionResult<ResponseCore<GetCourseDto>>> GetById(Guid id)
        {
            Course? course = await courseService.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound(new ResponseCore<Course?>(false, id + " not found!"));
            }
            GetCourseDto mappedRole = _mapper.Map<GetCourseDto>(course);
            return Ok(new ResponseCore<GetCourseDto?>(mappedRole));
        }


        [HttpGet]
        [Route("[action]")]
          [Authorize(Roles = "CourseGetAll")]
        public ActionResult<ResponseCore<GetCourseDto>> GetAllCourses(int page = 1, int pageSize = 10)
        {
            IEnumerable<Course>? roles = courseService.GetAllAsync(x => true);

            IEnumerable<GetCourseDto> mappedRoles = _mapper.Map<IEnumerable<GetCourseDto>>(roles);

            return Ok(new ResponseCore<IEnumerable<GetCourseDto>>(mappedRoles));
        }

        [HttpPost]
        [Route("[action]")]
          [Authorize(Roles = "CourseCreate")]
        public async Task<ActionResult<ResponseCore<GetCourseDto>>> Create([FromBody] CreateCourseDto course)
        {

            var mappedCourse = _mapper.Map<Course>(course);
            var createdCourse = await courseService.CreateAsync(mappedCourse);
            if (createdCourse is null || mappedCourse is null) return BadRequest();

            var GetCourse = _mapper.Map<GetCourseDto>(createdCourse);
            return Ok(new ResponseCore<GetCourseDto>(GetCourse));
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "CourseUpdate")]
        public async Task<ActionResult<ResponseCore<GetCourseDto>>> Update([FromBody] UpdateCourseDto entity)
        {
            var mappedCourse = _mapper.Map<Course>(entity);
            var updatedCourse = await courseService.UpdateAsync(mappedCourse);
            if (updatedCourse is null || mappedCourse is null) return BadRequest();

            var getRole = _mapper.Map<GetCourseDto>(updatedCourse);
            return Ok(new ResponseCore<GetCourseDto>(getRole));

        }

        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "CourseDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            return await courseService.DeleteAsync(Id) ?
           Ok(new ResponseCore<bool>(true))
         : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
