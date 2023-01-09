using N5Challenge.Transverse.Entities;

namespace N5Challenge.Infrastructure.Interfaces
{
    public interface IPermissionsQueriesRepository
    { 
        public Task<List<Permissions>> GetPermissions(int index = 0, int size = 20);
    }
}
