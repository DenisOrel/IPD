// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypesDescriptor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

public class AutoSelectionTypesDescriptor : HiveDescriptor
{
  private static string _typeCaption = string.Empty;

  public static string TypeCaption
  {
    [DebuggerStepThrough] get
    {
      if (AutoSelectionTypesDescriptor._typeCaption == string.Empty)
        AutoSelectionTypesDescriptor._typeCaption = LocalizationHolder.rm.GetString(sc_660.ssp_automatch_661());
      return AutoSelectionTypesDescriptor._typeCaption;
    }
  }

  public AutoSelectionTypesDescriptor()
    : base(AutosSelectConsts.CategoryAutoSelectionTypesNode, 0, AutoSelectionTypesDescriptor.TypeCaption)
  {
  }

  protected AutoSelectionTypesDescriptor(PersistentState state)
    : base(AutosSelectConsts.CategoryAutoSelectionTypesNode, 0, AutoSelectionTypesDescriptor.TypeCaption)
  {
  }

  public override void GetObjectData(PersistentState state)
  {
  }

  public override object GetData(INodeID nodeId, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new AutoSelectionTypesDescriptor();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeId, dataFormat);
  }

  public override INode GetChild(INodeID nodeId) => base.GetChild(nodeId);
}
