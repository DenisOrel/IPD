// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwFix32
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Archives.ScanDocums;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal struct TwFix32
{
  public short Whole;
  public ushort Frac;

  public float ToFloat() => (float) this.Whole + (float) this.Frac / 65536f;

  public void FromFloat(float f)
  {
    int num = (int) ((double) f * 65536.0 + 0.5);
    this.Whole = (short) (num >> 16 /*0x10*/);
    this.Frac = (ushort) (num & (int) ushort.MaxValue);
  }
}
