// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.TypeInfoHelper
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class TypeInfoHelper
{
  private Guid _guid = Guid.Empty;

  internal IDictionary<Guid, int> FormsDisplayOrder { get; private set; }

  internal bool IsEmpty => this.FormsDisplayOrder.Count == 0;

  internal TypeInfoHelper(Guid g)
  {
    this._guid = g;
    this.FormsDisplayOrder = (IDictionary<Guid, int>) new ConcurrentDictionary<Guid, int>();
  }

  internal TypeInfoHelper(Guid g, IDictionary<Guid, int> dict)
  {
    this._guid = g;
    this.FormsDisplayOrder = (IDictionary<Guid, int>) (dict as ConcurrentDictionary<Guid, int>);
    if (this.FormsDisplayOrder != null || dict == null)
      return;
    this.FormsDisplayOrder = (IDictionary<Guid, int>) new ConcurrentDictionary<Guid, int>((IEnumerable<KeyValuePair<Guid, int>>) dict);
  }

  internal void AddFormsDisplayOrder(Dictionary<Guid, int> dict)
  {
    if (dict == null)
      return;
    foreach (KeyValuePair<Guid, int> keyValuePair in dict)
    {
      if (!(keyValuePair.Key == Guid.Empty))
      {
        if (this.FormsDisplayOrder.ContainsKey(keyValuePair.Key))
          this.FormsDisplayOrder[keyValuePair.Key] = keyValuePair.Value;
        else
          this.FormsDisplayOrder.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
  }

  internal void ClearFormsDisplayOrder() => this.FormsDisplayOrder.Clear();

  internal void RemoveFormsDisplayOrder(List<Guid> guids)
  {
    guids?.ForEach((Action<Guid>) (x => this.FormsDisplayOrder.Remove(x)));
  }

  internal void SetFormDisplayIndexes(Dictionary<Guid, int> dict)
  {
    if (dict == null || dict.Count <= 0)
      return;
    this.FormsDisplayOrder = (IDictionary<Guid, int>) dict;
  }
}
