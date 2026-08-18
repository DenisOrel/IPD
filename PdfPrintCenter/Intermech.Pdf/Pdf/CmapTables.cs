// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.CmapTables
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal abstract class CmapTables
{
  private ushort m_firstcode;

  public abstract ushort GetGlyphId(ushort charCode);

  public abstract void Read(ReadFontArray reader);

  public static CmapTables ReadCmapTable(ReadFontArray reader)
  {
    CmapTables cmapTables;
    switch (reader.getnextUint16())
    {
      case 4:
        cmapTables = (CmapTables) new Cmap4();
        break;
      case 6:
        cmapTables = (CmapTables) new Cmap6();
        break;
      default:
        cmapTables = (CmapTables) new Cmap0();
        break;
    }
    cmapTables.Read(reader);
    return cmapTables;
  }

  public abstract ushort FirstCode { get; }
}
