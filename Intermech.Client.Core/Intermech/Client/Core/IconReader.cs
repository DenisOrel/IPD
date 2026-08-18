
// Type: Intermech.Client.Core.IconReader
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core;

/// <summary>Чтение икон из registry для типов файлов</summary>
public class IconReader : IIconReader
{
  private const int SHGFI_ICON = 256 /*0x0100*/;
  private const int SHGFI_SMALLICON = 1;
  private const int SHGFI_LARGEICON = 0;
  private const int SHGFI_OPENICON = 2;
  private const int SHGFI_LINKOVERLAY = 32768 /*0x8000*/;
  private const int SHGFI_SELECTED = 65536 /*0x010000*/;
  private const int SHGFI_TYPENAME = 1024 /*0x0400*/;
  private const int SHGFI_USEFILEATTRIBUTES = 16 /*0x10*/;
  private Hashtable hashtable = new Hashtable();

  [DllImport("gdi32")]
  private static extern void DeleteObject(IntPtr lObject);

  [DllImport("shell32.dll")]
  private static extern int SHGetFileInfo(
    string pszPath,
    int dwAttributes,
    ref IconReader.SHFILEINFO psfi,
    int cbSizeFileInfo,
    int uFlags);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool DestroyIcon(IntPtr handle);

  /// <summary>
  /// вернуть икону, зарегистрированную на тип или файл в винде.
  /// </summary>
  /// <param name="strFilePath"></param>
  /// <param name="largeIcon"></param>
  /// <param name="extensionOnly"></param>
  /// <returns></returns>
  public static Icon GetIcon(string strFilePath, bool largeIcon, bool extensionOnly)
  {
    IconReader.SHFILEINFO psfi = new IconReader.SHFILEINFO();
    int uFlags = 256 /*0x0100*/ | (largeIcon ? 0 : 1);
    if (extensionOnly)
      uFlags |= 16 /*0x10*/;
    Icon icon = (Icon) null;
    if (IconReader.SHGetFileInfo(strFilePath, 128 /*0x80*/, ref psfi, Marshal.SizeOf<IconReader.SHFILEINFO>(psfi), uFlags) != 0)
    {
      IntPtr num = new IntPtr(psfi.hIcon);
      if (!num.Equals((object) 0))
      {
        using (Icon original = Icon.FromHandle(num))
        {
          icon = new Icon(original, original.Width, original.Height);
          IconReader.DestroyIcon(original.Handle);
        }
      }
      IconReader.DeleteObject(num);
    }
    return icon;
  }

  public void Clear() => this.hashtable.Clear();

  public Icon GetIconByFileExt(string ext)
  {
    string lower = ext.ToLower();
    Icon icon = (Icon) this.hashtable[(object) lower];
    if (icon == null)
    {
      icon = IconReader.GetIcon(lower, false, true);
      if (icon != null)
        this.hashtable.Add((object) lower, (object) icon);
    }
    return icon;
  }

  private struct SHFILEINFO
  {
    public int hIcon;
    public int iIcon;
    public int dwAttributes;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256 /*0x0100*/)]
    public string szDisplayName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80 /*0x50*/)]
    public string szTypeName;
  }
}
