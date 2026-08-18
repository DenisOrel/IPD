// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwIdentity
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Archives.ScanDocums;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal class TwIdentity
{
  public IntPtr Id;
  public TwVersion Version;
  public short ProtocolMajor;
  public short ProtocolMinor;
  public int SupportedGroups;
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
  public string Manufacturer;
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
  public string ProductFamily;
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
  public string ProductName;
}
