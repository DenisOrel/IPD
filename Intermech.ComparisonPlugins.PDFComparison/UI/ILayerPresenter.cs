
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.ILayerPresenter




using Intermech.ComparisonPlugins.PDFComparison.Common;
using System;
using System.Drawing;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    internal interface ILayerPresenter
    {
      event EventHandler PageUpdated;

      event EventHandler OnSelectObjectClick;

      void LoadFile(FileDescription comparedFile);

      Image PageImage { get; }
    }
}
