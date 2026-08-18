
// Type: Intermech.NavBars.INavigationPane
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;


namespace Intermech.NavBars
{
    public interface INavigationPane
    {
      int Index { get; set; }

      Image LargeImage { get; set; }

      Image SmallImage { get; set; }

      bool Listed { get; set; }

      bool Enabled { get; set; }

      string Text { get; set; }
    }
}
