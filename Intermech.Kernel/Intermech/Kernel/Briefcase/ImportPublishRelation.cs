// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportPublishRelation
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.IO;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportPublishRelation : ImportRelation
{
  private readonly long _partObjectID;

  public ImportPublishRelation(
    UserSession session,
    ImportingRelation briefRelation,
    bool createLinksArray,
    long partObjectID)
    : this(session, briefRelation, createLinksArray, partObjectID, false)
  {
  }

  public ImportPublishRelation(
    UserSession session,
    ImportingRelation briefRelation,
    bool createLinksArray,
    long partObjectID,
    bool packetMode)
    : base(session, briefRelation, packetMode)
  {
    this.createLinksArray = createLinksArray;
    this.withAttributesCustomHandlers = true;
    this._partObjectID = partObjectID;
  }

  protected override IDBRelation FindRelation()
  {
    ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
    long int64 = Convert.ToInt64(this.BriefRelation.Relation.ProjId);
    if (SiteIDHelper.IsOwner(customService.Info.Code, DBHelper.GetSiteID(this.session, int64)))
    {
      int int32 = Convert.ToInt32(this.BriefRelation.Relation.RelationType);
      IDBRelationsApplicability applicability = this.session.GetRelationsApplicabilityCollection().GetApplicability(int32, DBHelper.GetObjectTypeID(this.session, this._partObjectID), DBHelper.GetObjectTypeID(this.session, int64));
      if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && (applicability.Options & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.None)
        return this.session.GetRelation(int64, this._partObjectID, int32, true);
    }
    return base.FindRelation();
  }

  protected override bool CheckFileName(
    AttributeRecord attr,
    long id,
    bool refresh,
    bool throwException)
  {
    IImportRulesService service;
    if ((service = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, false)) == null || !service.RenameCoincidenceFileNames || attr.StringValue == null)
      return base.CheckFileName(attr, id, refresh, throwException);
    if (!base.CheckFileName(attr, id, refresh, false))
    {
      string fileName = Convert.ToString(attr.StringValue);
      FileInfo fileInfo = new FileInfo(fileName);
      attr.StringValue = (object) fileName.Insert(fileName.Length - fileInfo.Extension.Length, $"_{Guid.NewGuid()}");
    }
    return true;
  }
}
