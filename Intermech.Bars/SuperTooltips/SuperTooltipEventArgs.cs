
// Type: SuperTooltips.SuperTooltipEventArgs
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;


namespace SuperTooltips
{
    public class SuperTooltipEventArgs : EventArgs
    {
      public bool Cancel;
      public Point Location;
      public readonly object Source;
      public readonly SuperTooltipInfo TooltipInfo;

      public SuperTooltipEventArgs(object source, SuperTooltipInfo info, Point location)
      {
        this.Source = source;
        this.TooltipInfo = info;
        this.Location = location;
      }
    }
}
