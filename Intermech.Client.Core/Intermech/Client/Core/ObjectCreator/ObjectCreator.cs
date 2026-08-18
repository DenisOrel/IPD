
// Type: Intermech.Client.Core.ObjectCreator.ObjectCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>Summary description for ObjectCreator.</summary>
public class ObjectCreator : IObjectCreatorService
{
  /// <summary>Список созданных объектов</summary>
  private readonly List<ObjectCreatedInfo> _objectCreatedInfoList = new List<ObjectCreatedInfo>();
  private Hashtable _objCrCustServ = new Hashtable();
  private static readonly string _configName = "Intermech.Client.Core.ObjectCreator_ObjectCreatorForm";

  /// <summary>
  /// Событие, возникающее перед созданием заготовки нового объекта
  /// (позволяет выполнить подстановку идентификатора прототипа объекта)
  /// </summary>
  public event BeforeDraftCreateEventHandler BeforeDraftCreateEvent;

  /// <summary>
  /// Cобытие, возникающее при создании заготовки нового объекта
  /// </summary>
  public event AfterDraftCreatedEventHandler AfterDraftCreatedEvent;

  /// <summary>
  /// Событие, возникающее при успешном завершении создания нового объекта
  /// </summary>
  public event AfterObjectCreatedEventHandler AfterObjectCreatedEvent;

  /// <summary>
  /// Событие для открытия успешно созданного объекта, если включен переключатель "Открыть редактор после создания"
  /// Вызывается после AfterObjectCreatedEvent, чтобы гарантировать, что новый объект откроется только после других обработчиков
  /// </summary>
  public event AfterObjectCreatedEventHandler OpenObjectAfterCreationEvent;

  /// <summary>
  /// Событие, возникающее при успешном включении в какой-либо состав создаваемого объекта
  /// </summary>
  public event AfterEntersInCreatedEventHandler AfterEntersInCreatedEvent;

  /// <summary>
  /// Событие, возникающее при ОТМЕНЕ создания нового объекта, если заготовка объекта была создана
  /// </summary>
  public event ObjectCreatorCanceledEventHandler ObjectCreatorCanceledEvent;

  /// <summary>
  /// Cобытиt, возникающего перед CommitCreation для заготовки
  /// </summary>
  public event BeforeCommitCreationEventHandler BeforeCommitCreationEvent;

  public event FilesRenamedEventHandler FilesRenamedEvent;

  /// <summary>
  /// Событие возникает при выборе пользовательского мастера создания объектов определенного типа.
  /// </summary>
  public event EventHandler<ObjectCreatorCustomServiceEventArgs> SelectCustomServiceEvent;

  internal void FireFilesRenamedEvent(long objectID, long prototypeID)
  {
    if (this.FilesRenamedEvent == null)
      return;
    this.FilesRenamedEvent((object) this, new FilesRenamedEventArgs(objectID, prototypeID));
  }

  public long CreateObjectDialog() => this.CreateObjectDialog(-1L);

  public long CreateObjectDialog(long aTemplateObjectID)
  {
    return this.CreateObjectDialog1((int[]) null, (int[]) null, (long[]) null, -1, aTemplateObjectID);
  }

  private long CreateObjectDialog1(
    int[] aObjectTypeIDs,
    int[] aRelationTypeIDs,
    long[] aRelatedObjectIDs,
    int selectedObjTypeID,
    long aTemplateObjectID)
  {
    OpenEditorMode openEditor = OpenEditorMode.None;
    return this.CreateObjectDialog1(aObjectTypeIDs, aRelationTypeIDs, aRelatedObjectIDs, selectedObjTypeID, aTemplateObjectID, false, ref openEditor, out int _);
  }

  private long CreateObjectDialog2(
    int[] aObjectTypeIDs,
    int[] aRelationTypeIDs,
    long[] aRelatedObjectIDs,
    int selectedObjTypeID,
    long aTemplateObjectID,
    out int objectTypeID)
  {
    OpenEditorMode openEditor = OpenEditorMode.None;
    return this.CreateObjectDialog1(aObjectTypeIDs, aRelationTypeIDs, aRelatedObjectIDs, selectedObjTypeID, aTemplateObjectID, false, ref openEditor, out objectTypeID);
  }

  private long CreateObjectDialog1(
    int[] aObjectTypeIDs,
    int[] aRelationTypeIDs,
    long[] aRelatedObjectIDs,
    int selectedObjTypeID,
    long aTemplateObjectID,
    bool isVersion,
    ref OpenEditorMode openEditor,
    out int objectTypeID)
  {
    if ((aRelatedObjectIDs == null || aRelatedObjectIDs.Length == 0) && aObjectTypeIDs != null && aObjectTypeIDs.Length != 0)
    {
      List<int> intList = new List<int>(aObjectTypeIDs.Length);
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      foreach (int aObjectTypeId in aObjectTypeIDs)
      {
        IDBObjectTypeInfo objectType = service.GetObjectType(aObjectTypeId, false);
        if (objectType != null && (objectType.Options & ObjectTypeOptions.DisableManualCreate) == ObjectTypeOptions.None && Utils.CreateFreeObject(aObjectTypeId))
          intList.Add(aObjectTypeId);
      }
      aObjectTypeIDs = intList.ToArray();
    }
    objectTypeID = aObjectTypeIDs == null || aObjectTypeIDs.Length != 1 ? ObjectCreatorSelectForm.ShowSelectDialog(aObjectTypeIDs, selectedObjTypeID) : aObjectTypeIDs[0];
    if (objectTypeID == -1)
      return -1;
    ObjectRelationLink[] aObjRelations = (ObjectRelationLink[]) null;
    if (aRelatedObjectIDs != null && aRelationTypeIDs != null)
    {
      for (int index1 = 0; index1 < aObjectTypeIDs.Length; ++index1)
      {
        if (aObjectTypeIDs[index1] == objectTypeID)
        {
          if (aRelationTypeIDs.Length > index1)
          {
            aObjRelations = new ObjectRelationLink[aRelatedObjectIDs.Length];
            for (int index2 = 0; index2 < aRelatedObjectIDs.Length; ++index2)
              aObjRelations[index2] = new ObjectRelationLink(aRelatedObjectIDs[index2], aRelationTypeIDs[index1]);
            break;
          }
          break;
        }
      }
    }
    return this.CreateNewObject(objectTypeID, aTemplateObjectID, aObjRelations, DateTime.Now, isVersion, ref openEditor);
  }

  private long CreateObjectDialog1(
    int[] aObjectTypeIDs,
    ObjectRelationLink[] aObjRelations,
    int selectedObjTypeID)
  {
    if (aObjRelations == null && aObjectTypeIDs != null && aObjectTypeIDs.Length != 0)
    {
      List<int> intList = new List<int>(aObjectTypeIDs.Length);
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      foreach (int aObjectTypeId in aObjectTypeIDs)
      {
        IDBObjectTypeInfo objectType = service.GetObjectType(aObjectTypeId);
        if (objectType != null && (objectType.Options & ObjectTypeOptions.DisableManualCreate) == ObjectTypeOptions.None && Utils.CreateFreeObject(aObjectTypeId))
          intList.Add(aObjectTypeId);
      }
      aObjectTypeIDs = intList.ToArray();
    }
    int aObjectTypeID = aObjectTypeIDs == null || aObjectTypeIDs.Length != 1 ? ObjectCreatorSelectForm.ShowSelectDialog(aObjectTypeIDs, selectedObjTypeID) : aObjectTypeIDs[0];
    return aObjectTypeID != -1 ? this.CreateNewObject(aObjectTypeID, -1L, aObjRelations, DateTime.Now) : -1L;
  }

  public long CreateObjectByTypeDialog(int aObjectTypeID)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeID, (ObjectRelationLink[]) null, DateTime.Now);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aObjectTypeID"></param>
  /// <param name="OpenEditor"></param>
  /// <returns></returns>
  public long CreateObjectByTypeDialog(int aObjectTypeID, out OpenEditorMode OpenEditor)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeID, out OpenEditor, (IObjectCreatorParams) null);
  }

  /// <summary>
  /// Метод для форсированного задания открытия редактора - всегда открывать или не открывать
  /// </summary>
  /// <param name="aObjectTypeID"></param>
  /// <param name="OpenEditor"></param>
  /// <returns></returns>
  public long CreateObjectByTypeDialog(int aObjectTypeID, bool openEditor)
  {
    OpenEditorMode openEditor1 = openEditor ? OpenEditorMode.Open : OpenEditorMode.NotOpen;
    return this.CreateNewObject(aObjectTypeID, -1L, (ObjectRelationLink[]) null, DateTime.Now, false, ref openEditor1, (IObjectCreatorParams) null);
  }

  /// <summary>Создание объекта с заданными параметрами</summary>
  /// <param name="aObjectTypeID"></param>
  /// <param name="OpenEditor"></param>
  /// <param name="creatorParams"></param>
  /// <returns></returns>
  public long CreateObjectByTypeDialog(
    int aObjectTypeID,
    out OpenEditorMode OpenEditor,
    IObjectCreatorParams creatorParams)
  {
    OpenEditorMode openEditor = OpenEditorMode.None;
    long newObject = this.CreateNewObject(aObjectTypeID, -1L, (ObjectRelationLink[]) null, DateTime.Now, false, ref openEditor, creatorParams);
    OpenEditor = openEditor;
    return newObject;
  }

  /// <summary>Создание объекта с заданными параметрами</summary>
  /// <param name="objectTypeId">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="templateObjectId">Идентификатор, который задает объект-прототип для создаваемого экземпляра.</param>
  /// <param name="objRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="startDate">Дата с которой начинают действовать созданные связи</param>
  /// <param name="isVersion">Признак - создавать версию, или объект</param>
  /// <param name="openEditor"></param>
  /// <param name="creatorParams">Доп. параметры для креатора</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  public long CreateObjectByTypeDialog(
    int objectTypeId,
    long templateObjectId,
    ObjectRelationLink[] objRelations,
    DateTime startDate,
    bool isVersion,
    ref OpenEditorMode openEditor,
    IObjectCreatorParams creatorParams)
  {
    return this.CreateNewObject(objectTypeId, templateObjectId, objRelations, startDate, isVersion, ref openEditor, creatorParams);
  }

  public long CreateObjectByTypeDialog(IDBObjectType aObjectType)
  {
    return this.CreateObjectByTypeDialog(aObjectType, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(Guid aObjectTypeGuid)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeGuid, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(string aObjectTypeName)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeName, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(int[] aObjectTypeIDs)
  {
    return this.CreateObjectDialog1(aObjectTypeIDs, (int[]) null, (long[]) null, -1, -1L);
  }

  public long CreateObjectByTypeDialog(int[] aObjectTypeIDs, out int objectTypeID)
  {
    return this.CreateObjectDialog2(aObjectTypeIDs, (int[]) null, (long[]) null, -1, -1L, out objectTypeID);
  }

  public long CreateObjectByTypeDialog(int[] aObjectTypeIDs, int selectedID)
  {
    return this.CreateObjectDialog1(aObjectTypeIDs, (int[]) null, (long[]) null, selectedID, -1L);
  }

  public long CreateObjectByTypeDialog(IDBObjectType[] aObjectTypes)
  {
    if (aObjectTypes == null)
      return this.CreateObjectByTypeDialog((int[]) null);
    int[] aObjectTypeIDs = new int[aObjectTypes.Length];
    for (int index = 0; index < aObjectTypes.Length; ++index)
      aObjectTypeIDs[index] = aObjectTypes[index] == null ? -1 : aObjectTypes[index].ObjectType;
    return this.CreateObjectByTypeDialog(aObjectTypeIDs);
  }

  public long CreateObjectByTypeDialog(Guid[] aObjectTypeGuids)
  {
    if (aObjectTypeGuids == null)
      return this.CreateObjectByTypeDialog((int[]) null);
    List<int> intList = new List<int>(aObjectTypeGuids.Length);
    foreach (Guid aObjectTypeGuid in aObjectTypeGuids)
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID(aObjectTypeGuid);
      if (objectTypeId != -1)
        intList.Add(objectTypeId);
    }
    return this.CreateObjectByTypeDialog(intList.ToArray());
  }

  public long CreateObjectByTypeDialog(
    int[] aObjectTypeIDs,
    ObjectRelationLink[] aObjRelations,
    int selectedTypeID)
  {
    return aObjectTypeIDs != null ? this.CreateObjectDialog1(aObjectTypeIDs, aObjRelations, selectedTypeID) : this.CreateObjectDialog1((int[]) null, aObjRelations, selectedTypeID);
  }

  public long CreateObjectByTypeDialog(string[] aObjectTypeNames)
  {
    if (aObjectTypeNames == null)
      return this.CreateObjectByTypeDialog((int[]) null);
    List<int> intList = new List<int>(aObjectTypeNames.Length);
    MetaDataHelper.GetObjectTypesList();
    foreach (string aObjectTypeName in aObjectTypeNames)
    {
      int objectTypeIdFromName = MetaDataHelper.GetObjectTypeIDFromName(aObjectTypeName);
      if (objectTypeIdFromName != -1)
        intList.Add(objectTypeIdFromName);
    }
    return this.CreateObjectByTypeDialog(intList.ToArray());
  }

  public long CreateObjectByTypeDialog(int aObjectTypeID, ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeID, aObjRelations, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(
    IDBObjectType aObjectType,
    ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectByTypeDialog(aObjectType, aObjRelations, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(Guid aObjectTypeGuid, ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeGuid, aObjRelations, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(string aObjectTypeName, ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeName, aObjRelations, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(
    int aObjectTypeID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    return this.CreateNewObject(aObjectTypeID, -1L, aObjRelations, aStartDate);
  }

  public long CreateObjectByTypeDialog(
    IDBObjectType aObjectType,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    return aObjectType != null ? this.CreateObjectByTypeDialog(aObjectType.ObjectType, aObjRelations, aStartDate) : this.CreateObjectByTypeDialog(-1, aObjRelations, aStartDate);
  }

  public long CreateObjectByTypeDialog(
    Guid aObjectTypeGuid,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    return Guid.Empty.Equals(aObjectTypeGuid) ? this.CreateObjectByTypeDialog(-1, aObjRelations, aStartDate) : this.CreateObjectByTypeDialog(MetaDataHelper.GetObjectTypeID(aObjectTypeGuid), aObjRelations, aStartDate);
  }

  public long CreateObjectByTypeDialog(
    string aObjectTypeName,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    return string.IsNullOrEmpty(aObjectTypeName) ? this.CreateObjectByTypeDialog(-1, aObjRelations, aStartDate) : this.CreateObjectByTypeDialog(MetaDataHelper.GetObjectTypeIDFromName(aObjectTypeName), aObjRelations, aStartDate);
  }

  public long CreateObjectByTypeDialog(
    Hashtable aObjectTypeIDRelationTypeIDs,
    long[] aRelatedObjectIDs)
  {
    return this.CreateObjectByTypeDialog(aObjectTypeIDRelationTypeIDs, aRelatedObjectIDs, DateTime.Now);
  }

  public long CreateObjectByTypeDialog(
    Hashtable aObjectTypeIDRelationTypeIDs,
    long[] aRelatedObjectIDs,
    DateTime aStartDate)
  {
    if (aObjectTypeIDRelationTypeIDs == null)
      return this.CreateObjectDialog();
    int[] aObjectTypeIDs = new int[aObjectTypeIDRelationTypeIDs.Count];
    int[] aRelationTypeIDs = new int[aObjectTypeIDRelationTypeIDs.Count];
    int index = 0;
    foreach (DictionaryEntry idRelationTypeId in aObjectTypeIDRelationTypeIDs)
    {
      aObjectTypeIDs[index] = (int) idRelationTypeId.Key;
      aRelationTypeIDs[index] = idRelationTypeId.Value == null ? -1 : (int) idRelationTypeId.Value;
      ++index;
    }
    return this.CreateObjectDialog1(aObjectTypeIDs, aRelationTypeIDs, aRelatedObjectIDs, -1, -1L);
  }

  public long CreateObjectByTemplateDialog(IDBObject aTemmplateObject)
  {
    return this.CreateObjectByTemplateDialog(aTemmplateObject, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectByTemplateDialog(long aTemplateObjectID)
  {
    return this.CreateObjectByTemplateDialog(aTemplateObjectID, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectByTemplateDialog(Guid aObjectGuid)
  {
    return this.CreateObjectByTemplateDialog(aObjectGuid, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectByTemplateDialog(
    IDBObject aTemmplateObject,
    ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectByTemplateDialog(aTemmplateObject, aObjRelations, DateTime.Now);
  }

  public long CreateObjectByTemplateDialog(
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectByTemplateDialog(aTemplateObjectID, aObjRelations, DateTime.Now);
  }

  public long CreateObjectByTemplateDialog(Guid aObjectGuid, ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectByTemplateDialog(aObjectGuid, aObjRelations, DateTime.Now);
  }

  public long CreateObjectByTemplateDialog(
    IDBObject aTemmplateObject,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    return aTemmplateObject != null ? this.CreateNewObject(aTemmplateObject.ObjectType, aTemmplateObject.ObjectID, aObjRelations, aStartDate) : this.CreateNewObject(-1, -1L, aObjRelations, aStartDate);
  }

  public long CreateObjectByTemplateDialog(
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    if (aTemplateObjectID == -1L || aTemplateObjectID == 0L)
      return this.CreateObjectByTemplateDialog((IDBObject) null, aObjRelations, aStartDate);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.CreateObjectByTemplateDialog(sessionKeeper.Session.GetObject(aTemplateObjectID), aObjRelations, aStartDate);
  }

  public long CreateObjectByTemplateDialog(
    Guid aObjectGuid,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    if (Guid.Empty.Equals(aObjectGuid))
      return this.CreateObjectByTemplateDialog((IDBObject) null, aObjRelations, aStartDate);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.CreateObjectByTemplateDialog(sessionKeeper.Session.GetObject(aObjectGuid), aObjRelations, aStartDate);
  }

  public long CreateObjectVersionByTemplateDialog(IDBObject aTemmplateObject)
  {
    return this.CreateObjectVersionByTemplateDialog(aTemmplateObject, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectVersionByTemplateDialog(long aTemplateObjectID)
  {
    return this.CreateObjectVersionByTemplateDialog(aTemplateObjectID, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateObjectVersionByTemplateDialog(Guid aObjectGuid)
  {
    return this.CreateObjectVersionByTemplateDialog(aObjectGuid, (ObjectRelationLink[]) null, DateTime.Now);
  }

  public long CreateVersionAnotherType(long aTemplateObjectID, int aTemplateObjectType)
  {
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(aTemplateObjectType);
    int parentTypeID = objectTypeParentsId.Count > 0 ? objectTypeParentsId[objectTypeParentsId.Count - 1] : aTemplateObjectType;
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetObjectTypeCollection(parentTypeID, true).SelectRecursive(string.Empty).Rows)
      {
        if (Convert.ToInt32(row["F_VERSIONABLE"]) != 0)
          intList.Add(Convert.ToInt32(row["F_OBJECT_TYPE"]));
      }
    }
    OpenEditorMode openEditor = OpenEditorMode.None;
    return this.CreateObjectDialog1(intList.ToArray(), (int[]) null, (long[]) null, -1, aTemplateObjectID, true, ref openEditor, out int _);
  }

  public long CreateObjectVersionByTemplateDialog(
    IDBObject aTemmplateObject,
    ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectVersionByTemplateDialog(aTemmplateObject, aObjRelations, DateTime.Now);
  }

  public long CreateObjectVersionByTemplateDialog(
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectVersionByTemplateDialog(aTemplateObjectID, aObjRelations, DateTime.Now);
  }

  public long CreateObjectVersionByTemplateDialog(
    Guid aObjectGuid,
    ObjectRelationLink[] aObjRelations)
  {
    return this.CreateObjectVersionByTemplateDialog(aObjectGuid, aObjRelations, DateTime.Now);
  }

  public long CreateObjectVersionByTemplateDialog(
    IDBObject aTemmplateObject,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    OpenEditorMode openEditor = OpenEditorMode.None;
    return aTemmplateObject == null ? this.CreateNewObject(-1, -1L, aObjRelations, aStartDate, true, ref openEditor) : this.CreateNewObject(aTemmplateObject.ObjectType, aTemmplateObject.ObjectID, aObjRelations, aStartDate, true, ref openEditor);
  }

  public long CreateObjectVersionByTemplateDialog(
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    if (aTemplateObjectID == -1L || aTemplateObjectID == 0L)
      return this.CreateObjectVersionByTemplateDialog((IDBObject) null, aObjRelations, aStartDate);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.CreateObjectVersionByTemplateDialog(sessionKeeper.Session.GetObject(aTemplateObjectID), aObjRelations, aStartDate);
  }

  public long CreateObjectVersionByTemplateDialog(
    Guid aObjectGuid,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    if (Guid.Empty.Equals(aObjectGuid))
      return this.CreateObjectVersionByTemplateDialog((IDBObject) null, aObjRelations, aStartDate);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.CreateObjectVersionByTemplateDialog(sessionKeeper.Session.GetObject(aObjectGuid), aObjRelations, aStartDate);
  }

  public void RegisterCreatorCustomService(int aObjectTypeID, System.Type aCustomServiceType)
  {
    if (!(aCustomServiceType != (System.Type) null))
      return;
    if (this._objCrCustServ.ContainsKey((object) aObjectTypeID))
      this._objCrCustServ[(object) aObjectTypeID] = (object) aCustomServiceType;
    else
      this._objCrCustServ.Add((object) aObjectTypeID, (object) aCustomServiceType);
  }

  public void UnregisterCreatorCustomService(int aObjectTypeID, System.Type aCustomServiceType)
  {
    if (!(aCustomServiceType != (System.Type) null))
      return;
    this._objCrCustServ.Remove((object) aObjectTypeID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IList<ObjectCreatedInfo> GetObjectCreatedInfo()
  {
    return (IList<ObjectCreatedInfo>) this._objectCreatedInfoList;
  }

  /// <summary>Проверяет перед созданием связи.</summary>
  /// <param name="aObjectTypeID">Тип создаваемого объекта</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  private void CheckRelations(int aObjectTypeID, ObjectRelationLink[] aObjRelations)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (aObjRelations == null)
        return;
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      foreach (ObjectRelationLink aObjRelation in aObjRelations)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(aObjRelation.ObjectID);
        IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(aObjRelation.RelationTypeID, aObjectTypeID, dbObject.ObjectType);
        if (applicability != null && applicability.IsContent)
          dbObject.CheckRelationsEdit();
      }
    }
  }

  /// <summary>
  /// Локальная вспомогательная функция для вызова диалога создания нового объекта
  /// </summary>
  /// <remarks>Для поддержки совместимости only</remarks>
  /// <param name="aObjectTypeID">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинают действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  private long CreateNewObject(
    int aObjectTypeID,
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate)
  {
    OpenEditorMode openEditor = OpenEditorMode.None;
    return this.CreateNewObject(aObjectTypeID, aTemplateObjectID, aObjRelations, aStartDate, false, ref openEditor);
  }

  /// <summary>
  /// Локальная вспомогательная функция для вызова диалога создания нового объекта
  /// </summary>
  /// <param name="aObjectTypeID">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинают действовать созданные связи</param>
  /// <param name="isVersion">Признак - создавать версию, или объект</param>
  /// <param name="openEditor"></param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  private long CreateNewObject(
    int aObjectTypeID,
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate,
    bool isVersion,
    ref OpenEditorMode openEditor)
  {
    return this.CreateNewObject(aObjectTypeID, aTemplateObjectID, aObjRelations, aStartDate, isVersion, ref openEditor, (IObjectCreatorParams) null);
  }

  /// <summary>
  /// Локальная вспомогательная функция для вызова диалога создания нового объекта
  /// </summary>
  /// <param name="aObjectTypeID">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинают действовать созданные связи</param>
  /// <param name="isVersion">Признак - создавать версию, или объект</param>
  /// <param name="openEditor"></param>
  /// <param name="creatorParams">Доп. параметры для креатора</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  private long CreateNewObject(
    int aObjectTypeID,
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate,
    bool isVersion,
    ref OpenEditorMode openEditor,
    IObjectCreatorParams creatorParams)
  {
    this.SaveTemplateObject(aTemplateObjectID);
    OpenEditorMode openEditorMode = OpenEditorMode.None;
    bool flag1 = UISettings.AutoCheckOutNewObjects;
    this._objectCreatedInfoList.Clear();
    long resultID = -1;
    this.CheckRelations(aObjectTypeID, aObjRelations);
    if (aObjectTypeID == -1)
      return this.CreateObjectDialog(aTemplateObjectID);
    IObjectCreatorCustomService creatorCustomService = (IObjectCreatorCustomService) null;
    ArrayList arrayList = new ArrayList()
    {
      (object) aObjectTypeID
    };
    for (int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(aObjectTypeID); objectTypeParentId >= 0; objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectTypeParentId))
      arrayList.Add((object) objectTypeParentId);
    bool flag2 = false;
    foreach (int objectTypeId in arrayList)
    {
      object[] constructorParams;
      System.Type customServiceType = this.TryGetCustomServiceType(objectTypeId, out constructorParams);
      if (customServiceType != (System.Type) null)
        creatorCustomService = constructorParams == null || constructorParams.Length == 0 ? (IObjectCreatorCustomService) Activator.CreateInstance(customServiceType) : (IObjectCreatorCustomService) Activator.CreateInstance(customServiceType, constructorParams);
      if (creatorCustomService != null)
      {
        if (creatorCustomService is ICreatorCheckoutHandler creatorCheckoutHandler)
          creatorCheckoutHandler.CheckoutObject = flag1;
        long[] RelatedObjectIDs = aObjRelations == null ? new long[0] : new long[aObjRelations.Length];
        int[] RelationTypeIDs = aObjRelations == null ? new int[0] : new int[aObjRelations.Length];
        if (aObjRelations != null)
        {
          for (int index = 0; index < aObjRelations.Length; ++index)
          {
            RelationTypeIDs[index] = aObjRelations[index].RelationTypeID;
            RelatedObjectIDs[index] = aObjRelations[index].ObjectID;
          }
        }
        if (creatorCustomService is IObjectCreatorRiderCustomService riderCustomService)
        {
          if (riderCustomService is IObjectCreatorRiderParamCustomService paramCustomService)
            paramCustomService.SetParams(creatorParams);
          if (riderCustomService.AcceptDialog(aObjectTypeID, aTemplateObjectID, RelationTypeIDs, RelatedObjectIDs, aStartDate, isVersion))
          {
            resultID = creatorCustomService.CreateObjectDialog(aObjectTypeID, aTemplateObjectID, RelationTypeIDs, RelatedObjectIDs, aStartDate, isVersion);
            flag2 = true;
            if (creatorCustomService is ICreatorMultiObjectHandler multiObjectHandler)
              this._objectCreatedInfoList.AddRange(multiObjectHandler.ObjectCreatedInfo);
          }
          else
          {
            flag2 = true;
            bool flag3 = aTemplateObjectID != 0L && aTemplateObjectID != -1L && !isVersion && (!(creatorCustomService is ICreatorFileHandler) || ((ICreatorFileHandler) creatorCustomService).ShowPage);
            using (ObjectCreatorForm objectCreatorForm = new ObjectCreatorForm(this))
            {
              if (riderCustomService is IObjectCreatorFormProvider)
                ((IObjectCreatorFormProvider) riderCustomService).ObjectCreatorForm = objectCreatorForm;
              objectCreatorForm.CustomService = riderCustomService;
              objectCreatorForm.OpenInNewWindowVisible(Utils.EnableOpenInNewWindow(aObjectTypeID));
              objectCreatorForm.CreateObjectWithRelations(aObjectTypeID, aTemplateObjectID, aObjRelations, aStartDate, isVersion);
              if (!riderCustomService.AfterCreate(objectCreatorForm.CreatedObjectID))
              {
                objectCreatorForm.CreatedObject.Cancel();
                return resultID;
              }
              if (riderCustomService.VisiblePages == null)
              {
                objectCreatorForm.CreateStepControlsByDefault();
              }
              else
              {
                AdjustableViews service = ServicesManager.GetService<AdjustableViews>();
                if (riderCustomService.VisiblePages.ContainsKey(ObjectCreatePages.Classifier) && riderCustomService.VisiblePages[ObjectCreatePages.Classifier])
                  objectCreatorForm.CreateClassifierControl();
                if (riderCustomService.VisiblePages.ContainsKey(ObjectCreatePages.Properties) && riderCustomService.VisiblePages[ObjectCreatePages.Properties] && service.Find((Predicate<AdjustableView>) (x => x.Name.Equals("ObjectProperties"))).Visible)
                  objectCreatorForm.CreatePropertiesControl();
                if (riderCustomService.VisiblePages.ContainsKey(ObjectCreatePages.Template) && riderCustomService.VisiblePages[ObjectCreatePages.Template])
                  objectCreatorForm.CreateTemplateControl();
                if (riderCustomService.VisiblePages.ContainsKey(ObjectCreatePages.FileAttributes) && riderCustomService.VisiblePages[ObjectCreatePages.FileAttributes] && !flag3)
                  objectCreatorForm.CreateFileAttributesControl();
                if (riderCustomService.VisiblePages.ContainsKey(ObjectCreatePages.Relations) && riderCustomService.VisiblePages[ObjectCreatePages.Relations] && service.Find((Predicate<AdjustableView>) (x => x.Name.Equals("RelationProperties"))).Visible)
                  objectCreatorForm.CreateRelationsControl();
              }
              int propertiesControlIndex = this.GetPropertiesControlIndex(objectCreatorForm);
              Dictionary<UserControl, int> dictionary = riderCustomService.AddPages((object) objectCreatorForm.CreatedObject, propertiesControlIndex);
              if (dictionary != null && dictionary.Count > 0)
              {
                IDictionaryEnumerator enumerator = (IDictionaryEnumerator) dictionary.GetEnumerator();
                while (enumerator.MoveNext())
                {
                  if (enumerator.Key is ObjectCreatorControl)
                  {
                    int index = (int) enumerator.Value;
                    if (index != -1 && index <= objectCreatorForm.CreatorSteps.Count)
                      objectCreatorForm.CreatorSteps.Insert(index, (object) (ObjectCreatorControl) enumerator.Key);
                    else
                      objectCreatorForm.CreatorSteps.Add((object) (ObjectCreatorControl) enumerator.Key);
                  }
                }
              }
              if (flag3)
                objectCreatorForm.CreateFileRenamedControl(aTemplateObjectID);
              objectCreatorForm.FinallyUpdateTabs();
              if (openEditor == OpenEditorMode.NotOpen)
                objectCreatorForm.DisableOpenEditor();
              this.RebuidIndexes4Pages(objectCreatorForm);
              if (!objectCreatorForm.IsDisposed && objectCreatorForm.ShowDialog() == DialogResult.OK)
              {
                resultID = objectCreatorForm.CreatedObjectID;
                aTemplateObjectID = objectCreatorForm.CreatedObject.PrototypeID;
                this.FireRelationCreatedEvent(aObjRelations);
                bool editorAfterCreate = objectCreatorForm.RunEditorAfterCreate;
                if (openEditor == OpenEditorMode.None)
                  openEditor = editorAfterCreate ? OpenEditorMode.Open : OpenEditorMode.NotOpen;
                openEditorMode = objectCreatorForm.OpenInNewWindowAfterCreate ? OpenEditorMode.Open : OpenEditorMode.NotOpen;
              }
              else if (objectCreatorForm.CreatedObjectID != -1L)
                this.FireOnObjectCreatorCanceledEvent(objectCreatorForm.CreatedObjectID, isVersion, aObjectTypeID);
            }
          }
        }
        else
        {
          resultID = creatorCustomService.CreateObjectDialog(aObjectTypeID, aTemplateObjectID, RelationTypeIDs, RelatedObjectIDs, aStartDate, isVersion);
          flag2 = true;
          if (creatorCustomService is ICreatorMultiObjectHandler multiObjectHandler)
            this._objectCreatedInfoList.AddRange(multiObjectHandler.ObjectCreatedInfo);
        }
        if (creatorCheckoutHandler != null)
        {
          flag1 = creatorCheckoutHandler.CheckoutObject;
          break;
        }
        break;
      }
    }
    if (!flag2)
    {
      using (ObjectCreatorForm objectCreatorForm = new ObjectCreatorForm(this))
      {
        objectCreatorForm.OpenInNewWindowVisible(Utils.EnableOpenInNewWindow(aObjectTypeID));
        objectCreatorForm.SetNewObjectWithRelations(aObjectTypeID, aTemplateObjectID, aObjRelations, aStartDate, isVersion);
        if (aTemplateObjectID != 0L && aTemplateObjectID != -1L && !isVersion)
          objectCreatorForm.CreateFileRenamedControl(aTemplateObjectID);
        objectCreatorForm.FinallyUpdateTabs();
        this.RebuidIndexes4Pages(objectCreatorForm);
        if (!objectCreatorForm.IsDisposed && objectCreatorForm.ShowDialog() == DialogResult.OK)
        {
          resultID = objectCreatorForm.CreatedObjectID;
          this.FireRelationCreatedEvent(aObjRelations);
          bool editorAfterCreate = objectCreatorForm.RunEditorAfterCreate;
          if (openEditor == OpenEditorMode.None)
            openEditor = editorAfterCreate ? OpenEditorMode.Open : OpenEditorMode.NotOpen;
          openEditorMode = objectCreatorForm.OpenInNewWindowAfterCreate ? OpenEditorMode.Open : OpenEditorMode.NotOpen;
        }
        else if (objectCreatorForm.CreatedObjectID != -1L)
          this.FireOnObjectCreatorCanceledEvent(objectCreatorForm.CreatedObjectID, isVersion, aObjectTypeID);
      }
    }
    if (resultID != -1L && resultID != 0L)
    {
      if (flag1)
      {
        int index = this._objectCreatedInfoList.FindIndex((Predicate<ObjectCreatedInfo>) (item => item.ObjectId == resultID));
        resultID = Intermech.Client.Core.ObjectCreator.ObjectCreator.TryToCheckOutObject(resultID);
        if (index != -1)
          this._objectCreatedInfoList[index].ObjectId = resultID;
      }
      if (this._objectCreatedInfoList.FirstOrDefault<ObjectCreatedInfo>((System.Func<ObjectCreatedInfo, bool>) (item => item.ObjectId == resultID)) == null)
        this._objectCreatedInfoList.Add(new ObjectCreatedInfo(resultID, aObjectTypeID, aTemplateObjectID, isVersion)
        {
          RelationLinks = aObjRelations
        });
      this.FireOnObjectCreatorCompletedEvent(resultID, openEditor == OpenEditorMode.Open, aTemplateObjectID, isVersion, aObjectTypeID);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (Constants.UserObjectTypeID == aObjectTypeID)
        {
          DataTable dataTable = sessionKeeper.Session.GetRelationCollection(Constants.SimpleRelationRelationTypeID).EntersInVersion(new DBRecordSetParams()
          {
            Columns = new object[2]
            {
              (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
              (object) ObligatoryObjectAttributes.F_PROJ_ID
            }
          }, resultID);
          INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsCreated", DataSetProcessor.GetInt64Value(row, 0, 0L), DataSetProcessor.GetInt64Value(row, 1, 0L), Constants.SimpleRelationRelationTypeID);
            service.FireEvent((object) this, (NotificationEventArgs) e);
          }
        }
      }
      if (openEditorMode == OpenEditorMode.Open)
        Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(resultID), (System.IServiceProvider) new AdvancedServiceContainer());
    }
    return resultID;
  }

  /// <summary>
  /// Под каким индексом контрол со свойствами
  /// 0 - если не нашли
  /// </summary>
  /// <param name="objectCreatorForm"></param>
  /// <returns></returns>
  private int GetPropertiesControlIndex(ObjectCreatorForm objectCreatorForm)
  {
    int propertiesControlIndex = 0;
    for (int index = 0; index < objectCreatorForm.CreatorSteps.Count; ++index)
    {
      if (objectCreatorForm.CreatorSteps[index] is ObjectPropertiesControl)
      {
        propertiesControlIndex = index;
        break;
      }
    }
    return propertiesControlIndex;
  }

  private void FireRelationCreatedEvent(ObjectRelationLink[] aObjRelations)
  {
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (aObjRelations == null || aObjRelations.Length == 0)
      return;
    foreach (ObjectRelationLink aObjRelation in aObjRelations)
      service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", aObjRelation.LinkID, aObjRelation.ObjectID, aObjRelation.RelationTypeID));
  }

  private void RebuidIndexes4Pages(ObjectCreatorForm objectCreatorForm)
  {
    for (int index = 0; index < objectCreatorForm.CreatorSteps.Count; ++index)
    {
      if (objectCreatorForm.CreatorSteps[index] is ObjectCreatorControl)
        ((ObjectCreatorControl) objectCreatorForm.CreatorSteps[index]).PageIndex = index;
    }
  }

  /// <summary>
  /// Метод проверяет две вещи - включена ли настройка в интерфейсе пользователя, а также
  /// шаг жизненного цикла у указанного объекта. Если да, то берёт объект на изменение и
  /// возвращает идентификатор его рабочей копии. Иначе вернёт значение newObjectID.
  /// </summary>
  /// <param name="newObjectID">Идентификатор вновь созданного объекта</param>
  /// <returns>Идентификатор рабочей копии или полученный идентификатор объекта</returns>
  internal static long TryToCheckOutObject(long newObjectID)
  {
    if (newObjectID <= 0L)
      return newObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(newObjectID);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        long checkoutBy = dbObject.CheckoutBy;
        if (checkoutBy == 0L)
        {
          if (!(dbObject as IDBSecurity).CheckAccess(ActionType.Edit, true, false))
            return newObjectID;
          dbObject = dbObject.CheckOut(false);
          if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          {
            DBObjectsCheckOutEventArgs e = new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
            {
              newObjectID
            }, (IList<long>) new long[1]
            {
              dbObject.ObjectID
            });
            service.FireEvent((object) null, (NotificationEventArgs) e);
          }
        }
        else if (newObjectID > 0L && checkoutBy == sessionKeeper.Session.UserID)
          return -newObjectID;
      }
      return dbObject.ObjectID;
    }
  }

  /// <summary>Вызвать обработчики события EntersInCreatedEvent</summary>
  internal void FireEntersInCreatedEvent(AfterEntersInCreatedEventArgs e)
  {
    AfterEntersInCreatedEventHandler entersInCreatedEvent = this.AfterEntersInCreatedEvent;
    if (entersInCreatedEvent == null)
      return;
    entersInCreatedEvent((object) this, e);
  }

  /// <summary>
  /// Вызвать обработчики события ObjectCreatorBeforeDraftCreateEvent
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="creatingObjType">Идентификатор типа создаваемого объекта</param>
  /// <param name="templateID">Предполагаемый шаблон</param>
  /// <returns>Новый шаблон</returns>
  internal long FireObjectCreatorBeforeDraftCreateEvent(
    IUserSession session,
    int creatingObjType,
    long templateID)
  {
    if (this.BeforeDraftCreateEvent == null)
      return templateID;
    BeforeDraftCreateEventArgs e = new BeforeDraftCreateEventArgs(creatingObjType, templateID);
    this.BeforeDraftCreateEvent((object) this, e);
    return e.TemplateID;
  }

  public void FireBeforeCommitCreationEvent(IDBObject obj, long prototypeID)
  {
    BeforeCommitCreationEventHandler commitCreationEvent = this.BeforeCommitCreationEvent;
    if (commitCreationEvent == null)
      return;
    commitCreationEvent((object) this, new BeforeCommitCreationEventArgs(obj, prototypeID));
  }

  /// <summary>
  /// Для вызова обработчиков события ObjectCreatorDraftCreatedEvent
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <param name="draftID">Идентификатор заготовки</param>
  /// <param name="prototypeID">Идентификатор прототипа</param>
  public void FireObjectCreatorDraftCreatedEvent(int objectType, long draftID, long prototypeID)
  {
    AfterDraftCreatedEventHandler draftCreatedEvent = this.AfterDraftCreatedEvent;
    if (draftCreatedEvent == null)
      return;
    draftCreatedEvent((object) this, (AfterDraftCreatedEventArgs) new AfterPrototypedDraftCreatedEventArgs(objectType, draftID, prototypeID));
  }

  public void FireOnObjectCreatorCanceledEvent(long zagId, bool isVersion = true, int objTypeID = -1)
  {
    ObjectCreatorCanceledEventHandler creatorCanceledEvent = this.ObjectCreatorCanceledEvent;
    if (creatorCanceledEvent == null)
      return;
    creatorCanceledEvent((object) this, new ObjectCreatorCanceledEventArgs(zagId, isVersion, objTypeID));
  }

  /// <summary>
  /// Для вызова обработчиков события OnObjectCreatorCompleatedEvent
  /// </summary>
  /// <param name="objId">Идентификатор созданного объекта</param>
  /// <param name="runEditor">Признак - нужно ли запускать редактор</param>
  /// <param name="prototypeId">Идентификатор прототипа по которому создается объект</param>
  /// <param name="isVersion">Признак - создается новый объект или его версия</param>
  /// <param name="objectTypeID">Идентификатор типа созданного объекта</param>
  internal void FireOnObjectCreatorCompletedEvent(
    long objId,
    bool runEditor,
    long prototypeId,
    bool isVersion,
    int objectTypeID)
  {
    AfterObjectCreatedEventHandler objectCreatedEvent = this.AfterObjectCreatedEvent;
    if (objectCreatedEvent != null)
      objectCreatedEvent((object) this, new AfterObjectCreatedEventArgs(objId, runEditor, prototypeId, isVersion, objectTypeID));
    AfterObjectCreatedEventHandler afterCreationEvent = this.OpenObjectAfterCreationEvent;
    if (afterCreationEvent == null)
      return;
    afterCreationEvent((object) this, new AfterObjectCreatedEventArgs(objId, runEditor, prototypeId, isVersion, objectTypeID));
  }

  private void SaveTemplateObject(long aTemplateObjectId)
  {
    DBObjectState objectByVersionId = ClientContext.FileVault.WorkArea.FindPublishedObjectByVersionId(aTemplateObjectId);
    if (objectByVersionId == null || !objectByVersionId.IsEditableState)
      return;
    ServiceUtils.GetService<ICaptureFileChangesService>((object) ServicesManager.ServiceContainer, true).CaptureChanges(aTemplateObjectId, SaveChangesMode.Default, (System.IServiceProvider) null);
  }

  public static void SaveSettings(Form form)
  {
    FormStorage.SaveLayout((Control) form, Intermech.Client.Core.ObjectCreator.ObjectCreator._configName, (IDictionary) null);
  }

  public static void LoadSettings(Form form) => Intermech.Client.Core.ObjectCreator.ObjectCreator.LoadSettings(form, true);

  public static void LoadSettings(Form form, bool setSize)
  {
    Point lLocation;
    Size lSize;
    if (!FormStorage.LoadLayout((Control) form, Intermech.Client.Core.ObjectCreator.ObjectCreator._configName, (IDictionary) null, true, out lLocation, out lSize))
      return;
    form.Location = lLocation;
    if (!setSize)
      return;
    form.Size = lSize;
  }

  private System.Type TryGetCustomServiceType(int objectTypeId, out object[] constructorParams)
  {
    constructorParams = (object[]) null;
    EventHandler<ObjectCreatorCustomServiceEventArgs> customServiceEvent = this.SelectCustomServiceEvent;
    if (customServiceEvent != null)
    {
      ObjectCreatorCustomServiceEventArgs e = new ObjectCreatorCustomServiceEventArgs(objectTypeId);
      customServiceEvent((object) this, e);
      if (e.Handled && e.CustomServiceType != (System.Type) null)
      {
        if (e.ConstructorParams != null && e.ConstructorParams.Length != 0)
          constructorParams = e.ConstructorParams;
        return e.CustomServiceType;
      }
    }
    return this._objCrCustServ.ContainsKey((object) objectTypeId) ? (System.Type) this._objCrCustServ[(object) objectTypeId] : (System.Type) null;
  }
}
