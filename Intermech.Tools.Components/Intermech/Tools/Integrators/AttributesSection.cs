// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.AttributesSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует секцию данных, позволяющую обрабатывать атрибуты объектов и связей.
/// </summary>
public sealed class AttributesSection
{
  private ContainerValues embeddedSet;
  private ValueBag dbSet;
  private ValueBag workingSet;

  /// <summary>Создает секцию данных.</summary>
  public AttributesSection()
  {
    this.embeddedSet = new ContainerValues(new ValueBag(), false);
    this.dbSet = new ValueBag();
    this.workingSet = new ValueBag();
  }

  /// <summary>
  /// Возвращает набор параметров, внедренных в файл документа.
  /// </summary>
  public ContainerValues EmbeddedSet
  {
    [DebuggerStepThrough] get => this.embeddedSet;
    [DebuggerStepThrough] set => this.embeddedSet = value;
  }

  /// <summary>
  /// Возвращает набор атрибутов объекта, хранящийся в базе PDM-системы.
  /// </summary>
  public ValueBag DatabaseSet
  {
    [DebuggerStepThrough] get => this.dbSet;
    [DebuggerStepThrough] set => this.dbSet = value;
  }

  /// <summary>
  /// Возвращает рабочий набор атрибутов, используемых для заполнения, корректировки и преобразования значений атрибутов.
  /// В зависимости от решаемой задачи он является копией EmbeddedSet и DatabaseSet, но доступной для записи.
  /// </summary>
  public ValueBag WorkingSet
  {
    [DebuggerStepThrough] get => this.workingSet;
    [DebuggerStepThrough] set => this.workingSet = value;
  }
}
