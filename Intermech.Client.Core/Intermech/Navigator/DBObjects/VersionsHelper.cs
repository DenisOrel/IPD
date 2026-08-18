
// Type: Intermech.Navigator.DBObjects.VersionsHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Вспомогательные статические методы для окна версий объектов
/// </summary>
internal static class VersionsHelper
{
  /// <summary>Получить список типов объектов версий объекта</summary>
  /// <param name="id">Идентификатор объекта</param>
  public static List<int> GetVersionsObjectTypes(long id)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return VersionsHelper.GetVersionsObjectTypes(sessionKeeper.Session, id);
  }

  /// <summary>Получить список типов объектов версий объекта</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="id">Идентификатор объекта</param>
  public static List<int> GetVersionsObjectTypes(IUserSession session, long id)
  {
    List<int> versionsObjectTypes = new List<int>();
    foreach (DataRow row in (InternalDataCollectionBase) session.GetAllObjectVersions(id, true, false, false, "F_OBJECT_TYPE").Rows)
    {
      int int32 = Convert.ToInt32(row[0]);
      if (!versionsObjectTypes.Contains(int32))
        versionsObjectTypes.Add(int32);
    }
    return versionsObjectTypes;
  }
}
