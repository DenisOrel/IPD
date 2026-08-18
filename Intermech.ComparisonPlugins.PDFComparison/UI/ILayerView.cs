
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.ILayerView




using System;
using System.Drawing;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    public interface ILayerView
    {
      event EventHandler ClickOpenButton;

      event EventHandler ClickNextPageButton;

      event EventHandler ClickPrevPageButton;

      event EventHandler ChangedPageNumber;

      int PageNumber { get; }

      void UpdateUI(string fileCaption, int pageNumber, int pageCount);

      void SetColor(Color color);
    }
}
