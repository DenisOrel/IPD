// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwCapability
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Archives.ScanDocums;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal class TwCapability
{
  public short Cap;
  public short ConType;
  public IntPtr Handle;

  public TwCapability(TwCap cap)
  {
    this.Cap = (short) cap;
    this.ConType = (short) -1;
  }

  public TwCapability(TwCap cap, short sval)
  {
    this.Cap = (short) cap;
    this.ConType = (short) 5;
    this.Handle = IntermechTwainDriver.GlobalAlloc(66, 6);
    IntPtr ptr = IntermechTwainDriver.GlobalLock(this.Handle);
    Marshal.WriteInt16(ptr, 0, (short) 1);
    Marshal.WriteInt32(ptr, 2, (int) sval);
    IntermechTwainDriver.GlobalUnlock(this.Handle);
  }

  ~TwCapability()
  {
    if (!(this.Handle != IntPtr.Zero))
      return;
    IntermechTwainDriver.GlobalFree(this.Handle);
  }
}
