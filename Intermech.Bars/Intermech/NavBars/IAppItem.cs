
// Type: Intermech.NavBars.IAppItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;


namespace Intermech.NavBars
{
    public interface IAppItem
    {
      string Text { get; set; }

      string ToolTipText { get; set; }

      object Tag { get; set; }

      bool Checked { get; set; }

      bool Enabled { get; set; }

      event EventHandler Click;
    }
}
