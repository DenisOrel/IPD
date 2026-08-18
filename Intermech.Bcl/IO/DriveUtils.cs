
// Type: Intermech.IO.DriveUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.IO
{
    public static class DriveUtils
    {
      /// <summary>
      /// Возвращает свободное место на указанном диске в байтах.
      /// Метод поддерживает как локальные диски, так UNC-ресурсы.
      /// </summary>
      /// <param name="driveRoot">Путь к корневому каталогу диска</param>
      /// <returns>Свободное место на диске в байтах</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="driveRoot" /> содержит null</exception>
      /// <exception cref="T:System.ComponentModel.Win32Exception">Ошибка при получении свободного места</exception>
      public static long GetAvailableFreeSpace(string driveRoot)
      {
        if (driveRoot == null)
          throw new ArgumentNullException(nameof (driveRoot));
        int newMode = DriveUtils.NativeMethods.SetErrorMode(1);
        try
        {
          long freeBytesForUser;
          if (DriveUtils.NativeMethods.GetDiskFreeSpaceEx(driveRoot, out freeBytesForUser, out long _, out long _))
            return freeBytesForUser;
          throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        finally
        {
          DriveUtils.NativeMethods.SetErrorMode(newMode);
        }
      }

      public static void MapDrive(char driveLetter, string path)
      {
        if (path == null)
          throw new ArgumentNullException(nameof (path));
        if (!Directory.Exists(path))
          throw new DirectoryNotFoundException(LocalizationHolder.rm.GetString("SR_1667"));
        if (!DriveUtils.NativeMethods.DefineDosDevice(0, DriveUtils.GetDosDrive(driveLetter), path))
          throw new Win32Exception(Marshal.GetLastWin32Error());
      }

      public static void UnmapDrive(char driveLetter)
      {
        if (!DriveUtils.NativeMethods.DefineDosDevice(2, DriveUtils.GetDosDrive(driveLetter), (string) null))
          throw new Win32Exception(Marshal.GetLastWin32Error());
      }

      public static void UnmapDrive(char driveLetter, string path)
      {
        if (path == null)
          throw new ArgumentNullException(nameof (path));
        if (!DriveUtils.NativeMethods.DefineDosDevice(6, DriveUtils.GetDosDrive(driveLetter), path))
          throw new Win32Exception(Marshal.GetLastWin32Error());
      }

      /// <summary>Возвращает путь, связанный с указанной буквой диска.</summary>
      /// <param name="driveLetter">Буква диска</param>
      /// <returns>Путь, связанный с буквой диска. Если null, значит такая буква диска не используется. Если "", значит буква диска используется устройством</returns>
      public static string GetMappedPath(char driveLetter)
      {
        string dosDrive = DriveUtils.GetDosDrive(driveLetter);
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(276))
        {
          StringBuilder lpTargetPath = objectPoolScope.Object;
          if (DriveUtils.NativeMethods.QueryDosDevice(dosDrive, lpTargetPath, lpTargetPath.Capacity) <= 0)
          {
            int lastWin32Error = Marshal.GetLastWin32Error();
            if (lastWin32Error == 2)
              return (string) null;
            throw new Win32Exception(lastWin32Error);
          }
          string str = lpTargetPath.ToString();
          if (str.StartsWith("\\Device\\") || str.StartsWith("\\??\\UNC\\"))
            return string.Empty;
          if (lpTargetPath.Length >= 4)
            lpTargetPath.Replace("\\??\\", string.Empty, 0, 4);
          return lpTargetPath.ToString();
        }
      }

      private static string GetDosDrive(char driveLetter)
      {
        if (char.IsLetter(driveLetter))
        {
          driveLetter = char.ToUpperInvariant(driveLetter);
          if (driveLetter >= 'A' && driveLetter <= 'Z')
          {
            using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
            {
              StringBuilder stringBuilder = objectPoolScope.Object;
              stringBuilder.Append(driveLetter);
              stringBuilder.Append(':');
              return stringBuilder.ToString();
            }
          }
        }
        throw new ArgumentOutOfRangeException(nameof (driveLetter));
      }

      private static class NativeMethods
      {
        public const int DDD_EXACT_MATCH_ON_REMOVE = 4;
        public const int DDD_NO_BROADCAST_SYSTEM = 8;
        public const int DDD_RAW_TARGET_PATH = 1;
        public const int DDD_REMOVE_DEFINITION = 2;

        public static int SetErrorMode(int newMode)
        {
          int oldMode;
          DriveUtils.NativeMethods.SetErrorMode_Win7AndNewer(newMode, out oldMode);
          return oldMode;
        }

        [DllImport("kernel32.dll", EntryPoint = "SetThreadErrorMode", SetLastError = true)]
        public static extern bool SetErrorMode_Win7AndNewer(int newMode, out int oldMode);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        public static extern bool GetDiskFreeSpaceEx(
          string drive,
          out long freeBytesForUser,
          out long totalBytes,
          out long freeBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DefineDosDevice(int flags, string lpDeviceName, string lpTargetPath);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int QueryDosDevice(
          string lpDeviceName,
          StringBuilder lpTargetPath,
          int ucchMax);
      }
    }
}
