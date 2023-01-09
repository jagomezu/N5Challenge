using MediatR;
using N5Challenge.Domain.Interfaces;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Dto;
using N5Challenge.Transverse.Enums;
using N5Challenge.Transverse.Logger;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Challenge.Domain.Core.Queries.PermissionTypes
{
    public class GetPermissionTypesQuery : IRequest<List<GetPermissionTypesQueryResponse>>
    {

    }

    public class GetPermissionTypesQueryHandler : IRequestHandler<GetPermissionTypesQuery, List<GetPermissionTypesQueryResponse>>
    {
        #region Properties
        private readonly IPermissionTypesQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetPermissionTypesQueryHandler(IPermissionTypesQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<GetPermissionTypesQueryResponse>> Handle(GetPermissionTypesQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get Permission types]");
            List<GetPermissionTypesQueryResponse> result = new();

            try
            {
                List<Transverse.Entities.PermissionTypes> list = await _repository.GetPermissionTypes();

                if (list != null && list.Count > 0)
                {
                    result = list
                        .Select(p => new GetPermissionTypesQueryResponse()
                        {
                            Id = p.Id,
                            Description = p.Description
                        })
                        .ToList();

                    Log.Information("[DOMAIN Get Permission types] -- Permission types found: {@PermissionsList}", result);
                }
                else
                {
                    Log.Warning("[DOMAIN Get Permission types] -- Permission types not found.");
                }
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("DOMAIN Get Permission types Error]", ex);

                throw;
            }

            return result;
        }
        #endregion
    }

    public class GetPermissionTypesQueryResponse
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
