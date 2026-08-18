// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseRestrictiveCache
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server;

public class ImbaseRestrictiveCache : LongLifeObject, IImbaseRestrictiveCache
{
  private Dictionary<long, HashSet<string>> _dict;

  public ImbaseRestrictiveCache() => this._dict = new Dictionary<long, HashSet<string>>();

  public void SubcribeEvents(IEventLogHelper eventLogHelper)
  {
    eventLogHelper.AfterLogoutEvent += new LoginHandler(this.EventLogHelper_AfterLogoutEvent);
  }

  private void EventLogHelper_AfterLogoutEvent(IUserSession session)
  {
    this._dict.Remove(session.UserID);
  }

  public void Add(long userId, string imbaseInternalKey)
  {
    HashSet<string> stringSet;
    if (this._dict.TryGetValue(userId, out stringSet))
      stringSet.Add(imbaseInternalKey);
    else
      this._dict.Add(userId, new HashSet<string>()
      {
        imbaseInternalKey
      });
  }

  public HashSet<string> GetList(long userId) => this._dict[userId];

  public bool Check(long userId, string imbaseInternalKey)
  {
    HashSet<string> stringSet;
    return this._dict.TryGetValue(userId, out stringSet) && stringSet.Contains(imbaseInternalKey);
  }

  public void Remove(long userId, string imbaseInternalKey)
  {
    HashSet<string> stringSet;
    if (!this._dict.TryGetValue(userId, out stringSet) || !stringSet.Contains(imbaseInternalKey))
      return;
    stringSet.Remove(imbaseInternalKey);
  }
}
