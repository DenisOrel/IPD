
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ExtractImageViewer.ShellThumbnail
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ExtractImageViewer;

public class ShellThumbnail : IDisposable
{
  private bool disposed;
  private Bitmap thumbNail;

  public Bitmap ThumbNail => this.thumbNail;

  public Bitmap GetThumbnail(string file, int width, int height)
  {
    if (!File.Exists(file) && !Directory.Exists(file))
      throw new FileNotFoundException($"The file '{file}' does not exist", file);
    if (this.thumbNail != null)
    {
      this.thumbNail.Dispose();
      this.thumbNail = (Bitmap) null;
    }
    ShellThumbnail.IShellFolder getDesktopFolder = this.GetDesktopFolder;
    if (getDesktopFolder != null)
    {
      IntPtr ppidl;
      try
      {
        int pchEaten = 0;
        int pdwAttributes = 0;
        string directoryName = Path.GetDirectoryName(file);
        ppidl = IntPtr.Zero;
        getDesktopFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, directoryName, out pchEaten, out ppidl, out pdwAttributes);
      }
      catch (Exception ex)
      {
        Marshal.ReleaseComObject((object) getDesktopFolder);
        throw ex;
      }
      if (ppidl != IntPtr.Zero)
      {
        Guid riid = new Guid("000214E6-0000-0000-C000-000000000046");
        ShellThumbnail.IShellFolder ppvOut = (ShellThumbnail.IShellFolder) null;
        try
        {
          getDesktopFolder.BindToObject(ppidl, IntPtr.Zero, ref riid, ref ppvOut);
        }
        catch (Exception ex)
        {
          Marshal.ReleaseComObject((object) getDesktopFolder);
          ShellThumbnail.UnmanagedMethods.CoTaskMemFree(ppidl);
          throw ex;
        }
        if (ppvOut != null)
        {
          ShellThumbnail.IEnumIDList ppenumIDList = (ShellThumbnail.IEnumIDList) null;
          try
          {
            ppvOut.EnumObjects(IntPtr.Zero, ShellThumbnail.ESHCONTF.SHCONTF_FOLDERS | ShellThumbnail.ESHCONTF.SHCONTF_NONFOLDERS, ref ppenumIDList);
          }
          catch (Exception ex)
          {
            Marshal.ReleaseComObject((object) getDesktopFolder);
            ShellThumbnail.UnmanagedMethods.CoTaskMemFree(ppidl);
            throw ex;
          }
          if (ppenumIDList != null)
          {
            IntPtr zero = IntPtr.Zero;
            int pceltFetched = 0;
            bool flag = false;
            while (!flag)
            {
              if (ppenumIDList.Next(1, ref zero, out pceltFetched) != 0)
              {
                zero = IntPtr.Zero;
                flag = true;
              }
              else if (this.GetThumbnail(file, zero, ppvOut, width, height))
                flag = true;
              if (zero != IntPtr.Zero)
                ShellThumbnail.UnmanagedMethods.CoTaskMemFree(zero);
            }
            Marshal.ReleaseComObject((object) ppenumIDList);
          }
          Marshal.ReleaseComObject((object) ppvOut);
        }
        ShellThumbnail.UnmanagedMethods.CoTaskMemFree(ppidl);
      }
      Marshal.ReleaseComObject((object) getDesktopFolder);
    }
    return this.thumbNail;
  }

  private bool GetThumbnail(
    string file,
    IntPtr pidl,
    ShellThumbnail.IShellFolder item,
    int width,
    int height)
  {
    IntPtr phBmpThumbnail = IntPtr.Zero;
    ShellThumbnail.IExtractImage o = (ShellThumbnail.IExtractImage) null;
    try
    {
      if (!Path.GetFileName(this.PathFromPidl(pidl)).ToUpper().Equals(Path.GetFileName(file).ToUpper()))
        return false;
      ShellThumbnail.IUnknown ppvOut = (ShellThumbnail.IUnknown) null;
      int prgfInOut = 0;
      Guid riid = new Guid("BB2E617C-0920-11d1-9A0B-00C04FC2D6C1");
      item.GetUIObjectOf(IntPtr.Zero, 1, ref pidl, ref riid, out prgfInOut, ref ppvOut);
      o = (ShellThumbnail.IExtractImage) ppvOut;
      if (o != null)
      {
        ShellThumbnail.SIZE prgSize = new ShellThumbnail.SIZE();
        prgSize.cx = width;
        prgSize.cy = height;
        StringBuilder pszPathBuffer = new StringBuilder(260, 260);
        int pdwPriority = 0;
        int dwRecClrDepth = 32 /*0x20*/;
        ShellThumbnail.EIEIFLAG eieiflag = ShellThumbnail.EIEIFLAG.IEIFLAG_ASPECT | ShellThumbnail.EIEIFLAG.IEIFLAG_SCREEN | ShellThumbnail.EIEIFLAG.IEIFLAG_QUALITY;
        if (width == 0 || height == 0)
          eieiflag |= ShellThumbnail.EIEIFLAG.IEIFLAG_ORIGSIZE;
        int pdwFlags = (int) eieiflag;
        o.GetLocation(pszPathBuffer, pszPathBuffer.Capacity, ref pdwPriority, ref prgSize, dwRecClrDepth, ref pdwFlags);
        o.Extract(out phBmpThumbnail);
        if (phBmpThumbnail != IntPtr.Zero)
          this.thumbNail = Image.FromHbitmap(phBmpThumbnail);
        Marshal.ReleaseComObject((object) o);
        o = (ShellThumbnail.IExtractImage) null;
      }
      return true;
    }
    catch (Exception ex)
    {
      if (phBmpThumbnail != IntPtr.Zero)
        ShellThumbnail.UnmanagedMethods.DeleteObject(phBmpThumbnail);
      if (o != null)
        Marshal.ReleaseComObject((object) o);
      throw ex;
    }
  }

  private string PathFromPidl(IntPtr pidl)
  {
    StringBuilder pszPath = new StringBuilder(260, 260);
    return ShellThumbnail.UnmanagedMethods.SHGetPathFromIDList(pidl, pszPath) == 0 ? string.Empty : pszPath.ToString();
  }

  private ShellThumbnail.IShellFolder GetDesktopFolder
  {
    get
    {
      ShellThumbnail.IShellFolder ppshf;
      ShellThumbnail.UnmanagedMethods.SHGetDesktopFolder(out ppshf);
      return ppshf;
    }
  }

  public void Dispose()
  {
    if (this.disposed)
      return;
    if (this.thumbNail != null)
      this.thumbNail.Dispose();
    this.disposed = true;
  }

  ~ShellThumbnail() => this.Dispose();

  [Flags]
  private enum ESTRRET
  {
    STRRET_WSTR = 0,
    STRRET_OFFSET = 1,
    STRRET_CSTR = 2,
  }

  [Flags]
  private enum ESHCONTF
  {
    SHCONTF_FOLDERS = 32, // 0x00000020
    SHCONTF_NONFOLDERS = 64, // 0x00000040
    SHCONTF_INCLUDEHIDDEN = 128, // 0x00000080
  }

  [Flags]
  private enum ESHGDN
  {
    SHGDN_NORMAL = 0,
    SHGDN_INFOLDER = 1,
    SHGDN_FORADDRESSBAR = 16384, // 0x00004000
    SHGDN_FORPARSING = 32768, // 0x00008000
  }

  [Flags]
  private enum ESFGAO
  {
    SFGAO_CANCOPY = 1,
    SFGAO_CANMOVE = 2,
    SFGAO_CANLINK = 4,
    SFGAO_CANRENAME = 16, // 0x00000010
    SFGAO_CANDELETE = 32, // 0x00000020
    SFGAO_HASPROPSHEET = 64, // 0x00000040
    SFGAO_DROPTARGET = 256, // 0x00000100
    SFGAO_CAPABILITYMASK = SFGAO_DROPTARGET | SFGAO_HASPROPSHEET | SFGAO_CANDELETE | SFGAO_CANRENAME | SFGAO_CANLINK | SFGAO_CANMOVE | SFGAO_CANCOPY, // 0x00000177
    SFGAO_LINK = 65536, // 0x00010000
    SFGAO_SHARE = 131072, // 0x00020000
    SFGAO_READONLY = 262144, // 0x00040000
    SFGAO_GHOSTED = 524288, // 0x00080000
    SFGAO_DISPLAYATTRMASK = SFGAO_GHOSTED | SFGAO_READONLY | SFGAO_SHARE | SFGAO_LINK, // 0x000F0000
    SFGAO_FILESYSANCESTOR = 268435456, // 0x10000000
    SFGAO_FOLDER = 536870912, // 0x20000000
    SFGAO_FILESYSTEM = 1073741824, // 0x40000000
    SFGAO_HASSUBFOLDER = -2147483648, // 0x80000000
    SFGAO_CONTENTSMASK = SFGAO_HASSUBFOLDER, // 0x80000000
    SFGAO_VALIDATE = 16777216, // 0x01000000
    SFGAO_REMOVABLE = 33554432, // 0x02000000
    SFGAO_COMPRESSED = 67108864, // 0x04000000
  }

  private enum EIEIFLAG
  {
    IEIFLAG_ASYNC = 1,
    IEIFLAG_CACHE = 2,
    IEIFLAG_ASPECT = 4,
    IEIFLAG_OFFLINE = 8,
    IEIFLAG_GLEAM = 16, // 0x00000010
    IEIFLAG_SCREEN = 32, // 0x00000020
    IEIFLAG_ORIGSIZE = 64, // 0x00000040
    IEIFLAG_NOSTAMP = 128, // 0x00000080
    IEIFLAG_NOBORDER = 256, // 0x00000100
    IEIFLAG_QUALITY = 512, // 0x00000200
  }

  [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
  private struct STRRET_CSTR
  {
    public ShellThumbnail.ESTRRET uType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 520)]
    public byte[] cStr;
  }

  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
  private struct STRRET_ANY
  {
    [FieldOffset(0)]
    public ShellThumbnail.ESTRRET uType;
    [FieldOffset(4)]
    public IntPtr pOLEString;
  }

  private struct SIZE
  {
    public int cx;
    public int cy;
  }

  [Guid("00000000-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  private interface IUnknown
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    IntPtr QueryInterface(ref Guid riid, out IntPtr pVoid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    IntPtr AddRef();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    IntPtr Release();
  }

  [Guid("000214F2-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  private interface IEnumIDList
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, ref IntPtr rgelt, out int pceltFetched);

    void Skip(int celt);

    void Reset();

    void Clone(ref ShellThumbnail.IEnumIDList ppenum);
  }

  [Guid("000214E6-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  private interface IShellFolder
  {
    void ParseDisplayName(
      IntPtr hwndOwner,
      IntPtr pbcReserved,
      [MarshalAs(UnmanagedType.LPWStr)] string lpszDisplayName,
      out int pchEaten,
      out IntPtr ppidl,
      out int pdwAttributes);

    void EnumObjects(
      IntPtr hwndOwner,
      [MarshalAs(UnmanagedType.U4)] ShellThumbnail.ESHCONTF grfFlags,
      ref ShellThumbnail.IEnumIDList ppenumIDList);

    void BindToObject(
      IntPtr pidl,
      IntPtr pbcReserved,
      ref Guid riid,
      ref ShellThumbnail.IShellFolder ppvOut);

    void BindToStorage(IntPtr pidl, IntPtr pbcReserved, ref Guid riid, IntPtr ppvObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CompareIDs(IntPtr lParam, IntPtr pidl1, IntPtr pidl2);

    void CreateViewObject(IntPtr hwndOwner, ref Guid riid, IntPtr ppvOut);

    void GetAttributesOf(int cidl, IntPtr apidl, [MarshalAs(UnmanagedType.U4)] ref ShellThumbnail.ESFGAO rgfInOut);

    void GetUIObjectOf(
      IntPtr hwndOwner,
      int cidl,
      ref IntPtr apidl,
      ref Guid riid,
      out int prgfInOut,
      ref ShellThumbnail.IUnknown ppvOut);

    void GetDisplayNameOf(
      IntPtr pidl,
      [MarshalAs(UnmanagedType.U4)] ShellThumbnail.ESHGDN uFlags,
      ref ShellThumbnail.STRRET_CSTR lpName);

    void SetNameOf(
      IntPtr hwndOwner,
      IntPtr pidl,
      [MarshalAs(UnmanagedType.LPWStr)] string lpszName,
      [MarshalAs(UnmanagedType.U4)] ShellThumbnail.ESHCONTF uFlags,
      ref IntPtr ppidlOut);
  }

  [Guid("BB2E617C-0920-11d1-9A0B-00C04FC2D6C1")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  private interface IExtractImage
  {
    void GetLocation(
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszPathBuffer,
      int cch,
      ref int pdwPriority,
      ref ShellThumbnail.SIZE prgSize,
      int dwRecClrDepth,
      ref int pdwFlags);

    void Extract(out IntPtr phBmpThumbnail);
  }

  private class UnmanagedMethods
  {
    [DllImport("ole32", CharSet = CharSet.Auto)]
    internal static extern void CoTaskMemFree(IntPtr ptr);

    [DllImport("shell32", CharSet = CharSet.Auto)]
    internal static extern int SHGetDesktopFolder(out ShellThumbnail.IShellFolder ppshf);

    [DllImport("shell32", CharSet = CharSet.Auto)]
    internal static extern int SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    internal static extern int DeleteObject(IntPtr hObject);
  }
}
