using MediatR;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Logger;
using Serilog;

namespace N5Challenge.Domain.Core.Commands.PermissionTypes
{
    public class AddPermissionTypeCommand : IRequest
    {
        public string Description { get; set; }
    }

    public class AddPermissionTypeCommandHandler : IRequestHandler<AddPermissionTypeCommand>
    {
        #region Properties
        private readonly IPermissionTypesCommandsRepository _repository;
        #endregion

        #region Constructor
        public AddPermissionTypeCommandHandler(IPermissionTypesCommandsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(AddPermissionTypeCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Add Permission type] --> Permission type info: {@PermissionType}", request);

            try
            {
                Transverse.Entities.PermissionTypes permissionType = new()
                {
                    Description = request.Description,
                };
                bool result = await _repository.AddPermissionType(permissionType);

                Log.Information("[DOMAIN Add Permission type] --> Permission type Info: {@PermissionType} -- Permission type aaded successfully.", request);
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[DOMAIN Add Permission type Error] --> Permission type Info: {@PermissionType}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
