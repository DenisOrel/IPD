// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.CreateObjectsResult
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.GroupAttributesChanging;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

[Serializable]
public sealed class CreateObjectsResult
{
  public static CreateObjectsResult Empty
  {
    get
    {
      return new CreateObjectsResult(new ObjectBlank[0], new Dictionary<long, long>(), new Dictionary<long, string>());
    }
  }

  public CreateObjectsResult(
    ObjectBlank[] objects,
    Dictionary<long, long> newObjects,
    Dictionary<long, string> errors)
  {
    if (objects == null)
      throw new ArgumentNullException(nameof (objects));
    if (newObjects == null)
      throw new ArgumentNullException("newObjectsMap");
    if (errors == null)
      throw new ArgumentNullException(nameof (errors));
    this.Objects = objects;
    this.NewObjects = newObjects;
    this.Errors = errors;
  }

  public ObjectBlank[] Objects { get; private set; }

  public Dictionary<long, long> NewObjects { get; private set; }

  public Dictionary<long, string> Errors { get; private set; }

  public CreateObjectsResult Merge(CreateObjectsResult other)
  {
    return new CreateObjectsResult(other.Objects, this.NewObjects.Union<KeyValuePair<long, long>>((IEnumerable<KeyValuePair<long, long>>) other.NewObjects).ToDictionary<KeyValuePair<long, long>, long, long>((Func<KeyValuePair<long, long>, long>) (o => o.Key), (Func<KeyValuePair<long, long>, long>) (o => o.Value)), other.Errors);
  }
}
