
// Type: Intermech.WindowsDll.Psapi
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
    public static class Psapi
    {
      private const string LibName = "Psapi.dll";
      private const string Namespace = "Psapi::";
      public const int DefaultGetModuleFileNameBufferSize = 4000;

      [DllImport("psapi.dll")]
      public static extern int GetModuleFileNameEx(
        [NotEmpty] IntPtr hProcess,
        [CanBeEmpty] IntPtr hModule,
        [NotNull, Out] StringBuilder lpBaseName,
        [NotEmpty, MarshalAs(UnmanagedType.U4), In] int nSize);

      /// <summary>Вызов WinAPI <see cref="M:Intermech.WindowsDll.Psapi.GetModuleFileNameEx(System.IntPtr,System.IntPtr,System.Text.StringBuilder,System.Int32)" /> с обработкой возвращаемых ошибок, включая <seealso cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <exception cref="T:Intermech.Diagnostics.WindowsApiException">Если вызов метода завершится ошибкой</exception>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string GetModuleFileNameEx_ThrowWinErrors(
        [NotEmpty] IntPtr hProcess,
        [CanBeEmpty] IntPtr hModule,
        [PositiveNumber] int bufferSize = 4000)
      {
        string result;
        Exception exception;
        if (Psapi.TryGetModuleFileNameEx(hProcess, hModule, bufferSize, out result, out exception))
          return result;
        throw exception;
      }

      [ContractAnnotation("=> true, exception: null, result: notnull; => false, exception: notnull, result: null")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetModuleFileNameEx(
        [NotEmpty] IntPtr hProcess,
        [CanBeEmpty] IntPtr hModule,
        out string result,
        out Exception exception)
      {
        return Psapi.TryGetModuleFileNameEx(hProcess, hModule, 4000, out result, out exception);
      }

      [ContractAnnotation("=> true, exception: null, result: notnull; => false, exception: notnull, result: null")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetModuleFileNameEx(
        [NotEmpty] IntPtr hProcess,
        [CanBeEmpty] IntPtr hModule,
        [PositiveNumber] int bufferSize,
        out string result,
        out Exception exception)
      {
        StringBuilder lpBaseName = new StringBuilder(bufferSize);
        int moduleFileNameEx;
        try
        {
          moduleFileNameEx = Psapi.GetModuleFileNameEx(hProcess, hModule, lpBaseName, bufferSize);
        }
        catch (Exception ex)
        {
          exception = ex;
          result = (string) null;
          return false;
        }
        if (moduleFileNameEx <= 0)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          exception = (Exception) WindowsApiException.GetLastForce("Psapi::GetModuleFileNameEx", (ArgumentDescriptor) @(typeof (IntPtr), (object) hProcess), (ArgumentDescriptor) @(typeof (IntPtr), (object) hModule), (ArgumentDescriptor) typeof (StringBuilder), (ArgumentDescriptor) @(typeof (int), (object) bufferSize));
          result = (string) null;
          return false;
        }
        exception = (Exception) null;
        result = lpBaseName.ToString(0, moduleFileNameEx);
        return true;
      }
    }
}
