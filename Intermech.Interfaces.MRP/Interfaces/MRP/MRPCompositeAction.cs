// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCompositeAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Составное действие для задач MRP, вызывающее несколько произвольных действий
/// </summary>
public class MRPCompositeAction : IAssignable, ICloneable, IMRPAction, IMRPContext
{
  /// <summary>
  /// Контейнер сервисов (контекст, в рамках которого работает действие)
  /// </summary>
  protected IServiceProvider services;
  /// <summary>
  /// Коллекция действий, которые требуется выполнить по очереди
  /// </summary>
  protected IList<IMRPAction> actions;

  /// <summary>
  /// Создать пустой экземпляр класса, добавить в него коллекцию действий
  /// </summary>
  /// <param name="actions">Действия</param>
  public MRPCompositeAction(params IMRPAction[] actions)
  {
    this.actions = (IList<IMRPAction>) actions;
  }

  /// <summary>
  /// Создать пустой экземпляр класса, добавить в него коллекцию действий
  /// </summary>
  /// <param name="actions">Действия</param>
  public MRPCompositeAction(IList<IMRPAction> actions) => this.actions = actions;

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPCompositeAction(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public virtual void Clear()
  {
    this.services = (IServiceProvider) null;
    this.actions = (IList<IMRPAction>) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public virtual void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is MRPCompositeAction mrpCompositeAction))
      return;
    this.services = mrpCompositeAction.Services;
    this.actions = mrpCompositeAction.actions;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>
  /// Контейнер сервисов (контекст, в рамках которого работает действие)
  /// </summary>
  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    set => this.services = value;
  }

  /// <summary>Выполнить действие</summary>
  public virtual void Execute()
  {
    if (this.actions == null)
      return;
    foreach (IMRPAction action in (IEnumerable<IMRPAction>) this.actions)
      action.Execute(this.Services);
  }

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public virtual void Execute(IServiceProvider context)
  {
    if (this.actions == null)
      return;
    foreach (IMRPAction action in (IEnumerable<IMRPAction>) this.actions)
      action.Execute(context ?? this.Services);
  }
}
