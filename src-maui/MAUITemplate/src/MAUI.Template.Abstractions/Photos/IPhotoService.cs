using MAUI.Template.Abstractions.Photos.Models;

namespace MAUI.Template.Abstractions.Photos
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPhotosAsync(CancellationToken cancellationToken);
    }
}