using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Challenge.Domain.Core.Commands.PermissionTypes;
using N5Challenge.Domain.Core.Queries.PermissionTypes;
using N5Challenge.Transverse.Dto;

namespace N5Challenge.Application.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionTypesController : Controller
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public PermissionTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Public methods
        [HttpPost(Name = "AddPermissionType")]
        public async Task<IActionResult> AddPermissionType([FromBody] AddPermissionTypeCommand permissionInfo)
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

        [HttpPut(Name = "UpdatePermissionType")]
        public async Task<IActionResult> UpdatePermissionType([FromBody] UpdatePermissionTypeCommand permissionInfo)
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

        [HttpGet(Name = "GetPermissionTypes")]
        public async Task<IActionResult> GetPermissionTypes()
        {
            try
            {
                List<GetPermissionTypesQueryResponse> result = await _mediator.Send(new GetPermissionTypesQuery() { });

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
