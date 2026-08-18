
// Type: Intermech.WindowsDll.Shell32
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.WindowsDll
{
    public static class Shell32
    {
      public static Type ShellFolderType = typeof (IShellFolder);
      public static Guid IID_IShellFolder = new Guid("{000214E6-0000-0000-C000-000000000046}");
      /// <summary>Maximal Length of unmanaged Windows-Path-strings</summary>
      public const int MAX_PATH = 260;
      /// <summary>Maximal Length of unmanaged Typename</summary>
      public const int MAX_TYPE = 80 /*0x50*/;

      /// <summary>Retrieves the path of a folder as an PIDL</summary>
      /// <param name="hwndOwner">Handle to the owner window</param>
      /// <param name="nFolder">A CSIDL value that identifies the folder to be located</param>
      /// <param name="hToken">Token that can be used to represent a particular user</param>
      /// <param name="dwReserved">The reserved</param>
      /// <param name="ppidl">[out]
      /// Address of a pointer to an item identifier list structure specifying the folder's location
      /// relative to the root of the namespace (the desktop)/.
      /// </param>
      /// <returns>HRESULT</returns>
      [DllImport("shell32.dll")]
      public static extern int SHGetFolderLocation(
        IntPtr hwndOwner,
        int nFolder,
        IntPtr hToken,
        [MarshalAs(UnmanagedType.U4)] int dwReserved,
        out IntPtr ppidl);

      /// <summary>Destroys the icon described by handle</summary>
      /// <param name="handle">A handle to the icon to be destroyed. The icon must not be in use</param>
      /// <returns>True if it succeeds, false if it fails</returns>
      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern bool DestroyIcon(IntPtr handle);

      /// <summary>Retrieves the IShellFolder interface for the desktop folder, which is the root of the Shell's namespace.</summary>
      /// <param name="ppshf">[out] Address that receives an <see cref="T:Intermech.WindowsDll.Shell32.IShellFolder" /> interface pointer for the desktop folder</param>
      /// <returns>HRESULT</returns>
      [DllImport("shell32.dll")]
      public static extern int SHGetDesktopFolder(out IntPtr ppshf);

      /// <summary>
      /// Takes a STRRET structure returned by <see cref="M:Intermech.WindowsDll.Shell32.IShellFolder.GetDisplayNameOf(System.IntPtr,Intermech.WindowsDll.Shell32.ESHGDN,Intermech.WindowsDll.Shell32.STRRET@)" />, converts it to a string,
      /// and places the result in a buffer.
      /// </summary>
      /// <param name="pstr">
      /// [in,out] Pointer to the <see cref="T:Intermech.WindowsDll.Shell32.STRRET" /> structure.
      /// When the function returns, this pointer will no longer be valid.
      /// </param>
      /// <param name="pidl">Pointer to the item's <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure</param>
      /// <param name="pszBuf">
      /// Buffer to hold the display name.
      /// It will be returned as a null-terminated string.
      /// If cchBuf is too small, the name will be truncated to fit.
      /// </param>
      /// <param name="cchBuf">Size of pszBuf, in characters. If cchBuf is too small, the string will be truncated to fit</param>
      /// <returns>HRESULT</returns>
      [DllImport("shlwapi.dll")]
      public static extern int StrRetToBuf(
        ref STRRET pstr,
        IntPtr pidl,
        StringBuilder pszBuf,
        [MarshalAs(UnmanagedType.U4)] int cchBuf);

      /// <summary>Retrieves information about an object in the file system, such as a file, folder, directory, or drive root</summary>
      /// <param name="pszPath">
      /// A pointer to a null-terminated string of maximum length <see cref="F:Intermech.WindowsDll.Shell32.MAX_PATH" /> that contains the path and file name.
      /// Both absolute and relative paths are valid.
      /// </param>
      /// <param name="dwFileAttribs">
      /// A combination of one or more file attribute flags (FILE_ATTRIBUTE_ values as defined in Winnt.h).
      /// If uFlags does not include the <see cref="!:SHGFI.SHGFI_USEFILEATTRIBUTES" /> flag, this parameter is ignored.</param>
      /// <param name="psfi">[out] Pointer to a <see cref="T:Intermech.WindowsDll.Shell32.SHFILEINFO" /> structure to receive the file information</param>
      /// <param name="cbFileInfo">The size, in bytes, of the <see cref="T:Intermech.WindowsDll.Shell32.SHFILEINFO" /> structure pointed to by the psfi parameter</param>
      /// <param name="uFlags">The flags that specify the file information to retrieve.</param>
      /// <returns>
      /// Returns a value whose meaning depends on the uFlags parameter.
      /// 
      /// If <see cref="!:uFlags" /> does not contain <see cref="!:SHGFI.SHGFI_EXETYPE" /> or <see cref="!:SHGFI.SHGFI_SYSICONINDEX" />,
      ///     the return value is nonzero if successful, or zero otherwise.
      /// 
      /// If <see cref="!:uFlags" /> contains the <see cref="!:SHGFI.SHGFI_EXETYPE" /> flag, the return value specifies the type of the executable file.
      ///    It will be one of the following values:
      /// 0 -- Nonexecutable file or an error condition.
      /// LOWORD = NE or PE and HIWORD = Windows version -- Windows application.
      /// LOWORD = MZ and HIWORD = 0 -- MS-DOS .exe or .com file
      /// LOWORD = PE and HIWORD = 0 -- Console application or .bat file
      /// </returns>
      /// <example>
      /// 1) Get file type name:
      /// <code>
      /// Shell32.SHFILEINFO shinfo = new Shell32.SHFILEINFO();
      /// IntPtr hSuccess = Shell32.SHGetFileInfo(extension,
      ///                                         0,
      ///                                         ref shinfo,
      ///                                         Marshal.SizeOf(shinfo),
      ///                                         Shell32.SHGFI_TYPENAME | Shell32.SHGFI_USEFILEATTRIBUTES);
      /// if (hSuccess != IntPtr.Zero)
      /// {
      ///     return Convert.ToString(shinfo.szTypeName.Trim());
      /// }
      /// </code>
      /// 
      /// 2) Get file icon:
      /// <code>
      /// public static System.Drawing.Icon GetFileIcon(string name, IconSize size, bool linkOverlay)
      /// {
      ///     Shell32.SHFILEINFO shfi = new Shell32.SHFILEINFO();
      ///     int flags = Shell32.SHGFI_ICON | Shell32.SHGFI_USEFILEATTRIBUTES;
      /// 
      ///     if (true == linkOverlay)
      ///         flags += Shell32.SHGFI_LINKOVERLAY;
      /// 
      /// 
      ///     /* Check the size specified for return. */
      ///     if (IconSize.Small == size)
      ///         flags += Shell32.SHGFI_SMALLICON ; // include the small icon flag
      ///     else
      ///         flags += Shell32.SHGFI_LARGEICON ;  // include the large icon flag
      /// 
      ///     Shell32.SHGetFileInfo( name,
      ///                            Shell32.FILE_ATTRIBUTE_NORMAL,
      ///                            ref shfi,
      ///                            Marshal.SizeOf(shfi),
      ///                            flags );
      /// 
      /// 
      ///     // Copy (clone) the returned icon to a new object, thus allowing us
      ///     // to call DestroyIcon immediately
      ///     if (shfi.hIcon == IntPtr.Zero)
      ///         return null;
      ///     else
      ///     {
      ///         System.Drawing.Icon icon = (System.Drawing.Icon) System.Drawing.Icon.FromHandle(shfi.hIcon).Clone();
      ///         User32.DestroyIcon( shfi.hIcon ); // Cleanup
      ///         return icon;
      ///     }
      /// }
      /// </code>
      /// </example>
      [DllImport("shell32.dll")]
      public static extern IntPtr SHGetFileInfo(
        [NotNull, FileExists] string pszPath,
        [MarshalAs(UnmanagedType.U4)] int dwFileAttribs,
        [NotNull, Out] SHFILEINFO psfi,
        [MarshalAs(UnmanagedType.U4)] int cbFileInfo,
        [MarshalAs(UnmanagedType.U4)] SHGFI uFlags);

      /// <summary>Retrieves information about an object in the file system, such as a file, folder, directory, or drive root</summary>
      [DllImport("shell32.dll")]
      public static extern IntPtr SHGetFileInfo(
        IntPtr pIDL,
        [MarshalAs(UnmanagedType.U4)] int dwFileAttributes,
        [NotNull, Out] SHFILEINFO psfi,
        [MarshalAs(UnmanagedType.U4)] int cbFileInfo,
        SHGFI uFlags);

      /// <summary>Combines two <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structures</summary>
      /// <param name="pIDLParent">A pointer to the first <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure</param>
      /// <param name="pIDLChild">A pointer to the second <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure. This structure is appended to the structure pointed to by <see cref="!:pidl1" /></param>
      /// <returns>
      /// Returns an <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> containing the combined structures.
      /// If you set either <see cref="!:pidl1" /> or <see cref="!:pidl2" /> to NULL, the returned <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure is a clone of the non-NULL parameter.
      /// Returns NULL if <see cref="!:pidl1" /> and <see cref="!:pidl2" /> are both set to NULL.
      /// </returns>
      [DllImport("shell32.dll")]
      public static extern IntPtr ILCombine(IntPtr pidl1, IntPtr pidl2);

      [DllImport("shell32.dll")]
      public static extern void ILFree([In] IntPtr pidl);

      [CanBeNull]
      public static IShellFolder GetDesktopFolder()
      {
        IntPtr ppshf;
        Shell32.SHGetDesktopFolder(out ppshf);
        return (IShellFolder) Marshal.GetTypedObjectForIUnknown(ppshf, Shell32.ShellFolderType);
      }

      /// <summary>
      /// Retrieves information about system-defined Shell icons.
      /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetstockiconinfo" />
      /// </summary>
      /// <param name="siid">One of the values from the <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONID" /> enumeration that specifies which icon should be retrieved.</param>
      /// <param name="uFlags">A combination of zero or more of the flags from <see cref="T:Intermech.WindowsDll.Shell32.SHGSI" /> enumeration that specify which information is requested.</param>
      /// <param name="psii">[in,out]
      /// A pointer to a <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO" /> structure.
      /// When this function is called, the <see cref="F:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO.Size" /> member of this structure needs to be set to the size of the <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO" /> structure.
      /// When this function returns, contains a pointer to a <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO" /> structure that contains the requested information.
      /// </param>
      /// <remarks>
      /// If this function returns an icon handle in the hIcon member of the <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO" /> structure pointed to by psii,
      ///     you are responsible for freeing the icon with <see cref="M:Intermech.WindowsDll.Shell32.DestroyIcon(System.IntPtr)" /> when you no longer need it.
      /// </remarks>
      [DllImport("Shell32.dll")]
      public static extern int SHGetStockIconInfo(
        [MarshalAs(UnmanagedType.U4)] SHSTOCKICONID siid,
        [MarshalAs(UnmanagedType.U4)] SHGSI uFlags,
        [NotNull, In, Out] SHSTOCKICONINFO psii);

      /// <summary>Contains information about a file object</summary>
      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
      public class SHFILEINFO
      {
        /// <summary>
        /// A handle to the icon that represents the file.
        /// You are responsible for destroying this handle with DestroyIcon when you no longer need it.
        /// </summary>
        public IntPtr hIcon = IntPtr.Zero;
        /// <summary>The index of the icon image within the system image list</summary>
        public int iIcon;
        /// <summary>
        /// An array of values that indicates the attributes of the file object.
        /// For information about these values, see the <see cref="M:Intermech.WindowsDll.Shell32.IShellFolder.GetAttributesOf(System.Int32,System.IntPtr[],Intermech.WindowsDll.Shell32.ESFGAO@)" /> method.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int dwAttributes;
        /// <summary>
        /// A string that contains the name of the file as it appears in the Windows Shell,
        /// or the path and file name of the file that contains the icon representing the file.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName = "";
        /// <summary>A string that describes the type of file</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80 /*0x50*/)]
        public string szTypeName = "";
      }

      /// <summary>Contains a list of item identifiers</summary>
      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
      public struct ITEMIDLIST
      {
        /// <summary>A list of item identifiers</summary>
        [MarshalAs(UnmanagedType.Struct)]
        public SHITEMID mkid;
      }

      /// <summary>Defines an item identifier </summary>
      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
      public struct SHITEMID
      {
        /// <summary>The size of identifier, in bytes, including cb itself</summary>
        [MarshalAs(UnmanagedType.U2)]
        public short cb;
        /// <summary>A variable-length item identifier</summary>
        public byte[] abID;
      }

      /// <summary>
      ///  Managed equivalent of IShellFolder interface
      ///  Msdn:      <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb775075(v=vs.85).aspx" />
      ///  Pinvoke:   <see href="http://pinvoke.net/default.aspx/Interfaces/IShellFolder.html" />
      /// </summary>
      [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      [Guid("000214E6-0000-0000-C000-000000000046")]
      [ComImport]
      public interface IShellFolder
      {
        /// <summary>
        /// Translates a file object's or folder's display name into an item identifier list.
        /// </summary>
        /// <param name="hwnd">Optional window handle</param>
        /// <param name="pbc">Optional bind context that controls the parsing operation. This parameter is normally set to NULL</param>
        /// <param name="pszDisplayName">Null-terminated UNICODE string with the display name</param>
        /// <param name="pchEaten">Pointer to a ULONG value that receives the number of characters of the display name that was parsed</param>
        /// <param name="ppidl"> Pointer to an <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> pointer that receives the item identifier list for the object</param>
        /// <param name="pdwAttributes">Optional parameter that can be used to query for file attributes.this can be values from the SFGAO enum</param>
        int ParseDisplayName(
          IntPtr hwnd,
          IntPtr pbc,
          string pszDisplayName,
          IntPtr pchEaten,
          out IntPtr ppidl,
          IntPtr pdwAttributes);

        /// <summary>
        /// Allows a client to determine the contents of a folder by creating an item identifier enumeration object and returning its IEnumIDList interface.
        /// </summary>
        /// <param name="hwnd">
        /// If user input is required to perform the enumeration, this window handle should be used by the enumeration object as the parent window to take user input.
        /// </param>
        /// <param name="grfFlags">Flags indicating which items to include in the  enumeration. For a list of possible values, see the SHCONTF enum</param>
        /// <param name="ppenumIDList">Address that receives a pointer to the IEnumIDList interface of the enumeration object created by this method</param>
        int EnumObjects(IntPtr hwnd, ESHCONTF grfFlags, out IntPtr ppenumIDList);

        /// <summary>Retrieves an IShellFolder object for a subfolder</summary>
        /// <param name="pidl">Address of an <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure (PIDL) that identifies the subfolder</param>
        /// <param name="pbc">Optional address of an IBindCtx interface on a bind context object to be used during this operation</param>
        /// <param name="riid">[in,out] Identifier of the interface to return</param>
        /// <param name="ppv">[out] Address that receives the interface pointer</param>
        int BindToObject(IntPtr pidl, IntPtr pbc, [In] ref Guid riid, out IntPtr ppv);

        /// <summary>Requests a pointer to an object's storage interface.</summary>
        /// <param name="pidl">Address of an <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure that identifies the subfolder relative to its parent folder</param>
        /// <param name="pbc">Optional address of an IBindCtx interface on a bind context object to be  used during this operation</param>
        /// <param name="riid">Interface identifier (IID) of the requested storage interface</param>
        /// <param name="ppv"> Address that receives the interface pointer specified by riid</param>
        int BindToStorage(IntPtr pidl, IntPtr pbc, [In] ref Guid riid, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int CompareIDs(int lParam, IntPtr pidl1, IntPtr pidl2);

        /// <summary>
        /// Requests an object that can be used to obtain information from or interact with a folder object.
        /// </summary>
        /// <param name="hwndOwner">Handle to the owner window</param>
        /// <param name="riid">Identifier of the requested interface</param>
        /// <param name="ppv">Address of a pointer to the requested interface</param>
        int CreateViewObject(IntPtr hwndOwner, [In] ref Guid riid, out IntPtr ppv);

        /// <summary>
        /// Retrieves the attributes of one or more file objects or subfolders.
        /// </summary>
        /// <param name="cidl">Number of file objects from which to retrieve attributes</param>
        /// <param name="apidl">Address of an array of pointers to <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structures, each of which  uniquely identifies a file object relative to the parent folder</param>
        /// <param name="rgfInOut">Address of a single ULONG value that, on entry contains the attributes that the caller is
        /// requesting. On exit, this value contains the requested attributes that are common to all of the specified objects. this value can be from the SFGAO enum
        /// </param>
        int GetAttributesOf([MarshalAs(UnmanagedType.U4)] int cidl, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, ref ESFGAO rgfInOut);

        /// <summary>
        /// Retrieves an OLE interface that can be used to carry out actions on the specified file objects or folders.
        /// </summary>
        /// <param name="hwndOwner">Handle to the owner window that the client should specify if it displays a dialog box or message box</param>
        /// <param name="cidl">Number of file objects or subfolders specified in the apidl parameter</param>
        /// <param name="apidl">Address of an array of pointers to <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structures, each of which  uniquely identifies a file object or subfolder relative to the parent folder</param>
        /// <param name="riid">Identifier of the COM interface object to return</param>
        /// <param name="rgfReserved"> Reserved</param>
        /// <param name="ppv">Pointer to the requested interface</param>
        int GetUIObjectOf(
          IntPtr hwndOwner,
          [MarshalAs(UnmanagedType.U4)] int cidl,
          [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] IntPtr[] apidl,
          [In] ref Guid riid,
          [MarshalAs(UnmanagedType.U4)] int rgfReserved,
          out IntPtr ppv);

        /// <summary>
        /// Retrieves the display name for the specified file object or subfolder.
        /// </summary>
        /// <param name="pidl">Address of an <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure (PIDL)  that uniquely identifies the file  object or subfolder relative to the parent  folder</param>
        /// <param name="uFlags">Flags used to request the type of display name to return. For a list of possible values</param>
        /// <param name="pName"> Address of a STRRET structure in which to return the display name</param>
        int GetDisplayNameOf(IntPtr pidl, ESHGDN uFlags, out STRRET pName);

        /// <summary>
        /// Sets the display name of a file object or subfolder, changing the item identifier in the process.
        /// </summary>
        /// <param name="hwnd"> Handle to the owner window of any dialog or message boxes that the client displays</param>
        /// <param name="pidl"> Pointer to an <see cref="T:Intermech.WindowsDll.Shell32.ITEMIDLIST" /> structure that uniquely identifies the file object or subfolder relative to the parent folder</param>
        /// <param name="pszName"> Pointer to a null-terminated string that specifies the new display name</param>
        /// <param name="uFlags">Flags indicating the type of name specified by  the lpszName parameter. For a list of possible values, see the description of the SHGNO enum</param>
        /// <param name="ppidlOut"></param>
        int SetNameOf(
          IntPtr hwnd,
          IntPtr pidl,
          string pszName,
          ESHCONTF uFlags,
          out IntPtr ppidlOut);
      }

      [Flags]
      public enum ESFGAO
      {
        SFGAO_CANCOPY = 1,
        SFGAO_CANMOVE = 2,
        SFGAO_CANLINK = 4,
        SFGAO_LINK = 65536, // 0x00010000
        SFGAO_SHARE = 131072, // 0x00020000
        SFGAO_READONLY = 262144, // 0x00040000
        SFGAO_HIDDEN = 524288, // 0x00080000
        SFGAO_FOLDER = 536870912, // 0x20000000
        SFGAO_FILESYSTEM = 1073741824, // 0x40000000
        SFGAO_HASSUBFOLDER = -2147483648, // 0x80000000
      }

      [Flags]
      public enum ESHCONTF
      {
        SHCONTF_FOLDERS = 32, // 0x00000020
        SHCONTF_NONFOLDERS = 64, // 0x00000040
        SHCONTF_INCLUDEHIDDEN = 128, // 0x00000080
        SHCONTF_INIT_ON_FIRST_NEXT = 256, // 0x00000100
        SHCONTF_NETPRINTERSRCH = 512, // 0x00000200
        SHCONTF_SHAREABLE = 1024, // 0x00000400
        SHCONTF_STORAGE = 2048, // 0x00000800
      }

      [Flags]
      public enum ESHGDN
      {
        SHGDN_NORMAL = 0,
        SHGDN_INFOLDER = 1,
        SHGDN_FOREDITING = 4096, // 0x00001000
        SHGDN_FORADDRESSBAR = 16384, // 0x00004000
        SHGDN_FORPARSING = 32768, // 0x00008000
      }

      [StructLayout(LayoutKind.Explicit, Size = 520)]
      public struct STRRETinternal
      {
        [FieldOffset(0)]
        public IntPtr pOleStr;
        [FieldOffset(0)]
        public IntPtr pStr;
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        public int uOffset;
      }

      public struct STRRET
      {
        [MarshalAs(UnmanagedType.U4)]
        public int uType;
        public STRRETinternal data;
      }

      public enum CSIDL
      {
        CSIDL_DESKTOP = 0,
        CSIDL_INTERNET = 1,
        CSIDL_PROGRAMS = 2,
        CSIDL_CONTROLS = 3,
        CSIDL_PRINTERS = 4,
        CSIDL_PERSONAL = 5,
        CSIDL_FAVORITES = 6,
        CSIDL_STARTUP = 7,
        CSIDL_RECENT = 8,
        CSIDL_SENDTO = 9,
        CSIDL_BITBUCKET = 10, // 0x0000000A
        CSIDL_STARTMENU = 11, // 0x0000000B
        CSIDL_MYDOCUMENTS = 12, // 0x0000000C
        CSIDL_MYMUSIC = 13, // 0x0000000D
        CSIDL_MYVIDEO = 14, // 0x0000000E
        CSIDL_DESKTOPDIRECTORY = 16, // 0x00000010
        CSIDL_DRIVES = 17, // 0x00000011
        CSIDL_NETWORK = 18, // 0x00000012
        CSIDL_NETHOOD = 19, // 0x00000013
        CSIDL_FONTS = 20, // 0x00000014
        CSIDL_TEMPLATES = 21, // 0x00000015
        CSIDL_COMMON_STARTMENU = 22, // 0x00000016
        CSIDL_COMMON_PROGRAMS = 23, // 0x00000017
        CSIDL_COMMON_STARTUP = 24, // 0x00000018
        CSIDL_COMMON_DESKTOPDIRECTORY = 25, // 0x00000019
        CSIDL_APPDATA = 26, // 0x0000001A
        CSIDL_PRINTHOOD = 27, // 0x0000001B
        CSIDL_LOCAL_APPDATA = 28, // 0x0000001C
        CSIDL_ALTSTARTUP = 29, // 0x0000001D
        CSIDL_COMMON_ALTSTARTUP = 30, // 0x0000001E
        CSIDL_COMMON_FAVORITES = 31, // 0x0000001F
        CSIDL_INTERNET_CACHE = 32, // 0x00000020
        CSIDL_COOKIES = 33, // 0x00000021
        CSIDL_HISTORY = 34, // 0x00000022
        CSIDL_COMMON_APPDATA = 35, // 0x00000023
        CSIDL_WINDOWS = 36, // 0x00000024
        CSIDL_SYSTEM = 37, // 0x00000025
        CSIDL_PROGRAM_FILES = 38, // 0x00000026
        CSIDL_MYPICTURES = 39, // 0x00000027
        CSIDL_PROFILE = 40, // 0x00000028
        CSIDL_PROGRAM_FILES_COMMON = 43, // 0x0000002B
        CSIDL_COMMON_TEMPLATES = 45, // 0x0000002D
        CSIDL_COMMON_DOCUMENTS = 46, // 0x0000002E
        CSIDL_COMMON_ADMINTOOLS = 47, // 0x0000002F
        CSIDL_ADMINTOOLS = 48, // 0x00000030
        CSIDL_COMMON_MUSIC = 53, // 0x00000035
        CSIDL_COMMON_PICTURES = 54, // 0x00000036
        CSIDL_COMMON_VIDEO = 55, // 0x00000037
        CSIDL_CDBURN_AREA = 59, // 0x0000003B
        CSIDL_PROFILES = 62, // 0x0000003E
        CSIDL_FLAG_CREATE = 32768, // 0x00008000
      }

      /// <summary>
      /// Used by SHGetStockIconInfo to identify which stock system icon to retrieve.
      /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ne-shellapi-shstockiconid" />
      /// </summary>
      public enum SHSTOCKICONID
      {
        DOCNOASSOC = 0,
        DOCASSOC = 1,
        APPLICATION = 2,
        FOLDER = 3,
        FOLDEROPEN = 4,
        DRIVE525 = 5,
        DRIVE35 = 6,
        DRIVEREMOVE = 7,
        DRIVEFIXED = 8,
        DRIVENET = 9,
        DRIVENETDISABLED = 10, // 0x0000000A
        DRIVECD = 11, // 0x0000000B
        DRIVERAM = 12, // 0x0000000C
        WORLD = 13, // 0x0000000D
        SERVER = 15, // 0x0000000F
        PRINTER = 16, // 0x00000010
        MYNETWORK = 17, // 0x00000011
        FIND = 22, // 0x00000016
        HELP = 23, // 0x00000017
        SHARE = 28, // 0x0000001C
        LINK = 29, // 0x0000001D
        SLOWFILE = 30, // 0x0000001E
        RECYCLER = 31, // 0x0000001F
        RECYCLERFULL = 32, // 0x00000020
        MEDIACDAUDIO = 40, // 0x00000028
        LOCK = 47, // 0x0000002F
        AUTOLIST = 49, // 0x00000031
        PRINTERNET = 50, // 0x00000032
        SERVERSHARE = 51, // 0x00000033
        PRINTERFAX = 52, // 0x00000034
        PRINTERFAXNET = 53, // 0x00000035
        PRINTERFILE = 54, // 0x00000036
        STACK = 55, // 0x00000037
        MEDIASVCD = 56, // 0x00000038
        STUFFEDFOLDER = 57, // 0x00000039
        DRIVEUNKNOWN = 58, // 0x0000003A
        DRIVEDVD = 59, // 0x0000003B
        MEDIADVD = 60, // 0x0000003C
        MEDIADVDRAM = 61, // 0x0000003D
        MEDIADVDRW = 62, // 0x0000003E
        MEDIADVDR = 63, // 0x0000003F
        MEDIADVDROM = 64, // 0x00000040
        MEDIACDAUDIOPLUS = 65, // 0x00000041
        MEDIACDRW = 66, // 0x00000042
        MEDIACDR = 67, // 0x00000043
        MEDIACDBURN = 68, // 0x00000044
        MEDIABLANKCD = 69, // 0x00000045
        MEDIACDROM = 70, // 0x00000046
        AUDIOFILES = 71, // 0x00000047
        IMAGEFILES = 72, // 0x00000048
        VIDEOFILES = 73, // 0x00000049
        MIXEDFILES = 74, // 0x0000004A
        FOLDERBACK = 75, // 0x0000004B
        FOLDERFRONT = 76, // 0x0000004C
        SHIELD = 77, // 0x0000004D
        WARNING = 78, // 0x0000004E
        INFO = 79, // 0x0000004F
        ERROR = 80, // 0x00000050
        KEY = 81, // 0x00000051
        SOFTWARE = 82, // 0x00000052
        RENAME = 83, // 0x00000053
        DELETE = 84, // 0x00000054
        MEDIAAUDIODVD = 85, // 0x00000055
        MEDIAMOVIEDVD = 86, // 0x00000056
        MEDIAENHANCEDCD = 87, // 0x00000057
        MEDIAENHANCEDDVD = 88, // 0x00000058
        MEDIAHDDVD = 89, // 0x00000059
        MEDIABLURAY = 90, // 0x0000005A
        MEDIAVCD = 91, // 0x0000005B
        MEDIADVDPLUSR = 92, // 0x0000005C
        MEDIADVDPLUSRW = 93, // 0x0000005D
        DESKTOPPC = 94, // 0x0000005E
        MOBILEPC = 95, // 0x0000005F
        USERS = 96, // 0x00000060
        MEDIASMARTMEDIA = 97, // 0x00000061
        MEDIACOMPACTFLASH = 98, // 0x00000062
        DEVICECELLPHONE = 99, // 0x00000063
        DEVICECAMERA = 100, // 0x00000064
        DEVICEVIDEOCAMERA = 101, // 0x00000065
        DEVICEAUDIOPLAYER = 102, // 0x00000066
        NETWORKCONNECT = 103, // 0x00000067
        INTERNET = 104, // 0x00000068
        ZIPFILE = 105, // 0x00000069
        SETTINGS = 106, // 0x0000006A
        DRIVEHDDVD = 132, // 0x00000084
        DRIVEBD = 133, // 0x00000085
        MEDIAHDDVDROM = 134, // 0x00000086
        MEDIAHDDVDR = 135, // 0x00000087
        MEDIAHDDVDRAM = 136, // 0x00000088
        MEDIABDROM = 137, // 0x00000089
        MEDIABDR = 138, // 0x0000008A
        MEDIABDRE = 139, // 0x0000008B
        CLUSTEREDDRIVE = 140, // 0x0000008C
        MAX_ICONS = 175, // 0x000000AF
      }

      [Flags]
      public enum SHGFI
      {
        Empty = 0,
        /// <summary>get icon</summary>
        Icon = 256, // 0x00000100
        /// <summary>get display name</summary>
        DisplayName = 512, // 0x00000200
        /// <summary>get type name</summary>
        TypeName = 1024, // 0x00000400
        /// <summary>get attributes</summary>
        Attributes = 2048, // 0x00000800
        /// <summary>get icon location</summary>
        IconLocation = 4096, // 0x00001000
        /// <summary>return exe type</summary>
        ExeType = 8192, // 0x00002000
        /// <summary>get system icon index</summary>
        SysIconIndex = 16384, // 0x00004000
        /// <summary>put a link overlay on icon</summary>
        LinkOverlay = 32768, // 0x00008000
        /// <summary>show icon in selected state</summary>
        Selected = 65536, // 0x00010000
        /// <summary>get only specified attributes</summary>
        Attr_Specified = 131072, // 0x00020000
        /// <summary>get large icon</summary>
        LargeIcon = 0,
        /// <summary>get small icon</summary>
        SmallIcon = 1,
        /// <summary>get open icon</summary>
        OpenIcon = 2,
        /// <summary>get shell size icon</summary>
        ShellIconSize = 4,
        /// <summary>pszPath is a pidl</summary>
        PIDL = 8,
        /// <summary>use passed dwFileAttribute</summary>
        UseFileAttributes = 16, // 0x00000010
        /// <summary>apply the appropriate overlays</summary>
        AddOverlays = 32, // 0x00000020
        /// <summary>Get the index of the overlay in the upper 8 bits of the iIcon</summary>
        OverlayIndex = 64, // 0x00000040
      }

      /// <summary>
      /// Flags, that specify which information is requested by <see cref="M:Intermech.WindowsDll.Shell32.SHGetStockIconInfo(Intermech.WindowsDll.Shell32.SHSTOCKICONID,Intermech.WindowsDll.Shell32.SHGSI,Intermech.WindowsDll.Shell32.SHSTOCKICONINFO)" /> method.
      /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetstockiconinfo" />
      /// </summary>
      [Flags]
      public enum SHGSI
      {
        /// <summary>
        /// The szPath and iIcon members of the <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO" /> structure receive the path and icon index of the requested icon,
        ///     in a format suitable for passing to the <see cref="!:ExtractIcon" /> function.
        /// The numerical value of this flag is zero, so you always get the icon location regardless of other flags.
        /// </summary>
        ICONLOCATION = 0,
        /// <summary>The hIcon member of the <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO" /> structure receives a handle to the specified icon</summary>
        ICON = 256, // 0x00000100
        /// <summary>The iSysImageImage member of the <see cref="T:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO" /> structure receives the index of the specified icon in the system imagelist</summary>
        SYSICONINDEX = 16384, // 0x00004000
        /// <summary>Modifies the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.ICON" /> value by causing the function to add the link overlay to the file's icon.</summary>
        LINKOVERLAY = 32768, // 0x00008000
        /// <summary>Modifies the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.ICON" /> value by causing the function to blend the icon with the system highlight color.</summary>
        SELECTED = 65536, // 0x00010000
        /// <summary>
        /// Modifies the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.ICON" /> value by causing the function to retrieve the large version of the icon,
        /// as specified by the <see cref="!:User32.SystemMetric.CXSICON" /> and <see cref="!:User32.SystemMetric.CYSICON" /> system metrics.
        /// </summary>
        LARGEICON = 0,
        /// <summary>
        /// Modifies the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.ICON" /> value by causing the function to retrieve the small version of the icon,
        ///     as specified by the <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CXSMICON" /> and <see cref="F:Intermech.WindowsDll.User32.SystemMetric.CYSMICON" /> system metrics.
        /// </summary>
        SMALLICON = 1,
        /// <summary>
        /// Modifies the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.LARGEICON" /> or <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.SMALLICON" /> values by causing the function
        ///    to retrieve the Shell-sized icons rather than the sizes specified by the system metrics.
        /// </summary>
        SHELLICONSIZE = 4,
      }

      /// <summary>
      /// Receives information used to retrieve a stock Shell icon. This structure is used in a call <see cref="M:Intermech.WindowsDll.Shell32.SHGetStockIconInfo(Intermech.WindowsDll.Shell32.SHSTOCKICONID,Intermech.WindowsDll.Shell32.SHGSI,Intermech.WindowsDll.Shell32.SHSTOCKICONINFO)" />.
      /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shstockiconinfo" />
      /// </summary>
      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
      public class SHSTOCKICONINFO
      {
        /// <summary>The size of this structure, in bytes</summary>
        [MarshalAs(UnmanagedType.U4)]
        private readonly int Size = Marshal.SizeOf(typeof (SHSTOCKICONINFO));
        /// <summary>When <see cref="M:Intermech.WindowsDll.Shell32.SHGetStockIconInfo(Intermech.WindowsDll.Shell32.SHSTOCKICONID,Intermech.WindowsDll.Shell32.SHGSI,Intermech.WindowsDll.Shell32.SHSTOCKICONINFO)" /> is called with the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.ICON" /> flag, this member receives a handle to the icon.</summary>
        public IntPtr hIcon;
        /// <summary>
        /// When <see cref="M:Intermech.WindowsDll.Shell32.SHGetStockIconInfo(Intermech.WindowsDll.Shell32.SHSTOCKICONID,Intermech.WindowsDll.Shell32.SHGSI,Intermech.WindowsDll.Shell32.SHSTOCKICONINFO)" /> is called with the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.SYSICONINDEX" /> flag, this member receives the index of the image in the system icon cache.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int iSysIconIndex;
        /// <summary>
        /// When <see cref="M:Intermech.WindowsDll.Shell32.SHGetStockIconInfo(Intermech.WindowsDll.Shell32.SHSTOCKICONID,Intermech.WindowsDll.Shell32.SHGSI,Intermech.WindowsDll.Shell32.SHSTOCKICONINFO)" /> is called with the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.ICONLOCATION" /> flag,
        ///     this member receives the index of the icon in the resource whose path is received in <see cref="F:Intermech.WindowsDll.Shell32.SHSTOCKICONINFO.szPath" />.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int iIcon;
        /// <summary>
        /// When <see cref="M:Intermech.WindowsDll.Shell32.SHGetStockIconInfo(Intermech.WindowsDll.Shell32.SHSTOCKICONID,Intermech.WindowsDll.Shell32.SHGSI,Intermech.WindowsDll.Shell32.SHSTOCKICONINFO)" /> is called with the <see cref="F:Intermech.WindowsDll.Shell32.SHGSI.ICONLOCATION" /> flag,
        ///     this member receives the path of the resource that contains the icon.
        ///     The index of the icon within the resource is received in iIcon.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
      }
    }
}
