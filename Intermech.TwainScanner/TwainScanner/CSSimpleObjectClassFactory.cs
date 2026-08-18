// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.CSSimpleObjectClassFactory
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

/// <summary>Class factory for the class CSSimpleObject.</summary>
internal class CSSimpleObjectClassFactory : IClassFactory
{
  public int CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject)
  {
    ppvObject = IntPtr.Zero;
    if (pUnkOuter != IntPtr.Zero)
      Marshal.ThrowExceptionForHR(-2147221232);
    if (riid == new Guid("3494789E-2865-4D27-9E07-92C39BD5AA40") || riid == new Guid("00020400-0000-0000-C000-000000000046") || riid == new Guid("00000000-0000-0000-C000-000000000046"))
      ppvObject = Marshal.GetComInterfaceForObject((object) new ImTwainScanner(), typeof (IImTwainScanner));
    else
      Marshal.ThrowExceptionForHR(-2147467262 /*0x80004002*/);
    return 0;
  }

  public int LockServer(bool fLock) => 0;
}
