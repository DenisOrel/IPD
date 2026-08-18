// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IParametersContainerProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Интерфейс для связи с контейнером именованных значений
/// </summary>
public interface IParametersContainerProvider
{
  /// <summary>
  /// Возвращает COM-объект контейнера именованных значений. Метод используется для ленивого получения COM-объекта контейнера при
  /// первом обращении к нему.
  /// </summary>
  /// <returns>COM-объект контейнера</returns>
  IParametersContainer GetContainer();
}
