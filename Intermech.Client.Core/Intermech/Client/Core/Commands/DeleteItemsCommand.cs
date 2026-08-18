
// Type: Intermech.Client.Core.Commands.DeleteItemsCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Client.Core.Commands;

/// <summary>Реализация команды контекстного меню "Удалить"</summary>
/// <summary>Конструктор</summary>
public class DeleteItemsCommand(string name = "Delete") : ExtendedSelectedItemsCommand(name)
{
  /// <summary>Коллекция описаний удаляемых объектов</summary>
  protected DeletingObjects _deletingObjects;
  /// <summary>Опции для окна "Удаление объектов" по умолчанию</summary>
  protected DeleteAnalyzerOptions _deleteOptions;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  protected virtual bool CouldDeleteItemObject(int itemIndex)
  {
    return this.Items.GetItemData(itemIndex, typeof (IDBTypedObjectID)) is IDBTypedObjectID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="itemIndex"></param>
  /// <returns></returns>
  protected virtual bool CouldDeleteItemRelation(int itemIndex)
  {
    if (!(this.Items.GetItemData(itemIndex, typeof (IDBRelationID)) is IDBRelationID))
      return false;
    return !(this.ContextServices.GetService(typeof (VersionsRule)) is VersionsRule service) || service.CurrentRuleType != VersionsRuleType.vrtAllVersionsRule;
  }

  /// <summary>Получение списка удаляемых объектов</summary>
  /// <returns></returns>
  protected virtual bool GetDeletingObjects()
  {
    ISelectedItems items = this.Items;
    System.IServiceProvider contextServices = this.ContextServices;
    if (items == null || contextServices == null)
      return false;
    this._deletingObjects = new DeletingObjects();
    for (int index = 0; index < items.Count; ++index)
    {
      if (this.CouldDeleteItemObject(index) && items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData3)
      {
        DeletingObject deletingObject = this._deletingObjects.FindDeletingObjectFromRoot(itemData3.ObjectID);
        if (deletingObject == null)
        {
          IDBLCStepID itemData1 = items.GetItemData(index, typeof (IDBLCStepID)) as IDBLCStepID;
          IDBCheckedOutByID itemData2 = items.GetItemData(index, typeof (IDBCheckedOutByID)) as IDBCheckedOutByID;
          deletingObject = this._deletingObjects.Add(0L, itemData3.ID, itemData3.ObjectID, true, itemData3.ObjectType, itemData3.Caption, itemData3.Owner, itemData2 != null ? itemData2.CheckedOutBy : 0L, itemData1 != null ? itemData1.LCStepID : -1, 0L, itemData3.Version, (itemData3.BaseVersion & 1L) == 1L, string.Empty);
        }
        if (this.CouldDeleteItemRelation(index))
        {
          IDBRelationID dbRelationId = !(items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData) || itemData.Value == 0L || itemData.Value == -1L ? (IDBRelationID) null : itemData;
          if (dbRelationId != null && !deletingObject.PrjLinkIDs.Contains(dbRelationId.Value))
            deletingObject.PrjLinkIDs.Add(dbRelationId.Value);
        }
      }
    }
    return true;
  }

  /// <summary>Анализ удаляемой информации</summary>
  /// <param name="jobStatus"></param>
  /// <returns></returns>
  protected virtual bool AnalyzeDeletingObjects(out DeleteAnalyzerJobStatus jobStatus)
  {
    jobStatus = (DeleteAnalyzerJobStatus) null;
    if (this._deletingObjects == null)
      return false;
    DeleteAnalyzerJobStatus analyzerJobStatus;
    while (true)
    {
      DialogResult dialogResult = DeleteObjectsForm.Execute(this.ContextServices, this._deletingObjects, ref this._deleteOptions);
      switch (dialogResult)
      {
        case DialogResult.Yes:
        case DialogResult.No:
          if (dialogResult == DialogResult.No)
          {
            for (int index = 0; index < this._deletingObjects.Count; ++index)
              this._deletingObjects[index].Items.Clear();
            analyzerJobStatus = DeleteAnalyzerForm.Execute(this._deletingObjects, this.DeleteOptions);
            if (analyzerJobStatus != null && analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Cancelled && analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Working)
            {
              if (analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Error)
              {
                this._deletingObjects = analyzerJobStatus.Items;
                continue;
              }
              goto label_11;
            }
            goto label_9;
          }
          goto label_15;
        default:
          goto label_3;
      }
    }
label_3:
    return false;
label_9:
    return false;
label_11:
    if (analyzerJobStatus.Exception != null)
      ExceptionHelper.ExceptionService.ShowException(analyzerJobStatus.Exception);
    return false;
label_15:
    return true;
  }

  /// <summary>Удаление данных</summary>
  /// <param name="jobStatus"></param>
  protected virtual bool PurgeDeletingObjects(out DeleteObjectsJobStatus jobStatus)
  {
    jobStatus = (DeleteObjectsJobStatus) null;
    if (this._deletingObjects == null)
      return false;
    try
    {
      jobStatus = DeleteProgressForm.Execute(this._deletingObjects);
      if (jobStatus == null || jobStatus.Progress == DeleteObjectsJobProgress.Cancelled || jobStatus.Progress == DeleteObjectsJobProgress.Working)
        return false;
      if (jobStatus.Progress == DeleteObjectsJobProgress.Error)
      {
        if (jobStatus.Exception != null)
          ExceptionHelper.ExceptionService.ShowException(jobStatus.Exception);
        return false;
      }
    }
    finally
    {
      if (jobStatus != null)
      {
        if (jobStatus.Relations != null && jobStatus.Relations.Count > 0)
          this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) jobStatus.Relations, (IList<long>) jobStatus.RelationsProjIDs, (IList<int>) null, (IList<int>) jobStatus.RelationsTypeIDs));
        if (jobStatus.Items != null && jobStatus.Items.Count > 0)
          this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) jobStatus.Items));
      }
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    if (!this.GetDeletingObjects() || !this.AnalyzeDeletingObjects(out DeleteAnalyzerJobStatus _))
      return;
    this.DeleteOptions &= ~DeleteAnalyzerOptions.FindAllVersions;
    this.PurgeDeletingObjects(out DeleteObjectsJobStatus _);
  }

  /// <summary>Опции для окна "Удаление объектов" по умолчанию</summary>
  public DeleteAnalyzerOptions DeleteOptions
  {
    get => this._deleteOptions;
    set => this._deleteOptions = value;
  }
}
