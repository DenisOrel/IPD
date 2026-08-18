
// Type: Intermech.Navigator.Controls.ChildrenViewRowGroupTextMaker
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

public sealed class ChildrenViewRowGroupTextMaker
{
  private ChildrenView _childrenView;
  private Stack<ChildrenViewRowGroupTextMaker.RowWithCounter> _stack = new Stack<ChildrenViewRowGroupTextMaker.RowWithCounter>();

  public ChildrenViewRowGroupTextMaker(ChildrenView childrenView)
  {
    this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
  }

  public void UpdateRowGroupText()
  {
    foreach (iGRow row in (IEnumerable) this._childrenView.Grid.Rows)
    {
      while (this._stack.Count > 0 && this._stack.Peek().Row.Level >= row.Level)
        this.SetRowGroupText(this._stack.Pop());
      if (row.Type == iGRowType.AutoGroupRow || row.Type == iGRowType.ManualGroupRow)
      {
        this._stack.Push(new ChildrenViewRowGroupTextMaker.RowWithCounter(row));
      }
      else
      {
        foreach (ChildrenViewRowGroupTextMaker.RowWithCounter rowWithCounter in this._stack)
          rowWithCounter.Increment();
      }
    }
    while (this._stack.Count > 0)
      this.SetRowGroupText(this._stack.Pop());
  }

  private void SetRowGroupText(
    ChildrenViewRowGroupTextMaker.RowWithCounter rowWithCounter)
  {
    if (rowWithCounter.Row.RowTextCell == null)
      return;
    if (rowWithCounter.Row.RowTextCell.Value is ChildrenViewRowGroupTextMaker.RowGroupTextAdapter)
      ((ChildrenViewRowGroupTextMaker.RowGroupTextAdapter) rowWithCounter.Row.RowTextCell.Value).RowCount = rowWithCounter.Counter;
    else
      rowWithCounter.Row.RowTextCell.Value = (object) new ChildrenViewRowGroupTextMaker.RowGroupTextAdapter(rowWithCounter.Row.RowTextCell.Text, rowWithCounter.Counter);
  }

  private sealed class RowWithCounter
  {
    public RowWithCounter(iGRow row) => this.Row = row;

    public iGRow Row { get; private set; }

    public int Counter { get; private set; }

    public void Increment() => ++this.Counter;
  }

  private sealed class RowGroupTextAdapter
  {
    private int _rowCount;
    private string _text;

    public RowGroupTextAdapter(string originalRowText, int rowCount)
    {
      this.OriginalRowText = originalRowText;
      this.RowCount = rowCount;
    }

    public string OriginalRowText { get; private set; }

    public int RowCount
    {
      get => this._rowCount;
      set
      {
        if (this._rowCount == value)
          return;
        this._rowCount = value;
        this._text = $"({this.RowCount}) {this.OriginalRowText}";
      }
    }

    public override string ToString() => this._text;
  }
}
