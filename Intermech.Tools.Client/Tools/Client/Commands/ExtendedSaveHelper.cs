// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.ExtendedSaveHelper
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class ExtendedSaveHelper
{
  private CrossIntegratorSettingsCache<ICollection<int>> supportedObjectTypesCache;

  public ExtendedSaveHelper(
    IntegratorSettingsCacheManager integratorSettingsCacheManager)
  {
    this.supportedObjectTypesCache = new CrossIntegratorSettingsCache<ICollection<int>>(integratorSettingsCacheManager, new Func<ICollection<int>>(this.GetSupportedObjectTypesSlow));
  }

  public ICollection<int> SupportedObjectTypes
  {
    [DebuggerStepThrough] get
    {
      lock (this.supportedObjectTypesCache)
        return this.supportedObjectTypesCache.Value;
    }
  }

  private ICollection<int> GetSupportedObjectTypesSlow()
  {
    HashSet<int> items = new HashSet<int>();
    foreach (IIntegrator integrator in ClientContext.Integrators.GetIntegrators())
    {
      if (IntegratorServices.Exists(integrator.Id))
      {
        try
        {
          IExtendedSaveSupport service = ServiceUtils.GetService<IExtendedSaveSupport>((object) integrator, false);
          if (service != null)
          {
            foreach (LocalId<int> supportedDocumentType in (IEnumerable<LocalId<int>>) service.GetSupportedDocumentTypes())
              items.Add(supportedDocumentType.Id);
          }
        }
        catch (Exception ex)
        {
          SuppressedExceptions.TraceException(ex, "ExtendedSaveHelper.GetSupportedObjectTypesSlow()");
        }
      }
    }
    return (ICollection<int>) new ReadOnlyCollectionWrapper<int>((ICollection<int>) items);
  }
}
