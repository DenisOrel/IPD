
// Type: Intermech.Client.Core.FavoritesCommandProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Провайдер команд для работы с Избранным</summary>
internal class FavoritesCommandProvider : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    items.GetParentPath(0);
    if (items.GetItemID(0).CategoryID == Intermech.Navigator.Consts.CategoryFavoritesNode)
    {
      mergedCommands.Add("AddTypeToFavorites", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.AddObjectTypeToFavorites)));
      mergedCommands.Add("Paste", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.PasteObjectsToFavorites)));
    }
    if (this.IsFavoritesNodeAParent(items))
    {
      switch (items.GetItemID(0).CategoryID)
      {
        case 1:
          mergedCommands.Add("RemoveFromFavoritesNavigator", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.RemoveObjectsFromFavorites)));
          break;
        case 4:
          mergedCommands.Add("RemoveFromFavoritesNavigator", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.RemoveTypesFromFavorites)));
          break;
      }
    }
    else if (items.Count > 0)
    {
      switch (items.GetItemID(0).CategoryID)
      {
        case 1:
          mergedCommands.Add("AddToFavoritesNavigator", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.AddObjectsToFavorites)));
          break;
        case 4:
          mergedCommands.Add("AddToFavoritesNavigator", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.AddTypesToFavorites)));
          break;
      }
    }
    return mergedCommands;
  }

  /// <summary>Вставить объекты в избранное</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  private void PasteObjectsToFavorites(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalinfo)
  {
    object dataObject = (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).GetDataObject();
    if (dataObject == null || !(dataObject is IDBObjectTypedIDCollection typedIdCollection))
      return;
    IDBTypedObjectID[] typedObjects = typedIdCollection.GetTypedObjects();
    long[] objIds = new long[typedObjects.Length];
    for (int index = 0; index < typedObjects.Length; ++index)
      objIds[index] = typedObjects[index].ObjectID;
    FavoritesCommandProvider.AddObjectsToFavorites(objIds);
  }

  /// <summary>
  /// Добавить тип объектов в Избранное через форму выбора типа объектов
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  private void AddObjectTypeToFavorites(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Все объекты", typeof (ObjectTypeFolder), false);
    selectorForm.Text = "Выберите тип объекта";
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    this.AddTypesToFavorites(new List<int>()
    {
      (int) selectorForm.IDList[0]
    });
  }

  /// <summary>Удалить версии объектов из Избранного</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  private void RemoveObjectsFromFavorites(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items.Count <= 0)
      return;
    long[] objectIDs = new long[items.Count];
    for (int index = 0; index < items.Count; ++index)
    {
      long num = (items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID).Value;
      objectIDs[index] = num;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IUserFavouritesService)) is IUserFavouritesService customService))
        throw new KernelException("Не найден сервис для работы с Избранным.");
      customService.ExcludeObjects(sessionKeeper.Session.SessionGUID, objectIDs);
      Holder.NotificationService.FireEvent((object) null, new NotificationEventArgs("FavoritesChanged"));
    }
  }

  /// <summary>Удалить типы из избранного</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  private void RemoveTypesFromFavorites(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items.Count <= 0)
      return;
    List<int> objectTypeIDs = new List<int>();
    for (int index = 0; index < items.Count; ++index)
    {
      int num = (items.GetItemData(index, typeof (IDBObjectTypeID)) as IDBObjectTypeID).Value;
      objectTypeIDs.Add(num);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IUserFavouritesService)) is IUserFavouritesService customService))
        throw new KernelException("Не найден сервис для работы с Избранным.");
      foreach (int objectTypeID in objectTypeIDs)
        customService.DeleteObjectType(sessionKeeper.Session.SessionGUID, objectTypeID);
      Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectTypesEventArgs("FavoritesRemoveType", (IList<int>) objectTypeIDs));
    }
  }

  /// <summary>Добавить типы в Избранное</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  private void AddTypesToFavorites(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items.Count <= 0)
      return;
    List<int> objIds = new List<int>();
    for (int index = 0; index < items.Count; ++index)
    {
      int num = (items.GetItemData(index, typeof (IDBObjectTypeID)) as IDBObjectTypeID).Value;
      objIds.Add(num);
    }
    this.AddTypesToFavorites(objIds);
  }

  /// <summary>Добавить список объектов в избранное</summary>
  /// <param name="objIds">Список идентификаторов типов</param>
  private void AddTypesToFavorites(List<int> objIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IUserFavouritesService)) is IUserFavouritesService customService))
        throw new KernelException("Не найден сервис для работы с Избранным.");
      foreach (int objId in objIds)
        customService.AddObjectType(sessionKeeper.Session.SessionGUID, objId);
      if (ServicesManager.GetService(typeof (IFavoritesWindow)) is IFavoritesWindow service)
        service.Update();
      Holder.NotificationService.FireEvent((object) null, new NotificationEventArgs("FavoritesChanged"));
    }
  }

  /// <summary>Есть ли узел Избранное в родительских узлах</summary>
  /// <param name="items"></param>
  /// <returns>true - если есть</returns>
  private bool IsFavoritesNodeAParent(ISelectedItems items)
  {
    NodeIDPath parentPath = items.GetParentPath(0);
    return parentPath != null && parentPath.Length - 1 >= 0 && parentPath[parentPath.Length - 1].CategoryID == Intermech.Navigator.Consts.CategoryFavoritesNode;
  }

  /// <summary>Добавить объекты в Избранное</summary>
  /// <param name="items"></param>
  /// <param name="viewservices"></param>
  /// <param name="additionalinfo"></param>
  private void AddObjectsToFavorites(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items.Count <= 0)
      return;
    long[] objIds = new long[items.Count];
    for (int index = 0; index < items.Count; ++index)
    {
      long num = (items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID).Value;
      objIds[index] = num;
    }
    FavoritesCommandProvider.AddObjectsToFavorites(objIds);
  }

  /// <summary>Добавляет объекты в избранное</summary>
  /// <param name="objIds"></param>
  private static void AddObjectsToFavorites(long[] objIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IUserFavouritesService)) is IUserFavouritesService customService))
        throw new KernelException("Не найден сервис для работы с Избранным.");
      customService.IncludeObjects(sessionKeeper.Session.SessionGUID, objIds);
      Holder.NotificationService.FireEvent((object) null, new NotificationEventArgs("FavoritesChanged"));
    }
  }
}
