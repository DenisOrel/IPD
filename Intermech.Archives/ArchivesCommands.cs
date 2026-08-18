// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesCommands
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives;

internal class ArchivesCommands
{
  public static void CreateNewCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (viewServices != null && viewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service1)
      service1.CanRestoreFocusedNode = false;
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service2))
      return;
    int[] array = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ConstsHolder.ArcTypeID).ToArray();
    long objectByTypeDialog = service2.CreateObjectByTypeDialog(array);
    if (objectByTypeDialog.Equals(-1L))
      return;
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
  }

  /// <summary>вставить объект</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void PasteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service1))
      return;
    IDBObjectTypedIDCollection dataObject = service1.GetDataObject() as IDBObjectTypedIDCollection;
    INotificationService service2 = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (dataObject != null && dataObject.Count == 1)
    {
      string format = ServiceHolder.rm.GetString("Archives_149");
      IDBTypedObjectID typedObjectId = dataObject.GetTypedObjectID(0);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ClipboardObject clipboardObject = typedObjectId as ClipboardObject;
        IDBRelation relation = sessionKeeper.Session.GetRelation(clipboardObject.Value, false);
        if (relation != null)
        {
          long projId = relation.ProjID;
          IDBObject dbObject = sessionKeeper.Session.GetObject(projId);
          sessionKeeper.Session.StartLogHistory();
          if (MessageBox.Show(string.Format(format, (object) typedObjectId.Caption, (object) dbObject.Caption), ServiceHolder.rm.GetString("Archives_150"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
          {
            relation.Delete(0L);
            (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).RefreshImage();
            if (ApplicationServices.Container.GetService(typeof (ArchiveHierarchyService)) is ArchiveHierarchyService service3)
              service3.AddArchiveToCashe(clipboardObject.PartID, 0L, ConstsHolder.ArcTypeID);
            service2.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", relation.RelationID));
            service2.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", typedObjectId.ObjectID));
          }
        }
      }
    }
    service1.RemoveCurrentDataObject();
  }
}
