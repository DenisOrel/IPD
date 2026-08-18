// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IDependencyFilterBehavior
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Позволяет реализовать необязательный сервисный объект, позволяющий фильтровать файловые зависимости документов, видимые интегратору.
/// </summary>
public interface IDependencyFilterBehavior
{
  /// <summary>
  /// Позволяет реализовать фильтрацию файловых зависимостей документа.
  /// </summary>
  /// <param name="dependencies">Список файловых зависимостей документа</param>
  void FilterDependencies(List<DocumentFileData> dependencies);
}
