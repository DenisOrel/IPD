// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.OTDFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class OTDFilter
{
  private readonly ExtendedPublishOptions _options;
  private readonly IPublishRulesService _exportRulesService;
  private ISitesCacheService _sitesService;
  private ICopiesService _copiesService;
  private List<int> _documentTypes;
  private string _beSurePublishForSitesInThisPublish;
  private int _attributeSubscribersID;
  private int _objectTypeSpecificationID;
  private List<int> _objectTypeAssemblyIDs;
  private int _relationTypeDocumentation;

  public bool Enable { get; }

  public OTDFilter(IUserSession session, ExtendedPublishOptions options)
  {
    this._options = options;
    this._exportRulesService = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
    if (this._exportRulesService.OTDFiltering && !string.Concat<char>((IEnumerable<char>) this._options.EnableSites.OrderBy<char, char>((System.Func<char, char>) (x => x)).ToArray<char>()).Equals(string.Concat<long>((IEnumerable<long>) this._exportRulesService.BeSurePublishForSites.OrderBy<long, long>((System.Func<long, long>) (x => x)).ToArray<long>())))
    {
      this.Initialize(session);
      this.Enable = true;
    }
    else
      this.Enable = false;
  }

  private void Initialize(IUserSession session)
  {
    this._sitesService = session.GetCustomService(typeof (ISitesCacheService)) as ISitesCacheService;
    this._copiesService = session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
    this._documentTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    this._beSurePublishForSitesInThisPublish = string.Empty;
    if (this._exportRulesService.BeSurePublishForSites.Count > 0)
    {
      string empty = string.Empty;
      foreach (long surePublishForSite in this._exportRulesService.BeSurePublishForSites)
      {
        long siteID = surePublishForSite;
        SiteInfo siteInfo = this._sitesService.Sites.Find((Predicate<SiteInfo>) (x => x.ID.Equals(siteID)));
        empty += siteInfo.Code.ToString();
        if (this._options.EnableSites.Contains<char>(siteInfo.Code))
          this._beSurePublishForSitesInThisPublish += siteInfo.Code.ToString();
      }
    }
    this._attributeSubscribersID = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeSubscribers);
    this._objectTypeSpecificationID = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    this._objectTypeAssemblyIDs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00132-306c-11d8-b4e9-00304f19f545"));
    this._relationTypeDocumentation = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
  }

  public void SetCompositionFilter(IUserSession session, PublishCompositionObject pco)
  {
    if (!this._documentTypes.Contains(this._objectTypeSpecificationID) || !this._objectTypeAssemblyIDs.Contains(pco.ObjectType))
      return;
    DataTable dataTable = session.GetRelationCollection(this._relationTypeDocumentation).ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) this._objectTypeSpecificationID, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -3 }), pco.ObjectID);
    if (dataTable.Rows.Count <= 0)
      return;
    pco.CompositionEnableSites = this.GetEnableSitesForDocument(session, Convert.ToInt64(dataTable.Rows[0][0]));
  }

  private string GetEnableSitesForDocument(IUserSession session, long id)
  {
    long deliveryListId = this._copiesService.GetDeliveryListID(session.SessionGUID, id);
    return deliveryListId == 0L ? this._beSurePublishForSitesInThisPublish : this.GetEnableSitesForObject(session.GetObject(deliveryListId).GetAttributeByID(this._attributeSubscribersID));
  }

  public bool InFilter(IUserSession session, PublishCompositionObject pco)
  {
    if (!this._documentTypes.Contains(pco.ObjectType))
      return false;
    pco.EnableSites = pco.CompositionEnableSites = this.GetEnableSitesForDocument(session, pco.ID);
    return string.IsNullOrEmpty(pco.EnableSites);
  }

  private string GetEnableSitesForObject(IDBAttribute subscribers)
  {
    string sitesInThisPublish = this._beSurePublishForSitesInThisPublish;
    if (subscribers.ValuesCount > 0)
    {
      for (int index = 0; index < subscribers.ValuesCount; ++index)
      {
        subscribers.Index = index;
        long subscriberID = subscribers.AsInteger;
        if (subscriberID != 0L && Array.Exists<long>(this._sitesService.SitesIDs, (Predicate<long>) (x => x.Equals(subscriberID))))
        {
          SiteInfo site = this._sitesService.GetSite(subscriberID);
          if (this._options.EnableSites.Contains<char>(site.Code) && !sitesInThisPublish.Contains<char>(site.Code))
            sitesInThisPublish += site.Code.ToString();
        }
      }
    }
    return sitesInThisPublish;
  }
}
