
// Type: Intermech.Mvp.Native.Windows.Dialogs.SystemDialogWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components.Dialogs;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    public abstract class SystemDialogWrapper : IView
    {
      private SystemDialogDisplayState viewDisplayState;

      public SystemDialogWrapper() => this.viewDisplayState = new SystemDialogDisplayState(this);

      public void ShowDialog(IWin32Window owner = null)
      {
        this.ShowInternal();
        try
        {
          this.DoPrepareDialog();
          if (this.DoShowDialog(owner) != DialogResult.OK)
            return;
          this.DoProcessSuccess();
        }
        finally
        {
          this.CloseInternal();
        }
      }

      private void ShowInternal() => this.viewDisplayState.RaiseDialogShown();

      private void CloseInternal() => this.viewDisplayState.RaiseDialogClosed();

      protected virtual void DoPrepareDialog()
      {
      }

      protected abstract DialogResult DoShowDialog(IWin32Window owner = null);

      protected virtual void DoProcessSuccess()
      {
      }

      protected static MessageBoxIcon ConvertIcon(MessageIcon mvpIcon)
      {
        switch (mvpIcon)
        {
          case MessageIcon.None:
            return MessageBoxIcon.None;
          case MessageIcon.Information:
            return MessageBoxIcon.Asterisk;
          case MessageIcon.Question:
            return MessageBoxIcon.Question;
          case MessageIcon.Warning:
            return MessageBoxIcon.Exclamation;
          case MessageIcon.Error:
            return MessageBoxIcon.Hand;
          case MessageIcon.Asterisk:
            return MessageBoxIcon.Asterisk;
          case MessageIcon.Exclamation:
            return MessageBoxIcon.Exclamation;
          case MessageIcon.Stop:
            return MessageBoxIcon.Hand;
          default:
            throw new NotImplementedException();
        }
      }

      public IViewDisplayState DisplayState
      {
        [DebuggerStepThrough] get => (IViewDisplayState) this.viewDisplayState;
      }
    }
}
