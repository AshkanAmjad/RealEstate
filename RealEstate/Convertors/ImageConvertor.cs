using SkiaSharp;

namespace RealEstate.Convertors
{
    public class ImageConvertor
    {
        public void ResizeImage(string inputImagePath, string outputImagePath, int newWidth,int quality)
        {
                using var inputStream = File.OpenRead(inputImagePath);
                using var originalBitmap = SKBitmap.Decode(inputStream);

                double aspectRatio = (double)originalBitmap.Height / originalBitmap.Width;
                int newHeight = (int)(newWidth * aspectRatio);

                using var resizedBitmap = originalBitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);

                using var output = File.OpenWrite(outputImagePath);
                using var image = SKImage.FromBitmap(resizedBitmap);

                using var data = image.Encode(SKEncodedImageFormat.Jpeg, quality);
                data.SaveTo(output);
            
        }
    }
}
