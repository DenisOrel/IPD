// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MechanicalDriverService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.DataExchange;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>Базовый класс для сервисов захвата изменений.</summary>
public class MechanicalDriverService
{
  private MechanicalDriver driver;
  private CaptureChangesDriverContext driverContext;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Драйвер захвата изменений</param>
  /// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
  /// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
  public MechanicalDriverService(MechanicalDriver driver, CaptureChangesDriverContext driverContext)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    this.driver = driver;
    this.driverContext = driverContext;
  }

  /// <summary>Возвращает драйвер захвата изменений.</summary>
  public MechanicalDriver Driver
  {
    [DebuggerStepThrough] get => this.driver;
  }

  /// <summary>Возвращает контекст операции захвата изменений.</summary>
  public CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }
}
