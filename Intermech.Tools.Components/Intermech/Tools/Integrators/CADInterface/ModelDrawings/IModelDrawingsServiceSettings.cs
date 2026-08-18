// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDrawings.IModelDrawingsServiceSettings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface.ModelDrawings;

/// <summary>
/// Предоставляет доступ к настройкам интегратора, необходимым для работы сервиса IModelDrawingsService.
/// Реализация не обязана быть thread safe.
/// </summary>
public interface IModelDrawingsServiceSettings
{
  /// <summary>
  /// Возвращает коллекцию суффиксов, по которым можно опознать файлы чертежей.
  /// </summary>
  /// <returns>Коллекция суффиксов, по которым можно опознать файлы чертежей</returns>
  ICollection<string> GetDrawingSuffixes();
}
