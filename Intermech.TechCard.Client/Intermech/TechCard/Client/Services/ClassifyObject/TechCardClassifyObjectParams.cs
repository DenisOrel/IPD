// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ClassifyObject.TechCardClassifyObjectParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.ClassifyObject;

/// <summary>Параметры классификации объекта</summary>
public class TechCardClassifyObjectParams
{
  /// <summary>Параметры классификации объекта</summary>
  /// <param name="classifyObjectItem">Описание классифицируемого объекта</param>
  /// <param name="contextObjectItem">Описание объекта - контекста, относительно которого классифицируем</param>
  public TechCardClassifyObjectParams([NotNull] ObjInfoItem classifyObjectItem, [NotNull] ObjInfoItem contextObjectItem)
  {
    this.ClassifyObjectItem = classifyObjectItem;
    this.ContextObjectItem = contextObjectItem;
  }

  /// <summary>Описание классифицируемого объекта</summary>
  public ObjInfoItem ClassifyObjectItem { get; }

  /// <summary>
  /// Описание объекта - контекста, относительно которого классифицируем
  /// </summary>
  public ObjInfoItem ContextObjectItem { get; }

  /// <summary>Вызов ЭС для классификации объекта</summary>
  public bool UseExpertService { get; set; } = true;

  /// <summary>Дополнительные объекты контекста.</summary>
  /// <remarks>Здесь могут быть родители для ContextObjectItem, или любые объекты
  /// которые нельзя определить однозначно через ЭС</remarks>
  public IEnumerable<ObjInfoItem> ExtraContextObjInfoItems { get; set; }

  /// <summary>
  /// Значения дополнительных атрибутов, участвующих в классификации
  /// </summary>
  public IEnumerable<Intermech.Interfaces.AttributeValues> AttributeValues { get; set; }
}
