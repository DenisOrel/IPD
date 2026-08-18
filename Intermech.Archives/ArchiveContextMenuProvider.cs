// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchiveContextMenuProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Позволяет реализовать провайдер команд для контекстного меню навигатора.
/// Провайдер должен проанализировать информацию о контексте, в котором будет показано
/// меню, и вернуть контейнер со сведениями о допустимых командах.
/// </summary>
internal class ArchiveContextMenuProvider : ICommandsProvider
{
  /// <summary>
  /// Метод вызывается для получения допустимых и подавляемых команд контекстного меню для
  /// выделенных элементов навигации одной категории и типа.
  /// Например, если в «Навигаторе» выделены элементы навигации нескольких разных категорий и типов,
  /// то данная команда будет вызываться для каждой из подгрупп этих элементов, сгруппированных
  /// по их категориям и типам. Наиболее применяемый метод даного интерфейса.
  /// Позволяет перекрывать команды контекстного меню для элементов навигации определённых категорий,
  /// типов, задавая более высокий приоритет описаниям этих команд.
  /// ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных  для того,
  /// чтобы проверить, можно ли отображать команду меню или нет!
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// Метод вызывается для получения допустимых и подавляемых команд контекстного меню для всей группы выделенных
  /// элементов навигации. Особенности данного метода:
  /// 1. Если команда зарегистрирована на все категории, то метод вызывается один раз и получает в качестве параметра
  /// items все выделенные в «Навигаторе» элементы навигации;
  /// 2. Если команда зарегистрирована на конкретную категорию, то метод будет вызван один раз для всех выделенных
  /// элементов навигации только в том случае, если все они принадлежат одной категории; для всех выделенных
  /// элементов навигации только в том случае, если все они принадлежат указанной категории;
  /// 3. Если команда зарегистрирована на конкретные категорию и тип, то метод будет вызван один раз для всех
  /// выделенных элементов навигации только в том случае, если все они принадлежат указанной категории и типу.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if ((((IViewState) viewServices.GetService(typeof (IViewState))).ViewState & ViewStateFlags.ReadOnly) != ViewStateFlags.None || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Suppress("CreateNew", 0);
    groupCommands.Suppress("CreateProto", 0);
    groupCommands.Suppress("CreateVersion", 0);
    groupCommands.Suppress("CreateInclude", 0);
    groupCommands.Add("CreateDocum", new CommandInfo(0, new ClickEventHandler(ArchiveContextMenuProvider.CreateDocum)));
    groupCommands.Add("CreateArchive", new CommandInfo(0, new ClickEventHandler(this.CreateArchive)));
    groupCommands.Add("CreateArchiveProto", new CommandInfo(0, new ClickEventHandler(this.CreateArchiveProto)));
    groupCommands.Add("AddDocum", new CommandInfo(0, new ClickEventHandler(this.AddCommand)));
    groupCommands.Add("Register", new CommandInfo(0, new ClickEventHandler(this.RegisterCommand)));
    groupCommands.Add("Cut", new CommandInfo(0, new ClickEventHandler(this.CutCommand)));
    if (items.Count == 1 && (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
    {
      groupCommands.Add("CheckFileStorage", new CommandInfo(0, new ClickEventHandler(this.CheckFileStorageCommand)));
      groupCommands.Add("RemoveFilesToStorage", new CommandInfo(0, new ClickEventHandler(this.RemoveToStorageCommand)));
    }
    IClipboard service = (IClipboard) ServicesManager.GetService(typeof (IClipboard));
    if (service != null && service.GetDataObject() is IDBObjectTypedIDCollection dataObject && dataObject.Count > 0)
      groupCommands.Add("Paste", new CommandInfo(0, new ClickEventHandler(this.PasteCommand)));
    return groupCommands;
  }

  /// <summary>
  /// Команда проверки расположения двоичных данных в файловом шкафу, который указан в архиве (по ТЗ ОКБМ N1415418)
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void CheckFileStorageCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count <= 0)
      return;
    long num1 = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (Convert.ToInt32(sessionKeeper.Session.ObjectsSelect(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(SystemGUIDs.attributeArchive, RelationalOperators.Equal, (object) num1, LogicalOperators.NONE, 0)
      })
      {
        RecordCount = 0
      }).Rows[0][0]) > 100 && MessageBox.Show("Эта операция может занять длительное время. Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      int num2 = (int) MessageBox.Show((sessionKeeper.Session.GetCustomService(typeof (IArchiveService)) as IArchiveService).ValidateDocsStorageID(num1, sessionKeeper.Session.SessionGUID), "IPS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  /// <summary>
  /// Команда перемещения двоичных данных в файловый шкаф, который указан в архиве (по ТЗ Газпром нефти N1586610)
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void RemoveToStorageCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count <= 0)
      return;
    long num1 = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (MessageBox.Show("Эта операция может занять длительное время. Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      int num2 = (sessionKeeper.Session.GetCustomService(typeof (IArchiveService)) as IArchiveService).RemoveDocs2ArcStorage(num1, sessionKeeper.Session.SessionGUID);
      string text;
      if (num2 == 0)
      {
        text = "Все документы уже размещены в указанном для архива файловом шкафу.";
      }
      else
      {
        IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(num1).GetAttributeByGuid(new Guid("cad0005c-306c-11d8-b4e9-00304f19f545"));
        string str = attributeByGuid != null ? attributeByGuid.AsString : string.Empty;
        text = string.Format("В файловый шкаф '{1}' перенесено {0} файл(ов)", (object) num2, (object) str);
      }
      int num3 = (int) MessageBox.Show(text, "IPS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void AddCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long num = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(ConstsHolder.DocTypeID);
      if (!(SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_38"), rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
        return;
      foreach (IDBObjectID dbObjectId in dbObjectIdArray)
        sessionKeeper.Session.SetObjectAttributesValues(dbObjectId.Value, false, new AttributeValues[1]
        {
          new AttributeValues(ConstsHolder.ArchiveAttrID, (object) new object[1]
          {
            (object) num
          })
        });
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, new NotificationEventArgs("ArchiveChanged"));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void CreateNewCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    long objectId = itemData.ObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
        return;
      long objectByTypeDialog = service.CreateObjectByTypeDialog(itemData.ObjectType);
      if (objectByTypeDialog == -1L)
        return;
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectByTypeDialog);
      if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(ConstsHolder.DocTypeID).Contains(dbObject.ObjectType))
      {
        AttributeValues attributeValues = new AttributeValues(ConstsHolder.ArchiveAttrID, (object) new object[1]
        {
          (object) objectId
        });
        dbObject.SetAttributesValues(new AttributeValues[1]
        {
          attributeValues
        }, false, true);
      }
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, new NotificationEventArgs("ArchiveChanged"));
    }
  }

  /// <summary>
  /// Создать документ в архиве.
  /// Выбор типа документа происходит с учетом настроек пользователя и настроек разрешенных типов архива
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void CreateDocum(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service) || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    using (SessionKeeper sk = new SessionKeeper())
    {
      IDBObject archiveObject = sk.Session.GetObject(itemData.ObjectID);
      if (archiveObject == null)
        return;
      List<int> typesIdsForArchive = ArchiveContextMenuProvider.GetEnabledTypesIDsForArchive(sk, archiveObject);
      if (service.CreateObjectByTypeDialog(typesIdsForArchive.ToArray()) == -1L)
        return;
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, new NotificationEventArgs("ArchiveChanged"));
    }
  }

  /// <summary>
  /// Получает доступные для архива типы документов с учетом настроек предметных областей и настроек разрешенных типов архива.
  /// </summary>
  /// <param name="sk">Сессия</param>
  /// <param name="archiveObject">Архив</param>
  /// <returns>Доступные для архива типы документов с учетом настроек предметных областей и настроек разрешенных типов архива.</returns>
  private static List<int> GetEnabledTypesIDsForArchive(SessionKeeper sk, IDBObject archiveObject)
  {
    List<int> typesIdsForArchive = new List<int>();
    DataTable dataTable = sk.Session.GetObjectTypeCollection(ConstsHolder.DocTypeID, true).SelectRecursive(string.Empty);
    List<int> collection = new List<int>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      collection.Add(Convert.ToInt32(dataTable.Rows[index]["F_OBJECT_TYPE"]));
    IDBAttribute attributeById1 = archiveObject.GetAttributeByID(ConstsHolder.ArchiveTypesUsingModeID);
    IDBAttribute attributeById2 = archiveObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00149-306c-11d8-b4e9-00304f19f545"));
    switch ((int) attributeById1.AsInteger)
    {
      case 0:
        typesIdsForArchive.AddRange((IEnumerable<int>) collection);
        break;
      case 1:
        if (attributeById2 != null)
        {
          using (List<string>.Enumerator enumerator = ((IEnumerable<string>) attributeById2.Descriptions).ToList<string>().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              string current = enumerator.Current;
              if (!(current == string.Empty))
              {
                foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(current)))
                {
                  if (collection.Contains(num))
                    typesIdsForArchive.Add(num);
                }
              }
            }
            break;
          }
        }
        break;
      case 2:
        typesIdsForArchive.AddRange((IEnumerable<int>) collection);
        using (List<int>.Enumerator enumerator = collection.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            int current = enumerator.Current;
            if (attributeById2 != null && ((IEnumerable<string>) attributeById2.Descriptions).ToList<string>().Contains(MetaDataHelper.GetObjectTypeGuid(current).ToString()))
            {
              foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(current))
                typesIdsForArchive.Remove(num);
              typesIdsForArchive.Remove(current);
            }
          }
          break;
        }
    }
    return typesIdsForArchive;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void CreateArchive(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    long objectId = itemData.ObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service1))
        return;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ConstsHolder.ArcTypeID);
      long objectByTypeDialog = service1.CreateObjectByTypeDialog(childrenIdRecursive.ToArray());
      if (objectByTypeDialog == -1L)
        return;
      IDBRelation dbRelation = sessionKeeper.Session.GetRelationCollection(ConstsHolder.RelTypeSimpleId).Create(objectId, objectByTypeDialog);
      if (ApplicationServices.Container.GetService(typeof (ArchiveHierarchyService)) is ArchiveHierarchyService service2)
        service2.AddArchiveToCashe(objectByTypeDialog, objectId, itemData.ObjectType);
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, dbRelation.ProjID, dbRelation.RelationType));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void CreateArchiveProto(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    long objectId = itemData.ObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
        return;
      long byTemplateDialog = service.CreateObjectByTemplateDialog(objectId);
      if (byTemplateDialog == -1L)
        return;
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
      relationCollection.ObjectTypeID = ConstsHolder.ArcTypeID;
      DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      })
      {
        RecordCount = 1
      }, objectId);
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
        IDBRelation dbRelation = relationCollection.Create(int64, byTemplateDialog);
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBRelationsManagedEventArgs("ManagedRelationsCreated", dbRelation.RelationID, true));
      }
      else
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", byTemplateDialog));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void CutCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ObjectCommands.CutCommand(items, viewServices, additionalInfo);
  }

  /// <summary>вставить объект</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void PasteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service1))
      return;
    IDBObjectTypedIDCollection dataObject = service1.GetDataObject() as IDBObjectTypedIDCollection;
    INotificationService service2 = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (dataObject != null && dataObject.Count > 0)
    {
      string format;
      if (dataObject.Count >= 10 && dataObject.Count <= 20)
      {
        format = ServiceHolder.rm.GetString("Archives_78");
      }
      else
      {
        switch (dataObject.Count % 10)
        {
          case 1:
            format = dataObject.Count != 1 ? ServiceHolder.rm.GetString("Archives_81") : ServiceHolder.rm.GetString("Archives_80");
            break;
          case 2:
          case 3:
          case 4:
            format = ServiceHolder.rm.GetString("Archives_79");
            break;
          default:
            format = ServiceHolder.rm.GetString("Archives_78");
            break;
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
        if (MessageBox.Show(string.Format(format, (object) dataObject.Count, (object) dbObject.NameInMessages), ServiceHolder.rm.GetString("Archives_49"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
        {
          for (int index = 0; index < dataObject.Count; ++index)
          {
            IDBTypedObjectID typedObjectId = dataObject.GetTypedObjectID(index);
            if (MetaDataHelper.IsObjectTypeChildOf(typedObjectId.ObjectType, ConstsHolder.DocTypeID))
            {
              sessionKeeper.Session.SetObjectAttributesValues(typedObjectId.ObjectID, false, new AttributeValues[1]
              {
                new AttributeValues(ConstsHolder.ArchiveAttrID, (object) new object[1]
                {
                  (object) objectID
                })
              });
            }
            else
            {
              if (!MetaDataHelper.IsObjectTypeChildOf(typedObjectId.ObjectType, ConstsHolder.ArcTypeID))
                throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_83"), (object) MetaDataHelper.GetObjectName(typedObjectId.ObjectType), (object) MetaDataHelper.GetObjectName(itemData.ObjectType)));
              ClipboardObject clipboardObject = typedObjectId as ClipboardObject;
              if (sessionKeeper.Session.GetRelation(clipboardObject.Value, false) != null)
              {
                ObjectCommands.DoInsertIntoObject(items.GetParentPath(0), itemData, new IDBTypedObjectID[1]
                {
                  typedObjectId
                }, (IDBRelationID[]) null, (Hashtable) null, true, viewServices, NavigatorRelationCommand.CutPaste);
              }
              else
              {
                List<long> relationIDs = new List<long>();
                List<long> projIDs = new List<long>();
                List<int> relTypeIDs = new List<int>();
                sessionKeeper.Session.StartLogHistory();
                try
                {
                  DataTable applicabilitiesList = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, typedObjectId.ObjectType, itemData.ObjectType);
                  if (applicabilitiesList.Rows.Count > 0)
                  {
                    int int32 = Convert.ToInt32(applicabilitiesList.Rows[0]["F_RELATION_TYPE"]);
                    IDBRelation dbRelation = sessionKeeper.Session.GetRelationCollection(int32).Create(itemData.ObjectID, typedObjectId.ObjectID);
                    relationIDs.Add(dbRelation.RelationID);
                    projIDs.Add(dbRelation.ProjID);
                    relTypeIDs.Add(dbRelation.RelationType);
                  }
                }
                finally
                {
                  (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).RefreshImage();
                  service2.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", typedObjectId.ObjectID));
                  if (relationIDs.Count > 0)
                  {
                    if (UISettings.DragDropNotofications)
                    {
                      DBRelationsManagedEventArgs e = new DBRelationsManagedEventArgs("ManagedRelationsCreated", (IList<long>) relationIDs, true);
                      service2.FireEvent((object) null, (NotificationEventArgs) e);
                    }
                    else
                    {
                      DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs);
                      service2.FireEvent((object) null, (NotificationEventArgs) e);
                    }
                  }
                }
              }
              if (ApplicationServices.Container.GetService(typeof (ArchiveHierarchyService)) is ArchiveHierarchyService service3)
                service3.AddArchiveToCashe(clipboardObject.PartID, itemData.ObjectID, itemData.ObjectType);
            }
          }
        }
      }
    }
    service1.RemoveCurrentDataObject();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void RegisterCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBObjectID archiveItem = (IDBObjectID) items.GetItemData(0, typeof (IDBObjectID));
    IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    if (service == null)
      return;
    ClientContext.FileImporter.BatchImport(ServiceHolder.rm.GetString("Archives_84"), service.WorkArea.AreaPath, (Action<long>) (importedObject => this.AddImportedObjectToArchive(importedObject, archiveItem.Value)));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="importedObject"></param>
  /// <param name="archiveId"></param>
  private void AddImportedObjectToArchive(long importedObject, long archiveId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.SetObjectAttributesValues(importedObject, true, new AttributeValues[1]
      {
        new AttributeValues(ConstsHolder.ArchiveAttrID, (object) new object[1]
        {
          (object) archiveId
        })
      });
  }
}
