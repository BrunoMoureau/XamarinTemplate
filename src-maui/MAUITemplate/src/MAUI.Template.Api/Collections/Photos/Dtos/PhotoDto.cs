namespace MAUI.Template.Api.Collections.Photos.Dtos
{
    public record PhotoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public int AlbumId { get; set; }
    }
}