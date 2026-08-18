
// Type: Intermech.Bars.ContainerBarClientPanel
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [ToolboxItem(false)]
    public class ContainerBarClientPanel : Panel
    {
      public ContainerBarClientPanel()
      {
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.SetStyle(ControlStyles.UserPaint, true);
      }

      protected override void OnPaintBackground(PaintEventArgs pevent)
      {
        if (this.Parent is ContainerBar)
          ((ToolBar) this.Parent).WorkingRenderer.DrawContainerBarClientBackground(pevent.Graphics, this.ClientRectangle);
        else
          base.OnPaintBackground(pevent);
      }
    }
}
