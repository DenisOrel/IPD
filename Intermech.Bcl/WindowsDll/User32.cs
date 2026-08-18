
// Type: Intermech.WindowsDll.User32
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.WindowsDll
{
    /// <summary>Функции User32.dll</summary>
    public static class User32
    {
      public const int WM_SETREDRAW = 11;
      public const int CCHDEVICENAME = 32 /*0x20*/;
      /// <summary>Window status "window is active"</summary>
      public const int WS_ACTIVECAPTION = 1;
      private const string LibName = "User32.dll";
      private const string Namespace = "User32::";

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(IntPtr hWnd, [ValueProvider("Intermech.Win32.Message")] int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(
        IntPtr hWnd,
        [ValueProvider("Intermech.Win32.Message")] int Msg,
        IntPtr wParam,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(IntPtr hWnd, [ValueProvider("Intermech.Win32.Message")] int Msg, IntPtr wParam, IntPtr lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(IntPtr hWnd, [ValueProvider("Intermech.Win32.Message")] int msg, int wParam, IntPtr lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SendMessage(IntPtr hWnd, [ValueProvider("Intermech.Win32.Message")] int msg, int wParam, int lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

      /// <summary>
      /// Enumerates all nonchild windows associated with a thread by passing the handle to each window, in turn, to an application-defined callback function.
      /// EnumThreadWindows continues until the last window is enumerated or the callback function returns FALSE. To enumerate child windows of a particular
      /// window, use the EnumChildWindows function
      /// </summary>
      /// <param name="tid">The identifier of the thread whose windows are to be enumerated</param>
      /// <param name="callback">Method, that receives the window handles associated with a thread</param>
      /// <param name="lp">An application-defined value to be passed to the callback function</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll")]
      public static extern bool EnumThreadWindows(
        int tid,
        [NotNull] EnumThreadWndProc callback,
        IntPtr lp);

      /// <summary>Retrieves the name of the class to which the specified window belongs</summary>
      /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs</param>
      /// <param name="buffer">The class name string</param>
      /// <param name="bufLen">The class name length</param>
      /// <returns>
      /// If the function succeeds, the return value is the number of characters copied to the buffer, not including the terminating null character.
      /// If the function fails, the return value is zero. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" />
      /// </returns>
      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern int GetClassName([NotEmpty] IntPtr hWnd, [NotNull] StringBuilder buffer, [PositiveNumber] int bufLen);

      /// <summary>Retrieves the name of the class to which the specified window belongs</summary>
      /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs</param>
      /// <param name="buffer">The class name string</param>
      /// <param name="bufLen">The class name length</param>
      /// <returns>Number of characters copied to the buffer, not including the terminating null character</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">if result value is 0</exception>
      [NotNull]
      public static string GetClassName_ThrowWinErrors([NotEmpty] IntPtr hWnd, [PositiveNumber] int bufferSize = 256 /*0x0100*/)
      {
        StringBuilder buffer = new StringBuilder(bufferSize);
        int className = User32.GetClassName(hWnd, buffer, bufferSize);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return className != 0 ? buffer.ToString(0, className) : throw WindowsApiException.GetLastForce("GetClassName", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) typeof (StringBuilder), (ArgumentDescriptor) @(typeof (int), (object) bufferSize));
      }

      /// <summary>
      /// Retrieves the dimensions of the bounding rectangle of the specified window.
      /// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen
      /// </summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="rc">[out] RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window</param>
      /// <returns>
      /// If the function succeeds, the return value is nonzero.
      /// If the function fails, the return value is zero. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool GetWindowRect([NotEmpty] IntPtr hWnd, out Interop.RECT rc);

      /// <summary>
      /// Retrieves the dimensions of the bounding rectangle of the specified window.
      /// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen
      /// </summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <returns>RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static Interop.RECT GetWindowRect_ThrowWinErrors([NotEmpty] IntPtr hWnd)
      {
        Interop.RECT rc;
        if (!User32.GetWindowRect(hWnd, out rc))
        {
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("User32::GetWindowRect", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd));
        }
        return rc;
      }

      /// <summary>
      /// Changes the position and dimensions of the specified window.
      /// For a top-level window, the position and dimensions are relative to the upper-left corner of the screen.
      /// For a child window, they are relative to the upper-left corner of the parent window's client area
      /// </summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="x">The x coordinate</param>
      /// <param name="y">The y coordinate</param>
      /// <param name="w">The width</param>
      /// <param name="h">The height</param>
      /// <param name="repaint">True to repaint</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool MoveWindow([NotEmpty] IntPtr hWnd, int x, int y, int w, int h, bool repaint);

      /// <summary>
      /// Changes the position and dimensions of the specified window.
      /// For a top-level window, the position and dimensions are relative to the upper-left corner of the screen.
      /// For a child window, they are relative to the upper-left corner of the parent window's client area
      /// </summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="x">The x coordinate</param>
      /// <param name="y">The y coordinate</param>
      /// <param name="w">The width</param>
      /// <param name="h">The height</param>
      /// <param name="repaint">True to repaint</param>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void MoveWindow_ThrowWinErrors(
        [NotEmpty] IntPtr hWnd,
        int x,
        int y,
        int w,
        int h,
        bool repaint)
      {
        if (!User32.MoveWindow(hWnd, x, y, w, h, repaint))
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("User32::MoveWindow", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) @(typeof (int), (object) x), (ArgumentDescriptor) @(typeof (int), (object) y), (ArgumentDescriptor) @(typeof (int), (object) w), (ArgumentDescriptor) @(typeof (int), (object) h), (ArgumentDescriptor) @(typeof (bool), (object) repaint));
        }
      }

      /// <summary>
      /// Retrieves the coordinates of a window's client area.
      /// The client coordinates specify the upper-left and lower-right corners of the client area.
      /// Because client coordinates are relative to the upper-left corner of a window's client area, the coordinates of the upper-left corner are (0,0)
      /// </summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="rect">[out] The rectangle</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool GetClientRect(IntPtr hWnd, out Interop.RECT rect);

      /// <summary>
      /// Retrieves the coordinates of a window's client area.
      /// The client coordinates specify the upper-left and lower-right corners of the client area.
      /// Because client coordinates are relative to the upper-left corner of a window's client area, the coordinates of the upper-left corner are (0,0)
      /// </summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <returns>The rectangle</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static Interop.RECT GetClientRect_ThrowWinErrors([NotEmpty] IntPtr hWnd)
      {
        Interop.RECT rect;
        if (!User32.GetClientRect(hWnd, out rect))
        {
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("User32::GetClientRect", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd));
        }
        return rect;
      }

      /// <summary>The ClientToScreen function converts the client-area coordinates of a specified point to screen coordinates</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="pt">[out] The point</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool ClientToScreen(IntPtr hWnd, ref Interop.POINT pt);

      /// <summary>The ClientToScreen function converts the client-area coordinates of a specified point to screen coordinates</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="pt">The point in client coordinats</param>
      /// <returns>The point in screen coordinats</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static Interop.POINT ClientToScreen_ThrowWinErrors([NotEmpty] IntPtr hWnd, in Interop.POINT pt)
      {
        Interop.POINT pt1 = pt;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return User32.ClientToScreen(hWnd, ref pt1) ? pt1 : throw WindowsApiException.GetLastForce("User32::ClientToScreen", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) @(Modifier.Out, typeof (Interop.POINT), (object) pt));
      }

      /// <summary>
      /// Retrieves the identifier of the thread that created the specified window and,
      /// optionally, the identifier of the process that created the window
      /// </summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="processID">Identifier for the process</param>
      /// <returns>The window thread process identifier</returns>
      [DllImport("user32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.U4)]
      public static extern int GetWindowThreadProcessId([NotEmpty] IntPtr hWnd, [CanBeEmpty] IntPtr processID);

      /// <summary>Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="processID">Identifier for the process</param>
      /// <returns>The window thread process identifier</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int GetWindowThreadProcessId_ThrowWinErrors([NotEmpty] IntPtr hWnd, [CanBeEmpty] IntPtr processID)
      {
        int windowThreadProcessId = User32.GetWindowThreadProcessId(hWnd, processID);
        if (windowThreadProcessId == 0)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error != 0)
          {
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            throw new WindowsApiException(lastWin32Error, "User32::GetWindowThreadProcessId", new ArgumentDescriptor[2]
            {
              (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd),
              (ArgumentDescriptor) @(typeof (IntPtr), (object) processID)
            });
          }
        }
        return windowThreadProcessId;
      }

      /// <summary>
      /// Retrieves a handle to the desktop window. The desktop window covers the entire screen.
      /// The desktop window is the area on top of which other windows are painted
      /// </summary>
      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr GetDesktopWindow();

      /// <summary>Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window</summary>
      [DllImport("user32.dll")]
      public static extern IntPtr MonitorFromWindow([NotEmpty] IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] DefaultMonitor defaultMonitor);

      /// <summary>Retrieves information about a display monitor</summary>
      /// <param name="hMonitor">The monitor</param>
      /// <param name="monitorInfo">Information describing the monitor</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern bool GetMonitorInfo([NotEmpty] IntPtr hMonitor, [NotNull, In, Out] MonitorInfoEx monitorInfo);

      /// <summary>Retrieves information about a display monitor</summary>
      /// <param name="hMonitor">The monitor</param>
      /// <param name="monitorInfo">Information describing the monitor</param>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static MonitorInfoEx GetMonitorInfo_ThrowWinErrors([NotEmpty] IntPtr hMonitor)
      {
            MonitorInfoEx monitorInfo = new MonitorInfoEx();
        // ISSUE: explicit reference operation
        return User32.GetMonitorInfo(hMonitor, monitorInfo) ? monitorInfo : throw WindowsApiException.GetLastForce("User32::GetMonitorInfo", (ArgumentDescriptor) @(typeof (IntPtr), (object) hMonitor), (ArgumentDescriptor) typeof (MonitorInfoEx));
      }

      /// <summary>Sets the specified window's show state</summary>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool ShowWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.U4), In] ShowWindowCommand nCmdShow);

      /// <summary>Sets the specified window's show state</summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void ShowWindow_HandleWinErros([NotEmpty] IntPtr hWnd, ShowWindowCommand nCmdShow)
      {
        if (!User32.ShowWindow(hWnd, nCmdShow))
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("User32::ShowWindow", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) @(typeof (ShowWindowCommand), (object) nCmdShow));
        }
      }

      /// <summary>Releases the mouse capture from a window in the current thread and restores normal mouse input processing</summary>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool ReleaseCapture();

      /// <summary>Releases the mouse capture from a window in the current thread and restores normal mouse input processing</summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void ReleaseCapture_ThrowWinErrors()
      {
        if (!User32.ReleaseCapture())
          throw WindowsApiException.GetLastForce("User32::ReleaseCapture");
      }

      /// <summary>Retrieves information about the specified window</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="WindowInfo">[in,out] Information describing the window</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool GetWindowInfo([NotEmpty] IntPtr hWnd, [NotNull, In, Out] WINDOWINFO WindowInfo);

      /// <summary>Retrieves information about the specified window</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="WindowInfo">[in,out] Information describing the window</param>
      /// <returns><see cref="T:Intermech.WindowsDll.User32.WINDOWINFO" /></returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static WINDOWINFO GetWindowInfo_ThrowWinErrors([NotEmpty] IntPtr hWnd)
      {
            WINDOWINFO WindowInfo = new WINDOWINFO();
        // ISSUE: explicit reference operation
        return User32.GetWindowInfo(hWnd, WindowInfo) ? WindowInfo : throw WindowsApiException.GetLastForce("User32::GetWindowInfo", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) typeof (WINDOWINFO));
      }

      /// <summary>Retrieves the show state and the restored, minimized, and maximized positions of the specified window</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="lpwndpl"><see cref="T:Intermech.WindowsDll.User32.WINDOWPLACEMENT" /></param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetWindowPlacement([NotEmpty] IntPtr hWnd, [NotNull, In, Out] WINDOWPLACEMENT lpwndpl);

      /// <summary>Retrieves the show state and the restored, minimized, and maximized positions of the specified window</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <returns><see cref="T:Intermech.WindowsDll.User32.WINDOWPLACEMENT" /></returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static WINDOWPLACEMENT GetWindowPlacement_ThrowWinErros([NotEmpty] IntPtr hWnd)
      {
            WINDOWPLACEMENT lpwndpl = new WINDOWPLACEMENT();
        // ISSUE: explicit reference operation
        return User32.GetWindowPlacement(hWnd, lpwndpl) ? lpwndpl : throw WindowsApiException.GetLastForce("User32::GetWindowPlacement", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) typeof (WINDOWPLACEMENT));
      }

      /// <summary>Sets the show state and the restored, minimized, and maximized positions of the specified window</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="lpwndpl"><see cref="T:Intermech.WindowsDll.User32.WINDOWPLACEMENT" /></param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetWindowPlacement([NotEmpty] IntPtr hWnd, [NotNull, In] WINDOWPLACEMENT lpwndpl);

      /// <summary>Sets the show state and the restored, minimized, and maximized positions of the specified window</summary>
      /// <param name="hWnd">A handle to the window</param>
      /// <param name="lpwndpl"><see cref="T:Intermech.WindowsDll.User32.WINDOWPLACEMENT" /></param>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void SetWindowPlacement_ThrowWinErrors([NotEmpty] IntPtr hWnd, [NotNull] WINDOWPLACEMENT lpwndpl)
      {
        if (!User32.SetWindowPlacement(hWnd, lpwndpl))
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("User32::SetWindowPlacement", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) @(typeof (WINDOWPLACEMENT), (object) lpwndpl));
        }
      }

      /// <summary>
      /// Retrieves the specified system metric or system configuration setting.
      /// All dimensions retrieved by GetSystemMetrics are in pixels.
      /// </summary>
      /// <param name="smIndex">The system metric or configuration setting to be retrieved.</param>
      /// <returns>
      /// System metric or configuration setting.
      /// If the function fails, the return value is 0. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" />
      /// </returns>
      [DllImport("user32.dll", SetLastError = true)]
      public static extern int GetSystemMetrics([MarshalAs(UnmanagedType.I4)] SystemMetric smIndex);

      /// <summary>
      /// Retrieves the specified system metric or system configuration setting.
      /// All dimensions retrieved by GetSystemMetrics are in pixels.
      /// </summary>
      /// <param name="smIndex">The system metric or configuration setting to be retrieved.</param>
      /// <returns>System metric or configuration setting</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int GetSystemMetrics_ThrowWinErrors([MarshalAs(UnmanagedType.I4)] SystemMetric smIndex)
      {
        int systemMetrics = User32.GetSystemMetrics(smIndex);
        // ISSUE: explicit reference operation
        return systemMetrics != 0 ? systemMetrics : throw WindowsApiException.GetLastForce("User32::SetWindowPlacement", (ArgumentDescriptor) @(typeof (SystemMetric), (object) smIndex));
      }

      /// <summary>Retrieves the active input locale identifier (formerly called the keyboard layout)</summary>
      /// <param name="idThread">The identifier of the thread to query, or 0 for the current thread</param>
      /// <returns>
      /// The return value is the input locale identifier for the thread.
      /// The low word contains a Language Identifier for the input language,
      /// the high word contains a device handle to the physical layout of the keyboard.
      /// </returns>
      /// <remarks>
      /// Beginning in Windows 8:
      /// The preferred method to retrieve the language associated with the current keyboard layout or
      /// input method is a call to Windows.Globalization.Language.CurrentInputMethodLanguageTag.
      /// If your app passes language tags from CurrentInputMethodLanguageTag to any National Language Support functions,
      /// it must first convert the tags by calling ResolveLocaleName.
      /// </remarks>
      [DllImport("User32.dll")]
      public static extern IntPtr GetKeyboardLayout(int idThread);

      /// <summary>Retrieves a handle to the window that contains the specified point</summary>
      /// <param name="pnt">The point to be checked</param>
      /// <returns>
      /// Handle to the window that contains the point. If no window exists at the given point, the return value is NULL.
      /// If the point is over a static text control, the return value is a handle to the window under the static text control.
      /// </returns>
      [CanBeEmpty]
      [DllImport("user32.dll")]
      public static extern IntPtr WindowFromPoint(Interop.POINT pnt);

      /// <summary>
      /// Retrieves a handle to the child window at the specified point.
      /// The search is restricted to immediate child windows; grandchildren and deeper descendant windows are not searched.
      /// </summary>
      /// <param name="hwndParent">A handle to the window whose child is to be retrieved</param>
      /// <param name="ptParentClientCoords">A <see cref="T:Intermech.WindowsDll.Interop.POINT" /> structure that defines the client coordinates of the point to be checked.</param>
      /// <returns>The return value is a handle to the child window that contains the specified point.</returns>
      /// <remarks>
      /// <see cref="M:Intermech.WindowsDll.User32.RealChildWindowFromPoint(System.IntPtr,Intermech.WindowsDll.Interop.POINT)" /> treats <see cref="!:Interop.HitTestValues.HTTRANSPARENT" /> areas of a standard control differently from other areas of the control;
      /// it returns the child window behind a transparent part of a control.
      /// In contrast, <see cref="!:ChildWindowFromPoint" /> treats <see cref="!:Interop.HitTestValues.HTTRANSPARENT" /> areas of a control the same as other areas.
      /// For example, if the point is in a transparent area of a groupbox, <see cref="M:Intermech.WindowsDll.User32.RealChildWindowFromPoint(System.IntPtr,Intermech.WindowsDll.Interop.POINT)" /> returns the child window behind a groupbox,
      /// whereas <see cref="!:ChildWindowFromPoint" /> returns the groupbox. However, both APIs return a static field, even though it, too,
      /// returns <see cref="!:Interop.HitTestValues.HTTRANSPARENT" />.
      /// </remarks>
      [CanBeEmpty]
      [DllImport("user32.dll")]
      public static extern IntPtr RealChildWindowFromPoint(
        [NotEmpty] IntPtr hwndParent,
        Interop.POINT ptParentClientCoords);

      /// <summary>Retrieves information about the specified combo box</summary>
      /// <param name="hwndCombo">A handle to the combo box</param>
      /// <param name="info"><see cref="T:Intermech.WindowsDll.User32.ComboBoxInfo" /> structure that receives the information</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32")]
      public static extern bool GetComboBoxInfo([NotEmpty] IntPtr hwndCombo, [NotNull, In, Out] ComboBoxInfo info);

      /// <summary>Retrieves information about the specified combo box</summary>
      /// <param name="hwndCombo">A handle to the combo box</param>
      /// <returns><see cref="T:Intermech.WindowsDll.User32.ComboBoxInfo" /> structure that receives the information</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static ComboBoxInfo GetComboBoxInfo_ThrowWinErrors([NotEmpty] IntPtr hwndCombo)
      {
            ComboBoxInfo info = new ComboBoxInfo();
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        return User32.GetComboBoxInfo(hwndCombo, info) ? info : throw WindowsApiException.GetLastForce("User32::GetComboBoxInfo", (ArgumentDescriptor) @(typeof (IntPtr), (object) hwndCombo), (ArgumentDescriptor) @(Modifier.Ref, typeof (ComboBoxInfo)));
      }

      /// <summary>
      /// Retrieves the device context (DC) for the entire window, including title bar, menus, and scroll bars.
      /// A window device context permits painting anywhere in a window,
      /// because the origin of the device context is the upper-left corner of the window instead of the client area.
      /// </summary>
      /// <param name="hWnd">
      /// A handle to the window with a device context that is to be retrieved. If this value is NULL, GetWindowDC retrieves the device context for the entire screen.
      /// If this parameter is NULL, method retrieves the device context for the primary display monitor.
      /// To get the device context for other display monitors, use the <see cref="!:EnumDisplayMonitors" /> and <see cref="!:Gdi32.CreateDC" /> functions.
      /// </param>
      /// <returns>
      /// If the function succeeds, the return value is a handle to a device context for the specified window.
      /// If the function fails, the return value is NULL. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" />
      /// </returns>
      [CanBeEmpty]
      [DllImport("user32", SetLastError = true)]
      public static extern IntPtr GetWindowDC([CanBeEmpty] IntPtr hWnd);

      /// <summary>
      /// Retrieves the device context (DC) for the entire window, including title bar, menus, and scroll bars.
      /// A window device context permits painting anywhere in a window,
      /// because the origin of the device context is the upper-left corner of the window instead of the client area.
      /// </summary>
      /// <param name="hWnd">
      /// A handle to the window with a device context that is to be retrieved. If this value is NULL, GetWindowDC retrieves the device context for the entire screen.
      /// If this parameter is NULL, method retrieves the device context for the primary display monitor.
      /// To get the device context for other display monitors, use the <see cref="!:EnumDisplayMonitors" /> and <see cref="!:Gdi32.CreateDC" /> functions.
      /// </param>
      /// <returns>Handle to a device context for the specified window</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [NotEmpty]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IntPtr GetWindowDC_ThrowWinErrors([CanBeEmpty] IntPtr hWnd)
      {
        IntPtr windowDc = User32.GetWindowDC(hWnd);
        // ISSUE: explicit reference operation
        return !(windowDc == IntPtr.Zero) ? windowDc : throw WindowsApiException.GetLastForce("User32::GetWindowDC", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd));
      }

      /// <summary>
      /// Releases a device context (DC), freeing it for use by other applications. The effect of the ReleaseDC function depends on the type of DC.
      /// It frees only common and window DCs. It has no effect on class or private DCs.
      /// </summary>
      /// <param name="hWnd">A handle to the window whose DC is to be released</param>
      /// <param name="hDc">A handle to the DC to be released</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("user32")]
      public static extern bool ReleaseDC([NotEmpty] IntPtr hWnd, [NotEmpty] IntPtr hDc);

      /// <summary>
      /// Releases a device context (DC), freeing it for use by other applications. The effect of the ReleaseDC function depends on the type of DC.
      /// It frees only common and window DCs. It has no effect on class or private DCs.
      /// </summary>
      /// <param name="hWnd">A handle to the window whose DC is to be released</param>
      /// <param name="hDc">A handle to the DC to be released</param>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void ReleaseDC_ThrowWinErrors([NotEmpty] IntPtr hWnd, [NotEmpty] IntPtr hDc)
      {
        if (User32.ReleaseDC(hWnd, hDc))
          return;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        WindowsApiException.GetLastForce("User32::GetWindowDC", (ArgumentDescriptor) @(typeof (IntPtr), (object) hWnd), (ArgumentDescriptor) @(typeof (IntPtr), (object) hDc));
      }

      /// <summary>The MONITORINFOEX structure contains information about a display monitor. The GetMonitorInfo function stores information into a MONITORINFOEX structure or
      /// a MONITORINFO structure. The MONITORINFOEX structure is a superset of the MONITORINFO structure. The MONITORINFOEX structure adds a string member to
      /// contain a name for the display monitor</summary>
      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
      public class MonitorInfoEx
      {
        /// <summary>The size, in bytes, of the structure. Set this member to sizeof(MONITORINFOEX) (72) before calling the GetMonitorInfo function. Doing so lets the function
        /// determine the type of structure you are passing to it</summary>
        private readonly int Size = Marshal.SizeOf(typeof (MonitorInfoEx));
        /// <summary>A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates. Note that if the monitor is not the primary display
        /// monitor, some of the rectangle's coordinates may be negative values</summary>
        public Interop.RECT Monitor;
        /// <summary>A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications, expressed in virtual-screen coordinates.
        /// Windows uses this rectangle to maximize an application on the monitor. The rest of the area in rcMonitor contains system windows such as the task bar
        /// and side bars. Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values</summary>
        public Interop.RECT WorkArea;
        /// <summary>
        /// The attributes of the display monitor.
        /// 
        /// This member can be the following value:
        ///   1 : MONITORINFOF_PRIMARY
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int Flags;
        /// <summary>
        /// A string that specifies the device name of the monitor being used. Most applications have no use for a display monitor name,
        /// and so can save some bytes by using a MONITORINFO structure.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
        public string DeviceName = "";
      }

      /// <summary>Determines the function's return value if the window does not intersect any display monitor</summary>
      public enum DefaultMonitor
      {
        DefaultToNull,
        DefaultToPrimary,
        DefaultToNearest,
      }

      /// <summary>Device state flags</summary>
      [Flags]
      public enum DisplayDeviceStateFlags
      {
        /// <summary>The device is part of the desktop</summary>
        AttachedToDesktop = 1,
        MultiDriver = 2,
        /// <summary>The device is part of the desktop</summary>
        PrimaryDevice = 4,
        /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes</summary>
        MirroringDriver = 8,
        /// <summary>The device is VGA compatible</summary>
        VGACompatible = 16, // 0x00000010
        /// <summary>The device is removable; it cannot be the primary display</summary>
        Removable = 32, // 0x00000020
        /// <summary>The device has more display modes than its output devices support</summary>
        ModesPruned = 134217728, // 0x08000000
        /// <summary>The device is removable; it cannot be the primary display</summary>
        Remote = 67108864, // 0x04000000
        Disconnect = 33554432, // 0x02000000
      }

      /// <summary>Structure that receives information about the display device specified by the iDevNum parameter of the <see cref="!:EnumDisplayDevices" /> function</summary>
      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
      public class DISPLAY_DEVICE
      {
        /// <summary>The size of the structure, in bytes</summary>
        [MarshalAs(UnmanagedType.U4)]
        public readonly int Size = Marshal.SizeOf(typeof (DISPLAY_DEVICE));
        /// <summary>
        /// String that identifying the device name.
        /// This is either the adapter device or the monitor device.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
        public string DeviceName;
        /// <summary>
        /// String that containing the device context string.
        /// This is either a description of the display adapter or of the display monitor.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128 /*0x80*/)]
        public string DeviceString;
        /// <summary>Device state flags</summary>
        [MarshalAs(UnmanagedType.U4)]
        public DisplayDeviceStateFlags StateFlags;
        /// <summary>Not used</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128 /*0x80*/)]
        public string DeviceID;
        /// <summary>Reserved</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128 /*0x80*/)]
        public string DeviceKey;
      }

      /// <summary>
      /// Window Styles.
      /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
      /// </summary>
      [Flags]
      public enum WindowStyles : long
      {
        /// <summary>The window has a thin-line border</summary>
        WS_BORDER = 8388608, // 0x0000000000800000
        /// <summary>The window has a title bar (includes the WS_BORDER style)</summary>
        WS_CAPTION = 12582912, // 0x0000000000C00000
        /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style</summary>
        WS_CHILD = 1073741824, // 0x0000000040000000
        /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window</summary>
        WS_CLIPCHILDREN = 33554432, // 0x0000000002000000
        /// <summary>Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message,
        /// the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
        /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window,
        /// to draw within the client area of a neighboring child window</summary>
        WS_CLIPSIBLINGS = 67108864, // 0x0000000004000000
        /// <summary>The window is initially disabled. A disabled window cannot receive input from the user.
        /// To change this after a window has been created, use the EnableWindow function</summary>
        WS_DISABLED = 134217728, // 0x0000000008000000
        /// <summary>The window has a border of a style typically used with dialog boxes.
        /// A window with this style cannot have a title bar</summary>
        WS_DLGFRAME = 4194304, // 0x0000000000400000
        /// <summary>
        /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it,
        ///     up to the next control with the WS_GROUP style.
        /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group.
        /// The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
        /// </summary>
        WS_GROUP = 131072, // 0x0000000000020000
        /// <summary>The window has a horizontal scroll bar</summary>
        WS_HSCROLL = 1048576, // 0x0000000000100000
        /// <summary>The window is initially maximized</summary>
        WS_MAXIMIZE = 16777216, // 0x0000000001000000
        /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified</summary>
        WS_MAXIMIZEBOX = 65536, // 0x0000000000010000
        /// <summary>The window is initially minimized</summary>
        WS_MINIMIZE = 536870912, // 0x0000000020000000
        /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified</summary>
        WS_MINIMIZEBOX = WS_GROUP, // 0x0000000000020000
        /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border</summary>
        WS_OVERLAPPED = 0,
        /// <summary>The window is an overlapped window</summary>
        WS_OVERLAPPEDWINDOW = 13565952, // 0x0000000000CF0000
        /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style</summary>
        WS_POPUP = 2147483648, // 0x0000000080000000
        /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible</summary>
        WS_POPUPWINDOW = 2156396544, // 0x0000000080880000
        /// <summary>The window has a sizing border</summary>
        WS_SIZEFRAME = 262144, // 0x0000000000040000
        /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified</summary>
        WS_SYSMENU = 524288, // 0x0000000000080000
        /// <summary>
        /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
        /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
        /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
        /// </summary>
        WS_TABSTOP = WS_MAXIMIZEBOX, // 0x0000000000010000
        /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function</summary>
        WS_VISIBLE = 268435456, // 0x0000000010000000
        /// <summary>The window has a vertical scroll bar</summary>
        WS_VSCROLL = 2097152, // 0x0000000000200000
      }

      [Flags]
      public enum WindowStylesEx : long
      {
        /// <summary>Specifies that a window created with this style accepts drag-drop files</summary>
        WS_EX_ACCEPTFILES = 16, // 0x0000000000000010
        /// <summary>Forces a top-level window onto the taskbar when the window is visible</summary>
        WS_EX_APPWINDOW = 262144, // 0x0000000000040000
        /// <summary>Specifies that a window has a border with a sunken edge</summary>
        WS_EX_CLIENTEDGE = 512, // 0x0000000000000200
        /// <summary>
        /// Windows XP: Paints all descendants of a window in bottom-to-top painting order using double-buffering.
        /// This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        /// </summary>
        WS_EX_COMPOSITED = 33554432, // 0x0000000002000000
        /// <summary>
        /// Includes a question mark in the title bar of the window. When the user clicks the question mark, the cursor changes to a question mark with a pointer.
        /// If the user then clicks a child window, the child receives a WM_HELP message.
        /// The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
        /// The Help application displays a pop-up window that typically contains help for the child window.
        /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
        /// </summary>
        WS_EX_CONTEXTHELP = 1024, // 0x0000000000000400
        /// <summary>
        /// The window itself contains child windows that should take part in dialog box navigation.
        /// If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key,
        ///     an arrow key, or a keyboard mnemonic.
        /// </summary>
        WS_EX_CONTROLPARENT = 65536, // 0x0000000000010000
        /// <summary>
        /// Creates a window that has a double border;
        /// the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
        /// </summary>
        WS_EX_DLGMODALFRAME = 1,
        /// <summary>
        /// Windows 2000/XP: Creates a layered window.
        /// Note that this cannot be used for child windows.
        /// Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        /// </summary>
        WS_EX_LAYERED = 524288, // 0x0000000000080000
        /// <summary>
        /// Arabic and Hebrew versions of Windows 98/Me, Windows 2000/XP: Creates a window whose horizontal origin is on the right edge.
        /// Increasing horizontal values advance to the left.
        /// </summary>
        WS_EX_LAYOUTRTL = 4194304, // 0x0000000000400000
        /// <summary>Creates a window that has generic left-aligned properties. This is the default</summary>
        WS_EX_LEFT = 0,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment,
        ///     the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
        /// </summary>
        WS_EX_LEFTSCROLLBAR = 16384, // 0x0000000000004000
        /// <summary> The window text is displayed using left-to-right reading-order properties. This is the default</summary>
        WS_EX_LTRREADING = 0,
        /// <summary>Creates a multiple-document interface (MDI) child window</summary>
        WS_EX_MDICHILD = 64, // 0x0000000000000040
        /// <summary>
        /// Windows 2000/XP: A top-level window created with this style does not become the foreground window when the user clicks it.
        /// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
        /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
        /// </summary>
        WS_EX_NOACTIVATE = 134217728, // 0x0000000008000000
        /// <summary>Windows 2000/XP: A window created with this style does not pass its window layout to its child windows</summary>
        WS_EX_NOINHERITLAYOUT = 1048576, // 0x0000000000100000
        /// <summary>
        /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
        /// </summary>
        WS_EX_NOPARENTNOTIFY = 4,
        /// <summary> Combines the WS_EX_CLIENTEDGE and WS_EX_WINDOWEDGE styles</summary>
        WS_EX_OVERLAPPEDWINDOW = 768, // 0x0000000000000300
        /// <summary>Combines the WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW, and WS_EX_TOPMOST styles</summary>
        WS_EX_PALETTEWINDOW = 392, // 0x0000000000000188
        /// <summary>
        /// The window has generic "right-aligned" properties.
        /// This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment;
        /// otherwise, the style is ignored.
        /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively.
        /// Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
        /// </summary>
        WS_EX_RIGHT = 4096, // 0x0000000000001000
        /// <summary>Vertical scroll bar (if present) is to the right of the client area. This is the default</summary>
        WS_EX_RIGHTSCROLLBAR = 0,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment,
        ///     the window text is displayed using right-to-left reading-order properties.
        /// For other languages, the style is ignored.
        /// </summary>
        WS_EX_RTLREADING = 8192, // 0x0000000000002000
        /// <summary>
        /// Creates a window with a three-dimensional border style intended to be used for items that do not accept user input.
        /// </summary>
        WS_EX_STATICEDGE = 131072, // 0x0000000000020000
        /// <summary>
        /// Creates a tool window; that is, a window intended to be used as a floating toolbar.
        /// A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
        /// A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
        /// If a tool window has a system menu, its icon is not displayed on the title bar.
        /// However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        /// </summary>
        WS_EX_TOOLWINDOW = 128, // 0x0000000000000080
        /// <summary>
        /// Specifies that a window created with this style should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
        /// To add or remove this style, use the SetWindowPos function.
        /// </summary>
        WS_EX_TOPMOST = 8,
        /// <summary>
        /// Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        /// The window appears transparent because the bits of underlying sibling windows have already been painted.
        /// To achieve transparency without these restrictions, use the SetWindowRgn function.
        /// </summary>
        WS_EX_TRANSPARENT = 32, // 0x0000000000000020
        /// <summary>Specifies that a window has a border with a raised edge</summary>
        WS_EX_WINDOWEDGE = 256, // 0x0000000000000100
      }

      /// <summary>A windowinfo</summary>
      [StructLayout(LayoutKind.Sequential)]
      public class WINDOWINFO
      {
        /// <summary>The size of the structure, in bytes</summary>
        [MarshalAs(UnmanagedType.U4)]
        public readonly int Size = Marshal.SizeOf(typeof (WINDOWINFO));
        /// <summary>The coordinates of the window</summary>
        public Interop.RECT rcWindow;
        /// <summary>The coordinates of the client area</summary>
        public Interop.RECT rcClient;
        /// <summary>The window styles</summary>
        [MarshalAs(UnmanagedType.U4)]
        public WindowStyles Style;
        /// <summary>The extended window styles</summary>
        [MarshalAs(UnmanagedType.U4)]
        public WindowStylesEx ExStyle;
        /// <summary>The window status. If this member is <see cref="F:Intermech.WindowsDll.User32.WS_ACTIVECAPTION" /> (0x0001), the window is active. Otherwise, this member is zero</summary>
        [MarshalAs(UnmanagedType.U4)]
        public int dwWindowStatus;
        /// <summary>The width of the window border, in pixels</summary>
        [MarshalAs(UnmanagedType.U4)]
        public int cxWindowBorders;
        /// <summary>The height of the window border, in pixels</summary>
        public int cyWindowBorders;
        /// <summary>The window class atom (see RegisterClass)</summary>
        public short atomWindowType;
        /// <summary>The Windows version of the application that created the window</summary>
        public short CreatorVersion;
      }

      /// <summary>The flags that control the position of the minimized window and the method by which the window is restored</summary>
      [Flags]
      public enum WindowPlacementFlags
      {
        /// <summary>
        /// The coordinates of the minimized window may be specified.
        /// This flag must be specified if the coordinates are set in the ptMinPosition member.
        /// </summary>
        WPF_SETMINPOSITION = 1,
        /// <summary>
        /// The restored window will be maximized, regardless of whether it was maximized before it was minimized.
        /// This setting is only valid the next time the window is restored.
        /// It does not change the default restoration behavior.
        /// </summary>
        /// <remarks>This flag is only valid when the SW_SHOWMINIMIZED value is specified for the showCmd member.</remarks>
        WPF_RESTORETOMAXIMIZED = 2,
        /// <summary>
        /// If the calling thread and the thread that owns the window are attached to different input queues,
        ///     the system posts the request to the thread that owns the window.
        /// This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        WPF_ASYNCWINDOWPLACEMENT = 4,
      }

      /// <summary>The current show state of the window, or state to be set</summary>
      public enum ShowWindowCommand
      {
        /// <summary>Hides the window and activates another window</summary>
        SW_HIDE = 0,
        /// <summary>
        /// Activates and displays a window.
        /// If the window is minimized or maximized, the system restores it to its original size and position.
        /// An application should specify this flag when displaying the window for the first time.
        /// </summary>
        SW_NORMAL = 1,
        /// <summary>Activates the window and displays it as a minimized window</summary>
        SW_SHOWMINIMIZED = 2,
        /// <summary>Maximizes the specified window</summary>
        SW_MAXIMIZE = 3,
        /// <summary>Activates the window and displays it as a maximized window</summary>
        SW_SHOWMAXIMIZED = 3,
        /// <summary>
        /// Displays a window in its most recent size and position.
        /// This value is similar to <see cref="!:SW_SHOWNORMAL" />, except the window is not activated.
        /// </summary>
        SW_SHOWNOACTIVATE = 4,
        /// <summary>Activates the window and displays it in its current size and position</summary>
        SW_SHOW = 5,
        /// <summary>Minimizes the specified window and activates the next top-level window in the z-order</summary>
        SW_MINIMIZE = 6,
        /// <summary>
        /// Displays the window as a minimized window.
        /// This value is similar to <see cref="F:Intermech.WindowsDll.User32.ShowWindowCommand.SW_SHOWMINIMIZED" />, except the window is not activated.
        /// </summary>
        SW_SHOWMINNOACTIVE = 7,
        /// <summary>
        /// Displays the window in its current size and position.
        /// This value is similar to <see cref="F:Intermech.WindowsDll.User32.ShowWindowCommand.SW_SHOW" />, except the window is not activated.
        /// </summary>
        SW_SHOWNA = 8,
        /// <summary>
        /// Activates and displays the window.
        /// If the window is minimized or maximized, the system restores it to its original size and position.
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        SW_RESTORE = 9,
        /// <summary>
        /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
        /// </summary>
        SW_SHOWDEFAULT = 10, // 0x0000000A
        /// <summary>
        /// Minimizes a window, even if the thread that owns the window is not responding.
        /// This flag should only be used when minimizing windows from a different thread.
        /// </summary>
        SW_FORCEMINIMIZE = 11, // 0x0000000B
      }

      [StructLayout(LayoutKind.Sequential)]
      public class WINDOWPLACEMENT
      {
        /// <summary>The size of the structure, in bytes</summary>
        [MarshalAs(UnmanagedType.U4)]
        private readonly int Size = Marshal.SizeOf(typeof (WINDOWPLACEMENT));
        /// <summary>The flags that control the position of the minimized window and the method by which the window is restored</summary>
        [MarshalAs(UnmanagedType.U4)]
        public WindowPlacementFlags Flags;
        /// <summary>The current show state of the window</summary>
        [MarshalAs(UnmanagedType.U4)]
        public ShowWindowCommand ShowCmd;
        /// <summary>The coordinates of the window's upper-left corner when the window is minimized</summary>
        public Interop.POINT ptMinPosition;
        /// <summary>The coordinates of the window's upper-left corner when the window is maximized</summary>
        public Interop.POINT ptMaxPosition;
        /// <summary>The window's coordinates when the window is in the restored position</summary>
        public Interop.RECT rcNormalPosition;
      }

      /// <summary> Flags used with the Windows API <see cref="M:Intermech.WindowsDll.User32.GetSystemMetrics(Intermech.WindowsDll.User32.SystemMetric)" />.</summary>
      public enum SystemMetric
      {
        /// <summary>
        /// The width of the screen of the primary display monitor, in pixels. This is the same value obtained by calling
        /// GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, HORZRES).
        /// </summary>
        CXSCREEN = 0,
        /// <summary>
        /// The height of the screen of the primary display monitor, in pixels. This is the same value obtained by calling
        /// <see cref="!:GetDeviceCaps" /> as follows: GetDeviceCaps( hdcPrimaryMonitor, VERTRES).
        /// </summary>
        CYSCREEN = 1,
        /// <summary>The width of a vertical scroll bar, in pixels.</summary>
        CXVSCROLL = 2,
        /// <summary>The height of a horizontal scroll bar, in pixels.</summary>
        CYHSCROLL = 3,
        /// <summary>The height of a caption area, in pixels.</summary>
        CYCAPTION = 4,
        /// <summary>
        /// The width of a window border, in pixels. This is equivalent to the <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXEDGE" /> value for windows with the 3-D look.
        /// </summary>
        CXBORDER = 5,
        /// <summary>The height of a window border, in pixels. This is equivalent to the CYEDGE value for windows with the 3-D look.</summary>
        CYBORDER = 6,
        /// <summary> This value is the same as <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXFIXEDFRAME" />. </summary>
        CXDLGFRAME = 7,
        /// <summary>
        /// The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
        /// <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXFIXEDFRAME" /> is the height of the horizontal border, and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYFIXEDFRAME" /> is the width of the vertical border.
        /// This value is the same as <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXDLGFRAME" />.
        /// </summary>
        CXFIXEDFRAME = 7,
        /// <summary>This value is the same as <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYFIXEDFRAME" />.</summary>
        CYDLGFRAME = 8,
        /// <summary>
        /// The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
        /// <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXFIXEDFRAME" /> is the height of the horizontal border, and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYFIXEDFRAME" /> is the width of the vertical border.
        /// This value is the same as <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYDLGFRAME" />.
        /// </summary>
        CYFIXEDFRAME = 8,
        /// <summary>The height of the thumb box in a vertical scroll bar, in pixels.</summary>
        CYVTHUMB = 9,
        /// <summary>The width of the thumb box in a horizontal scroll bar, in pixels.</summary>
        CXHTHUMB = 10, // 0x0000000A
        /// <summary>
        /// The default width of an icon, in pixels. The LoadIcon function can load only icons with the dimensions
        /// that <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXICON" /> and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYICON" /> specifies.
        /// </summary>
        CXICON = 11, // 0x0000000B
        /// <summary>
        /// The default height of an icon, in pixels. The LoadIcon function can load only icons with the dimensions <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXICON" /> and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYICON" />.
        /// </summary>
        CYICON = 12, // 0x0000000C
        /// <summary>The width of a cursor, in pixels. The system cannot create cursors of other sizes.</summary>
        CXCURSOR = 13, // 0x0000000D
        /// <summary>The height of a cursor, in pixels. The system cannot create cursors of other sizes.</summary>
        CYCURSOR = 14, // 0x0000000E
        /// <summary>The height of a single-line menu bar, in pixels.</summary>
        CYMENU = 15, // 0x0000000F
        /// <summary>
        /// The width of the client area for a full-screen window on the primary display monitor, in pixels.
        /// To get the coordinates of the portion of the screen that is not obscured by the system taskbar or by application desktop toolbars,
        /// call the <see cref="!:SystemParametersInfo" /> function with the <see cref="!:SPI_GETWORKAREA" /> value.
        /// </summary>
        CXFULLSCREEN = 16, // 0x00000010
        /// <summary>
        /// The height of the client area for a full-screen window on the primary display monitor, in pixels.
        /// To get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop toolbars,
        /// call the <see cref="!:SystemParametersInfo" /> function with the <see cref="!:SPI_GETWORKAREA" /> value.
        /// </summary>
        CYFULLSCREEN = 17, // 0x00000011
        /// <summary>
        /// For double byte character set versions of the system, this is the height of the Kanji window at the bottom of the screen, in pixels.
        /// </summary>
        CYKANJIWINDOW = 18, // 0x00000012
        /// <summary>
        /// Nonzero if a mouse is installed; otherwise, 0. This value is rarely zero, because of support for virtual mice and because
        /// some systems detect the presence of the port instead of the presence of a mouse.
        /// </summary>
        MOUSEPRESENT = 19, // 0x00000013
        /// <summary>The height of the arrow bitmap on a vertical scroll bar, in pixels.</summary>
        CYVSCROLL = 20, // 0x00000014
        /// <summary>The width of the arrow bitmap on a horizontal scroll bar, in pixels.</summary>
        CXHSCROLL = 21, // 0x00000015
        /// <summary>Nonzero if the debug version of User.exe is installed; otherwise, 0.</summary>
        DEBUG = 22, // 0x00000016
        /// <summary>Nonzero if the meanings of the left and right mouse buttons are swapped; otherwise, 0.</summary>
        SWAPBUTTON = 23, // 0x00000017
        /// <summary>The minimum width of a window, in pixels.</summary>
        CXMIN = 28, // 0x0000001C
        /// <summary>The minimum height of a window, in pixels.</summary>
        CYMIN = 29, // 0x0000001D
        /// <summary>The width of a button in a window caption or title bar, in pixels.</summary>
        CXSIZE = 30, // 0x0000001E
        /// <summary>The height of a button in a window caption or title bar, in pixels.</summary>
        CYSIZE = 31, // 0x0000001F
        /// <summary>This value is the same as <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXSIZEFRAME" />.</summary>
        CXFRAME = 32, // 0x00000020
        /// <summary>
        /// The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
        /// CXSIZEFRAME is the width of the horizontal border, and CYSIZEFRAME is the height of the vertical border.
        /// This value is the same as CXFRAME.
        /// </summary>
        CXSIZEFRAME = 32, // 0x00000020
        /// <summary>This value is the same as <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYSIZEFRAME" />.</summary>
        CYFRAME = 33, // 0x00000021
        /// <summary>
        /// The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
        /// <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXSIZEFRAME" /> is the width of the horizontal border, and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYSIZEFRAME" /> is the height of the vertical border.
        /// This value is the same as <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYFRAME" />.
        /// </summary>
        CYSIZEFRAME = 33, // 0x00000021
        /// <summary>
        /// The minimum tracking width of a window, in pixels. The user cannot drag the window frame to a size smaller than these dimensions.
        /// A window can override this value by processing the WM_GETMINMAXINFO message.
        /// </summary>
        CXMINTRACK = 34, // 0x00000022
        /// <summary>
        /// The minimum tracking height of a window, in pixels. The user cannot drag the window frame to a size smaller than these dimensions.
        /// A window can override this value by processing the <see cref="!:WM_GETMINMAXINFO" /> message.
        /// </summary>
        CYMINTRACK = 35, // 0x00000023
        /// <summary>
        /// The width of the rectangle around the location of a first click in a double-click sequence, in pixels.
        /// The second click must occur within the rectangle that is defined by <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXDOUBLECLK" /> and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYDOUBLECLK" /> for the system
        /// to consider the two clicks a double-click. The two clicks must also occur within a specified time.
        /// To set the width of the double-click rectangle, call <see cref="!:SystemParametersInfo" /> with <see cref="!:SPI_SETDOUBLECLKWIDTH" />.
        /// </summary>
        CXDOUBLECLK = 36, // 0x00000024
        /// <summary>
        /// The height of the rectangle around the location of a first click in a double-click sequence, in pixels.
        /// The second click must occur within the rectangle defined by <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXDOUBLECLK" /> and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYDOUBLECLK" /> for the system to consider
        /// the two clicks a double-click. The two clicks must also occur within a specified time. To set the height of the double-click
        /// rectangle, call <see cref="!:SystemParametersInfo" /> with <see cref="!:SPI_SETDOUBLECLKHEIGHT" />.
        /// </summary>
        CYDOUBLECLK = 37, // 0x00000025
        /// <summary>
        /// The width of a grid cell for items in large icon view, in pixels. Each item fits into a rectangle of size
        /// <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXICONSPACING" /> by <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYICONSPACING" /> when arranged.
        /// This value is always greater than or equal to <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXICON" />.
        /// </summary>
        CXICONSPACING = 38, // 0x00000026
        /// <summary>
        /// The height of a grid cell for items in large icon view, in pixels. Each item fits into a rectangle of size
        /// <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXICONSPACING" /> by <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYICONSPACING" /> when arranged. This value is always greater than or equal to <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYICON" />.
        /// </summary>
        CYICONSPACING = 39, // 0x00000027
        /// <summary>
        /// Nonzero if drop-down menus are right-aligned with the corresponding menu-bar item; 0 if the menus are left-aligned.
        /// </summary>
        MENUDROPALIGNMENT = 40, // 0x00000028
        /// <summary>Nonzero if the Microsoft Windows for Pen computing extensions are installed; zero otherwise.</summary>
        PENWINDOWS = 41, // 0x00000029
        /// <summary>Nonzero if User32.dll supports DBCS; otherwise, 0.</summary>
        DBCSENABLED = 42, // 0x0000002A
        /// <summary>The number of buttons on a mouse, or zero if no mouse is installed. </summary>
        CMOUSEBUTTONS = 43, // 0x0000002B
        /// <summary>This system metric should be ignored; it always returns 0.</summary>
        SECURE = 44, // 0x0000002C
        /// <summary>The width of a 3-D border, in pixels. This metric is the 3-D counterpart of <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXBORDER" />.</summary>
        CXEDGE = 45, // 0x0000002D
        /// <summary>The height of a 3-D border, in pixels. This is the 3-D counterpart of <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYBORDER" />.</summary>
        CYEDGE = 46, // 0x0000002E
        /// <summary>
        /// The width of a grid cell for a minimized window, in pixels. Each minimized window fits into a rectangle this size when arranged.
        /// This value is always greater than or equal to CXMINIMIZED.
        /// </summary>
        CXMINSPACING = 47, // 0x0000002F
        /// <summary>
        /// The height of a grid cell for a minimized window, in pixels. Each minimized window fits into a rectangle this size when arranged.
        /// This value is always greater than or equal to <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYMINIMIZED" />.
        /// </summary>
        CYMINSPACING = 48, // 0x00000030
        /// <summary>The recommended width of a small icon, in pixels. Small icons typically appear in window captions and in small icon view.</summary>
        CXSMICON = 49, // 0x00000031
        /// <summary>
        /// The recommended height of a small icon, in pixels. Small icons typically appear in window captions and in small icon view.
        /// </summary>
        CYSMICON = 50, // 0x00000032
        /// <summary>The height of a small caption, in pixels.</summary>
        CYSMCAPTION = 51, // 0x00000033
        /// <summary>The width of small caption buttons, in pixels.</summary>
        CXSMSIZE = 52, // 0x00000034
        /// <summary>The height of small caption buttons, in pixels.</summary>
        CYSMSIZE = 53, // 0x00000035
        /// <summary>
        /// The width of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
        /// </summary>
        CXMENUSIZE = 54, // 0x00000036
        /// <summary>
        /// The height of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
        /// </summary>
        CYMENUSIZE = 55, // 0x00000037
        /// <summary>The flags that specify how the system arranged minimized windows.</summary>
        ARRANGE = 56, // 0x00000038
        /// <summary>The width of a minimized window, in pixels.</summary>
        CXMINIMIZED = 57, // 0x00000039
        /// <summary>The height of a minimized window, in pixels.</summary>
        CYMINIMIZED = 58, // 0x0000003A
        /// <summary>
        /// The default maximum width of a window that has a caption and sizing borders, in pixels.
        /// This metric refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions.
        /// A window can override this value by processing the <see cref="!:WM_GETMINMAXINFO" /> message.
        /// </summary>
        CXMAXTRACK = 59, // 0x0000003B
        /// <summary>
        /// The default maximum height of a window that has a caption and sizing borders, in pixels. This metric refers to the entire desktop.
        /// The user cannot drag the window frame to a size larger than these dimensions. A window can override this value by processing
        /// the <see cref="!:WM_GETMINMAXINFO" /> message.
        /// </summary>
        CYMAXTRACK = 60, // 0x0000003C
        /// <summary>The default width, in pixels, of a maximized top-level window on the primary display monitor.</summary>
        CXMAXIMIZED = 61, // 0x0000003D
        /// <summary>The default height, in pixels, of a maximized top-level window on the primary display monitor.</summary>
        CYMAXIMIZED = 62, // 0x0000003E
        /// <summary>
        /// The least significant bit is set if a network is present; otherwise, it is cleared. The other bits are reserved for future use.
        /// </summary>
        NETWORK = 63, // 0x0000003F
        /// <summary>
        /// The value that specifies how the system is started:
        /// 0 Normal boot
        /// 1 Fail-safe boot
        /// 2 Fail-safe with network boot
        /// A fail-safe boot (also called SafeBoot, Safe Mode, or Clean Boot) bypasses the user startup files.
        /// </summary>
        CLEANBOOT = 67, // 0x00000043
        /// <summary>
        /// The number of pixels on either side of a mouse-down point that the mouse pointer can move before a drag operation begins.
        /// This allows the user to click and release the mouse button easily without unintentionally starting a drag operation.
        /// If this value is negative, it is subtracted from the left of the mouse-down point and added to the right of it.
        /// </summary>
        CXDRAG = 68, // 0x00000044
        /// <summary>
        /// The number of pixels above and below a mouse-down point that the mouse pointer can move before a drag operation begins.
        /// This allows the user to click and release the mouse button easily without unintentionally starting a drag operation.
        /// If this value is negative, it is subtracted from above the mouse-down point and added below it.
        /// </summary>
        CYDRAG = 69, // 0x00000045
        /// <summary>
        /// Nonzero if the user requires an application to present information visually in situations where it would otherwise present
        /// the information only in audible form; otherwise, 0.
        /// </summary>
        SHOWSOUNDS = 70, // 0x00000046
        /// <summary>The width of the default menu check-mark bitmap, in pixels.</summary>
        CXMENUCHECK = 71, // 0x00000047
        /// <summary>The height of the default menu check-mark bitmap, in pixels.</summary>
        CYMENUCHECK = 72, // 0x00000048
        /// <summary>Nonzero if the computer has a low-end (slow) processor; otherwise, 0.</summary>
        SLOWMACHINE = 73, // 0x00000049
        /// <summary>Nonzero if the system is enabled for Hebrew and Arabic languages, 0 if not.</summary>
        MIDEASTENABLED = 74, // 0x0000004A
        /// <summary>Nonzero if a mouse with a vertical scroll wheel is installed; otherwise 0.</summary>
        MOUSEWHEELPRESENT = 75, // 0x0000004B
        /// <summary>
        /// The coordinates for the left side of the virtual screen. The virtual screen is the bounding rectangle of all display monitors.
        /// The <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXVIRTUALSCREEN" /> metric is the width of the virtual screen.
        /// </summary>
        XVIRTUALSCREEN = 76, // 0x0000004C
        /// <summary>
        /// The coordinates for the top of the virtual screen. The virtual screen is the bounding rectangle of all display monitors.
        /// The <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYVIRTUALSCREEN" /> metric is the height of the virtual screen.
        /// </summary>
        YVIRTUALSCREEN = 77, // 0x0000004D
        /// <summary>
        /// The width of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all display monitors.
        /// The <see cref="F:Intermech.WindowsDll.User32.SystemMetric.XVIRTUALSCREEN" /> metric is the coordinates for the left side of the virtual screen.
        /// </summary>
        CXVIRTUALSCREEN = 78, // 0x0000004E
        /// <summary>
        /// The height of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all display monitors.
        /// The <see cref="F:Intermech.WindowsDll.User32.SystemMetric.YVIRTUALSCREEN" /> metric is the coordinates for the top of the virtual screen.
        /// </summary>
        CYVIRTUALSCREEN = 79, // 0x0000004F
        /// <summary>The number of display monitors on a desktop.</summary>
        CMONITORS = 80, // 0x00000050
        /// <summary>
        /// Nonzero if all the display monitors have the same color format, otherwise, 0. Two displays can have the same bit depth,
        /// but different color formats. For example, the red, green, and blue pixels can be encoded with different numbers of bits,
        /// or those bits can be located in different places in a pixel color value.
        /// </summary>
        SAMEDISPLAYFORMAT = 81, // 0x00000051
        /// <summary>
        /// Nonzero if Input Method Manager/Input Method Editor features are enabled; otherwise, 0.
        /// <see cref="F:Intermech.WindowsDll.User32.SystemMetric.IMMENABLED" /> indicates whether the system is ready to use a Unicode-based IME on a Unicode application.
        /// To ensure that a language-dependent IME works, check <see cref="F:Intermech.WindowsDll.User32.SystemMetric.DBCSENABLED" /> and the system ANSI code page.
        /// Otherwise the ANSI-to-Unicode conversion may not be performed correctly, or some components like fonts
        /// or registry settings may not be present.
        /// </summary>
        IMMENABLED = 82, // 0x00000052
        /// <summary>
        /// The width of the left and right edges of the focus rectangle that the DrawFocusRectdraws.
        /// This value is in pixels.
        /// Windows 2000:  This value is not supported.
        /// </summary>
        CXFOCUSBORDER = 83, // 0x00000053
        /// <summary>
        /// The height of the top and bottom edges of the focus rectangle drawn byDrawFocusRect.
        /// This value is in pixels.
        /// Windows 2000:  This value is not supported.
        /// </summary>
        CYFOCUSBORDER = 84, // 0x00000054
        /// <summary>
        /// Nonzero if the current operating system is the Windows XP Tablet PC edition or if the current operating system is Windows Vista
        /// or Windows 7 and the Tablet PC Input service is started; otherwise, 0. The <see cref="F:Intermech.WindowsDll.User32.SystemMetric.DIGITIZER" /> setting indicates the type of digitizer
        /// input supported by a device running Windows 7 or Windows Server 2008 R2.
        /// </summary>
        TABLETPC = 86, // 0x00000056
        /// <summary>Nonzero if the current operating system is the Windows XP, Media Center Edition, 0 if not.</summary>
        MEDIACENTER = 87, // 0x00000057
        /// <summary>
        /// Nonzero if the current operating system is Windows 7 Starter Edition, Windows Vista Starter, or Windows XP Starter Edition; otherwise, 0.
        /// </summary>
        STARTER = 88, // 0x00000058
        /// <summary>The build number if the system is Windows Server 2003 R2; otherwise, 0.</summary>
        SERVERR2 = 89, // 0x00000059
        /// <summary>Nonzero if a mouse with a horizontal scroll wheel is installed; otherwise 0.</summary>
        MOUSEHORIZONTALWHEELPRESENT = 91, // 0x0000005B
        /// <summary>
        /// The amount of border padding for captioned windows, in pixels.
        /// Windows XP/2000:  This value is not supported.
        /// </summary>
        CXPADDEDBORDER = 92, // 0x0000005C
        /// <summary>
        /// Nonzero if the current operating system is Windows 7 or Windows Server 2008 R2 and the Tablet PC Input
        /// service is started; otherwise, 0. The return value is a bitmask that specifies the type of digitizer input supported by the device.
        /// Windows Server 2008, Windows Vista, and Windows XP/2000:  This value is not supported.
        /// </summary>
        DIGITIZER = 94, // 0x0000005E
        /// <summary>
        /// Nonzero if there are digitizers in the system; otherwise, 0. <see cref="F:Intermech.WindowsDll.User32.SystemMetric.MAXIMUMTOUCHES" /> returns the aggregate maximum of the
        /// maximum number of contacts supported by every digitizer in the system. If the system has only single-touch digitizers,
        /// the return value is 1. If the system has multi-touch digitizers, the return value is the number of simultaneous contacts
        /// the hardware can provide.
        /// Windows Server 2008, Windows Vista, and Windows XP/2000:  This value is not supported.
        /// </summary>
        MAXIMUMTOUCHES = 95, // 0x0000005F
        /// <summary>
        /// This system metric is used in a Terminal Services environment. If the calling process is associated with a Terminal Services
        /// client session, the return value is nonzero. If the calling process is associated with the Terminal Services console session,
        /// the return value is 0.
        /// Windows Server 2003 and Windows XP:  The console session is not necessarily the physical console.
        /// For more information, seeWTSGetActiveConsoleSessionId.
        /// </summary>
        REMOTESESSION = 4096, // 0x00001000
        /// <summary>
        /// Nonzero if the current session is shutting down; otherwise, 0.
        /// Windows 2000:  This value is not supported.
        /// </summary>
        SHUTTINGDOWN = 8192, // 0x00002000
        /// <summary>
        /// This system metric is used in a Terminal Services environment to determine if the current Terminal Server session is
        /// being remotely controlled. Its value is nonzero if the current session is remotely controlled; otherwise, 0.
        /// You can use terminal services management tools such as Terminal Services Manager (tsadmin.msc) and shadow.exe to
        /// control a remote session. When a session is being remotely controlled, another user can view the contents of that session
        /// and potentially interact with it.
        /// </summary>
        REMOTECONTROL = 8193, // 0x00002001
      }

      public enum ComboBoxButtonState
      {
        STATE_SYSTEM_NONE = 0,
        STATE_SYSTEM_PRESSED = 8,
        STATE_SYSTEM_INVISIBLE = 32768, // 0x00008000
      }

      [StructLayout(LayoutKind.Sequential)]
      public class ComboBoxInfo
      {
        public readonly int cbSize = Marshal.SizeOf(typeof (ComboBoxInfo));
        public Interop.RECT rcItem;
        public Interop.RECT rcButton;
        public ComboBoxButtonState stateButton;
        public IntPtr hwndCombo;
        public IntPtr hwndItem;
        public IntPtr hwndList;

        public Size ButtonSize
        {
          [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.rcButton.Size;
        }
      }

      /// <summary>Method, that receives the window handles associated with a thread</summary>
      public delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
    }
}
