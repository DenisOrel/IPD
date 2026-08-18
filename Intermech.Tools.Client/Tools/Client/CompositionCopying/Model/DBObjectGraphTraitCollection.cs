// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectGraphTraitCollection
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class DBObjectGraphTraitCollection
{
  private IDBObjectGraphTraitOwner ownerObject;
  private List<DBObjectGraphTrait> traitList;

  public DBObjectGraphTraitCollection(IDBObjectGraphTraitOwner ownerObject)
  {
    this.ownerObject = ownerObject != null ? ownerObject : throw new ArgumentNullException(nameof (ownerObject));
    this.traitList = new List<DBObjectGraphTrait>(2);
  }

  public void Add(DBObjectGraphTrait trait)
  {
    Type traitType = trait != null ? trait.GetType() : throw new ArgumentNullException(nameof (trait));
    if (this.TryGetByType(traitType, false) != null)
      throw new InvalidOperationException($"The trait '{traitType}' is already added.");
    trait.OwnerObject = trait.OwnerObject == null ? this.ownerObject : throw new InvalidOperationException($"The trait '{traitType}' already has the owner object.");
    this.traitList.Add(trait);
  }

  public DBObjectGraphTrait TryGetByType(Type traitType, bool throwIfNotFound)
  {
    if (traitType == (Type) null)
      throw new ArgumentNullException(nameof (traitType));
    DBObjectGraphTrait objectGraphTrait = this.traitList.Find((Predicate<DBObjectGraphTrait>) (x => x.GetType() == traitType));
    return objectGraphTrait != null || !throwIfNotFound ? objectGraphTrait : throw new InvalidOperationException($"The trait '{traitType}' is not found.");
  }
}
