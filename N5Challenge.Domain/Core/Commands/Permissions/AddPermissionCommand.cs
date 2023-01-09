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
    public class AddPermissionCommand : MediatR.IRequest
    { 
        public string EmployeeForename { get; set; }

        public string EmployeeSurname { get; set; }

        public int PermissionTypeId { get; set; }
    }

    public class AddPermissionCommandHandler : MediatR.IRequestHandler<AddPermissionCommand>
    {
        #region Properties
        private readonly IPermissionsCommandsRepository _repository;
        private readonly IEventManagerDomain _eventManagerDomain;
        private readonly IElasticClient _elasticClient;
        #endregion

        #region Constructor
        public AddPermissionCommandHandler(IPermissionsCommandsRepository repository, IEventManagerDomain eventManagerDomain, IElasticClient elasticClient)
        {
            _repository = repository;
            _eventManagerDomain = eventManagerDomain;
            _elasticClient = elasticClient;
        }
        #endregion

        #region Public methods
        public async Task<Unit> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Add Permission] --> Permission Info: {@Permission}", request);

            try
            {
                Transverse.Entities.Permissions newPermission = new()
                {
                    EmployeeForename = request.EmployeeForename,
                    EmployeeSurname = request.EmployeeSurname,
                    PermissionDate = DateTime.Now,
                    PermissionTypeId= request.PermissionTypeId,
                };
                bool result = await _repository.AddPermission(newPermission);

                if (result)
                {
                    await _elasticClient.IndexDocumentAsync(newPermission);
                    _eventManagerDomain.PublishMessage(new EventDto { Id = Guid.NewGuid(), NameOperation = OperationType.request.ToString() });
                }

                Log.Information("[DOMAIN Add Permission] --> Permission Info: {@Permission} -- Permission added successfully.", request);
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[DOMAIN Add Permission Error] --> Permission Info: {@Permission}", ex, request);
                throw;
            }

            return Unit.Value;
        }
        #endregion
    }
}
