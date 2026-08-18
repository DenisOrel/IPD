
// Type: Intermech.Mvp.Native.Windows.Dialogs.SystemDialogDisplayState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    internal sealed class SystemDialogDisplayState : AbstractViewDisplayState
    {
      private SystemDialogWrapper dialog;

      public SystemDialogDisplayState(SystemDialogWrapper dialog) => this.dialog = dialog;

      public void RaiseDialogShown() => this.RaiseViewShown();

      public void RaiseDialogClosed() => this.RaiseViewClosed();
    }
}
