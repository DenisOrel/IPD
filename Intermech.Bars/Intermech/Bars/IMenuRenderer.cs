
// Type: Intermech.Bars.IMenuRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public interface IMenuRenderer
    {
      void DrawMenuActionsButton(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        DrawItemState state,
        bool designMode);

      void DrawMenuBackground(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        int breakOffset,
        int breakSize,
        MenuOffset menuDirection,
        bool rightToLeft);

      void DrawMenuItem(
        Graphics graphics,
        MenuButtonItem item,
        IPopupMenuHost host,
        int marginWidth,
        DrawItemState state,
        bool drawSpecial);

      void DrawMenuSeparator(Graphics graphics, Rectangle bounds, int marginWidth, bool rightToLeft);

      StringFormat MenuShortcutStringFormat { get; }

      StringFormat MenuTextStringFormat { get; }

      Color ShadowColor { get; }
    }
}
