// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.BaseOrderItemSetting
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Базовый класс, позволяющий добавлять какие-то настройки для элемента,
/// входящего в состав производственного заказа
/// </summary>
[Serializable]
public class BaseOrderItemSetting : IOrderItemSetting, IAssignable, ICloneable
{
  /// <summary>Объект для синхронизации</summary>
  protected object syncRoot = new object();

  /// <summary>Создать пустой экземпляр класса</summary>
  public BaseOrderItemSetting()
  {
  }

  /// <summary>
  /// Создать экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public BaseOrderItemSetting(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public virtual void Clear()
  {
    lock (this.syncRoot)
      ;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public virtual void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Объект для синхронизации</summary>
  public virtual object SyncRoot
  {
    [DebuggerStepThrough] get => this.syncRoot;
  }

  /// <summary>Редактируемые данные</summary>
  public virtual object Data
  {
    [DebuggerStepThrough] get => (object) null;
  }
}
