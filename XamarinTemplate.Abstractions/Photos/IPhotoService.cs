using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XamarinTemplate.Abstractions.Photos.Models;

namespace XamarinTemplate.Abstractions.Photos
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPhotosAsync(CancellationToken cancellationToken);
    }
}