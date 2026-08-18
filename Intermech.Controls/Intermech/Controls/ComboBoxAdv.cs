
// Type: Intermech.Controls.ComboBoxAdv
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.WindowsDll;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Controls;

public class ComboBoxAdv : FlatComboBox
{
  private const int WM_REFLECT = 8192 /*0x2000*/;
  private const int ConstLeftIconMargin = 0;
  private const int ConstLeftTextMargin = 2;
  [CanBeNull]
  private static Brush _graySelectionBkBrush;
  [CanBeNull]
  private static Brush _grayLightSelectionBkBrush;
  [CanBeNull]
  private static Brush _grayDarkSelectionBkBrush;
  [CanBeNull]
  private static Pen _selectedBoxBorderPen;
  [CanBeNull]
  private static Brush _whiteBkBrush;
  private int _selectedIndexBeforeDropDown = -1;
  private bool _itemsWithImages;
  private bool _showItemRemarks = true;
  private Color _remarksColor = SystemColors.GrayText;
  public ComboBoxAdv.OnGetItemCaptionDelegate OnGetItemCaption;
  public ComboBoxAdv.OnGetItemRemarksDelegate OnGetItemRemarks;
  public ComboBoxAdv.OnGetItemIconDelegate OnGetItemIcon;
  public ComboBoxAdv.OnGetItemImageDelegate OnGetItemImage;
  [NotNull]
  private readonly StringFormat _stringFormat;
  [NotNull]
  private readonly StringFormat _stringFormatWithTrimming;

  public ComboBoxAdv()
  {
    this._stringFormat = new StringFormat();
    this.DrawMode = DrawMode.OwnerDrawFixed;
    this._stringFormat.Alignment = StringAlignment.Near;
    this._stringFormat.LineAlignment = StringAlignment.Center;
    this._stringFormatWithTrimming = new StringFormat(this._stringFormat);
    this._stringFormatWithTrimming.Trimming = StringTrimming.EllipsisCharacter;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._stringFormatWithTrimming.Dispose();
      this._stringFormat.Dispose();
    }
    base.Dispose(disposing);
  }

  [NotNull]
  private static Brush GraySelectionBkBrush
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ComboBoxAdv._graySelectionBkBrush ?? (ComboBoxAdv._graySelectionBkBrush = (Brush) new SolidBrush(Color.FromArgb(220, 220, 220)));
    }
  }

  [NotNull]
  private static Brush GrayLightSelectionBkBrush
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ComboBoxAdv._grayLightSelectionBkBrush ?? (ComboBoxAdv._grayLightSelectionBkBrush = (Brush) new SolidBrush(Color.FromArgb(240 /*0xF0*/, 240 /*0xF0*/, 240 /*0xF0*/)));
    }
  }

  [NotNull]
  private static Brush GrayDarkSelectionBkBrush
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ComboBoxAdv._grayDarkSelectionBkBrush ?? (ComboBoxAdv._grayDarkSelectionBkBrush = (Brush) new SolidBrush(Color.FromArgb(200, 200, 200)));
    }
  }

  [NotNull]
  private static Pen SelectedBoxBorderPen
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ComboBoxAdv._selectedBoxBorderPen ?? (ComboBoxAdv._selectedBoxBorderPen = new Pen(Color.FromArgb(205, 205, 205)));
    }
  }

  private static Color ColorToGray(Color originalColor)
  {
    int num = (int) ((double) originalColor.R * 0.3 + (double) originalColor.G * 0.59 + (double) originalColor.B * 0.11);
    return Color.FromArgb(num, num, num);
  }

  [NotNull]
  private static Brush WhiteBkBrush
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ComboBoxAdv._whiteBkBrush ?? (ComboBoxAdv._whiteBkBrush = (Brush) new SolidBrush(SystemColors.Window));
    }
  }

  protected override void OnDropDown([NotNull] EventArgs e)
  {
    this._selectedIndexBeforeDropDown = this.SelectedIndex;
    base.OnDropDown(e);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool ItemsWithImages
  {
    get => this._itemsWithImages;
    set
    {
      if (this._itemsWithImages == value)
        return;
      this._itemsWithImages = value;
      if (!this.IsHandleCreated)
        return;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  public bool ShowItemRemarks
  {
    get => this._showItemRemarks && this.ItemHeight >= 32 /*0x20*/;
    set
    {
      if (this._showItemRemarks == value)
        return;
      this._showItemRemarks = value;
      if (!this.IsHandleCreated)
        return;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (SystemColors), "GrayText")]
  public Color RemarksColor
  {
    get => this._remarksColor;
    set
    {
      if (!(this._remarksColor != value))
        return;
      this._remarksColor = value;
      if (!this.IsHandleCreated)
        return;
      this.Invalidate();
    }
  }

  private void WmReflectDrawItem(ref Message m)
  {
    ComboBoxAdv.DRAWITEMSTRUCT lparam = m.GetLParam<ComboBoxAdv.DRAWITEMSTRUCT>();
    if ((lparam.itemState & DrawItemState.ComboBoxEdit) != DrawItemState.None)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      if (!this.GraySelection)
      {
        clientRectangle.Inflate(-2, -2);
        clientRectangle.Width -= this.DropDownButtonWidth;
      }
      else
        clientRectangle.Inflate(-1, -1);
      using (Graphics graphics = Graphics.FromHdcInternal(lparam.hDC))
      {
        graphics.Clip.MakeEmpty();
        DrawItemEventArgs drawItemEventArgs = new DrawItemEventArgs(graphics, this.Font, clientRectangle, this.SelectedIndex, DrawItemState.ComboBoxEdit);
        this.OnDrawItem(this.Focused || this.Parent is ContainerControl parent && parent.ActiveControl == this ? new DrawItemEventArgs(graphics, this.Font, clientRectangle, this.SelectedIndex, DrawItemState.ComboBoxEdit, SystemColors.HighlightText, SystemColors.Highlight) : new DrawItemEventArgs(graphics, this.Font, clientRectangle, this.SelectedIndex, DrawItemState.ComboBoxEdit, this.ForeColor, this.BackColor));
      }
      m.Result = (IntPtr) 1;
    }
    else
      base.WndProc(ref m);
  }

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 8235)
      this.WmReflectDrawItem(ref m);
    else
      base.WndProc(ref m);
  }

  protected override void OnGotFocus([NotNull] EventArgs e) => this.Invalidate();

  protected override void OnMouseEnter([NotNull] EventArgs e)
  {
    base.OnMouseEnter(e);
    this.Hoover = true;
  }

  protected override void OnMouseLeave([NotNull] EventArgs e)
  {
    base.OnMouseLeave(e);
    this.Hoover = false;
  }

  protected override void OnSelectedValueChanged([NotNull] EventArgs e)
  {
    base.OnSelectedValueChanged(e);
    this.Invalidate();
  }

  protected override void OnDrawItem(DrawItemEventArgs ea)
  {
    bool flag1 = (ea.State & DrawItemState.ComboBoxEdit) != 0;
    bool flag2 = (ea.State & DrawItemState.Selected) != DrawItemState.None || flag1 && this.Focused;
    Color foreColor;
    if (!this.GraySelection)
    {
      foreColor = ea.ForeColor;
      ea.DrawBackground();
    }
    else if (flag2 || this.Hoover & flag1)
    {
      foreColor = this.ForeColor;
      ea.Graphics.FillRectangle(!flag1 || !this.DroppedDown ? ComboBoxAdv.GraySelectionBkBrush : ComboBoxAdv.GrayDarkSelectionBkBrush, ea.Bounds);
    }
    else
    {
      foreColor = ea.ForeColor;
      ea.Graphics.FillRectangle(ComboBoxAdv.WhiteBkBrush, ea.Bounds);
    }
    if (ea.Index == -1 || ea.Index >= this.Items.Count)
      return;
    object obj = this.Items[ea.Index];
    Rectangle bounds1 = ea.Bounds;
    if (this._itemsWithImages)
    {
      int num1 = flag1 ? 2 : 0;
      Rectangle bounds2;
      if (!flag1 && this._selectedIndexBeforeDropDown == ea.Index)
      {
        Rectangle rect;
        ref Rectangle local = ref rect;
        int x = num1 + 1;
        int y = ea.Bounds.Top + 2;
        bounds2 = ea.Bounds;
        int width = bounds2.Height - 5;
        bounds2 = ea.Bounds;
        int height = bounds2.Height - 5;
        local = new Rectangle(x, y, width, height);
        ea.Graphics.FillRectangle(ComboBoxAdv.GrayLightSelectionBkBrush, rect);
        ea.Graphics.DrawRectangle(ComboBoxAdv.SelectedBoxBorderPen, rect);
      }
      Icon icon1;
      if (this.GetItemIcon(obj, out icon1))
      {
        int height1 = icon1.Height;
        int width = icon1.Width;
        Graphics graphics = ea.Graphics;
        Icon icon2 = icon1;
        int num2 = num1;
        bounds2 = ea.Bounds;
        int num3 = bounds2.Height - width >> 1;
        int x = num2 + num3;
        int y = bounds1.Bottom + bounds1.Top - height1 >> 1;
        graphics.DrawIcon(icon2, x, y);
        int num4 = num1;
        bounds2 = ea.Bounds;
        int height2 = bounds2.Height;
        int num5 = num4 + height2;
        bounds1.X += num5;
        bounds1.Width -= num5;
      }
      else
      {
        Image image1;
        if (this.GetItemImage(obj, out image1))
        {
          int height3 = image1.Height;
          int width = image1.Width;
          Graphics graphics = ea.Graphics;
          Image image2 = image1;
          int num6 = num1;
          bounds2 = ea.Bounds;
          int num7 = bounds2.Height - width >> 1;
          int x = num6 + num7;
          int y = bounds1.Bottom + bounds1.Top - height3 >> 1;
          graphics.DrawImageUnscaled(image2, x, y);
          int num8 = num1;
          bounds2 = ea.Bounds;
          int height4 = bounds2.Height;
          int num9 = num8 + height4;
          bounds1.X += num9;
          bounds1.Width -= num9;
        }
        else
        {
          int num10 = num1;
          bounds2 = ea.Bounds;
          int height = bounds2.Height;
          int num11 = num10 + height;
          bounds1.X += num11;
          bounds1.Width -= num11;
        }
      }
    }
    else
    {
      bounds1.X += 4;
      bounds1.Width -= 4;
    }
    bounds1.X += 2;
    bounds1.Width -= 2;
    string caption;
    if (!this.GetItemCaption(obj, out caption))
      caption = obj.ToString();
    StringFormat format1 = this._stringFormat;
    string remarks = (string) null;
    if (this.ShowItemRemarks && !this.GetItemRemarks(obj, out remarks))
      remarks = (string) null;
    bool flag3 = this.ShowItemRemarks && !string.IsNullOrEmpty(remarks);
    if (flag1 && this.GraySelection)
      bounds1.Width -= this.DropDownButtonWidth;
    RectangleF layoutRectangle1 = (RectangleF) bounds1;
    if (flag3)
    {
      format1 = this._stringFormatWithTrimming;
      layoutRectangle1.Height = (float) ((double) layoutRectangle1.Height / 2.0 - 1.0);
    }
    using (SolidBrush solidBrush1 = new SolidBrush(foreColor))
    {
      if (!string.IsNullOrEmpty(caption))
      {
        using (StringFormat format2 = new StringFormat(format1))
        {
          if (flag3)
          {
            format2.LineAlignment = StringAlignment.Far;
            format2.FormatFlags |= StringFormatFlags.NoWrap;
            format2.Trimming = StringTrimming.EllipsisCharacter;
          }
          if (!flag1 & flag3)
          {
            using (Font font = new Font(ea.Font, FontStyle.Bold))
              ea.Graphics.DrawString(caption, font, (Brush) solidBrush1, layoutRectangle1, format2);
          }
          else
            ea.Graphics.DrawString(caption, ea.Font, (Brush) solidBrush1, layoutRectangle1, format2);
        }
      }
      if (!string.IsNullOrEmpty(remarks))
      {
        using (StringFormat format3 = new StringFormat(this._stringFormatWithTrimming))
        {
          format3.LineAlignment = StringAlignment.Near;
          format3.FormatFlags |= StringFormatFlags.NoWrap;
          RectangleF layoutRectangle2 = new RectangleF((float) bounds1.Left, (float) (bounds1.Top + bounds1.Height / 2 + 1), (float) bounds1.Width, (float) (bounds1.Height / 2 - 1));
          if (this._remarksColor != Color.Empty & flag1)
          {
            using (SolidBrush solidBrush2 = new SolidBrush(this._remarksColor))
              ea.Graphics.DrawString(remarks, ea.Font, (Brush) solidBrush2, layoutRectangle2, format3);
          }
          else
            ea.Graphics.DrawString(remarks, ea.Font, (Brush) solidBrush1, layoutRectangle2, format3);
        }
      }
    }
    if (this.GraySelection)
      return;
    ea.DrawFocusRectangle();
  }

  protected override void OnMeasureItem([NotNull] MeasureItemEventArgs e)
  {
    base.OnMeasureItem(e);
    if (e.ItemWidth == 0)
      e.ItemWidth = this.Width;
    if (e.ItemHeight == 0)
      e.ItemHeight = this.ItemHeight;
    if (e.Index != -1 && e.Index < this.Items.Count)
    {
      int num1 = 0;
      object obj = this.Items[e.Index];
      Size size = new Size(e.ItemWidth, e.ItemHeight);
      int num2 = (!this._itemsWithImages ? num1 + 4 : num1 + e.ItemHeight) + 2;
      string caption;
      if (!this.GetItemCaption(obj, out caption))
        caption = obj.ToString();
      StringFormat format = this._stringFormat;
      string remarks = (string) null;
      if (this.ShowItemRemarks && !this.GetItemRemarks(obj, out remarks))
        remarks = (string) null;
      bool flag = this.ShowItemRemarks && !string.IsNullOrEmpty(remarks);
      size.Width -= num2;
      SizeF layoutArea = (SizeF) size;
      if (flag)
      {
        format = this._stringFormatWithTrimming;
        layoutArea.Height = (float) (size.Height / 2 - 1);
      }
      if (!string.IsNullOrEmpty(caption))
      {
        using (StringFormat stringFormat = new StringFormat(format))
        {
          if (flag)
          {
            stringFormat.LineAlignment = StringAlignment.Far;
            stringFormat.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
            stringFormat.Trimming = StringTrimming.None;
            using (Font font = new Font(this.Font, FontStyle.Bold))
            {
              layoutArea = e.Graphics.MeasureString(caption, font, layoutArea, stringFormat);
              e.ItemWidth = Math.Max(e.ItemWidth, (int) layoutArea.Width + num2 + 10);
            }
          }
          else
          {
            layoutArea = e.Graphics.MeasureString(caption, this.Font, layoutArea, stringFormat);
            e.ItemHeight = Math.Max(e.ItemHeight, (int) layoutArea.Height + 4);
          }
        }
      }
      if (!string.IsNullOrEmpty(remarks))
      {
        using (StringFormat stringFormat = new StringFormat(this._stringFormatWithTrimming))
        {
          stringFormat.LineAlignment = StringAlignment.Near;
          stringFormat.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
          stringFormat.Trimming = StringTrimming.None;
          SizeF sizeF = e.Graphics.MeasureString(remarks, this.Font, layoutArea, stringFormat);
          e.ItemWidth = Math.Max(e.ItemWidth, (int) sizeF.Width + num2 + 10);
        }
      }
    }
    if (this.DropDownWidth >= e.ItemWidth)
      return;
    this.DropDownWidth = e.ItemWidth;
  }

  [CanBeNull]
  public ImageList ImageList { get; set; }

  [ContractAnnotation("=> true, caption: notnull; => false, caption: null")]
  protected virtual bool GetItemCaption([NotNull] object item, out string caption)
  {
    if (this.OnGetItemCaption != null)
    {
      caption = this.OnGetItemCaption(item);
      return true;
    }
    if (item is IComboItemWithCaption comboItemWithCaption)
    {
      caption = comboItemWithCaption.Caption;
      return true;
    }
    caption = (string) null;
    return false;
  }

  [ContractAnnotation("=> true, remarks: notnull; => false, remarks: null")]
  protected virtual bool GetItemRemarks([NotNull] object item, out string remarks)
  {
    if (this.OnGetItemRemarks != null)
    {
      remarks = this.OnGetItemRemarks(item);
      return true;
    }
    if (item is IComboItemWithRemarks comboItemWithRemarks)
    {
      remarks = comboItemWithRemarks.Remarks;
      return true;
    }
    remarks = (string) null;
    return false;
  }

  [ContractAnnotation("=> true, icon: notnull; => false, icon: null")]
  protected virtual bool GetItemIcon([NotNull] object item, out Icon icon)
  {
    if (this.OnGetItemIcon != null)
    {
      icon = this.OnGetItemIcon(item);
      return true;
    }
    if (item is IComboItemWithIcon comboItemWithIcon)
    {
      icon = comboItemWithIcon.GetIcon(this.ItemHeight);
      return true;
    }
    icon = (Icon) null;
    return false;
  }

  [ContractAnnotation("=> true, image: notnull; => false, image: null")]
  protected virtual bool GetItemImage([NotNull] object item, out Image image)
  {
    if (this.OnGetItemImage != null)
    {
      image = this.OnGetItemImage(item);
      return true;
    }
    if (item is IComboItemWithImage comboItemWithImage)
    {
      image = comboItemWithImage.GetImage(this.ItemHeight);
      return true;
    }
    image = (Image) null;
    return false;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public override string Text
  {
    get
    {
      string caption;
      return this.SelectedItem == null || !this.GetItemCaption(this.SelectedItem, out caption) ? string.Empty : caption;
    }
    set
    {
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public class DRAWITEMSTRUCT
  {
    public int CtlType;
    public int CtlID;
    public int itemID;
    public int itemAction;
    public DrawItemState itemState;
    public IntPtr hwndItem;
    public IntPtr hDC;
    public Interop.RECT rcItem;
    public IntPtr itemData;
  }

  public delegate string OnGetItemCaptionDelegate(object item);

  public delegate string OnGetItemRemarksDelegate(object item);

  public delegate Icon OnGetItemIconDelegate(object item);

  public delegate Image OnGetItemImageDelegate(object item);
}
