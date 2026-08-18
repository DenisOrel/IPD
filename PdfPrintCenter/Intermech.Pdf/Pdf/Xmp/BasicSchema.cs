// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.BasicSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class BasicSchema : XmpSchema
{
  private const string c_name = "http://ns.adobe.com/xap/1.0/";
  private const string c_prefix = "xap";
  private const string c_propAdvisory = "Advisory";
  private const string c_propBaseUrl = "BaseURL";
  private const string c_propCreateData = "CreateDate";
  private const string c_propCreatorTool = "CreatorTool";
  private const string c_propIdentifier = "Identifier";
  private const string c_propLabel = "Label";
  private const string c_propMetadataDate = "MetadataDate";
  private const string c_propModifyDate = "ModifyDate";
  private const string c_propNickname = "Nickname";
  private const string c_propRating = "Rating";
  private const string c_propThumbnail = "Thumbnails";

  protected internal BasicSchema(XmpMetadata xmp)
    : base(xmp)
  {
  }

  public XmpArray Advisory => this.GetArray(nameof (Advisory), XmpArrayType.Bag);

  public Uri BaseURL
  {
    get => this.GetSimpleProperty(nameof (BaseURL)).GetUri();
    set
    {
      if (value == (Uri) null)
        throw new ArgumentNullException(nameof (BaseURL));
      this.GetSimpleProperty(nameof (BaseURL)).SetUri(value);
    }
  }

  public DateTime CreateDate
  {
    get => this.GetSimpleProperty(nameof (CreateDate)).GetDateTime();
    set => this.GetSimpleProperty(nameof (CreateDate)).SetDateTime(value);
  }

  public string CreatorTool
  {
    get => this.GetSimpleProperty(nameof (CreatorTool)).Value;
    set
    {
      this.GetSimpleProperty(nameof (CreatorTool)).Value = value != null ? value : throw new ArgumentNullException(nameof (CreatorTool));
    }
  }

  public XmpArray Identifier => this.GetArray(nameof (Identifier), XmpArrayType.Bag);

  public string Label
  {
    get => this.GetSimpleProperty(nameof (Label)).Value;
    set
    {
      this.GetSimpleProperty(nameof (Label)).Value = value != null ? value : throw new ArgumentNullException(nameof (Label));
    }
  }

  public DateTime MetadataDate
  {
    get => this.GetSimpleProperty(nameof (MetadataDate)).GetDateTime();
    set => this.GetSimpleProperty(nameof (MetadataDate)).SetDateTime(value);
  }

  public DateTime ModifyDate
  {
    get => this.GetSimpleProperty(nameof (ModifyDate)).GetDateTime();
    set => this.GetSimpleProperty(nameof (ModifyDate)).SetDateTime(value);
  }

  protected override string Name => "http://ns.adobe.com/xap/1.0/";

  public string Nickname
  {
    get => this.GetSimpleProperty(nameof (Nickname)).Value;
    set
    {
      this.GetSimpleProperty(nameof (Nickname)).Value = value != null ? value : throw new ArgumentNullException(nameof (Nickname));
    }
  }

  protected override string Prefix
  {
    get => this.Xmp.XmlData.InnerXml.ToString().Contains("xmlns:xmp") ? "xmp" : "xap";
  }

  public XmpArray Rating => this.GetArray(nameof (Rating), XmpArrayType.Bag);

  public override XmpSchemaType SchemaType => XmpSchemaType.BasicSchema;

  public XmpArray Thumbnails => this.GetArray(nameof (Thumbnails), XmpArrayType.Alt);
}
