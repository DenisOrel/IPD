// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MultiCAD.JTSourceDocumentTypesHelper
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.MultiCAD;

internal sealed class JTSourceDocumentTypesHelper
{
  private IIntegratorRegistry integratorRegistry;
  private CrossIntegratorSettingsCache<ICollection<int>> modelTypesCache;

  public JTSourceDocumentTypesHelper(
    IIntegratorRegistry integratorRegistry,
    IntegratorSettingsCacheManager integratorSettingsCacheManager)
  {
    this.integratorRegistry = integratorRegistry;
    this.modelTypesCache = new CrossIntegratorSettingsCache<ICollection<int>>(integratorSettingsCacheManager, new Func<ICollection<int>>(this.GetModelTypesSlow));
  }

  public bool IsSourceDocumentType(int objectType)
  {
    lock (this.modelTypesCache)
      return this.modelTypesCache.Value.Contains(objectType);
  }

  private ICollection<int> GetModelTypesSlow()
  {
    HashSet<int> items = new HashSet<int>();
    foreach (IIntegrator integrator in this.integratorRegistry.GetIntegrators())
    {
      if (IntegratorServices.Exists(integrator.Id))
      {
        ICADSettingsService service = ServiceUtils.GetService<ICADSettingsService>((object) integrator, false);
        if (service != null)
        {
          try
          {
            foreach (DocumentGroup allAs in CollectionUtils.FindAllAsList<DocumentGroup>((ICollection<DocumentGroup>) service.GetCADSettings().FileDocumentGroups, (Predicate<DocumentGroup>) (group => group.Name == "Part" || group.Name == "Assembly")))
            {
              foreach (GlobalId<int> documentType in allAs.DocumentTypes)
                items.Add(documentType.Id);
            }
          }
          catch (Exception ex)
          {
            SuppressedExceptions.TraceException(ex, "JTSourceDocumentTypesHelper.GetModelTypesSlow()");
          }
        }
      }
    }
    return (ICollection<int>) new ReadOnlyCollectionWrapper<int>((ICollection<int>) items);
  }
}
