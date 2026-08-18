
// Type: Intermech.Commands.UndoChangedItemsCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Commands;

public class UndoChangedItemsCommand : ExtendedSelectedItemsCommand
{
  public UndoChangedItemsCommand()
    : base("CancelItems")
  {
  }

  protected override void DoExecute()
  {
    ChangingObjects chObjects = new ChangingObjects();
    for (int index = 0; index < this.Items.Count; ++index)
    {
      if (this.Items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && itemData.ObjectID < 0L && chObjects.FindChangingObjectFromRoot(itemData.ObjectID) == null)
        chObjects.Add(itemData.ObjectID, Math.Abs(itemData.ObjectID), ObjectChangingAction.CancelChanges, true, false);
    }
    ChangingAnalyzerJobStatus analyzerJobStatus = ChangingAnalyzeForm.Execute(ObjectChangingAction.CancelChanges, chObjects);
    if (analyzerJobStatus == null || analyzerJobStatus.Progress == ChangingAnalyzerJobProgress.Cancelled || analyzerJobStatus.Progress == ChangingAnalyzerJobProgress.Working)
      return;
    if (analyzerJobStatus.Progress == ChangingAnalyzerJobProgress.Error)
    {
      if (analyzerJobStatus.Exception == null)
        return;
      ExceptionHelper.ExceptionService.ShowException(analyzerJobStatus.Exception);
    }
    else
    {
      if (ChangingObjectsForm.Execute(this.ContextServices, ObjectChangingAction.CancelChanges, analyzerJobStatus.Items) != DialogResult.Yes)
        return;
      List<long> ids = analyzerJobStatus.Items.ExtractIDs();
      if (ids.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<long> objectIDs = new List<long>();
        this.DoBeforeProceedItems(sessionKeeper.Session);
        try
        {
          for (int index = 0; index < ids.Count; ++index)
          {
            this.DoBeforeProceedItem(index);
            try
            {
              ObjectCopyCommand cancelChangesCommand = ObjectCommandFactory.CreateCancelChangesCommand(true);
              cancelChangesCommand.ObjectId = ids[index];
              cancelChangesCommand.UpdateUI = false;
              cancelChangesCommand.Execute();
              objectIDs.Add(ids[index]);
              RecentObjectsNode.MRUObjects.Add(!ObjectHelper.IsUnknownObjectVersionID(cancelChangesCommand.NewObjectId) ? cancelChangesCommand.NewObjectId : cancelChangesCommand.ObjectId, ObjectAction.CancelChanges, DateTime.UtcNow);
            }
            finally
            {
              this.DoAfterProceedItem(index);
            }
          }
        }
        finally
        {
          if (objectIDs.Count > 0)
          {
            DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs);
            Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
          }
          this.DoAfterProceedItems(sessionKeeper.Session);
        }
      }
    }
  }
}
