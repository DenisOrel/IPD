// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.ArticleAttributesLockHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class ArticleAttributesLockHandler : ServiceExtender
{
  private IAttributesLockService attributesLockService;
  private CrossIntegratorSettingsCache<ArticleAttributesLockHandler.AttrCache> attrCacheController;

  public ArticleAttributesLockHandler(
    IAttributesLockService attributesLockService,
    IntegratorSettingsCacheManager integratorSettingsCacheManager)
  {
    this.attributesLockService = attributesLockService;
    this.attrCacheController = new CrossIntegratorSettingsCache<ArticleAttributesLockHandler.AttrCache>(integratorSettingsCacheManager, new Func<ArticleAttributesLockHandler.AttrCache>(this.CreateEmptyCache));
  }

  protected override void DoEnable()
  {
    base.DoEnable();
    this.attributesLockService.GetLockedAttributesHandler += new EventHandler<AttributesLockArgs>(this.OnGetLockedAttributes);
  }

  protected override void DoDisable()
  {
    base.DoDisable();
    this.attributesLockService.GetLockedAttributesHandler -= new EventHandler<AttributesLockArgs>(this.OnGetLockedAttributes);
  }

  private void OnGetLockedAttributes(object sender, AttributesLockArgs e)
  {
    if (e.ElementKind != AttributableElements.Object || !PDMHelper.IsArticle(e.ElementType))
      return;
    IReadOnlyList<int> typesByArticleId = e.GetIntegratorDocumentTypesByArticleId();
    if (typesByArticleId.Count <= 0)
      return;
    try
    {
      this.DetectArticleAttributes(typesByArticleId, e.LockedAttributes);
    }
    catch
    {
    }
  }

  private ArticleAttributesLockHandler.AttrCache CreateEmptyCache()
  {
    return new ArticleAttributesLockHandler.AttrCache();
  }

  private void DetectArticleAttributes(IReadOnlyList<int> modelTypes, HashSet<int> lockedAttributes)
  {
    lock (this.attrCacheController)
    {
      ArticleAttributesLockHandler.AttrCache attrCache = this.attrCacheController.Value;
      List<int> intList;
      if (!attrCache.TryGetValue(modelTypes, out intList))
      {
        intList = new List<int>(16 /*0x10*/);
        intList.Add(IDCache.Default.Designation.Id);
        intList.Add(IDCache.Default.OKPCode.Id);
        intList.Add(IDCache.Default.Name.Id);
        intList.Add(IDCache.Default.Mass.Id);
        intList.Add(IDCache.Default.Material.Id);
        foreach (int modelType in (IEnumerable<int>) modelTypes)
        {
          IntegratorObject integrator = IntegratorServices.Find(modelType);
          if (integrator != null)
          {
            ICADSettingsService service = IntegratorServices.GetService<ICADSettingsService>(integrator, false);
            if (service != null)
            {
              foreach (GlobalId<int> articleAttribute in service.GetCADSettings().CustomArticleAttributes)
              {
                if (!intList.Contains(articleAttribute.Id))
                  intList.Add(articleAttribute.Id);
              }
            }
          }
        }
        attrCache.Add(modelTypes, intList);
      }
      foreach (int num in intList)
        lockedAttributes.Add(num);
    }
  }

  private sealed class AttrCacheComparer : IEqualityComparer<IReadOnlyList<int>>
  {
    public bool Equals(IReadOnlyList<int> x, IReadOnlyList<int> y)
    {
      switch ((x != null ? 2 : 0) | (y != null ? 1 : 0))
      {
        case 0:
          return true;
        case 1:
          return false;
        case 2:
          return false;
        case 3:
          if (x.Count != y.Count)
            return false;
          for (int index = 0; index < x.Count; ++index)
          {
            if (!x[index].Equals(y[index]))
              return false;
          }
          return true;
        default:
          throw new NotSupportedException();
      }
    }

    public int GetHashCode(IReadOnlyList<int> obj)
    {
      if (obj == null || obj.Count == 0)
        return 0;
      if (obj.Count == 1)
        return obj[0].GetHashCode();
      int hashCode = 0;
      for (int index = 0; index < obj.Count; ++index)
        hashCode ^= obj[index].GetHashCode();
      return hashCode;
    }
  }

  private sealed class AttrCache : Dictionary<IReadOnlyList<int>, List<int>>
  {
    public AttrCache()
      : base((IEqualityComparer<IReadOnlyList<int>>) new ArticleAttributesLockHandler.AttrCacheComparer())
    {
    }
  }
}
