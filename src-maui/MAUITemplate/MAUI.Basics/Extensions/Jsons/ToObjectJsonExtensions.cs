using System.Buffers;
using System.Text.Json;

namespace MAUI.Basics.Extensions.Jsons
{
    /// <summary>
    /// This extension is included in NET 6.0 projects.
    /// Deserialize method can be called on JsonElement.
    /// </summary>
    public static class ToObjectJsonExtensions
    {
        public static T ToObject<T>(this JsonElement element, JsonSerializerOptions options = null)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
                element.WriteTo(writer);
            
            return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, options);
        }

        public static T ToObject<T>(this JsonDocument document, JsonSerializerOptions options = null)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            
            return document.RootElement.ToObject<T>(options);
        }      
    }
}