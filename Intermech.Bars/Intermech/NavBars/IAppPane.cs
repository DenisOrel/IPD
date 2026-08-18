
// Type: Intermech.NavBars.IAppPane
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;


namespace Intermech.NavBars
{
    public interface IAppPane : INavigationPane
    {
      IAppItem Add(string text, EventHandler clickHandler, Icon icon);

      IAppItem Add(string text, EventHandler clickHandler, Image image);

      IAppItem Add(string text, EventHandler clickHandler, int imageIndex);

      IAppItem[] GetItems();
    }
}
