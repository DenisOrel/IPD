
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.WindowsThumbnailProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class WindowsThumbnailProvider
{
  private const string IShellItem2Guid = "7E9FB0D3-919F-4307-AB2E-9B1860310C93";

  [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
  internal static extern int SHCreateItemFromParsingName(
    [MarshalAs(UnmanagedType.LPWStr)] string path,
    IntPtr pbc,
    ref Guid riid,
    [MarshalAs(UnmanagedType.Interface)] out WindowsThumbnailProvider.IShellItem shellItem);

  [DllImport("gdi32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static extern bool DeleteObject(IntPtr hObject);

  public static Bitmap GetThumbnail(
    string fileName,
    int width,
    int height,
    ThumbnailOptions options)
  {
    IntPtr hbitmap = WindowsThumbnailProvider.GetHBitmap(Path.GetFullPath(fileName), width, height, options);
    try
    {
      return WindowsThumbnailProvider.GetBitmapFromHBitmap(hbitmap);
    }
    finally
    {
      WindowsThumbnailProvider.DeleteObject(hbitmap);
    }
  }

  public static Bitmap GetBitmapFromHBitmap(IntPtr nativeHBitmap)
  {
    Bitmap srcBitmap = Image.FromHbitmap(nativeHBitmap);
    return Image.GetPixelFormatSize(srcBitmap.PixelFormat) < 32 /*0x20*/ ? srcBitmap : WindowsThumbnailProvider.CreateAlphaBitmap(srcBitmap, PixelFormat.Format32bppArgb);
  }

  public static Bitmap CreateAlphaBitmap(Bitmap srcBitmap, PixelFormat targetPixelFormat)
  {
    Bitmap bitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height, targetPixelFormat);
    Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
    BitmapData bitmapdata = srcBitmap.LockBits(rect, ImageLockMode.ReadOnly, srcBitmap.PixelFormat);
    bool flag = false;
    try
    {
      for (int y = 0; y <= bitmapdata.Height - 1; ++y)
      {
        for (int x = 0; x <= bitmapdata.Width - 1; ++x)
        {
          Color color = Color.FromArgb(Marshal.ReadInt32(bitmapdata.Scan0, bitmapdata.Stride * y + 4 * x));
          if (color.A > (byte) 0 & color.A < byte.MaxValue)
            flag = true;
          bitmap.SetPixel(x, y, color);
        }
      }
    }
    finally
    {
      srcBitmap.UnlockBits(bitmapdata);
    }
    return !flag ? srcBitmap : bitmap;
  }

  private static IntPtr GetHBitmap(
    string fileName,
    int width,
    int height,
    ThumbnailOptions options)
  {
    Guid riid = new Guid("7E9FB0D3-919F-4307-AB2E-9B1860310C93");
    WindowsThumbnailProvider.IShellItem shellItem;
    int itemFromParsingName = WindowsThumbnailProvider.SHCreateItemFromParsingName(fileName, IntPtr.Zero, ref riid, out shellItem);
    if (itemFromParsingName != 0)
      throw Marshal.GetExceptionForHR(itemFromParsingName);
    WindowsThumbnailProvider.NativeSize size = new WindowsThumbnailProvider.NativeSize()
    {
      Width = width,
      Height = height
    };
    IntPtr phbm;
    WindowsThumbnailProvider.HResult image = ((WindowsThumbnailProvider.IShellItemImageFactory) shellItem).GetImage(size, options, out phbm);
    Marshal.ReleaseComObject((object) shellItem);
    if (image == WindowsThumbnailProvider.HResult.Ok)
      return phbm;
    throw Marshal.GetExceptionForHR((int) image);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
  [ComImport]
  internal interface IShellItem
  {
    void BindToHandler(IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid bhid, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv);

    void GetParent(out WindowsThumbnailProvider.IShellItem ppsi);

    void GetDisplayName(WindowsThumbnailProvider.SIGDN sigdnName, out IntPtr ppszName);

    void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

    void Compare(WindowsThumbnailProvider.IShellItem psi, uint hint, out int piOrder);
  }

  internal enum SIGDN : uint
  {
    NORMALDISPLAY = 0,
    PARENTRELATIVEPARSING = 2147581953, // 0x80018001
    PARENTRELATIVEFORADDRESSBAR = 2147598337, // 0x8001C001
    DESKTOPABSOLUTEPARSING = 2147647488, // 0x80028000
    PARENTRELATIVEEDITING = 2147684353, // 0x80031001
    DESKTOPABSOLUTEEDITING = 2147794944, // 0x8004C000
    FILESYSPATH = 2147844096, // 0x80058000
    URL = 2147909632, // 0x80068000
  }

  internal enum HResult
  {
    NoInterface = -2147467262, // 0x80004002
    Fail = -2147467259, // 0x80004005
    TypeElementNotFound = -2147319765, // 0x8002802B
    AccessDenied = -2147287035, // 0x80030005
    NoObject = -2147221019, // 0x800401E5
    OutOfMemory = -2147024882, // 0x8007000E
    InvalidArguments = -2147024809, // 0x80070057
    ResourceInUse = -2147024726, // 0x800700AA
    ElementNotFound = -2147023728, // 0x80070490
    Canceled = -2147023673, // 0x800704C7
    Ok = 0,
    False = 1,
    Win32ErrorCanceled = 1223, // 0x000004C7
  }

  [Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IShellItemImageFactory
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    WindowsThumbnailProvider.HResult GetImage(
      [MarshalAs(UnmanagedType.Struct), In] WindowsThumbnailProvider.NativeSize size,
      [In] ThumbnailOptions flags,
      out IntPtr phbm);
  }

  internal struct NativeSize
  {
    private int width;
    private int height;

    public int Width
    {
      set => this.width = value;
    }

    public int Height
    {
      set => this.height = value;
    }
  }

  public struct RGBQUAD
  {
    public byte rgbBlue;
    public byte rgbGreen;
    public byte rgbRed;
    public byte rgbReserved;
  }
}
