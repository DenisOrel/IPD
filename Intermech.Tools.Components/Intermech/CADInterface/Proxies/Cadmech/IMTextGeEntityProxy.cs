// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.IMTextGeEntityProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.Cadmech;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies.Cadmech;

/// <summary>
/// Реализует обертку для COM-объекта ссылки на графический примитив (интерфейс GeEntity_COM).
/// </summary>
public sealed class IMTextGeEntityProxy : CadmechObjectProxy
{
  private GeEntity_COM rawGeEntity;

  /// <summary>Создает прокси-объект.</summary>
  /// <param name="rawObject">COM-объект</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="rawObject" /> не должен быть равен null</exception>
  internal IMTextGeEntityProxy(GeEntity_COM rawObject)
  {
    this.rawGeEntity = rawObject != null ? rawObject : throw new ArgumentNullException(nameof (rawObject));
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект GeEntity_COM. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public GeEntity_COM RawObject
  {
    [DebuggerStepThrough] get => this.rawGeEntity;
  }
}
