
// Type: Intermech.Bars.OfficeRendererBase
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [TypeConverter(typeof (RendererConverter))]
    public abstract class OfficeRendererBase : 
      IToolBarRenderer,
      IComboBoxRenderer,
      IDisposable,
      IMenuRenderer,
      IContainerBarRenderer
    {
      private bool _customColors;

      public event EventHandler RedrawRequired;

      protected OfficeRendererBase()
      {
        this._customColors = false;
        SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
      }

      private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
      {
        if (e.Category != UserPreferenceCategory.Color || this._customColors)
          return;
        this.CalculateBaseColors();
      }

      internal void a(Graphics g, Rectangle bounds, ToolBarGlyphType gliph, System.Drawing.Color color)
      {
        using (Pen pen = new Pen(color))
        {
          int num1 = bounds.Left + bounds.Width / 2;
          int num2 = bounds.Top + bounds.Height / 2;
          switch (gliph)
          {
            case ToolBarGlyphType.Close:
              g.DrawLine(pen, num1 - 3, num2 - 3, num1 + 3, num2 + 3);
              g.DrawLine(pen, num1 - 2, num2 - 3, num1 + 4, num2 + 3);
              g.DrawLine(pen, num1 + 3, num2 - 3, num1 - 3, num2 + 3);
              g.DrawLine(pen, num1 + 4, num2 - 3, num1 - 2, num2 + 3);
              break;
            case ToolBarGlyphType.Minimize:
              g.DrawLine(pen, num1 - 3, num2 + 3, num1 + 2, num2 + 3);
              g.DrawLine(pen, num1 - 3, num2 + 4, num1 + 2, num2 + 4);
              break;
            case ToolBarGlyphType.Restore:
              g.DrawLine(pen, num1 - 4, num2 + 4, num1 + 1, num2 + 4);
              g.DrawLine(pen, num1 - 4, num2 + 4, num1 - 4, num2 - 1);
              g.DrawLine(pen, num1 + 1, num2 + 4, num1 + 1, num2 - 1);
              g.DrawLine(pen, num1 - 4, num2 - 1, num1 + 1, num2 - 1);
              g.DrawLine(pen, num1 - 4, num2, num1 + 1, num2);
              g.DrawLine(pen, num1 - 2, num2 - 1, num1 - 2, num2 - 4);
              g.DrawLine(pen, num1 - 2, num2 - 4, num1 + 3, num2 - 4);
              g.DrawLine(pen, num1 - 2, num2 - 3, num1 + 3, num2 - 3);
              g.DrawLine(pen, num1 + 3, num2 - 3, num1 + 3, num2 + 1);
              g.DrawLine(pen, num1 + 3, num2 + 1, num1 + 1, num2 + 1);
              break;
            case ToolBarGlyphType.Actions:
              g.DrawLine(pen, num1 - 4, num2 - 2, num1 + 4, num2 - 2);
              g.DrawLine(pen, num1 - 3, num2 - 1, num1 + 3, num2 - 1);
              g.DrawLine(pen, num1 - 2, num2, num1 + 2, num2);
              g.DrawLine(pen, num1 - 1, num2 + 1, num1 + 1, num2 + 1);
              g.DrawLine(pen, num1, num2 + 2, num1, num2);
              break;
          }
        }
      }

      protected virtual void CalculateBaseColors()
      {
      }

      public virtual void Dispose()
      {
        SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.UserPreferenceChanged);
      }

      public abstract void DrawButtonHighlight(
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        bool dropDown);

      protected abstract void DrawButtonItem(
        ButtonItemBase item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state,
        ToolBarTextAlign textAlign);

      public abstract void DrawComboBox(
        ComboBox comboBox,
        Graphics graphics,
        Rectangle bounds,
        DrawItemState state,
        bool rightToLeft);

      public abstract void DrawContainerBackground(
        Graphics graphics,
        Rectangle bounds,
        Rectangle layoutBounds);

      public abstract void DrawContainerBarBackground(
        ContainerBar containerBar,
        Graphics graphics,
        Rectangle bounds,
        Rectangle clientBounds);

      public abstract void DrawContainerBarClientBackground(Graphics graphics, Rectangle bounds);

      public abstract void DrawContainerBarText(
        string text,
        Graphics graphics,
        Font font,
        Rectangle bounds);

      public abstract void DrawContainerBarTitleBarBackground(
        Graphics graphics,
        Rectangle bounds,
        bool active);

      public abstract void DrawContainerBarToolBarBackground(Graphics graphics, Rectangle bounds);

      protected abstract void DrawContainerItem(
        ControlContainerItem item,
        Graphics graphics,
        Font font,
        DrawItemState state);

      public abstract void DrawFloatingFormBackground(Graphics graphics, Rectangle bounds);

      public abstract void DrawFloatingFormText(
        string text,
        Graphics graphics,
        Font font,
        Rectangle bounds);

      public abstract void DrawIconCore(
        Icon icon,
        Graphics graphics,
        DrawItemState state,
        Rectangle bounds);

      public abstract void DrawImageCore(
        Image image,
        Graphics graphics,
        DrawItemState state,
        Rectangle bounds);

      public abstract void DrawImageCore(
        ImageList imageList,
        int imageIndex,
        Graphics graphics,
        DrawItemState state,
        Rectangle bounds);

      protected abstract void DrawLabelItem(
        LabelItem item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state);

      public abstract void DrawMenuActionsButton(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        DrawItemState state,
        bool designMode);

      public abstract void DrawMenuBackground(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        int breakOffset,
        int breakSize,
        MenuOffset menuDirection,
        bool rightToLeft);

      public abstract void DrawMenuBarBackground(
        MenuBar menubar,
        Graphics graphics,
        Rectangle bounds,
        bool vertical);

      protected abstract void DrawMenuBarItem(
        MenuBarItem item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state);

      public abstract void DrawMenuItem(
        Graphics graphics,
        MenuButtonItem item,
        IPopupMenuHost host,
        int marginWidth,
        DrawItemState state,
        bool drawSpecial);

      public abstract void DrawMenuSeparator(
        Graphics graphics,
        Rectangle bounds,
        int marginWidth,
        bool rightToLeft);

      public abstract void DrawSystemButton(
        Graphics graphics,
        Rectangle bounds,
        ToolBarGlyphType glyphType,
        DrawItemState state,
        bool floating);

      protected abstract void DrawText(
        string text,
        Graphics graphics,
        Font font,
        Brush brush,
        DrawItemState state,
        Rectangle bounds,
        StringFormat textFormat);

      public abstract void DrawToolBarActionsButton(
        Graphics graphics,
        Rectangle bounds,
        bool vertical,
        bool chevron,
        DrawItemState state,
        bool designMode);

      public abstract void DrawToolBarBackground(
        ToolBar toolbar,
        Graphics graphics,
        Rectangle bounds,
        bool vertical);

      public abstract void DrawToolBarGrabHandle(Graphics graphics, Rectangle bounds, bool vertical);

      public virtual void DrawToolBarItem(
        ToolbarItemBase item,
        Graphics graphics,
        Font font,
        bool vertical,
        DrawItemState state,
        ToolBarTextAlign textAlign)
      {
        switch (item)
        {
          case MenuBarItem _:
            this.DrawMenuBarItem((MenuBarItem) item, graphics, item.Font, vertical, state);
            break;
          case ButtonItemBase _:
            this.DrawButtonItem((ButtonItemBase) item, graphics, item.Font, vertical, state, textAlign);
            break;
          case ControlContainerItem _:
            this.DrawContainerItem((ControlContainerItem) item, graphics, item.Font, state);
            break;
          case LabelItem _:
            this.DrawLabelItem((LabelItem) item, graphics, item.Font, vertical, state);
            break;
        }
      }

      public abstract void DrawToolBarSeparator(Graphics graphics, Rectangle bounds, bool vertical);

      public abstract void FinishToolBarRender();

      public abstract void LayoutContainerBar(
        Rectangle bounds,
        Size toolbarSize,
        out Rectangle titlebarBounds,
        out Rectangle toolbarBounds,
        out Rectangle clientBounds,
        out Rectangle gripperBounds);

      protected virtual void OnRedrawRequired()
      {
        if (this.RedrawRequired == null)
          return;
        this.RedrawRequired((object) this, EventArgs.Empty);
      }

      public abstract void StartToolBarRender(ToolBar toolbar, bool vertical, bool rightToLeft);

      public abstract StringFormat CenterStringFormat { get; }

      public bool CustomColors
      {
        get => this._customColors;
        set
        {
          this._customColors = value;
          if (!this._customColors)
            this.CalculateBaseColors();
          this.OnRedrawRequired();
        }
      }

      public abstract StringFormat LeftStringFormat { get; }

      public abstract StringFormat MenuShortcutStringFormat { get; }

      public abstract StringFormat MenuTextStringFormat { get; }

      public abstract System.Drawing.Color ShadowColor { get; }
    }
}
