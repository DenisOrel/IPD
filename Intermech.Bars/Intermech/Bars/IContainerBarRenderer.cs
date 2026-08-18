
// Type: Intermech.Bars.IContainerBarRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;


namespace Intermech.Bars
{
    public interface IContainerBarRenderer
    {
      void DrawContainerBarBackground(
        ContainerBar containerBar,
        Graphics graphics,
        Rectangle bounds,
        Rectangle clientBounds);

      void DrawContainerBarClientBackground(Graphics graphics, Rectangle bounds);

      void DrawContainerBarText(string text, Graphics graphics, Font font, Rectangle bounds);

      void DrawContainerBarTitleBarBackground(Graphics graphics, Rectangle bounds, bool active);

      void DrawContainerBarToolBarBackground(Graphics graphics, Rectangle bounds);

      void LayoutContainerBar(
        Rectangle bounds,
        Size toolbarSize,
        out Rectangle titlebarBounds,
        out Rectangle toolbarBounds,
        out Rectangle clientBounds,
        out Rectangle gripperBounds);
    }
}
