
// Type: Intermech.Client.Core.ParamsStorageService.ParamsStorageObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ParamsStorageService.Forms;
using Intermech.Interfaces;
using Intermech.Interfaces.ParamsStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.ParamsStorageService;

/// <summary>Реализация интерфейса IParamsStorageObject</summary>
internal class ParamsStorageObject : IParamsStorageObject
{
  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии контейнера</param>
  /// <param name="storageName">Имя объекта контейнера</param>
  public ParamsStorageObject(long objectId, string storageName)
  {
    this.ObjectID = objectId;
    this.StorageName = storageName;
  }

  /// <summary>Ид. версии объекта контейнера</summary>
  /// <remarks>Без крайней необходимости не использовать</remarks>
  public long ObjectID { get; }

  /// <summary>Имя объекта контейнера</summary>
  public string StorageName { get; private set; }

  /// <summary>
  /// Получение списка форм редактирования, назначенных контейнеру
  /// </summary>
  /// <returns></returns>
  public long[] GetFormDesignIDs()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID, false);
      if (dbObject == null)
        return new long[0];
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atFormListID);
      return attributeById?.Values == null ? new long[0] : ((IEnumerable<object>) attributeById.Values).Where<object>((Func<object, bool>) (item => item != null && item != DBNull.Value)).Select<object, long>(new Func<object, long>(Convert.ToInt64)).ToArray<long>();
    }
  }

  /// <summary>Назначение форм редактирования контейнеру</summary>
  /// <param name="formIDs"></param>
  public void SetFormDesignIDs(long[] formIDs)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID, false);
      if (dbObject == null)
        return;
      List<long> longList = new List<long>();
      if (formIDs != null)
      {
        foreach (long formId in formIDs)
        {
          if (formId != 0L && formId != 0L && !longList.Contains(formId))
            longList.Add(formId);
        }
      }
      if (longList.Count == 0)
      {
        dbObject.GetAttributeByID(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atFormListID)?.Delete(0L);
      }
      else
      {
        AttributeValues[] valuesList = new AttributeValues[1]
        {
          new AttributeValues(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atFormListID, (object) Array.ConvertAll<long, object>(longList.ToArray(), (Converter<long, object>) (item => (object) item)))
        };
        dbObject.SetAttributesValues(valuesList);
      }
    }
  }

  /// <summary>
  /// Получение списка значений атрибутов, назначенных контейнеру
  /// </summary>
  /// <param name="attrValues"></param>
  public AttributeValues[] GetAttributeValues()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID, false);
      return dbObject != null ? ParamsStorageObject.RemoveSystemAttrs(dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeCaption)) : (AttributeValues[]) null;
    }
  }

  /// <summary>Назначение значений атрибутов контейнеру</summary>
  /// <param name="attrValues"></param>
  /// <param name="deleteNotExistingAttr"></param>
  public void SetAttributeValues(AttributeValues[] attrValues, bool deleteNotExistingAttr)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID, false);
      if (dbObject == null)
        return;
      attrValues = ParamsStorageObject.RemoveSystemAttrs(attrValues);
      List<AttributeValues> attributeValuesList = new List<AttributeValues>();
      if (attrValues != null)
        attributeValuesList.AddRange((IEnumerable<AttributeValues>) attrValues);
      if (deleteNotExistingAttr)
      {
        foreach (int atSystem in Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atSystemList)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(atSystem);
          if (attributeById != null)
            attributeValuesList.Add(new AttributeValues(atSystem, (object) attributeById.Values));
        }
      }
      dbObject.SetAttributesValues(attributeValuesList.ToArray(), deleteNotExistingAttr, true);
    }
  }

  /// <summary>Отображение диалога с формами ввода/редактирования</summary>
  /// <remarks>Если формы не заданы - метод вернет false.
  /// Результирующие значения атрибутов сохраняются в контейнере</remarks>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="resultValues">Результирующий список значений атрибутов</param>
  /// <returns></returns>
  public DialogResult ShowDialog(string caption, out AttributeValues[] resultValues)
  {
    return this.ShowDialog(caption, false, out resultValues);
  }

  /// <summary>Отображение диалога с формами ввода/редактирования</summary>
  /// <remarks>Если формы не заданы - метод вернет false</remarks>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="temporaryMode">Флаг режима сохранения результирующих. False - результат не сохраняется в контейнер</param>
  /// <param name="resultValues">Результирующий список значений атрибутов</param>
  /// <returns></returns>
  public DialogResult ShowDialog(
    string caption,
    bool temporaryMode,
    out AttributeValues[] resultValues)
  {
    return this.ShowDialog(caption, false, (AttributeValues[]) null, out resultValues);
  }

  /// <summary>Отображение диалога с формами ввода/редактирования</summary>
  /// <remarks>Если формы не заданы - метод вернет false</remarks>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="temporaryMode">Флаг режима сохранения результирующих. False - результат не сохраняется в контейнер</param>
  /// <param name="paramValues">Список атрибутов - параметров</param>
  /// <param name="resultValues">Результирующий список значений атрибутов</param>
  /// <returns></returns>
  public DialogResult ShowDialog(
    string caption,
    bool temporaryMode,
    AttributeValues[] paramValues,
    out AttributeValues[] resultValues)
  {
    resultValues = (AttributeValues[]) null;
    long[] formDesignIds = this.GetFormDesignIDs();
    if (formDesignIds == null || formDesignIds.Length == 0)
      return DialogResult.None;
    ParamsStorageObject paramsStorage = this;
    if (temporaryMode)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        paramsStorage = ParamsStorageObject.CreateObject((string) null, true, sessionKeeper.Session);
        paramsStorage.StorageName = this.StorageName;
      }
      paramsStorage.SetFormDesignIDs(this.GetFormDesignIDs());
      paramsStorage.SetAttributeValues(this.GetAttributeValues(), false);
    }
    if (paramValues != null && paramValues.Length != 0)
      paramsStorage.SetAttributeValues(paramValues, false);
    try
    {
      ParamsStorageUserForm paramsStorageUserForm = new ParamsStorageUserForm((IParamsStorageObject) paramsStorage);
      paramsStorageUserForm.Text = caption;
      int num = (int) paramsStorageUserForm.ShowDialog();
      if (num == 1)
        resultValues = paramsStorage.GetAttributeValues();
      return (DialogResult) num;
    }
    finally
    {
      if (temporaryMode)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          ParamsStorageObject.DeleteObject(paramsStorage.ObjectID, sessionKeeper.Session);
      }
    }
  }

  /// <summary>Удаление "фиксированных" параметров из списка</summary>
  /// <param name="attrValues"></param>
  /// <returns></returns>
  internal static AttributeValues[] RemoveSystemAttrs(AttributeValues[] attrValues)
  {
    if (attrValues == null || attrValues.Length == 0)
      return attrValues;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>(attrValues.Length);
    foreach (AttributeValues attrValue in attrValues)
    {
      if (attrValue.AttributeID >= 0 && !Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atSystemList.Contains(attrValue.AttributeID))
        attributeValuesList.Add(attrValue);
    }
    return attributeValuesList.ToArray();
  }

  /// <summary>Создание нового объекта - контейнера</summary>
  /// <param name="objectID">Ид. версии контейнера</param>
  /// <param name="storageName">Имя объекта контейнера</param>
  /// <param name="tempMode">Признак временного контейнера</param>
  /// <param name="session"></param>
  internal static ParamsStorageObject CreateObject(
    string storageName,
    bool tempMode,
    IUserSession session)
  {
    if (!tempMode)
    {
      if (storageName == null)
        throw new ArgumentNullException(nameof (storageName));
      if (storageName == string.Empty)
        throw new ArgumentException(nameof (storageName));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.otParamsStorageID).Create();
      if (!string.IsNullOrEmpty(storageName))
        dbObject.Attributes.AddAttribute(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atDescrTypeID, false, new object[1]
        {
          (object) storageName
        });
      if (dbObject.IsCreationMode)
        dbObject.CommitCreation(true);
      return new ParamsStorageObject(dbObject.ObjectID, storageName);
    }
  }

  /// <summary>Удаление объекта - контейнера</summary>
  /// <param name="objectId"></param>
  /// <param name="session"></param>
  internal static void DeleteObject(long objectId, IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    session.GetObject(objectId, false)?.Delete(0L);
  }
}
