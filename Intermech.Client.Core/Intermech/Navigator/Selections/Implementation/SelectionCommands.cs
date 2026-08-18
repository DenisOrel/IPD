
// Type: Intermech.Navigator.Selections.Implementation.SelectionCommands
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Interfaces;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Класс реализации обработчиков контекстных команд для выборок
/// </summary>
internal static class SelectionCommands
{
  /// <summary>Является ли указанный тип объекта выборкой</summary>
  /// <param name="ObjectTypeID">Проверяемый тип объекта</param>
  /// <returns>true, если проверяемый тип объекта является выборкой</returns>
  public static bool IsSelection(int objectTypeID)
  {
    return MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00156-306c-11d8-b4e9-00304f19f545")).Exists((Predicate<int>) (_ => _.Equals(objectTypeID)));
  }

  /// <summary>
  /// Проверка - является ли тип объекта классификатором (папкой классификатора)
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
  /// <returns>результат проверки: true - классификатор, false - не классификатор</returns>
  public static bool IsClassifier(int objectTypeID)
  {
    return MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")).Exists((Predicate<int>) (_ => _.Equals(objectTypeID)));
  }

  /// <summary>
  /// Проверка - является ли тип объекта классификатором, но не папкой классификатора
  /// </summary>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <returns>результат проверки: true - классификатор, false - не классификатор</returns>
  public static bool IsClassifierExcludeFolder(int objectTypeID)
  {
    return MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad0014e-306c-11d8-b4e9-00304f19f545")).Exists((Predicate<int>) (_ => _.Equals(objectTypeID))) || MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad0014f-306c-11d8-b4e9-00304f19f545")).Exists((Predicate<int>) (_ => _.Equals(objectTypeID)));
  }

  /// <summary>
  /// Проверка - является ли тип объекта папкой классификатора
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
  /// <returns>результат проверки: true - папка классификатора, false - не папка классификатора</returns>
  public static bool IsClassifierFolder(int objectTypeID)
  {
    return MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00150-306c-11d8-b4e9-00304f19f545")).Exists((Predicate<int>) (_ => _.Equals(objectTypeID)));
  }

  /// <summary>
  /// Функция для проверки является ли данный идентификатор типа объекта типом выборки или классификатора
  /// </summary>
  public static bool IsSelectionOrClassifier(int objectTypeID)
  {
    return MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00119-306c-11d8-b4e9-00304f19f545")).Exists((Predicate<int>) (_ => _.Equals(objectTypeID)));
  }

  /// <summary>
  /// Получить строку вида " объект", " объекта", " объектов" в зависимости от количества объектов
  /// </summary>
  /// <param name="Num"></param>
  /// <returns></returns>
  public static string GetObjectsString(int Num)
  {
    string str1 = LocalizationHolder.rm.GetString("Client.Core_406");
    string str2 = LocalizationHolder.rm.GetString("Client.Core_407");
    string str3 = LocalizationHolder.rm.GetString("Client.Core_408");
    return $"{(object) Convert.ToUInt32(Num)} {(Num > 10 && Num < 19 || Num % 10 == 0 || Num % 10 > 4 ? (object) str1 : (Num % 10 == 1 ? (object) str2 : (object) str3))}";
  }

  /// <summary>Команда "Исключить"</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void ExcludeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(ServicesManager.GetService(typeof (ISelectionsService)) is ISelectionsService service) || !(items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData))
      return;
    bool flag1 = false;
    bool flag2 = SelectionCommands.IsSelection(parentData.ObjectType);
    SelectionCommands.IsClassifier(parentData.ObjectType);
    bool flag3 = SelectionCommands.IsClassifierFolder(parentData.ObjectType);
    if (MessageBox.Show(flag2 ? LocalizationHolder.rm.GetString("Client.Core_1194") : (flag3 ? LocalizationHolder.rm.GetString("Client.Core_1196") : LocalizationHolder.rm.GetString("Client.Core_1195")), LocalizationHolder.rm.GetString(sc_4368.ssp_imclient_4369()), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    if (flag2)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(parentData.ObjectID, false);
        if (dbObject != null)
        {
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cadd99b3-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            if (attributeByGuid.AsBoolean)
              goto label_13;
          }
          flag1 = true;
        }
      }
    }
label_13:
    List<long> longList = new List<long>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
      {
        if (flag1)
          longList.Add(itemData.ID);
        else
          longList.Add(itemData.Value);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (flag1)
        service.ExcludeObjectsByID((object) sessionKeeper.Session, parentData.ObjectID, longList.ToArray());
      else
        service.ExcludeObjects((object) sessionKeeper.Session, parentData.ObjectID, longList.ToArray());
      DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", parentData.ObjectID);
      Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    }
  }

  private static void InternalPaste(
    IUserSession session,
    IDBObject idboTargetObject,
    List<IDBTypedObjectID> objectTypedIDs,
    bool isCut,
    IDBTypedObjectID parent)
  {
    int objectType = idboTargetObject.ObjectType;
    SelectionType selectionType = SelectionType.None;
    bool flag1 = SelectionCommands.IsSelection(idboTargetObject.ObjectType);
    bool flag2 = false;
    if (flag1)
    {
      IDBAttribute attributeByGuid = idboTargetObject.GetAttributeByGuid(new Guid("cad00155-306c-11d8-b4e9-00304f19f545"));
      flag2 = attributeByGuid != null && attributeByGuid.AsBoolean;
      if (flag2)
        selectionType = (SelectionType) idboTargetObject.GetAttributeByGuid(new Guid("cad00158-306c-11d8-b4e9-00304f19f545")).AsInteger;
    }
    ISelectionsService service = ServicesManager.GetService(typeof (ISelectionsService)) as ISelectionsService;
    List<long> objectIDs1 = new List<long>();
    List<string> preparePasteErrors = new List<string>(1);
    List<int> intList = SelectionCommands.EnableTypes4Paste(idboTargetObject);
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
    MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    IDBRelationCollection relationCollection1 = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID);
    using (RemoteLock remoteLock = new RemoteLock())
    {
      remoteLock.Add((object) idboTargetObject);
      foreach (IDBTypedObjectID objectTypedId in objectTypedIDs)
      {
        if (SelectionCommands.IsClassifier(objectType) && SelectionCommands.IsClassifierFolder(objectTypedId.ObjectType) || SelectionCommands.IsSelection(objectType) && SelectionCommands.IsSelection(objectTypedId.ObjectType))
        {
          if (idboTargetObject.ObjectID == objectTypedId.ObjectID)
          {
            int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_685"), SelectionCommands.IsSelection(objectTypedId.ObjectType) ? (object) LocalizationHolder.rm.GetString("Client.Core_686") : (object) LocalizationHolder.rm.GetString("Client.Core_687")), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
          else
          {
            ISelectionsService customService = session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
            Guid guid = new Guid("cad0002e-306c-11d8-b4e9-00304f19f545");
            IDBRelation dbRelation1 = (IDBRelation) null;
            if (isCut && objectTypedId is IDBRelationID dbRelationId)
              dbRelation1 = session.GetRelation(dbRelationId.Value);
            int defaultRelationTypeId = MetaDataHelper.GetDefaultRelationTypeID(objectType);
            IDBRelationCollection relationCollection2 = session.GetRelationCollection(defaultRelationTypeId);
            if (!isCut && SelectionCommands.IsClassifierFolder(objectTypedId.ObjectType))
            {
              IDBObject dbObject = session.GetObjectCollection(objectTypedId.ObjectType).Create(objectTypedId.ObjectID);
              IDBRelation dbRelation2 = relationCollection2.Create(idboTargetObject.ObjectID, dbObject.ObjectID);
              dbObject.CommitCreation(true);
              Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", dbObject.ObjectID));
              CompositionCopierTask.BeginCreate(dbObject.ObjectID, objectTypedId.ObjectID);
              customService.UpdateCashe((object) session.SessionGUID, dbObject.ObjectID);
              if (dbRelation2 != null)
                Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation2.RelationID, dbRelation2.ProjID, objectType, dbRelation2.RelationType));
            }
            else if (!SelectionCommands.IsClassifier(objectTypedId.ObjectType))
            {
              IDBRelation dbRelation3 = relationCollection2.Create(idboTargetObject.ObjectID, objectTypedId.ObjectID);
              if (((dbRelation3 == null ? 0 : (dbRelation1 != null ? 1 : 0)) & (isCut ? 1 : 0)) != 0)
              {
                DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsRemoved", dbRelation1.RelationID, dbRelation1.ProjID, dbRelation1.RelationType);
                dbRelation1.Delete(0L);
                Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
              }
              else if (dbRelation3 == null)
              {
                int num2 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_688"), (object) idboTargetObject.Caption, (object) objectTypedId.Caption));
              }
              if (dbRelation3 != null)
                Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation3.RelationID, dbRelation3.ProjID, objectType, dbRelation3.RelationType));
            }
          }
        }
        else if (service != null && SelectionCommands.IsSelectionOrClassifier(objectType) && !SelectionCommands.IsSelection(objectTypedId.ObjectType))
        {
          bool flag3 = true;
          bool flag4 = false;
          if (flag1)
          {
            if (flag2)
            {
              int num3;
              if (childrenIdRecursive.Contains(objectTypedId.ObjectType) && session.Configurations.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.DocumentsIncludeIntoHandleSelectionParamName, false, DBConfigMode.GlobalOnly))
              {
                switch (selectionType)
                {
                  case SelectionType.Archiv:
                  case SelectionType.Archives:
                    num3 = 1;
                    goto label_31;
                  case SelectionType.ObjectType:
                    if (intList != null)
                    {
                      num3 = intList.Count > 0 ? 1 : 0;
                      goto label_31;
                    }
                    break;
                }
                num3 = 0;
              }
              else
                num3 = 0;
label_31:
              flag4 = num3 != 0;
              if (intList != null && !intList.Contains(objectTypedId.ObjectType))
              {
                if (flag4)
                  preparePasteErrors.Add(string.Format(LocalizationHolder.rm.GetString("Client.Core_689"), (object) MetaDataHelper.GetObjectTypeName(objectTypedId.ObjectType)));
                flag3 = false;
              }
            }
            else
            {
              preparePasteErrors.Add(string.Format(LocalizationHolder.rm.GetString("Client.Core_690"), (object) session.GetObject(objectTypedId.ObjectID).NameInMessages));
              flag3 = false;
            }
          }
          if (SelectionCommands.IsClassifier(objectTypedId.ObjectType) && SelectionCommands.IsClassifier(objectType))
          {
            int num4 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1178"), (object) MetaDataHelper.GetObjectTypeName(objectTypedId.ObjectType)), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            flag3 = false;
          }
          if (flag3)
            objectIDs1.Add(objectTypedId.ObjectID);
          if (flag4)
          {
            ConditionStructure[] conditions = (ConditionStructure[]) null;
            if (selectionType == SelectionType.ObjectType && intList != null && intList.Count > 0)
              conditions = new ConditionStructure[1]
              {
                new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.AND, 0, false)
              };
            foreach (DataRow row in (InternalDataCollectionBase) relationCollection1.ConsistFrom(new DBRecordSetParams(conditions, new object[1]
            {
              (object) -2
            }), objectTypedId.ObjectID).Rows)
              objectIDs1.Add(Convert.ToInt64(row[0]));
          }
        }
      }
      if (preparePasteErrors.Count == 0)
      {
        long[] numArray = service.ExistsObjectsID((object) session, idboTargetObject.ObjectID, objectIDs1.ToArray());
        for (int index = 0; index < numArray.Length; ++index)
        {
          preparePasteErrors.Add(string.Format(LocalizationHolder.rm.GetString("Client.Core_691"), (object) session.GetObject(numArray[index]).NameInMessages, SelectionCommands.IsClassifier(objectType) ? (object) LocalizationHolder.rm.GetString("Client.Core_692") : (object) LocalizationHolder.rm.GetString("Client.Core_693")));
          objectIDs1.Remove(numArray[index]);
          objectIDs1.Remove(-numArray[index]);
        }
      }
      if (preparePasteErrors.Count > 0 && new PreparePasteErrors(LocalizationHolder.rm.GetString("Client.Core_1179"), preparePasteErrors).ShowDialog() == DialogResult.Cancel || objectIDs1.Count <= 0)
        return;
      long[] numArray1 = objectIDs1.ToArray();
      IObjectClassificator objectClassificator = service.GetObjectClassificator((object) session, idboTargetObject.ObjectID);
      if (objectClassificator != null)
      {
        if (!Intermech.Navigator.Selections.Consts.RecurClassify(session, objectClassificator, numArray1).FullClassified)
        {
          int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_697"), LocalizationHolder.rm.GetString("Client.Core_676"), MessageBoxButtons.OK, IMMessageBoxImage.Error);
          return;
        }
        if (objectClassificator.NonClassifiedObjects != null && objectClassificator.NonClassifiedObjects.Length != 0 && objectClassificator.ObligatoryCalculated)
          numArray1 = DataSetProcessor.DifferenceArray(numArray1, objectClassificator.NonClassifiedObjects);
      }
      if (numArray1.Length != 0)
      {
        service.IncludeObjects((object) session, idboTargetObject.ObjectID, numArray1);
        string str = numArray1.Length == 1 ? $"\"{session.GetObject(Convert.ToInt64(numArray1[0])).Caption} \"" : SelectionCommands.GetObjectsString(numArray1.Length);
        if (objectClassificator != null)
        {
          List<long> objectIDs2 = objectClassificator.ClassifiedObjects.Where<ClassifiedObjectInfo>((System.Func<ClassifiedObjectInfo, bool>) (_ => _.AttributeValues != null)).ToList<ClassifiedObjectInfo>().ConvertAll<long>((Converter<ClassifiedObjectInfo, long>) (_ => _.ObjectID));
          if (objectIDs2.Count > 0)
            Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs2));
        }
        if (isCut && parent != null && SelectionCommands.IsSelectionOrClassifier(parent.ObjectType))
          service.ExcludeObjects((object) session, parent.ObjectID, numArray1);
        int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_698"), $"{str}{(numArray1.Length % 10 != 1 || numArray1.Length == 11 ? LocalizationHolder.rm.GetString("Client.Core_412") : LocalizationHolder.rm.GetString("Client.Core_411"))}{LocalizationHolder.rm.GetString("Client.Core_699")}{(SelectionCommands.IsClassifier(idboTargetObject.ObjectType) ? LocalizationHolder.rm.GetString("Client.Core_692") : LocalizationHolder.rm.GetString("Client.Core_693"))}.", MessageBoxButtons.OK, IMMessageBoxImage.Information);
      }
      CreatedExternallyEventArgs e1 = new CreatedExternallyEventArgs("ObjectsChanged", (IList<long>) objectIDs1);
      Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e1);
    }
  }

  /// <summary>
  /// Обработчик команды вставки для выборки (классификатора)
  /// </summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void PasteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject idboTargetObject = sessionKeeper.Session.GetObject(itemData.ObjectID);
      IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
      object dataObject1 = service.GetDataObject();
      IDBTypedObjectID parent = (IDBTypedObjectID) null;
      if (dataObject1 is ClipboardObjectsList clipboardObjectsList)
        parent = clipboardObjectsList.Parent;
      int num = !(dataObject1 is ICutCopy cutCopy) ? 0 : (cutCopy.IsCut ? 1 : 0);
      IDBObjectTypedIDCollection dataObject2 = service.GetDataObject() as IDBObjectTypedIDCollection;
      SelectionCommands.InternalPaste(sessionKeeper.Session, idboTargetObject, new List<IDBTypedObjectID>((IEnumerable<IDBTypedObjectID>) dataObject2.GetTypedObjects()), cutCopy != null && cutCopy.IsCut, parent);
    }
  }

  private static List<int> EnableTypes4Paste(IDBObject idboTargetObject)
  {
    List<int> intList = new List<int>();
    IDBAttribute attributeByGuid = idboTargetObject.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || attributeByGuid.ValuesCount == 0)
      return (List<int>) null;
    foreach (object obj in attributeByGuid.Values)
    {
      string str = Convert.ToString(obj);
      if (GuidHelper.IsGuid(str))
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(str);
        if (objectTypeId != -1 && !intList.Contains(objectTypeId))
        {
          intList.Add(objectTypeId);
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
          for (int index = 0; index < childrenIdRecursive.Count; ++index)
          {
            if (!intList.Contains(childrenIdRecursive[index]))
              intList.Add(childrenIdRecursive[index]);
          }
        }
      }
    }
    if (intList.Count == 0)
      intList = (List<int>) null;
    return intList;
  }

  public static void DeleteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<DeletedSelection> deletedSelectionList = new List<DeletedSelection>(items.Count);
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SortedRelationTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      });
      for (int index = 0; index < items.Count; ++index)
      {
        long objectID = (items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID).Value;
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(objectID);
        if (SelectionCommands.IsSelectionOrClassifier(dbObject1.ObjectType))
        {
          int icon1 = service != null ? service.IndexOf(4, dbObject1.ObjectType) : -1;
          DeletedSelection deletedSelection = new DeletedSelection(dbObject1.NameInMessages, icon1);
          IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null && attributeByGuid.ValuesCount > 0)
          {
            foreach (object obj in attributeByGuid.Values)
            {
              if (GuidHelper.IsGuid(Convert.ToString(obj)))
              {
                Guid anObjectTypeGuid = new Guid(obj.ToString());
                if (anObjectTypeGuid != Guid.Empty)
                {
                  IDBObjectType objectType = sessionKeeper.Session.GetObjectType(anObjectTypeGuid, false);
                  if (objectType != null)
                  {
                    int icon2 = service != null ? service.IndexOf(4, objectType.ObjectType) : -1;
                    deletedSelection.AddObjectType(objectType.ObjectTypeName, icon2);
                  }
                }
              }
            }
          }
          foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersInVersion(paramSet, objectID).Rows)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(Convert.ToInt64(row[0]));
            int icon3 = service != null ? service.IndexOf(4, dbObject2.ObjectType) : -1;
            deletedSelection.AddParentSelection(dbObject2.NameInMessages, icon3);
          }
          deletedSelectionList.Add(deletedSelection);
        }
      }
    }
    List<DeletedSelection> selection = new List<DeletedSelection>(deletedSelectionList.Count);
    foreach (DeletedSelection deletedSelection in deletedSelectionList)
    {
      if (deletedSelection.ObjectTypes.Count > 1 || deletedSelection.ParentSelections.Count > 1)
        selection.Add(deletedSelection);
    }
    bool flag = true;
    if (selection.Count > 0 && new DeleteCommandQuestion().ShowQuestion(selection) != DialogResult.Yes)
      flag = false;
    if (!flag)
      return;
    ObjectCommands.DeleteCommand(items, viewServices, additionalInfo);
  }

  /// <summary>
  /// Обработчик команды "Восстановить значения в условиях" (для выборки)
  /// </summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public static void RestoreSelectionValues(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).RemoveTemporaryValues(itemData.ObjectID);
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", itemData.ObjectID));
  }

  internal static void IncludeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(SelectionWindow.Select("Выберите выборку", (IDescriptor) new Intermech.Navigator.GlobalNode.Descriptor(), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect, MetaDataHelper.GetObjectTypeChildrenIDRecursive(Intermech.Navigator.Selections.Consts.SelectionsTypeID).ToArray()) is IDBTypedObjectID[] dbTypedObjectIdArray) || dbTypedObjectIdArray.Length != 1)
      return;
    List<IDBTypedObjectID> objectTypedIDs = new List<IDBTypedObjectID>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID obj;
      if ((obj = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID) != null && !objectTypedIDs.Exists((Predicate<IDBTypedObjectID>) (x => x.ObjectID == obj.ObjectID)))
        objectTypedIDs.Add(obj);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      SelectionCommands.InternalPaste(sessionKeeper.Session, sessionKeeper.Session.GetObject(dbTypedObjectIdArray[0].ObjectID), objectTypedIDs, false, (IDBTypedObjectID) null);
  }
}
