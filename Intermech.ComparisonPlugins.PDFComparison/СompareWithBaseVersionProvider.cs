// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.СompareWithBaseVersionProvider
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison;

public class СompareWithBaseVersionProvider : ComparisonProvider
{
  protected override void SetComparedVersions(long firstItem, long secondItem)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long idByObjectId = sessionKeeper.Session.GetIDByObjectID(firstItem);
      long objectId = sessionKeeper.Session.GetObjectBaseVersionByID(idByObjectId, false).ObjectID;
      base.SetComparedVersions(firstItem, objectId);
    }
  }
}
