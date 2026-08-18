// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientSelectionService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Cлужба для работы с выборками</summary>
public class ClientSelectionService : Intermech.Interfaces.SelectionService.SelectionService
{
  public ClientSelectionService(INotificationService notifService)
    : base(true)
  {
    if (notifService == null)
      throw new ArgumentNullException(nameof (notifService));
    notifService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.SelectionAttributeChanged));
  }

  public void SelectionAttributeChanged(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs))
      return;
    int[] array = new int[2]
    {
      MetaDataHelper.GetObjectTypeID(new Guid("cad00122-306c-11d8-b4e9-00304f19f545")),
      MetaDataHelper.GetObjectTypeID(new Guid("cad00123-306c-11d8-b4e9-00304f19f545"))
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
      {
        if (Array.IndexOf<int>(array, objectsEventArgs.ObjectTypeIDs[index]) >= 0)
          this.UpdateCashe((object) sessionKeeper.Session, objectsEventArgs.ObjectIDs[index]);
      }
    }
  }
}
