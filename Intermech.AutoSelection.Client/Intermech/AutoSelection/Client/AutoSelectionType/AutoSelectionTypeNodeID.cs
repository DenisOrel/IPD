// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypeNodeID
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

public class AutoSelectionTypeNodeID : INodeID
{
  protected internal readonly string _caption;
  protected internal readonly AutoSelectionNodeType _type;
  private object _cookie;

  public AutoSelectionTypeNodeID()
  {
  }

  public AutoSelectionTypeNodeID(int type)
  {
    this._type = (AutoSelectionNodeType) type;
    this._caption = EnumDescConverter.GetEnumDescription((Enum) this._type);
  }

  public int CategoryID
  {
    [DebuggerStepThrough] get => AutosSelectConsts.CategoryAutoSelectionTypeNode;
  }

  public int TypeID
  {
    [DebuggerStepThrough] get => (int) this._type;
  }

  public object Cookie
  {
    [DebuggerStepThrough] get => this._cookie;
    [DebuggerStepThrough] set => this._cookie = value;
  }

  public override bool Equals(object obj)
  {
    return obj is AutoSelectionTypeNodeID selectionTypeNodeId && this._type == selectionTypeNodeId._type;
  }

  public override int GetHashCode() => this._type.GetHashCode();
}
