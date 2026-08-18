
// Type: Intermech.ComparisonPlugins.PDFComparison.ImageProcessing.ImageContainer




using System.Drawing;


namespace Intermech.ComparisonPlugins.PDFComparison.ImageProcessing
{
    public class ImageContainer
    {
      private Image _image;

      public Image Data
      {
        get => this._image;
        set
        {
          this._image?.Dispose();
          this._image = value;
        }
      }
    }
}
