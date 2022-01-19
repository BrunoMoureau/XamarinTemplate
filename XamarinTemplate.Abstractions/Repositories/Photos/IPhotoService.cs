using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XamarinTemplate.Abstractions.Repositories.Photos.Models;

namespace XamarinTemplate.Abstractions.Repositories.Photos
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPhotosAsync(CancellationToken cancellationToken);
    }
}