// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.TraceEntry
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Запись в протоколе по конфигурированию составов</summary>
[Serializable]
public sealed class TraceEntry : IAssignable, ICloneable
{
  /// <summary>
  /// Состояние объекта состава, для которого была создана данная запись в протоколе
  /// </summary>
  public PdmConfiguratorResult Flags = PdmConfiguratorResult.Unknown;
  /// <summary>Результат трассировки объекта состава</summary>
  public PdmCompositionTraceResult Trace;
  /// <summary>Строка с информацией</summary>
  public string Message = string.Empty;

  /// <summary>Создать пустой экземпляр записи в протоколе</summary>
  public TraceEntry()
  {
  }

  /// <summary>Создать заполненный экземпляр записи в протоколе</summary>
  /// <param name="flags">Состояние объекта состава, для которого была создана данная запись в протоколе</param>
  /// <param name="message">Информация</param>
  public TraceEntry(PdmConfiguratorResult flags, string message)
    : this(flags, PdmCompositionTraceResult.None, message)
  {
  }

  /// <summary>Создать заполненный экземпляр записи в протоколе</summary>
  /// <param name="flags">Состояние объекта состава, для которого была создана данная запись в протоколе</param>
  /// <param name="trace">Результат трассировки объекта состава</param>
  /// <param name="message">Информация</param>
  public TraceEntry(PdmConfiguratorResult flags, PdmCompositionTraceResult trace, string message)
  {
    this.Flags = flags;
    this.Trace = trace;
    this.Message = message;
  }

  /// <summary>
  /// Создать экземпляр записи в протоколе, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public TraceEntry(object source) => this.Assign(source);

  /// <summary>Является ли элемент пустым</summary>
  public bool Empty
  {
    [DebuggerStepThrough] get
    {
      return (this.Flags == PdmConfiguratorResult.Unknown || this.Flags == PdmConfiguratorResult.False) && this.Trace == PdmCompositionTraceResult.None && string.IsNullOrEmpty(this.Message);
    }
  }

  /// <summary>Есть ли ошибки в результатах трассировки</summary>
  public bool HasErrors
  {
    get
    {
      return (this.Trace & PdmCompositionTraceResult.PdmConfiguratorError) == PdmCompositionTraceResult.PdmConfiguratorError || (this.Trace & PdmCompositionTraceResult.InstanceInPartyError) == PdmCompositionTraceResult.InstanceInPartyError || this.Flags == PdmConfiguratorResult.ApplOptionNotFound || this.Flags == PdmConfiguratorResult.ApplOptionValueNotFound || this.Flags == PdmConfiguratorResult.ConflictOptionNotFound || this.Flags == PdmConfiguratorResult.ConflictOptionValueNotFound || this.Flags == PdmConfiguratorResult.ContextNotFound || this.Flags == PdmConfiguratorResult.Exception || this.Flags == PdmConfiguratorResult.OptionNotFound || this.Flags == PdmConfiguratorResult.OptionValueNotFound;
    }
  }

  /// <summary>Есть ли предупреждения в результатах трассировки</summary>
  public bool HasWarnings
  {
    get
    {
      return (this.Trace & PdmCompositionTraceResult.HasSomeRoutes) == PdmCompositionTraceResult.HasSomeRoutes || (this.Trace & PdmCompositionTraceResult.HasSubstitutes) == PdmCompositionTraceResult.HasSubstitutes || (this.Trace & PdmCompositionTraceResult.WithoutQuantity) == PdmCompositionTraceResult.WithoutQuantity;
    }
  }

  /// <summary>
  /// Есть ли только предупреждения в результатах трассировки
  /// (наличие информации не учитывается)
  /// </summary>
  public bool HasWarningsOnly => this.HasWarnings && !this.HasErrors;

  /// <summary>Есть ли информация в результатах трассировки</summary>
  public bool HasInformation
  {
    get
    {
      return (this.Trace & PdmCompositionTraceResult.HasOneRoute) == PdmCompositionTraceResult.HasOneRoute || (this.Trace & PdmCompositionTraceResult.NotManufacturingLevel) == PdmCompositionTraceResult.NotManufacturingLevel || this.Flags == PdmConfiguratorResult.False || this.Flags == PdmConfiguratorResult.True;
    }
  }

  /// <summary>Есть ли только информация в результатах трассировки</summary>
  public bool HasInformationOnly => this.HasInformation && !this.HasErrors && !this.HasWarnings;

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.Flags = PdmConfiguratorResult.Unknown;
    this.Trace = PdmCompositionTraceResult.None;
    this.Message = string.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is TraceEntry traceEntry))
      return;
    this.Flags = traceEntry.Flags;
    this.Trace = traceEntry.Trace;
    this.Message = traceEntry.Message;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);
}
