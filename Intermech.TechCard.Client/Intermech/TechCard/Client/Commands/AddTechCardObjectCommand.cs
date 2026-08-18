// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.AddTechCardObjectCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Реализация команды контекстного меню "Добавить" (создание новых технологических объектов)
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="objectTypeId"></param>
internal class AddTechCardObjectCommand(int objectTypeId) : TechCardSelectedItemsCommand("add" + (object) objectTypeId)
{
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool ExecuteCommand()
  {
    AddTechCardObjectCommand.CreateObject(this.Items, this.ContextServices, this.AdditionalInfo);
    return true;
  }

  /// <summary>
  /// Получение допустимых "дочерних" типов для вызова команды
  /// </summary>
  /// <param name="items">Информация о выделенных элементах</param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public static List<int> GetAllowedObjectTypes(ISelectedItems items, IServiceProvider viewServices)
  {
    List<int> source = new List<int>();
    ViewStateFlags viewStateFlags = !(viewServices.GetService(typeof (IViewState)) is IViewState service) ? ViewStateFlags.None : service.ViewState;
    if ((viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.None || items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData1))
      return source;
    int num = itemData1.Value;
    if (TechCardConsts.Utils.IsTechcardObjectType((object) num))
    {
      IDBRelationID itemData2 = items.GetItemData<IDBRelationID>(0, false);
      if (itemData2 != null && !Intermech.Consts.IsUndefinedRelationId(itemData2.Value))
        source.Add(num);
    }
    List<int> visibleObjTypes = TechcardClientUtils.ObjectTypes.GetVisibleObjTypes();
    foreach (ApplicabilitiesKey applicabilitiesKey in MetaDataHelper.GetObjectTypeApplicabilities(num).Where<IMSApplicability>((Func<IMSApplicability, bool>) (a => a.RelationTypeID == TechCardConsts.RelTypes.TechRelationID)).ToList<IMSApplicability>().GetEnableChildApplicabilitiesKey())
    {
      if (visibleObjTypes.BinarySearch(applicabilitiesKey.ChildType) >= 0)
        source.Add(applicabilitiesKey.ChildType);
    }
    return source.Distinct<int>().ToList<int>();
  }

  /// <summary>команда добавления нового объекта</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void CreateObject(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0 || viewServices == null || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    if (viewServices.GetService(typeof (IViewState)) is IViewState service)
    {
      long viewState = (long) service.ViewState;
    }
    int int32 = Convert.ToInt32(additionalInfo);
    if (itemData.ObjectType == int32)
      AddTechCardObjectCommand.CreateObject_AfterCurrent(items, viewServices, additionalInfo);
    else
      AddTechCardObjectCommand.CreateObject_InsideParent(items, viewServices, additionalInfo);
  }

  /// <summary>
  /// команда добавления нового объекта в составе родителя после текущего объекта
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void CreateObject_AfterCurrent(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0 || viewServices == null || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID))
      return;
    IObjectCreatorService service1 = ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, false);
    if (service1 == null)
      return;
    int int32 = Convert.ToInt32(additionalInfo);
    TechObjectCreatorArgs creatorArgs = new TechObjectCreatorArgs(int32, 0L, (int[]) null, (long[]) null, DateTime.Now, false);
    ITechCardObjectCreateAnalyzingService service2 = ServiceUtils.GetService<ITechCardObjectCreateAnalyzingService>((object) ApplicationServices.Container, false);
    if (items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID dbRelationId && (dbRelationId.ProjID == 0L || dbRelationId.Value == -1L))
      dbRelationId = (IDBRelationID) null;
    TechObjectCreatorParams objectCreatorParams;
    if (dbRelationId == null)
    {
      objectCreatorParams = (TechObjectCreatorParams) null;
    }
    else
    {
      objectCreatorParams = new TechObjectCreatorParams(items, viewServices);
      objectCreatorParams.AsyncMode = true;
      objectCreatorParams.RelationMode = CompositionTargetMode.InsertAfter;
    }
    IObjectCreatorParams creatorParams = (IObjectCreatorParams) objectCreatorParams;
    if (service2 != null)
    {
      if (dbRelationId != null)
      {
        creatorArgs.RelatedObjectIDs = new long[1]
        {
          dbRelationId.ProjID
        };
        creatorArgs.RelationTypeIDs = new int[1]
        {
          dbRelationId.RelationType
        };
      }
      if (!service2.AllowObjectCreation(creatorArgs, (TechObjectCreatorParams) creatorParams))
        return;
      if (creatorParams != null)
        dbRelationId = ((TechObjectCreatorParams) creatorParams).Items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
      if (dbRelationId != null)
        dbRelationId = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    }
    OpenEditorMode OpenEditor = OpenEditorMode.None;
    long objectByTypeDialog;
    if (dbRelationId == null)
      objectByTypeDialog = service1.CreateObjectByTypeDialog(int32, out OpenEditor, (IObjectCreatorParams) null);
    else
      objectByTypeDialog = service1.CreateObjectByTypeDialog(int32, -1L, new ObjectRelationLink[1]
      {
        new ObjectRelationLink(dbRelationId.ProjID, dbRelationId.RelationType, dbRelationId.Value)
      }, DateTime.Now, false, ref OpenEditor, creatorParams);
    long objectID = objectByTypeDialog;
    switch (objectID)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        INotificationService service3 = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
        if (service3 == null)
          break;
        DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", objectID);
        service3.FireEvent((object) null, (NotificationEventArgs) e);
        break;
    }
  }

  /// <summary>команда добавления нового объекта в состав родителя</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void CreateObject_InsideParent(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0 || viewServices == null || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    IObjectCreatorService service1 = ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, false);
    if (service1 == null)
      return;
    int int32 = Convert.ToInt32(additionalInfo);
    TechObjectCreatorArgs creatorArgs = new TechObjectCreatorArgs(int32, 0L, (int[]) null, (long[]) null, DateTime.Now, false);
    ITechCardObjectCreateAnalyzingService service2 = ServiceUtils.GetService<ITechCardObjectCreateAnalyzingService>((object) ApplicationServices.Container, false);
    long aObjectID = itemData.ObjectID;
    int relationTypeId = MetaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechRelationGuid);
    TechObjectCreatorParams creatorParams = new TechObjectCreatorParams(items, viewServices)
    {
      AsyncMode = true
    };
    if (service2 != null)
    {
      creatorArgs.RelatedObjectIDs = new long[1]
      {
        aObjectID
      };
      creatorArgs.RelationTypeIDs = new int[1]
      {
        relationTypeId
      };
      if (!service2.AllowObjectCreation(creatorArgs, creatorParams))
        return;
      aObjectID = creatorArgs.RelatedObjectIDs[0];
    }
    ObjectRelationLink objectRelationLink = new ObjectRelationLink(aObjectID, relationTypeId);
    OpenEditorMode openEditor = OpenEditorMode.None;
    long objectByTypeDialog = service1.CreateObjectByTypeDialog(int32, -1L, new ObjectRelationLink[1]
    {
      objectRelationLink
    }, DateTime.Now, false, ref openEditor, (IObjectCreatorParams) creatorParams);
    switch (objectByTypeDialog)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        INotificationService service3 = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
        if (service3 == null)
          break;
        DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog);
        service3.FireEvent((object) null, (NotificationEventArgs) e);
        break;
    }
  }
}
