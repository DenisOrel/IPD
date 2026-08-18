
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.SetWindowPosFlags
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

[Flags]
internal enum SetWindowPosFlags : uint
{
  /// <summary> If the calling thread and the thread that owns the window are attached to different
  /// input queues, the system posts the request to the thread that owns the window.
  /// This prevents the calling thread from blocking its execution while other threads process
  /// the request.
  /// </summary>
  SWP_ASYNCWINDOWPOS = 16384, // 0x00004000
  /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
  SWP_DEFERERASE = 8192, // 0x00002000
  /// <summary>
  ///     Draws a frame (defined in the window's class description) around the window.
  /// </summary>
  SWP_DRAWFRAME = 32, // 0x00000020
  /// <summary>
  ///     Applies new frame styles set using the SetWindowLong function.
  ///     Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
  ///     If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size
  ///     is being changed.
  /// </summary>
  SWP_FRAMECHANGED = SWP_DRAWFRAME, // 0x00000020
  /// <summary>Hides the window.</summary>
  SWP_HIDEWINDOW = 128, // 0x00000080
  /// <summary>
  ///     Does not activate the window. If this flag is not set, the window is activated and moved
  ///     to the top of either the topmost or non-topmost group (depending on the setting of the
  ///     hWndInsertAfter parameter).
  /// </summary>
  SWP_NOACTIVATE = 16, // 0x00000010
  /// <summary>
  ///     Discards the entire contents of the client area. If this flag is not specified,
  ///     the valid contents of the client area are saved and copied back into the client area
  ///     after the window is sized or repositioned.
  /// </summary>
  SWP_NOCOPYBITS = 256, // 0x00000100
  /// <summary>
  ///     Retains the current position (ignores X and Y parameters).
  /// </summary>
  SWP_NOMOVE = 2,
  /// <summary>
  ///     Does not change the owner window's position in the Z order.
  /// </summary>
  SWP_NOOWNERZORDER = 512, // 0x00000200
  /// <summary>
  ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs.
  ///     This applies to the client area, the nonclient area (including the title bar and
  ///     scroll bars), and any part of the parent window uncovered as a result of the window
  ///     being moved. When this flag is set, the application must explicitly invalidate or redraw
  ///     any parts of the window and parent window that need redrawing.
  /// </summary>
  SWP_NOREDRAW = 8,
  /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
  SWP_NOREPOSITION = SWP_NOOWNERZORDER, // 0x00000200
  /// <summary>
  ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
  /// </summary>
  SWP_NOSENDCHANGING = 1024, // 0x00000400
  /// <summary>
  ///     Retains the current size (ignores the cx and cy parameters).
  /// </summary>
  SWP_NOSIZE = 1,
  /// <summary>
  ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
  /// </summary>
  SWP_NOZORDER = 4,
  /// <summary>Displays the window.</summary>
  SWP_SHOWWINDOW = 64, // 0x00000040
}
