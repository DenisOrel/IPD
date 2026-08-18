// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.TableRefsNodeID
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.Interfaces;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase.Views;

internal class TableRefsNodeID : INodeID
{
  internal long pdmOptionID;
  private object cookie;

  public TableRefsNodeID()
  {
  }

  public TableRefsNodeID(long optionID) => this.pdmOptionID = optionID;

  public int CategoryID
  {
    [DebuggerStepThrough] get => 1;
  }

  public int TypeID
  {
    [DebuggerStepThrough] get => 0;
  }

  public object Cookie
  {
    [DebuggerStepThrough] get => this.cookie;
    [DebuggerStepThrough] set => this.cookie = value;
  }

  public override bool Equals(object obj)
  {
    return obj is TableRefsNodeID tableRefsNodeId && this.pdmOptionID == tableRefsNodeId.pdmOptionID;
  }

  public override int GetHashCode() => this.pdmOptionID.GetHashCode();
}
