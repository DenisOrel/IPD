// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDrawings.ConstantModelDrawingsServiceSettings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface.ModelDrawings;

/// <summary>
/// Константная реализация провайдера, который предоставляет доступ к настройкам, необходимым для работы сервиса IModelDrawingsService.
/// Используется в отладочных и других специальных целях.
/// </summary>
/// <remarks>Реализация является является immutable и thread safe.</remarks>
public sealed class ConstantModelDrawingsServiceSettings : IModelDrawingsServiceSettings
{
  private readonly ICollection<string> drawingSuffixes;

  /// <summary>Создает объект.</summary>
  /// <param name="drawingSuffixes">Коллекция суффиксов для имен файлов чертежей</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="drawingSuffixes" /> содержит null</exception>
  public ConstantModelDrawingsServiceSettings(ICollection<string> drawingSuffixes)
  {
    if (drawingSuffixes == null)
      throw new ArgumentNullException(nameof (drawingSuffixes));
    this.drawingSuffixes = drawingSuffixes.IsReadOnly ? drawingSuffixes : throw new ArgumentException("The parameter must be a read-only collection.", nameof (drawingSuffixes));
  }

  /// <summary>
  /// Возвращает коллекцию суффиксов, по которым можно опознать файлы чертежей.
  /// </summary>
  /// <returns>Коллекция суффиксов, по которым можно опознать файлы чертежей</returns>
  public ICollection<string> GetDrawingSuffixes() => this.drawingSuffixes;
}
