// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.PossibleValuesCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Concurrent;
using System.Data;


namespace Intermech.Kernel;

internal class PossibleValuesCache : IPossibleValuesCache
{
  private ConcurrentDictionary<PossibleValueKey, string> pValues;

  public PossibleValuesCache(DataTable possibleValuesTable, IUserSession session)
  {
    this.ReloadCache(possibleValuesTable, session);
  }

  public void ReloadCache(DataTable possibleValuesTable, IUserSession session)
  {
    ConcurrentDictionary<PossibleValueKey, string> concurrentDictionary = new ConcurrentDictionary<PossibleValueKey, string>();
    IDBAttributeType dbAttributeType = (IDBAttributeType) null;
    int columnIndex1 = possibleValuesTable.Columns.IndexOf("F_DESCRIPTION");
    int columnIndex2 = possibleValuesTable.Columns.IndexOf("F_ATTRIBUTE_ID");
    (session as UserSession).DBCache.EnterReadLocker();
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) possibleValuesTable.Rows)
      {
        string str = row[columnIndex1].ToString();
        if (str != string.Empty)
        {
          int int32 = Convert.ToInt32(row[columnIndex2]);
          if (dbAttributeType == null || dbAttributeType.AttributeID != int32)
            dbAttributeType = session.GetAttributeType(int32, false);
          if (dbAttributeType != null)
          {
            object val = row[dbAttributeType.PossibleValueFieldName];
            if (dbAttributeType.PossibleValueFieldName == "F_DOUBLE_VALUE")
              val = (object) Convert.ToDouble(val);
            concurrentDictionary.TryAdd(new PossibleValueKey(int32, val), str);
          }
        }
      }
    }
    finally
    {
      (session as UserSession).DBCache.ExitReadLocker();
    }
    this.pValues = concurrentDictionary;
  }

  public string GetDescription(int attrID, object val)
  {
    string str;
    return val == null || !this.pValues.TryGetValue(new PossibleValueKey(attrID, val), out str) ? string.Empty : str;
  }
}
