// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.ManufactureOrderHolder
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Collections;
using Intermech.Interfaces.Compositions;
using Intermech.Search.Pdm.Analogs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, содержащий ключевую информацию для создания производственного заказа
/// </summary>
[Serializable]
public sealed class ManufactureOrderHolder : 
  IAssignable,
  ICloneable,
  IComparable,
  IComparable<ManufactureOrderHolder>,
  IStoreable
{
  /// <summary>Объект для синхронизации</summary>
  private object syncRoot = new object();
  /// <summary>Идентификатор версии объекта производственного заказа</summary>
  public long ObjectID;
  /// <summary>
  /// Идентификатор типа версии объекта производственного заказа
  /// </summary>
  public volatile int ObjectType = -1;
  /// <summary>Номер производственного заказа</summary>
  public volatile string OrderNumber = string.Empty;
  /// <summary>Guid версии объекта производственного заказа</summary>
  public Guid Guid = Guid.Empty;
  /// <summary>Заголовок производственного заказа</summary>
  public volatile string Caption = string.Empty;
  /// <summary>Создаём экземпляр настроек фильтрации составов</summary>
  public volatile FiltrationSettings FiltrationSettings = new FiltrationSettings(Guid.NewGuid());
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

  /// <summary>Создать пустой экземпляр класса</summary>
  public ManufactureOrderHolder()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ManufactureOrderHolder(object source) => this.Assign(source);

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.Guid.GetHashCode();

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты идентичны</returns>
  public override bool Equals(object obj)
  {
    ManufactureOrderHolder manufactureOrderHolder = obj as ManufactureOrderHolder;
    lock (this.syncRoot)
      return manufactureOrderHolder != null && this.ObjectID == manufactureOrderHolder.ObjectID && this.ObjectType == manufactureOrderHolder.ObjectType && this.Guid.Equals(manufactureOrderHolder.Guid) && this.Caption == manufactureOrderHolder.Caption;
  }

  /// <summary>Получить строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString() => $"[{this.ObjectID}] {this.Caption}";

  /// <summary>
  /// Список контекстов составов. По умолчанию - "Общий контекст" и "Производственный контекст"
  /// </summary>
  public List<long> CompositionContexts
  {
    get
    {
      if (this.FiltrationSettings.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] == null)
        this.FiltrationSettings.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) new List<long>((IEnumerable<long>) new long[2]
        {
          0L,
          3L
        });
      return this.FiltrationSettings.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] as List<long>;
    }
    set
    {
      HybridDictionary tags = this.FiltrationSettings.Tags;
      List<long> collection;
      if (value == null || value.Count <= 0)
        collection = new List<long>((IEnumerable<long>) new long[2]
        {
          0L,
          3L
        });
      else
        collection = value;
      List<long> longList = new List<long>((IEnumerable<long>) collection);
      tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) longList;
    }
  }

  public SeriesDateSettingsHolder SeriesDateSettingsHolder
  {
    get
    {
      return this.FiltrationSettings.Tags[(object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}"] as SeriesDateSettingsHolder;
    }
    set
    {
      if (value != null)
        this.FiltrationSettings.Tags[(object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}"] = (object) value;
      else
        this.FiltrationSettings.Tags.Remove((object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}");
    }
  }

  public AnalogSelectionMode AnalogSelectionMode
  {
    get
    {
      return AnalogsHelper.GetAnalogSelectionModeFromRecordSetParamsTags(this.FiltrationSettings.Tags);
    }
    set
    {
      AnalogsHelper.SetAnalogSelectionModeToRecordSetParamsTags(this.FiltrationSettings.Tags, value);
    }
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    lock (this.syncRoot)
    {
      this.ObjectID = 0L;
      this.ObjectType = -1;
      this.Guid = Guid.Empty;
      this.CompositionContexts = new List<long>((IEnumerable<long>) new long[2]
      {
        0L,
        3L
      });
      this.SeriesDateSettingsHolder = (SeriesDateSettingsHolder) null;
      this.Caption = string.Empty;
      this.Settings.Clear();
      this.RelSettings.Clear();
      this.OrderNumber = string.Empty;
      this.AnalogSelectionMode = AnalogSelectionMode.None;
      string ownerId = this.FiltrationSettings.OwnerID;
      try
      {
        this.FiltrationSettings.Clear();
      }
      finally
      {
        this.FiltrationSettings.OwnerID = ownerId;
      }
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    lock (this.syncRoot)
    {
      switch (source)
      {
        case ManufactureOrderHolder manufactureOrderHolder:
          lock (this.syncRoot)
          {
            this.ObjectID = manufactureOrderHolder.ObjectID;
            this.ObjectType = manufactureOrderHolder.ObjectType;
            this.Guid = manufactureOrderHolder.Guid;
            this.Caption = manufactureOrderHolder.Caption;
            this.CompositionContexts = new List<long>((IEnumerable<long>) manufactureOrderHolder.CompositionContexts);
            if (manufactureOrderHolder.SeriesDateSettingsHolder != null)
              this.SeriesDateSettingsHolder = (SeriesDateSettingsHolder) manufactureOrderHolder.SeriesDateSettingsHolder.Clone();
            this.FiltrationSettings.Assign(manufactureOrderHolder.FiltrationSettings);
            this.Settings = CloneHelper.Clone((object) manufactureOrderHolder.Settings) as SortedDictionary<long, OrderItem>;
            this.RelSettings = CloneHelper.Clone((object) manufactureOrderHolder.RelSettings) as SortedDictionary<long, OrderItem>;
            this.OrderNumber = manufactureOrderHolder.OrderNumber;
            this.AnalogSelectionMode = manufactureOrderHolder.AnalogSelectionMode;
            break;
          }
        case IDBObject dbObject:
          lock (this.syncRoot)
          {
            this.ObjectID = dbObject.ObjectID;
            this.ObjectType = dbObject.ObjectType;
            this.Guid = dbObject.ObjectGUID;
            this.Caption = dbObject.Caption;
            IDBAttribute attributeById = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cadd93c3-306c-11d8-b4e9-00304f19f545"));
            if (attributeById == null)
              break;
            this.OrderNumber = DataSetProcessor.GetStringValue(attributeById.Value, string.Empty);
            break;
          }
        case QuickObjectInfo quickObjectInfo:
          this.ObjectID = quickObjectInfo.ObjectID;
          this.ObjectType = quickObjectInfo.ObjectTypeID;
          this.Guid = quickObjectInfo.VersionGuid;
          this.Caption = quickObjectInfo.Caption;
          break;
      }
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone()
  {
    lock (this.syncRoot)
      return Activator.CreateInstance(this.GetType(), (object) this);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as ManufactureOrderHolder);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(ManufactureOrderHolder other)
  {
    if (other == null)
      return 1;
    lock (this.syncRoot)
      return this.Caption.CompareTo(other.Caption);
  }

  /// <summary>Загрузить информацию из объекта/связи базы данных</summary>
  /// <param name="obj">Источник</param>
  /// <returns>true - информация загружена успешно, false - были ошибки</returns>
  public bool LoadFromObject(IDBAttributable obj)
  {
    if (!(obj is IDBMRPProductionOrder source))
      return false;
    this.Assign((object) source);
    return true;
  }

  /// <summary>Записать информацию в указанный элемент базы данных</summary>
  /// <param name="obj">Элемент-назначение</param>
  /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
  public bool SaveToObject(IDBAttributable obj)
  {
    if (!(obj is IDBMRPProductionOrder dbmrpProductionOrder))
      return false;
    this.BeforeSave();
    lock (this.syncRoot)
      dbmrpProductionOrder.Caption = this.Caption;
    return true;
  }

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

  /// <summary>Перечитать номер производственного заказа</summary>
  /// <param name="session">Ссылка на сессию</param>
  public void ReloadOrderNumber(IUserSession session)
  {
    if (this.ObjectID == 0L)
      return;
    IDBAttribute dbAttribute = session != null ? session.GetObjectActualCopy(this.ObjectID, true).GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cadd93c3-306c-11d8-b4e9-00304f19f545")) : throw new ArgumentNullException(nameof (session));
    if (dbAttribute == null)
      return;
    this.OrderNumber = DataSetProcessor.GetStringValue(dbAttribute.Value, string.Empty);
  }

  /// <summary>Перечитать номер производственного заказа</summary>
  /// <param name="obj">Ссылка на объект производственного заказа</param>
  public void ReloadOrderNumber(IDBObject obj)
  {
    IDBAttribute dbAttribute = obj != null ? obj.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cadd93c3-306c-11d8-b4e9-00304f19f545")) : throw new ArgumentNullException(nameof (obj));
    if (dbAttribute == null)
      return;
    this.OrderNumber = DataSetProcessor.GetStringValue(dbAttribute.Value, string.Empty);
  }

  /// <summary>
  /// Метод вызывается для проверки содержимого на наличие ошибок. В случае ошибки
  /// будет сгенерировано исключение
  /// </summary>
  public void BeforeSave()
  {
  }

  /// <summary>
  /// Объединить текущие настройки с указанным контейнером (у его настроек более высокий приоритет)
  /// </summary>
  /// <param name="container">Контейнер с приоритетными настройками</param>
  public void Merge(MRPOrderItemsSettingsHolder container)
  {
    if (container == null)
      return;
    if (container.Settings != null)
    {
      foreach (KeyValuePair<long, OrderItem> setting in container.Settings)
        this.Settings[setting.Key] = setting.Value;
    }
    if (container.RelSettings != null)
    {
      foreach (KeyValuePair<long, OrderItem> relSetting in container.RelSettings)
        this.RelSettings[relSetting.Key] = relSetting.Value;
    }
    if (container.PathSettings == null)
      return;
    foreach (KeyValuePair<RelationPath, OrderItem> pathSetting in container.PathSettings)
      this.PathSettings[pathSetting.Key] = pathSetting.Value;
  }
}
