using Microsoft.EntityFrameworkCore;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Entities;
using N5Challenge.Transverse.Logger;
using Serilog;

namespace N5Challenge.Infrastructure.Repository
{
    public class PermissionsCommandsRepository : IPermissionsCommandsRepository
    {
        #region Properties
        private readonly SqlServerDbContext dbContext;
        #endregion

        #region Constructor
        public PermissionsCommandsRepository(SqlServerDbContext context)
        {
            dbContext = context;
        }
        #endregion

        #region Public methods
        public async Task<bool> AddPermission(Permissions permission)
        {
            Log.Information("[Add Permission] --> Permission Info: {@Permission}", permission);
            bool result;

            try
            {
                dbContext.Entry(permission).State = EntityState.Added;
                await dbContext.SaveChangesAsync();
                await dbContext.Entry(permission).ReloadAsync();

                Log.Information("[Add Permission] --> Permission Info: {@Permission} -- Permission added successfully.", permission);

                result = true;
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Add Permission Error] --> Permission Info: {@Permission}", ex, permission);

                throw;
            }

            return result;
        }

        public async Task<bool> UpdatePermission(Permissions permission)
        {
            Log.Information("[Update Permission] --> Permission Info: {@Permission}", permission);
            bool result;

            try
            {
                dbContext.Entry(permission).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                Log.Information("[Update Permission] --> Permission Info: {@Permission} -- Permission updated successfully.", permission);

                result = true;
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Update Permission Error] --> Permission Info: {@Permission}", ex, permission);

                throw;
            }

            return result;
        }
        #endregion
    }
}
