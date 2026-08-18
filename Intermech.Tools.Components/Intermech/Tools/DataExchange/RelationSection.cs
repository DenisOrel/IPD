// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.RelationSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Содержит основные сведения о связи между объектами - родительский объект и глобальный идентификатор связи.
/// Такая схема идентификации связей используется из-за того, что глобальный идентификатор связи не меняется
/// при связии родительского объекта на изменение.
/// </summary>
[DebuggerDisplay("RelationSection: [NewRelation: {NewRelation}]{RelationGuid}")]
public sealed class RelationSection
{
  /// <summary>
  /// Возвращает или задает признак того, что это новая связь, которая будет добавлена в базу IPS в
  /// процессе захвата изменений
  /// </summary>
  private bool newRelation;
  /// <summary>
  /// Возвращает или задает рабочий элемент для родительского объекта. Описываемая связь выходит из этого
  /// объекта
  /// </summary>
  private SectionEntity projectItem;
  /// <summary>Возвращает или задает глобальный идентификатор связи</summary>
  private Guid relationGuid;

  /// <summary>
  /// Возвращает или задает признак того, что это новая связь, которая будет добавлена в базу IPS в
  /// процессе захвата изменений.
  /// </summary>
  public bool NewRelation
  {
    [DebuggerStepThrough] get => this.newRelation;
    [DebuggerStepThrough] set => this.newRelation = value;
  }

  /// <summary>
  /// Возвращает или задает рабочий элемент для родительского объекта. Описываемая связь выходит из этого
  /// объекта.
  /// </summary>
  public SectionEntity ProjectItem
  {
    [DebuggerStepThrough] get => this.projectItem;
    [DebuggerStepThrough] set => this.projectItem = value;
  }

  /// <summary>Возвращает или задает глобальный идентификатор связи.</summary>
  public Guid RelationGuid
  {
    [DebuggerStepThrough] get => this.relationGuid;
    [DebuggerStepThrough] set => this.relationGuid = value;
  }
}
