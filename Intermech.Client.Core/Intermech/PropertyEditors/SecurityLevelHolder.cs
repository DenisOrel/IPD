
// Type: Intermech.PropertyEditors.SecurityLevelHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Data;


namespace Intermech.PropertyEditors;

public class SecurityLevelHolder
{
  private static DataTable securityLevelDataTable;

  /// <summary>
  /// Список уровней доступа в системе в виде DataTable: Consts.F_INLIST_ID, Consts.F_INTEGER_VALUE, Consts.F_DESCRIPTION
  /// </summary>
  public static DataTable SecurityLevelDataTable
  {
    get
    {
      if (SecurityLevelHolder.securityLevelDataTable == null)
        SecurityLevelHolder.securityLevelDataTable = SecurityLevelHolder.GetSecurityLevelList();
      return SecurityLevelHolder.securityLevelDataTable;
    }
  }

  /// <summary>
  /// Максимально допустимый уровень безопасности текущего пользователя
  /// </summary>
  public static int MaxSecurityLevel
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.SecurityLevel;
    }
  }

  /// <summary>
  /// Вернуть уровни доступа в системе в виде DataTable: Consts.F_INLIST_ID, Consts.F_INTEGER_VALUE, Consts.F_DESCRIPTION
  /// </summary>
  /// <returns></returns>
  private static DataTable GetSecurityLevelList()
  {
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(new Guid("cad00816-306c-11d8-b4e9-00304f19f545"), true);
    if (attributeType != null)
    {
      DataTable possibleValues = attributeType.GetPossibleValues();
      if (possibleValues != null)
        return possibleValues;
    }
    return new DataTable();
  }

  public static string GetDescriptionBySecurityLevel(int secLevel)
  {
    foreach (DataRow dataRow in SecurityLevelHolder.SecurityLevelDataTable.Select())
    {
      if (Convert.ToInt32(dataRow["F_INTEGER_VALUE"]) == secLevel)
        return Convert.ToString(dataRow["F_DESCRIPTION"]);
    }
    return string.Empty;
  }

  public static int GetSecurityLevelByDescription(string secLevelDescription)
  {
    foreach (DataRow dataRow in SecurityLevelHolder.SecurityLevelDataTable.Select())
    {
      if (Convert.ToString(dataRow["F_DESCRIPTION"]) == secLevelDescription)
        return Convert.ToInt32(dataRow["F_INTEGER_VALUE"]);
    }
    return 0;
  }
}
