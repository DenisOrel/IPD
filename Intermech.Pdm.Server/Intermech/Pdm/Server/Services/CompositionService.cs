// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Services.CompositionService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server.Services;

public class CompositionService : 
  LongLifeObject,
  ICompositionService,
  ICustomCompositionService,
  ISearchScheme
{
  public CompositionSelect SelectEvent;
  private const string _sectionCompositionService = "COMPOSITION_SERVICE";
  private SearchSchemesCache _schemesCache;
  private string formingTableName = LocalizationHolder.rm.GetString("Pdm.Server_24");
  private string compositionUPWord = LocalizationHolder.rm.GetString("Pdm.Server_25");
  private string compositionDownWord = LocalizationHolder.rm.GetString("Pdm.Server_26");
  private Dictionary<Guid, SelectCompositionThread> _selectThreads = new Dictionary<Guid, SelectCompositionThread>();

  public CompositionService() => this._schemesCache = new SearchSchemesCache();

  public DataTable Select(
    Guid userSessionGuid,
    long objectID,
    long schemeID,
    List<ColumnDescriptor> columns,
    Guid selectGUID,
    string filtrationOwnerID,
    HybridDictionary tags)
  {
    return this.Select(userSessionGuid, objectID, schemeID, (List<ConditionStructure>) null, columns, selectGUID, filtrationOwnerID, tags);
  }

  public DataTable Select(
    Guid userSessionGuid,
    long objectID,
    long schemeID,
    List<ConditionStructure> filterConditions,
    List<ColumnDescriptor> columns,
    Guid selectGUID,
    string filtrationOwnerID,
    HybridDictionary tags)
  {
    DataTable dataTable1 = (DataTable) null;
    IUserSession sessionById = (IUserSession) (UserSession.GetSessionByID(userSessionGuid) as UserSession);
    CompositionSelectEventArgs e = new CompositionSelectEventArgs((object) schemeID, columns);
    if (this.SelectEvent != null)
    {
      DataTable dataTable2;
      return dataTable2 = this.SelectEvent(sessionById, e);
    }
    if (!e.Handled)
    {
      SelectCompositionThread compositionThread = new SelectCompositionThread(selectGUID, userSessionGuid, objectID, (object) schemeID, filterConditions, columns, filtrationOwnerID, tags);
      compositionThread.EditingContext = CurrentEditingContextScope.TryGet() ?? CurrentEditingContext.Dummy;
      lock (this._selectThreads)
      {
        if (this._selectThreads.ContainsKey(selectGUID))
          this._selectThreads.Remove(selectGUID);
        this._selectThreads.Add(selectGUID, compositionThread);
      }
      compositionThread.Start();
    }
    return dataTable1;
  }

  public DataTable Select(
    Guid userSessionGuid,
    long objectID,
    RuntimeSearchScheme scheme,
    List<ColumnDescriptor> columns,
    Guid selectGUID,
    string filtrationOwnerID,
    HybridDictionary tags)
  {
    DataTable dataTable1 = (DataTable) null;
    IUserSession sessionById = (IUserSession) (UserSession.GetSessionByID(userSessionGuid) as UserSession);
    CompositionSelectEventArgs e = new CompositionSelectEventArgs((object) scheme, columns);
    if (this.SelectEvent != null)
    {
      DataTable dataTable2;
      return dataTable2 = this.SelectEvent(sessionById, e);
    }
    if (!e.Handled)
    {
      SelectCompositionThread compositionThread = new SelectCompositionThread(selectGUID, userSessionGuid, objectID, (object) scheme, (List<ConditionStructure>) null, columns, filtrationOwnerID, tags);
      compositionThread.EditingContext = CurrentEditingContextScope.TryGet() ?? CurrentEditingContext.Dummy;
      lock (this._selectThreads)
      {
        if (this._selectThreads.ContainsKey(selectGUID))
          this._selectThreads.Remove(selectGUID);
        this._selectThreads.Add(selectGUID, compositionThread);
      }
      compositionThread.Start();
    }
    return dataTable1;
  }

  public CompositionInfo GetInfo(Guid selectGUID)
  {
    lock (this._selectThreads)
    {
      SelectCompositionThread compositionThread;
      if (this._selectThreads.TryGetValue(selectGUID, out compositionThread))
      {
        if (compositionThread.IsError)
        {
          CompositionInfo info = new CompositionInfo(compositionThread.ErrorException);
          this._selectThreads.Remove(selectGUID);
          return info;
        }
        if (!compositionThread.IsCompleted)
          return new CompositionInfo(compositionThread.Percent);
        CompositionInfo info1 = new CompositionInfo((object) compositionThread.Result);
        this._selectThreads.Remove(selectGUID);
        return info1;
      }
    }
    return (CompositionInfo) null;
  }

  public void CancelSelect(Guid selectGUID)
  {
    lock (this._selectThreads)
    {
      SelectCompositionThread compositionThread;
      if (!this._selectThreads.TryGetValue(selectGUID, out compositionThread))
        return;
      compositionThread.Stop();
      this._selectThreads.Remove(selectGUID);
    }
  }

  public List<long> GetSchemesForRelationTypes(Guid userSessionGuid, List<Guid> relationTypes)
  {
    return this._schemesCache.GetSchemesForRelationTypes((IUserSession) (UserSession.GetSessionByID(userSessionGuid) as UserSession), relationTypes);
  }

  public List<long> GetSchemesForRelationTypes(
    Guid userSessionGuid,
    List<Guid> relationTypes,
    ContainsMode mode)
  {
    return this._schemesCache.GetSchemesForRelationTypes((IUserSession) (UserSession.GetSessionByID(userSessionGuid) as UserSession), relationTypes, mode);
  }

  public void AddScheme(IUserSession session, long schemeID)
  {
    this._schemesCache.AddScheme(session, schemeID);
  }

  public void ChangeScheme(IUserSession session, long schemeID)
  {
    this._schemesCache.DeleteScheme(session, schemeID);
    this._schemesCache.AddScheme(session, schemeID);
  }

  public void DeleteScheme(IUserSession session, long schemeID)
  {
    this._schemesCache.DeleteScheme(session, schemeID);
  }

  public List<SearchSchemaInfo> GetSchemesForRelationTypesEx(
    Guid userSessionGuid,
    List<Guid> relationTypes,
    ContainsMode mode,
    bool roleFiltration)
  {
    return this._schemesCache.GetSchemesForRelationTypesEx((IUserSession) (UserSession.GetSessionByID(userSessionGuid) as UserSession), relationTypes, mode, roleFiltration);
  }
}
