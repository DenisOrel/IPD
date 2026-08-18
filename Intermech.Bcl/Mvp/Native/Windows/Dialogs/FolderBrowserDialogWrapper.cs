
// Type: Intermech.Mvp.Native.Windows.Dialogs.FolderBrowserDialogWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using Intermech.Mvp.Components.Dialogs;
using System.Windows.Forms;


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    public sealed class FolderBrowserDialogWrapper : 
      SelectionDialogWrapper,
      IFolderBrowserView,
      IView,
      IOperationConfirmationView
    {
      private readonly FolderBrowserDialog dlg;

      public FolderBrowserDialogWrapper() => this.dlg = new FolderBrowserDialog();

      protected override DialogResult DoShowDialog(IWin32Window owner = null)
      {
        return this.dlg.ShowDialog(owner);
      }

      string IFolderBrowserView.Description
      {
        get => this.dlg.Description;
        set => this.dlg.Description = value;
      }

      bool IFolderBrowserView.AllowNewFolders
      {
        get => this.dlg.ShowNewFolderButton;
        set => this.dlg.ShowNewFolderButton = value;
      }

      string IFolderBrowserView.SelectedPath
      {
        get => this.dlg.SelectedPath;
        set => this.dlg.SelectedPath = value;
      }
    }
}
