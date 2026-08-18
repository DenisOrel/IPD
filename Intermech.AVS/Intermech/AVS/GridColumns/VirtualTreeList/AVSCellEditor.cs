// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.AVSCellEditor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

public class AVSCellEditor : CellEditor
{
  public AVSCellEditor()
  {
  }

  public AVSCellEditor(Control c)
    : base(c)
  {
  }

  public override void LayoutControl(Control control, Rectangle bounds, bool showControl)
  {
    if (bounds.Y < 5)
      showControl = false;
    base.LayoutControl(control, bounds, showControl);
  }
}
