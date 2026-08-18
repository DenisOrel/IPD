// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionDefRowList
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionDefRowList : List<AutoSelectionDefRow>
{
  public AutoSelectionDefRow GetRow(long rowId)
  {
    return this.FirstOrDefault<AutoSelectionDefRow>((Func<AutoSelectionDefRow, bool>) (row => row.RowID == rowId));
  }
}
