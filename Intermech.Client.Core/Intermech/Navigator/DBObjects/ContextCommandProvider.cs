
// Type: Intermech.Navigator.DBObjects.ContextCommandProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections;
using Intermech.Navigator.Snapshots;
using Intermech.Search.ObjectGroups;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Позволяет реализовать провайдер команд для контекстного меню навигатора.
/// Провайдер должен проанализировать информацию о контексте, в котором будет показано
/// меню, и вернуть контейнер со сведениями о допустимых командах.
/// </summary>
internal class ContextCommandProvider : ICommandsProvider
{
  private int fileAttributeId;
  private AssignSystemGuidCommandHandler assignSystemGuidCommandHandler;

  /// <summary>Создает объект.</summary>
  public ContextCommandProvider() => this.fileAttributeId = -1;

  /// <summary>
  /// Возвращает или задает обработчик для команды "Назначить системный GUID"
  /// </summary>
  public AssignSystemGuidCommandHandler AssignSystemGuidCommandHandler
  {
    get => this.assignSystemGuidCommandHandler;
    set => this.assignSystemGuidCommandHandler = value;
  }

  /// <summary>Возвращает идентификатор атрибута "Файл".</summary>
  private int FileAttributeId
  {
    get
    {
      if (this.fileAttributeId == -1)
        this.fileAttributeId = MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545");
      return this.fileAttributeId;
    }
  }

  /// <summary>Можно ли удалять указанные объекты</summary>
  /// <param name="items">Список выделенных элементов пространства навигации</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <returns>Можно ли удалять указанные объекты</returns>
  public static bool CanDeleteObjects(ISelectedItems items, IServiceProvider viewServices)
  {
    bool flag = items != null && items.Count > 0;
    if (!flag)
      return flag;
    for (int index = 0; index < items.Count; ++index)
    {
      if (!(items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectType == MetaDataHelper.GetObjectTypeID("cad0004a-306c-11d8-b4e9-00304f19f545"))
        return false;
    }
    return flag;
  }

  /// <summary>
  /// Определяет, можно ли восстановить все выделенные объекты.
  /// Все ли они на уровне продвижения Удалено.
  /// </summary>
  /// <param name="items">The items.</param>
  /// <param name="viewServices">The view services.</param>
  /// <returns></returns>
  public static bool CanRestoreObjects(ISelectedItems items, IServiceProvider viewServices)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      if (!(items.GetItemData(index, typeof (IDBLCStepID)) is IDBLCStepID itemData) || itemData.LCStepID == -1 || MetaDataHelper.GetLCStep(itemData.LCStepID).LevelID != MetaDataHelper.GetLCLevelID(new Guid("cad0000e-306c-11d8-b4e9-00304f19f545")))
        return false;
    }
    return true;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo commandsInfo = new CommandsInfo();
    commandsInfo.Add("Copy", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CopyCommand)));
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      bool flag1 = true;
      for (int index = 0; index < items.Count; ++index)
      {
        object parentData = items.GetParentData(index, typeof (INodeID));
        if (parentData == null || parentData.GetType() != typeof (ObjectGroupNodeID) && items.GetParentData(index, typeof (IDBObjectID)) == null)
        {
          flag1 = false;
          break;
        }
      }
      if (flag1)
        commandsInfo.Add("Cut", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CutCommand)));
      bool flag2 = true;
      bool flag3 = true;
      for (int index = 0; index < items.Count; ++index)
      {
        if (!(items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
        {
          flag3 = false;
          flag2 = false;
        }
        else
        {
          if (itemData.ObjectType == MetaDataHelper.GetObjectTypeID("cad0004a-306c-11d8-b4e9-00304f19f545"))
            flag3 = false;
          flag2 = flag2 && itemData != null && (itemData.BaseVersion & 1L) == 0L;
          if (!flag2)
            break;
        }
      }
      if (flag2)
        commandsInfo.Add("MakeBaseVersion", new CommandInfo(0, new ClickEventHandler(ObjectCommands.MakeBaseVersion)));
      if (flag3)
        commandsInfo.Add("Delete", new CommandInfo(0, new ClickEventHandler(ObjectCommands.DeleteCommand)));
      if (ContextCommandProvider.CanRestoreObjects(items, viewServices))
        commandsInfo.Add("RestoreObject", new CommandInfo(0, new ClickEventHandler(ObjectCommands.RestoreCommand)));
    }
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None)
    {
      commandsInfo.Add("ViewDocument", new CommandInfo(0, new ClickEventHandler(ObjectCommands.ViewCommand)));
      commandsInfo.Add("PrintDocument", new CommandInfo(0, new ClickEventHandler(ObjectCommands.PrintCommand)));
      commandsInfo.Add("PrintDocumentPDF", new CommandInfo(0, new ClickEventHandler(ObjectCommands.PrintPDFCommand)));
      if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      {
        commandsInfo.Add("OpenDocument", new CommandInfo(0, new ClickEventHandler(ObjectCommands.OpenCommand)));
        commandsInfo.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(ObjectCommands.EditCommand)));
      }
      commandsInfo.Add("OpenWith", new CommandInfo(0, new ClickEventHandler(ObjectCommands.OpenWithCommand)));
    }
    if ((viewStateFlags & ViewStateFlags.InParametersCard) == ViewStateFlags.None && items.Count == 1)
      commandsInfo.Add("ParametersCard", new CommandInfo(0, new ClickEventHandler(ObjectCommands.ParametersCardCommand)));
    new StepwiseProviderManager()
    {
      Providers = {
        (IStepwiseCommandsProvider) new CheckInOutCommandsProvider(),
        (IStepwiseCommandsProvider) new ExcludeCommandProvider()
      }
    }.CollectCommands(items, viewServices, commandsInfo);
    return commandsInfo;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo commandsInfo = new CommandsInfo();
    long viewState = viewServices.GetService(typeof (IViewState)) is IViewState service1 ? (long) service1.ViewState : 0L;
    bool flag1 = (viewState & 2L) == 2L;
    bool flag2 = (viewState & 536870913L /*0x20000001*/) == 536870913L /*0x20000001*/;
    if (items.Count == 1)
    {
      if (viewServices.GetService(typeof (ChildrenView)) is ChildrenView)
        commandsInfo.Add("CopyText", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CopyTextCommand)));
      if (!flag1)
      {
        commandsInfo.Add("ObjectHistory", new CommandInfo(0, new ClickEventHandler(ObjectCommands.ObjectLCHistoryCommand)));
        commandsInfo.Add("LocalizationCommand", new CommandInfo(0, new ClickEventHandler(ObjectCommands.LocalizationCommand)));
      }
      IMSObjectType objectType1 = items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1 ? MetaDataHelper.GetObjectType(itemData1.ObjectType) : (IMSObjectType) null;
      if (itemData1 != null && objectType1 != null && !objectType1.IsDisableManualCreate)
        commandsInfo.Add("CreateNew", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateCommand)));
      bool flag3 = itemData1 != null && MetaDataHelper.IsObjectTypeChildOf(itemData1.ObjectType, MetaDataHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545"));
      bool flag4 = itemData1 != null && MetaDataHelper.IsObjectTypeEditingContext(itemData1.ObjectType);
      bool flag5 = itemData1 != null && Utils.CreateFreeObject(itemData1.ObjectType);
      if (flag3)
      {
        commandsInfo.Add("BasedOnTemplate", new CommandInfo(2, new ClickEventHandler(ObjectCommands.BasedOnTemplate)));
        commandsInfo.Add("ActivateProject", new CommandInfo(2, new ClickEventHandler(ObjectCommands.ActivateProject)));
      }
      if (itemData1 != null & flag5 && !flag3 && objectType1 != null && !objectType1.IsDisableManualCreate)
        commandsInfo.Add("CreateProto", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreatePrototypeCommand)));
      if (items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData2 && itemData2.Value != -1L && itemData1 != null && !flag3 && objectType1 != null && !objectType1.IsDisableManualCreate)
        commandsInfo.Add("CreateLinkedProto", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateLinkedPrototypeCommand)));
      if (itemData1 != null & flag5 & flag4 && objectType1 != null && !objectType1.IsDisableManualCreate)
        commandsInfo.Add("CreateLinkedContext", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateLinkedContextCommand)));
      if (objectType1 != null && (objectType1.Options & ObjectTypeOptions.CreateSnapshots) == ObjectTypeOptions.CreateSnapshots && !SnapshotMasterForm.IsSnapshotCompositionShown)
      {
        commandsInfo.Add("CreateSnapshot", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateSnapshot)));
        commandsInfo.Add("SaveToSnapshot", new CommandInfo(0, new ClickEventHandler(ObjectCommands.SaveToSnapshot)));
      }
      if (!flag1)
      {
        IClientMetadataCache service2 = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
        IDBObjectTypeInfo objectType2 = objectType1 != null ? service2.GetObjectType(objectType1.ObjectTypeID) : (IDBObjectTypeInfo) null;
        string str = objectType1 != null ? objectType1.Guid.ToString() : string.Empty;
        if (str.Equals("cad0014e-306c-11d8-b4e9-00304f19f545") || str.Equals("cad00150-306c-11d8-b4e9-00304f19f545") || str.Equals("cad0014f-306c-11d8-b4e9-00304f19f545"))
          commandsInfo.Add("AddFolder", new CommandInfo(0, new ClickEventHandler(ObjectCommands.AddFolderCommand)));
        if (objectType2 != null && objectType2.HasPossibleChildren())
        {
          commandsInfo.Add("Add", new CommandInfo(0, new ClickEventHandler(ObjectCommands.AddCommand)));
          commandsInfo.Add("CreateInclude", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateIncludeCommand)));
          this.AddCreateInCompositionCommands(commandsInfo, objectType1.Guid);
        }
        if (objectType1 != null && objectType1.VersionsMode == ObjectVersionModes.MultiVersion)
        {
          if (!objectType1.IsDisableManualCreate)
          {
            commandsInfo.Add("CreateVersion", new CommandInfo(2, new ClickEventHandler(ObjectCommands.CreateVersionCommand)));
            commandsInfo.Add("CreateVersionAnotherType", new CommandInfo(2, new ClickEventHandler(ObjectCommands.CreateVersionAnotherType)));
          }
          if (items.GetItemData(0, typeof (ICanOpenInNewWindow)) != null)
          {
            commandsInfo.Add("ListVersions", new CommandInfo(2, new ClickEventHandler(ObjectCommands.ListVersions)));
            commandsInfo.Add("VersionHistory", new CommandInfo(2, new ClickEventHandler(ObjectCommands.VersionLCHistoryCommand)));
          }
        }
        if (this.assignSystemGuidCommandHandler != null && this.assignSystemGuidCommandHandler.IsAvailable)
          commandsInfo.Add("SetSystemGuid", new CommandInfo(0, new ClickEventHandler(this.assignSystemGuidCommandHandler.Invoke)));
        if (((IClipboard) ServicesManager.GetService(typeof (IClipboard))).GetDataObject() != null)
          commandsInfo.Add("Paste", new CommandInfo(0, new ClickEventHandler(ObjectCommands.PasteCommand)));
        bool flag6 = true;
        IDBRelationID itemData3 = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
        IDBObjectTypeID itemData4 = items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID;
        if (itemData3 != null && itemData4 != null)
        {
          if (MetaDataHelper.GetAttribute4RelationType(itemData3.RelationType, MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545")) == null)
            flag6 = false;
          IMSObjectType objectType3 = MetaDataHelper.GetObjectType(itemData4.Value);
          if (objectType3 == null || objectType3.VersionsMode != ObjectVersionModes.MultiVersion)
            flag6 = false;
        }
        commandsInfo.Add("ReplaceObjectInComposition", new CommandInfo(8, new ClickEventHandler(ObjectCommands.ReplaceObject)));
        if (flag6)
          commandsInfo.Add("ReplaceObjectVersionInComposition", new CommandInfo(8, new ClickEventHandler(ObjectCommands.ReplaceObjectVersion)));
      }
      if (!flag2)
        commandsInfo.Add("Find", new CommandInfo(0, new ClickEventHandler(ContextCommandProvider.Find)));
    }
    ICurrentUserAndRole service3 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (items.Count > 1 && !flag1)
    {
      object dataObject = ((IClipboard) ServicesManager.GetService(typeof (IClipboard))).GetDataObject();
      if (dataObject != null && dataObject is DBObjectTypedIDCollection && (!(dataObject is ICutCopy) ? 0 : ((dataObject as ICutCopy).IsCut ? 1 : 0)) == 0)
        commandsInfo.Add("Paste", new CommandInfo(0, new ClickEventHandler(ObjectCommands.PasteCommand)));
      bool flag7 = false;
      if (service3.IsAdmin && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        flag7 = true;
        int objectType = itemData.ObjectType;
        for (int index = 0; index < items.Count; ++index)
        {
          IDBTypedObjectID itemData5 = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          IDBCheckedOutByID itemData6 = items.GetItemData(index, typeof (IDBCheckedOutByID)) as IDBCheckedOutByID;
          if (itemData5 != null && itemData5.ObjectType != objectType || itemData6 != null && itemData6.CheckedOutBy != 0L)
          {
            flag7 = false;
            break;
          }
        }
      }
      if (flag7)
        commandsInfo.Add("CombineObjects", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CombineObjectsCommand)));
    }
    commandsInfo.Add("SaveToDisk", new CommandInfo(4, new ClickEventHandler(ObjectCommands.SaveToDisk)));
    bool flag8 = true;
    bool flag9 = true;
    long userId = service3.UserID;
    bool flag10 = true;
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBCheckedOutByID)) is IDBCheckedOutByID itemData && itemData.CheckedOutBy != 0L)
      {
        flag8 = false;
        if (itemData.CheckedOutBy != userId)
          flag10 = false;
      }
      else
        flag9 = false;
    }
    if (flag8)
      commandsInfo.Add("SetLifecycleStep", new CommandInfo(0, new ClickEventHandler(ObjectCommands.SetLifecycleStepCommand)));
    if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData7 && MetaDataHelper.HasObjectTypeGroupingRelTypes(itemData7.ObjectType))
      commandsInfo.Add("SetLifecycleStepChilds", new CommandInfo(2, new ClickEventHandler(ObjectCommands.SetLifecycleStepChildsCommand)));
    if (itemData7 != null & flag9 & flag10 && MetaDataHelper.IsObjectTypeChildOf(itemData7.ObjectType, MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545")) && this.HasObjectTypeAttributeType(itemData7.ObjectType, MetaDataHelper.GetAttributeTypeID("cadd9ac2-306c-11d8-b4e9-00304f19f545")))
      commandsInfo.Add("ChangeDocumentsStamp", new CommandInfo(0, new ClickEventHandler(ObjectCommands.ChangeDocumentsStamp)));
    if (itemData7 != null && this.HasObjectTypeAttributeType(itemData7.ObjectType, this.FileAttributeId))
    {
      commandsInfo.Add("AuthFilesCreate", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateAuthFilesCommand)));
      commandsInfo.Add("AuthFilesSave", new CommandInfo(0, new ClickEventHandler(ObjectCommands.SaveAuthFilesCommand)));
      if (items.Count == 1)
      {
        commandsInfo.Add("AuthFilesView", new CommandInfo(0, new ClickEventHandler(ObjectCommands.ViewAuthFilesCommand)));
        commandsInfo.Add("CompareFilesForCompareVersionObjectsMenu", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CompareFilesVersions)));
        commandsInfo.Add("CompareAuthFilesForCompareVersionObjectsMenu", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CompareAuthFilesVersions)));
      }
      if (items.Count == 2 && (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectType == (items.GetItemData(1, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectType)
      {
        commandsInfo.Add("CompareFilesForCompareObjectsMenu", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CompareFiles)));
        commandsInfo.Add("CompareAuthFilesForCompareObjectsMenu", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CompareAuthFiles)));
      }
    }
    if (itemData7 != null && MetaDataHelper.IsObjectTypeChildOf(itemData7.ObjectType, MetaDataHelper.GetObjectTypeID("cad001b3-306c-11d8-b4e9-00304f19f545")))
      commandsInfo.Add("SetVersionsRule", new CommandInfo(2, new ClickEventHandler(ObjectCommands.SetVersionsRule)));
    return commandsInfo;
  }

  /// <summary>
  /// Определяет, есть ли у типа объекта атрибут.
  /// Проверка, объявлен ли атрибут у типа объекта явно.
  /// </summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="attrType">Тип атрибута</param>
  /// <returns></returns>
  private bool HasObjectTypeAttributeType(int objectType, int attrType)
  {
    return MetaDataHelper.GetAttribute4ObjectType(objectType, attrType) != null;
  }

  /// <summary>
  /// Добавляет команды в пункт меню Состав объекта/ Создать в составе
  /// </summary>
  /// <param name="commandsInfo">Контейнер сведений о командах контекстного меню.</param>
  /// <param name="currentObjGuid">ГУИД выбранного итема.</param>
  private void AddCreateInCompositionCommands(CommandsInfo commandsInfo, Guid currentObjGuid)
  {
    commandsInfo.Add("CreateNewInComposition", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateIncludeCommand)));
    int objectTypeId = MetaDataHelper.GetObjectTypeID(currentObjGuid);
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service1))
      return;
    List<int> visibleRelations = service1.Rule.GetObjectTypeVisibleRelations(objectTypeId, true);
    if (!(ServicesManager.GetService(typeof (ICreateObjByTypeMRU)) is ICreateObjByTypeMRU service2))
      return;
    for (int index = 0; index < service2.Count; ++index)
    {
      foreach (int relTypeID in visibleRelations)
      {
        if (MetaDataHelper.GetApplicability(objectTypeId, (int) service2[index].Value, relTypeID) != null)
        {
          Tuple<int, int> additionalInfo = new Tuple<int, int>(Convert.ToInt32(service2[index].Value), relTypeID);
          commandsInfo.Add("CreateTypeInComposition" + service2[index].Caption, new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateIncludeCommand), (object) additionalInfo));
          break;
        }
      }
    }
  }

  private static void Find(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    IDescriptor rootDescriptor = (IDescriptor) new HiveDescriptor(Intermech.Navigator.Selections.Consts.SelectionTypeID, (ITopBinding) new Binding(itemData.ObjectType, itemData.ObjectID, BindingType.Selections));
    NavWindow navWindow = new NavWindow();
    navWindow.TreeView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    navWindow.TreeView.Build(rootDescriptor);
    navWindow.TreeView.RootNode.Icon = ImagesResizeHelper.ResizeIconTo32x16((ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService).GetIcon(4, itemData.ObjectType), SystemColors.ControlLight);
    navWindow.Show(Holder.DockManager);
    navWindow.Activate();
  }
}
