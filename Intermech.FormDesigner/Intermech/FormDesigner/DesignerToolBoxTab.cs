// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.DesignerToolBoxTab
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Класс, описывающий ToolBoxTab.</summary>
internal class DesignerToolBoxTab : Control
{
  private bool _collapsed;
  private bool _selected;
  private ToolboxService _parent;
  private int _headerHeight;
  private int _itemHeight;
  private Rectangle _bounds = Rectangle.Empty;
  private IMToolBoxItem _pointer;

  /// <summary>Наименование категории.</summary>
  public string Category { get; private set; }

  /// <summary>Область закладки.</summary>
  public Rectangle TabBounds => this._bounds;

  /// <summary>Выделенный элемент.</summary>
  public IMToolBoxItem SelectedItem { get; private set; }

  /// <summary>Список элементов.</summary>
  public List<IMToolBoxItem> Items { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="parent">Родитель</param>
  /// <param name="name">Наименование</param>
  public DesignerToolBoxTab(ToolboxService parent, string name)
  {
    this._parent = parent;
    this.Category = string.IsNullOrEmpty(name) ? LocalizationHolder.rm.GetString("FormDesigner_ToolBox_DefaultCategory") : name;
    this._pointer = new IMToolBoxItem(LocalizationHolder.rm.GetString("FormDesigner_165"), (System.Type) null, (System.Type) null);
    this.Items = new List<IMToolBoxItem>() { this._pointer };
    int height = TextRenderer.MeasureText(this.Category, parent.Font).Height;
    this._headerHeight = height + 6;
    this._itemHeight = height + 10;
    this._bounds = new Rectangle(0, 0, parent.Width, this._headerHeight);
  }

  /// <summary>Добавить элемент IMToolBoxItem.</summary>
  /// <param name="item">Элемент</param>
  public void AddItem(IMToolBoxItem item)
  {
    this.Items.Add(item);
    item.ItemCategory = this.Category;
  }

  /// <summary>Удалить элемент IMToolBoxItem.</summary>
  /// <param name="item">Элемент</param>
  public void RemoveItem(IMToolBoxItem item)
  {
    if (this.SelectedItem == item)
    {
      this.SelectedItem.Selected = false;
      this.SelectedItem = (IMToolBoxItem) null;
    }
    this.Items.Remove(item);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Collaps()
  {
    if (this.SelectedItem != null)
      this.SelectedItem.Selected = false;
    this.SelectedItem = (IMToolBoxItem) null;
    this._selected = this._pointer.Selected = false;
    this._collapsed = true;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Expand() => this._collapsed = false;

  /// <summary>Клик в области таба.</summary>
  /// <param name="e"></param>
  public void TabMouseClick(MouseEventArgs e)
  {
    if (this.SelectedItem != null)
    {
      this.SelectedItem.Selected = false;
      this.SelectedItem = (IMToolBoxItem) null;
    }
    if (this._selected = e.Y >= this._bounds.Top && e.Y < this._bounds.Top + this._headerHeight)
    {
      this._collapsed = !this._collapsed;
    }
    else
    {
      int num = this._bounds.Top + this._headerHeight;
      foreach (IMToolBoxItem imToolBoxItem in this.Items)
      {
        imToolBoxItem.Selected = e.Y >= num && e.Y < num + this._itemHeight;
        num += this._itemHeight;
        if (imToolBoxItem.Selected)
        {
          this.SelectedItem = imToolBoxItem != this._pointer ? imToolBoxItem : (IMToolBoxItem) null;
          break;
        }
      }
    }
  }

  /// <summary>Перемещение мыши в области таба.</summary>
  /// <param name="e"></param>
  public void TabMouseMove(MouseEventArgs e)
  {
    int num = this._bounds.Top + this._headerHeight;
    foreach (IMToolBoxItem imToolBoxItem in this.Items)
    {
      imToolBoxItem.Hovered = e.Y >= num && e.Y < num + this._itemHeight;
      num += this._itemHeight;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void ClearHovered()
  {
    this.Items.ForEach((Action<IMToolBoxItem>) (x => x.Hovered = false));
  }

  /// <summary>
  /// 
  /// </summary>
  public void ClearSelected()
  {
    this._selected = this._pointer.Selected = false;
    if (this.SelectedItem != null)
      this.SelectedItem.Selected = false;
    this.SelectedItem = (IMToolBoxItem) null;
  }

  /// <summary>
  /// 
  /// </summary>
  public void CancelSelected()
  {
    if (this._selected)
      return;
    this.ClearSelected();
    this._pointer.Selected = true;
  }

  /// <summary>
  /// 
  /// </summary>
  public void SetPointerSelected()
  {
    this.ClearSelected();
    this._collapsed = false;
    this._pointer.Selected = true;
  }

  /// <summary>Выделение элемента (перемешщение вверх).</summary>
  /// <param name="clearFirst">Необходимость снимать выделение у первого элемента (у первого элемента первого таба выделение не снимается)</param>
  /// <returns>true - если был выделен элемент</returns>
  public bool TabKeyUp(bool clearFirst)
  {
    bool flag = true;
    if (this._selected)
      flag = this._selected = !clearFirst;
    else if (this._pointer.Selected)
    {
      this._pointer.Selected = false;
      this._selected = true;
    }
    else if (this.SelectedItem != null)
    {
      int num1 = this.Items.IndexOf(this.SelectedItem);
      this.SelectedItem.Selected = false;
      if (num1 > 1)
      {
        int num2;
        this.SelectedItem = this.Items[num2 = num1 - 1];
        this.SelectedItem.Selected = true;
      }
      else
      {
        this.SelectedItem = (IMToolBoxItem) null;
        this._pointer.Selected = true;
      }
    }
    else if (this._collapsed)
    {
      this._selected = true;
    }
    else
    {
      this.SelectedItem = this.Items[this.Items.Count - 1];
      this.SelectedItem.Selected = true;
    }
    return flag;
  }

  /// <summary>Выделение элемента (перемешщение вниз).</summary>
  /// <param name="clearLast">Необходимость снимать выделение у последнего элемента (у последнего элемента последнего таба выделение не снимается)</param>
  /// <returns>true - если был выделен элемент</returns>
  public bool TabKeyDown(bool clearLast)
  {
    bool flag = true;
    if (this._selected)
    {
      if (this._collapsed)
      {
        flag = this._selected = !clearLast;
      }
      else
      {
        this._selected = false;
        this._pointer.Selected = true;
      }
    }
    else if (this._pointer.Selected)
    {
      this._pointer.Selected = false;
      this.SelectedItem = this.Items[1];
      this.SelectedItem.Selected = true;
    }
    else if (this.SelectedItem != null)
    {
      int num1 = this.Items.IndexOf(this.SelectedItem);
      if (num1 < this.Items.Count - 1)
      {
        this.SelectedItem.Selected = false;
        int num2;
        this.SelectedItem = this.Items[num2 = num1 + 1];
        this.SelectedItem.Selected = true;
      }
      else if (clearLast)
      {
        flag = this.SelectedItem.Selected = false;
        this.SelectedItem = (IMToolBoxItem) null;
      }
    }
    else
      this._selected = true;
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  public void TabKeyLeft()
  {
    if (this._selected)
    {
      this._collapsed = true;
    }
    else
    {
      if (this.SelectedItem != null)
        this.SelectedItem.Selected = false;
      this.SelectedItem = (IMToolBoxItem) null;
      this._pointer.Selected = false;
      this._selected = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void TabKeyRight()
  {
    if (!this._selected)
      return;
    if (this._collapsed)
    {
      this._collapsed = false;
    }
    else
    {
      this._selected = false;
      this._pointer.Selected = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void Tab()
  {
    if (this._selected || this._pointer.Selected)
    {
      this.SelectedItem = this.Items[1];
      this.SelectedItem.Selected = true;
      this._collapsed = this._selected = this._pointer.Selected = false;
    }
    else
    {
      this.SelectedItem.Selected = false;
      int num1 = this.Items.IndexOf(this.SelectedItem);
      if (num1 < this.Items.Count - 1)
      {
        int num2;
        this.SelectedItem = this.Items[num2 = num1 + 1];
        this.SelectedItem.Selected = true;
      }
      else
      {
        this.SelectedItem = (IMToolBoxItem) null;
        this._pointer.Selected = true;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void ShiftTab()
  {
    if (this._selected || this._pointer.Selected)
    {
      this.SelectedItem = this.Items[this.Items.Count - 1];
      this.SelectedItem.Selected = true;
      this._collapsed = this._selected = this._pointer.Selected = false;
    }
    else
    {
      this.SelectedItem.Selected = false;
      int num = this.Items.IndexOf(this.SelectedItem);
      int index;
      this.Items[index = num - 1].Selected = true;
      this.SelectedItem = index > 0 ? this.Items[index] : (IMToolBoxItem) null;
    }
  }

  /// <summary>Отрисовка всего таба.</summary>
  /// <param name="e"></param>
  public void TabPaint(PaintEventArgs e)
  {
    Rectangle clipRectangle = e.ClipRectangle;
    using (Brush brush = (Brush) new SolidBrush(this._parent.BackColor))
      e.Graphics.FillRectangle(brush, clipRectangle);
    Rectangle rect = this._bounds = new Rectangle(clipRectangle.X, clipRectangle.Y, clipRectangle.Width, this._headerHeight);
    this.DrawHeader(e.Graphics, this._bounds);
    if (this._collapsed)
      return;
    rect.Height = this._itemHeight;
    foreach (IMToolBoxItem imToolBoxItem in this.Items)
    {
      rect.Y = this.TabBounds.Bottom;
      this.DrawItem(e.Graphics, rect, imToolBoxItem);
      this._bounds.Height += this._itemHeight;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="rect"></param>
  private void DrawHeader(Graphics g, Rectangle rect)
  {
    if (this._selected)
    {
      using (Brush brush = (Brush) new SolidBrush(Color.DodgerBlue))
        g.FillRectangle(brush, rect);
    }
    g.DrawString(this.Category, this._parent.Font, this._selected ? SystemBrushes.Window : SystemBrushes.ControlText, (PointF) new Point(14, rect.Y + 3));
    int x = 5;
    int y = rect.Y + 14;
    if (this._collapsed)
    {
      using (Pen pen = new Pen(this._selected ? Color.White : Color.Black))
      {
        int num = 4;
        g.DrawLines(pen, new Point[4]
        {
          new Point(x, y - 2 * num),
          new Point(x + num, y - num),
          new Point(x, y),
          new Point(x, y - 2 * num)
        });
      }
    }
    else
    {
      using (Brush brush = (Brush) new SolidBrush(this._selected ? Color.White : Color.Black))
      {
        int num = 7;
        g.FillPolygon(brush, new Point[4]
        {
          new Point(x + num, y - num),
          new Point(x + num, y),
          new Point(x, y),
          new Point(x + num, y - num)
        });
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="rect"></param>
  /// <param name="item"></param>
  private void DrawItem(Graphics g, Rectangle rect, IMToolBoxItem item)
  {
    if (item.Selected)
    {
      using (Brush brush = (Brush) new SolidBrush(Color.DodgerBlue))
        g.FillRectangle(brush, rect);
    }
    else if (item.Hovered)
    {
      using (Brush brush = (Brush) new SolidBrush(Color.FromArgb(193, 210, 238)))
        g.FillRectangle(brush, rect);
      g.DrawRectangle(SystemPens.HotTrack, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
    }
    if (item.Bitmap != null)
      g.DrawImage((Image) item.Bitmap, 14, rect.Top + rect.Height / 2 - item.Bitmap.Height / 2);
    g.DrawString(item.DisplayName, this._parent.Font, item.Selected ? SystemBrushes.Window : SystemBrushes.ControlText, 51f, (float) ((double) rect.Top + (double) rect.Height / 2.0 - (double) this._parent.Font.Height / 2.0));
  }

  /// <summary>Отрисовать картинку указателя.</summary>
  /// <param name="g"></param>
  /// <param name="bmpPointer"></param>
  public void DrawPointer(Graphics g, Bitmap bmpPointer)
  {
    if (this._collapsed)
      return;
    Rectangle rectangle = new Rectangle(0, this._bounds.Top + this._headerHeight, this._parent.Width, this._itemHeight);
    g.DrawImage((Image) bmpPointer, 14, rectangle.Top + rectangle.Height / 2 - bmpPointer.Height / 2);
  }
}
