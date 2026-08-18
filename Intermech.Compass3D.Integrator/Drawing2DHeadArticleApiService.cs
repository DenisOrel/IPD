// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DHeadArticleApiService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Collections;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DHeadArticleApiService(
  K3DCaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext) : CIArticleApiService((CICaptureChangesDriver) driver, driverContext)
{
  private K3DCaptureChangesDriver K3DDriver => (K3DCaptureChangesDriver) this.CIDriver;

  public override ICollection<StringKey> GetArticleSyncAttributes(SectionEntity articleItem)
  {
    ICollection<StringKey> collection = base.GetArticleSyncAttributes(articleItem);
    ICollection<StringKey> dontSyncAttributes = this.GetArticleDontSyncAttributes(articleItem);
    if (dontSyncAttributes.Count != 0)
    {
      collection = (ICollection<StringKey>) new List<StringKey>((IEnumerable<StringKey>) collection);
      foreach (StringKey stringKey in (IEnumerable<StringKey>) dontSyncAttributes)
        collection.Remove(stringKey);
    }
    return collection;
  }

  private ICollection<StringKey> GetArticleDontSyncAttributes(SectionEntity articleItem)
  {
    List<StringKey> collection = new List<StringKey>();
    SectionEntity articleMainDocument = this.K3DDriver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null && this.K3DDriver.K3DSettings.AssemblyDrawings2D.ContainsType(ObjectSection.GetObjectType(articleMainDocument)))
    {
      CollectionUtils.AddNew<StringKey>((ICollection<StringKey>) collection, (StringKey) IDCache.Default.Mass.Text);
      CollectionUtils.AddNew<StringKey>((ICollection<StringKey>) collection, (StringKey) IDCache.Default.Material.Text);
      return (ICollection<StringKey>) collection;
    }
    AttributesSection attributesSection = articleItem.Sections.Get<AttributesSection>();
    ValueRecord mass = attributesSection.WorkingSet.Find((StringKey) IDCache.Default.Mass.Text);
    if (mass != null && this.MassIsEmpty(mass))
      CollectionUtils.AddNew<StringKey>((ICollection<StringKey>) collection, (StringKey) IDCache.Default.Mass.Text);
    ValueRecord material = attributesSection.WorkingSet.Find((StringKey) IDCache.Default.Material.Text);
    if (material != null && this.MaterialIsEmpty(material))
      CollectionUtils.AddNew<StringKey>((ICollection<StringKey>) collection, (StringKey) IDCache.Default.Material.Text);
    return (ICollection<StringKey>) collection;
  }

  private bool MassIsEmpty(ValueRecord mass)
  {
    return mass.IsNull || mass.DataType == typeof (MeasuredValue) && MathUtils.AlmostZero(((MeasuredValue) mass.Value).Value);
  }

  private bool MaterialIsEmpty(ValueRecord material)
  {
    if (material.IsNull)
      return true;
    if (material.DataType == typeof (long))
    {
      switch ((long) material.Value)
      {
        case -1:
        case 0:
          return true;
      }
    }
    return material.DataType == typeof (string) && (string) material.Value == string.Empty;
  }
}
