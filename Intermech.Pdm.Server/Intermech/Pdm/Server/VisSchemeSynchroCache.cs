// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.VisSchemeSynchroCache
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Services;
using System;
using System.Collections.Concurrent;

#nullable disable
namespace Intermech.Pdm.Server;

public class VisSchemeSynchroCache : CustomServerSynchronizer
{
  private ConcurrentDictionary<long, VisSchemeParms> _cache;

  public VisSchemeSynchroCache()
    : base(new Guid("EA0D6355-39F8-414C-9B14-366DE4EB3922"), "Кэш схем данных визуализатора")
  {
    this._cache = new ConcurrentDictionary<long, VisSchemeParms>();
    this._cache.GetOrAdd(0L, new VisSchemeParms());
  }

  public VisSchemeParms this[long objId]
  {
    get => this._cache.ContainsKey(objId) ? this._cache[objId] : (VisSchemeParms) null;
  }

  public bool TryGetValue(long key, out VisSchemeParms scheme)
  {
    return this._cache.TryGetValue(key, out scheme);
  }

  public void SaveValue(long key, VisSchemeParms scheme)
  {
    if (this._cache.ContainsKey(key))
      this._cache[key] = scheme;
    else
      this._cache.GetOrAdd(key, scheme);
  }

  public void DeleteValue(long key)
  {
    if (!this._cache.ContainsKey(key))
      return;
    this._cache.TryRemove(key, out VisSchemeParms _);
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    string[] strArray = eventProps.StringInfo.Split('!');
    if (strArray.Length < 2)
      return;
    if (strArray[0] == "UPDATE")
    {
      long int64 = Convert.ToInt64(strArray[1]);
      VisSchemeParms visSchemeParms = new VisSchemeParms(int64, session);
      if (this._cache.ContainsKey(int64))
        this._cache[int64] = visSchemeParms;
      else
        this._cache.GetOrAdd(int64, visSchemeParms);
    }
    else
    {
      if (!(strArray[0] == "DELETE"))
        return;
      long int64 = Convert.ToInt64(strArray[1]);
      if (!this._cache.ContainsKey(int64))
        return;
      this._cache.TryRemove(int64, out VisSchemeParms _);
    }
  }

  public void AddUpdateEvent(long objId, IUserSession ius)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps("UPDATE!" + Convert.ToString(objId)), ((UserSession) ius).DataManager);
  }

  public void AddDeleteEvent(long objId, IUserSession ius)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps("DELETE!" + Convert.ToString(objId)), ((UserSession) ius).DataManager);
  }
}
