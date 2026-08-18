// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLog
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Interfaces.AutoSelection.AutoSelectionLog;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionLog;

public class AutoSelectionLog : 
  List<IAutoSelectionLogRec>,
  IAutoSelectionLog,
  IList<IAutoSelectionLogRec>,
  ICollection<IAutoSelectionLogRec>,
  IEnumerable<IAutoSelectionLogRec>,
  IEnumerable
{
  public AutoSelectionLogRec AddRec(string data)
  {
    return this.AddRec((AutoSelectionLogRec) null, (AutoSelectionNodeBase) null, data);
  }

  public AutoSelectionLogRec AddRec(AutoSelectionLogRec owner, AutoSelectionNodeBase node)
  {
    return this.AddRec(owner, node, string.Empty);
  }

  public AutoSelectionLogRec AddRec(
    AutoSelectionLogRec owner,
    AutoSelectionNodeBase node,
    string data)
  {
    AutoSelectionLogRec autoSelectionLogRec = new AutoSelectionLogRec(owner, node, data);
    this.Add((IAutoSelectionLogRec) autoSelectionLogRec);
    return autoSelectionLogRec;
  }
}
