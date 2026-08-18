// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.RecordCodeHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Imbase.Server.Sync.Records;
using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class RecordCodeHandler(Guid taskGuid) : CodeHandler(taskGuid)
{
  public override void Handle(EventRecord record, IDataBase sourceDB, IUserSession session)
  {
    switch (record.Code)
    {
      case 120:
      case (int) sbyte.MaxValue:
        this.Add(record, sourceDB, session);
        break;
      case 121:
        this.ChangeAttributes(record, sourceDB, session);
        break;
      case 122:
        this.Delete(record, session);
        break;
      case 126:
        this.Move(record, sourceDB, session);
        break;
    }
  }

  private void Move(EventRecord record, IDataBase sourceDB, IUserSession session)
  {
    TableRecord tableRecord = CodeHandler.GetTableRecord(sourceDB, record.Catalog);
    if (tableRecord == null)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось переместить запись каталога. Запись {record.Catalog} в таблице IM_TABLES базы-источника не найдена");
    }
    else
    {
      string msgInfo1;
      long objectByImbaseCode1 = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID), record.ObjKey, record.Catalog, out msgInfo1);
      if (objectByImbaseCode1 == 0L)
      {
        objectByImbaseCode1 = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeGUID), record.ObjKey, record.Catalog, out string _);
        if (objectByImbaseCode1 == 0L)
        {
          this.AddEventInfo(EventType.Warning, $"Не удалось переместить запись каталога. Запись не найдена по коду Imbase {record.ObjKey} в базе-приемнике. {msgInfo1}");
          return;
        }
      }
      int key;
      int objectTypeId;
      if (record.Folder == 0)
      {
        key = record.Catalog;
        objectTypeId = MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545");
      }
      else
      {
        key = this.GetFolderRecord(sourceDB, tableRecord.TableName, record.Folder).Key;
        objectTypeId = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID);
      }
      string msgInfo2;
      long objectByImbaseCode2 = CodeHandler.GetObjectByImbaseCode(session, objectTypeId, key, record.Catalog, out msgInfo2);
      if (objectByImbaseCode2 == 0L)
      {
        this.AddEventInfo(EventType.Warning, $"Не удалось переместить запись каталога. {(record.Folder == 0 ? (object) "Каталог не найден" : (object) "Папка не найдена")} по коду Imbase = '{key}' в базе-приемнике. {msgInfo2}");
      }
      else
      {
        IDBObject thisObject = session.GetObject(objectByImbaseCode1);
        string str1 = $"{thisObject.NameInMessages} [{thisObject.ObjectID}]";
        IDBObject parentObject = session.GetObject(objectByImbaseCode2);
        IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545"));
        relationCollection.LocalTypesMode = true;
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-22, RelationalOperators.Equal, (object) thisObject.ID, LogicalOperators.AND, 0, false)
        }, new object[1]{ (object) -20 });
        DataTable dataTable = relationCollection.Select(paramSet);
        IDBRelation dbRelation = (IDBRelation) null;
        if (dataTable.Rows.Count > 0)
          dbRelation = session.GetRelation(Convert.ToInt64(dataTable.Rows[0][0]), false);
        if (dbRelation != null)
          dbRelation.ProjID = parentObject.ObjectID;
        else
          relationCollection.Create(parentObject.ObjectID, thisObject.ObjectID);
        CodeHandler.CreateNewClassifCode(session, thisObject, parentObject);
        string str2 = !string.IsNullOrEmpty(msgInfo1) ? msgInfo1 : string.Empty;
        if (str2 != msgInfo2)
          str2 = !string.IsNullOrEmpty(msgInfo2) ? (!string.IsNullOrEmpty(str2) ? $"{str2}. {msgInfo2}" : msgInfo2) : string.Empty;
        this.AddEventInfo(EventType.Text, $"Объект '{str1}' перемещен в '{parentObject.NameInMessages}'. {str2}");
      }
    }
  }

  private void Delete(EventRecord record, IUserSession session)
  {
    string msgInfo;
    long objectByImbaseCode;
    if (record.Table > 0)
    {
      objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID), record.ObjKey, record.Catalog, out msgInfo);
      if (objectByImbaseCode == 0L)
      {
        this.AddEventInfo(EventType.Warning, $"Невозможно удалить таблицу. Ссылка на таблицу по коду Imbase {record.Table} в базе-приемнике не найдена");
        return;
      }
    }
    else
    {
      objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeGUID), record.ObjKey, record.Catalog, out msgInfo);
      if (objectByImbaseCode == 0L)
      {
        this.AddEventInfo(EventType.Warning, $"Запись каталога по коду Imbase {record.ObjKey} в базе-приемнике не найдена");
        return;
      }
    }
    IDBObject dbObject = session.GetObject(objectByImbaseCode);
    string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
    dbObject.Delete(0L);
    this.AddEventInfo(EventType.Text, $"Объект {str} удален. {msgInfo}");
  }

  private void ChangeAttributes(EventRecord eventRecord, IDataBase sourceDB, IUserSession session)
  {
    TableRecord tableRecord1 = CodeHandler.GetTableRecord(sourceDB, eventRecord.Catalog);
    if (tableRecord1 == null)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось изменить атрибуты записи каталога. Запись {eventRecord.Catalog} в таблице IM_TABLES базы-источника не найдена");
    }
    else
    {
      IArticleService service = ServiceUtils.GetService<IArticleService>((object) session, true);
      if (eventRecord.Table > 0)
      {
        TableRecord tableRecord2 = CodeHandler.GetTableRecord(sourceDB, eventRecord.Table);
        if (tableRecord2 == null)
        {
          this.AddEventInfo(EventType.Warning, $"Не удалось изменить атрибуты записи каталога. Запись {eventRecord.Table} в таблице IM_TABLES базы-источника не найдена");
        }
        else
        {
          string msgInfo;
          long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID), eventRecord.ObjKey, eventRecord.Catalog, out msgInfo);
          if (objectByImbaseCode == 0L)
          {
            this.AddEventInfo(EventType.Warning, $"Не удалось изменить атрибуты записи каталога. Ссылка на таблицу по коду Imbase {tableRecord2.Key} в базе-приемнике не найдена. {msgInfo}");
          }
          else
          {
            IDBObject newTableObject = session.GetObject(objectByImbaseCode);
            string str = $"{newTableObject.NameInMessages} [{newTableObject.ObjectID}]";
            this.AddAtributesToTableLink(session, sourceDB, eventRecord, service, newTableObject, tableRecord2, tableRecord1, eventRecord.ObjKey);
            this.AddEventInfo(EventType.Text, $"Объект {str} изменен. {msgInfo}");
          }
        }
      }
      else if (eventRecord.Folder < 0)
      {
        FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRecord1.TableName, Math.Abs(eventRecord.Folder));
        if (folderRecord == null)
        {
          this.AddEventInfo(EventType.Warning, $"Не удалось изменить атрибуты папки. Запись {Math.Abs(eventRecord.Folder)} в таблице {tableRecord1.TableName} базы-источника не найдена");
        }
        else
        {
          string msgInfo;
          long objectID = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, eventRecord.Catalog, out msgInfo);
          if (objectID == 0L)
          {
            eventRecord.Folder = Math.Abs(eventRecord.Folder);
            objectID = new FolderCodeHandler(this.TaskGuid).AddFolder(CodeHandler.GetTableRecord(sourceDB, eventRecord.Catalog), eventRecord, sourceDB, session);
          }
          if (objectID == 0L)
          {
            this.AddEventInfo(EventType.Warning, $"Не удалось изменить атрибуты папки. Папка по коду Imbase {folderRecord.Key} в базе-приемнике не найдена. {msgInfo}");
          }
          else
          {
            IDBObject newObject = session.GetObject(objectID);
            string str = $"{newObject.NameInMessages} [{newObject.ObjectID}]";
            string recTableName = $"{tableRecord1.TableName}_REC";
            FieldRecord[] fields = this.GetFields(sourceDB, tableRecord1.Key);
            this.LinkAttributes(session, fields, sourceDB, eventRecord, tableRecord1.TableName, recTableName, folderRecord.Key);
            this.AddAttributesToRecord(session, sourceDB, recTableName, eventRecord.ObjKey, fields, newObject, service, true);
            IDBAttribute dbAttribute1 = newObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false);
            if (dbAttribute1 != null)
              dbAttribute1.AsString = folderRecord.Name;
            if (folderRecord.GraphID > 0)
            {
              IDBAttribute dbAttribute2 = newObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID));
              long oldPictureObjectId = 0;
              if (dbAttribute2 != null)
                oldPictureObjectId = dbAttribute2.AsInteger;
              try
              {
                long num = this.AddNewPicture(session, sourceDB, folderRecord.GraphID, oldPictureObjectId);
                if (num != 0L)
                {
                  if (dbAttribute2 == null)
                    dbAttribute2 = newObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false);
                  if (dbAttribute2 != null)
                    dbAttribute2.Value = (object) num;
                }
              }
              catch (Exception ex)
              {
                this.AddEventInfo(EventType.Warning, $"При изменении {str} возникла ошибка изменения изображения: {ex.Message}");
              }
            }
            this.AddEventInfo(EventType.Text, $"Объект '{str}' изменен. {msgInfo}");
          }
        }
      }
      else
      {
        string msgInfo;
        long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeGUID), eventRecord.ObjKey, eventRecord.Catalog, out msgInfo);
        if (objectByImbaseCode == 0L)
        {
          this.AddEventInfo(EventType.Warning, $"Не удалось изменить атрибуты записи каталога. Запись каталога по коду Imbase {eventRecord.ObjKey} в базе-приемнике не найдена");
        }
        else
        {
          IDBObject newObject = session.GetObject(objectByImbaseCode);
          string str = $"{newObject.NameInMessages} [{newObject.ObjectID}]";
          string recTableName = $"{tableRecord1.TableName}_REC";
          FieldRecord[] fields = this.GetFields(sourceDB, tableRecord1.Key);
          this.LinkAttributes(session, fields, sourceDB, eventRecord, tableRecord1.TableName, recTableName, eventRecord.ObjKey);
          this.AddAttributesToRecord(session, sourceDB, recTableName, eventRecord.ObjKey, fields, newObject, service, false);
          this.AddEventInfo(EventType.Text, $"Объект '{str}' изменен. {msgInfo}");
        }
      }
    }
  }

  internal void Add(EventRecord eventRecord, IDataBase sourceDB, IUserSession session)
  {
    bool flag1 = false;
    IDBTransactions customService1 = (IDBTransactions) session.GetCustomService(typeof (IDBTransactions));
    customService1.StartTransaction();
    try
    {
      IArticleService customService2 = session.GetCustomService(typeof (IArticleService)) as IArticleService;
      if (eventRecord.Table > 0)
      {
        TableRecord tableRecord1 = CodeHandler.GetTableRecord(sourceDB, eventRecord.Table);
        TableRecord tableRecord2 = CodeHandler.GetTableRecord(sourceDB, eventRecord.Catalog);
        if (tableRecord1 == null)
          this.AddEventInfo(EventType.Warning, $"Не удалось создать ярлык таблицы. Запись {eventRecord.Table} в таблице IM_TABLES базы-источника не найдена");
        else if (tableRecord1.Openmode == 0)
        {
          string msgInfo1;
          long objectByImbaseCode1 = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableTypeGUID), tableRecord1.Key, eventRecord.Catalog, out msgInfo1);
          if (objectByImbaseCode1 == 0L)
          {
            this.AddEventInfo(EventType.Text, $"Не удалось создать ярлык таблицы. Таблица по коду Imbase {tableRecord1.Key} в базе-приемнике не найдена. Событие будет обработано позже.");
            this.AddDelayedEvent(eventRecord);
          }
          else
          {
            long objectByImbaseCode2 = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID), eventRecord.ObjKey, 0, out string _);
            if (objectByImbaseCode2 != 0L)
            {
              this.AddEventInfo(EventType.Warning, $"Ярлык таблицы {tableRecord1.Key} уже существует.{objectByImbaseCode2}");
            }
            else
            {
              IDBObject dbObject = session.GetObject(objectByImbaseCode1);
              IDBObject newTableObject = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID)).Create();
              newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseTableRefAttGUID), false).Value = (object) dbObject.ObjectID;
              newTableObject.OwnerID = dbObject.OwnerID;
              IDBAttribute byId = dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID));
              if (byId != null && byId.Value != null && byId.Value != DBNull.Value)
                newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false, new object[1]
                {
                  (object) byId.AsInteger
                });
              if (tableRecord2 != null)
              {
                this.AddAtributesToTableLink(session, sourceDB, eventRecord, customService2, newTableObject, tableRecord1, tableRecord2, eventRecord.ObjKey);
                IDBAttribute dbAttribute = newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false);
                if (dbAttribute != null)
                  dbAttribute.AsInteger = (long) eventRecord.ObjKey;
              }
              int key = 0;
              int objType = -1;
              if (eventRecord.Folder == 0)
              {
                key = eventRecord.Catalog;
                objType = MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545");
              }
              else if (tableRecord2 != null)
              {
                FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRecord2.TableName, eventRecord.Folder);
                if (folderRecord == null)
                {
                  flag1 = true;
                  this.AddEventInfo(EventType.Warning, $"Не удалось создать ярлык таблицы. При создании ссылки на таблицу {dbObject.ObjectID}, в БД-приемнике не найдена родительская папка {eventRecord.Folder} в каталоге {tableRecord2.Description} ({tableRecord2.TableName})");
                  return;
                }
                key = folderRecord.Key;
                objType = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID);
              }
              string msgInfo2;
              long projectID = CodeHandler.GetObjectByImbaseCode(session, objType, key, 0, out msgInfo2);
              if (projectID == 0L && eventRecord.Folder != 0)
                projectID = new FolderCodeHandler(this.TaskGuid).AddFolder(CodeHandler.GetTableRecord(sourceDB, eventRecord.Catalog), eventRecord, sourceDB, session);
              if (projectID == 0L)
              {
                flag1 = true;
                this.AddEventInfo(EventType.Warning, $"Не удалось создать ярлык таблицы. При создании ссылки на таблицу {dbObject.ObjectID}, в БД-приемнике не найден родительский объект по значению атрибута {MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID)} = {key}");
              }
              else
              {
                session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545")).Create(projectID, newTableObject.ObjectID);
                newTableObject.CommitCreation(true);
                string str1 = !string.IsNullOrEmpty(msgInfo1) ? msgInfo1 : string.Empty;
                string str2 = !string.IsNullOrEmpty(msgInfo2) ? (!string.IsNullOrEmpty(str1) ? $"{str1}. {msgInfo2}" : msgInfo2) : string.Empty;
                this.AddEventInfo(EventType.Text, $"Объект {newTableObject.NameInMessages} [{newTableObject.ObjectID}] создан. {str2}");
              }
            }
          }
        }
        else
        {
          if (tableRecord1.Openmode != 2)
            return;
          bool flag2 = false;
          IDBObjectCollection objectCollection = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableMixTypeGUID));
          ConditionStructure conditionStructure = new ConditionStructure(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttGUID), RelationalOperators.Equal, (object) tableRecord1.TableName, LogicalOperators.AND, 0, false);
          DataTable dataTable = objectCollection.SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
          {
            conditionStructure
          }, new object[1]{ (object) -2 }));
          IDBObject dbObject;
          if (dataTable != null && dataTable.Rows.Count > 0)
          {
            dbObject = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
            if (dbObject.OwnerID != 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
              dbObject = dbObject.CheckOut();
          }
          else
          {
            dbObject = objectCollection.Create();
            flag2 = true;
          }
          int key = 0;
          int objType = -1;
          if (eventRecord.Folder == 0)
          {
            key = eventRecord.Catalog;
            objType = MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545");
          }
          else if (tableRecord2 != null)
          {
            FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRecord2.TableName, eventRecord.Folder);
            if (folderRecord == null)
            {
              flag1 = true;
              this.AddEventInfo(EventType.Warning, $"Не удалось создать таблицу рецептур. При создании таблицы рецептур {dbObject.ObjectID}, в БД-приемнике не найдена родительская папка {eventRecord.Folder} в каталоге {tableRecord2.Description} ({tableRecord2.TableName})");
              return;
            }
            key = folderRecord.Key;
            objType = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID);
          }
          string msgInfo;
          long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, objType, key, 0, out msgInfo);
          if (objectByImbaseCode == 0L)
          {
            flag1 = true;
            this.AddEventInfo(EventType.Warning, $"Не удалось создать таблицу рецептур. При создании таблицы рецептур {dbObject.ObjectID}, в БД-приемнике не найден родительский объект по значению атрибута {MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID)} = {key}");
          }
          else
          {
            if (dbObject == null)
              return;
            IDBAttribute dbAttribute1 = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false);
            if (dbAttribute1 != null)
              dbAttribute1.AsInteger = (long) eventRecord.Table;
            IDBAttribute dbAttribute2 = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttGUID), false);
            if (dbAttribute2 != null)
              dbAttribute2.AsString = tableRecord1.TableName;
            if (flag2)
              session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545")).Create(objectByImbaseCode, dbObject.ObjectID);
            if (dbObject.IsCreationMode)
              dbObject.CommitCreation(true);
            if (!flag2)
              return;
            string str = !string.IsNullOrEmpty(msgInfo) ? msgInfo : string.Empty;
            this.AddEventInfo(EventType.Text, $"Создан объект '{dbObject.NameInMessages}'. {str}");
          }
        }
      }
      else
      {
        TableRecord tableRecord = CodeHandler.GetTableRecord(sourceDB, eventRecord.Catalog);
        if (tableRecord == null)
        {
          this.AddEventInfo(EventType.Warning, $"Не удалось добавить запись каталога. Запись {eventRecord.Catalog} в таблице IM_TABLES базы-источника не найдена");
        }
        else
        {
          string recTableName = $"{tableRecord.TableName}_REC";
          FieldRecord[] fields = this.GetFields(sourceDB, tableRecord.Key);
          this.LinkAttributes(session, fields, sourceDB, eventRecord, tableRecord.TableName, recTableName, tableRecord.Key);
          if (eventRecord.Folder < 0)
          {
            FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRecord.TableName, Math.Abs(eventRecord.Folder));
            if (folderRecord == null)
            {
              this.AddEventInfo(EventType.Warning, $"Не удалось добавить/изменить атрибуты папки). Запись {Math.Abs(eventRecord.Folder)} в таблице {tableRecord.TableName} базы-источника не найдена");
            }
            else
            {
              string msgInfo;
              long objectID = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out msgInfo);
              if (objectID == 0L)
              {
                eventRecord.Folder = Math.Abs(eventRecord.Folder);
                objectID = new FolderCodeHandler(this.TaskGuid).AddFolder(tableRecord, eventRecord, sourceDB, session);
              }
              if (objectID == 0L)
              {
                this.AddEventInfo(EventType.Warning, $"Не удалось добавить добавить/изменить атрибуты папки. Папка по коду Imbase {folderRecord.Key} в базе-приемнике не найдена");
              }
              else
              {
                IDBObject newObject = session.GetObject(objectID);
                string str = $"{newObject.NameInMessages} [{newObject.ObjectID}]";
                this.AddAttributesToRecord(session, sourceDB, recTableName, eventRecord.ObjKey, fields, newObject, customService2, true);
                this.AddEventInfo(EventType.Text, $"Объект {str} изменен. {msgInfo}");
              }
            }
          }
          else
          {
            IDBObject newObject = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeGUID)).Create();
            this.AddAttributesToRecord(session, sourceDB, recTableName, eventRecord.ObjKey, fields, newObject, customService2, false);
            int key;
            int objectTypeId;
            if (eventRecord.Folder == 0)
            {
              key = eventRecord.Catalog;
              objectTypeId = MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545");
            }
            else
            {
              key = this.GetFolderRecord(sourceDB, tableRecord.TableName, eventRecord.Folder).Key;
              objectTypeId = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID);
            }
            string msgInfo;
            long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, objectTypeId, key, eventRecord.Catalog, out msgInfo);
            if (objectByImbaseCode == 0L)
              throw new Exception($"При создании записи {newObject.ObjectID}, в БД-приемнике не найден родительский объект по значению атрибута {MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID)} = {key}");
            session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545")).Create(objectByImbaseCode, newObject.ObjectID);
            newObject.CommitCreation(true);
            this.AddEventInfo(EventType.Text, $"Объект {$"{newObject.NameInMessages} [{newObject.ObjectID}]"} создан. {msgInfo}");
          }
        }
      }
    }
    catch (Exception ex)
    {
      flag1 = true;
      throw;
    }
    finally
    {
      if (flag1)
        customService1.Rollback();
      else
        customService1.Commit();
    }
  }

  private void AddAtributesToTableLink(
    IUserSession session,
    IDataBase sourceDB,
    EventRecord eventRecord,
    IArticleService artSrv,
    IDBObject newTableObject,
    TableRecord tableRec,
    TableRecord catalogRec,
    int recKey)
  {
    IDBAttribute dbAttribute = newTableObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false);
    if (dbAttribute != null && dbAttribute.AsString != tableRec.Description)
      dbAttribute.AsString = tableRec.Description;
    FieldRecord[] fields = this.GetFields(sourceDB, catalogRec.Key);
    this.LinkAttributes(session, fields, sourceDB, eventRecord, catalogRec.TableName, catalogRec.TableName + "_REC", tableRec.Key);
    this.AddAttributesToRecord(session, sourceDB, catalogRec.TableName + "_REC", recKey, fields, newTableObject, artSrv, true);
  }
}
