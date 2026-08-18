
// Type: Intermech.Mvp.Native.Windows.Dialogs.YesNoMessageDialogWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using Intermech.Mvp.Components.Dialogs;
using System.Windows.Forms;


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    public sealed class YesNoMessageDialogWrapper : 
      SelectionDialogWrapper,
      IYesNoMessageView,
      ISimpleMessageView,
      IView,
      IOperationConfirmationView
    {
      private string text;
      private string caption;
      private MessageIcon icon;
      private bool allowCancel;
      private bool isCancelled;

      protected override void DoPrepareDialog()
      {
        base.DoPrepareDialog();
        this.isCancelled = false;
      }

      protected override DialogResult DoShowDialog(IWin32Window owner = null)
      {
        MessageBoxButtons buttons = this.allowCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;
        DialogResult dialogResult = MessageBox.Show(owner, this.text, this.caption, buttons, SystemDialogWrapper.ConvertIcon(this.icon));
        switch (dialogResult)
        {
          case DialogResult.Cancel:
            this.isCancelled = true;
            break;
          case DialogResult.Yes:
            dialogResult = DialogResult.OK;
            break;
        }
        return dialogResult;
      }

      bool IYesNoMessageView.AllowCancel
      {
        get => this.allowCancel;
        set => this.allowCancel = value;
      }

      bool IYesNoMessageView.IsCancelled => this.isCancelled;

      string ISimpleMessageView.Caption
      {
        get => this.caption;
        set => this.caption = value;
      }

      string ISimpleMessageView.Text
      {
        get => this.text;
        set => this.text = value;
      }

      MessageIcon ISimpleMessageView.Icon
      {
        get => this.icon;
        set => this.icon = value;
      }
    }
}
