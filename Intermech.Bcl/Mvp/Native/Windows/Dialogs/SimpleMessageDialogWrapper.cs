
// Type: Intermech.Mvp.Native.Windows.Dialogs.SimpleMessageDialogWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components.Dialogs;
using System.Windows.Forms;


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    public sealed class SimpleMessageDialogWrapper : SystemDialogWrapper, ISimpleMessageView, IView
    {
      private string text;
      private string caption;
      private MessageIcon icon;

      protected override DialogResult DoShowDialog(IWin32Window owner = null)
      {
        return MessageBox.Show(owner, this.text, this.caption, MessageBoxButtons.OK, SystemDialogWrapper.ConvertIcon(this.icon));
      }

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
