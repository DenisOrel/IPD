// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPOrderItemsSettingsHolder
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Контейнер, в который можно добавлять настройки для производственного заказа.
/// Данные настройки будут доступны во время работы мастера по созданию и изменению
/// производственных заказов
/// </summary>
[Serializable]
public sealed class MRPOrderItemsSettingsHolder
{
  /// <summary>Объект для синхронизации</summary>
  private object syncRoot = new object();
  /// <summary>
  /// Словарик с настройками для объектов состава производственного заказа.
  /// Ключ - идентификатор версии объекта.
  /// </summary>
  public volatile SortedDictionary<long, OrderItem> Settings = new SortedDictionary<long, OrderItem>();
  /// <summary>
  /// Словарик с настройками для связей состава производственного заказа.
  /// Ключ - идентификатор связи.
  /// </summary>
  public volatile SortedDictionary<long, OrderItem> RelSettings = new SortedDictionary<long, OrderItem>();
  /// <summary>
  /// Словарик с настройками для узлов состава производственного заказа.
  /// Клич - полный путь к узлу состава.
  /// </summary>
  public volatile SortedDictionary<RelationPath, OrderItem> PathSettings = new SortedDictionary<RelationPath, OrderItem>();

  /// <summary>
  /// Получить настройки для указанной версии объекта, при необходимости создать их в контейнере
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="autoCreate">true - создать настройки, если их нет</param>
  /// <returns>Настройки для указанной версии объекта либо null</returns>
  public OrderItem GetObjectOrderItem(long objectID, bool autoCreate)
  {
    lock (this.syncRoot)
    {
      if (this.Settings.ContainsKey(objectID))
        return this.Settings[objectID];
      if (!autoCreate)
        return (OrderItem) null;
      OrderItem objectOrderItem = new OrderItem();
      this.Settings[objectID] = objectOrderItem;
      return objectOrderItem;
    }
  }

  /// <summary>
  /// Отыскать в списке Settings настройку указанного типа, реализующего интерфейс IOrderItemSetting
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  /// <returns>Настройка указанного типа или null</returns>
  public object GetObjectSetting(long objectID, Type t)
  {
    return this.GetObjectOrderItem(objectID, false)?.GetSetting(t);
  }

  /// <summary>
  /// Установить для версии объекта настройку указанного типа
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="setting">Настройка указанного типа</param>
  public void SetObjectSetting(long objectID, IOrderItemSetting setting)
  {
    if (setting == null)
      return;
    this.GetObjectOrderItem(objectID, true)?.AddOrReplace(setting.GetType(), setting);
  }

  /// <summary>Удалить настройку указанного типа для версии объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="setting">Настройка указанного типа</param>
  public void RemoveObjectSetting(long objectID, IOrderItemSetting setting)
  {
    if (setting == null)
      return;
    this.GetObjectOrderItem(objectID, false)?.RemoveSetting(setting.GetType());
  }

  /// <summary>
  /// Получить настройки для указанной связи, при необходимости создать их в контейнере
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="autoCreate">true - создать настройки, если их нет</param>
  /// <returns>Настройки для указанной связи либо null</returns>
  public OrderItem GetRelationOrderItem(long prjLinkID, bool autoCreate)
  {
    lock (this.syncRoot)
    {
      if (this.RelSettings.ContainsKey(prjLinkID))
        return this.RelSettings[prjLinkID];
      if (!autoCreate)
        return (OrderItem) null;
      OrderItem relationOrderItem = new OrderItem();
      this.RelSettings[prjLinkID] = relationOrderItem;
      return relationOrderItem;
    }
  }

  /// <summary>
  /// Отыскать в списке RelSettings настройку указанного типа, реализующего интерфейс IOrderItemSetting
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  /// <returns>Настройка указанного типа или null</returns>
  public object GetRelationSetting(long prjLinkID, Type t)
  {
    return this.GetRelationOrderItem(prjLinkID, false)?.GetSetting(t);
  }

  /// <summary>Установить для связи настройку указанного типа</summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="setting">Настройка указанного типа</param>
  public void SetRelationSetting(long prjLinkID, IOrderItemSetting setting)
  {
    if (setting == null)
      return;
    this.GetRelationOrderItem(prjLinkID, true)?.AddOrReplace(setting.GetType(), setting);
  }

  /// <summary>Удалить настройку указанного типа для связи</summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="setting">Настройка указанного типа</param>
  public void RemoveRelationSetting(long prjLinkID, IOrderItemSetting setting)
  {
    if (setting == null)
      return;
    this.GetRelationOrderItem(prjLinkID, false)?.RemoveSetting(setting.GetType());
  }

  /// <summary>
  /// Получить настройки для указанного узла состава, при необходимости создать их в контейнере
  /// </summary>
  /// <param name="path">Полный путь к указанному узлу состава</param>
  /// <param name="autoCreate">true - создать настройки, если их нет</param>
  /// <returns>Настройки для указанного узла состава либо null</returns>
  public OrderItem GetPathOrderItem(RelationPath path, bool autoCreate)
  {
    if (path == null || path.Empty)
      return (OrderItem) null;
    path = path.SignedClone(false);
    lock (this.syncRoot)
    {
      if (this.PathSettings.ContainsKey(path))
        return this.PathSettings[path];
      if (!autoCreate)
        return (OrderItem) null;
      OrderItem pathOrderItem = new OrderItem();
      this.PathSettings[path] = pathOrderItem;
      return pathOrderItem;
    }
  }

  /// <summary>
  /// Отыскать в списке PathSettings настройку указанного типа, реализующего интерфейс IOrderItemSetting
  /// </summary>
  /// <param name="path">Полный путь к указанному узлу состава</param>
  /// <param name="t">Тип настройки, реализующий интерфейс IOrderItemSetting</param>
  /// <returns>Настройка указанного типа или null</returns>
  public object GetPathSetting(RelationPath path, Type t)
  {
    if (path == null || path.Empty)
      return (object) null;
    path = path.SignedClone(false);
    return this.GetPathOrderItem(path, false)?.GetSetting(t);
  }

  /// <summary>Установить для узла состава настройку указанного типа</summary>
  /// <param name="path">Полный путь к указанному узлу состава</param>
  /// <param name="setting">Настройка указанного типа</param>
  public void SetPathSetting(RelationPath path, IOrderItemSetting setting)
  {
    if (path == null || path.Empty || setting == null)
      return;
    path = path.SignedClone(false);
    this.GetPathOrderItem(path, true)?.AddOrReplace(setting.GetType(), setting);
  }

  /// <summary>Удалить настройку указанного типа для узла состава</summary>
  /// <param name="path">Полный путь к указанному узлу состава</param>
  /// <param name="setting">Настройка указанного типа</param>
  public void RemovePathSetting(RelationPath path, IOrderItemSetting setting)
  {
    if (path == null || path.Empty || setting == null)
      return;
    path = path.SignedClone(false);
    this.GetPathOrderItem(path, false)?.RemoveSetting(setting.GetType());
  }
}
