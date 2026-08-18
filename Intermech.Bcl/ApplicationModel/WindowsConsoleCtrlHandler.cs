
// Type: Intermech.ApplicationModel.WindowsConsoleCtrlHandler
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;


namespace Intermech.ApplicationModel
{
    internal sealed class WindowsConsoleCtrlHandler : CriticalFinalizerObject
    {
      private object eventSender;
      private bool isActive;
      private NativeMethods.ConsoleCtrlHandlerRoutine handlerRoutine;

      public WindowsConsoleCtrlHandler(object eventSender)
      {
        this.eventSender = eventSender != null ? eventSender : throw new ArgumentNullException(nameof (eventSender));
        this.handlerRoutine = new NativeMethods.ConsoleCtrlHandlerRoutine(this.ConsoleCtrlHandler);
      }

      ~WindowsConsoleCtrlHandler() => this.Deactivate(false);

      public void Activate()
      {
        if (this.isActive)
          return;
        if (WindowsConsoleCtrlHandler.NativeMethods.SetConsoleCtrlHandler(this.handlerRoutine, true))
          this.isActive = true;
        else
          Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      public void Deactivate(bool throwOnError = true)
      {
        if (!this.isActive)
          return;
        if (WindowsConsoleCtrlHandler.NativeMethods.SetConsoleCtrlHandler(this.handlerRoutine, false))
        {
          this.isActive = false;
        }
        else
        {
          if (!throwOnError)
            return;
          Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
      }

      private bool ConsoleCtrlHandler(
        NativeMethods.ConsoleCtrlHandlerEventCode eventCode)
      {
        if (eventCode == WindowsConsoleCtrlHandler.NativeMethods.ConsoleCtrlHandlerEventCode.CTRL_CLOSE_EVENT)
        {
          EventHandler onCloseConsole = this.OnCloseConsole;
          if (onCloseConsole != null)
            onCloseConsole(this.eventSender, EventArgs.Empty);
        }
        return false;
      }

      public event EventHandler OnCloseConsole;

      private static class NativeMethods
      {
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleCtrlHandler(
          ConsoleCtrlHandlerRoutine handler,
          bool addOrRemove);

        public enum ConsoleCtrlHandlerEventCode
        {
          CTRL_C_EVENT = 0,
          CTRL_BREAK_EVENT = 1,
          CTRL_CLOSE_EVENT = 2,
          CTRL_LOGOFF_EVENT = 5,
          CTRL_SHUTDOWN_EVENT = 6,
        }

        public delegate bool ConsoleCtrlHandlerRoutine(
          ConsoleCtrlHandlerEventCode eventCode);
      }
    }
}
