
// Type: Intermech.Bars.SystemButton
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class SystemButton : SystemButonBase
    {
      private ToolBarGlyphType _gliphType;

      public SystemButton(ToolBarGlyphType gliph) => this._gliphType = gliph;

      public override void Paint(Graphics g, DrawItemState state)
      {
        this.ToolBar.WorkingRenderer.DrawSystemButton(g, this.ButtonBounds, this._gliphType, state, false);
      }
    }
}
