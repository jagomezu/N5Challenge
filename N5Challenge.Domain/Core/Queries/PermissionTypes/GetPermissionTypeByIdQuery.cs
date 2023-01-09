using MediatR;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Logger;
using Serilog;

namespace N5Challenge.Domain.Core.Queries.PermissionTypes
{
    public class GetPermissionTypeByIdQuery : IRequest<GetPermissionTypesQueryResponse?>
    {
        public int Id { get; set; }
    }

    public class GetPermissionTypeByIdQueryHandler : IRequestHandler<GetPermissionTypeByIdQuery, GetPermissionTypesQueryResponse?>
    {
        #region Properties
        private readonly IPermissionTypesQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetPermissionTypeByIdQueryHandler(IPermissionTypesQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<GetPermissionTypesQueryResponse?> Handle(GetPermissionTypeByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get Permission type by id --> Id: {@Id}]", request.Id);
            GetPermissionTypesQueryResponse? result = null;

            try
            {
                Transverse.Entities.PermissionTypes? permissionType = _repository.GetPermissionTypeById(request.Id).Result;

                if (permissionType != null)
                {
                    result = new()
                    {
                        Description = permissionType.Description,
                        Id = permissionType.Id
                    };

                    Log.Information("[DOMAIN Get Permission type by id] --> Id: {@Id} -- Permission type found: {@PermissionType}", request.Id, result);
                }
                else
                {
                    Log.Information("[DOMAIN Get Permission type by id] --> Id: {@Id} -- Permission type not found", request.Id);
                }
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("DOMAIN Get Permission type by id Error] --> Id: {@Id}", ex, request.Id);

                throw;
            }

            return result;
        }
        #endregion
    }
}
