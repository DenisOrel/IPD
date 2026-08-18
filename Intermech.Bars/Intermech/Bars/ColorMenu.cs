
// Type: Intermech.Bars.ColorMenu
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;


namespace Intermech.Bars
{
    public class ColorMenu : DropDownMenuItem
    {
      public int ITEMSPERLINE = 8;

      protected internal override Type DefaultChildType => typeof (ColorMenuItem);

      protected internal override PopupMenu CreatePopupMenu(IPopupMenuHost host)
      {
        return (PopupMenu) new ColorMenuPopup(this, host)
        {
          ITEMSPERLINE = this.ITEMSPERLINE
        };
      }
    }
}
