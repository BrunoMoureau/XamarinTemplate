namespace XamarinTemplate.Api.Collections.Photos.Factories
{
    public interface IPhotoApiFactory
    {
        IPhotoApi Create(string baseUrl);
    }
}