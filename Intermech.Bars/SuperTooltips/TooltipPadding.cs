
// Type: SuperTooltips.TooltipPadding
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;


namespace SuperTooltips
{
    public class TooltipPadding
    {
      public int Bottom;
      public int Left;
      public int Right;
      public int Top;

      public TooltipPadding(int left, int right, int top, int bottom)
      {
        this.Left = left;
        this.Right = right;
        this.Top = top;
        this.Bottom = bottom;
      }

      [Browsable(false)]
      public int Horizontal => this.Left + this.Right;

      [Browsable(false)]
      public int Vertical => this.Top + this.Bottom;
    }
}
