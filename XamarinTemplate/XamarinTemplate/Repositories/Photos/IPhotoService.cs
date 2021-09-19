using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XamarinTemplate.Features.Gallery.Models;

namespace XamarinTemplate.Repositories.Photos
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPhotosAsync(CancellationToken cancellationToken);
    }
}