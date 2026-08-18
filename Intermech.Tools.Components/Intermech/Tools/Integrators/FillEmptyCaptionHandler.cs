// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FillEmptyCaptionHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

public class FillEmptyCaptionHandler : IAction
{
  private readonly SectionEntity docItem;

  public FillEmptyCaptionHandler(SectionEntity docItem)
  {
    this.docItem = docItem != null ? docItem : throw new ArgumentNullException();
  }

  public void Perform()
  {
    ObjectSection objectSection = this.docItem.Sections.Get<ObjectSection>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObj = sessionKeeper.Session.GetObject(objectSection.ObjectId, true);
      if (!string.IsNullOrEmpty(dbObj.Caption))
        return;
      StringKey key = this.DetectCaptionAttribute(sessionKeeper.Session, dbObj);
      if (!(key != (StringKey) null))
        return;
      this.docItem.Sections.Get<AttributesSection>().WorkingSet.Update(key, (object) this.MakeUniqueCaptionFromFilename(sessionKeeper.Session));
    }
  }

  private StringKey DetectCaptionAttribute(IUserSession session, IDBObject dbObj)
  {
    int captionAttribute = session.GetObjectType(dbObj.ObjectType, true).CaptionAttribute;
    if (captionAttribute <= 0)
      return (StringKey) session.GetAttributeType(-50, true).Name;
    IDBAttribute attributeById1 = dbObj.GetAttributeByID(captionAttribute);
    if (attributeById1 != null && attributeById1.AttributeType.AttributeType == FieldTypes.ftString && attributeById1.AttributeType.Computed == ComputeValueModes.NotComputableValue)
      return (StringKey) attributeById1.Name;
    IDBAttribute attributeById2 = dbObj.GetAttributeByID(dbObj.Session.IdentHelper.NameID);
    return attributeById2 != null && attributeById2.AttributeType.Computed == ComputeValueModes.NotComputableValue ? (StringKey) IDCache.Default.Name.Text : (StringKey) null;
  }

  private string MakeUniqueCaptionFromFilename(IUserSession session)
  {
    IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    return PathUtils.GetRelativePath(FilesSection.GetMasterFile(this.docItem), service.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
  }
}
