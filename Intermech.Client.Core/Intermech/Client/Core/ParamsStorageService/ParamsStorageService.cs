
// Type: Intermech.Client.Core.ParamsStorageService.ParamsStorageService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.ParamsStorage;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Client.Core.ParamsStorageService;

/// <summary>Реализация интерфейса IParamsStorageService</summary>
internal sealed class ParamsStorageService : IParamsStorageService
{
  /// <summary>Инициализация данных класса / службы</summary>
  private void InitializeData()
  {
  }

  /// <summary>Получение / поиск контейнера по имени</summary>
  /// <param name="storageName"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  private long GetStorageByName(string storageName, IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if ((storageName == null || storageName == string.Empty) && session == null)
      throw new ArgumentNullException(nameof (storageName));
    long storageByName = 0;
    int atDescrTypeId = Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atDescrTypeID;
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new List<ConditionStructure>(1)
    {
      new ConditionStructure(atDescrTypeId, RelationalOperators.Equal, (object) storageName, LogicalOperators.NONE, 0, false)
    }.ToArray(), new List<ColumnDescriptor>(2)
    {
      new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) atDescrTypeId, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }.ToArray(), recordCount: 1);
    DataTable dataTable = session.ObjectsSelect(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.otParamsStorageID, dbRecordSetParams);
    if (dataTable != null && dataTable.Rows.Count != 0)
      storageByName = Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]);
    return storageByName;
  }

  /// <summary>Конструктор</summary>
  public ParamsStorageService() => this.InitializeData();

  /// <summary>
  /// Создание / регистрация объекта-контейнера параметров
  /// </summary>
  /// <param name="storagеName">Имя объекта-контейнера</param>
  /// <param name="exceptionIfExists">
  /// True  - генерируется ошибка при попытке создать контейнер с имененем, уже существующим в базе.
  /// False - возвращается ранее созданый контейнер.
  /// </param>
  /// <remarks>В качестве имени контейнера могут выступать любые строковые значения (наприм. тестовые представления GUID и их комбинации),
  /// длиной до 450 символов</remarks>
  /// <returns></returns>
  public IParamsStorageObject RegisterObject(string storagеName, bool exceptionIfExists)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long storageByName = this.GetStorageByName(storagеName, sessionKeeper.Session);
      if (storageByName == 0L)
        return (IParamsStorageObject) ParamsStorageObject.CreateObject(storagеName, false, sessionKeeper.Session);
      if (exceptionIfExists)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_1610"), (object) storagеName));
      return (IParamsStorageObject) new ParamsStorageObject(storageByName, storagеName);
    }
  }

  /// <summary>Получение объекта - контейнера по его имени</summary>
  /// <param name="storageName">Имя объекта-контейнера</param>
  /// <returns></returns>
  public IParamsStorageObject GetObject(string storageName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long storageByName = this.GetStorageByName(storageName, sessionKeeper.Session);
      return storageByName == 0L ? (IParamsStorageObject) null : (IParamsStorageObject) new ParamsStorageObject(storageByName, storageName);
    }
  }

  /// <summary>Удаление объекта - контейнера</summary>
  /// <param name="storageName">Имя объекта-контейнера</param>
  public void RemoveObject(string storageName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long storageByName = this.GetStorageByName(storageName, sessionKeeper.Session);
      if (storageByName == 0L)
        return;
      ParamsStorageObject.DeleteObject(storageByName, sessionKeeper.Session);
    }
  }

  /// <summary>Константы</summary>
  public sealed class Consts
  {
    /// <summary>Гл. ид. типа атрибута "Список форм редактирования"</summary>
    public static Guid atFormListGuid = new Guid("cad0019d-306c-11d8-b4e9-00304f19f545");
    /// <summary>
    /// Идентификатор типа объектов "Контейнер службы параметров"
    /// </summary>
    public static int otParamsStorageID = -1;
    /// <summary>Ид. типа атрибута "Описание"</summary>
    public static int atDescrTypeID = 0;
    /// <summary>Ид. типа атрибута "Список форм редактирования"</summary>
    public static int atFormListID = 0;
    /// <summary>
    /// Список "системных" / фиксированных атрибутов не подлежащих копированию, перезаписи пользователем
    /// </summary>
    public static List<int> atSystemList = new List<int>();

    /// <summary>Конструктор</summary>
    static Consts()
    {
      Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.otParamsStorageID = MetaDataHelper.GetObjectTypeID("cadd940d-306c-11d8-b4e9-00304f19f545");
      Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atDescrTypeID = MetaDataHelper.GetAttributeTypeID("cad0001c-306c-11d8-b4e9-00304f19f545");
      Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atFormListID = MetaDataHelper.GetAttributeTypeID(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atFormListGuid);
      Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atSystemList.Add(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atDescrTypeID);
      Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atSystemList.Add(Intermech.Client.Core.ParamsStorageService.ParamsStorageService.Consts.atFormListID);
    }
  }
}
