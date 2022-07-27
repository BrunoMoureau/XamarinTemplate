using MAUI.Template.Api.Collections.Photos.Dtos;
using Refit;

namespace MAUI.Template.Api.Collections.Photos
{
    public partial interface IPhotoApi
    {
        [Get("/photos")]
        public Task<List<PhotoDto>> GetPhotosAsync(CancellationToken cancellationToken);
    }
}