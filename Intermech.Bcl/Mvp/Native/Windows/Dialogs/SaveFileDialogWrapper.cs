
// Type: Intermech.Mvp.Native.Windows.Dialogs.SaveFileDialogWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using Intermech.Mvp.Components.Dialogs;
using System.Windows.Forms;


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    public sealed class SaveFileDialogWrapper : 
      SelectionDialogWrapper,
      ISaveFileView,
      IView,
      IOperationConfirmationView
    {
      private readonly SaveFileDialog dlg;
      private string selectedPath;

      public SaveFileDialogWrapper()
      {
        this.dlg = new SaveFileDialog();
        this.dlg.SupportMultiDottedExtensions = true;
        this.dlg.OverwritePrompt = true;
        this.dlg.AddExtension = true;
        this.dlg.CheckPathExists = true;
        this.dlg.RestoreDirectory = true;
      }

      protected override DialogResult DoShowDialog(IWin32Window owner = null)
      {
        return this.dlg.ShowDialog(owner);
      }

      protected override void DoProcessSuccess()
      {
        this.selectedPath = this.dlg.FileName;
        base.DoProcessSuccess();
      }

      string ISaveFileView.Title
      {
        get => this.dlg.Title;
        set => this.dlg.Title = value;
      }

      string ISaveFileView.InitialDirectory
      {
        get => this.dlg.InitialDirectory;
        set => this.dlg.InitialDirectory = value;
      }

      string ISaveFileView.FileName
      {
        get => this.dlg.FileName;
        set => this.dlg.FileName = value;
      }

      string ISaveFileView.DefaultExtension
      {
        get => this.dlg.DefaultExt;
        set => this.dlg.DefaultExt = value;
      }

      string ISaveFileView.ExtensionFilter
      {
        get => this.dlg.Filter;
        set => this.dlg.Filter = value;
      }

      string ISaveFileView.SelectedPath => this.selectedPath;
    }
}
