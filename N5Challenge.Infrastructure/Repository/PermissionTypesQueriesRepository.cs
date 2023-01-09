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
                result = await dbContext.PermissionTypes.AsNoTracking().ToListAsync();

                Log.Information("[Get Permissions types] -- Permission types found: {@PermissionTypesList}", result);
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Get Permission Types Error]", ex);

                throw;
            }

            return result;
        }

        public async Task<PermissionTypes?> GetPermissionTypeById(int id)
        {
            Log.Information("[Get Permission type by id] --> Id:{@Id}", id);
            PermissionTypes? result;

            try
            {
                result = dbContext.PermissionTypes.AsNoTracking().FirstOrDefault(pt => pt.Id == id);

                if (result != null)
                {
                    Log.Information("[Get Permission type by id] --> Id: {@Id} Permission type found: {@PermissionType}", id, result);
                }
                else
                {
                    Log.Warning("[Get Permission type by id] --> Id: {@Id} Permission type not found", result);
                }
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Get Permission Type by id Error] --> Id: {@Id}", ex, id);

                throw;
            }

            return result;
        }
        #endregion
    }
}
