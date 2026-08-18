
// Type: Intermech.Protection.Win32
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Protection
{
    internal class Win32
    {
      internal const int FILE_SHARE_READ = 1;
      internal const int FILE_SHARE_WRITE = 2;
      internal const int OPEN_EXISTING = 3;
      internal const int PIPE_READMODE_MESSAGE = 2;
      internal const int PIPE_WAIT = 0;
      internal const uint FILE_FLAG_WRITE_THROUGH = 2147483648 /*0x80000000*/;
      internal const uint FILE_FLAG_NO_BUFFERING = 536870912 /*0x20000000*/;
      internal const int MAX_COMPUTERNAME_LENGTH = 15;
      internal const int GENERIC_READ = -2147483648 /*0x80000000*/;
      internal const int GENERIC_WRITE = 1073741824 /*0x40000000*/;
      internal const int ERROR_PIPE_BUSY = 231;
      internal const int INVALID_HANDLE_VALUE = -1;
      internal const int SOCKET_ERROR = -1;

      [DllImport("user32")]
      internal static extern int GetSystemMetrics(int nIndex);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      internal static extern int CreateFile(
        string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        int dwCreationDisposition,
        uint dwFlagsAndAttributes,
        int hTemplateFile);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      internal static extern bool WaitNamedPipe(string lpNamedPipeName, int nTimeOut);

      [DllImport("kernel32.dll", SetLastError = true)]
      internal static extern bool SetNamedPipeHandleState(
        int hNamedPipe,
        ref int lpMode,
        int lpMaxCollectionCount,
        int lpCollectDataTimeout);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
      internal static extern bool CloseHandle(int handle);

      [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
      internal static extern bool GetComputerName(byte[] lpBuffer, ref int nSize);

      [DllImport("kernel32.dll", SetLastError = true)]
      internal static extern int GetCurrentProcessId();

      [DllImport("kernel32.dll", SetLastError = true)]
      internal static extern bool ProcessIdToSessionId(int dwProcessId, ref int pSessionId);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      internal static extern bool WriteFile(
        int hFile,
        byte[] lpBuffer,
        int nNumberOfBytesToWrite,
        out int lpNumberOfBytesWritten,
        int lpOverLapped);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      internal static extern bool ReadFile(
        int hFile,
        byte[] lpBuffer,
        int nNumberOfBytesToRead,
        out int lpNumberOfBytesRead,
        int lpOverLapped);

      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      internal static extern int MultiByteToWideChar(
        int CodePage,
        int dwFlags,
        byte[] lpMultiByteStr,
        int cchMultiByte,
        char[] lpWideCharStr,
        int cchWideChar);

      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      internal static extern int WideCharToMultiByte(
        int CodePage,
        int dwFlags,
        string lpWideCharStr,
        int cchWideChar,
        byte[] lpMultiByteStr,
        int cchMultiByte,
        int lpDefaultChar,
        int lpUsedDefaultChar);

      [DllImport("ws2_32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
      internal static extern int recv(
        IntPtr socketHandle,
        byte[] pinnedBuffer,
        int len,
        int socketFlags);

      [DllImport("ws2_32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
      internal static extern int send(
        IntPtr socketHandle,
        byte[] pinnedBuffer,
        int len,
        int socketFlags);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      internal static extern int GetVolumeInformation(
        string lpRootPathName,
        StringBuilder lpVolumeNameBuffer,
        int nVolumeNameSize,
        ref int lpVolumeSerialNumber,
        ref int lpMaximumComponentLength,
        ref int lpFileSystemFlags,
        StringBuilder lpFileSystemNameBuffer,
        int nFileSystemNameSize);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern int FormatMessage(
        int dwFlags,
        int handleRef,
        int dwMessageId,
        int dwLanguageId,
        StringBuilder lpBuffer,
        int nSize,
        IntPtr arguments);

      internal static object GetErrorMessage(int win32ErrorCode)
      {
        StringBuilder lpBuffer = new StringBuilder(256 /*0x0100*/);
        if (Win32.FormatMessage(12800, 0, win32ErrorCode, 0, lpBuffer, lpBuffer.Capacity + 1, IntPtr.Zero) == 0)
          return (object) $"Unknown error (0x{Convert.ToString(win32ErrorCode, 16 /*0x10*/)})";
        int length;
        for (length = lpBuffer.Length; length > 0; --length)
        {
          char ch = lpBuffer[length - 1];
          if (ch > ' ' && ch != '.')
            break;
        }
        return (object) lpBuffer.ToString(0, length);
      }
    }
}
