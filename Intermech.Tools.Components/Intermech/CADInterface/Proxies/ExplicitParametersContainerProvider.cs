// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ExplicitParametersContainerProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;
using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>Реализует объект-связку для контейнера параметров.</summary>
public sealed class ExplicitParametersContainerProvider : IParametersContainerProvider
{
  private IParametersContainer rawContainer;

  /// <summary>Создает объект.</summary>
  /// <param name="rawContainer">COM-объект контейнера параметров</param>
  /// <exception cref="T:ArgumentNullException">container</exception>
  public ExplicitParametersContainerProvider(IParametersContainer rawContainer)
  {
    this.rawContainer = rawContainer != null ? rawContainer : throw new ArgumentNullException(nameof (rawContainer));
  }

  /// <summary>
  /// Возвращает COM-объект контейнера именованных значений. Метод используется для ленивого получения COM-объекта контейнера при
  /// первом обращении к нему.
  /// </summary>
  /// <returns>COM-объект контейнера</returns>
  public IParametersContainer GetContainer() => this.rawContainer;
}
