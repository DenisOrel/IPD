
// Type: Intermech.Navigator.DBObjects.EditingContextHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.Navigator.DBObjects;

/// <remarks>Вынести в отдельный файл</remarks>
internal static class EditingContextHelper
{
  public static bool CheckEditingContextEditRight(long editingContextVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(Math.Abs(editingContextVersionID), false);
      return objectActualCopy != null && EditingContextHelper.CheckEditingContextEditRight(objectActualCopy);
    }
  }

  /// <remarks>Код вынесен из EditingContextEditor</remarks>
  public static bool CheckEditingContextEditRight(IDBObject editingContextDBObject)
  {
    if (editingContextDBObject == null)
      throw new ArgumentNullException();
    bool flag = false;
    if (editingContextDBObject is IDBSecurity dbSecurity && dbSecurity.CheckAccess(ActionType.Edit, true, false))
      flag = true;
    if (editingContextDBObject.ObjectModifyMode == ObjectModifyModes.CantModify)
      flag = false;
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (editingContextDBObject.ObjectModifyMode == ObjectModifyModes.Checkout && editingContextDBObject.CheckoutBy != service.UserID && editingContextDBObject.CheckoutBy != 0L)
      flag = false;
    if (editingContextDBObject.ObjectModifyMode == ObjectModifyModes.CreateVersion && editingContextDBObject.CheckoutBy != service.UserID && editingContextDBObject.CheckoutBy != 0L)
      flag = false;
    return flag;
  }
}
