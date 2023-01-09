using N5Challenge.Transverse.Entities;

namespace N5Challenge.Infrastructure.Interfaces
{
    public interface IPermissionsCommandsRepository
    {
        public Task<bool> AddPermission(Permissions permission);

        public Task<bool> UpdatePermission(Permissions permission);
    }
}
