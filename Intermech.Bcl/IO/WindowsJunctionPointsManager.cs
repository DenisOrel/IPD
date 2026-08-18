
// Type: Intermech.IO.WindowsJunctionPointsManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace Intermech.IO
{
    /// <summary>
    /// Реализация менеджера операций с точками соединения каталогов для Microsoft Windows. Реализация является thread safe.
    /// </summary>
    public class WindowsJunctionPointsManager
    {
      private Lazy<bool> isSupported;

      /// <summary>Создает объект.</summary>
      public WindowsJunctionPointsManager()
      {
        this.isSupported = new Lazy<bool>(new Func<bool>(this.TestSupported), LazyThreadSafetyMode.PublicationOnly);
      }

      /// <summary>
      /// Возвращает признак, что операции с точками соединения поддерживаются операционной системой.
      /// </summary>
      public bool IsSupported
      {
        [DebuggerStepThrough] get => this.isSupported.Value;
      }

      private bool TestSupported()
      {
        OperatingSystem osVersion = Environment.OSVersion;
        return osVersion.Platform == PlatformID.Win32NT && osVersion.Version.Major >= 5;
      }

      /// <summary>Создает точку соединения.</summary>
      /// <param name="linkPath">Абсолютный путь к каталогу, который будет точкой соединения</param>
      /// <param name="targetPath">Путь к целевому каталогу. Может быть в абсолютной или относительной форме</param>
      /// <exception cref="T:ArgumentNullException">linkPath || targetPath</exception>
      /// <exception cref="T:ArgumentException">Путь к каталогу, который будет точкой соеднения, задан не в абсолютной форме</exception>
      /// <exception cref="T:IOException">Ошибка при создании точки соединения</exception>
      public void CreateLink(string linkPath, string targetPath)
      {
        if (linkPath == null)
          throw new ArgumentNullException(nameof (linkPath));
        if (!Path.IsPathRooted(linkPath))
          throw new ArgumentException("Требуется путь в абсолютной форме.", nameof (linkPath));
        if (targetPath == null)
          throw new ArgumentNullException(nameof (targetPath));
        this.DoCreateLink(linkPath, targetPath);
      }

      /// <summary>Создает точку соединения.</summary>
      /// <param name="linkPath">Абсолютный путь к каталогу, который будет точкой соединения</param>
      /// <param name="targetPath">Путь к целевому каталогу. Может быть в абсолютной или относительной форме</param>
      /// <exception cref="T:IOException">Ошибка при создании точки соединения</exception>
      private void DoCreateLink(string linkPath, string targetPath)
      {
        if (!Path.IsPathRooted(targetPath))
          targetPath = Path.Combine(Path.GetDirectoryName(linkPath), targetPath);
        targetPath = Path.GetFullPath(targetPath);
        if (!Directory.Exists(targetPath))
          throw new DirectoryNotFoundException($"Невозможно создать точку соединения '{linkPath}', так как не удалось найти целевой каталог '{targetPath}'.");
        if (!Directory.Exists(linkPath))
          Directory.CreateDirectory(linkPath);
        using (SafeFileHandle hDevice = this.OpenReparsePoint(linkPath, WindowsJunctionPointsManager.NativeMethods.EFileAccess.GenericWrite))
        {
          byte[] bytes = Encoding.Unicode.GetBytes("\\??\\" + Path.GetFullPath(targetPath));
                NativeMethods.REPARSE_DATA_BUFFER junctionPointBuffer = WindowsJunctionPointsManager.NativeMethods.CreateJunctionPointBuffer() with
          {
            ReparseDataLength = (ushort) (bytes.Length + 12),
            SubstituteNameOffset = 0,
            SubstituteNameLength = (ushort) bytes.Length,
            PrintNameOffset = (ushort) (bytes.Length + 2),
            PrintNameLength = 0
          };
          Array.Copy((Array) bytes, (Array) junctionPointBuffer.PathBuffer, bytes.Length);
          IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(junctionPointBuffer));
          try
          {
            Marshal.StructureToPtr(junctionPointBuffer, num, false);
            if (!WindowsJunctionPointsManager.NativeMethods.DeviceIoControl((SafeHandle) hDevice, 589988U, num, bytes.Length + 20, IntPtr.Zero, 0, out int _, IntPtr.Zero))
            {
              int lastWin32Error = Marshal.GetLastWin32Error();
              throw this.WrapWin32Error($"При создании точки соединения '{linkPath}' произошла ошибка операционной системы.", lastWin32Error);
            }
          }
          finally
          {
            Marshal.FreeHGlobal(num);
          }
        }
      }

      /// <summary>
      /// Возвращает путь к целевому каталогу для указанной точки соединения. Метод может вернуть null, если указанный путь не является точкой соединения.
      /// </summary>
      /// <param name="linkPath">Абсолютный путь к каталогу, являющемуся точкой соединения</param>
      /// <returns>Путь к целевому каталогу в абсолютной форме или null, если указанный путь не является точкой соединения</returns>
      /// <exception cref="T:ArgumentNullException">linkPath</exception>
      /// <exception cref="T:ArgumentException">Путь к каталогу, который будет точкой соеднения, задан не в абсолютной форме</exception>
      /// <exception cref="T:IOException">Ошибка при операции с точкой соединения</exception>
      public string GetLinkTarget(string linkPath)
      {
        if (linkPath == null)
          throw new ArgumentNullException(nameof (linkPath));
        return Path.IsPathRooted(linkPath) ? this.DoGetLinkTarget(linkPath) : throw new ArgumentException("Требуется путь в абсолютной форме.", nameof (linkPath));
      }

      /// <summary>
      /// Возвращает путь к целевому каталогу для указанной точки соединения. Метод может вернуть null, если указанный путь не является точкой соединения.
      /// </summary>
      /// <param name="linkPath">Абсолютный путь к каталогу, являющемуся точкой соединения</param>
      /// <returns>Путь к целевому каталогу в абсолютной форме или null, если указанный путь не является точкой соединения</returns>
      /// <exception cref="T:IOException">Ошибка при операции с точкой соединения</exception>
      private string DoGetLinkTarget(string linkPath)
      {
        using (SafeFileHandle handle = this.OpenReparsePoint(linkPath, WindowsJunctionPointsManager.NativeMethods.EFileAccess.GenericRead))
          return this.InternalGetLinkTarget(linkPath, handle);
      }

      private string InternalGetLinkTarget(string linkPath, SafeFileHandle handle)
      {
        int num1 = Marshal.SizeOf(typeof (NativeMethods.REPARSE_DATA_BUFFER));
        IntPtr num2 = Marshal.AllocHGlobal(num1);
        try
        {
          if (!WindowsJunctionPointsManager.NativeMethods.DeviceIoControl((SafeHandle) handle, 589992U, IntPtr.Zero, 0, num2, num1, out int _, IntPtr.Zero))
          {
            int lastWin32Error = Marshal.GetLastWin32Error();
            if (lastWin32Error == 4390)
              return (string) null;
            throw this.WrapWin32Error($"При получении информации о точке соединения '{linkPath}' произошла ошибка операционной системы.", lastWin32Error);
          }
                NativeMethods.REPARSE_DATA_BUFFER structure = (NativeMethods.REPARSE_DATA_BUFFER) Marshal.PtrToStructure(num2, typeof (NativeMethods.REPARSE_DATA_BUFFER));
          if (structure.ReparseTag != 2684354563U /*0xA0000003*/)
            return (string) null;
          string linkTarget = Encoding.Unicode.GetString(structure.PathBuffer, (int) structure.SubstituteNameOffset, (int) structure.SubstituteNameLength);
          if (linkTarget.StartsWith("\\??\\"))
            linkTarget = linkTarget.Substring("\\??\\".Length);
          return linkTarget;
        }
        finally
        {
          Marshal.FreeHGlobal(num2);
        }
      }

      /// <summary>
      /// Разрывает связь между точкой соединения и целевым каталогом.
      /// </summary>
      /// <param name="linkPath">Абсолютный путь к каталогу, являющемуся точкой соединения</param>
      /// <exception cref="T:ArgumentNullException">linkPath</exception>
      /// <exception cref="T:ArgumentException">Путь к каталогу, который будет точкой соеднения, задан не в абсолютной форме</exception>
      /// <exception cref="T:IOException">Ошибка при операции с точкой соединения</exception>
      public void BreakLink(string linkPath)
      {
        if (linkPath == null)
          throw new ArgumentNullException(nameof (linkPath));
        if (!Path.IsPathRooted(linkPath))
          throw new ArgumentException("Требуется путь в абсолютной форме.", nameof (linkPath));
        this.DoBreakLink(linkPath);
      }

      /// <summary>
      /// Разрывает связь между точкой соединения и целевым каталогом.
      /// </summary>
      /// <param name="linkPath">Абсолютный путь к каталогу, являющемуся точкой соединения</param>
      /// <exception cref="T:IOException">Ошибка при операции с точкой соединения</exception>
      private void DoBreakLink(string linkPath)
      {
        if (!Directory.Exists(linkPath))
          throw new IOException($"Невозможно разорвать связь точки соединения '{linkPath}' с целевым каталогом, так как указанный каталог не является точкой соединения.");
        using (SafeFileHandle hDevice = this.OpenReparsePoint(linkPath, WindowsJunctionPointsManager.NativeMethods.EFileAccess.GenericWrite))
        {
                NativeMethods.REPARSE_DATA_BUFFER junctionPointBuffer = WindowsJunctionPointsManager.NativeMethods.CreateJunctionPointBuffer() with
          {
            ReparseDataLength = 0
          };
          IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(junctionPointBuffer));
          try
          {
            Marshal.StructureToPtr(junctionPointBuffer, num, false);
            if (!WindowsJunctionPointsManager.NativeMethods.DeviceIoControl((SafeHandle) hDevice, 589996U, num, 8, IntPtr.Zero, 0, out int _, IntPtr.Zero))
            {
              int lastWin32Error = Marshal.GetLastWin32Error();
              throw this.WrapWin32Error($"При разрыве связи между точкой соединения '{linkPath}' и целевым каталогом произошла ошибка операционной системы.", lastWin32Error);
            }
          }
          finally
          {
            Marshal.FreeHGlobal(num);
          }
        }
      }

      private SafeFileHandle OpenReparsePoint(
        string junctionPoint,
        NativeMethods.EFileAccess accessMode)
      {
        SafeFileHandle safeFileHandle = new SafeFileHandle(WindowsJunctionPointsManager.NativeMethods.CreateFile(junctionPoint, accessMode, WindowsJunctionPointsManager.NativeMethods.EFileShare.Read | WindowsJunctionPointsManager.NativeMethods.EFileShare.Write | WindowsJunctionPointsManager.NativeMethods.EFileShare.Delete, IntPtr.Zero, WindowsJunctionPointsManager.NativeMethods.ECreationDisposition.OpenExisting, WindowsJunctionPointsManager.NativeMethods.EFileAttributes.BackupSemantics | WindowsJunctionPointsManager.NativeMethods.EFileAttributes.OpenReparsePoint, IntPtr.Zero), true);
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error == 0)
          return safeFileHandle;
        throw this.WrapWin32Error($"Не удалось открыть точку соединения '{junctionPoint}'.", lastWin32Error);
      }

      private IOException WrapWin32Error(string errorMessage, int errorCode)
      {
        return this.WrapWin32Exception(errorMessage, new Win32Exception(errorCode));
      }

      private IOException WrapWin32Exception(string errorMessage, Win32Exception win32Exception)
      {
        return new IOException(errorMessage, (Exception) win32Exception);
      }

      private static class NativeMethods
      {
        /// <summary>The file or directory is not a reparse point.</summary>
        public const int ERROR_NOT_A_REPARSE_POINT = 4390;
        /// <summary>
        /// The reparse point attribute cannot be set because it conflicts with an existing attribute.
        /// </summary>
        public const int ERROR_REPARSE_ATTRIBUTE_CONFLICT = 4391;
        /// <summary>
        /// The data present in the reparse point buffer is invalid.
        /// </summary>
        public const int ERROR_INVALID_REPARSE_DATA = 4392;
        /// <summary>
        /// The tag present in the reparse point buffer is invalid.
        /// </summary>
        public const int ERROR_REPARSE_TAG_INVALID = 4393;
        /// <summary>
        /// There is a mismatch between the tag specified in the request and the tag present in the reparse point.
        /// </summary>
        public const int ERROR_REPARSE_TAG_MISMATCH = 4394;
        /// <summary>Command to set the reparse point data block.</summary>
        public const int FSCTL_SET_REPARSE_POINT = 589988;
        /// <summary>Command to get the reparse point data block.</summary>
        public const int FSCTL_GET_REPARSE_POINT = 589992;
        /// <summary>Command to delete the reparse point data base.</summary>
        public const int FSCTL_DELETE_REPARSE_POINT = 589996;
        /// <summary>
        /// Reparse point tag used to identify mount points and junction points.
        /// </summary>
        public const uint IO_REPARSE_TAG_MOUNT_POINT = 2684354563 /*0xA0000003*/;
        /// <summary>
        /// This prefix indicates to NTFS that the path is to be treated as a non-interpreted
        /// path in the virtual file system.
        /// </summary>
        public const string NonInterpretedPathPrefix = "\\??\\";

        public static REPARSE_DATA_BUFFER CreateJunctionPointBuffer()
        {
          return new REPARSE_DATA_BUFFER()
          {
            ReparseTag = 2684354563 /*0xA0000003*/,
            PathBuffer = new byte[16368]
          };
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DeviceIoControl(
          SafeHandle hDevice,
          uint dwIoControlCode,
          IntPtr InBuffer,
          int nInBufferSize,
          IntPtr OutBuffer,
          int nOutBufferSize,
          out int pBytesReturned,
          IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateFile(
          string lpFileName,
          EFileAccess dwDesiredAccess,
          EFileShare dwShareMode,
          IntPtr lpSecurityAttributes,
          ECreationDisposition dwCreationDisposition,
          EFileAttributes dwFlagsAndAttributes,
          IntPtr hTemplateFile);

        [Flags]
        public enum EFileAccess : uint
        {
          GenericRead = 2147483648, // 0x80000000
          GenericWrite = 1073741824, // 0x40000000
          GenericExecute = 536870912, // 0x20000000
          GenericAll = 268435456, // 0x10000000
        }

        [Flags]
        public enum EFileShare : uint
        {
          None = 0,
          Read = 1,
          Write = 2,
          Delete = 4,
        }

        public enum ECreationDisposition : uint
        {
          New = 1,
          CreateAlways = 2,
          OpenExisting = 3,
          OpenAlways = 4,
          TruncateExisting = 5,
        }

        [Flags]
        public enum EFileAttributes : uint
        {
          Readonly = 1,
          Hidden = 2,
          System = 4,
          Directory = 16, // 0x00000010
          Archive = 32, // 0x00000020
          Device = 64, // 0x00000040
          Normal = 128, // 0x00000080
          Temporary = 256, // 0x00000100
          SparseFile = 512, // 0x00000200
          ReparsePoint = 1024, // 0x00000400
          Compressed = 2048, // 0x00000800
          Offline = 4096, // 0x00001000
          NotContentIndexed = 8192, // 0x00002000
          Encrypted = 16384, // 0x00004000
          Write_Through = 2147483648, // 0x80000000
          Overlapped = 1073741824, // 0x40000000
          NoBuffering = 536870912, // 0x20000000
          RandomAccess = 268435456, // 0x10000000
          SequentialScan = 134217728, // 0x08000000
          DeleteOnClose = 67108864, // 0x04000000
          BackupSemantics = 33554432, // 0x02000000
          PosixSemantics = 16777216, // 0x01000000
          OpenReparsePoint = 2097152, // 0x00200000
          OpenNoRecall = 1048576, // 0x00100000
          FirstPipeInstance = 524288, // 0x00080000
        }

        public struct REPARSE_DATA_BUFFER
        {
          /// <summary>
          /// Reparse point tag. Must be a Microsoft reparse point tag.
          /// </summary>
          public uint ReparseTag;
          /// <summary>
          /// Size, in bytes, of the data after the Reserved member. This can be calculated by:
          /// (4 * sizeof(ushort)) + SubstituteNameLength + PrintNameLength +
          /// (namesAreNullTerminated ? 2 * sizeof(char) : 0);
          /// </summary>
          public ushort ReparseDataLength;
          /// <summary>Reserved; do not use.</summary>
          public ushort Reserved;
          /// <summary>
          /// Offset, in bytes, of the substitute name string in the PathBuffer array.
          /// </summary>
          public ushort SubstituteNameOffset;
          /// <summary>
          /// Length, in bytes, of the substitute name string. If this string is null-terminated,
          /// SubstituteNameLength does not include space for the null character.
          /// </summary>
          public ushort SubstituteNameLength;
          /// <summary>
          /// Offset, in bytes, of the print name string in the PathBuffer array.
          /// </summary>
          public ushort PrintNameOffset;
          /// <summary>
          /// Length, in bytes, of the print name string. If this string is null-terminated,
          /// PrintNameLength does not include space for the null character.
          /// </summary>
          public ushort PrintNameLength;
          /// <summary>
          /// A buffer containing the unicode-encoded path string. The path string contains
          /// the substitute name string and print name string.
          /// </summary>
          [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16368)]
          public byte[] PathBuffer;
        }
      }
    }
}
