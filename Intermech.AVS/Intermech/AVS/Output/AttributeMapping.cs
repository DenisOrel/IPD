// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.AttributeMapping
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable
namespace Intermech.AVS.Output;

public class AttributeMapping : OutputMappingBase
{
  private AttributeInfo _attrInfo;

  public AttributeMapping()
  {
  }

  public AttributeMapping(AttributeInfo attrInfo) => this._attrInfo = attrInfo;

  public bool IsVirtualAttribute
  {
    get
    {
      AttributeInfo attrInfo = this._attrInfo;
      return attrInfo != null && attrInfo.IsVirtualAttribute;
    }
  }

  public bool IsDocumentAttribute
  {
    get
    {
      AttributeInfo attrInfo = this._attrInfo;
      return attrInfo != null && attrInfo.IsDocField;
    }
  }

  public bool IsDBRelationAttribute
  {
    get
    {
      AttributeInfo attrInfo = this._attrInfo;
      return attrInfo != null && attrInfo.IsRelationAttribute;
    }
  }

  public bool IsDBObjectAttribute
  {
    get
    {
      AttributeInfo attrInfo = this._attrInfo;
      return attrInfo != null && attrInfo.IsObjectAttribute;
    }
  }

  public string AttributeType
  {
    get
    {
      AttributeInfo attrInfo = this._attrInfo;
      return (attrInfo != null ? attrInfo.AttrSrc : FieldSource.Relation).ToString();
    }
  }

  public string AttributeGuid
  {
    get
    {
      AttributeInfo attrInfo = this._attrInfo;
      return (attrInfo != null ? attrInfo.AttributeGuid : Guid.Empty).ToString();
    }
  }

  public int AttributeID
  {
    get
    {
      AttributeInfo attrInfo = this._attrInfo;
      return attrInfo == null ? -1 : attrInfo.AttributeId;
    }
  }

  public string AttributeName => this._attrInfo?.Name ?? string.Empty;

  public AttributeInfo AttributeInfo
  {
    get => this._attrInfo;
    private set => this._attrInfo = value ?? this._attrInfo;
  }

  public string GetAttributeGuid()
  {
    return this.IsDocumentAttribute ? this._attrInfo.Name : this.AttributeGuid;
  }

  public override string ToString() => $"[{this.AttributeName}]";

  internal override XElement ToXML()
  {
    return new XElement((XName) "Mapping", new object[4]
    {
      (object) new XAttribute((XName) "AttrGuid", (object) this.AttributeGuid),
      (object) new XAttribute((XName) "AttrType", (object) this.AttributeType),
      (object) new XAttribute((XName) "ID", (object) this.AttributeID.ToString()),
      (object) new XAttribute((XName) "Order", (object) this.Order.ToString())
    });
  }

  public override bool Equals(object obj)
  {
    switch (obj)
    {
      case AttributeMapping attributeMapping:
        return this.Equals((object) attributeMapping._attrInfo);
      case AttributeInfo attributeInfo:
        return attributeInfo.Equals((object) this._attrInfo);
      default:
        return false;
    }
  }

  public override int GetHashCode()
  {
    return 1253980453 + EqualityComparer<AttributeInfo>.Default.GetHashCode(this._attrInfo);
  }
}
