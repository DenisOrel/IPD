// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.RightsManagementSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

public class RightsManagementSchema : XmpSchema
{
  private const string c_Certificate = "Certificate";
  private const string c_Marked = "Marked";
  private const string c_name = "http://ns.adobe.com/xap/1.0/rights/";
  private const string c_Owner = "Owner";
  private const string c_prefix = "xmpRights";
  private const string c_UsageTerms = "UsageTerms";
  private const string c_WebStatement = "WebStatement";

  protected internal RightsManagementSchema(XmpMetadata xmp)
    : base(xmp)
  {
  }

  public Uri Certificate
  {
    get => this.GetSimpleProperty(nameof (Certificate)).GetUri();
    set
    {
      if (value == (Uri) null)
        throw new ArgumentNullException(nameof (Certificate));
      this.GetSimpleProperty(nameof (Certificate)).SetUri(value);
    }
  }

  public bool Marked
  {
    get => this.GetSimpleProperty(nameof (Marked)).GetBool();
    set => this.GetSimpleProperty(nameof (Marked)).SetBool(value);
  }

  protected override string Name => "http://ns.adobe.com/xap/1.0/rights/";

  public XmpArray Owner => this.GetArray(nameof (Owner), XmpArrayType.Bag);

  protected override string Prefix => "xmpRights";

  public override XmpSchemaType SchemaType => XmpSchemaType.RightsManagementSchema;

  public XmpLangArray UsageTerms => this.GetLangArray(nameof (UsageTerms));

  public Uri WebStatement
  {
    get => this.GetSimpleProperty(nameof (WebStatement)).GetUri();
    set
    {
      if (value == (Uri) null)
        throw new ArgumentNullException(nameof (WebStatement));
      this.GetSimpleProperty(nameof (WebStatement)).SetUri(value);
    }
  }
}
