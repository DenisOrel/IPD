
// Type: Intermech.Commands.CreateItemsVersionsCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Intermech.Commands;

/// <summary>
/// Реализация команды контекстного меню "Создать версию".
/// Она создает версии объектов, используя <see cref="T:Intermech.Navigator.Interfaces.ISelectedItems" /> в качестве источника данных.
/// </summary>
public sealed class CreateItemsVersionsCommand : SelectedItemsCommand
{
  private IObjectCreatorService objectCreatorService;
  private List<ObjectCreatedInfo> result;

  public CreateItemsVersionsCommand()
    : base("CreateItemsVersions")
  {
    this.DisplayName = "Создать версию";
    this.objectCreatorService = (IObjectCreatorService) ServicesManager.GetService(typeof (IObjectCreatorService));
    this.result = new List<ObjectCreatedInfo>();
  }

  /// <summary>
  /// Возвращает список описателей для созданных версий объектов IPS.
  /// Список может быть пуст, если ни одна версия не была создана (так как пользователь отказался их создавать)
  /// </summary>
  public List<ObjectCreatedInfo> Result
  {
    [DebuggerStepThrough] get => this.result;
  }

  /// <summary>Выполняет команду.</summary>
  protected override void DoExecute()
  {
    ViewStateFlags viewStateFlags = this.GetViewStateFlags();
    this.result.Clear();
    for (int itemIndex = 0; itemIndex < this.Items.Count; ++itemIndex)
    {
      ObjectCreatedInfo itemVersion = this.TryCreateItemVersion(itemIndex, viewStateFlags);
      if (itemVersion != null)
        this.result.Add(itemVersion);
    }
  }

  private ObjectCreatedInfo TryCreateItemVersion(int itemIndex, ViewStateFlags viewStateFlags)
  {
    long newObjectID = this.objectCreatorService.CreateObjectVersionByTemplateDialog((this.Items.GetItemData(itemIndex, typeof (IDBObjectID)) as IDBObjectID).Value);
    if (Consts.IsUndefinedObjectId(newObjectID))
      return (ObjectCreatedInfo) null;
    ObjectCreatedInfo newObjectInfo = this.objectCreatorService.GetObjectCreatedInfo().First<ObjectCreatedInfo>((Func<ObjectCreatedInfo, bool>) (x => x.ObjectId == newObjectID));
    AfterObjectCreatorDialogHandlers.Handle(newObjectID, itemIndex, this.Items, this.ContextServices, this.AdditionalInfo);
    if (this.UpdateUI)
      this.QueueUINotifications(newObjectInfo, itemIndex, viewStateFlags);
    return newObjectInfo;
  }

  private ViewStateFlags GetViewStateFlags()
  {
    IViewState service = (IViewState) this.ContextServices.GetService(typeof (IViewState));
    return service == null ? ViewStateFlags.None : service.ViewState;
  }

  private void QueueUINotifications(
    ObjectCreatedInfo newObjectInfo,
    int itemIndex,
    ViewStateFlags viewStateFlags)
  {
    this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", newObjectInfo.ObjectId, newObjectInfo.ObjectTypeId));
    if ((viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None || !(this.Items.GetItemData(itemIndex, typeof (IDBRelationID)) is IDBRelationID itemData))
      return;
    this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", itemData.Value, itemData.ProjID, itemData.RelationType));
    this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", itemData.Value, itemData.ProjID, itemData.RelationType));
  }
}
