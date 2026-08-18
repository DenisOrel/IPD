
// Type: Intermech.Docking.Designers.TabPageDesigner
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Docking.Designers;

internal class TabPageDesigner : ParentControlDesigner
{
  private Intermech.Docking.TabPage _page;

  public override bool CanBeParentedTo(IDesigner parentDesigner)
  {
    return parentDesigner is PageControlDesigner;
  }

  public override void Initialize(IComponent component)
  {
    base.Initialize(component);
    this._page = (Intermech.Docking.TabPage) component;
  }

  protected override void OnPaintAdornments(PaintEventArgs pe)
  {
    base.OnPaintAdornments(pe);
    if (this._page.BorderStyle != Intermech.Docking.Rendering.BorderStyle.None)
      return;
    using (Pen pen = new Pen(SystemColors.ControlDark))
    {
      pen.DashStyle = DashStyle.Dot;
      Rectangle clientRectangle = this._page.ClientRectangle;
      --clientRectangle.Width;
      --clientRectangle.Height;
      pe.Graphics.DrawRectangle(pen, clientRectangle);
    }
  }

  public override SelectionRules SelectionRules => SelectionRules.Visible;
}
