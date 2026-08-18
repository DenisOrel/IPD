// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIAssociativeDependencies
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует контейнер для информации об ассоциативных зависимостях 3D моделей.
/// </summary>
internal sealed class CIAssociativeDependencies
{
  private readonly PathCollection files;

  /// <summary>Создает объект.</summary>
  /// <param name="capacity">Начальная емкость списка файлов</param>
  public CIAssociativeDependencies(int capacity) => this.files = new PathCollection(capacity);

  /// <summary>
  /// Возвращает список файлов, зависимости от которых для текущей 3D модели являются ассоциативными.
  /// </summary>
  public PathCollection Files => this.files;
}
