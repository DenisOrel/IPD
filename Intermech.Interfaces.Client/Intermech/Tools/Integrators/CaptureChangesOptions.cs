// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CaptureChangesOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;
using Intermech.UI;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>Опции для операции захвата изменений в объекте.</summary>
public sealed class CaptureChangesOptions
{
  /// <summary>Создает объект.</summary>
  /// <param name="context">Режим выполнения сохранения изменений</param>
  public CaptureChangesOptions(SaveChangesMode mode) => this.Mode = mode;

  /// <summary>Возвращает режим выполнения сохранения изменений.</summary>
  public SaveChangesMode Mode { get; private set; }

  /// <summary>
  /// Возвращает или задает индикатор хода выполнения операции.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IPercentageProgressSink ProgressSink { get; set; }
}
