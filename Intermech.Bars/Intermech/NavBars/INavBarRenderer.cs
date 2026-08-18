
// Type: Intermech.NavBars.INavBarRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.NavBars
{
    public interface INavBarRenderer
    {
      void DrawBackground(Graphics graphics, Rectangle bounds, Color backColor);

      void DrawChevron(Graphics graphics, Rectangle bounds);

      void DrawContentPane(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        NavigationPane pane,
        Font font);

      void DrawContentPaneBackground(Graphics graphics, Rectangle bounds, DrawItemState state);

      void DrawDivider(Graphics graphics, Rectangle bounds, string text, Font font, Color foreColor);

      void DrawFooterPane(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        NavigationPane pane,
        Font font);

      void DrawFooterPaneBackground(Graphics graphics, Rectangle bounds, DrawItemState state);

      void DrawGripper(Graphics graphics, Rectangle bounds);

      void DrawHeader(Graphics graphics, Rectangle bounds, string text, Font font, Image image);

      event EventHandler RedrawRequired;
    }
}
