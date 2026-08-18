
// Type: Intermech.Controls.OleContainer.UnsafeMethods
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;


namespace Intermech.Controls.OleContainer;

[SuppressUnmanagedCodeSecurity]
public class UnsafeMethods
{
  public const int GHND = 66;
  public const int GMEM_DDESHARE = 8192 /*0x2000*/;
  public const int GMEM_DISCARDABLE = 256 /*0x0100*/;
  public const int GMEM_FIXED = 0;
  public const int GMEM_INVALID_HANDLE = 32768 /*0x8000*/;
  public const int GMEM_LOWER = 4096 /*0x1000*/;
  public const int GMEM_MODIFY = 128 /*0x80*/;
  public const int GMEM_MOVEABLE = 2;
  public const int GMEM_NOCOMPACT = 16 /*0x10*/;
  public const int GMEM_NODISCARD = 32 /*0x20*/;
  public const int GMEM_NOT_BANKED = 4096 /*0x1000*/;
  public const int GMEM_NOTIFY = 16384 /*0x4000*/;
  public const int GMEM_SHARE = 8192 /*0x2000*/;
  public const int GMEM_VALID_FLAGS = 32626;
  public const int GMEM_ZEROINIT = 64 /*0x40*/;
  public const int GPTR = 64 /*0x40*/;
  public const int LAYOUT_BITMAPORIENTATIONPRESERVED = 8;
  public const int LAYOUT_RTL = 1;
  public const int MB_PRECOMPOSED = 1;
  public const int URLACTION_CHANNEL_SOFTDIST_PERMISSIONS = 7685;
  public const int URLACTION_CREDENTIALS_USE = 6656;
  public const int URLACTION_HTML_FONT_DOWNLOAD = 5636;
  public const int URLACTION_JAVA_PERMISSIONS = 7168;
  public const int URLPOLICY_ALLOW = 0;
  public const int URLPOLICY_CHANNEL_SOFTDIST_PROHIBIT = 65536 /*0x010000*/;
  public const int URLPOLICY_CREDENTIALS_MUST_PROMPT_USER = 65536 /*0x010000*/;
  public const int URLPOLICY_DISALLOW = 3;
  public const int URLPOLICY_JAVA_PROHIBIT = 0;
  public const int URLPOLICY_QUERY = 1;

  [DllImport("user32.dll", EntryPoint = "ChildWindowFromPoint", CharSet = CharSet.Auto)]
  private static extern IntPtr _ChildWindowFromPoint(HandleRef hwndParent, Intermech.Controls.OleContainer.POINTSTRUCT pt);

  [DllImport("user32.dll", EntryPoint = "ChildWindowFromPointEx", CharSet = CharSet.Auto)]
  private static extern IntPtr _ChildWindowFromPointEx(
    HandleRef hwndParent,
    Intermech.Controls.OleContainer.POINTSTRUCT pt,
    int uFlags);

  [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto)]
  private static extern IntPtr _WindowFromPoint(Intermech.Controls.OleContainer.POINTSTRUCT pt);

  public static IntPtr BeginPaint(HandleRef hWnd, [MarshalAs(UnmanagedType.LPStruct), In, Out] ref PAINTSTRUCT lpPaint)
  {
    return HandleCollector.Add(UnsafeMethods.IntBeginPaint(hWnd, ref lpPaint), HelperMethods.CommonHandles.HDC);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr CallNextHookEx(
    HandleRef hhook,
    int code,
    IntPtr wparam,
    IntPtr lparam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr CallWindowProc(
    IntPtr wndProc,
    IntPtr hWnd,
    int msg,
    IntPtr wParam,
    IntPtr lParam);

  public static IntPtr ChildWindowFromPoint(HandleRef hwndParent, int x, int y)
  {
    Intermech.Controls.OleContainer.POINTSTRUCT pt = new Intermech.Controls.OleContainer.POINTSTRUCT(x, y);
    return UnsafeMethods._ChildWindowFromPoint(hwndParent, pt);
  }

  public static IntPtr ChildWindowFromPointEx(HandleRef hwndParent, int x, int y, int uFlags)
  {
    Intermech.Controls.OleContainer.POINTSTRUCT pt = new Intermech.Controls.OleContainer.POINTSTRUCT(x, y);
    return UnsafeMethods._ChildWindowFromPointEx(hwndParent, pt, uFlags);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int ClientToScreen(HandleRef hWnd, [In, Out] POINT pt);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool ClipCursor(ref RECT rcClip);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool ClipCursor(COMRECT rcClip);

  public static IntPtr CloseEnhMetaFile(HandleRef hdc)
  {
    HandleCollector.Remove((IntPtr) hdc, HelperMethods.CommonHandles.HDC);
    return HandleCollector.Add(UnsafeMethods.IntCloseEnhMetaFile(hdc), HelperMethods.CommonHandles.GDI);
  }

  public static bool CloseHandle(HandleRef handle)
  {
    HandleCollector.Remove((IntPtr) handle, HelperMethods.CommonHandles.Kernel);
    return UnsafeMethods.IntCloseHandle(handle);
  }

  [DllImport("ole32.dll", PreserveSig = false)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public static extern object CoCreateInstance(
    [In] ref Guid clsid,
    [MarshalAs(UnmanagedType.Interface)] object punkOuter,
    int context,
    [In] ref Guid iid);

  [DllImport("ole32.dll", PreserveSig = false)]
  public static extern UnsafeMethods.IClassFactory2 CoGetClassObject(
    [In] ref Guid clsid,
    int dwContext,
    int serverInfo,
    [In] ref Guid refiid);

  [DllImport("ole32.dll")]
  public static extern int CoGetMalloc(int dwReserved, out UnsafeMethods.IMalloc pMalloc);

  [DllImport("ole32.dll")]
  public static extern int CoGetStandardMarshal(
    ref Guid riid,
    [MarshalAs(UnmanagedType.Interface)] object pUnk,
    int dwDestContext,
    IntPtr pvDestContext,
    int mshlflags,
    out Intermech.Controls.OleContainer.IMarshal ppMarshal);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
  public static extern void CopyMemory(IntPtr pdst, string psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
  public static extern void CopyMemory(IntPtr pdst, byte[] psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
  public static extern void CopyMemory(byte[] pdst, HandleRef psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
  public static extern void CopyMemory(IntPtr pdst, HandleRef psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
  public static extern void CopyMemoryA(IntPtr pdst, char[] psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
  public static extern void CopyMemoryA(IntPtr pdst, string psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
  public static extern void CopyMemoryA(StringBuilder pdst, HandleRef psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
  public static extern void CopyMemoryA(char[] pdst, HandleRef psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Unicode)]
  public static extern void CopyMemoryW(IntPtr pdst, char[] psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Unicode)]
  public static extern void CopyMemoryW(IntPtr pdst, string psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Unicode)]
  public static extern void CopyMemoryW(char[] pdst, HandleRef psrc, int cb);

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Unicode)]
  public static extern void CopyMemoryW(StringBuilder pdst, HandleRef psrc, int cb);

  [DllImport("ole32.dll")]
  public static extern int CoRegisterMessageFilter(HandleRef newFilter, ref IntPtr oldMsgFilter);

  public static IntPtr CreateAcceleratorTable(HandleRef pentries, int cCount)
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateAcceleratorTable(pentries, cCount), HelperMethods.CommonHandles.Accelerator);
  }

  public static IntPtr CreateCompatibleDC(HandleRef hDC)
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateCompatibleDC(hDC), HelperMethods.CommonHandles.HDC);
  }

  public static IntPtr CreateDC(string lpszDriver)
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateDC(lpszDriver, (string) null, (string) null, HelperMethods.NullHandleRef), HelperMethods.CommonHandles.HDC);
  }

  public static IntPtr CreateDC(
    string lpszDriverName,
    string lpszDeviceName,
    string lpszOutput,
    HandleRef lpInitData)
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateDC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), HelperMethods.CommonHandles.HDC);
  }

  public static IntPtr CreateEnhMetaFile(
    HandleRef hdcRef,
    string lpFilename,
    [In] ref RECT lpRect,
    string lpDescription)
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateEnhMetaFile(hdcRef, lpFilename, ref lpRect, lpDescription), HelperMethods.CommonHandles.HDC);
  }

  public static IntPtr CreateFileMapping(
    HandleRef hFile,
    IntPtr lpAttributes,
    int flProtect,
    int dwMaxSizeHi,
    int dwMaxSizeLow,
    string lpName)
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateFileMapping(hFile, lpAttributes, flProtect, dwMaxSizeHi, dwMaxSizeLow, lpName), HelperMethods.CommonHandles.Kernel);
  }

  public static IntPtr CreateIC(
    string lpszDriverName,
    string lpszDeviceName,
    string lpszOutput,
    HandleRef lpInitData)
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateIC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), HelperMethods.CommonHandles.HDC);
  }

  [DllImport("ole32.dll", PreserveSig = false)]
  public static extern UnsafeMethods.ILockBytes CreateILockBytesOnHGlobal(
    HandleRef hGlobal,
    bool fDeleteOnRelease);

  public static IntPtr CreateMenu()
  {
    return HandleCollector.Add(UnsafeMethods.IntCreateMenu(), HelperMethods.CommonHandles.Menu);
  }

  public static IntPtr CreatePopupMenu()
  {
    return HandleCollector.Add(UnsafeMethods.IntCreatePopupMenu(), HelperMethods.CommonHandles.Menu);
  }

  [DllImport("oleacc.dll", CharSet = CharSet.Auto)]
  public static extern int CreateStdAccessibleObject(
    HandleRef hWnd,
    int objID,
    ref Guid refiid,
    [MarshalAs(UnmanagedType.Interface), In, Out] ref object pAcc);

  public static IntPtr CreateWindowEx(
    int dwExStyle,
    string lpszClassName,
    string lpszWindowName,
    int style,
    int x,
    int y,
    int width,
    int height,
    HandleRef hWndParent,
    HandleRef hMenu,
    HandleRef hInst,
    [MarshalAs(UnmanagedType.AsAny)] object pvParam)
  {
    return UnsafeMethods.IntCreateWindowEx(dwExStyle, lpszClassName, lpszWindowName, style, x, y, width, height, hWndParent, hMenu, hInst, pvParam);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr DefFrameProc(
    IntPtr hWnd,
    IntPtr hWndClient,
    int msg,
    IntPtr wParam,
    IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr DefMDIChildProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

  public static bool DeleteDC(HandleRef hDC)
  {
    HandleCollector.Remove((IntPtr) hDC, HelperMethods.CommonHandles.HDC);
    return UnsafeMethods.IntDeleteDC(hDC);
  }

  public static IntPtr DeleteEnhMetaFile(HandleRef handle)
  {
    HandleCollector.Remove((IntPtr) handle, HelperMethods.CommonHandles.GDI);
    return UnsafeMethods.IntDeleteEnhMetaFile(handle);
  }

  public static bool DestroyAcceleratorTable(HandleRef hAccel)
  {
    HandleCollector.Remove((IntPtr) hAccel, HelperMethods.CommonHandles.Accelerator);
    return UnsafeMethods.IntDestroyAcceleratorTable(hAccel);
  }

  public static bool DestroyCursor(HandleRef hCurs)
  {
    HandleCollector.Remove((IntPtr) hCurs, HelperMethods.CommonHandles.Cursor);
    return UnsafeMethods.IntDestroyCursor(hCurs);
  }

  public static bool DestroyMenu(HandleRef hMenu)
  {
    HandleCollector.Remove((IntPtr) hMenu, HelperMethods.CommonHandles.Menu);
    return UnsafeMethods.IntDestroyMenu(hMenu);
  }

  public static bool DestroyWindow(HandleRef hWnd) => UnsafeMethods.IntDestroyWindow(hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr DispatchMessage([In] ref MSG msg);

  [DllImport("user32.dll", CharSet = CharSet.Ansi)]
  public static extern IntPtr DispatchMessageA([In] ref MSG msg);

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  public static extern IntPtr DispatchMessageW([In] ref MSG msg);

  [DllImport("shell32.dll", CharSet = CharSet.Ansi)]
  public static extern void DragAcceptFiles(HandleRef hWnd, bool fAccept);

  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  public static extern int DragQueryFile(
    HandleRef hDrop,
    int iFile,
    StringBuilder lpszFile,
    int cch);

  public static IntPtr DuplicateHandle(
    HandleRef processSource,
    HandleRef handleSource,
    HandleRef processTarget,
    ref IntPtr handleTarget,
    int desiredAccess,
    bool inheritHandle,
    int options)
  {
    IntPtr num = UnsafeMethods.IntDuplicateHandle(processSource, handleSource, processTarget, ref handleTarget, desiredAccess, inheritHandle, options);
    HandleCollector.Add(handleTarget, HelperMethods.CommonHandles.Kernel);
    return num;
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool EnableMenuItem(HandleRef hMenu, int UIDEnabledItem, int uEnable);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool EnableScrollBar(HandleRef hWnd, int nBar, int value);

  public static bool EndPaint(HandleRef hWnd, [MarshalAs(UnmanagedType.LPStruct), In] ref PAINTSTRUCT lpPaint)
  {
    HandleCollector.Remove(lpPaint.hdc, HelperMethods.CommonHandles.HDC);
    return UnsafeMethods.IntEndPaint(hWnd, ref lpPaint);
  }

  [DllImport("user32.dll")]
  public static extern bool EnumChildWindows(
    HandleRef hwndParent,
    EnumChildrenCallback lpEnumFunc,
    HandleRef lParam);

  [DllImport("user32.dll")]
  public static extern bool EnumChildWindows(
    HandleRef hwndParent,
    EnumChildrenProc lpEnumFunc,
    HandleRef lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool EnumThreadWindows(
    int dwThreadId,
    EnumThreadWindowsCallback lpfn,
    HandleRef lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr FindWindow(string className, string windowName);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern bool FreeLibrary(HandleRef hModule);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetActiveWindow();

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetAncestor(HandleRef hWnd, int flags);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern short GetAsyncKeyState(int vkey);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetCapture();

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern uint GetCaretBlinkTime();

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, IntPtr h);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, [In, Out] WNDCLASS wc);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, [In, Out] WNDCLASS_I wc);

  [DllImport("user32.dll")]
  public static extern int GetClassName(HandleRef hwnd, StringBuilder lpClassName, int nMaxCount);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetClientRect(HandleRef hWnd, [In, Out] ref RECT rect);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetComputerName(StringBuilder lpBuffer, int[] nSize);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetCursorPos([In, Out] POINT pt);

  public static IntPtr GetDC(HandleRef hWnd)
  {
    return HandleCollector.Add(UnsafeMethods.IntGetDC(hWnd), HelperMethods.CommonHandles.HDC);
  }

  public static IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags)
  {
    return HandleCollector.Add(UnsafeMethods.IntGetDCEx(hWnd, hrgnClip, flags), HelperMethods.CommonHandles.HDC);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetDesktopWindow();

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetDlgItem(HandleRef hWnd, int nIDDlgItem);

  [DllImport("oleaut32.dll", PreserveSig = false)]
  public static extern void GetErrorInfo(int reserved, [In, Out] ref UnsafeMethods.IErrorInfo errorInfo);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetFocus();

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetForegroundWindow();

  [DllImport("ole32.dll", PreserveSig = false)]
  public static extern IntPtr GetHGlobalFromILockBytes(UnsafeMethods.ILockBytes pLkbyt);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int GetKeyboardState(byte[] keystate);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern short GetKeyState(int keyCode);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetLayout(HandleRef hDC);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern int GetLocaleInfo(
    int Locale,
    int LCType,
    StringBuilder lpLCData,
    int cchData);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetMenu(HandleRef hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int GetMenuItemCount(HandleRef hMenu);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int GetMenuItemID(HandleRef hMenu, int nPos);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetMenuItemInfo(
    HandleRef hMenu,
    int uItem,
    bool fByPosition,
    [In, Out] MENUITEMINFO_T lpmii);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetMenuItemInfo(
    HandleRef hMenu,
    int uItem,
    bool fByPosition,
    [In, Out] MENUITEMINFO_T_RW lpmii);

  [DllImport("user32.dll", CharSet = CharSet.Ansi)]
  public static extern bool GetMessageA(
    [In, Out] ref MSG msg,
    HandleRef hWnd,
    int uMsgFilterMin,
    int uMsgFilterMax);

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  public static extern bool GetMessageW(
    [In, Out] ref MSG msg,
    HandleRef hWnd,
    int uMsgFilterMin,
    int uMsgFilterMax);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern int GetModuleFileName(HandleRef hModule, StringBuilder buffer, int length);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetModuleHandle(string modName);

  public static int GetObject(HandleRef hObject, LOGBRUSH lb)
  {
    return UnsafeMethods.GetObject(hObject, Marshal.SizeOf(typeof (LOGBRUSH)), lb);
  }

  public static int GetObject(HandleRef hObject, LOGFONT lp)
  {
    return UnsafeMethods.GetObject(hObject, Marshal.SizeOf(typeof (LOGFONT)), lp);
  }

  public static int GetObject(HandleRef hObject, LOGPEN lp)
  {
    return UnsafeMethods.GetObject(hObject, Marshal.SizeOf(typeof (LOGPEN)), lp);
  }

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] BITMAP bm);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] DIBSECTION ds);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] LOGBRUSH lb);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] LOGPEN lp);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObject(HandleRef hObject, int nSize, ref int nEntries);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] LOGFONT lf);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObject(HandleRef hObject, int nSize, int[] nEntries);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetObjectType(HandleRef hObject);

  [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool GetOpenFileName([In, Out] OPENFILENAME_I ofn);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetParent(HandleRef hWnd);

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
  public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

  [DllImport("user32.dll")]
  public static extern IntPtr GetProcessWindowStation();

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetProp(HandleRef hWnd, int atom);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetProp(HandleRef hWnd, string name);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetRegionData(HandleRef hRgn, int size, IntPtr lpRgnData);

  [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool GetSaveFileName([In, Out] OPENFILENAME_I ofn);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern uint GetShortPathName(
    string lpszLongPath,
    string lpszShortPath,
    uint cchBuffer);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern void GetStartupInfo([In, Out] STARTUPINFO startupinfo);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern void GetStartupInfo([In, Out] STARTUPINFO_I startupinfo_i);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr GetStockObject(int nIndex);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetSubMenu(HandleRef hwnd, int index);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetSystemMenu(HandleRef hWnd, bool bRevert);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int GetSystemMetrics(int nIndex);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetSystemPowerStatus([In, Out] ref SYSTEM_POWER_STATUS systemPowerStatus);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern void GetTempFileName(
    string tempDirName,
    string prefixName,
    int unique,
    StringBuilder sb);

  [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetUserName(StringBuilder lpBuffer, int[] nSize);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool GetUserObjectInformation(
    HandleRef hObj,
    int nIndex,
    [MarshalAs(UnmanagedType.LPStruct)] USEROBJECTFLAGS pvBuffer,
    int nLength,
    ref int lpnLengthNeeded);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetWindowLong(HandleRef hWnd, int nIndex);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int GetWindowPlacement(HandleRef hWnd, ref WINDOWPLACEMENT placement);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern short GlobalAddAtom(string atomName);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern short GlobalDeleteAtom(short atom);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GlobalFree(HandleRef handle);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GlobalLock(HandleRef handle);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GlobalReAlloc(HandleRef handle, int bytes, int flags);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern int GlobalSize(HandleRef handle);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern bool GlobalUnlock(HandleRef handle);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr ImmAssociateContext(HandleRef hWnd, HandleRef hIMC);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr ImmCreateContext();

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern bool ImmDestroyContext(HandleRef hIMC);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr ImmGetContext(HandleRef hWnd);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern bool ImmGetConversionStatus(
    HandleRef hIMC,
    ref int conversion,
    ref int sentence);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern bool ImmGetOpenStatus(HandleRef hIMC);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern bool ImmNotifyIME(HandleRef hIMC, int dwAction, int dwIndex, int dwValue);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern bool ImmReleaseContext(HandleRef hWnd, HandleRef hIMC);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern bool ImmSetConversionStatus(HandleRef hIMC, int conversion, int sentence);

  [DllImport("imm32.dll", CharSet = CharSet.Auto)]
  public static extern bool ImmSetOpenStatus(HandleRef hIMC, bool open);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool InsertMenuItem(
    HandleRef hMenu,
    int uItem,
    bool fByPosition,
    MENUITEMINFO_T lpmii);

  [DllImport("user32.dll", EntryPoint = "BeginPaint", CharSet = CharSet.Auto)]
  private static extern IntPtr IntBeginPaint(HandleRef hWnd, [In, Out] ref PAINTSTRUCT lpPaint);

  [DllImport("gdi32.dll", EntryPoint = "CloseEnhMetaFile", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntCloseEnhMetaFile(HandleRef hdc);

  [DllImport("kernel32.dll", EntryPoint = "CloseHandle", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern bool IntCloseHandle(HandleRef handle);

  [DllImport("user32.dll", EntryPoint = "CreateAcceleratorTable", CharSet = CharSet.Auto)]
  private static extern IntPtr IntCreateAcceleratorTable(HandleRef pentries, int cCount);

  [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

  [DllImport("gdi32.dll", EntryPoint = "CreateDC", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntCreateDC(
    string lpszDriver,
    string lpszDeviceName,
    string lpszOutput,
    HandleRef devMode);

  [DllImport("gdi32.dll", EntryPoint = "CreateEnhMetaFile", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr IntCreateEnhMetaFile(
    HandleRef hdcRef,
    string lpFilename,
    [In] ref RECT lpRect,
    string lpDescription);

  [DllImport("kernel32.dll", EntryPoint = "CreateFileMapping", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntCreateFileMapping(
    HandleRef hFile,
    IntPtr lpAttributes,
    int flProtect,
    int dwMaxSizeHi,
    int dwMaxSizeLow,
    string lpName);

  [DllImport("gdi32.dll", EntryPoint = "CreateIC", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntCreateIC(
    string lpszDriverName,
    string lpszDeviceName,
    string lpszOutput,
    HandleRef lpInitData);

  [DllImport("user32.dll", EntryPoint = "CreateMenu", CharSet = CharSet.Auto)]
  private static extern IntPtr IntCreateMenu();

  [DllImport("user32.dll", EntryPoint = "CreatePopupMenu", CharSet = CharSet.Auto)]
  private static extern IntPtr IntCreatePopupMenu();

  [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr IntCreateWindowEx(
    int dwExStyle,
    string lpszClassName,
    string lpszWindowName,
    int style,
    int x,
    int y,
    int width,
    int height,
    HandleRef hWndParent,
    HandleRef hMenu,
    HandleRef hInst,
    [MarshalAs(UnmanagedType.AsAny)] object pvParam);

  [DllImport("gdi32.dll", EntryPoint = "DeleteDC", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern bool IntDeleteDC(HandleRef hDC);

  [DllImport("gdi32.dll", EntryPoint = "DeleteEnhMetaFile", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntDeleteEnhMetaFile(HandleRef handle);

  [DllImport("user32.dll", EntryPoint = "DestroyAcceleratorTable", CharSet = CharSet.Auto)]
  private static extern bool IntDestroyAcceleratorTable(HandleRef hAccel);

  [DllImport("user32.dll", EntryPoint = "DestroyCursor", CharSet = CharSet.Auto)]
  private static extern bool IntDestroyCursor(HandleRef hCurs);

  [DllImport("user32.dll", EntryPoint = "DestroyMenu", CharSet = CharSet.Auto)]
  private static extern bool IntDestroyMenu(HandleRef hMenu);

  [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Auto)]
  public static extern bool IntDestroyWindow(HandleRef hWnd);

  [DllImport("kernel32.dll", EntryPoint = "DuplicateHandle", SetLastError = true)]
  private static extern IntPtr IntDuplicateHandle(
    HandleRef processSource,
    HandleRef handleSource,
    HandleRef processTarget,
    ref IntPtr handleTarget,
    int desiredAccess,
    bool inheritHandle,
    int options);

  [DllImport("user32.dll", EntryPoint = "EndPaint", CharSet = CharSet.Auto)]
  private static extern bool IntEndPaint(HandleRef hWnd, ref PAINTSTRUCT lpPaint);

  [DllImport("user32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto)]
  private static extern IntPtr IntGetDC(HandleRef hWnd);

  [DllImport("user32.dll", EntryPoint = "GetDCEx", CharSet = CharSet.Auto)]
  private static extern IntPtr IntGetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);

  [DllImport("kernel32.dll", EntryPoint = "MapViewOfFile", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntMapViewOfFile(
    HandleRef hFileMapping,
    int dwDesiredAccess,
    int dwFileOffsetHigh,
    int dwFileOffsetLow,
    int dwNumberOfBytesToMap);

  [DllImport("ole32.dll", EntryPoint = "OleInitialize", SetLastError = true)]
  private static extern int IntOleInitialize(int val);

  [DllImport("kernel32.dll", EntryPoint = "OpenFileMapping", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr IntOpenFileMapping(
    int dwDesiredAccess,
    bool bInheritHandle,
    string lpName);

  [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto)]
  private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

  [DllImport("user32.dll", EntryPoint = "SetWindowRgn", CharSet = CharSet.Auto)]
  private static extern int IntSetWindowRgn(HandleRef hwnd, HandleRef hrgn, bool fRedraw);

  [DllImport("kernel32.dll", EntryPoint = "UnmapViewOfFile", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern bool IntUnmapViewOfFile(HandleRef pvBaseAddress);

  [DllImport("kernel32.dll")]
  public static extern bool IsBadReadPtr(HandleRef ptr, int size);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool IsChild(HandleRef hWndParent, HandleRef hwnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool IsDialogMessage(HandleRef hWndDlg, [In, Out] ref MSG msg);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool IsWindow(HandleRef hWnd);

  [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto)]
  public static extern void Keybd_event(byte vk, byte scan, int flags, int extrainfo);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr LoadLibrary(string libname);

  [DllImport("mscoree.dll", CharSet = CharSet.Unicode)]
  public static extern int LoadLibraryShim(
    string dllName,
    string version,
    IntPtr reserved,
    out IntPtr dllModule);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool LoadString(
    HandleRef hInstance,
    int uID,
    StringBuilder lpBuffer,
    int nBufferMax);

  [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool LookupAccountName(
    string machineName,
    string accountName,
    byte[] sid,
    ref int sidLen,
    StringBuilder domainName,
    ref int domainNameLen,
    out int peUse);

  [DllImport("oleacc.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr LresultFromObject(ref Guid refiid, IntPtr wParam, HandleRef pAcc);

  public static IntPtr MapViewOfFile(
    HandleRef hFileMapping,
    int dwDesiredAccess,
    int dwFileOffsetHigh,
    int dwFileOffsetLow,
    int dwNumberOfBytesToMap)
  {
    return HandleCollector.Add(UnsafeMethods.IntMapViewOfFile(hFileMapping, dwDesiredAccess, dwFileOffsetHigh, dwFileOffsetLow, dwNumberOfBytesToMap), HelperMethods.CommonHandles.Kernel);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int MapWindowPoints(
    HandleRef hWndFrom,
    HandleRef hWndTo,
    [In, Out] ref RECT rect,
    int cPoints);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int MapWindowPoints(
    HandleRef hWndFrom,
    HandleRef hWndTo,
    [In, Out] POINT pt,
    int cPoints);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr mmioAscend(IntPtr hMIO, MMCKINFO lpck, int flags);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern int mmioClose(IntPtr hMIO, int flags);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr mmioDescend(
    IntPtr hMIO,
    [MarshalAs(UnmanagedType.LPStruct)] MMCKINFO lpck,
    [MarshalAs(UnmanagedType.LPStruct)] MMCKINFO lcpkParent,
    int flags);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr mmioOpen(string fileName, IntPtr not_used, int flags);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern int mmioRead(IntPtr hMIO, [MarshalAs(UnmanagedType.I4)] int dw, int cch);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern int mmioRead(IntPtr hMIO, [MarshalAs(UnmanagedType.LPStruct)] WAVEFORMATEX wf, int cch);

  [DllImport("oledlg.dll", CharSet = CharSet.Auto)]
  public static extern uint OleUIInsertObject([In, Out] ref UnsafeMethods.OLEUIINSERTOBJECT lpIO);

  [DllImport("gdi32.dll")]
  public static extern bool DeleteMetaFile(IntPtr hmf);

  [DllImport("kernel32.dll")]
  public static extern bool GlobalUnlock(IntPtr hMem);

  [DllImport("kernel32.dll")]
  public static extern IntPtr GlobalFree(IntPtr hMem);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int MsgWaitForMultipleObjects(
    int nCount,
    IntPtr pHandles,
    bool fWaitAll,
    int dwMilliseconds,
    int dwWakeMask);

  [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
  public static extern int MultiByteToWideChar(
    int CodePage,
    int dwFlags,
    byte[] lpMultiByteStr,
    int cchMultiByte,
    char[] lpWideCharStr,
    int cchWideChar);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern void NotifyWinEvent(int winEvent, HandleRef hwnd, int objType, int objID);

  [DllImport("ole32.dll")]
  public static extern int OleCreate(
    ref Guid clsid,
    ref Guid iid,
    OLERENDER renderOpt,
    [MarshalAs(UnmanagedType.LPArray), In] FORMATETC[] pFormat,
    UnsafeMethods.IOleClientSite pClientSite,
    UnsafeMethods.IStorage pStorage,
    out UnsafeMethods.IOleObject pObject);

  [DllImport("ole32.dll")]
  public static extern int OleCreateFromFile(
    ref Guid clsid,
    [MarshalAs(UnmanagedType.LPWStr)] string fileName,
    ref Guid iid,
    OLERENDER renderOpt,
    [MarshalAs(UnmanagedType.LPArray), In] FORMATETC[] pFormat,
    UnsafeMethods.IOleClientSite pClientSite,
    UnsafeMethods.IStorage pStorage,
    out UnsafeMethods.IOleObject pObject);

  [DllImport("ole32.dll")]
  public static extern int OleCreateFromData(
    IDataObject pSrcDataObj,
    [In] ref Guid riid,
    uint renderopt,
    [MarshalAs(UnmanagedType.LPArray), In] FORMATETC[] pFormat,
    UnsafeMethods.IOleClientSite pClientSite,
    UnsafeMethods.IStorage pStg,
    out UnsafeMethods.IOleObject ppvObj);

  [DllImport("ole32.dll")]
  public static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);

  [DllImport("user32.dll")]
  public static extern uint RegisterClipboardFormat([MarshalAs(UnmanagedType.LPWStr)] string format);

  [DllImport("ole32.dll")]
  public static extern int OleDoAutoConvert(UnsafeMethods.IStorage pStg, out Guid guid);

  [DllImport("ole32.dll", CharSet = CharSet.Auto)]
  public static extern int OleFlushClipboard();

  [DllImport("ole32.dll")]
  public static extern int OleGetAutoConvert(ref Guid clsid, out Guid newClsid);

  [DllImport("ole32.dll", CharSet = CharSet.Auto)]
  public static extern int OleGetClipboard(ref IDataObject data);

  public static int OleInitialize() => UnsafeMethods.IntOleInitialize(0);

  [DllImport("ole32.dll")]
  public static extern int OleLoad(
    UnsafeMethods.IStorage pStorage,
    ref Guid iid,
    UnsafeMethods.IOleClientSite pClientSite,
    out UnsafeMethods.IOleObject pObject);

  [DllImport("ole32.dll")]
  public static extern int OleLoadFromStream(
    Intermech.Controls.OleContainer.IStream pStorage,
    ref Guid iid,
    out UnsafeMethods.IOleObject pObject);

  [DllImport("ole32.dll")]
  public static extern int OleLockRunning(
    UnsafeMethods.IOleObject pObject,
    bool fLock,
    bool fLastUnlockCloses);

  [DllImport("ole32.dll")]
  public static extern int OleRun(UnsafeMethods.IOleObject pObject);

  [DllImport("ole32.dll")]
  public static extern bool OleIsRunning(UnsafeMethods.IOleObject pObject);

  [DllImport("ole32.dll")]
  public static extern int OleSave(
    UnsafeMethods.IPersistStorage pPersistStorage,
    UnsafeMethods.IStorage pStorage,
    bool fSameAsLoad);

  [DllImport("ole32.dll")]
  public static extern int OleSaveToStream(
    UnsafeMethods.IPersistStream pPersistStream,
    Intermech.Controls.OleContainer.IStream pStream);

  [DllImport("ole32.dll", CharSet = CharSet.Auto)]
  public static extern int OleSetClipboard(IDataObject pDataObj);

  [DllImport("ole32.dll")]
  public static extern int OleSetMenuDescriptor(
    IntPtr hOleMenu,
    IntPtr hWndFrame,
    IntPtr hWndActiveObject,
    UnsafeMethods.IOleInPlaceFrame frame,
    UnsafeMethods.IOleInPlaceActiveObject activeObject);

  [DllImport("ole32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int OleUninitialize();

  public static IntPtr OpenFileMapping(int dwDesiredAccess, bool bInheritHandle, string lpName)
  {
    return HandleCollector.Add(UnsafeMethods.IntOpenFileMapping(dwDesiredAccess, bInheritHandle, lpName), HelperMethods.CommonHandles.Kernel);
  }

  [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool PageSetupDlg([In, Out] PAGESETUPDLG lppsd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool PeekMessage(
    [In, Out] ref MSG msg,
    HandleRef hwnd,
    int msgMin,
    int msgMax,
    int remove);

  [DllImport("user32.dll", CharSet = CharSet.Ansi)]
  public static extern bool PeekMessageA(
    [In, Out] ref MSG msg,
    HandleRef hwnd,
    int msgMin,
    int msgMax,
    int remove);

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  public static extern bool PeekMessageW(
    [In, Out] ref MSG msg,
    HandleRef hwnd,
    int msgMin,
    int msgMax,
    int remove);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool PlayEnhMetaFile(HandleRef hdc, HandleRef hemf, ref RECT lpRect);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern bool PlaySound(byte[] soundName, IntPtr hmod, int soundFlags);

  [DllImport("winmm.dll", CharSet = CharSet.Auto)]
  public static extern bool PlaySound(string soundName, IntPtr hmod, int soundFlags);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, int lparam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern void PostQuitMessage(int nExitCode);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int PostThreadMessage(int id, int msg, IntPtr wparam, IntPtr lparam);

  [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool PrintDlg([In, Out] PRINTDLG lppd);

  [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int PrintDlgEx([In, Out] PRINTDLGEX lppdex);

  [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
  [ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
  public static void PtrToStructure(IntPtr lparam, object data)
  {
    Marshal.PtrToStructure(lparam, data);
  }

  [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
  [ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
  public static object PtrToStructure(IntPtr lparam, Type cls)
  {
    return Marshal.PtrToStructure(lparam, cls);
  }

  [DllImport("ole32.dll")]
  public static extern int ReadClassStg(HandleRef pStg, [In, Out] ref Guid pclsid);

  [DllImport("ole32.dll")]
  public static extern int ReadClassStg(UnsafeMethods.IStorage pStorage, out Guid clsid);

  [DllImport("ole32.dll")]
  public static extern int ReadClassStm(Intermech.Controls.OleContainer.IStream pStream, out Guid clsid);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr RegisterClass(WNDCLASS_D wc);

  [DllImport("ole32.dll", CharSet = CharSet.Auto)]
  public static extern int RegisterDragDrop(HandleRef hwnd, UnsafeMethods.IOleDropTarget target);

  public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
  {
    HandleCollector.Remove((IntPtr) hDC, HelperMethods.CommonHandles.HDC);
    return UnsafeMethods.IntReleaseDC(hWnd, hDC);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool RemoveMenu(HandleRef hMenu, int uPosition, int uFlags);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr RemoveProp(HandleRef hWnd, int atom);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr RemoveProp(HandleRef hWnd, string propName);

  [DllImport("ole32.dll", CharSet = CharSet.Auto)]
  public static extern int RevokeDragDrop(HandleRef hwnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int ScreenToClient(HandleRef hWnd, [In, Out] POINT pt);

  [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
  public static extern IntPtr SendCallbackMessage(
    HandleRef hWnd,
    int Msg,
    IntPtr wParam,
    IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendDlgItemMessage(
    HandleRef hDlg,
    int nIDDlgItem,
    int Msg,
    IntPtr wParam,
    IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, bool wParam, int lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, HandleRef lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    StringBuilder lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    [MarshalAs(UnmanagedType.LPStruct), In, Out] CHARFORMAT2A lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct), In, Out] CHARFORMATA lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int Msg,
    int wParam,
    ref TVSORTCB tvSort);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct), In, Out] CHARFORMATW lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In, Out] ref RECT lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, CHARRANGE lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, [MarshalAs(UnmanagedType.Bool), In, Out] ref bool wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int Msg,
    ref short wParam,
    ref short lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, EDITSTREAM lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int[] lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    EDITSTREAM64 lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, FINDTEXT lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In, Out] LOGFONT lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, LVBKIMAGE lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, LVCOLUMN lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, LVCOLUMN_T lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, LVGROUP lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    LVHITTESTINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SendMessage(HandleRef hWnd, int msg, int wParam, LVINSERTMARK lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    MCHITTESTINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.IUnknown)] out object editOle);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref HDITEM lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    [In, Out] LVTILEVIEWINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    ref TBBUTTON lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    TOOLINFO_TOOLTIP lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    [In, Out] ref LVFINDINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, TOOLINFO_T lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, SYSTEMTIME lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    ref LVHITTESTINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In, Out] SIZE lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In, Out] ref LVITEM lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    ref TV_INSERTSTRUCT lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref TV_ITEM lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, MSG lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct), In, Out] PARAFORMAT lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    REPASTESPECIAL lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    ref TBBUTTONINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, POINT lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, TCITEM_T lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    ref TOOLINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, TEXTRANGE lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    TV_HITTESTINFO lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, [In, Out] ref RECT lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    GETTEXTLENGTHEX wParam,
    int lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int[] wParam, int[] lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, ref int wParam, ref int lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, string lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int Msg,
    IntPtr wParam,
    ListViewCompareCallback pfnCompare);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, HandleRef wParam, int lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, POINT wParam, int lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    POINT wParam,
    [In, Out] LVINSERTMARK lParam);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetActiveWindow(HandleRef hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetCapture(HandleRef hwnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int SetClassLong(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetCursor(HandleRef hcursor);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetCursorPos(int x, int y);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetFocus(HandleRef hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetForegroundWindow(HandleRef hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int SetKeyboardState(byte[] keystate);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool SetLayeredWindowAttributes(
    HandleRef hwnd,
    int crKey,
    byte bAlpha,
    int dwFlags);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int SetLayout(HandleRef hDC, int dwLayout);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetMenu(HandleRef hWnd, HandleRef hMenu);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetMenuDefaultItem(HandleRef hwnd, int nIndex, bool pos);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetMenuItemInfo(
    HandleRef hMenu,
    int uItem,
    bool fByPosition,
    MENUITEMINFO_T lpmii);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetParent(HandleRef hWnd, HandleRef hWndParent);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetProp(HandleRef hWnd, int atom, HandleRef data);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetProp(HandleRef hWnd, string propName, HandleRef data);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int SetScrollInfo(HandleRef hWnd, int fnBar, SCROLLINFO si, bool redraw);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int SetScrollPos(HandleRef hWnd, int nBar, int nPos, bool bRedraw);

  [DllImport("Powrprof.dll", CharSet = CharSet.Auto)]
  public static extern bool SetSuspendState(
    bool hiberate,
    bool forceCritical,
    bool disableWakeEvent);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, WndProc wndproc);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetWindowPlacement(HandleRef hWnd, [In] ref WINDOWPLACEMENT placement);

  public static int SetWindowRgn(HandleRef hwnd, HandleRef hrgn, bool fRedraw)
  {
    if ((IntPtr) hrgn != IntPtr.Zero)
      HandleCollector.Remove((IntPtr) hrgn, HelperMethods.CommonHandles.GDI);
    return UnsafeMethods.IntSetWindowRgn(hwnd, hrgn, fRedraw);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SetWindowsHookEx(
    int hookid,
    HookProc pfnhook,
    HandleRef hinst,
    int threadid);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetWindowText(HandleRef hWnd, string text);

  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  public static extern int Shell_NotifyIcon(int message, NOTIFYICONDATA pnid);

  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr ShellExecute(
    HandleRef hwnd,
    string lpOperation,
    string lpFile,
    string lpParameters,
    string lpDirectory,
    int nShowCmd);

  [DllImport("shell32.dll", EntryPoint = "ShellExecute", CharSet = CharSet.Auto)]
  public static extern IntPtr ShellExecute_NoBFM(
    HandleRef hwnd,
    string lpOperation,
    string lpFile,
    string lpParameters,
    string lpDirectory,
    int nShowCmd);

  [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
  public static extern int SHGetFolderPath(
    HandleRef hwndOwner,
    int nFolder,
    HandleRef hToken,
    int dwFlags,
    StringBuilder lpszPath);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int ShowCursor(bool bShow);

  [DllImport("ole32.dll")]
  public static extern int StgCreateDocfile(
    [MarshalAs(UnmanagedType.LPWStr)] string docName,
    int grfMode,
    int reserved,
    [MarshalAs(UnmanagedType.Interface)] out UnsafeMethods.IStorage pStorage);

  [DllImport("ole32.dll", PreserveSig = false)]
  public static extern UnsafeMethods.IStorage StgCreateDocfileOnILockBytes(
    UnsafeMethods.ILockBytes iLockBytes,
    int grfMode,
    int reserved);

  [DllImport("ole32.dll", PreserveSig = false)]
  public static extern UnsafeMethods.IStorage StgOpenStorageOnILockBytes(
    UnsafeMethods.ILockBytes iLockBytes,
    UnsafeMethods.IStorage pStgPriority,
    int grfMode,
    int sndExcluded,
    int reserved);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    [In, Out] LOGFONT font,
    int nUpdate);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    [In, Out] NONCLIENTMETRICS metrics,
    int nUpdate);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    ref bool value,
    int ignore);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    ref int value,
    int ignore);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    ref HIGHCONTRAST_I rc,
    int nUpdate);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    ref RECT rc,
    int nUpdate);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    bool[] flag,
    bool nUpdate);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SystemParametersInfo(
    int nAction,
    int nParam,
    [In, Out] IntPtr[] rc,
    int nUpdate);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool TranslateMDISysAccel(IntPtr hWndClient, [In, Out] ref MSG msg);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool TranslateMessage([In, Out] ref MSG msg);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool UnhookWindowsHookEx(HandleRef hhook);

  public static bool UnmapViewOfFile(HandleRef pvBaseAddress)
  {
    HandleCollector.Remove((IntPtr) pvBaseAddress, HelperMethods.CommonHandles.Kernel);
    return UnsafeMethods.IntUnmapViewOfFile(pvBaseAddress);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool UnregisterClass(string className, HandleRef hInstance);

  [DllImport("oleaut32.dll")]
  public static extern int VarFormat(
    ref object pvarIn,
    HandleRef pstrFormat,
    int iFirstDay,
    int iFirstWeek,
    uint dwFlags,
    [In, Out] ref IntPtr pbstr);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern short VkKeyScan(char key);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern void WaitMessage();

  [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
  public static extern int WideCharToMultiByte(
    int codePage,
    int flags,
    [MarshalAs(UnmanagedType.LPWStr)] string wideStr,
    int chars,
    [In, Out] byte[] pOutBytes,
    int bufferBytes,
    IntPtr defaultChar,
    IntPtr pDefaultUsed);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern IntPtr WindowFromDC(HandleRef hDC);

  public static IntPtr WindowFromPoint(int x, int y)
  {
    return UnsafeMethods._WindowFromPoint(new Intermech.Controls.OleContainer.POINTSTRUCT(x, y));
  }

  [DllImport("ole32.dll")]
  public static extern int WriteClassStg(UnsafeMethods.IStorage pStorage, ref Guid clsid);

  [DllImport("ole32.dll")]
  public static extern int WriteClassStm(Intermech.Controls.OleContainer.IStream pStream, ref Guid clsid);

  public struct OLEUIINSERTOBJECT
  {
    public int cbStruct;
    public int dwFlags;
    public IntPtr hWndOwner;
    public IntPtr lpszCaption;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public WndProc lpfnHook;
    public IntPtr lCustData;
    public IntPtr hInstance;
    public IntPtr lpszTemplate;
    public IntPtr hResource;
    public int clsid_data1;
    [MarshalAs(UnmanagedType.I2)]
    public short clsid_data2;
    [MarshalAs(UnmanagedType.I2)]
    public short clsid_data3;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b0;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b1;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b2;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b3;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b4;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b5;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b6;
    [MarshalAs(UnmanagedType.U1)]
    public byte clsid_b7;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string lpszFile;
    public uint cchFile;
    public uint cClsidExclude;
    public IntPtr lpClsidExclude;
    public Guid iid;
    public int oleRender;
    public IntPtr lpFormatEtc;
    [MarshalAs(UnmanagedType.Interface)]
    public object lpIOleClientSite;
    [MarshalAs(UnmanagedType.Interface)]
    public object lpIStorage;
    public IntPtr ppvObj;
    public int sc;
    public IntPtr hMetaPict;
  }

  public delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

  [Flags]
  public enum BrowseInfos
  {
    HideNewFolderButton = 512, // 0x00000200
    NewDialogStyle = 64, // 0x00000040
  }

  public class ComStreamFromDataStream : Intermech.Controls.OleContainer.IStream
  {
    protected Stream dataStream;
    private long virtualPosition;

    protected ComStreamFromDataStream() => this.virtualPosition = -1L;

    public void Stat([In] IntPtr pStatstg, [In] int grfStatFlag)
    {
    }

    public ComStreamFromDataStream(Stream dataStream)
    {
      this.virtualPosition = -1L;
      this.dataStream = dataStream != null ? dataStream : throw new ArgumentNullException(nameof (dataStream));
    }

    private void ActualizeVirtualPosition()
    {
      if (this.virtualPosition == -1L)
        return;
      if (this.virtualPosition > this.dataStream.Length)
        this.dataStream.SetLength(this.virtualPosition);
      this.dataStream.Position = this.virtualPosition;
      this.virtualPosition = -1L;
    }

    public Intermech.Controls.OleContainer.IStream Clone()
    {
      UnsafeMethods.ComStreamFromDataStream.NotImplemented();
      return (Intermech.Controls.OleContainer.IStream) null;
    }

    public void Commit(int grfCommitFlags)
    {
      this.dataStream.Flush();
      this.ActualizeVirtualPosition();
    }

    public long CopyTo(Intermech.Controls.OleContainer.IStream pstm, long cb, long[] pcbRead)
    {
      int cb1 = 4096 /*0x1000*/;
      IntPtr num1 = Marshal.AllocHGlobal(cb1);
      if (num1 == IntPtr.Zero)
        throw new OutOfMemoryException();
      long num2 = 0;
      try
      {
        int len;
        for (; num2 < cb; num2 += (long) len)
        {
          int length = cb1;
          if (num2 + (long) length > cb)
            length = (int) (cb - num2);
          len = this.Read(num1, length);
          if (len != 0)
          {
            if (pstm.Write(num1, len) != len)
              throw UnsafeMethods.ComStreamFromDataStream.EFail("Wrote an incorrect number of bytes");
          }
          else
            break;
        }
      }
      finally
      {
        Marshal.FreeHGlobal(num1);
      }
      if (pcbRead != null && pcbRead.Length != 0)
        pcbRead[0] = num2;
      return num2;
    }

    protected static ExternalException EFail(string msg)
    {
      throw new ExternalException(msg, -2147467259 /*0x80004005*/);
    }

    public Stream GetDataStream() => this.dataStream;

    public void LockRegion(long libOffset, long cb, int dwLockType)
    {
    }

    protected static void NotImplemented()
    {
      throw new ExternalException(LangStrings.GetString("UnsafeNativeMethodsNotImplemented"), -2147467263 /*0x80004001*/);
    }

    public int Read(IntPtr buf, int length)
    {
      byte[] numArray = new byte[length];
      int length1 = this.Read(numArray, length);
      Marshal.Copy(numArray, 0, buf, length1);
      return length1;
    }

    public int Read(byte[] buffer, int length)
    {
      this.ActualizeVirtualPosition();
      return this.dataStream.Read(buffer, 0, length);
    }

    public void Revert() => UnsafeMethods.ComStreamFromDataStream.NotImplemented();

    public long Seek(long offset, int origin)
    {
      long num = this.virtualPosition;
      if (this.virtualPosition == -1L)
        num = this.dataStream.Position;
      long length = this.dataStream.Length;
      switch (origin)
      {
        case 0:
          if (offset > length)
          {
            this.virtualPosition = offset;
            break;
          }
          this.dataStream.Position = offset;
          this.virtualPosition = -1L;
          break;
        case 1:
          if (offset + num > length)
          {
            this.virtualPosition = offset + num;
            break;
          }
          this.dataStream.Position = num + offset;
          this.virtualPosition = -1L;
          break;
        case 2:
          if (offset > 0L)
          {
            this.virtualPosition = length + offset;
            break;
          }
          this.dataStream.Position = length + offset;
          this.virtualPosition = -1L;
          break;
      }
      return this.virtualPosition != -1L ? this.virtualPosition : this.dataStream.Position;
    }

    public void SetSize(long value) => this.dataStream.SetLength(value);

    public void Stat(STATSTG pstatstg, int grfStatFlag)
    {
      pstatstg.type = 2;
      pstatstg.cbSize = this.dataStream.Length;
      pstatstg.grfLocksSupported = 2;
    }

    public void UnlockRegion(long libOffset, long cb, int dwLockType)
    {
    }

    public int Write(IntPtr buf, int length)
    {
      byte[] numArray = new byte[length];
      Marshal.Copy(buf, numArray, 0, length);
      return this.Write(numArray, length);
    }

    public int Write(byte[] buffer, int length)
    {
      this.ActualizeVirtualPosition();
      this.dataStream.Write(buffer, 0, length);
      return length;
    }
  }

  [Guid("CB2F6723-AB3A-11d2-9C40-00C04FA30A3E")]
  [ComImport]
  public class CorRuntimeHost
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern CorRuntimeHost();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f610-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLAnchorEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [Guid("3050f611-98b5-11cf-bb82-00aa00bdce0b")]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLAreaEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [Guid("3050f617-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLButtonElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [Guid("3050f612-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [ComImport]
  public interface DHTMLControlElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f613-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLDocumentEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(8)]
    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1023 /*0x03FF*/)]
    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1026)]
    bool onstop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onbeforeeditfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onselectionchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [Guid("3050f60f-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f614-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLFormElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onsubmit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onreset(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f7ff-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLFrameSiteEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f616-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLImgEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onabort(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [Guid("3050f61a-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLInputFileElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onabort(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f61b-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLInputImageEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onabort(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [Guid("3050f618-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [ComImport]
  public interface DHTMLInputTextElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onabort(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f61c-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLLabelEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f61d-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLLinkElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f61e-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLMapEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f61f-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLMarqueeElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onchange_void(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onbounce(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfinish(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f619-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLOptionButtonElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onabort(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f621-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLScriptEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [Guid("3050f622-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLSelectElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onchange_void(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [Guid("3050f615-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [ComImport]
  public interface DHTMLStyleElementEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onerror(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f623-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLTableEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [Guid("3050f624-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [ComImport]
  public interface DHTMLTextContainerEvents2
  {
    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-600)]
    bool onclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-601)]
    bool ondblclick(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-603)]
    bool onkeypress(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-602)]
    void onkeydown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-604)]
    void onkeyup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-606)]
    void onmousemove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-605)]
    void onmousedown(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(-607)]
    void onmouseup(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onselectstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfilterchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragstart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onafterupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onerrorupdate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onrowexit(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetchanged(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondataavailable(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondatasetcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlosecapture(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpropertychange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrag(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondragover(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondragleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool ondrop(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncut(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforecopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncopy(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforepaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onpaste(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontextmenu(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsdelete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onrowsinserted(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void oncellchange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onreadystatechange(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onlayoutcomplete(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onpage(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseenter(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmouseleave(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void ondeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforedeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onbeforeactivate(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1048)]
    void onfocusin(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1049)]
    void onfocusout(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmove(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool oncontrolselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmovestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onmoveend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1040)]
    bool onresizestart(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1041)]
    void onresizeend(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onmousewheel(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onchange_void(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onselect(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [Guid("3050f613-98b5-11cf-bb82-00aa00bdce0b")]
  [ComImport]
  public interface DHTMLWindowEvents2
  {
    [DispId(1003)]
    void onload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1008)]
    void onunload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    bool onhelp(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onfocus(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    void onblur(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1002)]
    void onerror(string description, string url, int line);

    [DispId(1016)]
    void onresize(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1014)]
    void onscroll(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1017)]
    void onbeforeunload(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1024 /*0x0400*/)]
    void onbeforeprint(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);

    [DispId(1025)]
    void onafterprint(Intermech.Controls.OleContainer.IHTMLEventObj evtObj);
  }

  public enum DOCHOSTUIDBLCLICK
  {
    DEFAULT,
    SHOWPROPERTIES,
    SHOWCODE,
  }

  public enum DOCHOSTUIFLAG
  {
    DIALOG = 1,
    DISABLE_HELP_MENU = 2,
    NO3DBORDER = 4,
    SCROLL_NO = 8,
    DISABLE_SCRIPT_INACTIVE = 16, // 0x00000010
    OPENNEWWIN = 32, // 0x00000020
    DISABLE_OFFSCREEN = 64, // 0x00000040
    FLAT_SCROLLBAR = 128, // 0x00000080
    DIV_BLOCKDEFAULT = 256, // 0x00000100
    ACTIVATE_CLIENTHIT_ONLY = 512, // 0x00000200
    DISABLE_COOKIE = 1024, // 0x00000400
    THEME = 262144, // 0x00040000
    NOTHEME = 524288, // 0x00080000
    NO3DOUTERBORDER = 2097152, // 0x00200000
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  [Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
  [TypeLibType(TypeLibTypeFlags.FHidden)]
  [ComImport]
  public interface DWebBrowserEvents2
  {
    [DispId(102)]
    void StatusTextChange([In] string text);

    [DispId(108)]
    void ProgressChange([In] int progress, [In] int progressMax);

    [DispId(105)]
    void CommandStateChange([In] long command, [In] bool enable);

    [DispId(106)]
    void DownloadBegin();

    [DispId(104)]
    void DownloadComplete();

    [DispId(113)]
    void TitleChange([In] string text);

    [DispId(112 /*0x70*/)]
    void PropertyChange([In] string szProperty);

    [DispId(250)]
    void BeforeNavigate2(
      [MarshalAs(UnmanagedType.IDispatch), In] object pDisp,
      [In] ref object URL,
      [In] ref object flags,
      [In] ref object targetFrameName,
      [In] ref object postData,
      [In] ref object headers,
      [In, Out] ref bool cancel);

    [DispId(251)]
    void NewWindow2([MarshalAs(UnmanagedType.IDispatch), In, Out] ref object pDisp, [In, Out] ref bool cancel);

    [DispId(252)]
    void NavigateComplete2([MarshalAs(UnmanagedType.IDispatch), In] object pDisp, [In] ref object URL);

    [DispId(259)]
    void DocumentComplete([MarshalAs(UnmanagedType.IDispatch), In] object pDisp, [In] ref object URL);

    [DispId(253)]
    void OnQuit();

    [DispId(254)]
    void OnVisible([In] bool visible);

    [DispId(255 /*0xFF*/)]
    void OnToolBar([In] bool toolBar);

    [DispId(256 /*0x0100*/)]
    void OnMenuBar([In] bool menuBar);

    [DispId(257)]
    void OnStatusBar([In] bool statusBar);

    [DispId(258)]
    void OnFullScreen([In] bool fullScreen);

    [DispId(260)]
    void OnTheaterMode([In] bool theaterMode);

    [DispId(262)]
    void WindowSetResizable([In] bool resizable);

    [DispId(264)]
    void WindowSetLeft([In] int left);

    [DispId(265)]
    void WindowSetTop([In] int top);

    [DispId(266)]
    void WindowSetWidth([In] int width);

    [DispId(267)]
    void WindowSetHeight([In] int height);

    [DispId(263)]
    void WindowClosing([In] bool isChildWindow, [In, Out] ref bool cancel);

    [DispId(268)]
    void ClientToHostWindow([In, Out] ref long cx, [In, Out] ref long cy);

    [DispId(269)]
    void SetSecureLockIcon([In] int secureLockIcon);

    [DispId(270)]
    void FileDownload([In, Out] ref bool cancel);

    [DispId(271)]
    void NavigateError(
      [MarshalAs(UnmanagedType.IDispatch), In] object pDisp,
      [In] ref object URL,
      [In] ref object frame,
      [In] ref object statusCode,
      [In, Out] ref bool cancel);

    [DispId(225)]
    void PrintTemplateInstantiation([MarshalAs(UnmanagedType.IDispatch), In] object pDisp);

    [DispId(226)]
    void PrintTemplateTeardown([MarshalAs(UnmanagedType.IDispatch), In] object pDisp);

    [DispId(227)]
    void UpdatePageStatus([MarshalAs(UnmanagedType.IDispatch), In] object pDisp, [In] ref object nPage, [In] ref object fDone);

    [DispId(272)]
    void PrivacyImpactedStateChange([In] bool bImpacted);
  }

  public enum EXTENDED_NAME_FORMAT
  {
    NameUnknown = 0,
    NameFullyQualifiedDN = 1,
    NameSamCompatible = 2,
    NameDisplay = 3,
    NameUniqueId = 6,
    NameCanonical = 7,
    NameUserPrincipal = 8,
    NameCanonicalEx = 9,
    NameServicePrincipal = 10, // 0x0000000A
  }

  [Guid("618736E0-3C3D-11CF-810C-00AA00389B71")]
  [TypeLibType(4176)]
  [ComImport]
  public interface IAccessibleInternal
  {
    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5000)]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object get_accParent();

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5001)]
    int get_accChildCount();

    [DispId(-5002)]
    [TypeLibFunc(64 /*0x40*/)]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object get_accChild([MarshalAs(UnmanagedType.Struct), In] object varChild);

    [DispId(-5003)]
    [TypeLibFunc(64 /*0x40*/)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accName([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [DispId(-5004)]
    [TypeLibFunc(64 /*0x40*/)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accValue([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5005)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accDescription([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5006)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object get_accRole([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [DispId(-5007)]
    [TypeLibFunc(64 /*0x40*/)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object get_accState([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5008)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accHelp([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [DispId(-5009)]
    [TypeLibFunc(64 /*0x40*/)]
    int get_accHelpTopic([MarshalAs(UnmanagedType.BStr)] out string pszHelpFile, [MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5010)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accKeyboardShortcut([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5011)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object get_accFocus();

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5012)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object get_accSelection();

    [DispId(-5013)]
    [TypeLibFunc(64 /*0x40*/)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string get_accDefaultAction([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5014)]
    void accSelect([In] int flagsSelect, [MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [DispId(-5015)]
    [TypeLibFunc(64 /*0x40*/)]
    void accLocation(
      out int pxLeft,
      out int pyTop,
      out int pcxWidth,
      out int pcyHeight,
      [MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [DispId(-5016)]
    [TypeLibFunc(64 /*0x40*/)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object accNavigate([In] int navDir, [MarshalAs(UnmanagedType.Struct), In, Optional] object varStart);

    [DispId(-5017)]
    [TypeLibFunc(64 /*0x40*/)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object accHitTest([In] int xLeft, [In] int yTop);

    [DispId(-5018)]
    [TypeLibFunc(64 /*0x40*/)]
    void accDoDefaultAction([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild);

    [TypeLibFunc(64 /*0x40*/)]
    [DispId(-5003)]
    void set_accName([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild, [MarshalAs(UnmanagedType.BStr), In] string pszName);

    [DispId(-5004)]
    [TypeLibFunc(64 /*0x40*/)]
    void set_accValue([MarshalAs(UnmanagedType.Struct), In, Optional] object varChild, [MarshalAs(UnmanagedType.BStr), In] string pszValue);
  }

  [SuppressUnmanagedCodeSecurity]
  [Guid("00bb2762-6a77-11d0-a535-00c04fd7d062")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IAutoComplete
  {
    int Init(
      [In] HandleRef hwndEdit,
      [In] UnsafeMethods.IEnumString punkACL,
      [In] string pwszRegKeyPath,
      [In] string pwszQuickComplete);

    void Enable([In] bool fEnable);
  }

  [SuppressUnmanagedCodeSecurity]
  [Guid("EAC04BC0-3791-11d2-BB95-0060977B464C")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IAutoComplete2
  {
    int Init(
      [In] HandleRef hwndEdit,
      [In] UnsafeMethods.IEnumString punkACL,
      [In] string pwszRegKeyPath,
      [In] string pwszQuickComplete);

    void Enable([In] bool fEnable);

    int SetOptions([In] int dwFlag);

    void GetOptions([Out] IntPtr pdwFlag);
  }

  [Guid("B196B28F-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IClassFactory2
  {
    void CreateInstance([MarshalAs(UnmanagedType.Interface), In] object unused, [In] ref Guid refiid, [MarshalAs(UnmanagedType.LPArray), Out] object[] ppunk);

    void LockServer(int fLock);

    void GetLicInfo([Out] tagLICINFO licInfo);

    void RequestLicKey([MarshalAs(UnmanagedType.U4), In] int dwReserved, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrKey);

    void CreateInstanceLic(
      [MarshalAs(UnmanagedType.Interface), In] object pUnkOuter,
      [MarshalAs(UnmanagedType.Interface), In] object pUnkReserved,
      [In] ref Guid riid,
      [MarshalAs(UnmanagedType.BStr), In] string bstrKey,
      [MarshalAs(UnmanagedType.Interface)] out object ppVal);
  }

  [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IConnectionPoint
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetConnectionInterface(out Guid iid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetConnectionPointContainer(
      [MarshalAs(UnmanagedType.Interface)] ref UnsafeMethods.IConnectionPointContainer pContainer);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Advise([MarshalAs(UnmanagedType.Interface), In] object pUnkSink, ref int cookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Unadvise(int cookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumConnections(out object pEnum);
  }

  [Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IConnectionPointContainer
  {
    [return: MarshalAs(UnmanagedType.Interface)]
    object EnumConnectionPoints();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FindConnectionPoint([In] ref Guid guid, [MarshalAs(UnmanagedType.Interface)] out UnsafeMethods.IConnectionPoint ppCP);
  }

  [Guid("CB2F6722-AB3A-11d2-9C40-00C04FA30A3E")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface ICorRuntimeHost
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateLogicalThreadState();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DeleteLogicalThreadState();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SwitchInLogicalThreadState([In] ref uint pFiberCookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SwitchOutLogicalThreadState(out uint FiberCookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int LocksHeldByLogicalThread(out uint pCount);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int MapFile(IntPtr hFile, out IntPtr hMapAddress);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetConfiguration([MarshalAs(UnmanagedType.IUnknown)] out object pConfiguration);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Start();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Stop();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateDomain(string pwzFriendlyName, [MarshalAs(UnmanagedType.IUnknown)] object pIdentityArray, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDefaultDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumDomains(out IntPtr hEnum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int NextDomain(IntPtr hEnum, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CloseEnum(IntPtr hEnum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateDomainEx(
      string pwzFriendlyName,
      [MarshalAs(UnmanagedType.IUnknown)] object pSetup,
      [MarshalAs(UnmanagedType.IUnknown)] object pEvidence,
      [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateDomainSetup([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomainSetup);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateEvidence([MarshalAs(UnmanagedType.IUnknown)] out object pEvidence);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UnloadDomain([MarshalAs(UnmanagedType.IUnknown)] object pAppDomain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CurrentDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);
  }

  [Guid("00020400-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IDispatch
  {
    int GetTypeInfoCount();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.ITypeInfo GetTypeInfo([MarshalAs(UnmanagedType.U4), In] int iTInfo, [MarshalAs(UnmanagedType.U4), In] int lcid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetIDsOfNames([In] ref Guid riid, [MarshalAs(UnmanagedType.LPArray), In] string[] rgszNames, [MarshalAs(UnmanagedType.U4), In] int cNames, [MarshalAs(UnmanagedType.U4), In] int lcid, [MarshalAs(UnmanagedType.LPArray), Out] int[] rgDispId);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Invoke(
      int dispIdMember,
      [In] ref Guid riid,
      [MarshalAs(UnmanagedType.U4), In] int lcid,
      [MarshalAs(UnmanagedType.U4), In] int dwFlags,
      [In, Out] tagDISPPARAMS pDispParams,
      [MarshalAs(UnmanagedType.LPArray), Out] object[] pVarResult,
      [In, Out] tagEXCEPINFO pExcepInfo,
      [MarshalAs(UnmanagedType.LPArray), Out] IntPtr[] pArgErr);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
  [ComImport]
  public interface IEnumConnectionPoints
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int cConnections, out UnsafeMethods.IConnectionPoint pCp, out int pcFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip(int cSkip);

    void Reset();

    UnsafeMethods.IEnumConnectionPoints Clone();
  }

  [Guid("00000104-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IEnumOLEVERB
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next([MarshalAs(UnmanagedType.U4)] int celt, [Out] tagOLEVERB rgelt, [MarshalAs(UnmanagedType.LPArray), Out] int[] pceltFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip([MarshalAs(UnmanagedType.U4), In] int celt);

    void Reset();

    void Clone(out UnsafeMethods.IEnumOLEVERB ppenum);
  }

  [SuppressUnmanagedCodeSecurity]
  [Guid("00000101-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IEnumString
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next([MarshalAs(UnmanagedType.U4), In] int celt, [MarshalAs(UnmanagedType.LPArray), Out] string[] rgelt, [MarshalAs(UnmanagedType.LPArray), In, Out] int[] pceltFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip([MarshalAs(UnmanagedType.U4), In] int celt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reset();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Clone([MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.IEnumString[] ppenum);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000100-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IEnumUnknown
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next([MarshalAs(UnmanagedType.U4), In] int celt, [Out] IntPtr rgelt, IntPtr pceltFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip([MarshalAs(UnmanagedType.U4), In] int celt);

    void Reset();

    void Clone(out UnsafeMethods.IEnumUnknown ppenum);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00020404-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IEnumVariant
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next([MarshalAs(UnmanagedType.U4), In] int celt, [In, Out] IntPtr rgvar, [MarshalAs(UnmanagedType.LPArray), Out] int[] pceltFetched);

    void Skip([MarshalAs(UnmanagedType.U4), In] int celt);

    void Reset();

    void Clone([MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.IEnumVariant[] ppenum);
  }

  [Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IErrorInfo
  {
    [SuppressUnmanagedCodeSecurity]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetGUID(out Guid pguid);

    [SuppressUnmanagedCodeSecurity]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSource([MarshalAs(UnmanagedType.BStr), In, Out] ref string pBstrSource);

    [SuppressUnmanagedCodeSecurity]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDescription([MarshalAs(UnmanagedType.BStr), In, Out] ref string pBstrDescription);

    [SuppressUnmanagedCodeSecurity]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetHelpFile([MarshalAs(UnmanagedType.BStr), In, Out] ref string pBstrHelpFile);

    [SuppressUnmanagedCodeSecurity]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetHelpContext([MarshalAs(UnmanagedType.U4), In, Out] ref int pdwHelpContext);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("3127CA40-446E-11CE-8135-00AA004BB851")]
  [ComImport]
  public interface IErrorLog
  {
    void AddError([MarshalAs(UnmanagedType.LPWStr), In] string pszPropName_p0, [MarshalAs(UnmanagedType.Struct), In] tagEXCEPINFO pExcepInfo_p1);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("39088D7E-B71E-11D1-8F39-00C04FD946D0")]
  [ComImport]
  public interface IExtender
  {
    int Align { get; set; }

    bool Enabled { get; set; }

    int Height { get; set; }

    int Left { get; set; }

    bool TabStop { get; set; }

    int Top { get; set; }

    bool Visible { get; set; }

    int Width { get; set; }

    string Name { [return: MarshalAs(UnmanagedType.BStr)] get; }

    object Parent { [return: MarshalAs(UnmanagedType.Interface)] get; }

    IntPtr Hwnd { get; }

    object Container { [return: MarshalAs(UnmanagedType.Interface)] get; }

    void Move([MarshalAs(UnmanagedType.Interface), In] object left, [MarshalAs(UnmanagedType.Interface), In] object top, [MarshalAs(UnmanagedType.Interface), In] object width, [MarshalAs(UnmanagedType.Interface), In] object height);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("8A701DA0-4FEB-101B-A82E-08002B2B2337")]
  [ComImport]
  public interface IGetOleObject
  {
    [return: MarshalAs(UnmanagedType.Interface)]
    object GetOleObject(ref Guid riid);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("91733A60-3F4C-101B-A3F6-00AA0034E4E9")]
  [ComImport]
  public interface IGetVBAObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetObject([In] ref Guid riid, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.IVBFormat[] rval, int dwReserved);
  }

  [Guid("626FC520-A41E-11cf-A731-00A0C9082637")]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  public interface IHTMLDocument
  {
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetScript();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  [Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
  public interface IHTMLDocument2
  {
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetScript();

    UnsafeMethods.IHTMLElementCollection GetAll();

    UnsafeMethods.IHTMLElement GetBody();

    UnsafeMethods.IHTMLElement GetActiveElement();

    UnsafeMethods.IHTMLElementCollection GetImages();

    UnsafeMethods.IHTMLElementCollection GetApplets();

    UnsafeMethods.IHTMLElementCollection GetLinks();

    UnsafeMethods.IHTMLElementCollection GetForms();

    UnsafeMethods.IHTMLElementCollection GetAnchors();

    void SetTitle(string p);

    string GetTitle();

    UnsafeMethods.IHTMLElementCollection GetScripts();

    void SetDesignMode(string p);

    string GetDesignMode();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetSelection();

    string GetReadyState();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetFrames();

    UnsafeMethods.IHTMLElementCollection GetEmbeds();

    UnsafeMethods.IHTMLElementCollection GetPlugins();

    void SetAlinkColor(object c);

    object GetAlinkColor();

    void SetBgColor(object c);

    object GetBgColor();

    void SetFgColor(object c);

    object GetFgColor();

    void SetLinkColor(object c);

    object GetLinkColor();

    void SetVlinkColor(object c);

    object GetVlinkColor();

    string GetReferrer();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLLocation GetLocation();

    string GetLastModified();

    void SetUrl(string p);

    string GetUrl();

    void SetDomain(string p);

    string GetDomain();

    void SetCookie(string p);

    string GetCookie();

    void SetExpando(bool p);

    bool GetExpando();

    void SetCharset(string p);

    string GetCharset();

    void SetDefaultCharset(string p);

    string GetDefaultCharset();

    string GetMimeType();

    string GetFileSize();

    string GetFileCreatedDate();

    string GetFileModifiedDate();

    string GetFileUpdatedDate();

    string GetSecurity();

    string GetProtocol();

    string GetNameProp();

    int Write([MarshalAs(UnmanagedType.SafeArray), In] object[] psarray);

    int WriteLine([MarshalAs(UnmanagedType.SafeArray), In] object[] psarray);

    [return: MarshalAs(UnmanagedType.Interface)]
    object Open(string mimeExtension, object name, object features, object replace);

    void Close();

    void Clear();

    bool QueryCommandSupported(string cmdID);

    bool QueryCommandEnabled(string cmdID);

    bool QueryCommandState(string cmdID);

    bool QueryCommandIndeterm(string cmdID);

    string QueryCommandText(string cmdID);

    object QueryCommandValue(string cmdID);

    bool ExecCommand(string cmdID, bool showUI, object value);

    bool ExecCommandShowHelp(string cmdID);

    UnsafeMethods.IHTMLElement CreateElement(string eTag);

    void SetOnhelp(object p);

    object GetOnhelp();

    void SetOnclick(object p);

    object GetOnclick();

    void SetOndblclick(object p);

    object GetOndblclick();

    void SetOnkeyup(object p);

    object GetOnkeyup();

    void SetOnkeydown(object p);

    object GetOnkeydown();

    void SetOnkeypress(object p);

    object GetOnkeypress();

    void SetOnmouseup(object p);

    object GetOnmouseup();

    void SetOnmousedown(object p);

    object GetOnmousedown();

    void SetOnmousemove(object p);

    object GetOnmousemove();

    void SetOnmouseout(object p);

    object GetOnmouseout();

    void SetOnmouseover(object p);

    object GetOnmouseover();

    void SetOnreadystatechange(object p);

    object GetOnreadystatechange();

    void SetOnafterupdate(object p);

    object GetOnafterupdate();

    void SetOnrowexit(object p);

    object GetOnrowexit();

    void SetOnrowenter(object p);

    object GetOnrowenter();

    void SetOndragstart(object p);

    object GetOndragstart();

    void SetOnselectstart(object p);

    object GetOnselectstart();

    UnsafeMethods.IHTMLElement ElementFromPoint(int x, int y);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLWindow2 GetParentWindow();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetStyleSheets();

    void SetOnbeforeupdate(object p);

    object GetOnbeforeupdate();

    void SetOnerrorupdate(object p);

    object GetOnerrorupdate();

    string toString();

    [return: MarshalAs(UnmanagedType.Interface)]
    object CreateStyleSheet(string bstrHref, int lIndex);
  }

  [Guid("3050F485-98B5-11CF-BB82-00AA00BDCE0B")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  public interface IHTMLDocument3
  {
    void ReleaseCapture();

    void Recalc([In] bool fForce);

    object CreateTextNode([In] string text);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement GetDocumentElement();

    string GetUniqueID();

    bool AttachEvent([In] string ev, [MarshalAs(UnmanagedType.IDispatch), In] object pdisp);

    void DetachEvent([In] string ev, [MarshalAs(UnmanagedType.IDispatch), In] object pdisp);

    void SetOnrowsdelete([In] object p);

    object GetOnrowsdelete();

    void SetOnrowsinserted([In] object p);

    object GetOnrowsinserted();

    void SetOncellchange([In] object p);

    object GetOncellchange();

    void SetOndatasetchanged([In] object p);

    object GetOndatasetchanged();

    void SetOndataavailable([In] object p);

    object GetOndataavailable();

    void SetOndatasetcomplete([In] object p);

    object GetOndatasetcomplete();

    void SetOnpropertychange([In] object p);

    object GetOnpropertychange();

    void SetDir([In] string p);

    string GetDir();

    void SetOncontextmenu([In] object p);

    object GetOncontextmenu();

    void SetOnstop([In] object p);

    object GetOnstop();

    object CreateDocumentFragment();

    object GetParentDocument();

    void SetEnableDownload([In] bool p);

    bool GetEnableDownload();

    void SetBaseUrl([In] string p);

    string GetBaseUrl();

    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetChildNodes();

    void SetInheritStyleSheets([In] bool p);

    bool GetInheritStyleSheets();

    void SetOnbeforeeditfocus([In] object p);

    object GetOnbeforeeditfocus();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElementCollection GetElementsByName([In] string v);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement GetElementById([In] string v);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElementCollection GetElementsByTagName([In] string v);
  }

  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  [Guid("3050F69A-98B5-11CF-BB82-00AA00BDCE0B")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  public interface IHTMLDocument4
  {
    void Focus();

    bool HasFocus();

    void SetOnselectionchange(object p);

    object GetOnselectionchange();

    object GetNamespaces();

    object createDocumentFromUrl(string bstrUrl, string bstrOptions);

    void SetMedia(string bstrMedia);

    string GetMedia();

    object CreateEventObject([In, Optional] ref object eventObject);

    bool FireEvent(string eventName);

    object CreateRenderStyle(string bstr);

    void SetOncontrolselect(object p);

    object GetOncontrolselect();

    string GetURLUnencoded();
  }

  [Guid("3050f5da-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  public interface IHTMLDOMNode
  {
    long GetNodeType();

    UnsafeMethods.IHTMLDOMNode GetParentNode();

    bool HasChildNodes();

    object GetChildNodes();

    object GetAttributes();

    UnsafeMethods.IHTMLDOMNode InsertBefore(UnsafeMethods.IHTMLDOMNode newChild, object refChild);

    UnsafeMethods.IHTMLDOMNode RemoveChild(UnsafeMethods.IHTMLDOMNode oldChild);

    UnsafeMethods.IHTMLDOMNode ReplaceChild(
      UnsafeMethods.IHTMLDOMNode newChild,
      UnsafeMethods.IHTMLDOMNode oldChild);

    UnsafeMethods.IHTMLDOMNode CloneNode(bool fDeep);

    UnsafeMethods.IHTMLDOMNode RemoveNode(bool fDeep);

    UnsafeMethods.IHTMLDOMNode SwapNode(UnsafeMethods.IHTMLDOMNode otherNode);

    UnsafeMethods.IHTMLDOMNode ReplaceNode(UnsafeMethods.IHTMLDOMNode replacement);

    UnsafeMethods.IHTMLDOMNode AppendChild(UnsafeMethods.IHTMLDOMNode newChild);

    string NodeName();

    void SetNodeValue(object v);

    object GetNodeValue();

    UnsafeMethods.IHTMLDOMNode FirstChild();

    UnsafeMethods.IHTMLDOMNode LastChild();

    UnsafeMethods.IHTMLDOMNode PreviousSibling();

    UnsafeMethods.IHTMLDOMNode NextSibling();
  }

  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
  public interface IHTMLElement
  {
    void SetAttribute(string attributeName, object attributeValue, int lFlags);

    object GetAttribute(string attributeName, int lFlags);

    bool RemoveAttribute(string strAttributeName, int lFlags);

    void SetClassName(string p);

    string GetClassName();

    void SetId(string p);

    string GetId();

    string GetTagName();

    UnsafeMethods.IHTMLElement GetParentElement();

    UnsafeMethods.IHTMLStyle GetStyle();

    void SetOnhelp(object p);

    object GetOnhelp();

    void SetOnclick(object p);

    object GetOnclick();

    void SetOndblclick(object p);

    object GetOndblclick();

    void SetOnkeydown(object p);

    object GetOnkeydown();

    void SetOnkeyup(object p);

    object GetOnkeyup();

    void SetOnkeypress(object p);

    object GetOnkeypress();

    void SetOnmouseout(object p);

    object GetOnmouseout();

    void SetOnmouseover(object p);

    object GetOnmouseover();

    void SetOnmousemove(object p);

    object GetOnmousemove();

    void SetOnmousedown(object p);

    object GetOnmousedown();

    void SetOnmouseup(object p);

    object GetOnmouseup();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLDocument2 GetDocument();

    void SetTitle(string p);

    string GetTitle();

    void SetLanguage(string p);

    string GetLanguage();

    void SetOnselectstart(object p);

    object GetOnselectstart();

    void ScrollIntoView(object varargStart);

    bool Contains(UnsafeMethods.IHTMLElement pChild);

    int GetSourceIndex();

    object GetRecordNumber();

    void SetLang(string p);

    string GetLang();

    int GetOffsetLeft();

    int GetOffsetTop();

    int GetOffsetWidth();

    int GetOffsetHeight();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement GetOffsetParent();

    void SetInnerHTML(string p);

    string GetInnerHTML();

    void SetInnerText(string p);

    string GetInnerText();

    void SetOuterHTML(string p);

    string GetOuterHTML();

    void SetOuterText(string p);

    string GetOuterText();

    void InsertAdjacentHTML(string where, string html);

    void InsertAdjacentText(string where, string text);

    UnsafeMethods.IHTMLElement GetParentTextEdit();

    bool GetIsTextEdit();

    void Click();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetFilters();

    void SetOndragstart(object p);

    object GetOndragstart();

    string toString();

    void SetOnbeforeupdate(object p);

    object GetOnbeforeupdate();

    void SetOnafterupdate(object p);

    object GetOnafterupdate();

    void SetOnerrorupdate(object p);

    object GetOnerrorupdate();

    void SetOnrowexit(object p);

    object GetOnrowexit();

    void SetOnrowenter(object p);

    object GetOnrowenter();

    void SetOndatasetchanged(object p);

    object GetOndatasetchanged();

    void SetOndataavailable(object p);

    object GetOndataavailable();

    void SetOndatasetcomplete(object p);

    object GetOndatasetcomplete();

    void SetOnfilterchange(object p);

    object GetOnfilterchange();

    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetChildren();

    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetAll();
  }

  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [Guid("3050f434-98b5-11cf-bb82-00aa00bdce0b")]
  public interface IHTMLElement2
  {
    string ScopeName();

    void SetCapture(bool containerCapture);

    void ReleaseCapture();

    void SetOnLoseCapture(object v);

    object GetOnLoseCapture();

    string GetComponentFromPoint(int x, int y);

    void DoScroll(object component);

    void SetOnScroll(object v);

    object GetOnScroll();

    void SetOnDrag(object v);

    object GetOnDrag();

    void SetOnDragEnd(object v);

    object GetOnDragEnd();

    void SetOnDragEnter(object v);

    object GetOnDragEnter();

    void SetOnDragOver(object v);

    object GetOnDragOver();

    void SetOnDragleave(object v);

    object GetOnDragLeave();

    void SetOnDrop(object v);

    object GetOnDrop();

    void SetOnBeforeCut(object v);

    object GetOnBeforeCut();

    void SetOnCut(object v);

    object GetOnCut();

    void SetOnBeforeCopy(object v);

    object GetOnBeforeCopy();

    void SetOnCopy(object v);

    object GetOnCopy(object p);

    void SetOnBeforePaste(object v);

    object GetOnBeforePaste(object p);

    void SetOnPaste(object v);

    object GetOnPaste(object p);

    object GetCurrentStyle();

    void SetOnPropertyChange(object v);

    object GetOnPropertyChange(object p);

    object GetClientRects();

    object GetBoundingClientRect();

    void SetExpression(string propName, string expression, string language);

    object GetExpression(string propName);

    bool RemoveExpression(string propName);

    void SetTabIndex(int v);

    short GetTabIndex();

    void Focus();

    void SetAccessKey(string v);

    string GetAccessKey();

    void SetOnBlur(object v);

    object GetOnBlur();

    void SetOnFocus(object v);

    object GetOnFocus();

    void SetOnResize(object v);

    object GetOnResize();

    void Blur();

    void AddFilter(object pUnk);

    void RemoveFilter(object pUnk);

    int ClientHeight();

    int ClientWidth();

    int ClientTop();

    int ClientLeft();

    bool AttachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch), In] object pdisp);

    void DetachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch), In] object pdisp);

    object ReadyState();

    void SetOnReadyStateChange(object v);

    object GetOnReadyStateChange();

    void SetOnRowsDelete(object v);

    object GetOnRowsDelete();

    void SetOnRowsInserted(object v);

    object GetOnRowsInserted();

    void SetOnCellChange(object v);

    object GetOnCellChange();

    void SetDir(string v);

    string GetDir();

    object CreateControlRange();

    int GetScrollHeight();

    int GetScrollWidth();

    void SetScrollTop(int v);

    int GetScrollTop();

    void SetScrollLeft(int v);

    int GetScrollLeft();

    void ClearAttributes();

    void MergeAttributes(object mergeThis);

    void SetOnContextMenu(object v);

    object GetOnContextMenu();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement InsertAdjacentElement(
      string where,
      [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IHTMLElement insertedElement);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement applyElement([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IHTMLElement apply, string where);

    string GetAdjacentText(string where);

    string ReplaceAdjacentText(string where, string newText);

    bool CanHaveChildren();

    int AddBehavior(string url, ref object oFactory);

    bool RemoveBehavior(int cookie);

    object GetRuntimeStyle();

    object GetBehaviorUrns();

    void SetTagUrn(string v);

    string GetTagUrn();

    void SetOnBeforeEditFocus(object v);

    object GetOnBeforeEditFocus();

    int GetReadyStateValue();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElementCollection GetElementsByTagName(string v);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  [Guid("3050f673-98b5-11cf-bb82-00aa00bdce0b")]
  public interface IHTMLElement3
  {
    void MergeAttributes(object mergeThis, object pvarFlags);

    bool IsMultiLine();

    bool CanHaveHTML();

    void SetOnLayoutComplete(object v);

    object GetOnLayoutComplete();

    void SetOnPage(object v);

    object GetOnPage();

    void SetInflateBlock(bool v);

    bool GetInflateBlock();

    void SetOnBeforeDeactivate(object v);

    object GetOnBeforeDeactivate();

    void SetActive();

    void SetContentEditable(string v);

    string GetContentEditable();

    bool IsContentEditable();

    void SetHideFocus(bool v);

    bool GetHideFocus();

    void SetDisabled(bool v);

    bool GetDisabled();

    bool IsDisabled();

    void SetOnMove(object v);

    object GetOnMove();

    void SetOnControlSelect(object v);

    object GetOnControlSelect();

    bool FireEvent(string bstrEventName, object pvarEventObject);

    void SetOnResizeStart(object v);

    object GetOnResizeStart();

    void SetOnResizeEnd(object v);

    object GetOnResizeEnd();

    void SetOnMoveStart(object v);

    object GetOnMoveStart();

    void SetOnMoveEnd(object v);

    object GetOnMoveEnd();

    void SetOnMouseEnter(object v);

    object GetOnMouseEnter();

    void SetOnMouseLeave(object v);

    object GetOnMouseLeave();

    void SetOnActivate(object v);

    object GetOnActivate();

    void SetOnDeactivate(object v);

    object GetOnDeactivate();

    bool DragDrop();

    int GlyphMode();
  }

  [ComVisible(true)]
  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B")]
  public interface IHTMLElementCollection
  {
    string toString();

    void SetLength(int p);

    int GetLength();

    [return: MarshalAs(UnmanagedType.Interface)]
    object Get_newEnum();

    [return: MarshalAs(UnmanagedType.IDispatch)]
    object Item(object idOrName, object index);

    [return: MarshalAs(UnmanagedType.Interface)]
    object Tags(object tagName);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  [Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B")]
  public interface IHTMLEventObj
  {
    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement GetSrcElement();

    bool GetAltKey();

    bool GetCtrlKey();

    bool GetShiftKey();

    void SetReturnValue(bool p);

    bool GetReturnValue();

    void SetCancelBubble(bool p);

    bool GetCancelBubble();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement GetFromElement();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLElement GetToElement();

    void SetKeyCode([In] int p);

    int GetKeyCode();

    int GetButton();

    string GetEventType();

    string GetQualifier();

    int GetReason();

    int GetX();

    int GetY();

    int GetClientX();

    int GetClientY();

    int GetOffsetX();

    int GetOffsetY();

    int GetScreenX();

    int GetScreenY();

    object GetSrcFilter();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  [Guid("3050f48B-98b5-11cf-bb82-00aa00bdce0b")]
  public interface IHTMLEventObj2
  {
    void SetAttribute(string attributeName, object attributeValue, int lFlags);

    object GetAttribute(string attributeName, int lFlags);

    bool RemoveAttribute(string attributeName, int lFlags);

    void SetPropertyName(string name);

    string GetPropertyName();

    void SetBookmarks(ref object bm);

    object GetBookmarks();

    void SetRecordset(ref object rs);

    object GetRecordset();

    void SetDataFld(string df);

    string GetDataFld();

    void SetBoundElements(ref object be);

    object GetBoundElements();

    void SetRepeat(bool r);

    bool GetRepeat();

    void SetSrcUrn(string urn);

    string GetSrcUrn();

    void SetSrcElement(ref object se);

    object GetSrcElement();

    void SetAltKey(bool alt);

    bool GetAltKey();

    void SetCtrlKey(bool ctrl);

    bool GetCtrlKey();

    void SetShiftKey(bool shift);

    bool GetShiftKey();

    void SetFromElement(ref object element);

    object GetFromElement();

    void SetToElement(ref object element);

    object GetToElement();

    void SetButton(int b);

    int GetButton();

    void SetType(string type);

    string GetType();

    void SetQualifier(string q);

    string GetQualifier();

    void SetReason(int r);

    int GetReason();

    void SetX(int x);

    int GetX();

    void SetY(int y);

    int GetY();

    void SetClientX(int x);

    int GetClientX();

    void SetClientY(int y);

    int GetClientY();

    void SetOffsetX(int x);

    int GetOffsetX();

    void SetOffsetY(int y);

    int GetOffsetY();

    void SetScreenX(int x);

    int GetScreenX();

    void SetScreenY(int y);

    int GetScreenY();

    void SetSrcFilter(ref object filter);

    object GetSrcFilter();

    object GetDataTransfer();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  [Guid("3050f814-98b5-11cf-bb82-00aa00bdce0b")]
  public interface IHTMLEventObj4
  {
    int GetWheelDelta();
  }

  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [Guid("332C4426-26CB-11D0-B483-00C04FD90119")]
  public interface IHTMLFramesCollection2
  {
    object Item(ref object idOrName);

    int GetLength();
  }

  [ComVisible(true)]
  [Guid("163BB1E0-6E00-11CF-837A-48DC04C10000")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  public interface IHTMLLocation
  {
    void SetHref([In] string p);

    string GetHref();

    void SetProtocol([In] string p);

    string GetProtocol();

    void SetHost([In] string p);

    string GetHost();

    void SetHostname([In] string p);

    string GetHostname();

    void SetPort([In] string p);

    string GetPort();

    void SetPathname([In] string p);

    string GetPathname();

    void SetSearch([In] string p);

    string GetSearch();

    void SetHash([In] string p);

    string GetHash();

    void Reload([In] bool flag);

    void Replace([In] string bstr);

    void Assign([In] string bstr);
  }

  [SuppressUnmanagedCodeSecurity]
  [Guid("3050f666-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  public interface IHTMLPopup
  {
    void show(int x, int y, int w, int h, ref object element);

    void hide();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLDocument GetDocument();

    bool IsOpen();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("3050f35c-98b5-11cf-bb82-00aa00bdce0b")]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  public interface IHTMLScreen
  {
    int GetColorDepth();

    void SetBufferDepth(int d);

    int GetBufferDepth();

    int GetWidth();

    int GetHeight();

    void SetUpdateInterval(int i);

    int GetUpdateInterval();

    int GetAvailHeight();

    int GetAvailWidth();

    bool GetFontSmoothingEnabled();
  }

  [Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [SuppressUnmanagedCodeSecurity]
  public interface IHTMLStyle
  {
    void SetFontFamily(string p);

    string GetFontFamily();

    void SetFontStyle(string p);

    string GetFontStyle();

    void SetFontObject(string p);

    string GetFontObject();

    void SetFontWeight(string p);

    string GetFontWeight();

    void SetFontSize(object p);

    object GetFontSize();

    void SetFont(string p);

    string GetFont();

    void SetColor(object p);

    object GetColor();

    void SetBackground(string p);

    string GetBackground();

    void SetBackgroundColor(object p);

    object GetBackgroundColor();

    void SetBackgroundImage(string p);

    string GetBackgroundImage();

    void SetBackgroundRepeat(string p);

    string GetBackgroundRepeat();

    void SetBackgroundAttachment(string p);

    string GetBackgroundAttachment();

    void SetBackgroundPosition(string p);

    string GetBackgroundPosition();

    void SetBackgroundPositionX(object p);

    object GetBackgroundPositionX();

    void SetBackgroundPositionY(object p);

    object GetBackgroundPositionY();

    void SetWordSpacing(object p);

    object GetWordSpacing();

    void SetLetterSpacing(object p);

    object GetLetterSpacing();

    void SetTextDecoration(string p);

    string GetTextDecoration();

    void SetTextDecorationNone(bool p);

    bool GetTextDecorationNone();

    void SetTextDecorationUnderline(bool p);

    bool GetTextDecorationUnderline();

    void SetTextDecorationOverline(bool p);

    bool GetTextDecorationOverline();

    void SetTextDecorationLineThrough(bool p);

    bool GetTextDecorationLineThrough();

    void SetTextDecorationBlink(bool p);

    bool GetTextDecorationBlink();

    void SetVerticalAlign(object p);

    object GetVerticalAlign();

    void SetTextTransform(string p);

    string GetTextTransform();

    void SetTextAlign(string p);

    string GetTextAlign();

    void SetTextIndent(object p);

    object GetTextIndent();

    void SetLineHeight(object p);

    object GetLineHeight();

    void SetMarginTop(object p);

    object GetMarginTop();

    void SetMarginRight(object p);

    object GetMarginRight();

    void SetMarginBottom(object p);

    object GetMarginBottom();

    void SetMarginLeft(object p);

    object GetMarginLeft();

    void SetMargin(string p);

    string GetMargin();

    void SetPaddingTop(object p);

    object GetPaddingTop();

    void SetPaddingRight(object p);

    object GetPaddingRight();

    void SetPaddingBottom(object p);

    object GetPaddingBottom();

    void SetPaddingLeft(object p);

    object GetPaddingLeft();

    void SetPadding(string p);

    string GetPadding();

    void SetBorder(string p);

    string GetBorder();

    void SetBorderTop(string p);

    string GetBorderTop();

    void SetBorderRight(string p);

    string GetBorderRight();

    void SetBorderBottom(string p);

    string GetBorderBottom();

    void SetBorderLeft(string p);

    string GetBorderLeft();

    void SetBorderColor(string p);

    string GetBorderColor();

    void SetBorderTopColor(object p);

    object GetBorderTopColor();

    void SetBorderRightColor(object p);

    object GetBorderRightColor();

    void SetBorderBottomColor(object p);

    object GetBorderBottomColor();

    void SetBorderLeftColor(object p);

    object GetBorderLeftColor();

    void SetBorderWidth(string p);

    string GetBorderWidth();

    void SetBorderTopWidth(object p);

    object GetBorderTopWidth();

    void SetBorderRightWidth(object p);

    object GetBorderRightWidth();

    void SetBorderBottomWidth(object p);

    object GetBorderBottomWidth();

    void SetBorderLeftWidth(object p);

    object GetBorderLeftWidth();

    void SetBorderStyle(string p);

    string GetBorderStyle();

    void SetBorderTopStyle(string p);

    string GetBorderTopStyle();

    void SetBorderRightStyle(string p);

    string GetBorderRightStyle();

    void SetBorderBottomStyle(string p);

    string GetBorderBottomStyle();

    void SetBorderLeftStyle(string p);

    string GetBorderLeftStyle();

    void SetWidth(object p);

    object GetWidth();

    void SetHeight(object p);

    object GetHeight();

    void SetStyleFloat(string p);

    string GetStyleFloat();

    void SetClear(string p);

    string GetClear();

    void SetDisplay(string p);

    string GetDisplay();

    void SetVisibility(string p);

    string GetVisibility();

    void SetListStyleType(string p);

    string GetListStyleType();

    void SetListStylePosition(string p);

    string GetListStylePosition();

    void SetListStyleImage(string p);

    string GetListStyleImage();

    void SetListStyle(string p);

    string GetListStyle();

    void SetWhiteSpace(string p);

    string GetWhiteSpace();

    void SetTop(object p);

    object GetTop();

    void SetLeft(object p);

    object GetLeft();

    string GetPosition();

    void SetZIndex(object p);

    object GetZIndex();

    void SetOverflow(string p);

    string GetOverflow();

    void SetPageBreakBefore(string p);

    string GetPageBreakBefore();

    void SetPageBreakAfter(string p);

    string GetPageBreakAfter();

    void SetCssText(string p);

    string GetCssText();

    void SetPixelTop(int p);

    int GetPixelTop();

    void SetPixelLeft(int p);

    int GetPixelLeft();

    void SetPixelWidth(int p);

    int GetPixelWidth();

    void SetPixelHeight(int p);

    int GetPixelHeight();

    void SetPosTop(float p);

    float GetPosTop();

    void SetPosLeft(float p);

    float GetPosLeft();

    void SetPosWidth(float p);

    float GetPosWidth();

    void SetPosHeight(float p);

    float GetPosHeight();

    void SetCursor(string p);

    string GetCursor();

    void SetClip(string p);

    string GetClip();

    void SetFilter(string p);

    string GetFilter();

    void SetAttribute(string strAttributeName, object AttributeValue, int lFlags);

    object GetAttribute(string strAttributeName, int lFlags);

    bool RemoveAttribute(string strAttributeName, int lFlags);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("332C4427-26CB-11D0-B483-00C04FD90119")]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  public interface IHTMLWindow2
  {
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object Item([In] ref object pvarIndex);

    int GetLength();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLFramesCollection2 GetFrames();

    void SetDefaultStatus([In] string p);

    string GetDefaultStatus();

    void SetStatus([In] string p);

    string GetStatus();

    int SetTimeout([In] string expression, [In] int msec, [In] ref object language);

    void ClearTimeout([In] int timerID);

    void Alert([In] string message);

    bool Confirm([In] string message);

    [return: MarshalAs(UnmanagedType.Struct)]
    object Prompt([In] string message, [In] string defstr);

    object GetImage();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLLocation GetLocation();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IOmHistory GetHistory();

    void Close();

    void SetOpener([In] object p);

    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetOpener();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IOmNavigator GetNavigator();

    void SetName([In] string p);

    string GetName();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLWindow2 GetParent();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLWindow2 Open([In] string URL, [In] string name, [In] string features, [In] bool replace);

    object GetSelf();

    object GetTop();

    object GetWindow();

    void Navigate([In] string URL);

    void SetOnfocus([In] object p);

    object GetOnfocus();

    void SetOnblur([In] object p);

    object GetOnblur();

    void SetOnload([In] object p);

    object GetOnload();

    void SetOnbeforeunload(object p);

    object GetOnbeforeunload();

    void SetOnunload([In] object p);

    object GetOnunload();

    void SetOnhelp(object p);

    object GetOnhelp();

    void SetOnerror([In] object p);

    object GetOnerror();

    void SetOnresize([In] object p);

    object GetOnresize();

    void SetOnscroll([In] object p);

    object GetOnscroll();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLDocument2 GetDocument();

    [return: MarshalAs(UnmanagedType.Interface)]
    Intermech.Controls.OleContainer.IHTMLEventObj GetEvent();

    object Get_newEnum();

    object ShowModalDialog([In] string dialog, [In] ref object varArgIn, [In] ref object varOptions);

    void ShowHelp([In] string helpURL, [In] object helpArg, [In] string features);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IHTMLScreen GetScreen();

    object GetOption();

    void Focus();

    bool GetClosed();

    void Blur();

    void Scroll([In] int x, [In] int y);

    object GetClientInformation();

    int SetInterval([In] string expression, [In] int msec, [In] ref object language);

    void ClearInterval([In] int timerID);

    void SetOffscreenBuffering([In] object p);

    object GetOffscreenBuffering();

    [return: MarshalAs(UnmanagedType.Struct)]
    object ExecScript([In] string code, [In] string language);

    string toString();

    void ScrollBy([In] int x, [In] int y);

    void ScrollTo([In] int x, [In] int y);

    void MoveTo([In] int x, [In] int y);

    void MoveBy([In] int x, [In] int y);

    void ResizeTo([In] int x, [In] int y);

    void ResizeBy([In] int x, [In] int y);

    object GetExternal();
  }

  [Guid("3050f4ae-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  public interface IHTMLWindow3
  {
    int GetScreenLeft();

    int GetScreenTop();

    bool AttachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch), In] object pdisp);

    void DetachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch), In] object pdisp);

    int SetTimeout([In] ref object expression, int msec, [In] ref object language);

    int SetInterval([In] ref object expression, int msec, [In] ref object language);

    void Print();

    void SetBeforePrint(object o);

    object GetBeforePrint();

    void SetAfterPrint(object o);

    object GetAfterPrint();

    object GetClipboardData();

    object ShowModelessDialog(string url, object varArgIn, object options);
  }

  [SuppressUnmanagedCodeSecurity]
  [Guid("3050f6cf-98b5-11cf-bb82-00aa00bdce0b")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  public interface IHTMLWindow4
  {
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object CreatePopup([In] ref object reserved);

    [return: MarshalAs(UnmanagedType.Interface)]
    object frameElement();
  }

  [ComVisible(true)]
  [Guid("79eac9ee-baf9-11ce-8c82-00aa004ba90b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IInternetSecurityManager
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetSecuritySite();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSecuritySite();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int MapUrlToZone();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSecurityId();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ProcessUrlAction(
      string url,
      int action,
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3), Out] byte[] policy,
      int cbPolicy,
      ref byte context,
      int cbContext,
      int flags,
      int reserved);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryCustomPolicy();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetZoneMapping();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetZoneMappings();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("0000000A-0000-0000-C000-000000000046")]
  [ComImport]
  public interface ILockBytes
  {
    void ReadAt([MarshalAs(UnmanagedType.U8), In] long ulOffset, [Out] IntPtr pv, [MarshalAs(UnmanagedType.U4), In] int cb, [MarshalAs(UnmanagedType.LPArray), Out] int[] pcbRead);

    void WriteAt([MarshalAs(UnmanagedType.U8), In] long ulOffset, IntPtr pv, [MarshalAs(UnmanagedType.U4), In] int cb, [MarshalAs(UnmanagedType.LPArray), Out] int[] pcbWritten);

    void Flush();

    void SetSize([MarshalAs(UnmanagedType.U8), In] long cb);

    void LockRegion([MarshalAs(UnmanagedType.U8), In] long libOffset, [MarshalAs(UnmanagedType.U8), In] long cb, [MarshalAs(UnmanagedType.U4), In] int dwLockType);

    void UnlockRegion([MarshalAs(UnmanagedType.U8), In] long libOffset, [MarshalAs(UnmanagedType.U8), In] long cb, [MarshalAs(UnmanagedType.U4), In] int dwLockType);

    void Stat([Out] STATSTG pstatstg, [MarshalAs(UnmanagedType.U4), In] int grfStatFlag);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [SuppressUnmanagedCodeSecurity]
  [Guid("00000002-0000-0000-c000-000000000046")]
  [ComImport]
  public interface IMalloc
  {
    IntPtr Alloc(int cb);

    void Free(IntPtr pv);

    IntPtr Realloc(IntPtr pv, int cb);

    int GetSize(IntPtr pv);

    int DidAlloc(IntPtr pv);

    void HeapMinimize();
  }

  [Guid("00000003-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMarshal
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetUnmarshalClass(
      ref Guid riid,
      [MarshalAs(UnmanagedType.Interface)] object pv,
      int dwDestContext,
      IntPtr pvDestContext,
      int mshlflags,
      out Guid pCid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMarshalSizeMax(
      ref Guid riid,
      [MarshalAs(UnmanagedType.Interface)] object pv,
      int dwDestContext,
      IntPtr pvDestContext,
      int mshlflags,
      out int pSize);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int MarshalInterface(
      [MarshalAs(UnmanagedType.Interface)] object pStm,
      ref Guid riid,
      [MarshalAs(UnmanagedType.Interface)] object pv,
      int dwDestContext,
      IntPtr pvDestContext,
      int mshlflags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UnmarshalInterface([MarshalAs(UnmanagedType.Interface)] object pStm, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ReleaseMarshalData([MarshalAs(UnmanagedType.Interface)] object pStm);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DisconnectObject(int dwReserved);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("000C0600-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IMsoComponent
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FDebugMessage(IntPtr hInst, int msg, IntPtr wParam, IntPtr lParam);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FPreTranslateMessage(ref MSG msg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void OnEnterState(int uStateID, bool fEnter);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void OnAppActivate(bool fActive, int dwOtherThreadID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void OnLoseActivation();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void OnActivationChange(
      UnsafeMethods.IMsoComponent component,
      bool fSameComponent,
      int pcrinfo,
      bool fHostIsActivating,
      int pchostinfo,
      int dwReserved);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FDoIdle(int grfidlef);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FContinueMessageLoop(int uReason, int pvLoopData, ref MSG pMsgPeeked);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FQueryTerminate(bool fPromptUser);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void Terminate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    IntPtr HwndGetWindow(int dwWhich, int dwReserved);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("000C0601-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IMsoComponentManager
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryService(ref Guid guidService, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FDebugMessage(IntPtr hInst, int msg, IntPtr wParam, IntPtr lParam);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FRegisterComponent(
      UnsafeMethods.IMsoComponent component,
      MSOCRINFOSTRUCT pcrinfo,
      out int dwComponentID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FRevokeComponent(int dwComponentID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FUpdateComponentRegistration(int dwComponentID, MSOCRINFOSTRUCT pcrinfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FOnComponentActivate(int dwComponentID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FSetTrackingComponent(int dwComponentID, bool fTrack);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void OnComponentEnterState(
      int dwComponentID,
      int uStateID,
      int uContext,
      int cpicmExclude,
      int rgpicmExclude,
      int dwReserved);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FOnComponentExitState(
      int dwComponentID,
      int uStateID,
      int uContext,
      int cpicmExclude,
      int rgpicmExclude);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FInState(int uStateID, IntPtr pvoid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FContinueIdle();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FPushMessageLoop(int dwComponentID, int uReason, int pvLoopData);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FCreateSubComponentManager(
      [MarshalAs(UnmanagedType.Interface)] object punkOuter,
      [MarshalAs(UnmanagedType.Interface)] object punkServProv,
      ref Guid riid,
      out IntPtr ppvObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FGetParentComponentManager(out UnsafeMethods.IMsoComponentManager ppicm);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool FGetActiveComponent(
      int dwgac,
      [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.IMsoComponent[] ppic,
      MSOCRINFOSTRUCT pcrinfo,
      int dwReserved);
  }

  [Guid("0000011e-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleCache
  {
    int Cache(ref FORMATETC pformatetc, int advf);

    void Uncache(int dwConnection);

    object EnumCache();

    void InitCache(IDataObject pDataObject);

    void SetData(ref FORMATETC pformatetc, ref STGMEDIUM pmedium, bool fRelease);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000118-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleClientSite
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SaveObject();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMoniker([MarshalAs(UnmanagedType.U4), In] int dwAssign, [MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetContainer(out UnsafeMethods.IOleContainer container);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ShowObject();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnShowWindow(int fShow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RequestNewObjectLayout();
  }

  [Guid("0000011B-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleContainer
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ParseDisplayName([MarshalAs(UnmanagedType.Interface), In] object pbc, [MarshalAs(UnmanagedType.BStr), In] string pszDisplayName, [MarshalAs(UnmanagedType.LPArray), Out] int[] pchEaten, [MarshalAs(UnmanagedType.LPArray), Out] object[] ppmkOut);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumObjects([MarshalAs(UnmanagedType.U4), In] int grfFlags, out UnsafeMethods.IEnumUnknown ppenum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int LockContainer(bool fLock);
  }

  [Guid("B196B288-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [SuppressUnmanagedCodeSecurity]
  [ComImport]
  public interface IOleControl
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetControlInfo([Out] tagCONTROLINFO pCI);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnMnemonic([In] ref MSG pMsg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnAmbientPropertyChange(int dispID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FreezeEvents(int bFreeze);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("B196B289-BAB4-101A-B69C-00AA00341D07")]
  [ComImport]
  public interface IOleControlSite
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnControlInfoChanged();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int LockInPlaceActive(int fLock);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetExtendedControl([MarshalAs(UnmanagedType.IDispatch)] out object ppDisp);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int TransformCoords([In, Out] _POINTL pPtlHimetric, [In, Out] tagPOINTF pPtfContainer, [MarshalAs(UnmanagedType.U4), In] int dwFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int TranslateAccelerator([In] ref MSG pMsg, [MarshalAs(UnmanagedType.U4), In] int grfModifiers);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnFocus(int fGotFocus);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ShowPropertyFrame();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("b722bcc5-4e68-101b-a2bc-00aa00404770")]
  [ComImport]
  public interface IOleDocument
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateView(
      UnsafeMethods.IOleInPlaceSite pIPSite,
      Intermech.Controls.OleContainer.IStream pstm,
      int dwReserved,
      out UnsafeMethods.IOleDocumentView ppView);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDocMiscStatus(out int pdwStatus);

    int EnumViews(out object ppEnum, out UnsafeMethods.IOleDocumentView ppView);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [Guid("B722BCC7-4E68-101B-A2BC-00AA00404770")]
  [ComImport]
  public interface IOleDocumentSite
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int ActivateMe([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleDocumentView pViewToActivate);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("B722BCC6-4E68-101B-A2BC-00AA00404770")]
  [ComVisible(true)]
  public interface IOleDocumentView
  {
    void SetInPlaceSite([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleInPlaceSite pIPSite);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IOleInPlaceSite GetInPlaceSite();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetDocument();

    void SetRect([In] ref RECT prcView);

    void GetRect([In, Out] ref RECT prcView);

    void SetRectComplex([In] RECT prcView, [In] RECT prcHScroll, [In] RECT prcVScroll, [In] RECT prcSizeBox);

    void Show(bool fShow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UIActivate(bool fUIActivate);

    void Open();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Close([MarshalAs(UnmanagedType.U4), In] int dwReserved);

    void SaveViewState([MarshalAs(UnmanagedType.Interface), In] Intermech.Controls.OleContainer.IStream pstm);

    void ApplyViewState([MarshalAs(UnmanagedType.Interface), In] Intermech.Controls.OleContainer.IStream pstm);

    void Clone([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleInPlaceSite pIPSiteNew, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.IOleDocumentView[] ppViewNew);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000121-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleDropSource
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleQueryContinueDrag(int fEscapePressed, [MarshalAs(UnmanagedType.U4), In] int grfKeyState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleGiveFeedback([MarshalAs(UnmanagedType.U4), In] int dwEffect);
  }

  [Guid("00000122-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleDropTarget
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDragEnter([MarshalAs(UnmanagedType.Interface), In] object pDataObj, [MarshalAs(UnmanagedType.U4), In] int grfKeyState, [MarshalAs(UnmanagedType.U8), In] long pt, [In, Out] ref int pdwEffect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDragOver([MarshalAs(UnmanagedType.U4), In] int grfKeyState, [MarshalAs(UnmanagedType.U8), In] long pt, [In, Out] ref int pdwEffect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDragLeave();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDrop([MarshalAs(UnmanagedType.Interface), In] object pDataObj, [MarshalAs(UnmanagedType.U4), In] int grfKeyState, [MarshalAs(UnmanagedType.U8), In] long pt, [In, Out] ref int pdwEffect);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000117-0000-0000-C000-000000000046")]
  [SuppressUnmanagedCodeSecurity]
  [ComImport]
  public interface IOleInPlaceActiveObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindow(out IntPtr hwnd);

    void ContextSensitiveHelp(int fEnterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int TranslateAccelerator([In] ref MSG lpmsg);

    void OnFrameWindowActivate(int fActivate);

    void OnDocWindowActivate(int fActivate);

    void ResizeBorder(
      [In] COMRECT prcBorder,
      [In] UnsafeMethods.IOleInPlaceUIWindow pUIWindow,
      bool fFrameWindow);

    void EnableModeless(int fEnable);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000116-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleInPlaceFrame
  {
    IntPtr GetWindow();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ContextSensitiveHelp(int fEnterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetBorder([Out] COMRECT lprectBorder);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RequestBorderSpace([In] COMRECT pborderwidths);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetBorderSpace([In] COMRECT pborderwidths);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetActiveObject(
      [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleInPlaceActiveObject pActiveObject,
      [MarshalAs(UnmanagedType.LPWStr), In] string pszObjName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int InsertMenus([In] IntPtr hmenuShared, [In, Out] tagOleMenuGroupWidths lpMenuWidths);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RemoveMenus([In] IntPtr hmenuShared);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetStatusText([MarshalAs(UnmanagedType.LPWStr), In] string pszStatusText);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnableModeless(bool fEnable);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int TranslateAccelerator([In] ref MSG lpmsg, [MarshalAs(UnmanagedType.U2), In] short wID);
  }

  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000113-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleInPlaceObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindow(out IntPtr hwnd);

    void ContextSensitiveHelp(int fEnterMode);

    void InPlaceDeactivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UIDeactivate();

    void SetObjectRects([In] COMRECT lprcPosRect, [In] COMRECT lprcClipRect);

    void ReactivateAndUndo();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("1C2056CC-5EF4-101B-8BC8-00AA003E3B29")]
  [ComImport]
  public interface IOleInPlaceObjectWindowless
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetClientSite([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleClientSite pClientSite);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetClientSite(out UnsafeMethods.IOleClientSite site);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetHostNames([MarshalAs(UnmanagedType.LPWStr), In] string szContainerApp, [MarshalAs(UnmanagedType.LPWStr), In] string szContainerObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Close(int dwSaveOption);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMoniker([MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface), In] object pmk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMoniker([MarshalAs(UnmanagedType.U4), In] int dwAssign, [MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int InitFromData([MarshalAs(UnmanagedType.Interface), In] IDataObject pDataObject, int fCreation, [MarshalAs(UnmanagedType.U4), In] int dwReserved);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetClipboardData([MarshalAs(UnmanagedType.U4), In] int dwReserved, out IDataObject data);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DoVerb(
      int iVerb,
      [In] IntPtr lpmsg,
      [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleClientSite pActiveSite,
      int lindex,
      IntPtr hwndParent,
      [In] COMRECT lprcPosRect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumVerbs(out UnsafeMethods.IEnumOLEVERB e);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleUpdate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsUpToDate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetUserClassID([In, Out] ref Guid pClsid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetUserType([MarshalAs(UnmanagedType.U4), In] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetExtent([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, [In] tagSIZEL pSizel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetExtent([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, [Out] tagSIZEL pSizel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Advise([MarshalAs(UnmanagedType.Interface), In] IAdviseSink pAdvSink, out int cookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Unadvise([MarshalAs(UnmanagedType.U4), In] int dwConnection);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumAdvise(out IEnumSTATDATA e);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMiscStatus([MarshalAs(UnmanagedType.U4), In] int dwAspect, out int misc);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetColorScheme([In] tagLOGPALETTE pLogpal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnWindowMessage([MarshalAs(UnmanagedType.U4), In] int msg, [MarshalAs(UnmanagedType.U4), In] int wParam, [MarshalAs(UnmanagedType.U4), In] int lParam, [MarshalAs(UnmanagedType.U4), Out] int plResult);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDropTarget([MarshalAs(UnmanagedType.Interface), Out] object ppDropTarget);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000119-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleInPlaceSite
  {
    IntPtr GetWindow();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ContextSensitiveHelp(int fEnterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CanInPlaceActivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnInPlaceActivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnUIActivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindowContext(
      [MarshalAs(UnmanagedType.Interface)] out UnsafeMethods.IOleInPlaceFrame ppFrame,
      [MarshalAs(UnmanagedType.Interface)] out UnsafeMethods.IOleInPlaceUIWindow ppDoc,
      [Out] COMRECT lprcPosRect,
      [Out] COMRECT lprcClipRect,
      [In, Out] tagOIFI lpFrameInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Scroll(tagSIZE scrollExtant);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnUIDeactivate(int fUndoable);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnInPlaceDeactivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DiscardUndoState();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DeactivateAndUndo();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnPosRectChange([In] COMRECT lprcPosRect);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000115-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleInPlaceUIWindow
  {
    IntPtr GetWindow();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ContextSensitiveHelp(int fEnterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetBorder([Out] COMRECT lprectBorder);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RequestBorderSpace([In] COMRECT pborderwidths);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetBorderSpace([In] COMRECT pborderwidths);

    void SetActiveObject(
      [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleInPlaceActiveObject pActiveObject,
      [MarshalAs(UnmanagedType.LPWStr), In] string pszObjName);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000016-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleMessageFilter
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int HandleInComingCall(
      int dwCallType,
      IntPtr hTaskCaller,
      int dwTickCount,
      IntPtr lpInterfaceInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [SuppressUnmanagedCodeSecurity]
  [Guid("00000112-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetClientSite([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleClientSite pClientSite);

    UnsafeMethods.IOleClientSite GetClientSite();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetHostNames([MarshalAs(UnmanagedType.LPWStr), In] string szContainerApp, [MarshalAs(UnmanagedType.LPWStr), In] string szContainerObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Close(int dwSaveOption);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMoniker([MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface), In] object pmk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMoniker([MarshalAs(UnmanagedType.U4), In] int dwAssign, [MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int InitFromData([MarshalAs(UnmanagedType.Interface), In] IDataObject pDataObject, int fCreation, [MarshalAs(UnmanagedType.U4), In] int dwReserved);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetClipboardData([MarshalAs(UnmanagedType.U4), In] int dwReserved, out IDataObject data);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DoVerb(
      int iVerb,
      [In] IntPtr lpmsg,
      [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IOleClientSite pActiveSite,
      int lindex,
      IntPtr hwndParent,
      [In] COMRECT lprcPosRect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumVerbs(out UnsafeMethods.IEnumOLEVERB e);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleUpdate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsUpToDate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetUserClassID([In, Out] ref Guid pClsid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetUserType([MarshalAs(UnmanagedType.U4), In] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetExtent([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, [In] tagSIZEL pSizel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetExtent([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, [Out] tagSIZEL pSizel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Advise(IAdviseSink pAdvSink, out int cookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Unadvise([MarshalAs(UnmanagedType.U4), In] int dwConnection);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumAdvise(out IEnumSTATDATA e);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMiscStatus([MarshalAs(UnmanagedType.U4), In] int dwAspect, out int misc);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetColorScheme([In] tagLOGPALETTE pLogpal);
  }

  [Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleServiceProvider
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryService([In] ref Guid guidService, [In] ref Guid riid, out IntPtr ppvObject);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000114-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleWindow
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindow(out IntPtr hwnd);

    void ContextSensitiveHelp(int fEnterMode);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  [Guid("FECEAAA2-8405-11CF-8BA1-00AA00476DA6")]
  [ComVisible(true)]
  public interface IOmHistory
  {
    short GetLength();

    void Back([In] ref object pvargdistance);

    void Forward([In] ref object pvargdistance);

    void Go([In] ref object pvargdistance);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("FECEAAA5-8405-11CF-8BA1-00AA00476DA6")]
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(true)]
  public interface IOmNavigator
  {
    string GetAppCodeName();

    string GetAppName();

    string GetAppVersion();

    string GetUserAgent();

    bool JavaEnabled();

    bool TaintEnabled();

    object GetMimeTypes();

    object GetPlugins();

    bool GetCookieEnabled();

    object GetOpsProfile();

    string GetCpuClass();

    string GetSystemLanguage();

    string GetBrowserLanguage();

    string GetUserLanguage();

    string GetPlatform();

    string GetAppMinorVersion();

    int GetConnectionSpeed();

    bool GetOnLine();

    object GetUserProfile();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("0000010C-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IPersist
  {
    [SuppressUnmanagedCodeSecurity]
    void GetClassID(out Guid pClassID);
  }

  [Guid("37D84F60-42CB-11CE-8135-00AA004BB851")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IPersistPropertyBag
  {
    void GetClassID(out Guid pClassID);

    void InitNew();

    void Load([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IPropertyBag pPropBag, [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IErrorLog pErrorLog);

    void Save([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IPropertyBag pPropBag, [MarshalAs(UnmanagedType.Bool), In] bool fClearDirty, [MarshalAs(UnmanagedType.Bool), In] bool fSaveAllProperties);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("0000010A-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IPersistStorage
  {
    void GetClassID(out Guid pClassID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    void InitNew(UnsafeMethods.IStorage pstg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Load(UnsafeMethods.IStorage pstg);

    void Save(UnsafeMethods.IStorage pStgSave, bool fSameAsLoad);

    void SaveCompleted(UnsafeMethods.IStorage pStgNew);

    void HandsOffStorage();
  }

  [Guid("00000109-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IPersistStream
  {
    void GetClassID(out Guid pClassId);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    void Load([MarshalAs(UnmanagedType.Interface), In] Intermech.Controls.OleContainer.IStream pstm);

    void Save([MarshalAs(UnmanagedType.Interface), In] Intermech.Controls.OleContainer.IStream pstm, [MarshalAs(UnmanagedType.Bool), In] bool fClearDirty);

    long GetSizeMax();
  }

  [Guid("7FD52380-4E07-101B-AE2D-08002B2EC713")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [SuppressUnmanagedCodeSecurity]
  [ComImport]
  public interface IPersistStreamInit
  {
    void GetClassID(out Guid pClassID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    void Load([MarshalAs(UnmanagedType.Interface), In] Intermech.Controls.OleContainer.IStream pstm);

    void Save([MarshalAs(UnmanagedType.Interface), In] Intermech.Controls.OleContainer.IStream pstm, [MarshalAs(UnmanagedType.Bool), In] bool fClearDirty);

    void GetSizeMax([MarshalAs(UnmanagedType.LPArray), Out] long pcbSize);

    void InitNew();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
  [ComImport]
  public interface IPropertyBag
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Read([MarshalAs(UnmanagedType.LPWStr), In] string pszPropName, [In, Out] ref object pVar, [In] UnsafeMethods.IErrorLog pErrorLog);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Write([MarshalAs(UnmanagedType.LPWStr), In] string pszPropName, [In] ref object pVar);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("9BFBBC02-EFF1-101A-84ED-00AA00341D07")]
  [ComImport]
  public interface IPropertyNotifySink
  {
    void OnChanged(int dispID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnRequestEdit(int dispID);
  }

  [Guid("00020D03-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IRichEditOleCallback
  {
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00020D03-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IRichTextBoxOleCallback
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetNewStorage(out UnsafeMethods.IStorage ret);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ShowContainerUI(int fShow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DeleteObject(IntPtr lpoleobj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryAcceptData(
      IDataObject lpdataobj,
      IntPtr lpcfFormat,
      int reco,
      int fReally,
      IntPtr hMetaPict);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ContextSensitiveHelp(int fEnterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetClipboardData(CHARRANGE lpchrg, int reco, IntPtr lplpdataobj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetContextMenu(short seltype, IntPtr lpoleobj, CHARRANGE lpchrg, out IntPtr hmenu);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000126-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IRunnableObject
  {
    void GetRunningClass(out Guid guid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Run(IntPtr lpBindContext);

    bool IsRunning();

    void LockRunning(bool fLock, bool fLastUnlockCloses);

    void SetContainedObject(bool fContained);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("742B0E01-14E6-101B-914E-00AA00300CAB")]
  [ComImport]
  public interface ISimpleFrameSite
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PreMessageFilter(
      IntPtr hwnd,
      [MarshalAs(UnmanagedType.U4), In] int msg,
      IntPtr wp,
      IntPtr lp,
      [In, Out] ref IntPtr plResult,
      [MarshalAs(UnmanagedType.U4), In, Out] ref int pdwCookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PostMessageFilter(
      IntPtr hwnd,
      [MarshalAs(UnmanagedType.U4), In] int msg,
      IntPtr wp,
      IntPtr lp,
      [In, Out] ref IntPtr plResult,
      [MarshalAs(UnmanagedType.U4), In] int dwCookie);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("0000000B-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IStorage
  {
    [return: MarshalAs(UnmanagedType.Interface)]
    Intermech.Controls.OleContainer.IStream CreateStream(
      [MarshalAs(UnmanagedType.BStr), In] string pwcsName,
      [MarshalAs(UnmanagedType.U4), In] int grfMode,
      [MarshalAs(UnmanagedType.U4), In] int reserved1,
      [MarshalAs(UnmanagedType.U4), In] int reserved2);

    [return: MarshalAs(UnmanagedType.Interface)]
    Intermech.Controls.OleContainer.IStream OpenStream(
      [MarshalAs(UnmanagedType.BStr), In] string pwcsName,
      IntPtr reserved1,
      [MarshalAs(UnmanagedType.U4), In] int grfMode,
      [MarshalAs(UnmanagedType.U4), In] int reserved2);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IStorage CreateStorage(
      [MarshalAs(UnmanagedType.BStr), In] string pwcsName,
      [MarshalAs(UnmanagedType.U4), In] int grfMode,
      [MarshalAs(UnmanagedType.U4), In] int reserved1,
      [MarshalAs(UnmanagedType.U4), In] int reserved2);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.IStorage OpenStorage(
      [MarshalAs(UnmanagedType.BStr), In] string pwcsName,
      IntPtr pstgPriority,
      [MarshalAs(UnmanagedType.U4), In] int grfMode,
      IntPtr snbExclude,
      [MarshalAs(UnmanagedType.U4), In] int reserved);

    void CopyTo(
      int ciidExclude,
      [MarshalAs(UnmanagedType.LPArray), In] Guid[] pIIDExclude,
      IntPtr snbExclude,
      [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IStorage stgDest);

    void MoveElementTo(
      [MarshalAs(UnmanagedType.BStr), In] string pwcsName,
      [MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.IStorage stgDest,
      [MarshalAs(UnmanagedType.BStr), In] string pwcsNewName,
      [MarshalAs(UnmanagedType.U4), In] int grfFlags);

    void Commit(int grfCommitFlags);

    void Revert();

    void EnumElements([MarshalAs(UnmanagedType.U4), In] int reserved1, IntPtr reserved2, [MarshalAs(UnmanagedType.U4), In] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);

    void DestroyElement([MarshalAs(UnmanagedType.BStr), In] string pwcsName);

    void RenameElement([MarshalAs(UnmanagedType.BStr), In] string pwcsOldName, [MarshalAs(UnmanagedType.BStr), In] string pwcsNewName);

    void SetElementTimes([MarshalAs(UnmanagedType.BStr), In] string pwcsName, [In] FILETIME pctime, [In] FILETIME patime, [In] FILETIME pmtime);

    void SetClass([In] ref Guid clsid);

    void SetStateBits(int grfStateBits, int grfMask);

    void Stat([Out] STATSTG pStatStg, int grfStatFlag);
  }

  [SuppressUnmanagedCodeSecurity]
  [Guid("0000000C-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IStream
  {
    int Read(IntPtr buf, int len);

    int Write(IntPtr buf, int len);

    [return: MarshalAs(UnmanagedType.I8)]
    long Seek([MarshalAs(UnmanagedType.I8), In] long dlibMove, int dwOrigin);

    void SetSize([MarshalAs(UnmanagedType.I8), In] long libNewSize);

    [return: MarshalAs(UnmanagedType.I8)]
    long CopyTo([MarshalAs(UnmanagedType.Interface), In] Intermech.Controls.OleContainer.IStream pstm, [MarshalAs(UnmanagedType.I8), In] long cb, [MarshalAs(UnmanagedType.LPArray), Out] long[] pcbRead);

    void Commit(int grfCommitFlags);

    void Revert();

    void LockRegion([MarshalAs(UnmanagedType.I8), In] long libOffset, [MarshalAs(UnmanagedType.I8), In] long cb, int dwLockType);

    void UnlockRegion([MarshalAs(UnmanagedType.I8), In] long libOffset, [MarshalAs(UnmanagedType.I8), In] long cb, int dwLockType);

    void Stat([Out] STATSTG pStatstg, int grfStatFlag);

    [return: MarshalAs(UnmanagedType.Interface)]
    Intermech.Controls.OleContainer.IStream Clone();
  }

  [Guid("DF0B3D60-548F-101B-8E65-08002B2BD119")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface ISupportErrorInfo
  {
    int InterfaceSupportsErrorInfo([In] ref Guid riid);
  }

  [Guid("8CC497C0-A1DF-11ce-8098-00AA0047BE5D")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [SuppressUnmanagedCodeSecurity]
  public interface ITextDocument
  {
    string GetName();

    object GetSelection();

    int GetStoryCount();

    object GetStoryRanges();

    int GetSaved();

    void SetSaved(int value);

    object GetDefaultTabStop();

    void SetDefaultTabStop(object value);

    void New();

    void Open(object pVar, int flags, int codePage);

    void Save(object pVar, int flags, int codePage);

    int Freeze();

    int Unfreeze();

    void BeginEditCollection();

    void EndEditCollection();

    int Undo(int count);

    int Redo(int count);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.ITextRange Range(int cp1, int cp2);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.ITextRange RangeFromPoint(int x, int y);
  }

  [ComVisible(true)]
  [SuppressUnmanagedCodeSecurity]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("8CC497C2-A1DF-11ce-8098-00AA0047BE5D")]
  public interface ITextRange
  {
    string GetText();

    void SetText(string text);

    object GetChar();

    void SetChar(object ch);

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.ITextRange GetDuplicate();

    [return: MarshalAs(UnmanagedType.Interface)]
    UnsafeMethods.ITextRange GetFormattedText();

    void SetFormattedText([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.ITextRange range);

    int GetStart();

    void SetStart(int cpFirst);

    int GetEnd();

    void SetEnd(int cpLim);

    object GetFont();

    void SetFont(object font);

    object GetPara();

    void SetPara(object para);

    int GetStoryLength();

    int GetStoryType();

    void Collapse(int start);

    int Expand(int unit);

    int GetIndex(int unit);

    void SetIndex(int unit, int index, int extend);

    void SetRange(int cpActive, int cpOther);

    int InRange([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.ITextRange range);

    int InStory([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.ITextRange range);

    int IsEqual([MarshalAs(UnmanagedType.Interface), In] UnsafeMethods.ITextRange range);

    void Select();

    int StartOf(int unit, int extend);

    int EndOf(int unit, int extend);

    int Move(int unit, int count);

    int MoveStart(int unit, int count);

    int MoveEnd(int unit, int count);

    int MoveWhile(object cset, int count);

    int MoveStartWhile(object cset, int count);

    int MoveEndWhile(object cset, int count);

    int MoveUntil(object cset, int count);

    int MoveStartUntil(object cset, int count);

    int MoveEndUntil(object cset, int count);

    int FindText(string text, int cch, int flags);

    int FindTextStart(string text, int cch, int flags);

    int FindTextEnd(string text, int cch, int flags);

    int Delete(int unit, int count);

    void Cut(out object pVar);

    void Copy(out object pVar);

    void Paste(object pVar, int format);

    int CanPaste(object pVar, int format);

    int CanEdit();

    void ChangeCase(int type);

    void GetPoint(int type, out int x, out int y);

    void SetPoint(int x, int y, int type, int extend);

    void ScrollIntoView(int value);

    object GetEmbeddedObject();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00020403-0000-0000-C000-000000000046")]
  [ComImport]
  public interface ITypeComp
  {
    void RemoteBind(
      [MarshalAs(UnmanagedType.LPWStr), In] string szName,
      [MarshalAs(UnmanagedType.U4), In] int lHashVal,
      [MarshalAs(UnmanagedType.U2), In] short wFlags,
      [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeInfo[] ppTInfo,
      [MarshalAs(UnmanagedType.LPArray), Out] tagDESCKIND[] pDescKind,
      [MarshalAs(UnmanagedType.LPArray), Out] tagFUNCDESC[] ppFuncDesc,
      [MarshalAs(UnmanagedType.LPArray), Out] tagVARDESC[] ppVarDesc,
      [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeComp[] ppTypeComp,
      [MarshalAs(UnmanagedType.LPArray), Out] int[] pDummy);

    void RemoteBindType([MarshalAs(UnmanagedType.LPWStr), In] string szName, [MarshalAs(UnmanagedType.U4), In] int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeInfo[] ppTInfo);
  }

  [Guid("00020401-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface ITypeInfo
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeAttr(ref IntPtr pTypeAttr);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeComp([MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeComp[] ppTComp);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetFuncDesc([MarshalAs(UnmanagedType.U4), In] int index, ref IntPtr pFuncDesc);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetVarDesc([MarshalAs(UnmanagedType.U4), In] int index, ref IntPtr pVarDesc);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetNames(int memid, [MarshalAs(UnmanagedType.LPArray), Out] string[] rgBstrNames, [MarshalAs(UnmanagedType.U4), In] int cMaxNames, [MarshalAs(UnmanagedType.LPArray), Out] int[] pcNames);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetRefTypeOfImplType([MarshalAs(UnmanagedType.U4), In] int index, [MarshalAs(UnmanagedType.LPArray), Out] int[] pRefType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetImplTypeFlags([MarshalAs(UnmanagedType.U4), In] int index, [MarshalAs(UnmanagedType.LPArray), Out] int[] pImplTypeFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetIDsOfNames(IntPtr rgszNames, int cNames, IntPtr pMemId);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Invoke();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDocumentation(
      int memid,
      ref string pBstrName,
      ref string pBstrDocString,
      [MarshalAs(UnmanagedType.LPArray), Out] int[] pdwHelpContext,
      [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrHelpFile);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDllEntry(
      int memid,
      tagINVOKEKIND invkind,
      [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrDllName,
      [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrName,
      [MarshalAs(UnmanagedType.LPArray), Out] short[] pwOrdinal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetRefTypeInfo(IntPtr hreftype, ref UnsafeMethods.ITypeInfo pTypeInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AddressOfMember();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateInstance([In] ref Guid riid, [MarshalAs(UnmanagedType.LPArray), Out] object[] ppvObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMops(int memid, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrMops);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetContainingTypeLib([MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeLib[] ppTLib, [MarshalAs(UnmanagedType.LPArray), Out] int[] pIndex);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseTypeAttr(IntPtr typeAttr);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseFuncDesc(IntPtr funcDesc);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseVarDesc(IntPtr varDesc);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00020402-0000-0000-C000-000000000046")]
  [ComImport]
  public interface ITypeLib
  {
    void RemoteGetTypeInfoCount([MarshalAs(UnmanagedType.LPArray), Out] int[] pcTInfo);

    void GetTypeInfo([MarshalAs(UnmanagedType.U4), In] int index, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeInfo[] ppTInfo);

    void GetTypeInfoType([MarshalAs(UnmanagedType.U4), In] int index, [MarshalAs(UnmanagedType.LPArray), Out] tagTYPEKIND[] pTKind);

    void GetTypeInfoOfGuid([In] ref Guid guid, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeInfo[] ppTInfo);

    void RemoteGetLibAttr([MarshalAs(UnmanagedType.LPArray), Out] tagTLIBATTR[] ppTLibAttr, [MarshalAs(UnmanagedType.LPArray), Out] int[] pDummy);

    void GetTypeComp([MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeComp[] ppTComp);

    void RemoteGetDocumentation(
      int index,
      [MarshalAs(UnmanagedType.U4), In] int refPtrFlags,
      [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrName,
      [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrDocString,
      [MarshalAs(UnmanagedType.LPArray), Out] int[] pdwHelpContext,
      [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrHelpFile);

    void RemoteIsName([MarshalAs(UnmanagedType.LPWStr), In] string szNameBuf, [MarshalAs(UnmanagedType.U4), In] int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] IntPtr[] pfName, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrLibName);

    void RemoteFindName(
      [MarshalAs(UnmanagedType.LPWStr), In] string szNameBuf,
      [MarshalAs(UnmanagedType.U4), In] int lHashVal,
      [MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.ITypeInfo[] ppTInfo,
      [MarshalAs(UnmanagedType.LPArray), Out] int[] rgMemId,
      [MarshalAs(UnmanagedType.LPArray), In, Out] short[] pcFound,
      [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrLibName);

    void LocalReleaseTLibAttr();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("9849FD60-3768-101B-8D72-AE6164FFE3CF")]
  [ComImport]
  public interface IVBFormat
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Format(
      [In] ref object var,
      IntPtr pszFormat,
      IntPtr lpBuffer,
      short cpBuffer,
      int lcid,
      short firstD,
      short firstW,
      [MarshalAs(UnmanagedType.LPArray), Out] short[] result);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("40A050A0-3C31-101B-A82E-08002B2B2337")]
  [ComImport]
  public interface IVBGetControl
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumControls(int dwOleContF, int dwWhich, out UnsafeMethods.IEnumUnknown ppenum);
  }

  [Guid("0000010d-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IViewObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Draw(
      [MarshalAs(UnmanagedType.U4), In] int dwDrawAspect,
      int lindex,
      IntPtr pvAspect,
      [In] tagDVTARGETDEVICE ptd,
      IntPtr hdcTargetDev,
      IntPtr hdcDraw,
      [In] COMRECT lprcBounds,
      [In] COMRECT lprcWBounds,
      IntPtr pfnContinue,
      [In] int dwContinue);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetColorSet(
      [MarshalAs(UnmanagedType.U4), In] int dwDrawAspect,
      int lindex,
      IntPtr pvAspect,
      [In] tagDVTARGETDEVICE ptd,
      IntPtr hicTargetDev,
      [Out] tagLOGPALETTE ppColorSet);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Freeze([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Unfreeze([MarshalAs(UnmanagedType.U4), In] int dwFreeze);

    void SetAdvise([MarshalAs(UnmanagedType.U4), In] int aspects, [MarshalAs(UnmanagedType.U4), In] int advf, [MarshalAs(UnmanagedType.Interface), In] IAdviseSink pAdvSink);

    void GetAdvise([MarshalAs(UnmanagedType.LPArray), In, Out] int[] paspects, [MarshalAs(UnmanagedType.LPArray), In, Out] int[] advf, [MarshalAs(UnmanagedType.LPArray), In, Out] IAdviseSink[] pAdvSink);
  }

  [Guid("00000127-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IViewObject2
  {
    void Draw(
      [MarshalAs(UnmanagedType.U4), In] int dwDrawAspect,
      int lindex,
      IntPtr pvAspect,
      [In] tagDVTARGETDEVICE ptd,
      IntPtr hdcTargetDev,
      IntPtr hdcDraw,
      [In] COMRECT lprcBounds,
      [In] COMRECT lprcWBounds,
      IntPtr pfnContinue,
      [In] int dwContinue);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetColorSet(
      [MarshalAs(UnmanagedType.U4), In] int dwDrawAspect,
      int lindex,
      IntPtr pvAspect,
      [In] tagDVTARGETDEVICE ptd,
      IntPtr hicTargetDev,
      [Out] tagLOGPALETTE ppColorSet);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Freeze([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Unfreeze([MarshalAs(UnmanagedType.U4), In] int dwFreeze);

    void SetAdvise([MarshalAs(UnmanagedType.U4), In] int aspects, [MarshalAs(UnmanagedType.U4), In] int advf, [MarshalAs(UnmanagedType.Interface), In] IAdviseSink pAdvSink);

    void GetAdvise([MarshalAs(UnmanagedType.LPArray), In, Out] int[] paspects, [MarshalAs(UnmanagedType.LPArray), In, Out] int[] advf, [MarshalAs(UnmanagedType.LPArray), In, Out] IAdviseSink[] pAdvSink);

    void GetExtent([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, int lindex, [In] tagDVTARGETDEVICE ptd, [Out] tagSIZEL lpsizel);
  }

  [Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
  [TypeLibType(TypeLibTypeFlags.FHidden | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FOleAutomation)]
  [SuppressUnmanagedCodeSecurity]
  [ComImport]
  public interface IWebBrowser2
  {
    [DispId(100)]
    void GoBack();

    [DispId(101)]
    void GoForward();

    [DispId(102)]
    void GoHome();

    [DispId(103)]
    void GoSearch();

    [DispId(104)]
    void Navigate(
      [In] string Url,
      [In] ref object flags,
      [In] ref object targetFrameName,
      [In] ref object postData,
      [In] ref object headers);

    [DispId(-550)]
    void Refresh();

    [DispId(105)]
    void Refresh2([In] ref object level);

    [DispId(106)]
    void Stop();

    [DispId(200)]
    object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

    [DispId(201)]
    object Parent { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

    [DispId(202)]
    object Container { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

    [DispId(203)]
    object Document { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

    [DispId(204)]
    bool TopLevelContainer { get; }

    [DispId(205)]
    string Type { get; }

    [DispId(206)]
    int Left { get; set; }

    [DispId(207)]
    int Top { get; set; }

    [DispId(208 /*0xD0*/)]
    int Width { get; set; }

    [DispId(209)]
    int Height { get; set; }

    [DispId(210)]
    string LocationName { get; }

    [DispId(211)]
    string LocationURL { get; }

    [DispId(212)]
    bool Busy { get; }

    [DispId(300)]
    void Quit();

    [DispId(301)]
    void ClientToWindow(out int pcx, out int pcy);

    [DispId(302)]
    void PutProperty([In] string property, [In] object vtValue);

    [DispId(303)]
    object GetProperty([In] string property);

    [DispId(0)]
    string Name { get; }

    [DispId(-515)]
    int HWND { get; }

    [DispId(400)]
    string FullName { get; }

    [DispId(401)]
    string Path { get; }

    [DispId(402)]
    bool Visible { get; set; }

    [DispId(403)]
    bool StatusBar { get; set; }

    [DispId(404)]
    string StatusText { get; set; }

    [DispId(405)]
    int ToolBar { get; set; }

    [DispId(406)]
    bool MenuBar { get; set; }

    [DispId(407)]
    bool FullScreen { get; set; }

    [DispId(500)]
    void Navigate2(
      [In] ref object URL,
      [In] ref object flags,
      [In] ref object targetFrameName,
      [In] ref object postData,
      [In] ref object headers);

    [DispId(501)]
    UnsafeMethods.OLECMDF QueryStatusWB([In] UnsafeMethods.OLECMDID cmdID);

    [DispId(502)]
    void ExecWB(
      [In] UnsafeMethods.OLECMDID cmdID,
      [In] UnsafeMethods.OLECMDEXECOPT cmdexecopt,
      ref object pvaIn,
      IntPtr pvaOut);

    [DispId(503)]
    void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);

    [DispId(-525)]
    WebBrowserReadyState ReadyState { get; }

    [DispId(550)]
    bool Offline { get; set; }

    [DispId(551)]
    bool Silent { get; set; }

    [DispId(552)]
    bool RegisterAsBrowser { get; set; }

    [DispId(553)]
    bool RegisterAsDropTarget { get; set; }

    [DispId(554)]
    bool TheaterMode { get; set; }

    [DispId(555)]
    bool AddressBar { get; set; }

    [DispId(556)]
    bool Resizable { get; set; }
  }

  public enum OLECMDEXECOPT
  {
    OLECMDEXECOPT_DODEFAULT,
    OLECMDEXECOPT_PROMPTUSER,
    OLECMDEXECOPT_DONTPROMPTUSER,
    OLECMDEXECOPT_SHOWHELP,
  }

  public enum OLECMDF
  {
    OLECMDF_SUPPORTED = 1,
    OLECMDF_ENABLED = 2,
    OLECMDF_LATCHED = 4,
    OLECMDF_NINCHED = 8,
    OLECMDF_INVISIBLE = 16, // 0x00000010
    OLECMDF_DEFHIDEONCTXTMENU = 32, // 0x00000020
  }

  public enum OLECMDID
  {
    OLECMDID_SAVEAS = 4,
    OLECMDID_PRINT = 6,
    OLECMDID_PRINTPREVIEW = 7,
    OLECMDID_PAGESETUP = 8,
    OLECMDID_PROPERTIES = 10, // 0x0000000A
  }

  private struct POINTSTRUCT(int x, int y)
  {
    public int x = x;
    public int y = y;
  }

  public struct RGNDATAHEADER
  {
    public int cbSizeOfStruct;
    public int iType;
    public int nCount;
    public int nRgnSize;
  }

  [SuppressUnmanagedCodeSecurity]
  public class Shell32
  {
    [DllImport("shell32.dll")]
    public static extern int SHGetMalloc([MarshalAs(UnmanagedType.LPArray), Out] UnsafeMethods.IMalloc[] ppMalloc);

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

    [DllImport("shell32.dll")]
    public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);
  }

  [Guid("000C060B-0000-0000-C000-000000000046")]
  [ComImport]
  public class SMsoComponentManager
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern SMsoComponentManager();
  }
}
