// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.RootDocumentTypesCache
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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying;

internal sealed class RootDocumentTypesCache
{
  private IntegratorSettingsCacheManager integratorSettingsCacheManager;
  private IIntegratorRegistry integratorRegistry;
  private CrossIntegratorSettingsCache<ICollection<int>> documentTypesCache;

  public RootDocumentTypesCache(
    IntegratorSettingsCacheManager integratorSettingsCacheManager,
    IIntegratorRegistry integratorRegistry)
  {
    this.integratorSettingsCacheManager = integratorSettingsCacheManager;
    this.integratorRegistry = integratorRegistry;
    this.documentTypesCache = new CrossIntegratorSettingsCache<ICollection<int>>(integratorSettingsCacheManager, new Func<ICollection<int>>(this.GetDocumentTypesSlow));
  }

  public ICollection<int> DocumentTypes
  {
    [DebuggerStepThrough] get
    {
      lock (this.documentTypesCache)
        return this.documentTypesCache.Value;
    }
  }

  private ICollection<int> GetDocumentTypesSlow()
  {
    HashSet<int> items = new HashSet<int>();
    foreach (IIntegrator integrator in this.integratorRegistry.GetIntegrators())
    {
      if (IntegratorServices.Exists(integrator.Id))
      {
        ICADSettingsService service = ServiceUtils.GetService<ICADSettingsService>((object) integrator, false);
        if (service != null)
        {
          CADSettings validCadSettings = this.TryGetValidCADSettings(service);
          if (validCadSettings != null)
          {
            foreach (DocumentGroup fileDocumentGroup in (Collection<DocumentGroup>) validCadSettings.FileDocumentGroups)
            {
              if (((IEnumerable<string>) fileDocumentGroup.Flags).Contains<string>("model") || ((IEnumerable<string>) fileDocumentGroup.Flags).Contains<string>("drawing"))
              {
                foreach (GlobalId<int> documentType in fileDocumentGroup.DocumentTypes)
                  items.Add(documentType.Id);
              }
            }
          }
        }
      }
    }
    return (ICollection<int>) new ReadOnlyCollectionWrapper<int>((ICollection<int>) items);
  }

  private CADSettings TryGetValidCADSettings(ICADSettingsService cadSettingsService)
  {
    try
    {
      return cadSettingsService.GetCADSettings();
    }
    catch (Exception ex)
    {
      SuppressedExceptions.TraceException(ex, "RootDocumentTypesCache.TryGetValidCADSettings()");
    }
    return (CADSettings) null;
  }
}
