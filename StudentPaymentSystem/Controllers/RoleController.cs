using Application.DTO.RoleDtos;
using Application.Interfaces;
using Application.ResponseModel;
using Domein.AccessEntities;
using Microsoft.AspNetCore.Mvc;
using StudentPaymentSystem.Controllers;
using StudentPaymentSystem.Filters;

namespace RolePaymentSystem.Controllers
{
    public class RoleController : ApiController<Role>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        public RoleController(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }
        [HttpGet("[action]")]
        //[Authorize(Roles = "RoleGetAll")]
        public ActionResult<ResponseCore<IEnumerable<GetRoleDto>>> GetAll()
        {
            IEnumerable<Role>? roles = _roleRepository.GetAllAsync(x => true);

            IEnumerable<GetRoleDto> mappedRoles = _mapper.Map<IEnumerable<GetRoleDto>>(roles);

            return Ok(new ResponseCore<IEnumerable<GetRoleDto>>(mappedRoles));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "RoleGet")]
        public async Task<ActionResult<ResponseCore<GetRoleDto>>> GetById(Guid id)
        {
            IEnumerable<Role>? roles = _roleRepository.GetAllAsync(x => true, nameof(Role.Permissions));
            Role? role = roles.FirstOrDefault(x => x.ID == id);
            if (role == null)
            {
                return NotFound(new ResponseCore<Role?>(false, id + " not found!"));
            }
            GetRoleDto mappedRole = _mapper.Map<GetRoleDto>(role);
            return Ok(new ResponseCore<GetRoleDto?>(mappedRole));
        }

        [HttpPut("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "UpdateRole")]
        public async Task<ActionResult<ResponseCore<GetRoleDto>>> Update([FromBody] UpdateRoleDto role)
        {
            Role? mappedRole = _mapper.Map<Role>(role);
            var validationResult = _validator.Validate(mappedRole);
      
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Role>(false, validationResult.Errors));
            }

            if (role.PermissionIDs is null) return BadRequest();

            Role? maybeRole = await _roleRepository.GetByIdAsync(role.ID);

            if (maybeRole is null)
            {
                return BadRequest();
            }

            List<Permission> maybePermissions =
                _permissionRepository.GetAllAsync(x => true).Where(p => role.PermissionIDs.Contains(p.ID)).ToList();

            if (maybePermissions.Count != role.PermissionIDs.Count)
            {
                return BadRequest();
            }

            maybeRole.Permissions = maybePermissions;

            await _roleRepository.UpdateAsync(maybeRole);
                return Ok(new ResponseCore<GetRoleDto>(_mapper.Map<GetRoleDto>(maybeRole)));

        }

        [HttpPost("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "CreateRole")]
        public async Task<ActionResult<ResponseCore<GetRoleDto>>> Create([FromBody] CreateRoleDto role)
        {
            Role? mappedRole = _mapper.Map<Role>(role);
            var validationResult = await _validator.ValidateAsync(mappedRole);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            mappedRole.Permissions = new List<Permission>();
            if (role.PermissionIDs is null)
                return BadRequest();
            foreach (Guid item in role.PermissionIDs)
            {
                Permission? permission = await _permissionRepository.GetByIdAsync(item);
                if (permission != null)
                    mappedRole.Permissions.Add(permission);
                else return BadRequest(
                    new ResponseCore<string>(false, item + " Id not found")
                    );
            }

            mappedRole = await _roleRepository.CreateAsync(mappedRole);
            var res = _mapper.Map<GetRoleDto>(mappedRole);
            return Ok(new ResponseCore<GetRoleDto?>(res));
        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteRole")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _roleRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }


    }
}
