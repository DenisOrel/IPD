// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.AddObjectCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.CacheServices;
using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Реализация команды "Добавить в состав" для технологических объектов
/// </summary>
internal class AddObjectCommand : ExtendedSelectedItemsCommand
{
  /// <summary>Флаг добавления "Объектов из ТП"</summary>
  protected readonly bool _addTpNodes;
  /// <summary>Список выбранных объектов для вставки</summary>
  protected List<IDBTypedObjectID> _selectedObjInfoItems;
  /// <summary>Кеш допустимых типов связей</summary>
  protected Hashtable _linkTypes;

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  protected virtual bool ValidateCommandArgs()
  {
    return this.Items != null && this.ContextServices != null && this.Items.Count > 0;
  }

  /// <summary>Проверка допустимости команды для тек. параметров</summary>
  /// <returns></returns>
  protected virtual bool AllowCommand()
  {
    return this.Items.Count == 1 && this.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID;
  }

  /// <summary>Выполнение команды вставки в дереве навигатора</summary>
  protected virtual void ProceedCommand()
  {
    if (this.Items == null || this.Items.Count == 0)
      return;
    this.ProceedCommand(this.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? new ObjInfoItem(itemData.ObjectID, itemData.ObjectType) : (ObjInfoItem) null);
  }

  /// <summary>Загрузка метаданных для тек. объекта</summary>
  /// <param name="targetObjInfo"></param>
  protected virtual void LoadMetaDataInfo(ObjInfoItem targetObjInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      this._linkTypes = session.GetObjectType(targetObjInfo.ObjTypeID).GetPossibleChildren();
      if (this._linkTypes.Count == 0)
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString(sc_19253.ssp_techcard_19254()), (object) session.GetObjectType(targetObjInfo.ObjTypeID).ObjectTypeName));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  protected virtual void ProceedCommand(ObjInfoItem targetObjInfo)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) targetObjInfo))
      return;
    this.LoadMetaDataInfo(targetObjInfo);
    if (!this.SelectObjects4Command(targetObjInfo) || !this.CheckTargetObjectAllowModification(targetObjInfo))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.DoBeforeProceedItems(sessionKeeper.Session);
      try
      {
        this.DoProceedItems(sessionKeeper.Session, targetObjInfo);
      }
      finally
      {
        this.DoAfterProceedItems(sessionKeeper.Session);
      }
    }
  }

  /// <summary>Обработка объектов</summary>
  /// <param name="session"></param>
  /// <param name="targetObjInfo"></param>
  protected virtual void DoProceedItems(IUserSession session, ObjInfoItem targetObjInfo)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if ((TypedInfoItem) targetObjInfo == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (targetObjInfo));
    this.DoProceedItems_Add(session, targetObjInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <exception cref="T:System.Exception"></exception>
  /// <returns></returns>
  protected virtual bool SelectObjects4Command(ObjInfoItem targetObjInfo)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) targetObjInfo) || this._linkTypes.Count == 0)
      return false;
    IServiceContainer services = (IServiceContainer) new ServiceContainer();
    DescriptorCollection descriptors = new DescriptorCollection();
    IObjectTypeNodeFilter serviceInstance = (IObjectTypeNodeFilter) new ObjectTypeNodeFilter();
    services.AddService(typeof (IObjectTypeNodeFilter), (object) serviceInstance);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IObjectTypeHierarchy service = (IObjectTypeHierarchy) ServiceUtils.GetService<ICacheServices>((object) ApplicationServices.Container, false).GetService("ObjectTypeHierarchy");
      List<int> intList1 = new List<int>(0);
      List<int> intList2 = new List<int>(0);
      List<int> intList3 = new List<int>((IEnumerable<int>) sessionKeeper.Session.GetObjectTypeCollection(-2, true).GetVisibleList());
      intList3.Sort();
      foreach (int key in (IEnumerable) this._linkTypes.Keys)
      {
        if (intList3.BinarySearch(key) >= 0 && !intList1.Contains(key) && service.EnabledObjectType(key))
        {
          intList1.Add(key);
          serviceInstance.EnabledObjectTypes.Add(key);
          int parentType = service.GetParentType(key);
          if (parentType != -1)
          {
            IDBObjectType objectType = session.GetObjectType(parentType);
            if (objectType.Versionable == ObjectVersionModes.Abstract)
            {
              intList1.Add(objectType.ObjectType);
              serviceInstance.EnabledObjectTypes.Add(objectType.ObjectType);
            }
          }
        }
      }
      for (int index1 = 0; index1 < intList1.Count; ++index1)
      {
        int childTypeID = intList1[index1];
        int[] parentTypes = service.GetParentTypes(childTypeID);
        if (parentTypes == null || parentTypes.Length == 0)
        {
          if (!intList2.Contains(childTypeID))
            intList2.Add(childTypeID);
        }
        else
        {
          if (!intList2.Contains(childTypeID))
            intList2.Add(childTypeID);
          for (int index2 = 0; index2 < parentTypes.Length; ++index2)
          {
            if (intList1.Contains(parentTypes[index2]))
            {
              intList2.Remove(childTypeID);
              childTypeID = parentTypes[index2];
              if (!intList2.Contains(childTypeID))
                intList2.Add(childTypeID);
            }
            else if (!intList2.Contains(childTypeID))
              intList2.Add(childTypeID);
          }
        }
      }
      if (intList2.Count == 0)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_19253.ssp_techcard_19255()), (object) MetaDataHelper.GetObjectTypeName(targetObjInfo.ObjTypeID)));
      foreach (int objTypeID in intList2)
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objTypeID));
      if (this._addTpNodes)
      {
        long parentTp = TechCardUtils.GetParentTP(targetObjInfo.ObjectID, session);
        List<ObjInfoItem> objInfoItemList = (List<ObjInfoItem>) null;
        if (parentTp != 0L)
        {
          List<ObjInfoItem> articles4Object = TechCardObjUtils.Article.GetArticles4Object(parentTp, session);
          if (articles4Object != null && articles4Object.Count != 0)
            objInfoItemList = TechCardObjUtils.Article.GetTechProcList(articles4Object, new int[1]
            {
              TechCardConsts.ObjectTypes.TechProcEdinID
            }, session);
        }
        if (objInfoItemList != null)
        {
          if (objInfoItemList.Count != 0)
          {
            List<long> longList = new List<long>(objInfoItemList.Count);
            foreach (ObjInfoItem objInfoItem in objInfoItemList)
            {
              if (objInfoItem.ObjectID != parentTp)
                longList.Add(objInfoItem.ObjectID);
            }
            Dictionary<int, List<long>> objectIDs = new Dictionary<int, List<long>>(1)
            {
              {
                TechCardConsts.ObjectTypes.TechProcEdinID,
                longList
              }
            };
            DictDescriptor dictDescriptor = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, TechCardConsts.ObjectTypes.TechProcEdinID, LocalizationHolder.rm.GetString("TechCard.Client_458"), objectIDs)
            {
              ExpandNodes = false
            };
            descriptors.Add((IDescriptor) dictDescriptor);
          }
        }
      }
    }
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategoryCustomNode, 1, LocalizationHolder.rm.GetString("TechCard.Client_461"), descriptors);
    return this.DoSelectObjects4Command(targetObjInfo, rootDescriptor, services);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <param name="rootDescriptor"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  protected virtual bool DoSelectObjects4Command(
    ObjInfoItem targetObjInfo,
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor,
    IServiceContainer services)
  {
    string caption;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      caption = sessionKeeper.Session.GetObjectInfo(targetObjInfo.ObjectID).Caption;
    IDBTypedObjectID[] source = (IDBTypedObjectID[]) Intermech.Navigator.SelectionWindow.Select(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_460"), (object) caption), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), (IServiceProvider) services, SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule);
    this._selectedObjInfoItems = source != null ? ((IEnumerable<IDBTypedObjectID>) source).ToList<IDBTypedObjectID>() : (List<IDBTypedObjectID>) null;
    return this._selectedObjInfoItems != null;
  }

  /// <summary>
  /// Проверка возможности создания (вставки) дочерних объектов для родительского объекта,
  /// согласно его состоянию, правам доступа
  /// </summary>
  /// <returns></returns>
  protected virtual bool CheckTargetObjectAllowModification(ObjInfoItem targetObjInfo)
  {
    ITechCardObjectCreateAnalyzingService service = ServiceUtils.GetService<ITechCardObjectCreateAnalyzingService>((object) ApplicationServices.Container, false);
    if (service == null)
      return false;
    List<int> intList = new List<int>(this._selectedObjInfoItems.Count);
    foreach (IDBTypedObjectID selectedObjInfoItem in this._selectedObjInfoItems)
    {
      if (this._linkTypes.ContainsKey((object) selectedObjInfoItem.ObjectType))
        intList.Add(Convert.ToInt32(this._linkTypes[(object) selectedObjInfoItem.ObjectType]));
    }
    TechObjectCreatorArgs creatorArgs = new TechObjectCreatorArgs(this._selectedObjInfoItems.Select<IDBTypedObjectID, int>((Func<IDBTypedObjectID, int>) (item => item.ObjectType)).ToArray<int>(), this._selectedObjInfoItems.Select<IDBTypedObjectID, long>((Func<IDBTypedObjectID, long>) (item => item.ObjectID)).ToArray<long>(), (int[]) null, (long[]) null, DateTime.Now, false);
    creatorArgs.RelatedObjectIDs = new long[1]
    {
      targetObjInfo.ObjectID
    };
    creatorArgs.RelationTypeIDs = intList.ToArray();
    if (!service.AllowObjectCreation(creatorArgs, (TechObjectCreatorParams) null))
      return false;
    this.ReloadSelectedObjectInfo(targetObjInfo, new ObjInfoItem(creatorArgs.RelatedObjectIDs[0], targetObjInfo.ObjTypeID));
    targetObjInfo.ObjectID = creatorArgs.RelatedObjectIDs[0];
    return true;
  }

  /// <summary>Обновление информации о добавляемых объектах, связях</summary>
  /// <param name="oldObjInfo"></param>
  /// <param name="newObjInfo"></param>
  protected virtual void ReloadSelectedObjectInfo(ObjInfoItem oldObjInfo, ObjInfoItem newObjInfo)
  {
    if (oldObjInfo.ObjectID == newObjInfo.ObjectID)
      return;
    for (int index = 0; index < this._selectedObjInfoItems.Count; ++index)
    {
      IDBTypedObjectID selectedObjInfoItem = this._selectedObjInfoItems[index];
      if (selectedObjInfoItem.ObjectID == oldObjInfo.ObjectID)
        this._selectedObjInfoItems[index] = (IDBTypedObjectID) new DBTypedObjectID(selectedObjInfoItem.ObjectType, newObjInfo.ObjectID, selectedObjInfoItem.ID, selectedObjInfoItem.Caption, selectedObjInfoItem.Owner, selectedObjInfoItem.Version, selectedObjInfoItem.BaseVersion, selectedObjInfoItem.SiteID, selectedObjInfoItem.ModificationID);
    }
  }

  /// <summary>Обработка объектов в режиме "Добавить в состав"</summary>
  /// <param name="session"></param>
  /// <param name="targetObjInfo"></param>
  protected virtual void DoProceedItems_Add(IUserSession session, ObjInfoItem targetObjInfo)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) targetObjInfo) || this._selectedObjInfoItems == null || this._selectedObjInfoItems.Count == 0)
      return;
    TechcardClientUtils.StartCreateRelations(targetObjInfo.ObjectID, session);
    try
    {
      IDBTypedObjectID itemData = this.Items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      session.StartLogHistory();
      Intermech.Navigator.ContextCommands.ObjectCommands.DoInsertIntoObject(this.Items.GetParentPath(0), itemData, this._selectedObjInfoItems.ToArray(), (IDBRelationID[]) null, this._linkTypes, this.ContextServices, NavigatorRelationCommand.InsertIn);
    }
    finally
    {
      session.StopLogHistory();
      TechcardClientUtils.StopCreateRelations(session);
    }
    if (!(this.ContextServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service))
      return;
    int num = service.ManualSort ? 1 : 0;
  }

  /// <summary>Конструктор</summary>
  public AddObjectCommand(bool addTpNodes = false)
    : this(addTpNodes, "Add")
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="addTpNodes"></param>
  /// <param name="name"></param>
  protected AddObjectCommand(bool addTpNodes, string name)
    : base(name)
  {
    this._addTpNodes = addTpNodes;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    if (!this.ValidateCommandArgs() || !this.AllowCommand())
      return;
    this.ProceedCommand();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoBeforeProceedItems(IUserSession session)
  {
    base.DoBeforeProceedItems(session);
    if (this.ContextServices == null || !(this.ContextServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service))
      return;
    service.CanRestoreFocusedNode = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoAfterProceedItems(IUserSession session)
  {
    base.DoAfterProceedItems(session);
  }
}
