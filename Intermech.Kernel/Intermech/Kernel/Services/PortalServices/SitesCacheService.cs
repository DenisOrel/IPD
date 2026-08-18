// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.SitesCacheService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class SitesCacheService : LongLifeObject, ISitesCacheService
{
  private List<SiteInfo> _sitesCache;
  private SiteInfo _info;
  private IEventLogHelper _eventLog;

  public SitesCacheService(IEventLogHelper eventLog)
  {
    this._sitesCache = new List<SiteInfo>();
    this._eventLog = eventLog;
  }

  public void Reload(object session)
  {
    try
    {
      SiteInfo[] sitesFromDb = SiteInfoHelper.GetSitesFromDB(session is Guid sessionGUID ? UserSession.GetSessionByID(sessionGUID) : (IUserSession) session);
      if (this._sitesCache != null)
        this._sitesCache.Clear();
      else
        this._sitesCache = new List<SiteInfo>(sitesFromDb.Length);
      if (sitesFromDb.Length == 0)
        return;
      this._sitesCache.AddRange((IEnumerable<SiteInfo>) sitesFromDb);
    }
    catch (Exception ex)
    {
      this._eventLog.AddToTrace(ex.Message, Consts.traceAlways, string.Empty);
    }
  }

  public SiteInfo GetSite(char code)
  {
    if (this._sitesCache == null || this._sitesCache.Count == 0)
      return (SiteInfo) null;
    for (int index = 0; index < this._sitesCache.Count; ++index)
    {
      if ((int) this._sitesCache[index].Code == (int) code)
        return this._sitesCache[index];
    }
    return (SiteInfo) null;
  }

  public SiteInfo GetSite(long id)
  {
    if (this._sitesCache == null || this._sitesCache.Count == 0)
      return (SiteInfo) null;
    for (int index = 0; index < this._sitesCache.Count; ++index)
    {
      if (this._sitesCache[index].ID == id)
        return this._sitesCache[index];
    }
    return (SiteInfo) null;
  }

  public SiteInfo GetSite(Guid guid) => this.GetSite(guid, false);

  public SiteInfo GetSite(Guid guid, bool throwException)
  {
    if (this._sitesCache == null || this._sitesCache.Count == 0)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_1098"));
      return (SiteInfo) null;
    }
    for (int index = 0; index < this._sitesCache.Count; ++index)
    {
      if (this._sitesCache[index].GUID == guid)
        return this._sitesCache[index];
    }
    if (throwException)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1099"), (object) guid));
    return (SiteInfo) null;
  }

  public long[] SitesIDs
  {
    get
    {
      if (this._sitesCache == null || this._sitesCache.Count == 0)
        return (long[]) null;
      List<long> longList = new List<long>(this._sitesCache.Count);
      for (int index = 0; index < this._sitesCache.Count; ++index)
        longList.Add(this._sitesCache[index].ID);
      return longList.ToArray();
    }
  }

  public SiteInfo Info
  {
    get => this._info;
    internal set => this._info = value;
  }

  public List<SiteInfo> Sites => this._sitesCache;

  public bool IsPortal { get; set; }

  public char NextCode()
  {
    DataTable dataTable = new DataTable();
    dataTable.Columns.Add(new DataColumn("F_SITE_ID", typeof (string)));
    foreach (SiteInfo siteInfo in this._sitesCache)
    {
      DataRow row = dataTable.NewRow();
      row[0] = (object) siteInfo.Code;
      dataTable.Rows.Add(row);
    }
    dataTable.AcceptChanges();
    return SqlHelper.NextLetter(dataTable.Rows);
  }

  public string GetSiteDescription(string siteID)
  {
    return siteID == null || siteID == string.Empty ? string.Empty : SiteIDHelper.GetCaption((ISitesCacheService) this, siteID);
  }

  public SiteInfo GetSite(string name)
  {
    if (this._sitesCache == null || this._sitesCache.Count == 0)
      return (SiteInfo) null;
    for (int index = 0; index < this._sitesCache.Count; ++index)
    {
      if (this._sitesCache[index].Caption == name)
        return this._sitesCache[index];
    }
    return (SiteInfo) null;
  }
}
