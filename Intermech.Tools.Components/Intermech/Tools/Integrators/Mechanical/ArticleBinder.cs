// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleBinder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal static class ArticleBinder
{
  public static void BindArticle(
    CaptureChangesDriverContext ctx,
    SectionEntity articleItem,
    IObjectLocator locator,
    bool requireBind)
  {
    if (ctx == null)
      throw new ArgumentNullException();
    if (articleItem == null)
      throw new ArgumentNullException();
    if (locator == null)
      throw new ArgumentNullException();
    ObjectSection objectSection = articleItem.Sections.Get<ObjectSection>();
    if (objectSection.ObjectId != 0L)
      return;
    ObjectLocatorResult objectLocatorResult = locator.LocateObject();
    if (objectLocatorResult != null)
    {
      if (ObjectSection.FindByObjectId(ctx.Database, objectLocatorResult.ObjectId, true) != null)
      {
        objectSection.ExistenceStatus = ObjectExistenceStatus.NewObject;
      }
      else
      {
        objectSection.ExistenceStatus = ObjectExistenceStatus.ExistingObject;
        objectSection.ObjectId = objectLocatorResult.ObjectId;
        objectSection.ObjectType = objectLocatorResult.ObjectType;
      }
    }
    else
    {
      if (requireBind)
        throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Attribute.Tools.Components_37"), (object) DisplaySection.GetDisplayName(articleItem)));
      objectSection.ExistenceStatus = ObjectExistenceStatus.NewObject;
    }
  }
}
