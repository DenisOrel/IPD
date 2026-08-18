
// Type: Intermech.Runtime.ComInterop.MessageFilter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime.ComInterop.ComTypes;
using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>Базовый класс фильтра для очереди сообщений COM.</summary>
    public abstract class MessageFilter : IMessageFilter
    {
      [ThreadStatic]
      private static IMessageFilter lastInstalledFilter;

      /// <summary>
      /// Provides a single entry point for incoming calls.
      /// This method is called prior to each method invocation originating outside the current process and provides the ability to filter or reject incoming calls (or callbacks) to an object or a process.
      /// </summary>
      /// <param name="dwCallType">The type of incoming call that has been received. Possible values are from the enumeration CALLTYPE</param>
      /// <param name="hTaskCaller">The thread id of the caller</param>
      /// <param name="dwTickCount">The elapsed tick count since the outgoing call was made, if dwCallType is not CALLTYPE_TOPLEVEL. If dwCallType is CALLTYPE_TOPLEVEL, dwTickCount should be ignored</param>
      /// <param name="lpInterfaceInfo">A pointer to an INTERFACEINFO structure that identifies the object, interface, and method being called. In the case of DDE calls, lpInterfaceInfo can be NULL because the DDE layer does not return interface information</param>
      /// <returns>Message status</returns>
      public virtual SERVERCALL HandleInComingCall(
        int dwCallType,
        IntPtr hTaskCaller,
        int dwTickCount,
        IntPtr lpInterfaceInfo)
      {
        return SERVERCALL.SERVERCALL_ISHANDLED;
      }

      /// <summary>
      /// Provides applications with an opportunity to display a dialog box offering retry, cancel, or task-switching options.
      /// </summary>
      /// <param name="hTaskCallee">The thread id of the called application</param>
      /// <param name="dwTickCount">The number of elapsed ticks since the call was made</param>
      /// <param name="dwRejectType">Specifies either SERVERCALL_REJECTED or SERVERCALL_RETRYLATER, as returned by the object application</param>
      /// <returns>The number of tick before retry or -1 to cancel a call</returns>
      public virtual int RetryRejectedCall(
        IntPtr hTaskCallee,
        int dwTickCount,
        SERVERCALL dwRejectType)
      {
        return -1;
      }

      /// <summary>
      /// Indicates that a message has arrived while COM is waiting to respond to a remote call.
      /// Handling input while waiting for an outgoing call to finish can introduce complications. The application should determine whether to process the message without interrupting the call, to continue waiting, or to cancel the operation.
      /// </summary>
      /// <param name="hTaskCallee">The thread id of the called application</param>
      /// <param name="dwTickCount">The number of ticks since the call was made. It is calculated from the GetTickCount function</param>
      /// <param name="dwPendingType">The type of call made during which a message or event was received</param>
      /// <returns>Message status</returns>
      public virtual PENDINGMSG MessagePending(
        IntPtr hTaskCallee,
        int dwTickCount,
        PENDINGTYPE dwPendingType)
      {
        return PENDINGMSG.PENDINGMSG_WAITNOPROCESS;
      }

      /// <summary>Возвращает или задает фильтр для текущего потока.</summary>
      public static IMessageFilter Current
      {
        get
        {
          return MessageFilter.lastInstalledFilter == null ? MessageFilter.GetCurrentThreadFilter() : MessageFilter.lastInstalledFilter;
        }
        set
        {
          IMessageFilter current = MessageFilter.Current;
          if (current == value)
            return;
          MessageFilter.SetCurrentThreadFilter(value != null ? value : current);
          MessageFilter.lastInstalledFilter = value;
        }
      }

      private static IMessageFilter GetCurrentThreadFilter()
      {
        IMessageFilter lplpMessageFilter;
        int errorCode = NativeMethods.CoRegisterMessageFilter((IMessageFilter) null, out lplpMessageFilter);
        if (errorCode != 0)
          Marshal.ThrowExceptionForHR(errorCode);
        return lplpMessageFilter;
      }

      private static void SetCurrentThreadFilter(IMessageFilter filter)
      {
        int errorCode = NativeMethods.CoRegisterMessageFilter(filter, out IMessageFilter _);
        if (errorCode == 0)
          return;
        Marshal.ThrowExceptionForHR(errorCode);
      }
    }
}
