// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.EmbedAttributesActionParameters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime;
using Intermech.UI;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Параметры операции по записи в файловую копию объекта значений атрибутов объекта.
/// </summary>
public sealed class EmbedAttributesActionParameters
{
  /// <summary>Возвращает или задает идентификатор версии объекта.</summary>
  public long ObjectId { get; set; }

  /// <summary>
  /// Возвращает или задает записываемые значения атрибутов объекта.
  /// </summary>
  public IList<Intermech.Interfaces.AttributeValues> AttributeValues { get; set; }

  /// <summary>
  /// Возвращает или задает индикатор хода выполнения операции.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IPercentageProgressSink ProgressSink { get; set; }

  /// <summary>Проверяет корректность заполнения свойств объекта.</summary>
  /// <exception cref="T:InvalidOperationException">Значения свойств объекта заполнены некорректно</exception>
  public void ValidateProperties()
  {
    if (this.ObjectId == 0L)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ObjectId");
    if (this.AttributeValues == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "AttributeValues");
  }
}
