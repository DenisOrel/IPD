// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.AncillaryFilesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.IO;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис дополнительных файлов, используемый обработчиками документов для поиска новых дополнительных файлов и чтения их метаданных.
/// </summary>
public class AncillaryFilesService
{
  private List<AncillaryFilesProvider> providers;

  /// <summary>Создает объект.</summary>
  public AncillaryFilesService() => this.providers = new List<AncillaryFilesProvider>();

  public void Register(AncillaryFilesProvider provider)
  {
    if (provider == null)
      throw new ArgumentNullException(nameof (provider));
    lock (this.providers)
    {
      if (this.providers.Contains(provider))
        return;
      this.providers.Add(provider);
    }
  }

  public void Unregister(AncillaryFilesProvider provider)
  {
    if (provider == null)
      throw new ArgumentNullException(nameof (provider));
    lock (this.providers)
      this.providers.Remove(provider);
  }

  public PathCollection GetFiles(SectionEntity documentEntity)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    PathCollection result = new PathCollection();
    lock (this.providers)
    {
      foreach (AncillaryFilesProvider provider in this.providers)
        provider.CollectFiles(documentEntity, result);
    }
    return result;
  }
}
