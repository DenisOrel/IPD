
// Type: Intermech.Mvp.Components.Dialogs.IOpenFileView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Mvp.Components.Dialogs
{
    public interface IOpenFileView : IView, IOperationConfirmationView
    {
      string Title { get; set; }

      string InitialDirectory { get; set; }

      string FileName { get; set; }

      string DefaultExtension { get; set; }

      string ExtensionFilter { get; set; }

      bool AllowMultiSelect { get; set; }

      List<string> SelectedFiles { get; }
    }
}
