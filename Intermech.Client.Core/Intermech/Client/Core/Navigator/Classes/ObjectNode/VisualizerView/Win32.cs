
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.Win32
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.CustomMarshalers;
using System.Text;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

internal class Win32
{
  public const uint GW_CHILD = 5;
  public const uint GW_ENABLEDPOPUP = 6;
  public const uint GW_HWNDFIRST = 0;
  public const uint GW_HWNDLAST = 1;
  public const uint GW_HWNDNEXT = 2;
  public const uint GW_HWNDPREV = 3;
  public const uint GW_OWNER = 4;
  public const int GWL_STYLE = -16;
  public const uint SWP_ASYNCWINDOWPOS = 16384 /*0x4000*/;
  public const uint SWP_DEFERERASE = 8192 /*0x2000*/;
  public const uint SWP_DRAWFRAME = 32 /*0x20*/;
  public const uint SWP_FRAMECHANGED = 32 /*0x20*/;
  public const uint SWP_HIDEWINDOW = 128 /*0x80*/;
  public const uint SWP_NOACTIVATE = 16 /*0x10*/;
  public const uint SWP_NOCOPYBITS = 256 /*0x0100*/;
  public const uint SWP_NOMOVE = 2;
  public const uint SWP_NOOWNERZORDER = 512 /*0x0200*/;
  public const uint SWP_NOREDRAW = 8;
  public const uint SWP_NOREPOSITION = 512 /*0x0200*/;
  public const uint SWP_NOSENDCHANGING = 1024 /*0x0400*/;
  public const uint SWP_NOSIZE = 1;
  public const uint SWP_NOZORDER = 4;
  public const uint SWP_SHOWWINDOW = 64 /*0x40*/;
  public const int WS_BORDER = 8388608 /*0x800000*/;
  public const int WS_CAPTION = 12582912 /*0xC00000*/;
  public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
  public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
  public static readonly IntPtr HWND_TOP = new IntPtr(0);
  public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

  [DllImport("kernel32", SetLastError = true)]
  public static extern bool CloseHandle(IntPtr hObject);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool EnumWindows(Win32.EnumWindowsProc enumProc, IntPtr lParam);

  [DllImport("user32.dll")]
  public static extern IntPtr FindWindowEx(
    IntPtr parentWindow,
    IntPtr previousChildWindow,
    string windowClass,
    string windowTitle);

  [DllImport("user32.dll")]
  public static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

  [DllImport("User32.dll", CharSet = CharSet.Unicode)]
  private static extern void GetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);

  /// <summary>Return the window title of handle</summary>
  /// <param name="hWnd"></param>
  /// <returns></returns>
  public static string GetWindowText(IntPtr hWnd)
  {
    StringBuilder lpString = new StringBuilder(256 /*0x0100*/);
    Win32.GetWindowText(hWnd, lpString, lpString.Capacity);
    return lpString.ToString();
  }

  public static IntPtr[] GetProcessMainWindows(int process)
  {
    List<Tuple<IntPtr, WINDOWINFO, string>> lWindows = new List<Tuple<IntPtr, WINDOWINFO, string>>();
    List<IntPtr> ListHandles = new List<IntPtr>();
    Win32.EnumWindows((Win32.EnumWindowsProc) ((hWnd, lParam) =>
    {
      int process1;
      Win32.GetWindowThreadProcessId(hWnd, out process1);
      if (process1 != process)
        return true;
      string windowText = Win32.GetWindowText(hWnd);
      if (!Win32.HasFlagsWindowStyles(hWnd, WindowStyles.WS_SYSMENU) || !Win32.HasFlagsWindowStyles(hWnd, WindowStyles.WS_SIZEFRAME))
        return true;
      WINDOWINFO WindowInfo = new WINDOWINFO();
      Win32.GetWindowInfo(hWnd, ref WindowInfo);
      lWindows.Add(Tuple.Create<IntPtr, WINDOWINFO, string>(hWnd, WindowInfo, windowText));
      ListHandles.Add(hWnd);
      return true;
    }), IntPtr.Zero);
    return ListHandles.ToArray();
  }

  public static IntPtr[] GetProcessWindows(int process)
  {
    IntPtr[] array = new IntPtr[256 /*0x0100*/];
    int newSize = 0;
    IntPtr num = IntPtr.Zero;
    do
    {
      num = Win32.FindWindowEx(IntPtr.Zero, num, (string) null, (string) null);
      int process1;
      Win32.GetWindowThreadProcessId(num, out process1);
      if (process1 == process)
        array[newSize++] = num;
    }
    while (num != IntPtr.Zero);
    Array.Resize<IntPtr>(ref array, newSize);
    return array;
  }

  [DllImport("User32.dll")]
  public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool GetWindowInfo(IntPtr hWnd, ref WINDOWINFO WindowInfo);

  [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
  private static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

  [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
  private static extern IntPtr GetWindowLong64(IntPtr hWnd, int nIndex);

  public static IntPtr GetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex)
  {
    return IntPtr.Size == 8 ? Win32.GetWindowLong64(hWnd, (int) nIndex) : Win32.GetWindowLong32(hWnd, (int) nIndex);
  }

  [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
  private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

  [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
  private static extern IntPtr SetWindowLong64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

  public static IntPtr SetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex, IntPtr dwNewLong)
  {
    return IntPtr.Size == 8 ? Win32.SetWindowLong64(hWnd, (int) nIndex, dwNewLong) : Win32.SetWindowLong32(hWnd, (int) nIndex, dwNewLong);
  }

  public static IntPtr SetWindowStyles(IntPtr hWnd, WindowStyles ws)
  {
    IntPtr windowLongPtr = Win32.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE);
    return Win32.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE, IntPtrEnumHelper.SetFlag(windowLongPtr, (object) ws));
  }

  public static IntPtr SetWindowStylesEx(IntPtr hWnd, WindowStylesEx wse)
  {
    IntPtr windowLongPtr = Win32.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE);
    return Win32.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE, IntPtrEnumHelper.SetFlag(windowLongPtr, (object) wse));
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

  [DllImport("user32.dll")]
  public static extern IntPtr GetWindowThreadProcessId(IntPtr window, out int process);

  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool IsWindowVisible(IntPtr hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool IsWindowVisible(HandleRef hWnd);

  /// <summary>
  /// The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
  /// </summary>
  /// <param name="hWnd">Handle to the window.</param>
  /// <param name="X">Specifies the new position of the left side of the window.</param>
  /// <param name="Y">Specifies the new position of the top of the window.</param>
  /// <param name="nWidth">Specifies the new width of the window.</param>
  /// <param name="nHeight">Specifies the new height of the window.</param>
  /// <param name="bRepaint">Specifies whether the window is to be repainted. If this parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window.</param>
  /// <returns>If the function succeeds, the return value is nonzero.
  /// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para></returns>
  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool MoveWindow(
    IntPtr hWnd,
    int X,
    int Y,
    int nWidth,
    int nHeight,
    bool bRepaint);

  [DllImport("user32.dll")]
  public static extern bool SetForegroundWindow(IntPtr hWnd);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool SetWindowPos(
    IntPtr hWnd,
    IntPtr hWndInsertAfter,
    int X,
    int Y,
    int cx,
    int cy,
    SetWindowPosFlags uFlags);

  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

  public static IntPtr UnsetWindowStyles(IntPtr hWnd, WindowStyles ws)
  {
    IntPtr windowLongPtr = Win32.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE);
    return Win32.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE, IntPtrEnumHelper.UnsetFlag(windowLongPtr, (object) ws));
  }

  public static IntPtr UnsetWindowStylesEx(IntPtr hWnd, WindowStylesEx wse)
  {
    IntPtr windowLongPtr = Win32.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE);
    return Win32.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE, IntPtrEnumHelper.UnsetFlag(windowLongPtr, (object) wse));
  }

  public static bool HasFlagsWindowStyles(IntPtr hWnd, WindowStyles ws)
  {
    return IntPtrEnumHelper.HasFlags(Win32.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE), (object) ws);
  }

  [ComVisible(true)]
  [Guid("00020400-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IDispatch
  {
    void Unused1();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetTypeInfo([MarshalAs(UnmanagedType.U4)] int iTInfo, [MarshalAs(UnmanagedType.U4)] int lcid, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (TypeToTypeInfoMarshaler))] out Type type);
  }

  public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

  public class COMIDispatch
  {
    public static Win32.COMIDispatch.MethodInformation[] GetMethodInformation(object com_obj)
    {
      Win32.COMIDispatch.IDispatch dispatch;
      try
      {
        dispatch = (Win32.COMIDispatch.IDispatch) com_obj;
      }
      catch (InvalidCastException ex)
      {
        return (Win32.COMIDispatch.MethodInformation[]) null;
      }
      ITypeInfo typeInfo = (ITypeInfo) null;
      dispatch.GetTypeInfo(0, 0, out typeInfo);
      if (typeInfo == null)
        return (Win32.COMIDispatch.MethodInformation[]) null;
      IntPtr ppTypeAttr = IntPtr.Zero;
      typeInfo.GetTypeAttr(out ppTypeAttr);
      System.Runtime.InteropServices.ComTypes.TYPEATTR structure1 = (System.Runtime.InteropServices.ComTypes.TYPEATTR) Marshal.PtrToStructure(ppTypeAttr, typeof (System.Runtime.InteropServices.ComTypes.TYPEATTR));
      typeInfo.ReleaseTypeAttr(ppTypeAttr);
      IntPtr zero1 = IntPtr.Zero;
      Win32.COMIDispatch.MethodInformation[] methodInformation = new Win32.COMIDispatch.MethodInformation[(int) structure1.cFuncs];
      for (int index = 0; index < (int) structure1.cFuncs; ++index)
      {
        IntPtr ppFuncDesc = IntPtr.Zero;
        typeInfo.GetFuncDesc(index, out ppFuncDesc);
        System.Runtime.InteropServices.ComTypes.FUNCDESC structure2 = (System.Runtime.InteropServices.ComTypes.FUNCDESC) Marshal.PtrToStructure(ppFuncDesc, typeof (System.Runtime.InteropServices.ComTypes.FUNCDESC));
        string strName;
        string strDocString;
        typeInfo.GetDocumentation(structure2.memid, out strName, out strDocString, out int _, out string _);
        methodInformation[index].m_strName = strName;
        methodInformation[index].m_strDocumentation = strDocString;
        if (structure2.invkind == System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_FUNC)
        {
          methodInformation[index].m_method_type = Win32.COMIDispatch.MethodType.Method;
          methodInformation[index].m_cParams = (int) structure2.cParams;
        }
        else if (structure2.invkind == System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYGET)
          methodInformation[index].m_method_type = Win32.COMIDispatch.MethodType.Property_Getter;
        else if (structure2.invkind == System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYPUT)
          methodInformation[index].m_method_type = Win32.COMIDispatch.MethodType.Property_Putter;
        else if (structure2.invkind == System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYPUTREF)
          methodInformation[index].m_method_type = Win32.COMIDispatch.MethodType.Property_PutRef;
        typeInfo.ReleaseFuncDesc(ppFuncDesc);
        IntPtr zero2 = IntPtr.Zero;
      }
      return methodInformation;
    }

    public enum MethodType
    {
      Method,
      Property_Getter,
      Property_Putter,
      Property_PutRef,
    }

    [Guid("00020400-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDispatch
    {
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetTypeInfoCount(out int Count);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetTypeInfo([MarshalAs(UnmanagedType.U4)] int iTInfo, [MarshalAs(UnmanagedType.U4)] int lcid, out ITypeInfo typeInfo);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetIDsOfNames(ref Guid riid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rgsNames, int cNames, int lcid, [MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int Invoke(
        int dispIdMember,
        ref Guid riid,
        uint lcid,
        ushort wFlags,
        ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams,
        out object pVarResult,
        ref System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo,
        IntPtr[] pArgErr);
    }

    public struct MethodInformation
    {
      public int m_cParams;
      public Win32.COMIDispatch.MethodType m_method_type;
      public string m_strDocumentation;
      public string m_strName;
    }
  }
}
