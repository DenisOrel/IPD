// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TablesDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;

#nullable disable
namespace Intermech.Imbase;

public class TablesDescriptor : HiveDescriptor
{
  public TablesDescriptor()
    : base(Consts.TablesNodeCategoryID, 0, LocalizationHolder.rm.GetString("Imbase.Client_139"))
  {
  }

  protected TablesDescriptor(PersistentState state)
    : this()
  {
  }

  public override void GetObjectData(PersistentState state)
  {
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new TablesDescriptor();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }
}
