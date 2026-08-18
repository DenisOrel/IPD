// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADSystemComponentProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Удобный базовый класс для создания proxy-объектов для основных COM-объектов, получаемых от объекта CAD-системы.
/// </summary>
public abstract class CADSystemComponentProxy : CADInterfaceObjectProxy
{
  private CADSystemProxy cadSystem;

  /// <summary>Создает объект.</summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <exception cref="T:ArgumentNullException">cadSystem</exception>
  protected CADSystemComponentProxy(CADSystemProxy cadSystem)
  {
    this.cadSystem = cadSystem != null ? cadSystem : throw new ArgumentNullException(nameof (cadSystem));
  }

  /// <summary>Возвращает объект CAD-системы.</summary>
  public CADSystemProxy CADSystem
  {
    [DebuggerStepThrough] get => this.cadSystem;
  }

  /// <summary>
  /// Возвращает кэш объектов CAD-интерфейса и результатов вызовов "тяжелых" методов CAD-интерфейса. Он используется всеми компонентами <see cref="T:CADSystemProxy" />.
  /// Значение свойства может быть не задано, если кэширование не требуется.
  /// </summary>
  public CADSystemCache Cache
  {
    [DebuggerStepThrough] get => this.CADSystem.Cache;
  }

  public TValue EvaluateCached<TValue>(string valueName, Func<TValue> valueFunction)
  {
    return this.CADSystem.EvaluateCached<TValue>((object) this, valueName, valueFunction);
  }
}
