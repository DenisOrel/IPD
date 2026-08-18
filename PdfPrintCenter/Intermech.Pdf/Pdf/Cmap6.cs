// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Cmap6
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class Cmap6 : CmapTables
{
  private ushort firstCode;
  private ushort[] glyphIdArray;

  public override ushort GetGlyphId(ushort charCode)
  {
    return (int) this.firstCode <= (int) charCode && (int) charCode < (int) this.firstCode + this.glyphIdArray.Length ? this.glyphIdArray[(int) charCode - (int) this.firstCode] : (ushort) 0;
  }

  public override void Read(ReadFontArray reader)
  {
    int num1 = (int) reader.getnextUshort();
    int num2 = (int) reader.getnextUshort();
    this.firstCode = reader.getnextUshort();
    ushort length = reader.getnextUshort();
    this.glyphIdArray = new ushort[(int) length];
    for (int index = 0; index < (int) length; ++index)
      this.glyphIdArray[index] = reader.getnextUshort();
  }

  public override ushort FirstCode => this.firstCode;
}
