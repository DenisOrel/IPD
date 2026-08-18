// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.EmbedAttributesActionOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.UI;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Опции операции по записи в файловую копию объекта значений атрибутов объекта.
/// </summary>
public sealed class EmbedAttributesActionOptions
{
  /// <summary>
  /// Возвращает или задает индикатор хода выполнения операции.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IPercentageProgressSink ProgressSink { get; set; }
}
