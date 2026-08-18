// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypeNode
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

public class AutoSelectionTypeNode : CompositeNode, IContextAware
{
  private string _caption;
  private int _id;
  private IServiceProvider _services;

  public AutoSelectionTypeNode() => this.options = NodeOptions.None;

  public AutoSelectionTypeNode(int id)
  {
    this._id = id;
    this._caption = MetaDataHelper.GetLCSchemaName(id);
    this.options = NodeOptions.None;
  }

  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    [DebuggerStepThrough] set => this._services = value;
  }

  protected override List<PartSlot> CreateNonFolderSlots() => (List<PartSlot>) null;
}
