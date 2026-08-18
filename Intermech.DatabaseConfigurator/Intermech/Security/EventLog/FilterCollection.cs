// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterCollection
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using ImSSP;
using Intermech.Localization;
using System;
using System.Collections;

#nullable disable
namespace Intermech.Security.EventLog;

internal class FilterCollection
{
  private ArrayList _filters;
  private Hashtable _guidHash;
  private Hashtable _nameHash;
  private bool _modified;

  public FilterCollection()
  {
    this._filters = new ArrayList();
    this._guidHash = new Hashtable();
    this._nameHash = new Hashtable();
    this._modified = false;
  }

  public int Count => this._filters.Count;

  public bool ContainsFilter(Guid guid) => this._guidHash.ContainsKey((object) guid);

  public bool ContainsFilter(string name) => this._nameHash.ContainsKey((object) name);

  public Filter this[int index] => (Filter) this._filters[index];

  public Filter FindFilter(Guid guid)
  {
    return this._guidHash.ContainsKey((object) guid) ? (Filter) this._guidHash[(object) guid] : (Filter) null;
  }

  public Filter FindFilter(string name)
  {
    return this._nameHash.ContainsKey((object) name) ? (Filter) this._nameHash[(object) name] : (Filter) null;
  }

  public int Add(Filter filter)
  {
    if (filter.Collection != null && filter.Collection != this)
      filter.Collection.Remove(filter);
    if (this._guidHash.ContainsKey((object) filter.Guid))
      return this._filters.IndexOf((object) (Filter) this._guidHash[(object) filter.Guid]);
    if (filter.Name.Length == 0)
    {
      string format = LocalizationHolder.rm.GetString("DatabaseConfigurator_98");
      int num = 1;
      while (this._nameHash.ContainsKey((object) string.Format(format, (object) num)))
        ++num;
      string str = string.Format(format, (object) num);
      filter.Name = str;
    }
    int num1 = this._filters.Add((object) filter);
    filter.Collection = this;
    this._guidHash.Add((object) filter.Guid, (object) filter);
    this._nameHash.Add((object) filter.Name, (object) filter);
    this._modified = true;
    return num1;
  }

  public void Remove(Filter filter)
  {
    if (filter.Collection != this)
      return;
    this._guidHash.Remove((object) filter.Guid);
    this._nameHash.Remove((object) filter.Name);
    this._filters.Remove((object) filter);
    filter.Collection = (FilterCollection) null;
    this._modified = true;
  }

  public void RemoveAt(int index) => this.Remove((Filter) this._filters[index]);

  public void Clear()
  {
    this._filters.Clear();
    this._guidHash.Clear();
    this._nameHash.Clear();
    this._modified = true;
  }

  internal void ChangeName(string oldName, string newName)
  {
    Filter filter = !this._nameHash.ContainsKey((object) newName) ? (Filter) this._nameHash[(object) oldName] : throw new ApplicationException(LocalizationHolder.rm.GetString(sc_5854.ssp_imclient_5855()));
    this._nameHash.Remove((object) oldName);
    this._nameHash.Add((object) newName, (object) filter);
    this._modified = true;
  }

  internal void ChangeGuid(Guid oldGuid, Guid newGuid)
  {
    Filter filter = !this._guidHash.ContainsKey((object) newGuid) ? (Filter) this._guidHash[(object) oldGuid] : throw new ApplicationException(LocalizationHolder.rm.GetString(sc_5854.ssp_imclient_5856()));
    this._guidHash.Remove((object) oldGuid);
    this._guidHash.Add((object) newGuid, (object) filter);
    this._modified = true;
  }

  internal bool Modified
  {
    get => this._modified;
    set => this._modified = value;
  }
}
