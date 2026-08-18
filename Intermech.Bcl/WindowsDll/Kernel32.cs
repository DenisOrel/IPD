
// Type: Intermech.WindowsDll.Kernel32
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
    [CLSCompliant(false)]
    public static class Kernel32
    {
      private const string LibName = "Kernel32.dll";
      private const string Namespace = "Kernel32::";

      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern IntPtr GlobalLock([NotEmpty] IntPtr hMem);

      /// <summary>Вызов WinAPI <see cref="M:Intermech.WindowsDll.Kernel32.GlobalLock(System.IntPtr)" /> с обработкой возвращаемых ошибок, включая <seealso cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IntPtr GlobalLock_ThrowWinErrors([NotEmpty] IntPtr hMem)
      {
        IntPtr num = Kernel32.GlobalLock(hMem);
        // ISSUE: explicit reference operation
        return !(num == IntPtr.Zero) ? num : throw WindowsApiException.GetLastForce("Kernel32::GlobalLock", (ArgumentDescriptor) @(typeof (IntPtr), (object) hMem));
      }

      /// <summary>
      /// См. описание https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-globalunlock
      ///  если GlobalUnlock вернул true это значит всё ок, память разблокирована
      ///  если GlobalUnlock вернул false это значит, что память не разблокирована
      ///     и это произошло либо по причине ошибки (GetLastError вернёт код ошибки)
      ///     либо по причине того, что счётчик блокировок ещё не обнулился (GetLastError вернёт 0)
      /// </summary>
      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern bool GlobalUnlock(IntPtr hMem);

      /// <summary>Вызов WinAPI <see cref="M:Intermech.WindowsDll.Kernel32.GlobalUnlock(System.IntPtr)" /> с обработкой возвращаемых ошибок, включая <seealso cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool GlobalUnlock_ThrowWinErrors([CanBeEmpty] IntPtr hMem)
      {
        if (Kernel32.GlobalUnlock(hMem))
          return true;
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error != 0)
        {
          // ISSUE: explicit reference operation
          throw new WindowsApiException(lastWin32Error, "Kernel32::GlobalUnlock", new ArgumentDescriptor[1]
          {
            (ArgumentDescriptor) @(typeof (IntPtr), (object) hMem)
          });
        }
        return false;
      }

      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern IntPtr GlobalFree(IntPtr hMem);

      /// <summary>Вызов WinAPI <see cref="M:Intermech.WindowsDll.Kernel32.GlobalFree(System.IntPtr)" /> с обработкой возвращаемых ошибок, включая <seealso cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      public static void GlobalFree_ThrowWinErrors([CanBeEmpty] IntPtr hMem)
      {
        if (!(hMem == IntPtr.Zero) && Kernel32.GlobalFree(hMem) != IntPtr.Zero)
        {
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("GlobalFree", (ArgumentDescriptor) @(typeof (IntPtr), (object) hMem));
        }
      }

      /// <summary>Retrieves the thread identifier of the calling thread</summary>
      [DllImport("kernel32.dll")]
      public static extern int GetCurrentThreadId();

      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern uint FormatMessage(
        [MarshalAs(UnmanagedType.U4)] FormatMessageFlags dwFlags,
        IntPtr lpSource,
        uint dwMessageId,
        uint dwLanguageId,
        ref IntPtr lpBuffer,
        uint nSize,
        IntPtr argumentsLong);

      [DllImport("kernel32.dll", SetLastError = true)]
      internal static extern IntPtr LocalFree(IntPtr hMem);

      /// <summary>Вызов WinAPI <see cref="M:Intermech.WindowsDll.Kernel32.LocalFree(System.IntPtr)" /> с обработкой возвращаемых ошибок, включая <seealso cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      public static void LocalFree_ThrowWinErrors([CanBeEmpty] IntPtr hMem)
      {
        if (!(hMem == IntPtr.Zero) && Kernel32.LocalFree(hMem) != IntPtr.Zero)
        {
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("LocalFree", (ArgumentDescriptor) @(typeof (IntPtr), (object) hMem));
        }
      }

      [DllImport("kernel32.dll")]
      public static extern IntPtr OpenProcess(
        [MarshalAs(UnmanagedType.U4)] ProcessAccessFlags processAccess,
        bool bInheritHandle,
        int processId);

      /// <summary>Вызов WinAPI <see cref="M:Intermech.WindowsDll.Kernel32.OpenProcess(Intermech.WindowsDll.ProcessAccessFlags,System.Boolean,System.Int32)" /> с обработкой возвращаемых ошибок, включая <seealso cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IntPtr OpenProcess_ThrowWinErrors(
        ProcessAccessFlags processAccess,
        bool bInheritHandle,
        int processId)
      {
        IntPtr result;
        Exception exception;
        if (Kernel32.TryOpenProcess(processAccess, bInheritHandle, processId, out result, out exception))
          return result;
        throw exception;
      }

      [ContractAnnotation("=> true, exception: null; => false, exception: notnull")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryOpenProcess(
        ProcessAccessFlags processAccess,
        bool bInheritHandle,
        int processId,
        out IntPtr result,
        out Exception exception)
      {
        try
        {
          result = Kernel32.OpenProcess(processAccess, bInheritHandle, processId);
        }
        catch (Exception ex)
        {
          exception = ex;
          result = IntPtr.Zero;
          return false;
        }
        if (result == IntPtr.Zero)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          exception = (Exception) WindowsApiException.GetLastForce("Kernel32::OpenProcess", (ArgumentDescriptor) @(typeof (uint), (object) processAccess), (ArgumentDescriptor) @(typeof (bool), (object) bInheritHandle), (ArgumentDescriptor) @(typeof (int), (object) processId));
          return false;
        }
        exception = (Exception) null;
        return true;
      }

      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool CloseHandle(IntPtr hObject);

      /// <summary>Вызов WinAPI <see cref="M:Intermech.WindowsDll.Kernel32.CloseHandle(System.IntPtr)" /> с обработкой возвращаемых ошибок, включая <seealso cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void CloseHandle_ThrowWinErrors([CanBeEmpty] IntPtr hObject)
      {
        Exception exception;
        if (!Kernel32.TryCloseHandle(hObject, out exception))
          throw exception;
      }

      [ContractAnnotation("=> true, exception: null; => false, exception: notnull")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryCloseHandle([CanBeEmpty] IntPtr hObject, out Exception exception)
      {
        if (hObject == IntPtr.Zero)
        {
          exception = (Exception) null;
          return true;
        }
        bool flag;
        try
        {
          flag = Kernel32.CloseHandle(hObject);
        }
        catch (Exception ex)
        {
          exception = ex;
          return false;
        }
        if (!flag)
        {
          // ISSUE: explicit reference operation
          exception = (Exception) WindowsApiException.GetLastForce("Kernel32::CloseHandle", (ArgumentDescriptor) @(typeof (IntPtr), (object) hObject));
          return false;
        }
        exception = (Exception) null;
        return true;
      }

      /// <summary>Copies a string into the specified section of an initialization file</summary>
      /// <param name="section">
      /// The name of the section to which the string will be copied.
      /// If the section does not exist, it is created.
      /// The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.
      /// </param>
      /// <param name="key">
      /// The name of the key to be associated with a string.
      /// If the key does not exist in the specified section, it is created.
      /// If this parameter is NULL, the entire section, including all entries within the section, is deleted.
      /// </param>
      /// <param name="value">String to be written to the file. If this parameter is NULL, the key pointed to by the lpKeyName parameter is deleted</param>
      /// <param name="filePath">The name of the initialization file</param>
      /// <returns>True if it succeeds, false if it fails. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></returns>
      [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
      public static extern bool WritePrivateProfileString(
        [CanBeNull, NotWhitespace] string section,
        [CanBeNull, NotWhitespace] string key,
        [CanBeNull] string value,
        [NotNull, NotWhitespace, FileExists] string filePath);

      /// <summary>Copies a string into the specified section of an initialization file</summary>
      /// <param name="section">
      /// The name of the section to which the string will be copied.
      /// If the section does not exist, it is created.
      /// The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.
      /// </param>
      /// <param name="key">
      /// The name of the key to be associated with a string.
      /// If the key does not exist in the specified section, it is created.
      /// If this parameter is NULL, the entire section, including all entries within the section, is deleted.
      /// </param>
      /// <param name="value">String to be written to the file. If this parameter is NULL, the key pointed to by the lpKeyName parameter is deleted</param>
      /// <param name="filePath">The name of the initialization file</param>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void WritePrivateProfileString_ThrowWinErrors(
        [CanBeNull, NotWhitespace] string section,
        [CanBeNull, NotWhitespace] string key,
        [CanBeNull] string value,
        [NotNull, NotWhitespace, FileExists] string filePath)
      {
        if (!Kernel32.WritePrivateProfileString(section, key, value, filePath))
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          throw WindowsApiException.GetLastForce("Kernel32::WritePrivateProfileString", (ArgumentDescriptor) @(typeof (string), (object) section), (ArgumentDescriptor) @(typeof (string), (object) key), (ArgumentDescriptor) @(typeof (string), (object) value), (ArgumentDescriptor) @(typeof (string), (object) filePath));
        }
      }

      /// <summary>Retrieves a string from the specified section in an initialization file</summary>
      /// <param name="section">
      /// The name of the section containing the key name.
      /// If this parameter is NULL, the GetPrivateProfileString function copies all section names in the file to the supplied buffer.
      /// </param>
      /// <param name="key">
      /// The name of the key whose associated string is to be retrieved.
      /// If this parameter is NULL, all key names in the section specified by the lpAppName parameter are copied
      /// to the buffer specified by the <see cref="!:retVal" /> parameter.
      /// </param>
      /// <param name="defaultValue">
      /// A default string.
      /// If the lpKeyName key cannot be found in the initialization file, method copies the default string to the <see cref="!:retVal" /> buffer.
      /// If this parameter is NULL, the default is an empty string, "".
      /// Avoid specifying a default string with trailing blank characters. The function inserts a null character
      /// in the <see cref="!:retVal" /> buffer to strip any trailing blanks.
      /// </param>
      /// <param name="retVal">Buffer that receives the retrieved string</param>
      /// <param name="size">The size of the buffer pointed to by the <see cref="!:retVal" /> parameter, in characters.</param>
      /// <param name="filePath">
      /// The name of the initialization file.
      /// If this parameter does not contain a full path to the file, the system searches for the file in the Windows directory.
      /// </param>
      /// <returns>
      /// Number of characters copied to the buffer, not including the terminating null character.
      /// 
      /// If neither <see cref="!:section" /> nor <see cref="!:key" /> is NULL and the supplied destination buffer is too small
      /// to hold the requested string, the string is truncated and followed by a null character,
      /// and the return value is equal to <see cref="!:size" /> minus one.
      /// 
      /// If either lpAppName or <see cref="!:key" /> is NULL and the supplied destination buffer is too small to hold all the strings,
      /// the last string is truncated and followed by two null characters. In this case, the return value is equal to <see cref="!:size" /> minus two.
      /// 
      /// In the event the initialization file specified by <see cref="!:filePath" /> is not found, or contains invalid values,
      /// calling <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /> will return '0x2' (File Not Found).
      /// To retrieve extended error information, call GetLastError.
      /// </returns>
      [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
      public static extern int GetPrivateProfileString(
        [NotNull, NotWhitespace] string section,
        [NotNull, NotWhitespace] string key,
        [CanBeNull] string defaultValue,
        [NotNull] StringBuilder retVal,
        int size,
        [NotNull, FileExists] string filePath);

      /// <summary>Retrieves a string from the specified section in an initialization file</summary>
      /// <param name="section">
      /// The name of the section containing the key name.
      /// If this parameter is NULL, the GetPrivateProfileString function copies all section names in the file to the supplied buffer.
      /// </param>
      /// <param name="key">
      /// The name of the key whose associated string is to be retrieved.
      /// If this parameter is NULL, all key names in the section specified by the lpAppName parameter are copied
      /// to the buffer specified by the <see cref="!:retVal" /> parameter.
      /// </param>
      /// <param name="defaultValue">
      /// A default string.
      /// If the lpKeyName key cannot be found in the initialization file, method copies the default string to the <see cref="!:retVal" /> buffer.
      /// If this parameter is NULL, the default is an empty string, "".
      /// Avoid specifying a default string with trailing blank characters. The function inserts a null character
      /// in the <see cref="!:retVal" /> buffer to strip any trailing blanks.
      /// </param>
      /// <param name="retVal">Buffer that receives the retrieved string</param>
      /// <param name="size">The size of the buffer pointed to by the <see cref="!:retVal" /> parameter, in characters.</param>
      /// <param name="filePath">
      /// The name of the initialization file.
      /// If this parameter does not contain a full path to the file, the system searches for the file in the Windows directory.
      /// </param>
      /// <returns>
      /// Number of characters copied to the buffer, not including the terminating null character.
      /// 
      /// If neither <see cref="!:section" /> nor <see cref="!:key" /> is NULL and the supplied destination buffer is too small
      /// to hold the requested string, the string is truncated and followed by a null character,
      /// and the return value is equal to <see cref="!:size" /> minus one.
      /// 
      /// If either lpAppName or <see cref="!:key" /> is NULL and the supplied destination buffer is too small to hold all the strings,
      /// the last string is truncated and followed by two null characters. In this case, the return value is equal to <see cref="!:size" /> minus two.
      /// 
      /// In the event the initialization file specified by <see cref="!:filePath" /> is not found, or contains invalid values,
      /// calling <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /> will return '0x2' (File Not Found).
      /// To retrieve extended error information, call GetLastError.
      /// </returns>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetPrivateProfileString_ThrowWinErrors(
        [NotNull, NotWhitespace] string section,
        [NotNull, NotWhitespace] string key,
        [CanBeNull] string defaultValue,
        [NotNull, FileExists] string filePath,
        [PositiveNumber] int bufferSize = 500)
      {
        StringBuilder retVal = new StringBuilder(bufferSize);
        int privateProfileString = Kernel32.GetPrivateProfileString(section, key, defaultValue, retVal, bufferSize, filePath);
        return privateProfileString != 0 ? retVal.ToString(0, privateProfileString) : string.Empty;
      }

      /// <summary>
      /// Loads the specified module into the address space of the calling process.
      /// The specified module may cause other modules to be loaded.
      /// </summary>
      /// <param name="lpFileName">
      /// The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).
      /// The name specified is the file name of the module and is not related to the name stored in the library module itself,
      ///   as specified by the LIBRARY keyword in the module-definition (.def) file.
      /// If the string specifies a full path, the function searches only that path for the module.
      /// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module.
      /// If the function cannot find the module, the function fails. When specifying a path, be sure to use backslashes (\), not forward slashes (/).
      /// If the string specifies a module name without a path and the file name extension is omitted,
      ///   the function appends the default library extension .dll to the module name.
      ///   To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.
      /// </param>
      /// <returns>
      /// If the function succeeds, the return value is a handle to a to the module.
      /// If the function fails, the return value is NULL. To get extended error information, call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" />
      /// </returns>
      [CanBeEmpty]
      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern IntPtr LoadLibrary([NotNull] string lpFileName);

      /// <summary>
      /// Loads the specified module into the address space of the calling process.
      /// The specified module may cause other modules to be loaded.
      /// </summary>
      /// <param name="lpFileName">
      /// The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).
      /// The name specified is the file name of the module and is not related to the name stored in the library module itself,
      ///   as specified by the LIBRARY keyword in the module-definition (.def) file.
      /// If the string specifies a full path, the function searches only that path for the module.
      /// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module.
      /// If the function cannot find the module, the function fails. When specifying a path, be sure to use backslashes (\), not forward slashes (/).
      /// If the string specifies a module name without a path and the file name extension is omitted,
      ///   the function appends the default library extension .dll to the module name.
      ///   To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.
      /// </param>
      /// <returns>Handle to a to the module</returns>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [NotEmpty]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IntPtr LoadLibrary_ThrowWinErrors([NotNull, NotWhitespace] string lpFileName)
      {
        IntPtr libHandle;
        Exception exception;
        if (!Kernel32.TryLoadLibrary(lpFileName, out libHandle, out exception))
          throw exception;
        return libHandle;
      }

      /// <summary>
      /// Try to loads the specified module into the address space of the calling process.
      /// The specified module may cause other modules to be loaded.
      /// </summary>
      /// <param name="lpFileName">
      /// The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).
      /// The name specified is the file name of the module and is not related to the name stored in the library module itself,
      ///   as specified by the LIBRARY keyword in the module-definition (.def) file.
      /// If the string specifies a full path, the function searches only that path for the module.
      /// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module.
      /// If the function cannot find the module, the function fails. When specifying a path, be sure to use backslashes (\), not forward slashes (/).
      /// If the string specifies a module name without a path and the file name extension is omitted,
      ///   the function appends the default library extension .dll to the module name.
      ///   To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.
      /// </param>
      /// <param name="libHandle">Handle of the library</param>
      /// <param name="exception">The exception. This may be null</param>
      /// <returns>True if it succeeds, false if it fails</returns>
      [ContractAnnotation("=> true, exception: null; => false, exception: notnull")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryLoadLibrary(
        [NotNull, NotWhitespace] string lpFileName,
        [CanBeEmpty] out IntPtr libHandle,
        out Exception exception)
      {
        try
        {
          libHandle = Kernel32.LoadLibrary(lpFileName);
        }
        catch (Exception ex)
        {
          exception = ex;
          libHandle = IntPtr.Zero;
          return false;
        }
        if (libHandle == IntPtr.Zero)
        {
          // ISSUE: explicit reference operation
          exception = (Exception) WindowsApiException.GetLastForce("Kernel32::LoadLibrary", (ArgumentDescriptor) @(typeof (string), (object) lpFileName));
          return false;
        }
        exception = (Exception) null;
        return true;
      }
    }
}
