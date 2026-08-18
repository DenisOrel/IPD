
// Type: Intermech.Navigator.DBObjects.EditingContextsCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер команд для контекстов редактирования</summary>
public class EditingContextsCommandsProvider : ICommandsProvider
{
  /// <summary>Информация о текущем пользователе</summary>
  private static ICurrentUserAndRole _userRole;

  /// <summary>Информация о текущем пользователе</summary>
  protected internal static ICurrentUserAndRole UserRole
  {
    [DebuggerStepThrough] get
    {
      if (EditingContextsCommandsProvider._userRole == null)
        EditingContextsCommandsProvider._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      return EditingContextsCommandsProvider._userRole;
    }
  }

  /// <summary>Вернуть команды</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <returns>Команды</returns>
  public virtual CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      return CommandsInfo.Empty;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IViewState service = viewServices != null ? viewServices.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.InDialog || (viewStateFlags & ViewStateFlags.InParametersCard) == ViewStateFlags.InParametersCard)
      return CommandsInfo.Empty;
    bool flag1 = (viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.NodeInTree;
    bool flag2 = (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.NodeInViews;
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items.Count == 1 && itemData != null && MetaDataHelper.IsObjectTypeEditingContext(itemData.ObjectType))
      mergedCommands.Add("EditingContextActivate", new CommandInfo(0, new ClickEventHandler(EditingContextsCommandsProvider.EditingContextActivate)));
    if (EditingContextsCommandsProvider.UserRole.CachedEditingContextID != 0L)
    {
      if (flag1 | flag2)
        mergedCommands.Add("EditingContextAdd", new CommandInfo(0, new ClickEventHandler(EditingContextsCommandsProvider.EditingContextAdd)));
      if (flag1 && !flag2)
        mergedCommands.Add("EditingContextAddComposition", new CommandInfo(0, new ClickEventHandler(EditingContextsCommandsProvider.EditingContextAddComposition)));
      if (flag1 | flag2)
        mergedCommands.Add("EditingContextReplaceVersion", new CommandInfo(0, new ClickEventHandler(this.EditingContextReplaceVersion)));
    }
    return mergedCommands;
  }

  /// <summary>Вернуть групповые команды</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <returns>Групповые команды</returns>
  public virtual CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Активизировать контекст редактирования".
  /// </summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  protected static void EditingContextActivate(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !MetaDataHelper.IsObjectTypeEditingContext(itemData.ObjectType))
      return;
    ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    service1.EditingContextID = itemData.ObjectID;
    service1.EditingContextMode = service1.CachedContextMode;
    if (service1.CachedEditingContextSource == EditingContextSource.SessionContext || !(ServicesManager.GetService(typeof (IFiltrationService)) is IFiltrationService service2))
      return;
    service2.FiltrationApplyUpdates(true);
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Добавить в контекст".
  /// </summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  protected static void EditingContextAdd(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    EditingContextsObjectContainer currentEditingContext = EditingContextsCommandsProvider.GetEditingContextsObjectContainer4CurrentEditingContext();
    if (EditingContextsCommandsProvider.CheckEditingContextsObjectContainerOnEmptinessAndShowInformationMessageIfSuccess(currentEditingContext) || !EditingContextsCommandsProvider.CheckEditingContextEditRightAndShowInformationMessageIfFail(currentEditingContext.ContextID))
      return;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IViewState service = viewServices != null ? viewServices.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.InDialog || (viewStateFlags & ViewStateFlags.InParametersCard) == ViewStateFlags.InParametersCard)
      return;
    bool flag1 = (viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.NodeInTree;
    bool flag2 = (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.NodeInViews;
    if (itemData == null || !flag1 && !flag2)
      return;
    EditingContextsCommandsProvider.AddItems(items, EditingContextsCompositionLevel.OnlyObjects, false, currentEditingContext);
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Добавить с составом".
  /// </summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  protected static void EditingContextAddComposition(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    EditingContextsObjectContainer currentEditingContext = EditingContextsCommandsProvider.GetEditingContextsObjectContainer4CurrentEditingContext();
    if (EditingContextsCommandsProvider.CheckEditingContextsObjectContainerOnEmptinessAndShowInformationMessageIfSuccess(currentEditingContext) || !EditingContextsCommandsProvider.CheckEditingContextEditRightAndShowInformationMessageIfFail(currentEditingContext.ContextID))
      return;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IViewState service = viewServices != null ? viewServices.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.InDialog || (viewStateFlags & ViewStateFlags.InParametersCard) == ViewStateFlags.InParametersCard)
      return;
    bool flag1 = (viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.NodeInTree;
    bool flag2 = (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.NodeInViews;
    if (itemData == null || !flag1 & flag2)
      return;
    DialogResult dialogResult = IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1461"), LocalizationHolder.rm.GetString("Client.Core_1462") + LocalizationHolder.rm.GetString("Client.Core_1463"), new IMMessageBoxButton[3]
    {
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1464"), DialogResult.No),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1465"), DialogResult.Yes),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1466"), DialogResult.Cancel)
    }, IMMessageBoxImage.Question);
    switch (dialogResult)
    {
      case DialogResult.Yes:
      case DialogResult.No:
        EditingContextsCompositionLevel mode = dialogResult == DialogResult.No ? EditingContextsCompositionLevel.FirstLevel : EditingContextsCompositionLevel.AllLevels;
        EditingContextsCommandsProvider.AddItems(items, mode, false, currentEditingContext);
        break;
    }
  }

  protected virtual void EditingContextReplaceVersion(
    ISelectedItems selectedItems,
    System.IServiceProvider serviceProvider,
    object additionalInfo)
  {
    if (selectedItems == null)
      return;
    EditingContextsObjectContainer currentEditingContext = EditingContextsCommandsProvider.GetEditingContextsObjectContainer4CurrentEditingContext();
    if (EditingContextsCommandsProvider.CheckEditingContextsObjectContainerOnEmptinessAndShowInformationMessageIfSuccess(currentEditingContext) || !EditingContextsCommandsProvider.CheckEditingContextEditRightAndShowInformationMessageIfFail(currentEditingContext.ContextID))
      return;
    IDBTypedObjectID itemData = selectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    EditingContextsObjectVersion version = currentEditingContext.GetVersion(itemData.ObjectID, currentEditingContext.ContextID);
    if (!EditingContextsCommandsProvider.CheckEditingContextObjectVersionExistInEditingContextsObjectContainer(version, currentEditingContext))
    {
      EditingContextsCommandsProvider.ShowInformationMessage(LocalizationHolder.rm.GetString("EditingContext_ReplaceVersionError_VersionExistInLinkedContext"));
    }
    else
    {
      long fId = version.F_ID;
      int num = currentEditingContext.SimpleContext ? 1 : 0;
      List<long> colored = new List<long>();
      colored.Add(version.F_OBJECT_ID);
      long[] numArray = Array.Empty<long>();
      long replacementObjectVersionId = ObjectVersionSelection.SelectVersion(fId, num != 0, colored, numArray);
      if (replacementObjectVersionId == 0L || Math.Abs(replacementObjectVersionId) == Math.Abs(version.F_OBJECT_ID))
        return;
      this.BeforeReplaceObjectVersionInEditingContextObjectContainer(currentEditingContext, version.F_OBJECT_ID, replacementObjectVersionId);
      this.ReplaceObjectVersionInEditingContextObjectContainer(currentEditingContext, version.F_OBJECT_ID, replacementObjectVersionId);
      this.AfterReplaceObjectVersionInEditingContextObjectContainer(currentEditingContext, version.F_OBJECT_ID, replacementObjectVersionId);
      EditingContextsCommandsProvider.FixChangesInEditingContextObjectContainer(currentEditingContext);
      EditingContextsCommandsProvider.NotifyAllAboutVersionInEditingContextWasReplaced();
    }
  }

  /// <summary>
  /// Добавить в контекст указанные объекты, а также их составы (при необходимости)
  /// </summary>
  /// <param name="items">Список добавляемых версий объектов</param>
  /// <param name="mode">Режим добавления</param>
  /// <param name="silentMode">true - действия выполняются без диалога с пользователем</param>
  private static void AddItems(
    ISelectedItems items,
    EditingContextsCompositionLevel mode,
    bool silentMode,
    EditingContextsObjectContainer editingContextsObjectContainer)
  {
    if (items == null || items.Count == 0)
      return;
    List<IDBTypedObjectID> objects = new List<IDBTypedObjectID>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        objects.Add(itemData);
    }
    if (objects.Count <= 0)
      return;
    EditingContextsCommandsProvider.AddObjects((IList<IDBTypedObjectID>) objects, mode, silentMode, editingContextsObjectContainer);
  }

  /// <summary>
  /// Добавить в контекст указанные объекты, а также их составы (при необходимости)
  /// </summary>
  /// <param name="objects">Список добавляемых версий объектов</param>
  /// <param name="mode">Режим добавления</param>
  /// <param name="silentMode">true - действия выполняются без диалога с пользователем</param>
  private static void AddObjects(
    IList<IDBTypedObjectID> objects,
    EditingContextsCompositionLevel mode,
    bool silentMode,
    EditingContextsObjectContainer editingContextsObjectContainer)
  {
    if (objects == null || objects.Count == 0 || EditingContextsCommandsProvider.UserRole.CachedEditingContextID == 0L)
      return;
    EditingContextsLog log = new EditingContextsLog();
    int num1 = 0;
    int num2 = 0;
    bool flag1 = false;
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    long num3 = 0;
    string format = LocalizationHolder.rm.GetString("Client.Core_1449") + LocalizationHolder.rm.GetString("Client.Core_1450") + LocalizationHolder.rm.GetString("Client.Core_1451");
    ProgressForm progressForm = !silentMode ? ProgressForm.Execute(LocalizationHolder.rm.GetString("Client.Core_1452"), string.Format(format, (object) objects.Count, (object) 0), 0, objects.Count, false, string.Empty, (EventHandler) null) : (ProgressForm) null;
    try
    {
      ObjectVersionDescription versionDescription = new ObjectVersionDescription();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
        for (int index1 = 0; index1 < objects.Count; ++index1)
        {
          if (progressForm != null)
          {
            progressForm.Maximum = objects.Count;
            if ((index1 + 1) % 50 == 0)
              progressForm.SetProgressValue(index1, string.Format(format, (object) objects.Count, (object) (index1 + 1)));
          }
          IDBTypedObjectID dbTypedObjectId1 = objects[index1];
          bool flag2 = false;
          IMSObjectType objectType = MetaDataHelper.GetObjectType(dbTypedObjectId1.ObjectType);
          bool flag3 = objectType != null && MetaDataHelper.IsObjectTypeChildOf(objectType.ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
          bool simpleContext = editingContextsObjectContainer.SimpleContext;
          bool flag4 = false;
          if (!flag4 && editingContextsObjectContainer.ExistsVersion(dbTypedObjectId1.ObjectID, false))
          {
            log.Add(EditingContextsLogError.ExistsVersion, dbTypedObjectId1.ObjectID);
            flag4 = true;
          }
          if (editingContextsObjectContainer.ExistsObject(dbTypedObjectId1.ID) && !editingContextsObjectContainer.ExistsVersion(dbTypedObjectId1.ObjectID, true))
          {
            log.Add(EditingContextsLogError.ExistsAnotherVersionLinked, dbTypedObjectId1.ObjectID);
            flag4 = true;
          }
          if (!flag4 && MetaDataHelper.IsObjectTypeEditingContext(dbTypedObjectId1.ObjectType))
          {
            log.Add(EditingContextsLogError.IsEditingContext, dbTypedObjectId1.ObjectID);
            flag4 = true;
          }
          if (!flag4 && !flag3 && (objectType == null || objectType.VersionsMode != ObjectVersionModes.MultiVersion))
            log.Add(EditingContextsLogError.NonversionObject, dbTypedObjectId1.ObjectID);
          if (editingContextsObjectContainer.ExistsObject(dbTypedObjectId1.ID) && !editingContextsObjectContainer.ExistsLinkedVersion(dbTypedObjectId1.ObjectID) || editingContextsObjectContainer.ExistsVersion(dbTypedObjectId1.ObjectID, false) || MetaDataHelper.IsObjectTypeEditingContext(dbTypedObjectId1.ObjectType) || !flag3 && (objectType == null || objectType.VersionsMode != ObjectVersionModes.MultiVersion))
          {
            longList1.Add(Math.Abs(dbTypedObjectId1.ObjectID));
            flag2 = true;
            if (mode == EditingContextsCompositionLevel.OnlyObjects)
            {
              ++num1;
              continue;
            }
          }
          if (longList1.Contains(Math.Abs(dbTypedObjectId1.ObjectID)))
            flag2 = true;
          EditingContextsObjectVersion newVersion = new EditingContextsObjectVersion(editingContextsObjectContainer.ContextID, 0L, 0L, Math.Abs(editingContextsObjectContainer.ModificationID));
          if (!flag2)
          {
            if (!simpleContext && dbTypedObjectId1.ModificationID != 0L && Math.Abs(dbTypedObjectId1.ModificationID) != Math.Abs(editingContextsObjectContainer.ModificationID))
            {
              log.Add(EditingContextsLogError.ExistsAnotherVersion, dbTypedObjectId1.ObjectID);
              ++num1;
              continue;
            }
            newVersion.F_ID = dbTypedObjectId1.ID;
            newVersion.F_OBJECT_ID = dbTypedObjectId1.ObjectID;
          }
          bool flag5 = !flag2 && editingContextsObjectContainer.AddVersion(newVersion, (ObjectVersionDescription) null);
          if (flag5)
          {
            longList1.Add(dbTypedObjectId1.ObjectID);
            ++num2;
          }
          else
            ++num1;
          if (mode == EditingContextsCompositionLevel.FirstLevel && dbTypedObjectId1.Owner != 0L || mode == EditingContextsCompositionLevel.AllLevels)
          {
            if (longList2.Contains(Math.Abs(dbTypedObjectId1.ObjectID)))
            {
              ++num3;
            }
            else
            {
              longList2.Add(Math.Abs(dbTypedObjectId1.ObjectID));
              if (customService != null)
              {
                List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
                columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.ASC, 0));
                columns.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.ASC, 1));
                columns.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
                columns.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
                columns.Add(new ColumnDescriptor((object) -5, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
                columns.Add(new ColumnDescriptor((object) -16, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
                columns.Add(new ColumnDescriptor((object) -17, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
                columns.Add(new ColumnDescriptor((object) -15, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
                IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
                DataTable dataTable = customService.LoadCompositions((object) sessionKeeper.Session.SessionGUID, dbTypedObjectId1.ObjectID, (IEnumerable<ColumnDescriptor>) columns, service.FiltrationServiceOwnerID);
                if (dataTable != null)
                {
                  for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                  {
                    DataRow row = dataTable.Rows[index2];
                    DBTypedObjectID dbTypedObjectId2 = new DBTypedObjectID(DataSetProcessor.GetInt32Value(row, 0, -1), DataSetProcessor.GetInt64Value(row, 2, -1L), DataSetProcessor.GetInt64Value(row, 1, 0L), string.Empty, 0L, DataSetProcessor.GetInt64Value(row, 4, 0L), DataSetProcessor.GetInt64Value(row, 5, 0L), DataSetProcessor.GetStringValue(row, 6, string.Empty), DataSetProcessor.GetInt64Value(row, 7, 0L));
                    if (dbTypedObjectId2.ObjectID != 0L && dbTypedObjectId2.ID != 0L && dbTypedObjectId2.ObjectType != -1)
                      objects.Add((IDBTypedObjectID) dbTypedObjectId2);
                  }
                  dataTable.Dispose();
                }
              }
            }
          }
          flag1 |= flag5;
        }
      }
    }
    finally
    {
      if (progressForm != null)
      {
        progressForm.CanCloseForm = true;
        progressForm.Close();
        progressForm.Dispose();
      }
    }
    if (flag1)
    {
      editingContextsObjectContainer.ClearCacheTables();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService)
          customService.SetEditingContextsObject((object) sessionKeeper.Session.SessionGUID, editingContextsObjectContainer, true);
      }
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", editingContextsObjectContainer.ContextID));
      if (EditingContextsCommandsProvider.UserRole.CachedEditingContextID == editingContextsObjectContainer.ContextID)
        EditingContextsCommandsProvider.UserRole.EditingContextID = editingContextsObjectContainer.ContextID;
    }
    if (silentMode)
      return;
    IMMessageBoxButton[] messageBoxButtonArray;
    if (log.Count != 0)
      messageBoxButtonArray = new IMMessageBoxButton[2]
      {
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.OK),
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1453"), DialogResult.Yes)
      };
    else
      messageBoxButtonArray = new IMMessageBoxButton[1]
      {
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.OK)
      };
    IMMessageBoxButton[] Buttons = messageBoxButtonArray;
    if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1317"), string.Format(LocalizationHolder.rm.GetString("Client.Core_1454"), (object) num2, (object) num1), Buttons, IMMessageBoxImage.Information) != DialogResult.Yes)
      return;
    EditingContextsEventLogForm.Execute(log);
  }

  private static EditingContextsObjectContainer GetEditingContextsObjectContainer4CurrentEditingContext()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      return (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetEditingContextsObject((object) session.SessionGUID, EditingContextsCommandsProvider.UserRole.CachedEditingContextID, true, true);
    }
  }

  private static bool CheckEditingContextsObjectContainerOnEmptinessAndShowInformationMessageIfSuccess(
    EditingContextsObjectContainer editingContextsObjectContainer)
  {
    int num = EditingContextsCommandsProvider.CheckEditingContextsObjectContainerOnEmptiness(editingContextsObjectContainer) ? 1 : 0;
    if (num == 0)
      return num != 0;
    EditingContextsCommandsProvider.ShowInformationMessage(LocalizationHolder.rm.GetString("EditingContext_ContextIsEmpty"));
    return num != 0;
  }

  private static bool CheckEditingContextsObjectContainerOnEmptiness(
    EditingContextsObjectContainer editingContextsObjectContainer)
  {
    return editingContextsObjectContainer == null || editingContextsObjectContainer.ContextID == 0L;
  }

  private static bool CheckEditingContextEditRightAndShowInformationMessageIfFail(
    long editingContextVersionID)
  {
    int num = EditingContextHelper.CheckEditingContextEditRight(editingContextVersionID) ? 1 : 0;
    if (num != 0)
      return num != 0;
    EditingContextsCommandsProvider.ShowInformationMessage(LocalizationHolder.rm.GetString("EditingContext_ForbiddenAccess"));
    return num != 0;
  }

  private static bool CheckEditingContextObjectVersionExistInEditingContextsObjectContainer(
    EditingContextsObjectVersion editingContextsObjectVersion,
    EditingContextsObjectContainer editingContextsObjectContainer)
  {
    return editingContextsObjectVersion != null && Math.Abs(editingContextsObjectVersion.F_CONTEXT_ID) == Math.Abs(editingContextsObjectContainer.ContextID);
  }

  private static void ShowInformationMessage(string message)
  {
    int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1317"), message, new IMMessageBoxButton[1]
    {
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.OK)
    }, IMMessageBoxImage.Information);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editingContextsObjectContainer"></param>
  /// <param name="objectVersionId"></param>
  /// <param name="replacementObjectVersionId"></param>
  protected virtual void BeforeReplaceObjectVersionInEditingContextObjectContainer(
    EditingContextsObjectContainer editingContextsObjectContainer,
    long objectVersionId,
    long replacementObjectVersionId)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editingContextsObjectContainer"></param>
  /// <param name="objectVersionId"></param>
  /// <param name="replacementObjectVersionId"></param>
  protected virtual void AfterReplaceObjectVersionInEditingContextObjectContainer(
    EditingContextsObjectContainer editingContextsObjectContainer,
    long objectVersionId,
    long replacementObjectVersionId)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editingContextsObjectContainer"></param>
  /// <param name="objectVersionId"></param>
  /// <param name="replacementObjectVersionId"></param>
  protected virtual void ReplaceObjectVersionInEditingContextObjectContainer(
    EditingContextsObjectContainer editingContextsObjectContainer,
    long objectVersionId,
    long replacementObjectVersionId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(ObjectVersionDescriptionsHelper.LoadDescription(sessionKeeper.Session, typeof (ObjectVersionDescription), replacementObjectVersionId) is ObjectVersionDescription newVerDesc))
        return;
      EditingContextsObjectVersion newVersion = new EditingContextsObjectVersion(editingContextsObjectContainer.ContextID, newVerDesc.F_ID, Math.Abs(newVerDesc.F_OBJECT_ID), editingContextsObjectContainer.ModificationID);
      editingContextsObjectContainer.ReplaceVersion(objectVersionId, newVersion, newVerDesc);
    }
  }

  private static void FixChangesInEditingContextObjectContainer(
    EditingContextsObjectContainer editingContextsObjectContainer)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).SetEditingContextsObject((object) session.SessionGUID, editingContextsObjectContainer, true);
    }
  }

  private static void NotifyAllAboutVersionInEditingContextWasReplaced()
  {
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, new NotificationEventArgs("ObjectTypeAndRelationFiltrationChanged"));
  }
}
