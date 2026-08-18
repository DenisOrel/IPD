
// Type: Intermech.PdfPrintCenter.Interfaces.ILayoutSettingsService




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Interfaces
{
    internal interface ILayoutSettingsService
    {
      object ChooseLayout();

      LayoutDescriptor LoadLayout(object layoutId);

      List<LayoutDescriptor> LoadAllLayouts();

      object SaveLayout(LayoutDescriptor layout, object layoutId = null);
    }
}
