using MediatR;
using N5Challenge.Domain.Interfaces;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Dto;
using N5Challenge.Transverse.Enums;
using N5Challenge.Transverse.Logger;
using Nest;
using Serilog;

namespace N5Challenge.Domain.Core.Queries.Permissions
{
    public class GetPermissionsQuery : MediatR.IRequest<List<GetPermissionsQueryResponse>>
    {
        public int Index { get; set; } = 0;

        public int Size { get; set; } = 20;
    }

    public class GetPermissionsQueryHandler : MediatR.IRequestHandler<GetPermissionsQuery, List<GetPermissionsQueryResponse>>
    {
        #region Properties
        //private readonly IPermissionsQueriesRepository _repository;
        private readonly IElasticClient _repository;
        private readonly IEventManagerDomain _eventManagerDomain;
        #endregion

        #region Constructor
        public GetPermissionsQueryHandler(/*IPermissionsQueriesRepository repository, */IElasticClient repository, IEventManagerDomain eventManagerDomain)
        {
            _repository = repository;
            _eventManagerDomain = eventManagerDomain;
        }
        #endregion

        #region Public methods
        public async Task<List<GetPermissionsQueryResponse>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get Permissions] --> Index: {@Index} and size {@Size}", request.Index, request.Size);
            List<GetPermissionsQueryResponse> result = new();

            try
            {
                //List<Transverse.Entities.Permissions> list = await _repository.GetPermissions(request.Index, request.Size);
                var list = _repository.Search<PermissionsDto>(s => s.Query(q => q.MatchAll()));

                /*if (list != null && list.Count() > 0)
                {
                    result = list
                        .Select(p => new GetPermissionsQueryResponse()
                        {
                            Id = p.Id,
                            EmployeeForename = p.EmployeeForename,
                            EmployeeSurname = p.EmployeeSurname,
                            PermissionDate = p.PermissionDate,
                            PermissionTypeId = p.PermissionTypeId
                        })
                        .ToList();

                    Log.Information("[DOMAIN Get Permissions] --> Index: {@Index} and size {@Size} -- Permissions found: {@PermissionsList}", request.Index, request.Size, result);
                }
                else
                {
                    Log.Warning("[DOMAIN Get Permissions] --> Index: {@Index} and size {@Size} -- Permissions not found.", request.Index, request.Size);
                }*/

                _eventManagerDomain.PublishMessage(new EventDto { Id = Guid.NewGuid(), NameOperation = OperationType.get.ToString() });
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("DOMAIN Get Permissions Error] --> Index: {@Index} and size {@Size}", ex, request.Index, request.Size);

                throw;
            }

            return result;
        }
        #endregion
    }

    public class GetPermissionsQueryResponse
    {
        public int Id { get; set; }

        public string EmployeeForename { get; set; }

        public string EmployeeSurname { get; set; }

        public int PermissionTypeId { get; set; }

        public DateTime PermissionDate { get; set; }
    }
}

