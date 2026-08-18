// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypesNode
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

internal class AutoSelectionTypesNode : CompositeNode, IContextAware, IDisposable
{
  private static DescriptorCollection _selectionTypes;
  private readonly AdvancedServiceContainer _services = new AdvancedServiceContainer();

  public AutoSelectionTypesNode() => this.LoadTypes();

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new AutoSelectionTypesPart(this.Services));
  }

  protected override List<PartSlot> CreateNonFolderSlots() => (List<PartSlot>) null;

  public override object GetData(INodeID nodeId, Type dataFormat)
  {
    return nodeId is AutoSelectionTypeNodeID selectionTypeNodeId && dataFormat == typeof (AutoSelectionTypeRec) ? (object) new AutoSelectionTypeRec(selectionTypeNodeId._type) : base.GetData(nodeId, dataFormat);
  }

  private void LoadTypes()
  {
    if (AutoSelectionTypesNode._selectionTypes != null)
      return;
    AutoSelectionTypesNode._selectionTypes = new DescriptorCollection();
    foreach (object obj in Enum.GetValues(typeof (AutoSelectionNodeType)))
    {
      if (obj is AutoSelectionNodeType type && type != AutoSelectionNodeType.None)
        AutoSelectionTypesNode._selectionTypes.Add((IDescriptor) new AutoSelectionTypeDescriptor(type));
    }
  }

  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this._services;
    [DebuggerStepThrough] set => this._services.AdvancedProvider = value;
  }

  public void Dispose() => this._services?.Dispose();
}
