
// Type: Intermech.Client.Core.PdmConfiguratorHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core;

/// <summary>
/// Вспомогательный статический класс, используемый конфигуратором составов IPS
/// </summary>
public static class PdmConfiguratorHelper
{
  /// <summary>Информация о текущем пользователе</summary>
  private static ICurrentUserAndRole _userAndRole;

  /// <summary>Информация о текущем пользователе</summary>
  private static ICurrentUserAndRole UserAndRole
  {
    get
    {
      if (PdmConfiguratorHelper._userAndRole == null)
        PdmConfiguratorHelper._userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      return PdmConfiguratorHelper._userAndRole;
    }
  }

  /// <summary>
  /// Уникальный идентификатор клиентского подключения к серверу приложений.
  /// Идентификатор присваивается сервером приложений при создании первой сессии клиента.
  /// Все сессии одного клиента будут иметь один и тот же идентификатор клиентского подключения;
  /// два разных клиента, вошедших под одним и тем же пользователем IPS, будут иметь разные идентификаторы.
  /// </summary>
  private static long ClientConnectionID
  {
    get
    {
      return PdmConfiguratorHelper.UserAndRole == null ? 0L : PdmConfiguratorHelper.UserAndRole.ClientConnectionID;
    }
  }

  /// <summary>Уникальный идентификатор текущего пользователя</summary>
  private static long UserID
  {
    get
    {
      return PdmConfiguratorHelper.UserAndRole == null ? 0L : PdmConfiguratorHelper.UserAndRole.UserID;
    }
  }

  /// <summary>Создать ключ контекста конфигуратора составов</summary>
  /// <param name="topObjectID">Корневой объект конфигурируемого состава</param>
  /// <param name="topObjectType">Тип корневого объекта конфигурируемого состава</param>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Ключ контекста конфигуратора составов</returns>
  public static RelationPair CreateKey(
    long topObjectID,
    int topObjectType,
    long relID,
    int relTypeID,
    long objID,
    int objTypeID)
  {
    RelationPair relationPair = (RelationPair) null;
    if (relID != 0L && MetaDataHelper.IsPdmPartiallyConfigurableRelationType(relTypeID))
      relationPair = new RelationPair(PdmConfiguratorHelper.ClientConnectionID, topObjectID, topObjectType, relID, PdmConfiguratorHelper.UserID, objID, relTypeID, objTypeID);
    else if (objID != 0L)
      relationPair = new RelationPair(PdmConfiguratorHelper.ClientConnectionID, topObjectID, topObjectType, 0L, PdmConfiguratorHelper.UserID, objID, relTypeID, objTypeID);
    return relationPair ?? new RelationPair();
  }

  /// <summary>
  /// Создать ключ контекста конфигуратора составов для указанного выделенного узла Навигатора
  /// (по двум реализуемым интерфейсам)
  /// </summary>
  /// <param name="topObjectID">Корневой объект конфигурируемого состава</param>
  /// <param name="topObjectType">Тип корневого объекта конфигурируемого состава</param>
  /// <param name="selectedRel">Описание выделенной связи</param>
  /// <param name="selectedObj">Описание выделенного объекта</param>
  /// <returns>Ключ контекста конфигуратора составов</returns>
  public static RelationPair CreateKey(
    long topObjectID,
    int topObjectType,
    IDBRelationID selectedRel,
    IDBTypedObjectID selectedObj)
  {
    RelationPair relationPair = (RelationPair) null;
    if (selectedRel != null && MetaDataHelper.IsPdmPartiallyConfigurableRelationType(selectedRel.RelationType))
      relationPair = new RelationPair(PdmConfiguratorHelper.ClientConnectionID, topObjectID, topObjectType, selectedRel.Value, PdmConfiguratorHelper.UserID, selectedObj != null ? selectedObj.ObjectID : 0L, selectedRel.RelationType, selectedObj != null ? selectedObj.ObjectType : -1);
    else if (selectedObj != null && selectedObj.ObjectID != 0L)
      relationPair = new RelationPair(PdmConfiguratorHelper.ClientConnectionID, topObjectID, topObjectType, 0L, PdmConfiguratorHelper.UserID, selectedObj.ObjectID, selectedRel != null ? selectedRel.RelationType : -1, selectedObj.ObjectType);
    return relationPair ?? new RelationPair();
  }

  /// <summary>
  /// Попробовать сформировать ключ контекста конфигуратора составов для указанного родительского узла Навигатора
  /// </summary>
  /// <param name="topObjectID">Корневой объект конфигурируемого состава</param>
  /// <param name="topObjectType">Тип корневого объекта конфигурируемого состава</param>
  /// <param name="items">Коллекция выделенных элементов пространства навигации</param>
  /// <param name="parentIndex">Индекс родительского узла</param>
  /// <returns>Ключ родительского контекста конфигуратора или null</returns>
  public static RelationPair CreateParentKey(
    long topObjectID,
    int topObjectType,
    ISelectedItems items,
    int parentIndex)
  {
    IDBRelationID parentData1 = items.GetParentData(parentIndex, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID parentData2 = items.GetParentData(parentIndex, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    RelationPair key = PdmConfiguratorHelper.CreateKey(topObjectID, topObjectType, parentData1, parentData2);
    return key.Empty ? (RelationPair) null : key;
  }

  /// <summary>
  /// Определить корневой объект конфигурируемого состава на основании указанной информации
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <returns>Описание версии корневого объекта конфигурируемого состава или null</returns>
  public static IDBTypedObjectID GetTopObjectID(IServiceProvider services, ISelectedItems items)
  {
    if (services == null)
      return (IDBTypedObjectID) null;
    if (services.GetService(typeof (ChildrenView)) is ChildrenView service1)
      return service1.GetCompositionTopObject();
    if (services.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service2 && service2.FocusedNode != null)
      return service2.GetTopCompositionObject(service2.FocusedNode);
    if (items == null || items.Count <= 0)
      return (IDBTypedObjectID) null;
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1))
      return (IDBTypedObjectID) null;
    return !(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData2) || itemData2.Value == 0L || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(itemData2.RelationType) || !(items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData) ? itemData1 : parentData;
  }
}
