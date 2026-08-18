
// Type: Intermech.PdfPrintCenter.Services.LayoutSettingsService




using Intermech.PdfPrintCenter.Connector;
using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace Intermech.PdfPrintCenter.Services
{
    internal class LayoutSettingsService : ILayoutSettingsService
    {
      private static readonly string DefaultLayoutExtension = "lxml";
      private static readonly string LayoutsDirectory = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "layouts");
      private readonly object syncRoot = new object();
      private IPDMSystemService pdmSystemService;

      public LayoutSettingsService(IPDMSystemService pdmSystemService)
      {
        this.pdmSystemService = pdmSystemService;
      }

      public object ChooseLayout()
      {
        lock (this.syncRoot)
          return this.pdmSystemService.ChooseLayout();
      }

      public LayoutDescriptor LoadLayout(object layoutId)
      {
        lock (this.syncRoot)
        {
          PDMLayoutInfo pdmLayoutInfo = this.pdmSystemService.LoadLayout(layoutId);
          if (pdmLayoutInfo != null)
          {
            LayoutDescriptor layoutDescriptor = new LayoutDescriptor(pdmLayoutInfo.Name, pdmLayoutInfo.Content);
            if (layoutDescriptor.IsLoaded)
              return layoutDescriptor;
          }
          return (LayoutDescriptor) null;
        }
      }

      public List<LayoutDescriptor> LoadAllLayouts()
      {
        List<LayoutDescriptor> layoutDescriptorList = new List<LayoutDescriptor>();
        lock (this.syncRoot)
        {
          foreach (object layoutId in this.pdmSystemService.GetLayoutsId())
          {
            PDMLayoutInfo pdmLayoutInfo = this.pdmSystemService.LoadLayout(layoutId);
            if (pdmLayoutInfo != null)
            {
              LayoutDescriptor layoutDescriptor = new LayoutDescriptor(pdmLayoutInfo.Name, pdmLayoutInfo.Content);
              if (layoutDescriptor.IsLoaded)
                layoutDescriptorList.Add(layoutDescriptor);
            }
          }
          return layoutDescriptorList;
        }
      }

      public object SaveLayout(LayoutDescriptor layout, object layoutId = null)
      {
        lock (this.syncRoot)
        {
          string xml = layout.CreateXml();
          layoutId = this.pdmSystemService.SaveLayout(new PDMLayoutInfo(layout.Caption, xml), layoutId);
        }
        return layoutId;
      }
    }
}
