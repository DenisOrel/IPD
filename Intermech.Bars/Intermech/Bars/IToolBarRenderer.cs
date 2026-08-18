
// Type: Intermech.Bars.IToolBarRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public interface IToolBarRenderer : 
      IComboBoxRenderer,
      IDisposable,
      IMenuRenderer,
      IContainerBarRenderer
    {
      event EventHandler RedrawRequired;

      void DrawContainerBackground(Graphics graphics, Rectangle bounds, Rectangle layoutBounds);

      void DrawFloatingFormBackground(Graphics graphics, Rectangle bounds);

      void DrawFloatingFormText(string text, Graphics graphics, Font font, Rectangle bounds);

      void DrawMenuBarBackground(MenuBar menubar, Graphics graphics, Rectangle bounds, bool vertical);

      void DrawSystemButton(
        Graphics graphics,
        Rectangle bounds,
        ToolBarGlyphType glyphType,
        DrawItemState state,
        bool floating);

      void DrawToolBarActionsButton(
        Graphics graphics,
        Rectangle bounds,
        bool vertical,
        bool chevron,
        DrawItemState state,
        bool designMode);

      void DrawToolBarBackground(ToolBar toolbar, Graphics graphics, Rectangle bounds, bool vertical);

      void DrawToolBarGrabHandle(Graphics graphics, Rectangle bounds, bool vertical);

      void DrawToolBarItem(
        ToolbarItemBase item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state,
        ToolBarTextAlign textAlign);

      void DrawToolBarSeparator(Graphics graphics, Rectangle bounds, bool vertical);

      void FinishToolBarRender();

      void StartToolBarRender(ToolBar toolbar, bool vertical, bool rightToLeft);

      StringFormat CenterStringFormat { get; }

      StringFormat LeftStringFormat { get; }
    }
}
