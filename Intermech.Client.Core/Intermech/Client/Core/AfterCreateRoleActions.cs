
// Type: Intermech.Client.Core.AfterCreateRoleActions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Client.Core;

/// <summary>
/// Класс добавляет вновь созданную по прототипу роль в состав загружаемых
/// модулей, в которое входит роль-прототип
/// </summary>
public class AfterCreateRoleActions
{
  /// <summary>Ссылка на службу по созданию новых объектов</summary>
  private static IObjectCreatorService _creatorService;
  /// <summary>Идентификатор типа объекта "Роль"</summary>
  private static int _roleTypeID = -1;
  /// <summary>Идентификатор типа объекта "Загружаемый модуль"</summary>
  private static int _pluginTypeID = -1;

  /// <summary>
  /// Создать экземпляр класса, подписаться на событие у службы
  /// </summary>
  public AfterCreateRoleActions()
  {
    if (AfterCreateRoleActions._creatorService != null)
      return;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    AfterCreateRoleActions._roleTypeID = service.RolesTypeID;
    AfterCreateRoleActions._pluginTypeID = service.PluginTypeID;
    AfterCreateRoleActions._creatorService = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    AfterCreateRoleActions._creatorService.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.NewObjectCreated);
  }

  /// <summary>Создан новый экземпляр объекта</summary>
  /// <param name="sender">Ссылка на экземпляр создателя объекта</param>
  /// <param name="ea">Аргументы события</param>
  internal void NewObjectCreated(object sender, AfterObjectCreatedEventArgs ea)
  {
    if (ea.ObjectTypeID != AfterCreateRoleActions._roleTypeID || ea.PrototypeId == -1L)
      return;
    this.CorrectRole(ea.ObjectID, ea.PrototypeId);
  }

  /// <summary>
  /// Внести корректировку новой роли на основании данных из роли-прототипа
  /// </summary>
  /// <param name="roleID">Новая роль</param>
  /// <param name="prototypeID">Роль-прототип</param>
  internal void CorrectRole(long roleID, long prototypeID)
  {
    if (roleID < 0L || prototypeID < 0L || roleID == prototypeID)
      return;
    List<long> longList = new List<long>();
    DataTable dataTable = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID, "cad001e0-306c-11d8-b4e9-00304f19f545");
      if (relationCollection != null)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(prototypeID);
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          (object) ObligatoryObjectAttributes.F_PROJ_ID
        });
        dataTable = relationCollection.EntersIn(paramSet, dbObject.ID);
      }
    }
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row[0]);
      long int64 = Convert.ToInt64(row[1]);
      if (int32 == AfterCreateRoleActions._pluginTypeID)
        longList.Add(int64);
    }
    dataTable.Dispose();
    List<long> relationIDs = new List<long>();
    List<long> projIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
      for (int index = 0; index < longList.Count; ++index)
      {
        IDBRelation dbRelation = relationCollection.Create(longList[index], roleID);
        relationIDs.Add(dbRelation.RelationID);
        projIDs.Add(dbRelation.ProjID);
        relTypeIDs.Add(dbRelation.RelationType);
      }
    }
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (relationIDs.Count <= 0)
      return;
    DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs);
    service.FireEvent((object) this, (NotificationEventArgs) e);
  }
}
