using MediatR;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Logger;
using Serilog;

namespace N5Challenge.Domain.Core.Commands.PermissionTypes
{
    public class UpdatePermissionTypeCommand : IRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePermissionTypeCommandHandler : IRequestHandler<UpdatePermissionTypeCommand>
    {
        #region Properties
        private readonly IPermissionTypesCommandsRepository _repository;
        #endregion

        #region Constructor
        public UpdatePermissionTypeCommandHandler(IPermissionTypesCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(UpdatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Update Permission type] --> Permission type info: {@PermissionType}", request);

            try
            {
                Transverse.Entities.PermissionTypes permissionType = new()
                {
                    Id = request.Id,
                    Description = request.Description,
                };
                bool result = await _repository.UpdatePermissionType(permissionType);

                Log.Information("[DOMAIN Update Permission type] --> Permission type Info: {@PermissionType} -- Permission type updated successfully.", request);
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[DOMAIN Update Permission type Error] --> Permission Info: {@Permission}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
