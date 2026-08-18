
// Type: Intermech.Security.RightConditionList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Security;

internal class RightConditionList : List<RightConditionClass>
{
  /// <summary>атрибут Условие проверки прав доступа</summary>
  private static Guid RightConditionAttrGuid = new Guid("cadd9a26-306c-11d8-b4e9-00304f19f545");
  public bool Initialized;

  /// <summary>Существует ли условие?</summary>
  /// <param name="val"></param>
  /// <returns></returns>
  public bool ConditionExists(object val)
  {
    bool flag = false;
    if (val == null || val == DBNull.Value)
      return flag;
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].Value.Equals(val))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  /// <summary>Условие строку по коду условия</summary>
  /// <param name="val"></param>
  /// <returns></returns>
  public string ValueToString(object val)
  {
    if (val == null || val == DBNull.Value)
      return string.Empty;
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].Value.Equals(val))
        return this[index].Text;
    }
    return $"<{val}>";
  }

  public void Initialize()
  {
    this.Clear();
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(RightConditionList.RightConditionAttrGuid, true);
    if (attributeType == null)
      return;
    DataTable possibleValues = attributeType.GetPossibleValues();
    if (possibleValues == null)
      return;
    string valueFieldName = ClientCommons.ExtractValueFieldName(possibleValues);
    foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
      this.Add(new RightConditionClass(Convert.ToInt64(row[valueFieldName]), Convert.ToString(row["F_DESCRIPTION"])));
    this.Initialized = true;
  }
}
