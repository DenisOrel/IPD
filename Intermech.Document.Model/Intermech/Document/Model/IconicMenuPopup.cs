// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.IconicMenuPopup
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Iconic MenuPopup</summary>
public class IconicMenuPopup : PopupMenu
{
  internal int ItemsPerLine = 8;
  private const int ITEMSIZE = 25;
  private const int SIMPLEITEMHEIGHT = 25;
  private const int SPACEBETWEENTYPES = 3;

  /// <summary>Конструктор</summary>
  /// <param name="menu"></param>
  /// <param name="host"></param>
  public IconicMenuPopup(IconicMenu menu, IPopupMenuHost host)
    : base((MenuItemBase) menu, host)
  {
    this.EnableToolTips();
  }

  /// <summary>DesiredClientSize</summary>
  protected override Size DesiredClientSize
  {
    get
    {
      Size desiredClientSize = new Size(4, 2);
      desiredClientSize.Width += this.ItemsPerLine * 25;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      bool? nullable1 = new bool?();
      bool? nullable2;
      foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.MenuItem.Items)
      {
        if (menuButtonItem is TextMenuItem)
        {
          ++num2;
          nullable2 = nullable1;
          bool flag = true;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
            ++num3;
          nullable1 = new bool?(false);
        }
        else
        {
          ++num1;
          nullable2 = nullable1;
          bool flag = false;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
            ++num3;
          nullable1 = new bool?(true);
        }
      }
      int num4 = (int) Math.Ceiling((double) num1 / (double) this.ItemsPerLine);
      ref Size local = ref desiredClientSize;
      int height = local.Height;
      int num5 = num4 * 25 + num2 * 25 + num3 * 3;
      bool? nullable3 = nullable1;
      bool flag1 = false;
      int num6 = nullable3.GetValueOrDefault() == flag1 & nullable3.HasValue ? 5 : 0;
      int num7 = num5 + num6;
      local.Height = height + num7;
      return desiredClientSize;
    }
  }

  /// <summary>PaintChildItems</summary>
  /// <param name="e"></param>
  protected override void PaintChildItems(PaintEventArgs e)
  {
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.MenuItem.Items)
    {
      if (menuButtonItem is IconicMenuItem)
      {
        IconicMenuItem iconicMenuItem = menuButtonItem as IconicMenuItem;
        DrawItemState state = DrawItemState.Default;
        if (this.ShouldHighlightItem((MenuButtonItem) iconicMenuItem))
          state |= DrawItemState.HotLight;
        if (iconicMenuItem.Checked)
          state |= DrawItemState.Checked;
        (this.Host.Renderer as OfficeRendererBase).DrawButtonHighlight(e.Graphics, iconicMenuItem.ButtonBounds, state, false);
        if (iconicMenuItem.Image != null)
        {
          Rectangle iconBounds = iconicMenuItem.iconBounds;
          e.Graphics.DrawImageUnscaled(iconicMenuItem.Image, iconBounds.Left, iconBounds.Top, 16 /*0x10*/, 16 /*0x10*/);
        }
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

  /// <summary>LayoutChildItems</summary>
  /// <param name="graphics"></param>
  /// <param name="itemDisplayArea"></param>
  protected override void LayoutChildItems(Graphics graphics, Rectangle itemDisplayArea)
  {
    int num1 = (itemDisplayArea.Width - 4) / 25;
    int x = itemDisplayArea.X + 2;
    int y = itemDisplayArea.Y + 1 - this.ScrollOffset;
    bool? nullable1 = new bool?();
    bool? nullable2;
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.MenuItem.Items)
    {
      if (menuButtonItem is IconicMenuItem)
      {
        nullable2 = nullable1;
        bool flag = false;
        if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          y += 3;
        nullable1 = new bool?(true);
        IconicMenuItem iconicMenuItem = menuButtonItem as IconicMenuItem;
        Rectangle rectangle = new Rectangle(x, y, 24, 24);
        Graphics graphics1 = graphics;
        Rectangle bounds = rectangle;
        int num2 = this.Host.RightToLeft ? 1 : 0;
        iconicMenuItem.SetBounds(graphics1, bounds, false, num2 != 0);
        x += 25;
        if (x + 25 > itemDisplayArea.Right)
        {
          x = itemDisplayArea.X + 2;
          y += 25;
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
        Rectangle rectangle = new Rectangle(x, y, this.ItemsPerLine * 25, 25);
        Graphics graphics2 = graphics;
        Rectangle bounds = rectangle;
        int num3 = this.Host.RightToLeft ? 1 : 0;
        textMenuItem.SetBounds(graphics2, bounds, false, num3 != 0);
        y += 25;
      }
    }
  }
}
