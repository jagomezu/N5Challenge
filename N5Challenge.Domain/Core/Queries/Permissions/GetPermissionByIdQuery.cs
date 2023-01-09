using MediatR;
using N5Challenge.Domain.Core.Queries.PermissionTypes;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Logger;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Challenge.Domain.Core.Queries.Permissions
{
    public class GetPermissionByIdQuery : IRequest<GetPermissionsQueryResponse?>
    {
        public int Id { get; set; }
    }

    public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, GetPermissionsQueryResponse?>
    {
        #region Properties
        private readonly IPermissionsQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetPermissionByIdQueryHandler(IPermissionsQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<GetPermissionsQueryResponse?> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get Permission by id --> Id: {@Id}]", request.Id);
            GetPermissionsQueryResponse? result = null;

            try
            {
                Transverse.Entities.Permissions? permissionType = _repository.GetPermissionById(request.Id).Result;

                if (permissionType != null)
                {
                    result = new()
                    {
                        EmployeeForename = permissionType.EmployeeForename,
                        EmployeeSurname = permissionType.EmployeeSurname,
                        PermissionDate = permissionType.PermissionDate,
                        PermissionTypeId = permissionType.PermissionTypeId,
                        Id = permissionType.Id
                    };

                    Log.Information("[DOMAIN Get Permission by id] --> Id: {@Id} -- Permission found: {@PermissionType}", request.Id, result);
                }
                else
                {
                    Log.Information("[DOMAIN Get Permission by id] --> Id: {@Id} -- Permission not found", request.Id);
                }
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("DOMAIN Get Permission by id Error] --> Id: {@Id}", ex, request.Id);

                throw;
            }

            return result;
        }
        #endregion
    }
}
