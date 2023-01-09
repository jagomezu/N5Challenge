using MediatR;
using N5Challenge.Domain.Interfaces;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Dto;
using N5Challenge.Transverse.Enums;
using N5Challenge.Transverse.Logger;
using Nest;
using Serilog;

namespace N5Challenge.Domain.Core.Commands.Permissions
{
    public class UpdatePermissionCommand : MediatR.IRequest
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }

        public string EmployeeSurname { get; set; }

        public int PermissionTypeId { get; set; }
    }

    public class UpdatePermissionCommandHandler : MediatR.IRequestHandler<UpdatePermissionCommand>
    {
        #region Properties
        private readonly IPermissionsCommandsRepository _repository;
        private readonly IEventManagerDomain _eventManagerDomain;
        private readonly IElasticClient _elasticClient;
        #endregion

        #region Constructor
        public UpdatePermissionCommandHandler(IPermissionsCommandsRepository repository, IEventManagerDomain eventManagerDomain, IElasticClient elasticClient)
        {
            _repository = repository;
            _eventManagerDomain = eventManagerDomain;
            _elasticClient = elasticClient;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Update Permission] --> Permission Info: {@Permission}", request);

            try
            {
                Transverse.Entities.Permissions newPermission = new()
                {
                    Id = request.Id,
                    EmployeeForename = request.EmployeeForename,
                    EmployeeSurname = request.EmployeeSurname,
                    PermissionTypeId = request.PermissionTypeId,
                    PermissionDate = DateTime.Now
                };
                bool result = await _repository.UpdatePermission(newPermission);

                if (result)
                {
                    await _elasticClient.IndexDocumentAsync(newPermission);
                    _eventManagerDomain.PublishMessage(new EventDto { Id = Guid.NewGuid(), NameOperation = OperationType.modify.ToString() });
                }

                Log.Information("[DOMAIN Update Permission] --> Permission Info: {@Permission} -- Permission update successfully.", request);
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[DOMAIN Update Permission Error] --> Permission Info: {@Permission}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
