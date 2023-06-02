using Application.DTO.AUserDtos;
using Application.Interfaces;
using Application.ResponseModel;
using Domein.AccessEntities;
using Domein.Token;
using FluentValidation;
using Infrustructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Serilog;
using System.Data;

namespace StudentPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AUserController : ApiController<AUser>
    {

        private readonly IAUserRepository aUserService;
        private readonly IRoleRepository roleRepository;
        private readonly ITokenService tokenService;

        public AUserController(IAUserRepository _aUserService, IRoleRepository _roleRepository, ITokenService _tokenService)
        {
            aUserService = _aUserService;
            roleRepository = _roleRepository;
            tokenService = _tokenService;
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AUserCredentials credential)
        {
            string HashPassword = credential.Password.ComputeSha256Hash();
            AUser? user =  aUserService.GetAllAsync(x=>true)?.FirstOrDefault(x => x.Username == credential.Username &&
                                                             x.Password == HashPassword);

            Log.Warning("This is Warning");

            if (user is null)
            {
                return NotFound("Not found Objects");
            }
            Tokens token = await tokenService.CreateTokensAsync(user);

            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] Tokens tokens)
        {
            var principal = tokenService.GetClaimsFromExpiredToken(tokens.AccessToken);
            string? username = principal.Identity?.Name;
            if (username == null)
            {
                return NotFound("Refresh token not found!");
            }
            var savedRefreshToken = tokenService.Get(x => x.UserName == username &&
                                                      x.RefreshToken == tokens.RefreshToken)
                                                     .FirstOrDefault();

            if (savedRefreshToken == null)
            {
                return BadRequest("Refresh token or Access token invalid!");
            }
            if (savedRefreshToken.ExpirationDate < DateTime.UtcNow)
            {
                tokenService.Delete(savedRefreshToken);
                return StatusCode(405, "Refresh token already expired");
            }
            Tokens newTokens = await tokenService.CreateTokensFromRefresh(principal, savedRefreshToken);

            return Ok(newTokens);

        }



        [HttpGet]
        [Route("[action]")]
       // [Authorize(Roles = "AUserGet")]
        public async Task<ActionResult<ResponseCore<GetAUserDto>>> GetById(Guid id)
        {
            IEnumerable<AUser>? roles = aUserService.GetAllAsync(x => true, nameof(AUser.Roles));
            AUser? role = roles.FirstOrDefault(x => x.ID == id);
            if (role == null)
            {
                return NotFound(new ResponseCore<AUser?>(false, id + " not found!"));
            }
            GetAUserDto mappedRole = _mapper.Map<GetAUserDto>(role);
            return Ok(new ResponseCore<GetAUserDto?>(mappedRole));
        }
        [HttpGet]
        [Route("[action]")]
       // [Authorize(Roles = "AUserGetAll")]
        public  ActionResult<IEnumerable<ResponseCore<GetAUserDto>>> AllAUsers(int page = 1, int pageSize = 10)
        {
            IEnumerable<AUser>? roles = aUserService.GetAllAsync(x => true);

            IEnumerable<GetAUserDto> mappedRoles = _mapper.Map<IEnumerable<GetAUserDto>>(roles);

            return Ok(new ResponseCore<IEnumerable<GetAUserDto>>(mappedRoles));
        }

        [HttpPost]
        [Route("[action]")]
       // [Authorize(Roles = "AUserCreate")]
        public async Task<IActionResult> Create([FromBody] CreateAUserDto aUser)
        {

            AUser? mappedAUser = _mapper.Map<AUser>(aUser);
            var validationResult = await _validator.ValidateAsync(mappedAUser);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }

            mappedAUser.Roles = new List<Role?>();

            if (aUser.RolesIds is null)
                return BadRequest();

            foreach (Guid item in aUser.RolesIds)
            {
                Role? role = await roleRepository.GetByIdAsync(item);
                if (role != null)
                    mappedAUser.Roles.Add(role);
                else return BadRequest(
                    new ResponseCore<string>(false, item + " Id not found")
                    );
            }

            mappedAUser = await aUserService.CreateAsync(mappedAUser);
            var res = _mapper.Map<GetAUserDto>(mappedAUser);
            return Ok(new ResponseCore<object?>(res));
        }

        [HttpPut]
        [Route("[action]")]
       // [Authorize(Roles = "AUserUpdate")]
        public async Task<IActionResult> Update([FromBody] UpdateAUserDto entity)
        {
            AUser? mappedAUser = _mapper.Map<AUser>(entity);
            var validationResult = _validator.Validate(mappedAUser);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<AUser>(false, validationResult.Errors));
            }

            if (entity.RolesIds is null) return BadRequest();

            AUser? maybeRole = await aUserService.GetByIdAsync(entity.ID);

            if (maybeRole is null)
            {
                return BadRequest();
            }

            List<Role> maybeRoles =
                roleRepository.GetAllAsync(x => true).Where(p => entity.RolesIds.Contains(p.ID)).ToList();

            if (maybeRoles.Count != entity.RolesIds.Count)
            {
                return BadRequest();
            }

            maybeRole.Roles = maybeRoles;

            await aUserService.UpdateAsync(maybeRole);
            return Ok(new ResponseCore<GetAUserDto>(_mapper.Map<GetAUserDto>(maybeRole)));
        }

        [HttpDelete]
        [Route("[action]")]
      //  [Authorize(Roles = "AUserDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            return await aUserService.DeleteAsync(Id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
        }

    }
}
