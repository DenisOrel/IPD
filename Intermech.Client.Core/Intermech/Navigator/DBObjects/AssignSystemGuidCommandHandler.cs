
// Type: Intermech.Navigator.DBObjects.AssignSystemGuidCommandHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Обработчик команды "Назначить системный GUID" для объектов IPS.
/// </summary>
internal sealed class AssignSystemGuidCommandHandler
{
  private bool isGuidServicePresent;

  /// <summary>Выполняет инициализацию обработчика.</summary>
  /// <param name="session">Сессия сервера приложений</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="session" /> содержит null</exception>
  public void Initialize(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    this.isGuidServicePresent = session.GetCustomService(typeof (IGuidService)) != null;
  }

  /// <summary>Возвращает признак доступности команды.</summary>
  public bool IsAvailable => this.isGuidServicePresent;

  /// <summary>Выполняет команду "Назначить системный GUID"</summary>
  public void Invoke(ISelectedItems items, System.IServiceProvider viewServices, object additionalInfo)
  {
    if (!this.isGuidServicePresent)
      throw new NotSupportedException();
    if (MessageBox.Show("Назначить сиcтемный GUID?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectId = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(objectId);
      IGuidService customService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      string nameInMessages = dbObject1.NameInMessages;
      string empty = string.Empty;
      IDBObject dbObject2 = dbObject1.CheckoutBy != 0L ? dbObject1 : dbObject1.CheckOut(false);
      if (!SystemGUIDs.IsSystemGUID(dbObject1.GUID))
        dbObject1.GUID = customService.GenerateNextSystemGuid(2, nameInMessages, empty);
      if (!SystemGUIDs.IsSystemGUID(dbObject1.ObjectGUID))
        dbObject1.ObjectGUID = customService.GenerateNextSystemGuid(1, nameInMessages, empty);
      if (dbObject1.CheckoutBy != 0L)
        return;
      dbObject2.CheckIn();
    }
  }
}
