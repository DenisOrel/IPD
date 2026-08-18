
// Type: Intermech.Bars.IPopupMenuHost
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public interface IPopupMenuHost
    {
      ToolBarLayout Flow { get; }

      Font Font { get; }

      MenuAnimation MenuAnimation { get; }

      ImageList MenuImageList { get; }

      IMenuRenderer Renderer { get; }

      bool RightToLeft { get; }

      bool RightAlignMenus { get; }

      bool FullMenus { get; }

      ToolBar ToolBar { get; }
    }
}
