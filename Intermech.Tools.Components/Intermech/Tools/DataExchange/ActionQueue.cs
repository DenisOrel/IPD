// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ActionQueue
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Реализует очередь операций, допускающую только пополнение и просмотр.
/// </summary>
public class ActionQueue : IEnumerable<IAction>, IEnumerable
{
  private readonly LinkedList<IAction> actions;
  private bool readOnly;

  /// <summary>Создает объект.</summary>
  public ActionQueue() => this.actions = new LinkedList<IAction>();

  /// <summary>Добавляет операцию в очередь.</summary>
  /// <param name="action">Объект операции</param>
  /// <exception cref="T:System.ArgumentNullException">Объект операции не задан</exception>
  /// <exception cref="T:System.InvalidOperationException">Изменение очереди операций запрещено</exception>
  public void Add(IAction action)
  {
    if (action == null)
      throw new ArgumentNullException();
    this.CheckReadOnly();
    this.actions.AddLast(action);
  }

  /// <summary>Проверяет наличие операции в очереди.</summary>
  /// <param name="match">Предикат для определения операции</param>
  /// <returns>Результат проверки</returns>
  public bool Exist(Predicate<IAction> match)
  {
    if (match == null)
      throw new ArgumentNullException(nameof (match));
    if (this.actions.Count != 0)
    {
      foreach (IAction action in this.actions)
      {
        if (match(action))
          return true;
      }
    }
    return false;
  }

  private void CheckReadOnly()
  {
    if (this.ReadOnly)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_414"));
  }

  /// <summary>Возвращает перечислитель операций в очереди.</summary>
  /// <returns>Объект перечислителя</returns>
  public IEnumerator<IAction> GetEnumerator()
  {
    return (IEnumerator<IAction>) this.actions.GetEnumerator();
  }

  /// <summary>Возвращает перечислитель операций в очереди.</summary>
  /// <returns>Объект перечислителя</returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.actions.GetEnumerator();

  /// <summary>Возвращает количество операций в очереди.</summary>
  public int Count => this.actions.Count;

  /// <summary>Разрешает или запрещает изменение очереди операций.</summary>
  protected bool ReadOnly
  {
    get => this.readOnly;
    set => this.readOnly = value;
  }
}
