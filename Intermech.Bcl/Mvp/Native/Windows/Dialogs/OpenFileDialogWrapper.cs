
// Type: Intermech.Mvp.Native.Windows.Dialogs.OpenFileDialogWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using Intermech.Mvp.Components;
using Intermech.Mvp.Components.Dialogs;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    public sealed class OpenFileDialogWrapper : 
      SelectionDialogWrapper,
      IOpenFileView,
      IView,
      IOperationConfirmationView
    {
      private readonly OpenFileDialog dlg;
      private List<string> selectedFiles;

      public OpenFileDialogWrapper()
      {
        this.dlg = new OpenFileDialog();
        this.dlg.AutoUpgradeEnabled = true;
        this.dlg.RestoreDirectory = true;
        this.dlg.CheckFileExists = true;
        this.dlg.Filter = LocalizationHolder.rm.GetString("SR_1688");
        this.selectedFiles = new List<string>(0);
      }

      protected override DialogResult DoShowDialog(IWin32Window owner = null)
      {
        return this.dlg.ShowDialog(owner);
      }

      protected override void DoProcessSuccess()
      {
        this.selectedFiles.Clear();
        this.selectedFiles.AddRange((IEnumerable<string>) this.dlg.FileNames);
        base.DoProcessSuccess();
      }

      string IOpenFileView.Title
      {
        get => this.dlg.Title;
        set => this.dlg.Title = value;
      }

      string IOpenFileView.InitialDirectory
      {
        get => this.dlg.InitialDirectory;
        set => this.dlg.InitialDirectory = value;
      }

      string IOpenFileView.FileName
      {
        get => this.dlg.FileName;
        set => this.dlg.FileName = value;
      }

      string IOpenFileView.DefaultExtension
      {
        get => this.dlg.DefaultExt;
        set => this.dlg.DefaultExt = value;
      }

      string IOpenFileView.ExtensionFilter
      {
        get => this.dlg.Filter;
        set => this.dlg.Filter = value;
      }

      bool IOpenFileView.AllowMultiSelect
      {
        get => this.dlg.Multiselect;
        set => this.dlg.Multiselect = value;
      }

      List<string> IOpenFileView.SelectedFiles => this.selectedFiles;
    }
}
