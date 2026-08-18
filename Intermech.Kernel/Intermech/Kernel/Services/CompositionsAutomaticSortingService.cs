// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CompositionsAutomaticSortingService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace Intermech.Kernel.Services;

internal class CompositionsAutomaticSortingService : 
  LongLifeObject,
  ICompositionsAutomaticSortingService
{
  private readonly object _syncRoot = new object();
  internal readonly IDictionary<Guid, CompositionsAutomaticSortingSession> _autoSortingSessions = (IDictionary<Guid, CompositionsAutomaticSortingSession>) new ConcurrentDictionary<Guid, CompositionsAutomaticSortingSession>();
  internal static string sessionRuleKey = "CompositionsAutoSortRule";

  private IUserSession GetUserSession(object session)
  {
    switch (session)
    {
      case IUserSession userSession:
        return userSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string g:
        return UserSession.GetSessionByID(new Guid(g));
      default:
        return (IUserSession) null;
    }
  }

  private Guid GetUserSessionGuid(object session)
  {
    switch (session)
    {
      case IUserSession userSession:
        return userSession.SessionGUID;
      case Guid userSessionGuid:
        return userSessionGuid;
      case string str:
        if (GuidHelper.IsGuid(str))
          return new Guid(str);
        break;
    }
    return Guid.Empty;
  }

  public CompositionsAutosortRule GetAutosortRule(object session, bool forceReload)
  {
    IUserSession userSession = this.GetUserSession(session);
    if (userSession == null)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1010"));
    if (!(userSession is IServerSession serverSession))
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1011"));
    CompositionsAutosortRule rule = serverSession.GetSessionPluginsData((object) CompositionsAutomaticSortingService.sessionRuleKey) is CompositionsAutomaticSortingRuleHolder sessionPluginsData ? sessionPluginsData.Rule : new CompositionsAutosortRule();
    if (forceReload || sessionPluginsData?.Rule == null)
    {
      IDBObject dbObject = userSession.GetObject(new Guid("cad00693-306c-11d8-b4e9-00304f19f545"), false);
      long num = dbObject != null ? dbObject.ObjectID : -1L;
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545");
      IDBAttribute attributeById = userSession.GetObject(userSession.RoleID).GetAttributeByID(attributeTypeId);
      object objectID = attributeById != null ? attributeById.Value : (object) num;
      if (objectID != null && !objectID.Equals((object) DBNull.Value))
        rule.Load(userSession, (long) objectID, false);
      serverSession.SetSessionPluginsData((object) CompositionsAutomaticSortingService.sessionRuleKey, (object) new CompositionsAutomaticSortingRuleHolder(rule));
    }
    return rule.Clone() as CompositionsAutosortRule;
  }

  public ICompositionsAutomaticSortingSession CreateSession(object session)
  {
    Guid userSessionGuid = this.GetUserSessionGuid(session);
    if (userSessionGuid == Guid.Empty)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1010"));
    lock (this._syncRoot)
    {
      CompositionsAutomaticSortingSession session1;
      if (!this._autoSortingSessions.TryGetValue(userSessionGuid, out session1) || session1 == null)
      {
        session1 = new CompositionsAutomaticSortingSession(userSessionGuid)
        {
          RefCount = 1
        };
        this._autoSortingSessions.Add(userSessionGuid, session1);
      }
      else
        ++session1.RefCount;
      return (ICompositionsAutomaticSortingSession) session1;
    }
  }

  public int IsSessionPresent(object session)
  {
    Guid userSessionGuid = this.GetUserSessionGuid(session);
    lock (this._syncRoot)
    {
      CompositionsAutomaticSortingSession automaticSortingSession;
      return !this._autoSortingSessions.TryGetValue(userSessionGuid, out automaticSortingSession) || automaticSortingSession == null ? 0 : automaticSortingSession.RefCount;
    }
  }

  public void DisposeSession(
    ICompositionsAutomaticSortingSession sortingSession)
  {
    if (!(sortingSession is CompositionsAutomaticSortingSession automaticSortingSession))
      return;
    this.DisposeSession((object) automaticSortingSession.SessionGuid);
  }

  public void DisposeSession(object session)
  {
    Guid userSessionGuid = this.GetUserSessionGuid(session);
    if (userSessionGuid == Guid.Empty)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1010"));
    lock (this._syncRoot)
    {
      CompositionsAutomaticSortingSession automaticSortingSession;
      if (!this._autoSortingSessions.TryGetValue(userSessionGuid, out automaticSortingSession) || automaticSortingSession == null)
        return;
      --automaticSortingSession.RefCount;
      if (automaticSortingSession.RefCount != 0)
        return;
      this._autoSortingSessions.Remove(userSessionGuid);
    }
  }
}
