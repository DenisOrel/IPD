
// Type: Intermech.ShellLink.FileIcon
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.ShellLink;

/// <summary>
/// Enables extraction of icons for any file type from
/// the Shell.
/// </summary>
public class FileIcon
{
  private const int MAX_PATH = 260;
  private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256 /*0x0100*/;
  private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192 /*0x2000*/;
  private const int FORMAT_MESSAGE_FROM_HMODULE = 2048 /*0x0800*/;
  private const int FORMAT_MESSAGE_FROM_STRING = 1024 /*0x0400*/;
  private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096 /*0x1000*/;
  private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512 /*0x0200*/;
  private const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 255 /*0xFF*/;
  private string fileName;
  private string displayName;
  private string typeName;
  private FileIcon.SHGetFileInfoConstants flags;
  private Icon fileIcon;

  [DllImport("shell32")]
  private static extern int SHGetFileInfo(
    string pszPath,
    int dwFileAttributes,
    ref FileIcon.SHFILEINFO psfi,
    uint cbFileInfo,
    uint uFlags);

  [DllImport("user32.dll")]
  private static extern int DestroyIcon(IntPtr hIcon);

  [DllImport("kernel32")]
  private static extern int FormatMessage(
    int dwFlags,
    IntPtr lpSource,
    int dwMessageId,
    int dwLanguageId,
    string lpBuffer,
    uint nSize,
    int argumentsLong);

  [DllImport("kernel32")]
  private static extern int GetLastError();

  /// <summary>Gets/sets the flags used to extract the icon</summary>
  public FileIcon.SHGetFileInfoConstants Flags
  {
    get => this.flags;
    set => this.flags = value;
  }

  /// <summary>Gets/sets the filename to get the icon for</summary>
  public string FileName
  {
    get => this.fileName;
    set => this.fileName = value;
  }

  /// <summary>Gets the icon for the chosen file</summary>
  public Icon ShellIcon => this.fileIcon;

  /// <summary>
  /// Gets the display name for the selected file
  /// if the SHGFI_DISPLAYNAME flag was set.
  /// </summary>
  public string DisplayName => this.displayName;

  /// <summary>
  /// Gets the type name for the selected file
  /// if the SHGFI_TYPENAME flag was set.
  /// </summary>
  public string TypeName => this.typeName;

  /// <summary>
  ///  Gets the information for the specified
  ///  file name and flags.
  /// </summary>
  public void GetInfo()
  {
    this.fileIcon = (Icon) null;
    this.typeName = "";
    this.displayName = "";
    FileIcon.SHFILEINFO psfi = new FileIcon.SHFILEINFO();
    uint cbFileInfo = (uint) Marshal.SizeOf(psfi.GetType());
    if (FileIcon.SHGetFileInfo(this.fileName, 0, ref psfi, cbFileInfo, (uint) this.flags) != 0)
    {
      if (psfi.hIcon != IntPtr.Zero)
        this.fileIcon = Icon.FromHandle(psfi.hIcon);
      this.typeName = psfi.szTypeName;
      this.displayName = psfi.szDisplayName;
    }
    else
    {
      int lastError = FileIcon.GetLastError();
      Console.WriteLine("Error {0}", (object) lastError);
      string lpBuffer = new string(char.MinValue, 256 /*0x0100*/);
      Console.WriteLine("Len {0} text {1}", (object) FileIcon.FormatMessage(4608, IntPtr.Zero, lastError, 0, lpBuffer, 256U /*0x0100*/, 0), (object) lpBuffer);
    }
  }

  /// <summary>
  /// Constructs a new, default instance of the FileIcon
  /// class.  Specify the filename and call GetInfo()
  /// to retrieve an icon.
  /// </summary>
  public FileIcon()
  {
    this.flags = FileIcon.SHGetFileInfoConstants.SHGFI_ICON | FileIcon.SHGetFileInfoConstants.SHGFI_DISPLAYNAME | FileIcon.SHGetFileInfoConstants.SHGFI_TYPENAME | FileIcon.SHGetFileInfoConstants.SHGFI_ATTRIBUTES | FileIcon.SHGetFileInfoConstants.SHGFI_EXETYPE;
  }

  /// <summary>
  /// Constructs a new instance of the FileIcon class
  /// and retrieves the icon, display name and type name
  /// for the specified file.
  /// </summary>
  /// <param name="fileName">The filename to get the icon,
  /// display name and type name for</param>
  public FileIcon(string fileName)
    : this()
  {
    this.fileName = fileName;
    this.GetInfo();
  }

  /// <summary>
  /// Constructs a new instance of the FileIcon class
  /// and retrieves the information specified in the
  /// flags.
  /// </summary>
  /// <param name="fileName">The filename to get information
  /// for</param>
  /// <param name="flags">The flags to use when extracting the
  /// icon and other shell information.</param>
  public FileIcon(string fileName, FileIcon.SHGetFileInfoConstants flags)
  {
    this.fileName = fileName;
    this.flags = flags;
    this.GetInfo();
  }

  private struct SHFILEINFO
  {
    public IntPtr hIcon;
    public int iIcon;
    public int dwAttributes;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string szDisplayName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80 /*0x50*/)]
    public string szTypeName;
  }

  [System.Flags]
  public enum SHGetFileInfoConstants
  {
    SHGFI_ICON = 256, // 0x00000100
    SHGFI_DISPLAYNAME = 512, // 0x00000200
    SHGFI_TYPENAME = 1024, // 0x00000400
    SHGFI_ATTRIBUTES = 2048, // 0x00000800
    SHGFI_ICONLOCATION = 4096, // 0x00001000
    SHGFI_EXETYPE = 8192, // 0x00002000
    SHGFI_SYSICONINDEX = 16384, // 0x00004000
    SHGFI_LINKOVERLAY = 32768, // 0x00008000
    SHGFI_SELECTED = 65536, // 0x00010000
    SHGFI_ATTR_SPECIFIED = 131072, // 0x00020000
    SHGFI_LARGEICON = 0,
    SHGFI_SMALLICON = 1,
    SHGFI_OPENICON = 2,
    SHGFI_SHELLICONSIZE = 4,
    SHGFI_USEFILEATTRIBUTES = 16, // 0x00000010
    SHGFI_ADDOVERLAYS = 32, // 0x00000020
    SHGFI_OVERLAYINDEX = 64, // 0x00000040
  }
}
