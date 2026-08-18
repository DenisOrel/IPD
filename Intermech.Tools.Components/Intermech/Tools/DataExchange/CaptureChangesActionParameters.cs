// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesActionParameters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime;
using Intermech.UI;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Параметры для операции захвата изменений в файлах объекта IPS.
/// </summary>
public sealed class CaptureChangesActionParameters
{
  /// <summary>Возвращает или задает идентификатор версии объекта.</summary>
  public long ObjectId { get; set; }

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
  }
}
