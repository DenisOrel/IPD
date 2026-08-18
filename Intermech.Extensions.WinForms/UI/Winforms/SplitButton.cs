// Decompiled with JetBrains decompiler
// Type: Intermech.UI.Winforms.SplitButton
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI.Winforms;

public class SplitButton : Button
{
  private bool _showDropDownMenu = true;
  private bool _calculateSplitRect = true;
  private int _splitHeight;
  private int _splitWidth;
  private ImageList _defaultSplitImages;
  private IContainer components;

  [Browsable(true)]
  [Category("Action")]
  [Description("Вызывается при показе контекстного меню")]
  public event ShowMenuStripEventHandler ShowMenuStrip;

  [Browsable(true)]
  [Category("Action")]
  [Description("Occurs when the button part of the SplitButton is clicked.")]
  public event EventHandler ButtonClick;

  [Browsable(true)]
  [Category("Action")]
  [Description("Occurs when the button part of the SplitButton is clicked.")]
  public event EventHandler ButtonDoubleClick;

  [Category("Behavior")]
  [Description("Indicates whether the double click event is raised on the SplitButton")]
  [DefaultValue(false)]
  public bool DoubleClickedEnabled { get; set; }

  [Category("Split Button")]
  [Description("Indicates whether the SplitButton shows the drop down menu.")]
  [DefaultValue(true)]
  public bool ShowDropDownMenu
  {
    get => this._showDropDownMenu;
    set
    {
      this._showDropDownMenu = value;
      this.SetSplit(this.Enabled ? this.NormalImage : this.DisabledImage);
    }
  }

  [Category("Split Button")]
  [Description("Indicates whether the SplitButton always shows the drop down menu even if the button part of the SplitButton is clicked.")]
  [DefaultValue(false)]
  public bool AlwaysDropDown { get; set; }

  [Category("Split Button")]
  [Description("Indicates whether the SplitButton always shows the Hover image status in the split part even if the button part of the SplitButton is hovered.")]
  [DefaultValue(false)]
  public bool AlwaysHoverChange { get; set; }

  [Category("Split Button")]
  [Description("Indicates whether the split rectange must be calculated (basing on Split image size)")]
  [DefaultValue(true)]
  public bool CalculateSplitRect
  {
    get => this._calculateSplitRect;
    set
    {
      int num1 = this._calculateSplitRect ? 1 : 0;
      this._calculateSplitRect = value;
      int num2 = this._calculateSplitRect ? 1 : 0;
      if (num1 == num2 || this._splitWidth <= 0 || this._splitHeight <= 0)
        return;
      this.InitDefaultSplitImages(true);
    }
  }

  [Category("Split Button")]
  [Description("Indicates whether the split height must be filled to the button height even if the split image height is lower.")]
  [DefaultValue(true)]
  public bool FillSplitHeight { get; set; } = true;

  [Category("Split Button")]
  [Description("The split height (ignored if CalculateSplitRect is setted to true).")]
  [DefaultValue(0)]
  public int SplitHeight
  {
    get => this._splitHeight;
    set
    {
      this._splitHeight = value;
      if (this._calculateSplitRect || this._splitWidth <= 0 || this._splitHeight <= 0)
        return;
      this.InitDefaultSplitImages(true);
    }
  }

  [Category("Split Button")]
  [Description("The split width (ignored if CalculateSplitRect is setted to true).")]
  [DefaultValue(0)]
  public int SplitWidth
  {
    get => this._splitWidth;
    set
    {
      this._splitWidth = value;
      if (this._calculateSplitRect || this._splitWidth <= 0 || this._splitHeight <= 0)
        return;
      this.InitDefaultSplitImages(true);
    }
  }

  [Category("Split Button Images")]
  [Description("The Normal status image name in the ImageList.")]
  [DefaultValue("")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [Localizable(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  [TypeConverter(typeof (ImageKeyConverter))]
  public string NormalImage { get; set; }

  [Category("Split Button Images")]
  [Description("The Hover status image name in the ImageList.")]
  [DefaultValue("")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [Localizable(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  [TypeConverter(typeof (ImageKeyConverter))]
  public string HoverImage { get; set; }

  [Category("Split Button Images")]
  [Description("The Clicked status image name in the ImageList.")]
  [DefaultValue("")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [Localizable(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  [TypeConverter(typeof (ImageKeyConverter))]
  public string ClickedImage { get; set; }

  [Category("Split Button Images")]
  [Description("The Disabled status image name in the ImageList.")]
  [DefaultValue("")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [Localizable(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  [TypeConverter(typeof (ImageKeyConverter))]
  public string DisabledImage { get; set; }

  [Category("Split Button Images")]
  [Description("The Focused status image name in the ImageList.")]
  [DefaultValue("")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [Localizable(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  [TypeConverter(typeof (ImageKeyConverter))]
  public string FocusedImage { get; set; }

  public SplitButton() => this.InitializeComponent();

  protected override void OnCreateControl()
  {
    this.ImageAlign = ContentAlignment.MiddleRight;
    this.InitDefaultSplitImages();
    if (this.ImageList == null)
      this.ImageList = this._defaultSplitImages;
    this.SetSplit(this.Enabled ? this.NormalImage : this.DisabledImage);
    base.OnCreateControl();
  }

  private void InitDefaultSplitImages() => this.InitDefaultSplitImages(false);

  private void InitDefaultSplitImages(bool refresh)
  {
    if (string.IsNullOrEmpty(this.NormalImage))
      this.NormalImage = "Normal";
    if (string.IsNullOrEmpty(this.HoverImage))
      this.HoverImage = "Hover";
    if (string.IsNullOrEmpty(this.ClickedImage))
      this.ClickedImage = "Clicked";
    if (string.IsNullOrEmpty(this.DisabledImage))
      this.DisabledImage = "Disabled";
    if (string.IsNullOrEmpty(this.FocusedImage))
      this.FocusedImage = "Focused";
    if (this._defaultSplitImages == null)
      this._defaultSplitImages = new ImageList();
    if (!(this._defaultSplitImages.Images.Count == 0 | refresh))
      return;
    if (this._defaultSplitImages.Images.Count > 0)
      this._defaultSplitImages.Images.Clear();
    try
    {
      int width = this._calculateSplitRect || this._splitWidth <= 0 ? 18 : this._splitWidth;
      int num1 = (this.CalculateSplitRect || this.SplitHeight <= 0 ? this.Height : this.SplitHeight) - 8;
      this._defaultSplitImages.ImageSize = new Size(width, num1);
      int num2 = width / 2;
      int x = num2 + num2 % 2;
      int num3 = num1 / 2;
      Pen pen = new Pen(this.ForeColor, 1f);
      SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
      Bitmap bitmap1 = new Bitmap(width, num1);
      Graphics graphics1 = Graphics.FromImage((Image) bitmap1);
      graphics1.CompositingQuality = CompositingQuality.HighQuality;
      graphics1.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, num1 - 2));
      graphics1.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, num1));
      graphics1.FillPolygon((Brush) solidBrush1, new Point[3]
      {
        new Point(x - 2, num3 - 1),
        new Point(x + 3, num3 - 1),
        new Point(x, num3 + 2)
      });
      graphics1.Dispose();
      Bitmap bitmap2 = new Bitmap(width, num1);
      Graphics graphics2 = Graphics.FromImage((Image) bitmap2);
      graphics2.CompositingQuality = CompositingQuality.HighQuality;
      graphics2.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, num1 - 2));
      graphics2.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, num1));
      graphics2.FillPolygon((Brush) solidBrush1, new Point[3]
      {
        new Point(x - 3, num3 - 2),
        new Point(x + 4, num3 - 2),
        new Point(x, num3 + 2)
      });
      graphics2.Dispose();
      Bitmap bitmap3 = new Bitmap(width, num1);
      Graphics graphics3 = Graphics.FromImage((Image) bitmap3);
      graphics3.CompositingQuality = CompositingQuality.HighQuality;
      graphics3.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, num1 - 2));
      graphics3.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, num1));
      graphics3.FillPolygon((Brush) solidBrush1, new Point[3]
      {
        new Point(x - 2, num3 - 1),
        new Point(x + 3, num3 - 1),
        new Point(x, num3 + 2)
      });
      graphics3.Dispose();
      Bitmap bitmap4 = new Bitmap(width, num1);
      Graphics graphics4 = Graphics.FromImage((Image) bitmap4);
      graphics4.CompositingQuality = CompositingQuality.HighQuality;
      graphics4.DrawLine(SystemPens.GrayText, new Point(1, 1), new Point(1, num1 - 2));
      using (SolidBrush solidBrush2 = new SolidBrush(SystemColors.GrayText))
        graphics4.FillPolygon((Brush) solidBrush2, new Point[3]
        {
          new Point(x - 2, num3 - 1),
          new Point(x + 3, num3 - 1),
          new Point(x, num3 + 2)
        });
      graphics4.Dispose();
      Bitmap bitmap5 = new Bitmap(width, num1);
      Graphics graphics5 = Graphics.FromImage((Image) bitmap5);
      graphics5.CompositingQuality = CompositingQuality.HighQuality;
      graphics5.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, num1 - 2));
      graphics5.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, num1));
      graphics5.FillPolygon((Brush) solidBrush1, new Point[3]
      {
        new Point(x - 2, num3 - 1),
        new Point(x + 3, num3 - 1),
        new Point(x, num3 + 2)
      });
      graphics5.Dispose();
      pen.Dispose();
      solidBrush1.Dispose();
      this._defaultSplitImages.Images.Add(this.NormalImage, (Image) bitmap1);
      this._defaultSplitImages.Images.Add(this.HoverImage, (Image) bitmap2);
      this._defaultSplitImages.Images.Add(this.ClickedImage, (Image) bitmap3);
      this._defaultSplitImages.Images.Add(this.DisabledImage, (Image) bitmap4);
      this._defaultSplitImages.Images.Add(this.FocusedImage, (Image) bitmap5);
    }
    catch
    {
    }
  }

  protected override void OnMouseMove([NotNull] MouseEventArgs mEvent)
  {
    if (this.AlwaysDropDown || this.AlwaysHoverChange || this.MouseInSplit())
    {
      if (this.Enabled)
        this.SetSplit(this.HoverImage);
    }
    else if (this.Enabled)
      this.SetSplit(this.NormalImage);
    base.OnMouseMove(mEvent);
  }

  protected override void OnMouseLeave([NotNull] EventArgs e)
  {
    if (this.Enabled)
      this.SetSplit(this.NormalImage);
    base.OnMouseLeave(e);
  }

  protected override void OnMouseDown(MouseEventArgs mEvent)
  {
    if (this.AlwaysDropDown || this.MouseInSplit())
    {
      if (this.ShowDropDownMenu && this.Enabled)
      {
        this.SetSplit(this.ClickedImage);
        bool flag = false;
        if (this.ShowMenuStrip != null)
        {
          ShowMenuStripEventArgs empty = ShowMenuStripEventArgs.Empty;
          ShowMenuStripEventHandler showMenuStrip = this.ShowMenuStrip;
          if (showMenuStrip != null)
            showMenuStrip(this, empty);
          flag = empty.Handled;
        }
        if (!flag && this.ContextMenuStrip != null && this.ContextMenuStrip.Items.Count > 0)
        {
          this.ContextMenuStrip.Show((Control) this, new Point(0, this.Height));
          return;
        }
      }
    }
    else if (this.Enabled)
      this.SetSplit(this.NormalImage);
    base.OnMouseDown(mEvent);
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    bool flag = false;
    if (e.KeyCode == Keys.Apps && this.ShowMenuStrip != null)
    {
      ShowMenuStripEventArgs empty = ShowMenuStripEventArgs.Empty;
      this.ShowMenuStrip(this, empty);
      flag = empty.Handled;
    }
    if (flag)
      return;
    base.OnKeyDown(e);
  }

  protected override void OnMouseUp(MouseEventArgs mEvent)
  {
    bool flag = false;
    if (this.AlwaysDropDown || this.AlwaysHoverChange || this.MouseInSplit())
    {
      if (this.Enabled)
        this.SetSplit(this.HoverImage);
    }
    else if (this.Enabled)
      this.SetSplit(this.NormalImage);
    if (this.ShowDropDownMenu && this.ShowMenuStrip != null && mEvent.Button == MouseButtons.Right)
    {
      ShowMenuStripEventArgs empty = ShowMenuStripEventArgs.Empty;
      this.ShowMenuStrip(this, empty);
      flag = empty.Handled;
    }
    if (flag)
      return;
    base.OnMouseUp(mEvent);
  }

  protected override void OnEnabledChanged([NotNull] EventArgs e)
  {
    if (!this.Enabled)
      this.SetSplit(this.DisabledImage);
    else
      this.SetSplit(this.MouseInSplit() ? this.HoverImage : this.NormalImage);
    base.OnEnabledChanged(e);
  }

  protected override void OnGotFocus([NotNull] EventArgs e)
  {
    if (this.Enabled)
      this.SetSplit(this.FocusedImage);
    base.OnGotFocus(e);
  }

  protected override void OnLostFocus([NotNull] EventArgs e)
  {
    if (this.Enabled)
      this.SetSplit(this.NormalImage);
    base.OnLostFocus(e);
  }

  protected override void OnClick([NotNull] EventArgs e)
  {
    base.OnClick(e);
    if (this.MouseInSplit() || this.AlwaysDropDown)
      return;
    EventHandler buttonClick = this.ButtonClick;
    if (buttonClick == null)
      return;
    buttonClick((object) this, e);
  }

  protected override void OnDoubleClick([NotNull] EventArgs e)
  {
    if (!this.DoubleClickedEnabled)
      return;
    base.OnDoubleClick(e);
    if (this.MouseInSplit() || this.AlwaysDropDown)
      return;
    EventHandler buttonDoubleClick = this.ButtonDoubleClick;
    if (buttonDoubleClick == null)
      return;
    buttonDoubleClick((object) this, e);
  }

  private void SetSplit([CanBeNull] string imageName)
  {
    if (this.ShowDropDownMenu)
    {
      if (imageName == null)
        return;
      ImageList imageList = this.ImageList;
      if ((imageList != null ? (imageList.Images.ContainsKey(imageName) ? 1 : 0) : 0) == 0)
        return;
      this.ImageKey = imageName;
    }
    else
      this.ImageKey = "";
  }

  public bool MouseInSplit() => this.PointInSplit(this.PointToClient(Control.MousePosition));

  public bool PointInSplit(Point pt)
  {
    Rectangle imageRect = this.GetImageRect(this.NormalImage ?? string.Empty);
    if (!this._calculateSplitRect)
    {
      imageRect.Width = this._splitWidth;
      imageRect.Height = this._splitHeight;
    }
    return imageRect.Contains(pt);
  }

  public Rectangle GetImageRect([NotNull] string imageKey)
  {
    Image image = this.GetImage(imageKey);
    if (image == null)
      return Rectangle.Empty;
    int width = image.Width + 1;
    int height = image.Height + 1;
    if (width > this.Width)
      width = this.Width;
    if (height > this.Width)
      height = this.Width;
    int x;
    int y;
    switch (this.ImageAlign)
    {
      case ContentAlignment.TopLeft:
        x = 0;
        y = 0;
        break;
      case ContentAlignment.TopCenter:
        x = (this.Width - width) / 2;
        y = 0;
        if ((this.Width - width) % 2 > 0)
        {
          ++x;
          break;
        }
        break;
      case ContentAlignment.TopRight:
        x = this.Width - width;
        y = 0;
        break;
      case ContentAlignment.MiddleLeft:
        x = 0;
        y = (this.Height - height) / 2;
        if ((this.Height - height) % 2 > 0)
        {
          ++y;
          break;
        }
        break;
      case ContentAlignment.MiddleCenter:
        x = (this.Width - width) / 2;
        y = (this.Height - height) / 2;
        if ((this.Width - width) % 2 > 0)
          ++x;
        if ((this.Height - height) % 2 > 0)
        {
          ++y;
          break;
        }
        break;
      case ContentAlignment.MiddleRight:
        x = this.Width - width;
        y = (this.Height - height) / 2;
        if ((this.Height - height) % 2 > 0)
        {
          ++y;
          break;
        }
        break;
      case ContentAlignment.BottomLeft:
        x = 0;
        y = this.Height - height;
        if ((this.Height - height) % 2 > 0)
        {
          ++y;
          break;
        }
        break;
      case ContentAlignment.BottomCenter:
        x = (this.Width - width) / 2;
        y = this.Height - height;
        if ((this.Width - width) % 2 > 0)
        {
          ++x;
          break;
        }
        break;
      case ContentAlignment.BottomRight:
        x = this.Width - width;
        y = this.Height - height;
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
    if (this.FillSplitHeight && height < this.Height)
      height = this.Height;
    if (x > 0)
      --x;
    if (y > 0)
      --y;
    return new Rectangle(x, y, width, height);
  }

  [CanBeNull]
  private Image GetImage([NotNull] string imageName)
  {
    ImageList imageList = this.ImageList;
    return (imageList != null ? (imageList.Images.ContainsKey(imageName) ? 1 : 0) : 0) != 0 ? this.ImageList.Images[imageName] : (Image) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (disposing && this._defaultSplitImages != null)
    {
      foreach (Image image in this._defaultSplitImages.Images)
        image.Dispose();
      this._defaultSplitImages.Images.Clear();
      this._defaultSplitImages.Dispose();
      this._defaultSplitImages = (ImageList) null;
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
