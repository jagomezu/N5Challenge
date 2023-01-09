using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Challenge.Domain.Core.Commands.Permissions;
using N5Challenge.Domain.Core.Queries.Permissions;
using N5Challenge.Domain.Core.Queries.PermissionTypes;
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
                ValidatePermissionType(permissionInfo.PermissionTypeId);

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
                ValidatePermissionType(permissionInfo.PermissionTypeId);

                ValidatePermission(permissionInfo.Id);

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

        #region Private methods
        private void ValidatePermission(int permissionId)
        {
            GetPermissionsQueryResponse validPermission = _mediator.Send(new GetPermissionByIdQuery() { Id = permissionId }).Result;

            if (validPermission == null || validPermission.Id == 0)
            {
                throw new Exception("Permiso no existe. Ingrese un tipo de permiso correcto");
            }
        }

        private void ValidatePermissionType(int permissionTypeId)
        {
            GetPermissionTypesQueryResponse? permissionType = _mediator.Send(new GetPermissionTypeByIdQuery() { Id = permissionTypeId }).Result;

            if (permissionType == null || permissionType.Id == 0)
            {
                throw new Exception("Tipo de permiso no existe. Ingrese un tipo de permiso correcto");
            }
        }
        #endregion
    }
}
