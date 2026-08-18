
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.IMainView




using System;
using System.Drawing;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    public interface IMainView
    {
      event EventHandler ChangedView;

      ILayerView TopLayerView { get; }

      ILayerView LowLayerView { get; }

      float Angle { get; }

      double Zoom { get; }

      Point Offset { get; }

      int ViewType { get; }

      void SetImage(Image image);

      void UpdateImage(Image image);
    }
}
