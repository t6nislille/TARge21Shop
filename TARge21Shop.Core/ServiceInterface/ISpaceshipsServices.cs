using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;

namespace TARge21Shop.Core.ServiceInterface
{
    public interface ISpaceshipsServices : IApplicationServices
    {
        Task<Spaceship> Create(SpaceshipDto dto);
        Task<Spaceship> GetAsync(Guid id);
        Task<Spaceship> Update(SpaceshipDto dto);
        Task<Spaceship> Delete(Guid id);
        Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto);
    }
}
