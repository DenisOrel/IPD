// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ObjectsFromImbaseRootNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

internal class ObjectsFromImbaseRootNode : CompositeNode, IContextAware
{
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();
  private DescriptorCollection _descriptors;

  public ObjectsFromImbaseRootNode(DescriptorCollection descriptors)
  {
    this._descriptors = descriptors;
  }

  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    return new List<PartSlot>(1)
    {
      new PartSlot(Consts.ObjectsFromImbaseNodeGuid, (INodePart) new DescriptorsPart(this._descriptors))
    };
  }
}
