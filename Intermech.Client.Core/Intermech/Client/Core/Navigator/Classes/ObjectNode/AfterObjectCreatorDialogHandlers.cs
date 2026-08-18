
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.AfterObjectCreatorDialogHandlers
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

/// <summary>
/// Класс содержит коллекцию обработчиков, вызываемых после закрытия формы создания объектов
/// </summary>
public static class AfterObjectCreatorDialogHandlers
{
  private static IAfterObjectCreatorDialogHandler[] _handlers = new IAfterObjectCreatorDialogHandler[2]
  {
    (IAfterObjectCreatorDialogHandler) new ProjectHandler(),
    (IAfterObjectCreatorDialogHandler) new HandSelectionHandler()
  };

  public static void Handle(
    long newObjectID,
    int itemIndex,
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (Intermech.Consts.IsUndefinedObjectId(newObjectID) || items == null || items.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject newObject = sessionKeeper.Session.GetObject(newObjectID);
      bool flag = false;
      for (int index = 0; index < AfterObjectCreatorDialogHandlers._handlers.Length; ++index)
      {
        if (AfterObjectCreatorDialogHandlers._handlers[index].Handle(newObject, itemIndex, items, viewServices, additionalInfo))
          flag = true;
      }
      if (!flag)
        return;
      DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", newObjectID);
      Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    }
  }
}
