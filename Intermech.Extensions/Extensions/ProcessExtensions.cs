// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ProcessExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Diagnostics;
using System.Security.Principal;

#nullable disable
namespace Intermech.Extensions;

public static class ProcessExtensions
{
  [NotNull]
  [NotWhitespace]
  public static string GetMainModuleFileName([NotNull] this Process process, bool quiet = true)
  {
    IntPtr result1;
    Exception exception1;
    if (!Kernel32.TryOpenProcess(ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.QueryInformation, false, process.Id, out result1, out exception1))
    {
      if (!quiet)
        throw exception1;
      return !(exception1 is WindowsApiException windowsApiException) ? "Unknown: " + exception1.Message : "Unknown: " + windowsApiException.CodeAndShortText;
    }
    try
    {
      string result2;
      Exception exception2;
      if (Psapi.TryGetModuleFileNameEx(result1, IntPtr.Zero, out result2, out exception2))
        return result2;
      if (!quiet)
        throw exception2;
      return exception2 is WindowsApiException windowsApiException ? "Unknown: " + windowsApiException.CodeAndShortText : "Unknown: " + exception2.Message;
    }
    finally
    {
      Kernel32.CloseHandle(result1);
    }
  }

  [NotNull]
  public static string GetProcessOwner([NotNull] this Process process, bool quiet = true)
  {
    IntPtr tokenHandle = IntPtr.Zero;
    try
    {
      Exception exception;
      if (!Advapi32.TryOpenProcessToken(process.Handle, TokenAccessRights.Query, out tokenHandle, out exception))
      {
        if (!quiet)
          throw exception;
        return exception is WindowsApiException windowsApiException ? "Unknown: " + windowsApiException.CodeAndShortText : "Unknown: " + exception.Message;
      }
      using (WindowsIdentity windowsIdentity = new WindowsIdentity(tokenHandle))
        return windowsIdentity.Name;
    }
    finally
    {
      if (tokenHandle != IntPtr.Zero)
        Kernel32.CloseHandle(tokenHandle);
    }
  }
}
