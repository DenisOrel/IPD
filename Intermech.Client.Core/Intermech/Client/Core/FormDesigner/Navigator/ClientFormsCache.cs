
// Type: Intermech.Client.Core.FormDesigner.Navigator.ClientFormsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Intermech.Client.Core.FormDesigner.Navigator;

/// <summary>Клиентский кэш форм.</summary>
public static class ClientFormsCache
{
  private static Dictionary<long, byte[]> _dict = new Dictionary<long, byte[]>();
  private static int formDataAttrId = MetaDataHelper.GetAttributeID((object) "cad0011d-306c-11d8-b4e9-00304f19f545");

  /// <summary>Событие от глобальной службы уведомлений.</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private static void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e == null)
      return;
    if (e.EventName == "MetadataCacheReloaded")
      ClientFormsCache.Clear();
    else if (e.EventName == "FileReplaced")
    {
      FileReplacedEventArgs replacedEventArgs = e as FileReplacedEventArgs;
      if (replacedEventArgs.AttributeID != ClientFormsCache.formDataAttrId)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(replacedEventArgs.ElementID, false);
        if (dbObject == null)
          return;
        IDBAttribute attributeById = dbObject.GetAttributeByID(replacedEventArgs.AttributeID);
        if (attributeById == null)
          return;
        attributeById.Index = replacedEventArgs.ReplaceFileIndex;
        MemoryStream aDestStream = new MemoryStream();
        new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        ClientFormsCache.Save(replacedEventArgs.ElementID, aDestStream.ToArray());
      }
    }
    else
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      if (e.EventName == "ObjectsRemoved")
      {
        foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
          ClientFormsCache.Remove(objectId);
      }
      else
      {
        Action<long> action = (Action<long>) null;
        switch (e.EventName)
        {
          case "ObjectsCheckedIn":
            action = new Action<long>(ClientFormsCache.CheckIn);
            break;
          case "ObjectsCheckedOut":
            action = new Action<long>(ClientFormsCache.CheckOut);
            break;
          case "ObjectsChangesCancelled":
            action = new Action<long>(ClientFormsCache.UndoCheckOut);
            break;
        }
        if (action == null)
          return;
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(GuidHolder.FormsTypeGuid);
        if (childrenIdRecursive == null || childrenIdRecursive.Count <= 0)
          return;
        Dictionary<long, int> objIDsTypeIDs = new Dictionary<long, int>(objectsEventArgs.ObjectIDs.Count);
        List<ObjInfoItem> itemInfoList = SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList(objectsEventArgs.ObjectIDs.Select<long, long>((Func<long, long>) (x => Math.Abs(x))));
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) itemInfoList, sessionKeeper.Session))
            itemInfoList.ForEach((Action<ObjInfoItem>) (x => objIDsTypeIDs.Add(x.ObjectID, x.ObjTypeID)));
        }
        foreach (KeyValuePair<long, int> keyValuePair in objIDsTypeIDs)
        {
          if (childrenIdRecursive.Contains(keyValuePair.Value))
            action(keyValuePair.Key);
        }
      }
    }
  }

  /// <summary>Инициализация кэша.</summary>
  public static void Initialize()
  {
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.Subscribe(new NotificationEventHandler(ClientFormsCache.GlobalNotificationEventFired));
  }

  /// <summary>Получение формы.</summary>
  /// <param name="formID">Идентификатор формы</param>
  /// <returns>Тело формы</returns>
  public static byte[] GetForm(long formID)
  {
    byte[] form = (byte[]) null;
    if (!ClientFormsCache._dict.TryGetValue(formID, out form))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IServerFormsCache)) is IServerFormsCache customService)
        {
          form = customService.GetForm(sessionKeeper.Session.SessionGUID, formID);
          if (form != null)
            ClientFormsCache._dict.Add(formID, form);
        }
      }
    }
    return form;
  }

  /// <summary>Созранить изменения.</summary>
  /// <param name="formID">Идентификатор формы</param>
  /// <param name="bytes">Тело формы</param>
  public static void Save(long formID, byte[] bytes)
  {
    if (bytes == null || bytes.Length == 0)
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IServerFormsCache)) is IServerFormsCache customService)
      customService.Save(formID, bytes);
    ClientFormsCache._dict[formID] = bytes;
  }

  /// <summary>Завершить редактирование формы.</summary>
  /// <param name="formID">Идентификатор формы</param>
  private static void CheckIn(long formID)
  {
    formID = Math.Abs(formID);
    byte[] numArray = (byte[]) null;
    if (ClientFormsCache._dict.TryGetValue(-formID, out numArray))
    {
      ClientFormsCache._dict[formID] = numArray;
      formID = -formID;
    }
    ClientFormsCache.Remove(formID);
  }

  /// <summary>Взять форму на редактирование.</summary>
  /// <param name="formID">Идентификатор формы</param>
  private static void CheckOut(long formID)
  {
    formID = Math.Abs(formID);
    byte[] numArray = (byte[]) null;
    if (!ClientFormsCache._dict.TryGetValue(formID, out numArray))
      return;
    ClientFormsCache._dict.Add(-formID, numArray);
  }

  /// <summary>Отмена изменений.</summary>
  /// <param name="formID">Идентификатор формы</param>
  private static void UndoCheckOut(long formID) => ClientFormsCache.Remove(-Math.Abs(formID));

  /// <summary>Удаление формы из кэша.</summary>
  /// <param name="formID">Идентификатор формы</param>
  private static void Remove(long formID) => ClientFormsCache._dict.Remove(formID);

  /// <summary>Очистить кэш.</summary>
  private static void Clear() => ClientFormsCache._dict.Clear();
}
