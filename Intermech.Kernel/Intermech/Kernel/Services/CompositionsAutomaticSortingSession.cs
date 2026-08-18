// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CompositionsAutomaticSortingSession
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services.Compositions.Sorting;
using Intermech.Kernel.Services.Compositions.Sorting.Method;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services;

internal class CompositionsAutomaticSortingSession : 
  MarshalByRefObject,
  ICompositionsAutomaticSortingSession
{
  private int _refCount = 1;
  private readonly object _refLocker = new object();
  private CompositionObjectInfoCache _objectCompositionCache;
  private CompositionsAutosortRule _autoSortRule;
  private bool _autoSortRuleLoaded;

  private CompositionsAutosortRule GetAutoSortRule(IUserSession session, bool forceLoad)
  {
    if (forceLoad)
      this._autoSortRuleLoaded = false;
    if (this._autoSortRuleLoaded)
      return this._autoSortRule;
    this._autoSortRule = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) ServerServices.ServiceContainer, true).GetAutosortRule((object) session, forceLoad);
    this._autoSortRuleLoaded = true;
    return this._autoSortRule;
  }

  private CompositionObjectInfoCache GetCompositionObjectCache(IUserSession session)
  {
    if (this._objectCompositionCache != null)
      return this._objectCompositionCache;
    CompositionsAutosortRule autoSortRule = this.GetAutoSortRule(session, false);
    return autoSortRule == null ? (CompositionObjectInfoCache) null : (this._objectCompositionCache = new CompositionObjectInfoCache((ICompositionSortingComparer<CompositionSortingInfoItem>) new CompositionSortingInfoItemComparer<CompositionSortingInfoItem>(autoSortRule, CompositionSortingDirectionMode.Desc)));
  }

  public CompositionsAutomaticSortingSession(Guid session)
  {
    this.SessionGuid = session;
    this._objectCompositionCache = (CompositionObjectInfoCache) null;
  }

  public void PrefetchObjectComposition(IEnumerable<long> objectIDs, object session)
  {
    if (!(objectIDs is long[] numArray))
      numArray = objectIDs.ToArray<long>();
    long[] objectIDs1 = numArray;
    if (objectIDs1.Length == 0)
      return;
    this.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) objectIDs1), session);
  }

  public void PrefetchObjectComposition(IEnumerable<ObjInfoItem> objectIDs, object session)
  {
    if (!(objectIDs is ObjInfoItem[] objInfoItemArray))
      objInfoItemArray = objectIDs.ToArray<ObjInfoItem>();
    ObjInfoItem[] objectItems = objInfoItemArray;
    if (objectItems.Length == 0)
      return;
    IUserSession userSession = CompositionsAutomaticSortingSession.GetUserSession(session);
    if (userSession == null)
      return;
    this.GetCompositionObjectCache(userSession)?.LoadData(userSession, (IEnumerable<ObjInfoItem>) objectItems);
  }

  public void ProceedRelation(long relationId, object session)
  {
    this.ProceedRelation((IEnumerable<long>) new long[1]
    {
      relationId
    }, session);
  }

  public void ProceedRelation(IEnumerable<long> relationIDs, object session)
  {
    this.ProceedRelation(relationIDs, CompositionTargetMode.Add, 0L, session);
  }

  public void ProceedRelation(
    IEnumerable<long> relationIDs,
    CompositionTargetMode targetMode,
    long targetRelationId,
    object session)
  {
    if (!(relationIDs is long[] numArray))
      numArray = relationIDs.ToArray<long>();
    long[] source = numArray;
    if (source.Length == 0)
      return;
    CompositionSortingParams sortingParams = new CompositionSortingParams(((IEnumerable<long>) source).Select<long, CompositionSortingProjInfo>((System.Func<long, CompositionSortingProjInfo>) (relationId => new CompositionSortingProjInfo(relationId))), targetRelationId);
    this.ProceedRelation(session, targetMode, sortingParams);
  }

  public void ProceedRelation(CompositionSortingProjInfo relationInfo, object session)
  {
    this.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) new CompositionSortingProjInfo[1]
    {
      relationInfo
    }, session);
  }

  public void ProceedRelation(
    IEnumerable<CompositionSortingProjInfo> relationInfo,
    object session)
  {
    this.ProceedRelation(relationInfo, CompositionTargetMode.Add, 0L, session);
  }

  public void ProceedRelation(
    IEnumerable<CompositionSortingProjInfo> relationInfo,
    CompositionTargetMode targetMode,
    long targetRelationId,
    object session)
  {
    if (relationInfo == null)
      return;
    CompositionSortingParams sortingParams = new CompositionSortingParams(relationInfo, targetRelationId);
    this.ProceedRelation(session, targetMode, sortingParams);
  }

  public void ProceedRelation(
    object session,
    CompositionTargetMode targetMode,
    CompositionSortingParams sortingParams)
  {
    IUserSession userSession = CompositionsAutomaticSortingSession.GetUserSession(session);
    if (session == null)
      return;
    CompositionObjectInfoCache compositionObjectCache = this.GetCompositionObjectCache(userSession);
    if (compositionObjectCache == null)
      return;
    CompositionAutomaticSortingMethod automaticSortingMethod = (CompositionAutomaticSortingMethod) null;
    switch (targetMode)
    {
      case CompositionTargetMode.Add:
        automaticSortingMethod = (CompositionAutomaticSortingMethod) new CompositionAutomaticSortingInsertLastMethod(compositionObjectCache);
        break;
      case CompositionTargetMode.InsertBefore:
        automaticSortingMethod = (CompositionAutomaticSortingMethod) new CompositionAutomaticSortingInsertBeforeMethod(compositionObjectCache);
        break;
      case CompositionTargetMode.InsertAfter:
        automaticSortingMethod = (CompositionAutomaticSortingMethod) new CompositionAutomaticSortingInsertAfterMethod(compositionObjectCache);
        break;
      case CompositionTargetMode.InsertFirst:
        automaticSortingMethod = (CompositionAutomaticSortingMethod) new CompositionAutomaticSortingInsertFirstMethod(compositionObjectCache);
        break;
    }
    automaticSortingMethod?.Execute(userSession, sortingParams);
  }

  public static IUserSession GetUserSession(object session)
  {
    switch (session)
    {
      case IUserSession _:
        return session as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) session));
      default:
        return (IUserSession) null;
    }
  }

  [Obsolete("Use CompositionObjectInfoCache.SortTableByRule instead", true)]
  public static DataTable SortTableByRule(DataTable dataTable, CompositionsAutosortRule sortRule)
  {
    return CompositionObjectInfoCache.SortTableByRule(dataTable, sortRule);
  }

  public Guid SessionGuid { get; }

  public int RefCount
  {
    get
    {
      lock (this._refLocker)
        return this._refCount;
    }
    set
    {
      lock (this._refLocker)
        this._refCount = value;
    }
  }
}
