using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XamarinTemplate.Views.List.Models;

namespace XamarinTemplate.Api
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPhotosAsync(CancellationToken cancellationToken);
    }
}