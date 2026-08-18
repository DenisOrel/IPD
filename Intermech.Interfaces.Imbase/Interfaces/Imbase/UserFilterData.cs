// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.UserFilterData
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[Serializable]
public class UserFilterData
{
  private Dictionary<Guid, UserFilter> _userData;

  public UserFilterData()
    : this(new Dictionary<Guid, UserFilter>())
  {
  }

  public UserFilterData(Dictionary<Guid, UserFilter> userData) => this._userData = userData;

  public UserFilter GetUserFilter(Guid objGuid)
  {
    UserFilter userFilter;
    return this._userData.TryGetValue(objGuid, out userFilter) ? userFilter : new UserFilter();
  }

  public void SetUserFilter(Guid objGuid, UserFilter userFilter)
  {
    if (this._userData.ContainsKey(objGuid))
      this._userData[objGuid] = userFilter;
    else
      this._userData.Add(objGuid, userFilter);
  }

  public void RemoveUserFilter(Guid objGuid)
  {
    if (!this._userData.ContainsKey(objGuid))
      return;
    this._userData.Remove(objGuid);
  }

  public List<Guid> GetObjectGuids() => this._userData.Keys.ToList<Guid>();

  public void DeleteNonExisitingObjs(List<Guid> exisitingObjGuids)
  {
    this._userData.Keys.Except<Guid>((IEnumerable<Guid>) exisitingObjGuids).ToList<Guid>().ForEach((Action<Guid>) (x => this._userData.Remove(x)));
  }
}
