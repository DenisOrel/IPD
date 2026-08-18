
// Type: Intermech.Bars.EmptyToolbarRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class EmptyToolbarRenderer : 
      IToolBarRenderer,
      IComboBoxRenderer,
      IDisposable,
      IMenuRenderer,
      IContainerBarRenderer
    {
      public event EventHandler RedrawRequired;

      public void DrawContainerBackground(Graphics graphics, Rectangle bounds, Rectangle layoutBounds)
      {
      }

      public void DrawFloatingFormBackground(Graphics graphics, Rectangle bounds)
      {
      }

      public void DrawFloatingFormText(string text, Graphics graphics, Font font, Rectangle bounds)
      {
      }

      public void DrawMenuBarBackground(
        MenuBar menubar,
        Graphics graphics,
        Rectangle bounds,
        bool vertical)
      {
      }

      public void DrawSystemButton(
        Graphics graphics,
        Rectangle bounds,
        ToolBarGlyphType glyphType,
        DrawItemState state,
        bool floating)
      {
      }

      public void DrawToolBarActionsButton(
        Graphics graphics,
        Rectangle bounds,
        bool vertical,
        bool chevron,
        DrawItemState state,
        bool designMode)
      {
      }

      public void DrawToolBarBackground(
        ToolBar toolbar,
        Graphics graphics,
        Rectangle bounds,
        bool vertical)
      {
      }

      public void DrawToolBarGrabHandle(Graphics graphics, Rectangle bounds, bool vertical)
      {
      }

      public void DrawToolBarItem(
        ToolbarItemBase item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state,
        ToolBarTextAlign textAlign)
      {
      }

      public void DrawToolBarSeparator(Graphics graphics, Rectangle bounds, bool vertical)
      {
      }

      public void FinishToolBarRender()
      {
      }

      public void StartToolBarRender(ToolBar toolbar, bool vertical, bool rightToLeft)
      {
      }

      public StringFormat CenterStringFormat => StringFormat.GenericDefault;

      public StringFormat LeftStringFormat => StringFormat.GenericDefault;

      public void DrawComboBox(
        ComboBox comboBox,
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        bool rightToLeft)
      {
      }

      public void Dispose()
      {
      }

      public void DrawMenuActionsButton(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        DrawItemState state,
        bool designMode)
      {
      }

      public void DrawMenuBackground(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        int breakOffset,
        int breakSize,
        MenuOffset menuDirection,
        bool rightToLeft)
      {
      }

      public void DrawMenuItem(
        Graphics graphics,
        MenuButtonItem item,
        IPopupMenuHost host,
        int marginWidth,
        DrawItemState state,
        bool drawSpecial)
      {
      }

      public void DrawMenuSeparator(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        bool rightToLeft)
      {
      }

      public StringFormat MenuShortcutStringFormat => StringFormat.GenericDefault;

      public StringFormat MenuTextStringFormat => StringFormat.GenericDefault;

      public Color ShadowColor => Color.White;

      public void DrawContainerBarBackground(
        ContainerBar containerBar,
        Graphics graphics,
        Rectangle bounds,
        Rectangle clientBounds)
      {
      }

      public void DrawContainerBarClientBackground(Graphics graphics, Rectangle bounds)
      {
      }

      public void DrawContainerBarText(string text, Graphics graphics, Font font, Rectangle bounds)
      {
      }

      public void DrawContainerBarTitleBarBackground(Graphics graphics, Rectangle bounds, bool active)
      {
      }

      public void DrawContainerBarToolBarBackground(Graphics graphics, Rectangle bounds)
      {
      }

      public void LayoutContainerBar(
        Rectangle bounds,
        Size toolbarSize,
        out Rectangle titlebarBounds,
        out Rectangle toolbarBounds,
        out Rectangle clientBounds,
        out Rectangle gripperBounds)
      {
        toolbarBounds = Rectangle.Empty;
        titlebarBounds = Rectangle.Empty;
        clientBounds = Rectangle.Empty;
        gripperBounds = Rectangle.Empty;
      }
    }
}
