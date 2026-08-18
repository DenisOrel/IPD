// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwIdentity
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Archives.ScanDocums;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal class TwIdentity
{
  private IntPtr id;
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

  public IntPtr Id
  {
    get => this.id;
    set => this.id = value;
  }
}
