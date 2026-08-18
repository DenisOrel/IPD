// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.AutoTransferPublishTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

public class AutoTransferPublishTask : PublishTask
{
  protected List<SiteCodesInfo> recordedCodes;

  public AutoTransferPublishTask(IDBAttribute attributeTaskFiles)
    : base(attributeTaskFiles)
  {
  }

  public AutoTransferPublishTask(
    long userID,
    Guid userGuid,
    string name,
    TaskType taskType,
    TaskPriority priority,
    ITransferedObject[] units,
    List<PublishCompositionObject> publishedObjects,
    ExtendedPublishOptions options,
    Packet4Publish packet,
    List<SiteCodesInfo> recordedCodes,
    IDBAttribute attributeTaskFiles)
    : base(userID, userGuid, name, taskType, priority, publishedObjects, options, units, packet, 0L, attributeTaskFiles)
  {
    this.recordedCodes = recordedCodes;
  }

  protected override void AfterCompletePublish(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector)
  {
  }

  protected override void SetCodes(IDBObject publishObject, bool isLink, char currentSiteCode)
  {
    if (isLink)
    {
      if (!string.IsNullOrEmpty(publishObject.SiteID))
        return;
      (publishObject as DBObject).SetSiteID(string.Format("{0}{0}{0}", (object) currentSiteCode));
    }
    else
    {
      SiteCodesInfo siteCodesInfo = this.recordedCodes.Find((Predicate<SiteCodesInfo>) (x => x.ObjectID == publishObject.ObjectID));
      string siteID = string.Empty + siteCodesInfo.Creator.ToString();
      char? nullable = siteCodesInfo.Owner;
      if (nullable.HasValue)
      {
        string str1 = siteID;
        nullable = siteCodesInfo.Owner;
        string str2 = nullable.Value.ToString();
        siteID = str1 + str2;
      }
      else
      {
        nullable = siteCodesInfo.CompositionOwner;
        if (nullable.HasValue)
          siteID += Consts.NoSymbol.ToString();
      }
      nullable = siteCodesInfo.CompositionOwner;
      if (nullable.HasValue)
      {
        string str3 = siteID;
        nullable = siteCodesInfo.CompositionOwner;
        string str4 = nullable.Value.ToString();
        siteID = str3 + str4;
      }
      (publishObject as DBObject).SetSiteID(siteID);
    }
  }

  protected override void SaveData(BinaryWriter bw)
  {
    base.SaveData(bw);
    if (this.recordedCodes != null && this.recordedCodes.Count > 0)
    {
      bw.Write(this.recordedCodes.Count);
      foreach (SiteCodesInfo recordedCode in this.recordedCodes)
      {
        bw.Write(recordedCode.ObjectID);
        bw.Write(recordedCode.ObjectType);
        bw.Write(recordedCode.Creator);
        BinaryWriter binaryWriter1 = bw;
        char? nullable = recordedCode.Owner;
        int num1 = (int) nullable ?? (int) Consts.NoSymbol;
        binaryWriter1.Write((char) num1);
        BinaryWriter binaryWriter2 = bw;
        nullable = recordedCode.CompositionOwner;
        int num2 = (int) nullable ?? (int) Consts.NoSymbol;
        binaryWriter2.Write((char) num2);
      }
    }
    else
      bw.Write(0);
  }

  protected override void LoadData(BinaryReader br)
  {
    base.LoadData(br);
    int capacity = br.ReadInt32();
    if (capacity == 0)
      return;
    this.recordedCodes = new List<SiteCodesInfo>(capacity);
    for (int index = 0; index < capacity; ++index)
      this.recordedCodes.Add(new SiteCodesInfo(br.ReadInt64(), br.ReadInt32(), br.ReadChar(), this.GetCodeChar(br), this.GetCodeChar(br)));
  }

  private char? GetCodeChar(BinaryReader br)
  {
    char ch = br.ReadChar();
    return (int) ch == (int) Consts.NoSymbol ? new char?() : new char?(ch);
  }
}
