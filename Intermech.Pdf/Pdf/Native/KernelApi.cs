// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.KernelApi
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Runtime.InteropServices;


namespace Syncfusion.Pdf.Native
{
    internal sealed class KernelApi
    {
      private KernelApi() => throw new NotImplementedException();

      [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
      internal static extern bool FileTimeToSystemTime(IntPtr lpFileTime, ref SYSTEMTIME lpSystemTime);

      [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
      internal static extern uint FormatMessage(
        FormatMessageFlags dwFlags,
        IntPtr lpSource,
        uint messageId,
        uint dwLanguageId,
        IntPtr lpBuffer,
        uint nSize,
        IntPtr Arguments);

      [DllImport("kernel32.dll")]
      internal static extern uint GetLastError();

      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      internal static extern bool GetStringTypeExW(
        uint Locale,
        StringInfoType dwInfoType,
        string lpSrcStr,
        int cchSrc,
        [Out] ushort[] lpCharType);
    }
}
