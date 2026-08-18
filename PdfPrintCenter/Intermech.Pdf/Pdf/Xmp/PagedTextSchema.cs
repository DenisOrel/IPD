// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.PagedTextSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class PagedTextSchema : XmpSchema
{
  private const string c_Colorants = "Colorants";
  private const string c_Fonts = "Fonts";
  private const string c_MaxPageSize = "MaxPageSize";
  private const string c_name = "http://ns.adobe.com/xap/1.0/t/pg/";
  private const string c_NPages = "NPages";
  private const string c_PlateName = "PlateNames";
  private const string c_prefix = "xmpTPg";

  protected internal PagedTextSchema(XmpMetadata xmp)
    : base(xmp)
  {
  }

  public XmpArray Colorants => this.GetArray(nameof (Colorants), XmpArrayType.Seq);

  public XmpArray Fonts => this.GetArray(nameof (Fonts), XmpArrayType.Bag);

  public XmpDimensionsStruct MaxPageSize
  {
    get
    {
      return this.GetStructure(nameof (MaxPageSize), XmpStructureType.Dimensions) as XmpDimensionsStruct;
    }
  }

  protected override string Name => "http://ns.adobe.com/xap/1.0/t/pg/";

  public int NPages
  {
    get => this.GetSimpleProperty(nameof (NPages)).GetInt();
    set => this.GetSimpleProperty(nameof (NPages)).SetInt(value);
  }

  public XmpArray PlateNames => this.GetArray(nameof (PlateNames), XmpArrayType.Seq);

  protected override string Prefix => "xmpTPg";

  public override XmpSchemaType SchemaType => XmpSchemaType.PagedTextSchema;
}
