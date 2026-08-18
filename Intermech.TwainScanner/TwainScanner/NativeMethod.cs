// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.NativeMethod
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

/// <summary>Native methods</summary>
internal class NativeMethod
{
  internal const int WM_QUIT = 18;

  /// <summary>Get current thread ID.</summary>
  /// <returns></returns>
  [DllImport("kernel32.dll")]
  internal static extern uint GetCurrentThreadId();

  /// <summary>Get current process ID.</summary>
  [DllImport("kernel32.dll")]
  internal static extern uint GetCurrentProcessId();

  /// <summary>
  /// The GetMessage function retrieves a message from the calling thread's
  /// message queue. The function dispatches incoming sent messages until a
  /// posted message is available for retrieval.
  /// </summary>
  /// <param name="lpMsg">
  /// Pointer to an MSG structure that receives message information from
  /// the thread's message queue.
  /// </param>
  /// <param name="hWnd">
  /// Handle to the window whose messages are to be retrieved.
  /// </param>
  /// <param name="wMsgFilterMin">
  /// Specifies the integer value of the lowest message value to be
  /// retrieved.
  /// </param>
  /// <param name="wMsgFilterMax">
  /// Specifies the integer value of the highest message value to be
  /// retrieved.
  /// </param>
  /// <returns></returns>
  [DllImport("user32.dll")]
  internal static extern bool GetMessage(
    out MSG lpMsg,
    IntPtr hWnd,
    uint wMsgFilterMin,
    uint wMsgFilterMax);

  /// <summary>
  /// The TranslateMessage function translates virtual-key messages into
  /// character messages. The character messages are posted to the calling
  /// thread's message queue, to be read the next time the thread calls the
  /// GetMessage or PeekMessage function.
  /// </summary>
  /// <param name="lpMsg"></param>
  /// <returns></returns>
  [DllImport("user32.dll")]
  internal static extern bool TranslateMessage([In] ref MSG lpMsg);

  /// <summary>
  /// The DispatchMessage function dispatches a message to a window
  /// procedure. It is typically used to dispatch a message retrieved by
  /// the GetMessage function.
  /// </summary>
  /// <param name="lpMsg"></param>
  /// <returns></returns>
  [DllImport("user32.dll")]
  internal static extern IntPtr DispatchMessage([In] ref MSG lpMsg);

  /// <summary>
  /// The PostThreadMessage function posts a message to the message queue
  /// of the specified thread. It returns without waiting for the thread to
  /// process the message.
  /// </summary>
  /// <param name="idThread">
  /// Identifier of the thread to which the message is to be posted.
  /// </param>
  /// <param name="Msg">Specifies the type of message to be posted.</param>
  /// <param name="wParam">
  /// Specifies additional message-specific information.
  /// </param>
  /// <param name="lParam">
  /// Specifies additional message-specific information.
  /// </param>
  /// <returns></returns>
  [DllImport("user32.dll")]
  internal static extern bool PostThreadMessage(
    uint idThread,
    uint Msg,
    UIntPtr wParam,
    IntPtr lParam);
}
