// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNodeSupport.AutoSelectionObject
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Interfaces.Compositions;
using System;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNodeSupport;

[Serializable]
public class AutoSelectionObject : ICloneable
{
  protected object _value;
  protected ObjInfoItem _createdObjInfo;
  protected RelObjInfoItem _createdRelnfo;
  protected AutoSelectionNodeCommon _node;
  protected bool _needAutoSelection = true;

  public AutoSelectionObject(AutoSelectionNodeCommon node, object value)
  {
    this._node = node;
    this._value = value;
  }

  public AutoSelectionNodeCommon Node
  {
    get => this._node;
    set => this._node = value;
  }

  public object Value
  {
    get => this._value;
    set => this._value = value;
  }

  public ObjInfoItem CreatedObjInfo
  {
    get => this._createdObjInfo;
    set => this._createdObjInfo = value;
  }

  public RelObjInfoItem CreatedRelnfo
  {
    get => this._createdRelnfo;
    set => this._createdRelnfo = value;
  }

  public bool NeedAutoSelection
  {
    get => this._needAutoSelection;
    set => this._needAutoSelection = value;
  }

  public object Clone() => this.MemberwiseClone();
}
