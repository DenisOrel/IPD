
// Type: Intermech.Mvp.Components.Dialogs.IFolderBrowserView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Mvp.Components.Dialogs
{
    public interface IFolderBrowserView : IView, IOperationConfirmationView
    {
      string Description { get; set; }

      bool AllowNewFolders { get; set; }

      string SelectedPath { get; set; }
    }
}
