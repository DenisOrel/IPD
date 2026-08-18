// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Nodes.ImbaseCatalogFavoritesNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Nodes;

public class ImbaseCatalogFavoritesNode : CompositeNode, IContextAware
{
  private int _typeId = -1;
  private long _objID;
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();

  public ImbaseCatalogFavoritesNode(int typeID, long objID)
  {
    this._typeId = typeID;
    this._objID = objID;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    RelatedObjectsPart part = new RelatedObjectsPart(this._typeId, this._objID, RelatedObjectsRole.Composition, Intermech.Imbase.Consts.ImbaseFavoritesRelationID, (IServiceProvider) null);
    folderSlots.Add(new PartSlot(Intermech.Imbase.Consts.ImbaseFavoritesTypeGUID, (INodePart) part));
    return folderSlots;
  }

  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }
}
