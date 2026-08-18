// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.RemotingInfoService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Services;
using System.Security.Permissions;


namespace Intermech.Kernel.Services;

public sealed class RemotingInfoService : LongLifeObject, ITrackingHandler, IRemotingInfoService
{
  private ConcurrentDictionary<string, bool> _uriTable;
  private Dictionary<string, int> _objectCountTable;
  private volatile bool _verbose;
  private List<Type> _ignoredTypes;
  private ConcurrentDictionary<Type, bool> _ignoredTypesCache;

  public RemotingInfoService()
  {
    this._uriTable = new ConcurrentDictionary<string, bool>(Environment.ProcessorCount, 128 /*0x80*/);
    this._objectCountTable = new Dictionary<string, int>(128 /*0x80*/);
    this._ignoredTypes = new List<Type>();
    this._ignoredTypes.Add(typeof (AppDomain));
    this._ignoredTypes.Add(typeof (ILease));
    this._ignoredTypes.Add(typeof (LongLifeObject));
    this._ignoredTypes.Add(typeof (IntermechServerService));
    this._ignoredTypesCache = new ConcurrentDictionary<Type, bool>();
  }

  public bool Verbose
  {
    [DebuggerStepThrough] get => this._verbose;
    [DebuggerStepThrough] set => this._verbose = value;
  }

  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public void MarshaledObject(object mbrObject, ObjRef mbrObjectRef)
  {
    Type type = mbrObject.GetType();
    if (this.Verbose)
      Console.WriteLine("+Marshalled : {0}", (object) type.FullName);
    if (this.IsObjectIgnored(mbrObject, type))
      return;
    string uri = mbrObjectRef.URI;
    if (string.IsNullOrEmpty(uri) || !this._uriTable.TryAdd(uri, true))
      return;
    this.IncreaseObjectCount(type.FullName);
  }

  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public void DisconnectedObject(object mbrObject)
  {
    Type type = mbrObject.GetType();
    if (this.Verbose)
      Console.WriteLine("-Disconnected : {0}", (object) type.FullName);
    if (this.IsObjectIgnored(mbrObject, type))
      return;
    string objectUri = RemotingServices.GetObjectUri((MarshalByRefObject) mbrObject);
    if (objectUri == null || !this._uriTable.TryRemove(objectUri, out bool _))
      return;
    this.DecreaseObjectCount(type.FullName);
  }

  [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public void UnmarshaledObject(object externalObject, ObjRef externalObjectRef)
  {
    if (!this.Verbose)
      return;
    Console.WriteLine("*Unmarshalled : {0}", (object) externalObjectRef.TypeInfo.TypeName);
  }

  private void IncreaseObjectCount(string objTypeName)
  {
    lock (this._objectCountTable)
    {
      int num;
      if (this._objectCountTable.TryGetValue(objTypeName, out num))
        this._objectCountTable[objTypeName] = num + 1;
      else
        this._objectCountTable.Add(objTypeName, 1);
    }
  }

  private void DecreaseObjectCount(string objTypeName)
  {
    lock (this._objectCountTable)
    {
      int num;
      if (!this._objectCountTable.TryGetValue(objTypeName, out num) || num <= 0)
        return;
      this._objectCountTable[objTypeName] = num - 1;
    }
  }

  private bool IsObjectIgnored(object objectInstance, Type objectType)
  {
    if (this._ignoredTypesCache.GetOrAdd(objectType, new Func<Type, bool>(this.IsObjectTypeIgnoredSlow)))
      return true;
    if (!(objectInstance is MarshalByRefObject))
      return false;
    bool flag = ((MarshalByRefObject) objectInstance).GetLifetimeService() == null;
    this._ignoredTypesCache.TryAdd(objectType, flag);
    return flag;
  }

  private bool IsObjectTypeIgnoredSlow(Type objectType)
  {
    for (int index = 0; index < this._ignoredTypes.Count; ++index)
    {
      if (this._ignoredTypes[index].IsAssignableFrom(objectType))
        return true;
    }
    return false;
  }

  public List<Tuple<string, int>> GetMarshalledObjectsStatistics()
  {
    lock (this._objectCountTable)
    {
      List<Tuple<string, int>> objectsStatistics = new List<Tuple<string, int>>(this._objectCountTable.Count);
      foreach (KeyValuePair<string, int> keyValuePair in this._objectCountTable)
      {
        if (keyValuePair.Value > 0)
          objectsStatistics.Add(Tuple.Create<string, int>(keyValuePair.Key, keyValuePair.Value));
      }
      return objectsStatistics;
    }
  }
}
