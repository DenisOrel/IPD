// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypeDescriptor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

public class AutoSelectionTypeDescriptor : HiveDescriptor
{
  private readonly AutoSelectionNodeType _type;

  public AutoSelectionTypeDescriptor(AutoSelectionNodeType type)
    : base(AutosSelectConsts.CategoryAutoSelectionTypeNode, (int) type, EnumDescConverter.GetEnumDescription((Enum) type))
  {
    this._type = type;
  }

  protected AutoSelectionTypeDescriptor(PersistentState state)
    : base(state)
  {
  }

  public override object GetData(INodeID nodeId, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new AutoSelectionTypeDescriptor(this._type);
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeId, dataFormat);
  }

  public override INode GetChild(INodeID nodeId) => base.GetChild(nodeId);
}
