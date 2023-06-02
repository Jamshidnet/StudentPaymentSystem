using Application.DTO.PermissionDtos;
using Application.Interfaces;
using Application.ResponseModel;
using AutoMapper;
using Domein.AccessEntities;
using Microsoft.AspNetCore.Mvc;
using StudentPaymentSystem.Controllers;

namespace PermissionPaymentSystem.Controllers
{
    public class PermissionController : ApiController<Permission>
    {
        private readonly IPermissionRepository permissionService;

        public PermissionController(IPermissionRepository _permissionService)
        {
            permissionService = _permissionService;
        }

        [HttpGet]
        [Route("[action]")]
       // [Authorize(Roles = "PermissionGet")]
        public async Task<ActionResult<ResponseCore<GetPermissionDto>>> GetById(Guid id)
        {
           var permission = await permissionService.GetByIdAsync(id);
            GetPermissionDto per= _mapper.Map<GetPermissionDto>(permission);
            return (permission is null)? BadRequest() : Ok(per);
        }
        [HttpGet]
        [Route("[action]")]
        //[Authorize(Roles = "PermissionGetAll")]
        public async Task<ActionResult<ResponseCore<IEnumerable<GetPermissionDto>>>> GetAllPermissions(int page = 1, int pageSize = 10)
        {
            //string[] permissionss = new string[2]
            //{
            //    "StudentCreate", "StudentGetAll"
            //};
            //foreach (var item in permissionss)
            //{
            //    await permissionService.CreateAsync(new Permission() { PermissionName = item });
            //}

            IEnumerable<Permission>? permissions = permissionService.GetAllAsync(x => true);

            IEnumerable<GetPermissionDto> mappedPermissions = _mapper.Map<IEnumerable<GetPermissionDto>>(permissions);

            return Ok(new ResponseCore<IEnumerable<GetPermissionDto>>(mappedPermissions));
        }
    }
}
