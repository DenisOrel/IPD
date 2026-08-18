// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TablesNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase;

public class TablesNode : CompositeNode, IContextAware
{
  private IServiceProvider _services;

  public TablesNode() => this.options = NodeOptions.CanContainsObjectsList;

  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new ObjectsPart(Consts.ImbaseTableTypeID, this.Services));
  }
}
