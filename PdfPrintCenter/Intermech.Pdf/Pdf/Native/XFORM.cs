// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.XFORM
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Native;

internal struct XFORM
{
  public float eM11;
  public float eM12;
  public float eM21;
  public float eM22;
  public float eDx;
  public float eDy;

  public override string ToString()
  {
    return $"{(object) this.eM11} {(object) this.eM12} {(object) this.eM21} {(object) this.eM22} {(object) this.eDx} {(object) this.eDy}";
  }
}
