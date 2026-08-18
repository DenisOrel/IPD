// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.ReportItemInfo
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Document.Client.Reports;

internal class ReportItemInfo
{
  public long ObjectID;
  public long PrjLinkID;
  public int AttributeID;
  public FieldTypes AttributeType;
  public AttributeSourceTypes AttributeSource;

  public ReportItemInfo(
    long objectID,
    long prjLinkID,
    int attributeID,
    FieldTypes attributeType,
    AttributeSourceTypes attributeSource)
  {
    this.ObjectID = objectID;
    this.PrjLinkID = prjLinkID;
    this.AttributeID = attributeID;
    this.AttributeType = attributeType;
    this.AttributeSource = attributeSource;
  }
}
