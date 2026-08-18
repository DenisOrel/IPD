// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.CatalogCodeHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Imbase.Server.Sync.Records;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class CatalogCodeHandler(Guid taskGuid) : CodeHandler(taskGuid)
{
  public override void Handle(EventRecord record, IDataBase sourceDB, IUserSession session)
  {
    TableRecord tableRec = (TableRecord) null;
    if (record.Code != 102)
      tableRec = CodeHandler.GetTableRecord(sourceDB, record.Catalog);
    if (tableRec == null && record.Code != 102)
    {
      this.AddEventInfo(EventType.Warning, $"Каталог {record.Catalog} в таблице IM_TABLES базы-источника не найден");
    }
    else
    {
      switch (record.Code)
      {
        case 100:
          this.Add(tableRec, sourceDB, session);
          break;
        case 102:
          this.Delete(tableRec, session);
          break;
        case 105:
          this.Rename(tableRec, session);
          break;
        case 106:
          this.ChangeType(tableRec, session);
          break;
      }
    }
  }

  private void ChangeType(TableRecord tableRec, IUserSession session)
  {
    string msgInfo;
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545"), tableRec.Key, 0, out msgInfo);
    if (objectByImbaseCode == 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось изменить тип каталога. Не найден каталог '{tableRec.Description}' в базе-приемнике по коду Imbase = '{tableRec.Key}'. {msgInfo}");
    }
    else
    {
      IDBObject dbObject = session.GetObject(objectByImbaseCode);
      IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CatalogTypeAttGUID), false);
      if (dbAttribute == null)
        return;
      string str1 = string.Empty;
      switch (tableRec.TableType)
      {
        case ImTablesType.IMTT_CATALOG:
          str1 = "Каталоги";
          break;
        case ImTablesType.IMTT_CTLREF:
          str1 = "Справочники";
          break;
        case ImTablesType.IMTT_TECHREF:
          str1 = "Технологические справочники";
          break;
      }
      string asString = dbAttribute.AsString;
      string str2 = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
      dbAttribute.AsString = str1;
      this.AddEventInfo(EventType.Text, $"Для {str2} изменен тип каталога: с '{asString}' на '{str1}'. {msgInfo}");
    }
  }

  private void Rename(TableRecord tableRec, IUserSession session)
  {
    string msgInfo;
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545"), tableRec.Key, 0, out msgInfo);
    if (objectByImbaseCode == 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось переименовать каталог. Не найден каталог '{tableRec.Description}' в базе-приемнике по коду Imbase = '{tableRec.Key}'. {msgInfo}");
    }
    else
    {
      IDBObject dbObject = session.GetObject(objectByImbaseCode);
      IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false);
      if (dbAttribute == null)
        return;
      string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
      if (string.Equals(dbAttribute.AsString, tableRec.Description))
        return;
      dbAttribute.AsString = tableRec.Description;
      this.AddEventInfo(EventType.Text, $"{str} переименован в '{tableRec.Description}'. {msgInfo}");
    }
  }

  private void Delete(TableRecord tableRec, IUserSession session)
  {
    string msgInfo;
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545"), tableRec.Key, 0, out msgInfo);
    if (objectByImbaseCode == 0L)
    {
      this.AddEventInfo(EventType.Warning, $"Не удалось удалить каталог. Не найден каталог '{tableRec.Description}' в базе-приемнике по коду Imbase = '{tableRec.Key}'. {msgInfo}");
    }
    else
    {
      IDBObject dbObject = session.GetObject(objectByImbaseCode);
      string str = $"{dbObject.NameInMessages} [{dbObject.ObjectID}]";
      dbObject.Delete(0L);
      this.AddEventInfo(EventType.Text, $"Каталог удален: '{str}'. {msgInfo}");
    }
  }

  private void Add(TableRecord tableRec, IDataBase sourceDB, IUserSession session)
  {
    IDBObject dbObject = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545")).Create();
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) tableRec.Description
    });
    string str = string.Empty;
    switch (tableRec.TableType)
    {
      case ImTablesType.IMTT_CATALOG:
        str = "Каталоги";
        break;
      case ImTablesType.IMTT_CTLREF:
        str = "Справочники";
        break;
      case ImTablesType.IMTT_TECHREF:
        str = "Технологические справочники";
        break;
    }
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CatalogTypeAttGUID), false, new object[1]
    {
      (object) str
    });
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false, new object[1]
    {
      (object) tableRec.Key
    });
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttGUID), false, new object[1]
    {
      (object) tableRec.TableName
    });
    dbObject.OwnerID = CodeHandler.GetUserID(session, tableRec.User);
    if (tableRec.TextID > 0)
    {
      BlobRecord blobRecord = CodeHandler.CreateBlobRecord(sourceDB, tableRec.TextID);
      if (blobRecord != null)
        dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseNoteAttGuid), false).AsString = blobRecord.Memo;
    }
    if (tableRec.GraphID > 0)
    {
      try
      {
        long num = this.AddNewPicture(session, sourceDB, tableRec.GraphID, 0L);
        if (num != 0L)
          dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.PictureAttGUID), false, new object[1]
          {
            (object) num
          });
      }
      catch (Exception ex)
      {
        this.AddEventInfo(EventType.Warning, $"Ошибка при добавлении изображения для каталога {tableRec.Description} (код Imbase = {tableRec.Key}) : {ex.Message}");
      }
    }
    dbObject.CommitCreation(true);
    this.AddEventInfo(EventType.Text, $"Каталог создан: '{dbObject.NameInMessages}'.");
  }
}
