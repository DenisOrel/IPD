// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.OrderItem
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, содержащий коллекцию настроек для объекта, входящего в состав производственного заказа
/// </summary>
[Serializable]
public class OrderItem : 
  CompositionObject,
  IAssignable,
  ICloneable,
  IComparable<OrderItem>,
  IOrderItem
{
  /// <summary>Объект для синхронизации</summary>
  protected object syncRoot = new object();
  /// <summary>
  /// Список настроек, связанных с указанным объектом состава производственного заказа
  /// </summary>
  protected List<IOrderItemSetting> settings = new List<IOrderItemSetting>(4);

  /// <summary>Создать пустой экземпляр класса</summary>
  public OrderItem()
  {
  }

  /// <summary>
  /// Создать экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public OrderItem(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    lock (this.syncRoot)
    {
      base.Clear();
      this.settings = (List<IOrderItemSetting>) null;
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    base.Assign(source);
    if (!(source is OrderItem orderItem))
      return;
    lock (this.syncRoot)
    {
      if (orderItem.Settings == null || orderItem.Settings.Count <= 0)
        return;
      this.settings = new List<IOrderItemSetting>(orderItem.Settings.Count);
      for (int index = 0; index < orderItem.Settings.Count; ++index)
        this.Settings.Add(orderItem.Settings[index].Clone() as IOrderItemSetting);
    }
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(OrderItem other)
  {
    if (other == null)
      return 1;
    lock (this.syncRoot)
      return this.F_PROJ_ID.CompareTo(other.F_PROJ_ID);
  }

  /// <summary>Объект для синхронизации</summary>
  public virtual object SyncRoot
  {
    [DebuggerStepThrough] get => this.syncRoot;
  }

  /// <summary>
  /// Список настроек, связанных с указанным объектом состава производственного заказа
  /// </summary>
  public virtual List<IOrderItemSetting> Settings
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this.settings;
    }
    set
    {
      lock (this.syncRoot)
        this.settings = value;
    }
  }

  /// <summary>Какие-то дополнительные свойства</summary>
  public virtual object Tag
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this.Tag;
    }
    set
    {
      lock (this.syncRoot)
        this.Tag = value;
    }
  }

  /// <summary>
  /// Отыскать в списке Settings настройку указанного типа, реализующего интерфейс IOrderItemSetting
  /// </summary>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  /// <returns>Настройка указанного типа или null</returns>
  public virtual object GetSetting(Type t)
  {
    if (t == (Type) null || this.settings == null || this.settings.Count == 0)
      return (object) null;
    lock (this.syncRoot)
      return (object) this.settings.Find((Predicate<IOrderItemSetting>) (item => t.IsInstanceOfType((object) item)));
  }

  /// <summary>Удалить из списка настройку указанного типа</summary>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  public virtual void RemoveSetting(Type t)
  {
    if (t == (Type) null || this.settings == null || this.settings.Count == 0)
      return;
    lock (this.syncRoot)
      this.settings.RemoveAll((Predicate<IOrderItemSetting>) (item => t.IsInstanceOfType((object) item)));
  }

  /// <summary>
  /// Добавить или заменить в коллекции настройку указанного типа
  /// </summary>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  /// <param name="setting">Экземпляр настройки</param>
  public virtual void AddOrReplace(Type t, IOrderItemSetting setting)
  {
    if (t == (Type) null || setting == null || !t.IsInstanceOfType((object) setting))
      return;
    this.RemoveSetting(t);
    lock (this.syncRoot)
      this.settings.Add(setting);
  }

  [SpecialName]
  long IOrderItem.get_F_PROJ_ID() => this.F_PROJ_ID;

  [SpecialName]
  long IOrderItem.get_F_PRJLINK_ID() => this.F_PRJLINK_ID;

  [SpecialName]
  int IOrderItem.get_F_RELATION_TYPE() => this.F_RELATION_TYPE;
}
