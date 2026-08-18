// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.BaseFieldContents
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

public abstract class BaseFieldContents : DocumentConfigElement, IFieldContents
{
  private const string ParamType = "Type";
  private const string ParamTypeId = "TypeId";

  protected abstract FieldContentsType GetContentsType();

  public FieldContentsType ContentsType => this.GetContentsType();

  public abstract void CollectAttributeSettings(ICollection<AttributeSettings> attributeSettings);

  public static FieldContentsType LoadContentsType([NotNull] XElement element)
  {
    XAttribute xattribute = element.Attribute(XName.Get("TypeId"));
    if (xattribute == null)
      return Convert.ToString(element.Attribute(XName.Get("Type"))?.Value).ToEnum<FieldContentsType>();
    int result;
    int.TryParse(xattribute.Value, out result);
    return (FieldContentsType) result;
  }
}
