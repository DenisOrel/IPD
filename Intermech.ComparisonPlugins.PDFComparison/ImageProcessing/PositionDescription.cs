
// Type: Intermech.ComparisonPlugins.PDFComparison.ImageProcessing.PositionDescription




using System.Drawing;


namespace Intermech.ComparisonPlugins.PDFComparison.ImageProcessing
{
    public class PositionDescription
    {
      public float Angle { get; }

      public double Scale { get; }

      public Point Offset { get; }

      public PositionDescription(float angle, double scale, Point offset)
      {
        this.Angle = angle;
        this.Scale = scale;
        this.Offset = offset;
      }
    }
}
