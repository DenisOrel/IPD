// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.codestream.CBlkCoordInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.codestream;

internal class CBlkCoordInfo : CoordInfo
{
  public JPXImageCoordinates idx;

  public CBlkCoordInfo() => this.idx = new JPXImageCoordinates();

  public CBlkCoordInfo(int m, int n) => this.idx = new JPXImageCoordinates(n, m);

  public override string ToString() => $"{base.ToString()},idx={(object) this.idx}";
}
