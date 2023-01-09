using Microsoft.EntityFrameworkCore;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Entities;
using N5Challenge.Transverse.Logger;
using Serilog;

namespace N5Challenge.Infrastructure.Repository
{
    public class PermissionTypesCommandsRepository : IPermissionTypesCommandsRepository
    {
        #region Properties
        private readonly SqlServerDbContext dbContext;
        #endregion

        #region Constructor
        public PermissionTypesCommandsRepository(SqlServerDbContext context)
        {
            dbContext = context;
        }
        #endregion

        #region Public methods
        public async Task<bool> AddPermissionType(PermissionTypes permissionType)
        {
            Log.Information("[Add Permission type] --> Permission type Info: {@PermissionType}", permissionType);
            bool result;

            try
            {
                dbContext.Entry(permissionType).State = EntityState.Added;
                await dbContext.SaveChangesAsync();

                Log.Information("[Add Permission type] --> Permission type Info: {@PermissionType} -- Permission type added succesfully", permissionType);

                result = true;
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Add Permission type Error] --> Permission type Info: {@PermissionType}", ex, permissionType);

                throw;
            }

            return result;
        }

        public async Task<bool> UpdatePermissionType(PermissionTypes permissionType)
        {
            Log.Information("[Update Permission type] --> Permission type Info: {@PermissionType}", permissionType);
            bool result;

            try
            {
                dbContext.Entry(permissionType).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                Log.Information("[Update Permission type] --> Permission type Info: {@PermissionType} -- Permission type updated succesfully", permissionType);

                result = true;
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Update Permission type Error] --> Permission type Info: {@PermissionType}", ex, permissionType);

                throw;
            }

            return result;
        }
        #endregion
    }
}
