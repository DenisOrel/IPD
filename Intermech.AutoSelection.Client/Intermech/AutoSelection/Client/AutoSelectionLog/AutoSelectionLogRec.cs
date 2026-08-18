// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLogRec
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces.AutoSelection.AutoSelectionLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionLog;

public class AutoSelectionLogRec : MarshalByRefObject, IAutoSelectionLogRec
{
  private readonly AutoSelectionLogRec _owner;
  private readonly List<IAutoSelectionLogRec> _childsList;
  private string _data;
  private readonly AutoSelectionNodeBase _node;

  public AutoSelectionLogRec(AutoSelectionLogRec owner, AutoSelectionNodeBase node)
    : this(owner, node, string.Empty)
  {
  }

  public AutoSelectionLogRec(AutoSelectionLogRec owner, AutoSelectionNodeBase node, string data)
  {
    this._owner = owner;
    this._data = data;
    this._node = node;
    this._childsList = new List<IAutoSelectionLogRec>();
    if (this._owner == null)
      return;
    this._owner.ChildsList.Add((IAutoSelectionLogRec) this);
  }

  [Browsable(false)]
  public IAutoSelectionLogRec Owner => (IAutoSelectionLogRec) this._owner;

  [Browsable(false)]
  public IList<IAutoSelectionLogRec> ChildsList => (IList<IAutoSelectionLogRec>) this._childsList;

  [Browsable(false)]
  [ReadOnly(true)]
  public string Data
  {
    get => this._data;
    set => this._data = value;
  }

  [CustomCategory("Attribute.AutoSelection.Client_88")]
  [CustomDisplayName("Attribute.AutoSelection.Client_1")]
  [ReadOnly(true)]
  public string LogData => this._data;

  [CustomCategory("Attribute.AutoSelection.Client_88")]
  [CustomDisplayName("Attribute.AutoSelection.Client_2")]
  [TypeConverter(typeof (AutoSelectionTestNodeExpConverter))]
  [ReadOnly(true)]
  public AutoSelectionNodeBase Node => this._node;

  public override string ToString()
  {
    return string.IsNullOrEmpty(this._data) ? LocalizationHolder.rm.GetString("AutoSelection.Client_1") : this._data;
  }
}
