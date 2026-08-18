// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ObligatoryObjectsService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ObligatoryObjectsService : 
  LongLifeObject,
  IObligatoryObjectsService,
  IObligatoryObjectsRegistryService
{
  private Dictionary<(int, object), HashSet<ObligatoryElementKey>> guardedObjects;
  private object syncRoot;

  public ObligatoryObjectsService()
  {
    this.guardedObjects = new Dictionary<(int, object), HashSet<ObligatoryElementKey>>();
    this.syncRoot = new object();
  }

  public void RegisterObligatoryObject(int categoryID, object id)
  {
    this.RegisterObligatoryObjectElement(categoryID, id, ObligatoryElementKeys.GetKeyForObject());
  }

  public void RegisterObligatoryObjectElement(
    int categoryID,
    object id,
    ObligatoryElementKey elementKey)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    if (elementKey == null)
      throw new ArgumentNullException(nameof (elementKey));
    (int, object) key = (categoryID, id);
    lock (this.syncRoot)
    {
      HashSet<ObligatoryElementKey> obligatoryElementKeySet;
      if (!this.guardedObjects.TryGetValue(key, out obligatoryElementKeySet))
      {
        obligatoryElementKeySet = new HashSet<ObligatoryElementKey>();
        this.guardedObjects.Add(key, obligatoryElementKeySet);
      }
      obligatoryElementKeySet.Add(elementKey);
    }
  }

  public bool IsObligatoryObject(int categoryID, object id)
  {
    return this.IsObligatoryObjectElement(categoryID, id, ObligatoryElementKeys.GetKeyForObject());
  }

  public bool IsObligatoryObjectElement(int categoryID, object id, ObligatoryElementKey elementKey)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    if (elementKey == null)
      throw new ArgumentNullException(nameof (elementKey));
    (int, object) key = (categoryID, id);
    lock (this.syncRoot)
    {
      HashSet<ObligatoryElementKey> obligatoryElementKeySet;
      return this.guardedObjects.TryGetValue(key, out obligatoryElementKeySet) && obligatoryElementKeySet.Contains(elementKey);
    }
  }
}
