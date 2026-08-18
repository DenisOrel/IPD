// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ClassifyObject.TechCardClassifyObjectAttributeParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;

#nullable disable
namespace Intermech.TechCard.Client.Services.ClassifyObject;

/// <summary>Параметры классификации атрибута объекта</summary>
public class TechCardClassifyObjectAttributeParams : TechCardClassifyObjectParams
{
  /// <summary>Constructor</summary>
  /// <param name="attributeId">Классифицируемый атрибут</param>
  /// <param name="classifyObjectItem">Описание классифицируемого объекта</param>
  /// <param name="contextObjectItem">Описание объекта - контекста, относительно которого классифицируем</param>
  public TechCardClassifyObjectAttributeParams(
    int attributeId,
    [NotNull] ObjInfoItem classifyObjectItem,
    [NotNull] ObjInfoItem contextObjectItem)
    : base(classifyObjectItem, contextObjectItem)
  {
    this.AttributeId = attributeId;
  }

  /// <summary>Constructor</summary>
  /// <param name="attributeId">Классифицируемый атрибут</param>
  /// <param name="classifyObjectItem">Параметры классифицируемого объекта</param>
  public TechCardClassifyObjectAttributeParams(
    int attributeId,
    [NotNull] TechCardClassifyObjectParams classifyObjectParams)
    : base(classifyObjectParams.ClassifyObjectItem, classifyObjectParams.ContextObjectItem)
  {
    this.AttributeId = attributeId;
  }

  /// <summary>Идентификатор классифицируемого атрибута</summary>
  public int AttributeId { get; }
}
