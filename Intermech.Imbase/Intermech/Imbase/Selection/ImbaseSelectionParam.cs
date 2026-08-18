// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseSelectionParam
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseSelectionParam
{
  public ImbaseSelectionParam(
    long ownerObjectId,
    [CanBeNull] IEnumerable<int> objectTypeIds = null,
    [CanBeNull] IEnumerable<long> imbaseCatalogIds = null)
  {
    this.OwnerObjectId = ownerObjectId;
    this.ObjectTypeIds = objectTypeIds;
    this.ImbaseCatalogIds = imbaseCatalogIds;
  }

  public long OwnerObjectId { get; }

  public IEnumerable<int> ObjectTypeIds { get; }

  public IEnumerable<long> ImbaseCatalogIds { get; }
}
