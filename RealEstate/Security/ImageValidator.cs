using Microsoft.AspNetCore.Http;
using SkiaSharp;

namespace RealEstate.Security
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    // Create SKBitmap from the stream
                    using (var skBitmap = SKBitmap.Decode(stream))
                    {
                        return skBitmap != null;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
