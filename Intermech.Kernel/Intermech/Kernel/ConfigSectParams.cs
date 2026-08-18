// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ConfigSectParams
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class ConfigSectParams
{
  private ConcurrentDictionary<string, object> _ParamsList = new ConcurrentDictionary<string, object>();

  public void AddValue(string paramName, object paramValue)
  {
    this._ParamsList[paramName] = paramValue;
  }

  public object GetValue(string paramName)
  {
    object obj;
    return this._ParamsList.TryGetValue(paramName, out obj) ? obj : (object) null;
  }

  public void DeleteValue(string paramName) => this._ParamsList.TryRemove(paramName, out object _);

  public void Clear() => this._ParamsList.Clear();

  public void FillDataTable(DataTable tbl)
  {
    foreach (KeyValuePair<string, object> keyValuePair in this._ParamsList)
    {
      DataRow row = tbl.NewRow();
      row[0] = (object) keyValuePair.Key;
      row[1] = keyValuePair.Value;
      tbl.Rows.Add(row);
    }
    tbl.AcceptChanges();
  }
}
