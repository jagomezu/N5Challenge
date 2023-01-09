using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Challenge.Domain.Core.Commands.Permissions;
using N5Challenge.Domain.Core.Queries.Permissions;
using N5Challenge.Transverse.Dto;

namespace N5Challenge.Application.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController : Controller
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Public methods
        [HttpPost(Name = "AddPermission")]
        public async Task<IActionResult> AddPermission([FromBody] AddPermissionCommand permissionInfo)
        {
            try
            {
                await _mediator.Send(permissionInfo);

                return Ok();
            }
            catch (Exception ex)
            {
                ResponseDto<bool> response = new()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };

                return BadRequest(response);
            }
        }

        [HttpPut(Name = "UpdatePermission")]
        public async Task<IActionResult> UpdatePermission([FromBody] UpdatePermissionCommand permissionInfo)
        {
            try
            {
                await _mediator.Send(permissionInfo);

                return Ok();
            }
            catch (Exception ex)
            {
                ResponseDto<bool> response = new()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };

                return BadRequest(response);
            }
        }

        [HttpGet(Name = "GetPermissions")]
        public async Task<IActionResult> GetPermissions()
        {
            try
            {
                List<GetPermissionsQueryResponse> result = await _mediator.Send(new GetPermissionsQuery() { Index = 0, Size = 20 });

                return Ok(result);
            }
            catch (Exception ex)
            {
                ResponseDto<bool> response = new()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };

                return BadRequest(response);
            }
        }
        #endregion
    }
}
