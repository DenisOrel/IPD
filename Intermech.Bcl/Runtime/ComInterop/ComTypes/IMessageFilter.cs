
// Type: Intermech.Runtime.ComInterop.ComTypes.IMessageFilter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop.ComTypes
{
    /// <summary>
    /// Provides COM servers and applications with the ability to selectively handle incoming and outgoing COM messages while waiting for
    /// responses from synchronous calls. Filtering messages helps to ensure that calls are handled in a manner that improves performance and
    /// avoids deadlocks. COM messages can be synchronous, asynchronous, or input-synchronized; the majority of interface calls are synchronous.
    /// </summary>
    [Guid("00000016-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IMessageFilter
    {
      /// <summary>
      /// Provides a single entry point for incoming calls.
      /// This method is called prior to each method invocation originating outside the current process and provides the ability to filter or reject incoming calls (or callbacks) to an object or a process.
      /// </summary>
      /// <param name="dwCallType">The type of incoming call that has been received. Possible values are from the enumeration CALLTYPE</param>
      /// <param name="hTaskCaller">The thread id of the caller</param>
      /// <param name="dwTickCount">The elapsed tick count since the outgoing call was made, if dwCallType is not CALLTYPE_TOPLEVEL. If dwCallType is CALLTYPE_TOPLEVEL, dwTickCount should be ignored</param>
      /// <param name="lpInterfaceInfo">A pointer to an INTERFACEINFO structure that identifies the object, interface, and method being called. In the case of DDE calls, lpInterfaceInfo can be NULL because the DDE layer does not return interface information</param>
      /// <returns>Message status</returns>
      [MethodImpl(MethodImplOptions.PreserveSig)]
      [return: MarshalAs(UnmanagedType.I4)]
      SERVERCALL HandleInComingCall(
        int dwCallType,
        IntPtr hTaskCaller,
        int dwTickCount,
        IntPtr lpInterfaceInfo);

      /// <summary>
      /// Provides applications with an opportunity to display a dialog box offering retry, cancel, or task-switching options.
      /// </summary>
      /// <param name="hTaskCallee">The thread id of the called application</param>
      /// <param name="dwTickCount">The number of elapsed ticks since the call was made</param>
      /// <param name="dwRejectType">Specifies either SERVERCALL_REJECTED or SERVERCALL_RETRYLATER, as returned by the object application</param>
      /// <returns>The number of tick before retry or -1 to cancel a call</returns>
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, [MarshalAs(UnmanagedType.I4)] SERVERCALL dwRejectType);

      /// <summary>
      /// Indicates that a message has arrived while COM is waiting to respond to a remote call.
      /// Handling input while waiting for an outgoing call to finish can introduce complications. The application should determine whether to process the message without interrupting the call, to continue waiting, or to cancel the operation.
      /// </summary>
      /// <param name="hTaskCallee">The thread id of the called application</param>
      /// <param name="dwTickCount">The number of ticks since the call was made. It is calculated from the GetTickCount function</param>
      /// <param name="dwPendingType">The type of call made during which a message or event was received</param>
      /// <returns>Message status</returns>
      [MethodImpl(MethodImplOptions.PreserveSig)]
      [return: MarshalAs(UnmanagedType.I4)]
      PENDINGMSG MessagePending(IntPtr hTaskCallee, int dwTickCount, [MarshalAs(UnmanagedType.I4)] PENDINGTYPE dwPendingType);
    }
}
