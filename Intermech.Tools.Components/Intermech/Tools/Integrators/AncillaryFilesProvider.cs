// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.AncillaryFilesProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.IO;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

public abstract class AncillaryFilesProvider
{
  public void CollectFiles(SectionEntity documentEntity, PathCollection result)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    if (result == null)
      throw new ArgumentNullException(nameof (result));
    this.DoCollectFiles(documentEntity, result);
  }

  protected abstract void DoCollectFiles(SectionEntity documentEntity, PathCollection result);
}
