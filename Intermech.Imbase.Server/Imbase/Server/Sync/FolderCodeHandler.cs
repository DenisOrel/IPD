// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.FolderCodeHandler
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

internal class FolderCodeHandler(Guid taskGuid) : CodeHandler(taskGuid)
{
  public override void Handle(EventRecord record, IDataBase sourceDB, IUserSession session)
  {
    TableRecord tableRec = (TableRecord) null;
    if (record.Code != 142)
      tableRec = CodeHandler.GetTableRecord(sourceDB, record.Catalog);
    if (tableRec == null && record.Code != 142)
    {
      this.AddEventInfo(EventType.Warning, $"Папка {record.Catalog} в таблице IM_TABLES базы-источника не найдена.");
    }
    else
    {
      switch (record.Code)
      {
        case 140:
        case 147:
          this.AddFolder(tableRec, record, sourceDB, session);
          this.ChangeVisibility(tableRec, record, sourceDB, session);
          break;
        case 141:
          this.Change(tableRec, record, sourceDB, session);
          break;
        case 142:
          this.Delete(record, session);
          break;
        case 143:
          this.ChangeVisibility(tableRec, record, sourceDB, session);
          break;
        case 145:
          this.Rename(tableRec, record, sourceDB, session);
          break;
        case 146:
          this.Move(tableRec, record, sourceDB, session);
          break;
        case 148:
          this.ChangeImage(tableRec, record, sourceDB, session);
          break;
        case 149:
          this.ChangeNote(tableRec, record, sourceDB, session);
          break;
      }
    }
  }

  internal long AddFolder(
    TableRecord tableRec,
    EventRecord record,
    IDataBase sourceDB,
    IUserSession session)
  {
    FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRec.TableName, record.Folder);
    if (folderRecord == null)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось создать папку код Imbase = '{record.Folder}' в таблице {tableRec.TableName} базы-источника не найдена");
      return 0;
    }
    long objectByImbaseCode1 = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out string _);
    if (objectByImbaseCode1 != 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Папка с кодом Imbase = '{record.Folder}' уже существует. {objectByImbaseCode1}");
      return 0;
    }
    int key;
    int objectTypeId;
    if (folderRecord.Owner == 0)
    {
      key = record.Catalog;
      objectTypeId = MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545");
    }
    else
    {
      key = this.GetFolderRecord(sourceDB, tableRec.TableName, folderRecord.Owner).Key;
      objectTypeId = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID);
    }
    string msgInfo;
    long objectByImbaseCode2 = CodeHandler.GetObjectByImbaseCode(session, objectTypeId, key, 0, out msgInfo);
    if (objectByImbaseCode2 == 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось создать папку код Imbase = '{folderRecord.Key}', в БД-приемнике не найден родительский объект в базе-приемнике по коду Imbase = '{key}'");
      return 0;
    }
    IDBObject dbObject1 = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID)).Create();
    dbObject1.OwnerID = CodeHandler.GetUserID(session, folderRecord.User);
    dbObject1.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) folderRecord.Name
    });
    IDBObject dbObject2 = session.GetObject(objectByImbaseCode2);
    string str = $"{dbObject2.NameInMessages} [{dbObject2.ObjectID}]";
    IDBAttribute byId = dbObject2.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545"));
    if (byId == null || byId.AsString == string.Empty)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось создать папку '{folderRecord.Name}' код Imbase = '{folderRecord.Key}', в БД-приемнике у родительского объекта {str} отсутствует значение у атрибута 'код Imbase'.");
      return 0;
    }
    dbObject1.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false, new object[1]
    {
      (object) folderRecord.Key
    });
    if (folderRecord.GraphID > 0)
    {
      try
      {
        long num = this.AddNewPicture(session, sourceDB, folderRecord.GraphID, 0L);
        if (num != 0L)
        {
          IDBAttribute dbAttribute = dbObject1.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false);
          if (dbAttribute != null)
            dbAttribute.Value = (object) num;
        }
      }
      catch (Exception ex)
      {
        this.AddEventInfo(EventType.Warning, $"При создании папки '{folderRecord.Name}' код Imbase = '{folderRecord.Key}' возникла ошибка добавления изображения: {ex.Message}");
      }
    }
    if (folderRecord.TextID > 0)
    {
      try
      {
        BlobRecord blobRecord = CodeHandler.CreateBlobRecord(sourceDB, folderRecord.TextID);
        if (blobRecord != null)
          dbObject1.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid), false).AsString = blobRecord.Memo;
      }
      catch (Exception ex)
      {
        this.AddEventInfo(EventType.Warning, $"При создании папки '{folderRecord.Name}' код Imbase = '{folderRecord.Key}' возникла ошибка добавления описания: {ex.Message}");
      }
    }
    session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545")).Create(objectByImbaseCode2, dbObject1.ObjectID).Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) folderRecord.Sort
    });
    dbObject1.CommitCreation(true);
    this.AddEventInfo(EventType.Text, $"Объект {$"{dbObject1.NameInMessages} [{dbObject1.ObjectID}]"} создан. {msgInfo}");
    return dbObject1.ObjectID;
  }

  private void Change(
    TableRecord tableRec,
    EventRecord eventRecord,
    IDataBase sourceDB,
    IUserSession session)
  {
    FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRec.TableName, eventRecord.Folder);
    if (folderRecord == null)
    {
      this.AddEventInfo(EventType.Warning, $"Невозможно изменить папку. Папка {eventRecord.Folder} в таблице {tableRec.TableName} базы-источника не найдена");
    }
    else
    {
      string msgInfo;
      long objectID = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out msgInfo);
      if (objectID == 0L)
      {
        objectID = this.AddFolder(tableRec, eventRecord, sourceDB, session);
        if (objectID == 0L)
        {
          this.AddEventInfo(EventType.Warning, $"Невозможно изменить папку. Папка с кодом Imbase {folderRecord.Key} в базе-приемнике не найдена");
          return;
        }
      }
      string recTableName = $"{tableRec.TableName}_REC";
      FieldRecord[] fields = this.GetFields(sourceDB, tableRec.Key);
      this.LinkAttributes(session, fields, sourceDB, eventRecord, tableRec.TableName, recTableName, folderRecord.Key);
      IArticleService service = ServiceUtils.GetService<IArticleService>((object) session, true);
      IDBObject newObject = session.GetObject(objectID);
      string str = $"{newObject.NameInMessages} [{newObject.ObjectID}]";
      this.AddAttributesToRecord(session, sourceDB, recTableName, eventRecord.ObjKey, fields, newObject, service, true);
      newObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false).AsString = folderRecord.Name;
      if (folderRecord.GraphID > 0)
      {
        IDBAttribute dbAttribute = newObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false);
        if (dbAttribute != null)
        {
          long asInteger = dbAttribute.AsInteger;
          try
          {
            long num = this.AddNewPicture(session, sourceDB, folderRecord.GraphID, asInteger);
            if (num != 0L)
              dbAttribute.Value = (object) num;
          }
          catch (Exception ex)
          {
            this.AddEventInfo(EventType.Warning, $"При изменении {str} возникла ошибка изменения изображения: {ex.Message}");
          }
        }
      }
      this.AddEventInfo(EventType.Text, $"Объект {str} изменен. {msgInfo}");
    }
  }

  private void ChangeImage(
    TableRecord tableRec,
    EventRecord record,
    IDataBase sourceDB,
    IUserSession session)
  {
    FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRec.TableName, record.Folder);
    if (folderRecord == null)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось изменить изображение папки. Папка {record.Folder} в таблице {tableRec.TableName} базы-источника не найдена");
    }
    else
    {
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out string _);
      if (objectByImbaseCode == 0L)
      {
        this.AddEventInfo(EventType.Warning, $"Не удалось изменить изображение папки. Папка с кодом Imbase {folderRecord.Key} в базе-приемнике не найдена");
      }
      else
      {
        long oldPictureObjectId = 0;
        IDBObject dbObject = session.GetObject(objectByImbaseCode);
        string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
        IDBAttribute attributeById = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID));
        if (attributeById != null)
          oldPictureObjectId = attributeById.AsInteger;
        if (folderRecord.GraphID == 0)
        {
          if (attributeById == null)
            return;
          try
          {
            attributeById.Delete(0L);
            this.AddEventInfo(EventType.Text, $"Для {str} атрибут 'Изображение' удален.");
          }
          catch (Exception ex1)
          {
            this.AddEventInfo(EventType.Warning, $"Ошибка при удалении атрибута 'Изображение' для {str}: {ex1.Message}");
            try
            {
              attributeById.Clear();
              this.AddEventInfo(EventType.Text, $"Для {str} атрибут 'Изображение' очищен.");
            }
            catch (Exception ex2)
            {
              this.AddEventInfo(EventType.Warning, $"Ошибка при очистке атрибута 'Изображение'  для {str}: {ex2.Message}");
            }
          }
        }
        else
        {
          try
          {
            long num = this.AddNewPicture(session, sourceDB, folderRecord.GraphID, oldPictureObjectId);
            if (num == 0L)
              return;
            IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false);
            if (dbAttribute == null)
              return;
            dbAttribute.Value = (object) num;
            this.AddEventInfo(EventType.Text, $"Для {str} атрибут 'Изображение' изменен.");
          }
          catch (Exception ex)
          {
            this.AddEventInfo(EventType.Warning, $"Ошибка при добавлении изображения для {str}: {ex.Message}");
          }
        }
      }
    }
  }

  private void ChangeNote(
    TableRecord tableRec,
    EventRecord record,
    IDataBase sourceDB,
    IUserSession session)
  {
    FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRec.TableName, record.Folder);
    if (folderRecord == null)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось изменить описание папки. Папка {record.Folder} в таблице {tableRec.TableName} базы-источника не найдена.");
    }
    else
    {
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out string _);
      if (objectByImbaseCode == 0L)
      {
        this.AddEventInfo(EventType.Warning, $"Не удалось изменить описание папки. Папка с кодом Imbase {folderRecord.Key} в базе-приемнике не найдена.");
      }
      else
      {
        IDBObject dbObject = session.GetObject(objectByImbaseCode);
        string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
        if (folderRecord.TextID == 0)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid));
          if (attributeById == null)
            return;
          attributeById.Delete(0L);
          this.AddEventInfo(EventType.Text, $"Описание {str} удалено.");
        }
        else
        {
          BlobRecord blobRecord = CodeHandler.CreateBlobRecord(sourceDB, folderRecord.TextID);
          if (blobRecord == null)
            return;
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid), false);
          if (dbAttribute == null)
            return;
          dbAttribute.AsString = blobRecord.Memo;
          this.AddEventInfo(EventType.Text, $"Описание {str} изменено.");
        }
      }
    }
  }

  private void ChangeVisibility(
    TableRecord tableRec,
    EventRecord record,
    IDataBase sourceDB,
    IUserSession session)
  {
    FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRec.TableName, record.Folder);
    if (folderRecord == null)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось изменить видимость папки. Папка {record.Folder} в таблице {tableRec.TableName} базы-источника не найдена.");
    }
    else
    {
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out string _);
      if (objectByImbaseCode == 0L)
      {
        this.AddEventInfo(EventType.Warning, $"Не удалось изменить видимость папки. Папка с кодом Imbase {folderRecord.Key} в базе-приемнике не найдена.");
      }
      else
      {
        IDBObject dbObject = session.GetObject(objectByImbaseCode);
        string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
        if (!CodeHandler.UpdateVisibleObjectState(dbObject, (ImFileAtt) folderRecord.Mask, true))
          return;
        this.AddEventInfo(EventType.Text, $"Изменена видимость {str}.");
      }
    }
  }

  private void Delete(EventRecord record, IUserSession session)
  {
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545"), record.Catalog, 0, out string _);
    if (objectByImbaseCode == 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Невозможно удалить папку. Каталог с кодом Imbase {record.Catalog} в базе-приемнике не найден");
    }
    else
    {
      IDBObject dbObject1 = session.GetObject(objectByImbaseCode);
      IDBAttribute byId = dbObject1.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545"));
      if (byId == null || !(byId.AsString != string.Empty))
        return;
      DataTable dataTable = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID)).Select(new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) record.Text, LogicalOperators.AND, 0, false),
        new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545"), RelationalOperators.StartString, (object) byId.AsString, LogicalOperators.AND, 0, false)
      }, new object[1]{ (object) -2 }));
      if (dataTable.Rows.Count == 0)
      {
        this.AddEventInfo(EventType.Warning, $"Невозможно удалить папку. Папка '{record.Text}' в каталоге '{dbObject1.Caption}' базы-приемника не найдена.");
      }
      else
      {
        IDBObject dbObject2 = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
        string str = $"{dbObject2.NameInMessages} [{dbObject2.ObjectID}]";
        dbObject2.Delete(0L);
        this.AddEventInfo(EventType.Text, $"Удален объект {str}.");
      }
    }
  }

  private void Move(
    TableRecord tableRec,
    EventRecord record,
    IDataBase sourceDB,
    IUserSession session)
  {
    bool flag = false;
    IDBTransactions customService = (IDBTransactions) session.GetCustomService(typeof (IDBTransactions));
    customService.StartTransaction();
    try
    {
      FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRec.TableName, record.Folder);
      if (folderRecord == null)
      {
        this.AddEventInfo(EventType.Warning, $"Не удалось переместить папку. Исходная папка {record.Folder} в таблице {tableRec.TableName} базы-источника не найдена");
        flag = true;
      }
      else
      {
        string msgInfo;
        long objectByImbaseCode1 = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out msgInfo);
        if (objectByImbaseCode1 == 0L)
        {
          flag = true;
          this.AddEventInfo(EventType.Warning, $"Не удалось переместить папку. Исходная папка с кодом Imbase {folderRecord.Key} в базе-приемнике не найдена");
        }
        else
        {
          IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545"));
          relationCollection.LocalTypesMode = true;
          IDBObject thisObject = session.GetObject(objectByImbaseCode1);
          string str = $"{thisObject.NameInMessages} [{thisObject.ObjectID}]";
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(-22, RelationalOperators.Equal, (object) thisObject.ID, LogicalOperators.AND, 0, false)
          }, new object[1]{ (object) -20 });
          IDBRelation dbRelation = (IDBRelation) null;
          DataTable dataTable = relationCollection.Select(paramSet);
          if (dataTable.Rows.Count > 0)
            dbRelation = session.GetRelation(Convert.ToInt64(dataTable.Rows[0][0]), false);
          int key;
          int objectTypeId;
          if (folderRecord.Owner == 0)
          {
            key = record.Catalog;
            objectTypeId = MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545");
          }
          else
          {
            key = this.GetFolderRecord(sourceDB, tableRec.TableName, folderRecord.Owner).Key;
            objectTypeId = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID);
          }
          long objectByImbaseCode2 = CodeHandler.GetObjectByImbaseCode(session, objectTypeId, key, 0, out string _);
          if (objectByImbaseCode2 == 0L)
          {
            flag = true;
            this.AddEventInfo(EventType.Warning, $"Невозможно переместить папку {str}, в БД-приемнике не найден родительский объект по значению атрибута {MetaDataHelper.GetAttributeTypeName(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID)} = {key}");
          }
          else
          {
            if (dbRelation != null)
              dbRelation.ProjID = objectByImbaseCode2;
            else
              relationCollection.Create(objectByImbaseCode2, thisObject.ObjectID).Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), false, new object[1]
              {
                (object) folderRecord.Sort
              });
            IDBObject parentObject = session.GetObject(objectByImbaseCode2);
            CodeHandler.CreateNewClassifCode(session, thisObject, parentObject);
            this.AddEventInfo(EventType.Text, $"Объект '{thisObject.NameInMessages}' перемещен в '{str}'. {msgInfo}");
          }
        }
      }
    }
    catch (Exception ex)
    {
      flag = true;
      throw;
    }
    finally
    {
      if (flag)
        customService.Rollback();
      else
        customService.Commit();
    }
  }

  private void Rename(
    TableRecord tableRec,
    EventRecord record,
    IDataBase sourceDB,
    IUserSession session)
  {
    FolderRecord folderRecord = this.GetFolderRecord(sourceDB, tableRec.TableName, record.Folder);
    if (folderRecord == null)
    {
      this.AddEventInfo(EventType.Warning, $"Невозможно переименовать папку. Папка {record.Folder} в таблице {tableRec.TableName} базы-источника не найдена");
    }
    else
    {
      string msgInfo;
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID), folderRecord.Key, 0, out msgInfo);
      if (objectByImbaseCode == 0L)
      {
        this.AddEventInfo(EventType.Warning, $"Невозможно переименовать папку. Папка с кодом Imbase {folderRecord.Key} в базе-приемнике не найдена");
      }
      else
      {
        IDBObject dbObject = session.GetObject(objectByImbaseCode);
        string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
        dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545")).AsString = folderRecord.Name;
        this.AddEventInfo(EventType.Text, $"Объект '{str}' переименован в '{folderRecord.Name}'. {msgInfo}");
      }
    }
  }
}
