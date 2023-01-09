using N5Challenge.Transverse.Entities;

namespace N5Challenge.Infrastructure.Interfaces
{
    public interface IPermissionTypesCommandsRepository
    {
        public Task<bool> AddPermissionType(PermissionTypes permission);

        public Task<bool> UpdatePermissionType(PermissionTypes permission);
    }
}
