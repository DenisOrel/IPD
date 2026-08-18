
// Type: Intermech.Search.Diff.ListItemDiff
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search.Diff;

public sealed class ListItemDiff : DiffBase
{
  public ListItemDiff(int index, DiffOperand firstOperand, DiffOperand secondOperand)
    : base(firstOperand, secondOperand)
  {
    this.Index = index >= 0 ? index : throw new ArgumentException();
  }

  public int Index { get; private set; }
}
