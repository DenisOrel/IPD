// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ExtendedServiceHelper
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

public static class ExtendedServiceHelper
{
  /// <summary>Введдем фиктивную переменную для блокировки</summary>
  private static string _locker = string.Empty;
  /// <summary>
  /// Информация по текщему (последнему загруженному) типу объекта
  /// </summary>
  private static ExtendedServiceHelper.ObjTypeInfo _objTypeData = (ExtendedServiceHelper.ObjTypeInfo) null;

  /// <summary>Получение информации по ид. типа объекта</summary>
  /// <remarks>Общий метод и для сервера и для клиента</remarks>
  /// <param name="objTypeID">Ид. типа объекта</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static ExtendedServiceHelper.ObjTypeInfo GetObjTypeData(
    int objTypeID,
    IUserSession session)
  {
    IImbaseExtendedService imbaseExtendedService = ExtendedServiceHelper.GetImbaseExtendedService(session, false);
    return imbaseExtendedService == null ? (ExtendedServiceHelper.ObjTypeInfo) null : ExtendedServiceHelper.GetObjTypeData(objTypeID, imbaseExtendedService);
  }

  /// <summary>Получение информации по ид. типа объекта</summary>
  /// <remarks>Общий метод и для сервера и для клиента</remarks>
  /// <param name="objTypeID">Ид. типа объекта</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static ExtendedServiceHelper.ObjTypeInfo GetObjTypeData(
    int objTypeID,
    IImbaseExtendedService imExtSrv)
  {
    if (imExtSrv == null)
      return (ExtendedServiceHelper.ObjTypeInfo) null;
    lock (ExtendedServiceHelper._locker)
    {
      if (ExtendedServiceHelper.CurObjTypeInfo != null && ExtendedServiceHelper.CurObjTypeInfo.ObjTypeID == objTypeID)
        return ExtendedServiceHelper.CurObjTypeInfo;
      ExtendedServiceHelper._objTypeData = imExtSrv == null ? (ExtendedServiceHelper.ObjTypeInfo) null : new ExtendedServiceHelper.ObjTypeInfo(objTypeID, imExtSrv);
      return ExtendedServiceHelper._objTypeData;
    }
  }

  /// <summary>Получить идентификатор каталога/справочника IMBASE.</summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="typeID">Идентификатор типа объекта (для связи нужно передать -1)</param>
  /// <param name="attributeID">Идентификатор типа атрибута</param>
  /// <returns>Идентификатор каталога/справочника IMBASE</returns>
  public static ImbaseExtendedItem GetImbaseExtendedItem(
    IUserSession session,
    int typeID,
    int attributeID)
  {
    return ExtendedServiceHelper.GetObjTypeData(typeID, session)?.GetValue(attributeID, session);
  }

  /// <summary>
  /// Информация по текщему (последнему загруженному) типу объекта
  /// </summary>
  public static ExtendedServiceHelper.ObjTypeInfo CurObjTypeInfo
  {
    get => ExtendedServiceHelper._objTypeData;
  }

  /// <summary>Получение сервиса IImbaseExtendedService</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="throwErrorIfNotFound">Генерация ошибки в случае, если сервис не найден</param>
  /// <returns></returns>
  public static IImbaseExtendedService GetImbaseExtendedService(
    IUserSession session,
    bool throwErrorIfNotFound)
  {
    IImbaseExtendedService imbaseExtendedService = session != null ? ServiceUtils.GetService<IImbaseExtendedService>((object) session, false) : ServiceUtils.GetService<IImbaseExtendedService>((object) ApplicationServices.Container, false);
    return !(imbaseExtendedService == null & throwErrorIfNotFound) ? imbaseExtendedService : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.Imbase_10"), (object) typeof (IImbaseExtendedService)));
  }

  /// <summary>Настройки для типа объекта</summary>
  public class ObjTypeInfo
  {
    /// <summary>Ид. типа объекта</summary>
    protected int _objTypeID = -1;
    /// <summary>Флаг изменения настроек</summary>
    protected bool _modified;
    /// <summary>
    /// Хранятся первоначальные значения (для сравнения на изменения значений)
    /// int   - идентификатор типа аттрибута
    /// ImbaseExtendedItem - инфоормация о справочнике, режиме выбора
    /// </summary>
    private Dictionary<int, ImbaseExtendedItem> _originalDict;
    /// <summary>Рабочая версия</summary>
    private Dictionary<int, ImbaseExtendedItem> _currentDict;

    /// <summary>Загрузка данных</summary>
    /// <param name="objTypeID"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    protected bool LoadData(
      int objTypeID,
      IImbaseExtendedService imExtSrv,
      out Dictionary<int, ImbaseExtendedItem> data)
    {
      data = (Dictionary<int, ImbaseExtendedItem>) null;
      if (imExtSrv == null)
        return false;
      data = imExtSrv.GetValues(objTypeID);
      return true;
    }

    /// <summary>
    /// Поиск настроек для аттрибута по родительским типам объектов
    /// </summary>
    /// <param name="objTypeID"></param>
    /// <param name="attrID"></param>
    /// <param name="imExtSrv"></param>
    /// <returns></returns>
    private ImbaseExtendedItem FindValueOfParent(
      int objTypeID,
      int attrID,
      IImbaseExtendedService imExtSrv)
    {
      ImbaseExtendedItem valueOfParent = (ImbaseExtendedItem) null;
      Dictionary<int, ImbaseExtendedItem> values = imExtSrv.GetValues(objTypeID);
      if (values != null && values.TryGetValue(attrID, out valueOfParent))
      {
        this._currentDict.Add(attrID, valueOfParent);
        this._modified = true;
        return valueOfParent;
      }
      return objTypeID == -1 ? (ImbaseExtendedItem) null : this.FindValueOfParent(MetaDataHelper.GetObjectTypeParentID(objTypeID), attrID, imExtSrv);
    }

    /// <summary>Ид. типа объкта</summary>
    /// <param name="objTypeID"></param>
    /// <param name="imExtSrv"></param>
    public ObjTypeInfo(int objTypeID, IImbaseExtendedService imExtSrv)
    {
      this._originalDict = new Dictionary<int, ImbaseExtendedItem>();
      this._currentDict = new Dictionary<int, ImbaseExtendedItem>();
      this._objTypeID = objTypeID;
      this.LoadData(imExtSrv);
    }

    /// <summary>Ид. типа объкта</summary>
    /// <param name="objTypeID"></param>
    /// <param name="session"></param>
    public ObjTypeInfo(int objTypeID, IUserSession session)
      : this(objTypeID, ExtendedServiceHelper.GetImbaseExtendedService(session, true))
    {
    }

    /// <summary>Получить идентификатор каталога/справочника IMBASE.</summary>
    /// <param name="attrTypeID">Идентификатор атрибута</param>
    /// <param name="imExtSrv"></param>
    /// <returns>Элемент для хранения информации IMBASE для типа аттрибута</returns>
    public ImbaseExtendedItem GetValue(int attrTypeID, IImbaseExtendedService imExtSrv)
    {
      if (attrTypeID == 0)
        return (ImbaseExtendedItem) null;
      ImbaseExtendedItem imbaseExtendedItem = (ImbaseExtendedItem) null;
      if (!this._currentDict.TryGetValue(attrTypeID, out imbaseExtendedItem))
        imbaseExtendedItem = this.FindValueOfParent(MetaDataHelper.GetObjectTypeParentID(this._objTypeID), attrTypeID, imExtSrv);
      return imbaseExtendedItem;
    }

    /// <summary>Получить идентификатор каталога/справочника IMBASE.</summary>
    /// <param name="attrTypeID">Идентификатор атрибута</param>
    /// <param name="session"></param>
    /// <returns>Элемент для хранения информации IMBASE для типа аттрибута</returns>
    public ImbaseExtendedItem GetValue(int attrTypeID, IUserSession session)
    {
      if (attrTypeID == 0)
        return (ImbaseExtendedItem) null;
      IImbaseExtendedService imbaseExtendedService = ExtendedServiceHelper.GetImbaseExtendedService(session, true);
      return this.GetValue(attrTypeID, imbaseExtendedService);
    }

    /// <summary>
    /// Установить значение.
    /// Работает и для удаления, удаляет если value = null или value.СatalogID = -1.
    /// </summary>
    /// <param name="attrID">Идентификатор атрибута</param>
    /// <param name="value">Элемент для хранения информации IMBASE для типа аттрибута</param>
    /// <returns></returns>
    public bool SetValue(int attrTypeID, ImbaseExtendedItem value)
    {
      if (attrTypeID == 0)
        return false;
      ImbaseExtendedItem imbaseExtendedItem = (ImbaseExtendedItem) null;
      if (this._currentDict.ContainsKey(attrTypeID))
      {
        if (value == null || value.CatalogIDs == null || value.CatalogIDs.Count == 0)
        {
          this._currentDict.Remove(attrTypeID);
          this._modified = true;
        }
        else if (imbaseExtendedItem == null || imbaseExtendedItem != value)
        {
          this._currentDict[attrTypeID] = value;
          this._modified = true;
        }
      }
      else
      {
        if (value == null || value.CatalogIDs == null || value.CatalogIDs.Count == 0)
          return false;
        this._currentDict.Add(attrTypeID, value);
        this._modified = true;
      }
      return true;
    }

    /// <summary>Проверка на изменение идентификатора атрибута.</summary>
    /// <param name="attrID">Идентификатор атрибута</param>
    /// <returns>Результат проверки</returns>
    public bool IsAttrValueChanged(int attrTypeID)
    {
      if (!this._modified)
        return false;
      ImbaseExtendedItem imbaseExtendedItem1 = (ImbaseExtendedItem) null;
      ImbaseExtendedItem imbaseExtendedItem2 = (ImbaseExtendedItem) null;
      this._originalDict.TryGetValue(attrTypeID, out imbaseExtendedItem1);
      this._currentDict.TryGetValue(attrTypeID, out imbaseExtendedItem2);
      return imbaseExtendedItem1 != imbaseExtendedItem2;
    }

    /// <summary>Загрузить / обновить значения.</summary>
    /// <param name="session"></param>
    /// <remarks>Не сохраняя изменения, в случае их наличия</remarks>
    public void LoadData(IUserSession session)
    {
      this.LoadData(ExtendedServiceHelper.GetImbaseExtendedService(session, true));
    }

    /// <summary>Загрузить / обновить значения.</summary>
    /// <param name="imExtSrv"></param>
    /// <remarks>Не сохраняя изменения, в случае их наличия</remarks>
    public void LoadData(IImbaseExtendedService imExtSrv)
    {
      if (imExtSrv == null)
        return;
      Dictionary<int, ImbaseExtendedItem> data = (Dictionary<int, ImbaseExtendedItem>) null;
      if (!this.LoadData(this.ObjTypeID, imExtSrv, out data) || data == null)
        this._originalDict.Clear();
      else
        this._originalDict = data;
      this._currentDict = new Dictionary<int, ImbaseExtendedItem>((IDictionary<int, ImbaseExtendedItem>) this._originalDict);
      this._modified = false;
    }

    /// <summary>Сохранить установленные значения.</summary>
    public void SaveData(IUserSession session)
    {
      if (!this._modified)
        return;
      ExtendedServiceHelper.GetImbaseExtendedService(session, true).SetValues(session.SessionGUID, this._objTypeID, (IDictionary<int, ImbaseExtendedItem>) this._currentDict);
      this._modified = false;
    }

    /// <summary>Ид. типа объекта</summary>
    public int ObjTypeID => this._objTypeID;

    /// <summary>Флаг изменения настроек</summary>
    public bool Modified => this._modified;
  }
}
