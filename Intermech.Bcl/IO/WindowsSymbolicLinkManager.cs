
// Type: Intermech.IO.WindowsSymbolicLinkManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
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
    /// Реализация менеджера операций с символическими ссылками для Microsoft Windows. Реализация является thread safe.
    /// </summary>
    /// <remarks>
    /// Для корректной работы менеджеру требуется системная привилегия SeCreateSymbolicLinkPrivilege, которую можно
    /// раздать через редактор локальной политики безопасности. Кроме того, если включен UAC и пользователь входит
    /// в группу 'Администраторы' то требуется повышение привилегий до административных. Для обычных пользователей
    /// достаточно наличия привилегии.
    /// </remarks>
    public sealed class WindowsSymbolicLinkManager : SymbolicLinkManager
    {
      private Lazy<bool> isSupported;

      /// <summary>Создает объект.</summary>
      public WindowsSymbolicLinkManager()
      {
        this.isSupported = new Lazy<bool>(new Func<bool>(this.TestSupported), LazyThreadSafetyMode.PublicationOnly);
      }

      /// <summary>
      /// Возвращает признак, что операции с символическими ссылками поддерживаются операционной системой.
      /// </summary>
      public override bool IsSupported
      {
        [DebuggerStepThrough] get => this.isSupported.Value;
      }

      private bool TestSupported()
      {
        OperatingSystem osVersion = Environment.OSVersion;
        return osVersion.Platform == PlatformID.Win32NT && osVersion.Version.Major >= 6;
      }

      /// <summary>Создает символическую ссылку.</summary>
      /// <param name="symlinkPath">Абсолютный путь символической ссылки</param>
      /// <param name="targetPath">Путь к цели символической ссылки - файлу или каталогу. Может быть в абсолютной или относительной форме</param>
      /// <exception cref="T:IOException">Ошибка при создании символической ссылки</exception>
      protected override void DoCreateLink(string symlinkPath, string targetPath)
      {
        string path = targetPath;
        if (!Path.IsPathRooted(path))
          path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(symlinkPath), targetPath));
            NativeMethods.SYMBOLIC_LINK_FLAGS flags;
        if (Directory.Exists(path))
        {
          flags = WindowsSymbolicLinkManager.NativeMethods.SYMBOLIC_LINK_FLAGS.SYMBOLIC_LINK_FLAG_DIRECTORY;
        }
        else
        {
          if (!File.Exists(path))
            throw new IOException($"Невозможно создать символическую ссылку '{symlinkPath}', так как не удалось найти целевой файл или каталог '{targetPath}'.");
          flags = WindowsSymbolicLinkManager.NativeMethods.SYMBOLIC_LINK_FLAGS.SYMBOLIC_LINK_FLAG_FILE;
        }
        string directoryName = Path.GetDirectoryName(symlinkPath);
        if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
        if (!WindowsSymbolicLinkManager.NativeMethods.CreateSymbolicLink(symlinkPath, targetPath, flags))
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          throw this.WrapWin32Error($"При создании символической ссылки '{symlinkPath}' произошла ошибка операционной системы.", lastWin32Error);
        }
      }

      /// <summary>
      /// Возвращает путь к цели для указанной символической ссылки. Метод должен вернуть null, если указанный путь не является символической ссылкой
      /// </summary>
      /// <param name="symlinkPath">Абсолютный путь символической ссылки</param>
      /// <returns>Путь к цели символической ссылки в абсолютной форме или null, если указанный путь не является символической ссылкой</returns>
      /// <exception cref="T:IOException">Ошибка при операции с символической ссылкой</exception>
      protected override string DoGetLinkTarget(string symlinkPath)
      {
        uint dwFlagsAndAttributes = 33554432 /*0x02000000*/;
        SafeFileHandle file = WindowsSymbolicLinkManager.NativeMethods.CreateFile(symlinkPath, 0U, FileShare.Delete, IntPtr.Zero, FileMode.Open, dwFlagsAndAttributes, IntPtr.Zero);
        if (file.IsInvalid)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          throw this.WrapWin32Error($"При открытии объекта символической ссылки '{symlinkPath}' произошла ошибка операционной системы.", lastWin32Error);
        }
        try
        {
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(513))
          {
            StringBuilder pathBuffer = objectPoolScope.Object;
            if (WindowsSymbolicLinkManager.NativeMethods.GetFinalPathNameByHandle(file, pathBuffer, 512 /*0x0200*/, 0) == 0)
            {
              int lastWin32Error = Marshal.GetLastWin32Error();
              throw this.WrapWin32Error($"При получении цели для символической ссылки '{symlinkPath}' произошла ошибка операционной системы.", lastWin32Error);
            }
            if (pathBuffer.Length > 4)
              pathBuffer.Replace("\\\\?\\", string.Empty, 0, 4);
            string secondPath = pathBuffer.ToString();
            if (PathUtils.IsSamePath(symlinkPath, secondPath))
              secondPath = (string) null;
            return secondPath;
          }
        }
        finally
        {
          file.Dispose();
        }
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
        public const uint GENERIC_READ = 2147483648 /*0x80000000*/;
        public const uint FILE_FLAG_BACKUP_SEMANTICS = 33554432 /*0x02000000*/;
        public const uint FILE_FLAG_OPEN_REPARSE_POINT = 2097152 /*0x200000*/;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool CreateSymbolicLink(
          string symlinkPath,
          string targetPath,
          SYMBOLIC_LINK_FLAGS flags);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetFinalPathNameByHandle(
          SafeFileHandle fileHandle,
          StringBuilder pathBuffer,
          int pathBufferLength,
          int flags);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
          string lpFileName,
          uint dwDesiredAccess,
          FileShare dwShareMode,
          IntPtr securityAttrs,
          FileMode dwCreationDisposition,
          uint dwFlagsAndAttributes,
          IntPtr hTemplateFile);

        public enum SYMBOLIC_LINK_FLAGS
        {
          SYMBOLIC_LINK_FLAG_FILE,
          SYMBOLIC_LINK_FLAG_DIRECTORY,
        }
      }
    }
}
