
// Type: Intermech.Client.Core.AfterCreateContextActions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Класс позволяет связать вновь созданный контекст редактирования с активным контекстом
/// </summary>
public class AfterCreateContextActions
{
  /// <summary>Ссылка на службу по созданию новых объектов</summary>
  private static IObjectCreatorService _creatorService;
  /// <summary>
  /// Ссылка на службу информации о текущем пользователе и его роли
  /// </summary>
  private static ICurrentUserAndRole _userRole;
  /// <summary>Идентификатор типа объекта "Контекст редактирования"</summary>
  private static int _contextTypeID = -1;

  /// <summary>
  /// Создать экземпляр класса, подписаться на событие у службы
  /// </summary>
  public AfterCreateContextActions()
  {
    if (AfterCreateContextActions._creatorService != null)
      return;
    AfterCreateContextActions._contextTypeID = MetaDataHelper.GetObjectTypeID("cad0146b-306c-11d8-b4e9-00304f19f545");
    AfterCreateContextActions._creatorService = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    AfterCreateContextActions._creatorService.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.NewObjectCreated);
    AfterCreateContextActions._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
  }

  /// <summary>Создан новый экземпляр объекта</summary>
  /// <param name="sender">Ссылка на экземпляр создателя объекта</param>
  /// <param name="ea">Аргументы события</param>
  internal void NewObjectCreated(object sender, AfterObjectCreatedEventArgs ea)
  {
    if (ea.ObjectTypeID != AfterCreateContextActions._contextTypeID || AfterCreateContextActions._userRole == null || AfterCreateContextActions._userRole.CachedEditingContextID == 0L)
      return;
    this.CorrectContext(ea.ObjectID, ea.PrototypeId);
  }

  /// <summary>Внести корректировку в новый контекст редактирования</summary>
  /// <param name="contextID">Новый контекст</param>
  /// <param name="prototypeID">Контекст-прототип</param>
  internal void CorrectContext(long contextID, long prototypeID)
  {
    long contextModificationId = AfterCreateContextActions._userRole.CachedEditingContextModificationID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo1 = sessionKeeper.Session.GetObjectInfo(contextID);
      QuickObjectInfo objectInfo2 = sessionKeeper.Session.GetObjectInfo(AfterCreateContextActions._userRole.CachedEditingContextID);
      if (objectInfo1.Empty || MetaDataHelper.IsSimpleEditingContext(objectInfo1.ObjectTypeID) || objectInfo2.Empty || objectInfo2.ObjectID == contextID)
        return;
      if (MetaDataHelper.IsSimpleEditingContext(objectInfo2.ObjectTypeID))
        return;
    }
    if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1317"), LocalizationHolder.rm.GetString("Client.Core_1539"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObject(contextID) is IDBEditingContextsObject editingContextsObject))
        return;
      editingContextsObject.LinkedContextNumber = Math.Abs(contextModificationId);
    }
  }
}
