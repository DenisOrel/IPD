
// Type: Intermech.Bars.ColorMenuPopup
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class ColorMenuPopup : PopupMenu
    {
      internal int ITEMSPERLINE = 8;
      private const int ITEMSIZE = 18;
      private const int SIMPLEITEMHEIGHT = 20;
      private const int SPACEBETWEENTYPES = 3;

      public ColorMenuPopup(ColorMenu menu, IPopupMenuHost host)
        : base((MenuItemBase) menu, host)
      {
        this.EnableToolTips();
      }

      protected override Size DesiredClientSize
      {
        get
        {
          Size desiredClientSize = new Size(4, 2);
          desiredClientSize.Width += this.ITEMSPERLINE * 18;
          int num1 = 0;
          int num2 = 0;
          int num3 = 0;
          bool? nullable1 = new bool?();
          bool? nullable2;
          foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.MenuItem.Items)
          {
            if (menuButtonItem is ColorMenuItem)
            {
              ++num1;
              nullable2 = nullable1;
              bool flag = false;
              if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
                ++num3;
              nullable1 = new bool?(true);
            }
            else if (menuButtonItem is TextMenuItem)
            {
              ++num2;
              nullable2 = nullable1;
              bool flag = true;
              if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
                ++num3;
              nullable1 = new bool?(false);
            }
          }
          int num4 = (int) Math.Ceiling((double) num1 / (double) this.ITEMSPERLINE);
          ref Size local = ref desiredClientSize;
          int height = local.Height;
          int num5 = num4 * 18 + num2 * 20 + num3 * 3;
          bool? nullable3 = nullable1;
          bool flag1 = false;
          int num6 = nullable3.GetValueOrDefault() == flag1 & nullable3.HasValue ? 5 : 0;
          int num7 = num5 + num6;
          local.Height = height + num7;
          return desiredClientSize;
        }
      }

      protected override void PaintChildItems(PaintEventArgs e)
      {
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.MenuItem.Items)
        {
          if (menuButtonItem is ColorMenuItem)
          {
            ColorMenuItem colorMenuItem = menuButtonItem as ColorMenuItem;
            DrawItemState state = DrawItemState.Default;
            if (this.ShouldHighlightItem((MenuButtonItem) colorMenuItem))
              state |= DrawItemState.HotLight;
            if (colorMenuItem.Checked)
              state |= DrawItemState.Checked;
            (this.Host.Renderer as OfficeRendererBase).DrawButtonHighlight(e.Graphics, colorMenuItem.ButtonBounds, state, false);
            using (SolidBrush solidBrush = new SolidBrush(colorMenuItem.Color))
            {
              e.Graphics.FillRectangle((Brush) solidBrush, colorMenuItem.colorBounds);
              if (this.MenuItem.ForeColor.ToArgb() == colorMenuItem.Color.ToArgb())
              {
                Color color = colorMenuItem.Color;
                color = Color.FromArgb((int) color.A, (int) ~color.R, (int) ~color.G, (int) ~color.B);
                Rectangle colorBounds = colorMenuItem.colorBounds;
                colorBounds.Inflate(-2, -2);
                using (Pen pen = new Pen(color))
                  e.Graphics.DrawRectangle(pen, colorBounds);
              }
            }
            e.Graphics.DrawRectangle(SystemPens.ControlDark, colorMenuItem.colorBounds);
          }
          else if (menuButtonItem is TextMenuItem)
          {
            TextMenuItem textMenuItem = menuButtonItem as TextMenuItem;
            DrawItemState state = DrawItemState.Default;
            if (this.ShouldHighlightItem(menuButtonItem))
              state |= DrawItemState.HotLight;
            if (menuButtonItem.Checked)
              state |= DrawItemState.Checked;
            (this.Host.Renderer as OfficeRendererBase).DrawButtonHighlight(e.Graphics, textMenuItem.TextBounds, state, false);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            using (SolidBrush solidBrush = new SolidBrush(Color.Black))
              e.Graphics.DrawString(menuButtonItem.Text, this.Font, (Brush) solidBrush, (RectangleF) textMenuItem.TextBounds, format);
          }
        }
      }

      protected override void LayoutChildItems(Graphics graphics, Rectangle itemDisplayArea)
      {
        int num1 = (itemDisplayArea.Width - 4) / 18;
        int x = itemDisplayArea.X + 2;
        int y = itemDisplayArea.Y + 1 - this.ScrollOffset;
        bool? nullable1 = new bool?();
        bool? nullable2;
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.MenuItem.Items)
        {
          if (menuButtonItem is ColorMenuItem)
          {
            nullable2 = nullable1;
            bool flag = false;
            if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
              y += 3;
            nullable1 = new bool?(true);
            ColorMenuItem colorMenuItem = menuButtonItem as ColorMenuItem;
            Rectangle rectangle = new Rectangle(x, y, 17, 17);
            Graphics graphics1 = graphics;
            Rectangle bounds = rectangle;
            int num2 = this.Host.RightToLeft ? 1 : 0;
            colorMenuItem.SetBounds(graphics1, bounds, false, num2 != 0);
            x += 18;
            if (x + 18 > itemDisplayArea.Right)
            {
              x = itemDisplayArea.X + 2;
              y += 18;
            }
          }
          else if (menuButtonItem is TextMenuItem)
          {
            TextMenuItem textMenuItem = menuButtonItem as TextMenuItem;
            nullable2 = nullable1;
            bool flag = true;
            if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
            {
              y += 3;
              x = itemDisplayArea.X + 2;
            }
            nullable1 = new bool?(false);
            Rectangle rectangle = new Rectangle(x, y, this.ITEMSPERLINE * 18, 20);
            Graphics graphics2 = graphics;
            Rectangle bounds = rectangle;
            int num3 = this.Host.RightToLeft ? 1 : 0;
            textMenuItem.SetBounds(graphics2, bounds, false, num3 != 0);
            y += 20;
          }
        }
      }
    }
}
