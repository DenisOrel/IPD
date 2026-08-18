// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IFileDependenciesHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Интерфейс обработчика файловых зависимостей документа.
/// </summary>
public interface IFileDependenciesHandler
{
  /// <summary>
  /// Получает файловые зависимости документа, выполняет их анализ и обработку.
  /// </summary>
  /// <param name="docItem">Сущность анализируемого документа</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="documentItem" /> не должен быть равен null</exception>
  void Run(SectionEntity documentItem);
}
