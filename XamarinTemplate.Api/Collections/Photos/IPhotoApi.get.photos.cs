using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using XamarinTemplate.Api.Collections.Photos.Dtos;

namespace XamarinTemplate.Api.Collections.Photos
{
    public partial interface IPhotoApi
    {
        [Get("/photos")]
        public Task<List<PhotoDto>> GetPhotosAsync(CancellationToken cancellationToken);
    }
}