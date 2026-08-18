// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.ValueInfo
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class ValueInfo
{
  private Guid _sessionGuid = Guid.Empty;
  private Dictionary<string, ValueInfo.ValueState> _changedValuesState = new Dictionary<string, ValueInfo.ValueState>(0);

  internal long FormID { get; private set; }

  internal ValueInfo(Guid sessionGuid, long formID)
  {
    this._sessionGuid = sessionGuid;
    this.FormID = formID;
  }

  internal void AddValueInfo(string oldValue, string newValue)
  {
    if (!(oldValue != newValue))
      return;
    if (!this._changedValuesState.ContainsKey(oldValue))
    {
      if (!string.IsNullOrEmpty(oldValue))
        this._changedValuesState.Add(oldValue, ValueInfo.ValueState.Deleted);
    }
    else if (this._changedValuesState[oldValue] == ValueInfo.ValueState.Added)
      this._changedValuesState.Remove(oldValue);
    if (!this._changedValuesState.ContainsKey(newValue))
    {
      if (string.IsNullOrEmpty(newValue))
        return;
      this._changedValuesState.Add(newValue, ValueInfo.ValueState.Added);
    }
    else
    {
      if (this._changedValuesState[newValue] != ValueInfo.ValueState.Deleted)
        return;
      this._changedValuesState.Remove(newValue);
    }
  }

  internal void GetChangedValues(out List<string> addedValues, out List<string> deletedValues)
  {
    addedValues = new List<string>();
    deletedValues = new List<string>();
    foreach (KeyValuePair<string, ValueInfo.ValueState> keyValuePair in this._changedValuesState)
    {
      if (keyValuePair.Value == ValueInfo.ValueState.Added)
        addedValues.Add(keyValuePair.Key);
      else
        deletedValues.Add(keyValuePair.Key);
    }
  }

  private enum ValueState
  {
    Added,
    Deleted,
  }
}
