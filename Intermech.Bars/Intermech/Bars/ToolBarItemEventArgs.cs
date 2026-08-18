
// Type: Intermech.Bars.ToolBarItemEventArgs
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;


namespace Intermech.Bars
{
    public class ToolBarItemEventArgs : EventArgs
    {
      private ToolbarItemBase _item;

      public ToolBarItemEventArgs(ToolbarItemBase item) => this._item = item;

      public ToolbarItemBase Item => this._item;
    }
}
