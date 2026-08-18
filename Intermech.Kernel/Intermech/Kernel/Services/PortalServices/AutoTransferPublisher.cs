// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.AutoTransferPublisher
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

public class AutoTransferPublisher : CustomObjectsPublisher
{
  protected CustomPublishDataInfo info;
  protected List<SiteCodesInfo> recordedCodes = new List<SiteCodesInfo>();

  public AutoTransferPublisher(
    CustomPublishDataInfo customInfo,
    PublishComposition composition,
    ExtendedPublishOptions options)
    : this(customInfo, composition, options, (Packet4Publish) null, false)
  {
  }

  public AutoTransferPublisher(
    CustomPublishDataInfo customInfo,
    PublishComposition composition,
    ExtendedPublishOptions options,
    Packet4Publish packet,
    bool createReceipt)
    : base(composition, options, packet, createReceipt)
  {
    this.info = customInfo;
  }

  private void GetCodes(
    IDBObject obj,
    char currentSiteCode,
    char? setOwner,
    char? setCompOwner,
    out char creator,
    out char? owner,
    out char? compOwner)
  {
    if (string.IsNullOrEmpty(obj.SiteID))
    {
      creator = currentSiteCode;
      ref char? local1 = ref owner;
      char? nullable1 = setOwner;
      char? nullable2 = new char?((char) ((int) nullable1 ?? (int) currentSiteCode));
      local1 = nullable2;
      ref char? local2 = ref compOwner;
      nullable1 = setCompOwner;
      char? nullable3 = new char?((char) ((int) nullable1 ?? (int) currentSiteCode));
      local2 = nullable3;
    }
    else
    {
      creator = obj.SiteID[0];
      char? nullable4 = new char?();
      if (obj.SiteID.Length >= 2)
        nullable4 = new char?(obj.SiteID[1]);
      if (nullable4.HasValue)
      {
        char? nullable5;
        if ((int) nullable4.Value != (int) currentSiteCode)
        {
          owner = new char?(nullable4.Value);
        }
        else
        {
          ref char? local = ref owner;
          nullable5 = setOwner;
          char? nullable6 = new char?((char) ((int) nullable5 ?? (int) nullable4.Value));
          local = nullable6;
        }
        char? nullable7 = new char?();
        if (obj.SiteID.Length >= 3)
          nullable7 = new char?(obj.SiteID[2]);
        if (nullable7.HasValue)
        {
          nullable5 = nullable7;
          int? nullable8 = nullable5.HasValue ? new int?((int) nullable5.GetValueOrDefault()) : new int?();
          int num = (int) currentSiteCode;
          if (!(nullable8.GetValueOrDefault() == num & nullable8.HasValue))
          {
            compOwner = nullable7;
          }
          else
          {
            ref char? local = ref compOwner;
            nullable5 = setCompOwner;
            char? nullable9 = new char?((char) ((int) nullable5 ?? (int) nullable7.Value));
            local = nullable9;
          }
        }
        else
        {
          ref char? local = ref compOwner;
          nullable5 = setCompOwner;
          char? nullable10 = new char?((char) ((int) nullable5 ?? (int) owner.Value));
          local = nullable10;
        }
      }
      else
      {
        owner = new char?();
        compOwner = new char?();
      }
    }
  }

  protected override void WriteCodes(ObjectTag tag, char currentSiteCode, IDBObject obj)
  {
    this.GetCodes(obj, currentSiteCode, this.info.Options.OwnerSite, this.info.Options.CompositionOwnerSite, out tag.CreatorCode, out tag.OwnerCode, out tag.CompositionOwnerCode);
    this.recordedCodes.Add(new SiteCodesInfo(obj.ObjectID, obj.ObjectType, tag.CreatorCode, tag.OwnerCode, tag.CompositionOwnerCode));
  }
}
