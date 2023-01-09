using Microsoft.EntityFrameworkCore;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Entities;
using N5Challenge.Transverse.Logger;
using Serilog;
using System.Drawing;
using System.Security;

namespace N5Challenge.Infrastructure.Repository
{
    public class PermissionTypesQueriesRepository : IPermissionTypesQueriesRepository
    {
        #region Properties
        private readonly SqlServerDbContext dbContext;
        #endregion

        #region Constructor
        public PermissionTypesQueriesRepository(SqlServerDbContext context)
        {
            dbContext = context;
        }
        #endregion

        #region Public methods
        public async Task<List<PermissionTypes>> GetPermissionTypes()
        {
            Log.Information("[Get Permission types]");
            List<PermissionTypes> result;

            try
            {
                result = await dbContext.PermissionTypes.ToListAsync();

                Log.Information("[Get Permissions types] -- Permission types found: {@PermissionTypesList}", result);
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Get Permission Types Error]", ex);

                throw;
            }

            return result;
        }
        #endregion
    }
}
