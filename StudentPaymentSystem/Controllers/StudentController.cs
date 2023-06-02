using Application.DTO.StudentDtos;
using Application.Interfaces;
using Application.ResponseModel;
using Domein.Models;
using Infrustructure.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ApiController<Student>
    {

        private readonly IStudentRepository studentService;

        public StudentController(IStudentRepository _studentService)
        {
          //Console.WriteLine("DI created Instance. ");
            studentService = _studentService;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "StudentGet")]
        public async Task<ActionResult<ResponseCore<GetStudentDto>>> GetById(Guid id)
        {
             Student? student = await studentService.GetByIdAsync( id);

            if (student == null)
            {
                return NotFound(new ResponseCore<Student?>(false, id + " not found!"));
            }
            GetStudentDto mappedRole = _mapper.Map<GetStudentDto>(student);
            return Ok(new ResponseCore<GetStudentDto?>(mappedRole));
        }


        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "StudentGetAll")]
        public  async  Task<ResponseCore<ActionResult<PaginatedList<GetStudentDto>>>> GetAllStudents( int pageSize=5, int  page=1)
        {
            IEnumerable<Student>? roles = studentService.GetAllAsync(x => true);

            IEnumerable<GetStudentDto> mappedRoles = _mapper.Map<IEnumerable<GetStudentDto>>(roles);
            PaginatedList<GetStudentDto> list = await PaginatedList<GetStudentDto>.CreateAsync(mappedRoles, page, pageSize);

            return new ResponseCore<ActionResult<PaginatedList<GetStudentDto>>>(list);
        }

        [HttpPost]
        [Route("[action]")]
     // [Authorize(Roles = "StudentCreate")]
        public async Task<ActionResult<ResponseCore<GetStudentDto>>> Create([FromBody] CreateStudentDto student)
        {

            var mappedStudent = _mapper.Map<Student>(student);
            var createdStudent = await studentService.CreateAsync(mappedStudent);
            if (createdStudent is null || mappedStudent is null) return BadRequest();

            var GetStudent = _mapper.Map<GetStudentDto>(createdStudent);
            return Ok(new ResponseCore<GetStudentDto>(GetStudent));
        }

        [HttpPut]
        [Route("[action]")]
       [Authorize(Roles = "StudentUpdate")]
        public async Task<ActionResult<ResponseCore<GetStudentDto>>> Update([FromBody] UpdateStudentDto entity)
        {
            var mappedStudent = _mapper.Map<Student>(entity);
            var updatedStudent = await studentService.UpdateAsync(mappedStudent);
            if (updatedStudent is null || mappedStudent is null) return BadRequest();

            var getRole = _mapper.Map<GetStudentDto>(updatedStudent);
            return Ok(new ResponseCore<GetStudentDto>(getRole));

        }

        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "StudentDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            return await studentService.DeleteAsync(Id) ?
           Ok(new ResponseCore<bool>(true))
         : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<ResponseCore<PaginatedList<Student>>> Search(string text, int page = 1, int pageSize = 10)
        {

            var Students = studentService.GetAllAsync(x => x.Address.Contains(text)
                                                       || x.Email.Contains(text)
                                                       || x.Name.Contains(text));


            PaginatedList<Student> students =  PaginatedList<Student>.CreateAsync(Students, page, pageSize).Result;

            ResponseCore<PaginatedList<Student>> res = new()
            {
                Result = students
            };

            return Ok(res);

        }
    }
}
