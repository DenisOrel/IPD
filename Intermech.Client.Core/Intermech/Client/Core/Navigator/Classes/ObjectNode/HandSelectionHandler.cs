
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.HandSelectionHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections.Implementation;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

/// <summary>Создание происходит в контексте ручной выборки</summary>
internal class HandSelectionHandler : IAfterObjectCreatorDialogHandler
{
  public bool Handle(
    IDBObject newObject,
    int itemIndex,
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items != null && items.Count > 0)
    {
      NodeIDPath parentPath = items.GetParentPath(itemIndex);
      for (int Index = 0; Index < parentPath.Length; ++Index)
      {
        if (parentPath[Index] is SelectionNodeID selectionNodeId)
        {
          if (selectionNodeId.HandSelection && MetaDataHelper.GetObjectTypeChildrenIDRecursive(selectionNodeId.BindedObjectTypeID).Contains(newObject.ObjectType) && MessageBox.Show($"Включить {newObject.NameInMessages} в ручную выборку {selectionNodeId.Caption}?", "Включение в ручную выборку", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(selectionNodeId.ObjectID);
              ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).IncludeObjects((object) sessionKeeper.Session, dbObject.ObjectGUID, new long[1]
              {
                newObject.ObjectID
              });
              DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", dbObject.ObjectID);
              Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
              break;
            }
          }
          break;
        }
      }
    }
    return false;
  }
}
