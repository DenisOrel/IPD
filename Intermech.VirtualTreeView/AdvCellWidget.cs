// Decompiled with JetBrains decompiler
// Type: Intermech.VirtualTreeView.AdvCellWidget
// Assembly: Intermech.VirtualTreeView, Version=4.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: CFAE8D69-6554-4155-8AB7-42592C2FC48A
// Assembly location: D:\IPS\Client\Intermech.VirtualTreeView.dll

using Infralution.Controls.VirtualTree;

#nullable disable
namespace Intermech.VirtualTreeView;

public class AdvCellWidget(RowWidget rowWidget, Column column) : CellWidget(rowWidget, column)
{
  public override void StartEdit()
  {
    BeforeShowCellEditEventArgs cellEditEventArgs = (this.Tree as Intermech.VirtualTreeView.VirtualTreeView).FireBeforeShowCellEdit(this);
    if (cellEditEventArgs != null && cellEditEventArgs.Cancel)
      return;
    base.StartEdit();
  }
}
