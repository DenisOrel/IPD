
// Type: Intermech.Search.UI.VirtualTree.ExtendedCellWidget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Navigator;
using System;
using System.Drawing;


namespace Intermech.Search.UI.VirtualTree;

public sealed class ExtendedCellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column) : 
  CellWidget(rowWidget, column)
{
  private CellBehaviour _cellBehaviour;
  private bool _checked;

  public bool Checked
  {
    get => this._checked;
    set
    {
      if (this._checked == value)
        return;
      if (this._cellBehaviour != CellBehaviour.CheckBox)
        throw new InvalidOperationException();
      this._checked = value;
      if (this.Tree == null)
        return;
      ((Intermech.Search.UI.VirtualTree.VirtualTree) this.Tree).OnCellWidgetPropertyChanged(this);
    }
  }

  public CellBehaviour Behaviour
  {
    get => this._cellBehaviour;
    set
    {
      if (this._cellBehaviour == value)
        return;
      this._cellBehaviour = value;
      if (this._cellBehaviour != CellBehaviour.CheckBox || this.CellData == null || this.CellData.Editor == null)
        return;
      this.CellData.Editor.Dispose();
      this.CellData.Editor = (CellEditor) null;
    }
  }

  public override void OnClick(EventArgs e)
  {
    if (this._cellBehaviour == CellBehaviour.Default)
    {
      base.OnClick(e);
    }
    else
    {
      if (this._cellBehaviour != CellBehaviour.CheckBox)
        return;
      this.Checked = !this.Checked;
    }
  }

  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
    if (this._cellBehaviour == CellBehaviour.Default)
    {
      base.PaintForeground(graphics, style, printing);
    }
    else
    {
      if (this._cellBehaviour != CellBehaviour.CheckBox)
        return;
      Rectangle contentBounds = this.GetContentBounds();
      Rectangle targetRect = new Rectangle(contentBounds.X + contentBounds.Width / 2 - 8, contentBounds.Y + contentBounds.Height / 2 - 8, 16 /*0x10*/, 16 /*0x10*/);
      if (this.Checked)
      {
        using (Icon icon = new Icon(Services.GetResourceStream("Checkbox_checked.ico")))
          graphics.DrawIcon(icon, targetRect);
      }
      else
      {
        using (Icon icon = new Icon(Services.GetResourceStream("Checkbox_unchecked.ico")))
          graphics.DrawIcon(icon, targetRect);
      }
    }
  }

  public override void UpdateData()
  {
    base.UpdateData();
    if (this.Tree == null)
      return;
    ((Intermech.Search.UI.VirtualTree.VirtualTree) this.Tree).OnInitializeCellWidget(this);
  }
}
