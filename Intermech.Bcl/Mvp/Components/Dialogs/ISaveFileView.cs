
// Type: Intermech.Mvp.Components.Dialogs.ISaveFileView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Mvp.Components.Dialogs
{
    public interface ISaveFileView : IView, IOperationConfirmationView
    {
      string Title { get; set; }

      string InitialDirectory { get; set; }

      string FileName { get; set; }

      string DefaultExtension { get; set; }

      string ExtensionFilter { get; set; }

      string SelectedPath { get; }
    }
}
