using N5Challenge.Transverse.Entities;

namespace N5Challenge.Infrastructure.Interfaces
{
    public interface IPermissionTypesQueriesRepository
    { 
        public Task<List<PermissionTypes>> GetPermissionTypes();

        public Task<PermissionTypes?> GetPermissionTypeById(int id);
    }
}
