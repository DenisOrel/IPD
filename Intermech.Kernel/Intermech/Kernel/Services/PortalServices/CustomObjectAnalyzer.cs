// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.CustomObjectAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class CustomObjectAnalyzer : ICustomObjectAnalyzer
{
  private readonly Dictionary<int, List<int>> _freeAttributesCache;
  private readonly ExtendedPublishOptions _options;
  private readonly OTDFilter _otdFilter;
  private readonly PublishType _publishType;

  public CustomObjectAnalyzer(
    IUserSession session,
    PublishType publishType,
    ExtendedPublishOptions options,
    Dictionary<int, List<int>> freeAttributesCache)
  {
    this._options = options;
    this._freeAttributesCache = freeAttributesCache;
    this._otdFilter = new OTDFilter(session, options);
    this._publishType = publishType;
  }

  public PublishCompositionObject GetObjectInfo(IUserSession session, IDBObject obj)
  {
    return this.GetObjectInfo(session, obj, false);
  }

  private PublishCompositionObject GetObjectInfo(IUserSession session, IDBObject obj, bool isRoot)
  {
    PublishCompositionObject objectInfo = new PublishCompositionObject()
    {
      ObjectID = obj.ObjectID,
      ID = obj.ID,
      ObjectType = obj.ObjectType,
      Published = obj.SiteID != null && obj.SiteID != string.Empty,
      OwnerID = obj.OwnerID,
      CheckOutBy = obj.CheckoutBy,
      ObjectGuid = obj.ObjectGUID,
      SiteID = obj.SiteID,
      EnableSites = this._options.EnableSites,
      CompositionEnableSites = this._options.EnableSites
    };
    this.SetCaption(obj.Caption, objectInfo);
    objectInfo.Root = isRoot;
    this.CheckDBObjectInclude(session, objectInfo, obj);
    return objectInfo;
  }

  public PublishCompositionObject GetObjectInfo(IUserSession session, long objectID, bool isRoot)
  {
    return this.GetObjectInfo(session, session.GetObject(objectID), isRoot);
  }

  public void GetRecordInfo(
    IUserSession session,
    List<PublishCompositionObject> objects,
    DataRow row,
    FieldsMapper fieldsMapper,
    List<int> enabledRelationTypes,
    out PublishCompositionObject pco,
    out PublishCompositionRelation pcr)
  {
    pco = new PublishCompositionObject()
    {
      ObjectID = Convert.ToInt64(row[fieldsMapper.idxObjectID]),
      ID = Convert.ToInt64(row[fieldsMapper.idxID]),
      ObjectType = Convert.ToInt32(row[fieldsMapper.idxObjectType]),
      Published = Convert.ToString(row[fieldsMapper.idxSiteID]) != string.Empty,
      ProjID = Convert.ToInt64(row[fieldsMapper.idxProjID]),
      OwnerID = Convert.ToInt64(row[fieldsMapper.idxOwnerID]),
      CheckOutBy = Convert.ToInt64(row[fieldsMapper.idxCheckOutBy]),
      ObjectGuid = new Guid(Convert.ToString(row[fieldsMapper.idxGuid])),
      SiteID = Convert.ToString(row[fieldsMapper.idxSiteID]),
      EnableSites = this._options.EnableSites,
      CompositionEnableSites = this._options.EnableSites
    };
    this.SetCaption(Convert.ToString(row[fieldsMapper.idxCaption]), pco);
    pcr = new PublishCompositionRelation(Convert.ToInt64(row[fieldsMapper.idxPrjlinkID]), new Guid(Convert.ToString(row[fieldsMapper.idxGuid])), Convert.ToInt32(row[fieldsMapper.idxRelationType]));
    this.CheckRecordInclude(session, objects, enabledRelationTypes, pco, pcr, fieldsMapper, row);
  }

  private void CheckDBObjectInclude(
    IUserSession session,
    PublishCompositionObject pco,
    IDBObject obj)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(PortalConsts.attributePublicationNecessary, false);
    pco.Include = this.CheckInclude(session, (List<int>) null, pco, (PublishCompositionRelation) null, attributeByGuid != null ? (PublicationNecessary) attributeByGuid.AsInteger : this.GetDefaultPublicationNecessary((List<PublishCompositionObject>) null, pco, (PublishCompositionRelation) null), obj.AccessLevel, (PublishCompositionObject) null);
  }

  private void CheckRecordInclude(
    IUserSession session,
    List<PublishCompositionObject> objects,
    List<int> enabledRelationTypes,
    PublishCompositionObject pco,
    PublishCompositionRelation pcr,
    FieldsMapper fieldsMapper,
    DataRow row)
  {
    PublishCompositionObject compositionObject = pco;
    PublishCompositionRelation compositionRelation = pcr;
    IUserSession session1 = session;
    List<int> enabledRelationTypes1 = enabledRelationTypes;
    PublishCompositionObject pco1 = pco;
    PublishCompositionRelation pcr1 = pcr;
    int publicationNeccesary = row[fieldsMapper.idxPublicationNeccesary] != DBNull.Value ? Convert.ToInt32(row[fieldsMapper.idxPublicationNeccesary]) : (int) this.GetDefaultPublicationNecessary(objects, pco, pcr);
    int int32 = Convert.ToInt32(row[fieldsMapper.idxAccessLevel]);
    PublishCompositionObject proj = objects.Find((Predicate<PublishCompositionObject>) (_ => _.ObjectID.Equals(Convert.ToInt64(row[fieldsMapper.idxProjID]))));
    int num1;
    IncludeTypes includeTypes = (IncludeTypes) (num1 = (int) this.CheckInclude(session1, enabledRelationTypes1, pco1, pcr1, (PublicationNecessary) publicationNeccesary, int32, proj));
    compositionRelation.Include = (IncludeTypes) num1;
    int num2 = (int) includeTypes;
    compositionObject.Include = (IncludeTypes) num2;
  }

  private PublicationNecessary GetDefaultPublicationNecessary(
    List<PublishCompositionObject> objects,
    PublishCompositionObject pco,
    PublishCompositionRelation pcr)
  {
    return this._publishType != PublishType.Autoreplication || pcr != null && objects.Exists((Predicate<PublishCompositionObject>) (x =>
    {
      if (!x.ObjectID.Equals(pco.ProjID))
        return false;
      return x.Include == IncludeTypes.Include || x.Include == IncludeTypes.FCAttributesOnly || x.Include == IncludeTypes.FCFileAttributesOnly;
    })) ? PublicationNecessary.Object : PublicationNecessary.None;
  }

  private IncludeTypes CheckInclude(
    IUserSession session,
    List<int> enabledRelationTypes,
    PublishCompositionObject pco,
    PublishCompositionRelation pcr,
    PublicationNecessary publicationNeccesary,
    int accessLevel,
    PublishCompositionObject proj)
  {
    if (proj != null && !this._options.EnableSites.Equals(proj.CompositionEnableSites))
    {
      pco.EnableSites = pco.CompositionEnableSites = proj.CompositionEnableSites;
      if (string.IsNullOrEmpty(proj.CompositionEnableSites))
        return IncludeTypes.FilteredCompositionByOTD;
    }
    if (this._options.EnableTypes != null && !this._options.EnableTypes.Contains(pco.ObjectType) || pcr != null && enabledRelationTypes != null && !enabledRelationTypes.Contains(pcr.RelationType))
      return IncludeTypes.FilteredByTypes;
    if (this._options.AccessLevel < accessLevel)
      return IncludeTypes.NoAccess;
    if (this._otdFilter.Enable)
    {
      if (this._otdFilter.InFilter(session, pco))
        return IncludeTypes.FilteredByOTD;
      this._otdFilter.SetCompositionFilter(session, pco);
    }
    if (publicationNeccesary == PublicationNecessary.Forbidden)
      return IncludeTypes.Forbidden;
    bool flag1 = SiteIDHelper.IsOwner(((ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService))).Info.Code, pco.SiteID);
    if (!flag1 && this._publishType == PublishType.Autoreplication)
      return IncludeTypes.NoChanged;
    IPublishCompositionService service = ServerServices.GetService(typeof (IPublishCompositionService)) as IPublishCompositionService;
    bool flag2 = (this._options.CompositionOptions & PublishCompositionOptions.IncludeObjectsAlways) == PublishCompositionOptions.IncludeObjectsAlways || service.IncludeObjectsAlwaysObjectTypeIDs.Contains(pco.ObjectType);
    if ((this._options.CompositionOptions & PublishCompositionOptions.ForcedPublication) == PublishCompositionOptions.ForcedPublication)
      return !(flag1 | flag2) ? IncludeTypes.FCAttributesOnly : IncludeTypes.Include;
    switch (publicationNeccesary)
    {
      case PublicationNecessary.None:
        if (proj == null || !proj.Include.Equals((object) IncludeTypes.Include))
          break;
        goto case PublicationNecessary.FCAttributes;
      case PublicationNecessary.Object:
        if (flag1)
          return IncludeTypes.Include;
        if ((this._options.CompositionOptions & PublishCompositionOptions.IncludeFreeChangeAttributes) > PublishCompositionOptions.None && this.GetFreeAttributes(session, pco.ObjectType) != null)
          return IncludeTypes.FCAttributesOnly;
        break;
      case PublicationNecessary.FCAttributes:
        return !(flag1 & flag2) ? IncludeTypes.FCAttributesOnly : IncludeTypes.Include;
    }
    return IncludeTypes.NoChanged;
  }

  private void SetCaption(string caption, PublishCompositionObject result)
  {
    if ((this._options.CompositionOptions & PublishCompositionOptions.InfoRequired) <= PublishCompositionOptions.None)
      return;
    result.Caption = caption == null || !(caption != string.Empty) ? $"[{result.ObjectID}]" : caption;
  }

  private List<int> GetFreeAttributes(IUserSession session, int objectType)
  {
    List<int> changeAttributes;
    if (!this._freeAttributesCache.TryGetValue(objectType, out changeAttributes))
    {
      changeAttributes = Helper.GetFreeChangeAttributes(session, objectType);
      this._freeAttributesCache.Add(objectType, changeAttributes);
    }
    return changeAttributes;
  }
}
