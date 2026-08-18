// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DComponentArticleApiService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DComponentArticleApiService : CIArticleApiService
{
  private ICollection<StringKey> articleSyncableAttributes;
  private ICollection<StringKey> articleIdentityAttributes;

  public Drawing2DComponentArticleApiService(
    K3DCaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext)
    : base((CICaptureChangesDriver) driver, driverContext)
  {
    this.articleSyncableAttributes = (ICollection<StringKey>) new ReadOnlyCollection<StringKey>((IList<StringKey>) Drawing2DComponentArticleApiService.CreateSyncableAttributesList());
    this.articleIdentityAttributes = this.K3DDriver.MechanicalOperations.Articles.GetIdentityKeys();
  }

  private static List<StringKey> CreateSyncableAttributesList()
  {
    return new List<StringKey>()
    {
      (StringKey) IDCache.Default.Designation.Text,
      (StringKey) IDCache.Default.OKPCode.Text,
      (StringKey) IDCache.Default.Name.Text
    };
  }

  private K3DCaptureChangesDriver K3DDriver => (K3DCaptureChangesDriver) this.CIDriver;

  public override ICollection<StringKey> GetArticleSyncAttributes(SectionEntity articleItem)
  {
    if (ObjectSection.IsNewObject(articleItem))
      return this.articleSyncableAttributes;
    ValueRecord identityAttribute = DbOperations.FindIdentityAttribute(articleItem, (IEnumerable<StringKey>) this.articleIdentityAttributes, false);
    if (identityAttribute == null)
      return this.articleSyncableAttributes;
    List<StringKey> articleSyncAttributes = new List<StringKey>((IEnumerable<StringKey>) this.articleSyncableAttributes);
    articleSyncAttributes.Remove(identityAttribute.Key);
    return (ICollection<StringKey>) articleSyncAttributes;
  }
}
