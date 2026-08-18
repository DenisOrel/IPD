// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.BrowseFileFolderDialog.IconHolder
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.GTC.Server.BrowseFileFolderDialog;

internal class IconHolder
{
  private static Dictionary<string, Icon> _extensionIconsDict = new Dictionary<string, Icon>();
  private static Icon _folderIcon = IconHolder.IconHelper.GetSmallFolderIcon();

  public static Icon GetIcon(FileFolderEnum itemType, string filePath)
  {
    Icon icon = (Icon) null;
    switch (itemType)
    {
      case FileFolderEnum.Drive:
        icon = IconHolder.IconHelper.GetSmallIcon(filePath);
        break;
      case FileFolderEnum.Folder:
        icon = IconHolder._folderIcon;
        break;
      case FileFolderEnum.File:
        string str = Path.GetExtension(filePath);
        if (!IconHolder._extensionIconsDict.TryGetValue(str, out icon))
        {
          icon = str != string.Empty ? IconHolder.IconHelper.GetSmallIconFromExtension(str) : IconHolder.IconHelper.GetSmallFolderIcon();
          IconHolder._extensionIconsDict.Add(str, icon);
          break;
        }
        break;
    }
    return icon;
  }

  private static class IconHelper
  {
    public static Icon GetSmallFolderIcon() => IconHolder.IconHelper.GetIcon("folder", 17U, true);

    public static Icon GetSmallIcon(string fileName) => IconHolder.IconHelper.GetIcon(fileName, 1U);

    public static Icon GetSmallIconFromExtension(string @extension)
    {
      return IconHolder.IconHelper.GetIcon(@extension, 17U);
    }

    private static Icon GetIcon(string name, uint flags, bool isFolder = false)
    {
      Icon icon = (Icon) null;
      try
      {
        IconHolder.IconHelper.Shfileinfo psfi = new IconHolder.IconHelper.Shfileinfo();
        if (IconHolder.IconHelper.Shell32.SHGetFileInfo(name, isFolder ? 16U /*0x10*/ : 128U /*0x80*/, ref psfi, (uint) Marshal.SizeOf<IconHolder.IconHelper.Shfileinfo>(psfi), 256U /*0x0100*/ | flags) == IntPtr.Zero)
          throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
        icon = (Icon) Icon.FromHandle(psfi.hIcon).Clone();
        IconHolder.IconHelper.User32.DestroyIcon(psfi.hIcon);
      }
      catch
      {
      }
      return icon;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct Shfileinfo
    {
      public IntPtr hIcon;
      private int iIcon;
      private uint dwAttributes;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
      private string szDisplayName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80 /*0x50*/)]
      private string szTypeName;
    }

    private static class Shell32
    {
      public const uint ShgfiIcon = 256 /*0x0100*/;
      public const uint ShgfiSmallicon = 1;
      public const uint FileAttributeNormal = 128 /*0x80*/;
      public const uint FileAttributeDirectory = 16 /*0x10*/;

      [DllImport("shell32.dll")]
      public static extern IntPtr SHGetFileInfo(
        string pszPath,
        uint dwFileAttributes,
        ref IconHolder.IconHelper.Shfileinfo psfi,
        uint cbSizeFileInfo,
        uint uFlags);
    }

    private static class User32
    {
      [DllImport("user32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool DestroyIcon(IntPtr hIcon);
    }
  }
}
