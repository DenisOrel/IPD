
// Type: OfficePickers.ColorPicker.ToolStripColorPicker
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace OfficePickers.ColorPicker;

/// <summary>
/// Represents a ToolStripButtonItem that contains Color Picker control.
/// </summary>
[DefaultEvent("SelectedColorChanged")]
[DefaultProperty("Color")]
[Description("ToolStripItem that allows selecting a color from a color picker control.")]
[ToolboxItem(false)]
[ToolboxBitmap(typeof (ToolStripColorPicker), "ToolStripColorPicker")]
public class ToolStripColorPicker : ToolStripDropDownButton
{
  private ToolStripColorPickerDisplayType _buttonDisplayStyle = ToolStripColorPickerDisplayType.UnderLineAndImage;
  private bool _addColorNameToToolTip = true;
  private string _originalToolTipText = "";
  /// <summary>
  /// The color picker control that opens when clicking on the button
  /// </summary>
  private OfficeColorPicker _colorPicker = new OfficeColorPicker();
  /// <summary>Default color rectangle (under line)</summary>
  private Rectangle _colorRectangle = new Rectangle(2, 17, 14, 4);
  /// <summary>The underline picture rectangle - stretch to 14X14</summary>
  private Rectangle _pictureRectangle = new Rectangle(2, 2, 14, 14);
  private bool _showColorUnderLine = true;
  private bool _showUnderLineImage = true;
  private bool _showUnderLineText;
  private IContainer components;

  /// <summary>Occurs when the value of the Color property changes.</summary>
  [Category("Behavior")]
  [Description("Occurs when the value of the Color property changes.")]
  public event EventHandler SelectedColorChanged;

  /// <summary>
  /// Gets or sets the ToolStripColorPickerDisplayType in order to
  /// specified the display style of the button - image, text, underline etc.
  /// </summary>
  [Category("Appearance")]
  [Description("Specifies whether to display the image, text and underline on the button.")]
  [DefaultValue(typeof (ToolStripColorPickerDisplayType), "ToolStripColorPickerDisplayType.UnderLineAndImage")]
  public ToolStripColorPickerDisplayType ButtonDisplayStyle
  {
    get => this._buttonDisplayStyle;
    set
    {
      this._buttonDisplayStyle = value;
      this.UpdateDisplayStyle();
    }
  }

  /// <summary>
  /// Overrides, Gets or sets the ToolStripItem.DisplayStyle property, use
  /// the ButtonDisplayStyle instead.
  /// </summary>
  [Browsable(false)]
  public override ToolStripItemDisplayStyle DisplayStyle
  {
    get => base.DisplayStyle;
    set => base.DisplayStyle = value;
  }

  /// <summary>
  /// Gets or sets the color assign to the color picker control.
  /// </summary>
  [Category("Data")]
  [Description("Gets or sets the color assign to the color picker control.")]
  [DefaultValue(typeof (Color), "Color.Black")]
  public Color Color
  {
    get => this._colorPicker.Color;
    set
    {
      this._colorPicker.Color = value;
      this.Refresh();
      this.OnSelectedColorChanged(EventArgs.Empty);
    }
  }

  /// <summary>
  /// Gets or sets value indicating whether to render the color name to the tool tip text.
  /// </summary>
  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Value indicating whether to render the color name to the tool tip text.")]
  public bool AddColorNameToToolTip
  {
    get => this._addColorNameToToolTip;
    set => this._addColorNameToToolTip = value;
  }

  /// <summary>
  /// Gets or sets the text that appears as a tooltip in the button.
  /// the color name will be rendered to the tooltip if the AddColorNameToolTip property set to true.
  /// </summary>
  [Category("Behavior")]
  [Description("The text that appears as a tooltip (the color name will be render  automatically if defined to do so.")]
  public new string ToolTipText
  {
    get => this._originalToolTipText;
    set
    {
      this._originalToolTipText = value;
      if (this._addColorNameToToolTip)
        base.ToolTipText = $"{this._originalToolTipText} ({this._colorPicker.ColorName})";
      else
        base.ToolTipText = value;
    }
  }

  /// <summary>
  /// Initializes a new instance of the ToolStripColorPicker that holds
  /// OfficeColorPicker control inside a ToolStripItem to add to ToolStrip containers.
  /// </summary>
  public ToolStripColorPicker() => this.InitControl();

  /// <summary>
  /// Initializes a new instance of the ToolStripColorPicker that holds
  /// OfficeColorPicker control inside a ToolStripItem to add to ToolStrip containers.
  /// </summary>
  /// <param name="startingColor">The color to assign to the color picker control</param>
  public ToolStripColorPicker(Color startingColor)
  {
    this.Color = startingColor;
    this.InitControl();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      if (!this._colorPicker.IsDisposed)
        this._colorPicker.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Set starting properties for the control and register the needed events.
  /// </summary>
  private void InitControl()
  {
    this._colorPicker.SelectedColorChanged += new EventHandler(this.HandleSelectedColorChanged);
    this.AutoSize = false;
    this.Width = 30;
  }

  /// <summary>
  /// Set the painting properties by the _buttonDisplayStyle property.
  /// </summary>
  private void UpdateDisplayStyle()
  {
    switch (this._buttonDisplayStyle)
    {
      case ToolStripColorPickerDisplayType.NormalImage:
        this.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this._showColorUnderLine = false;
        this._showUnderLineImage = false;
        this._showUnderLineText = false;
        break;
      case ToolStripColorPickerDisplayType.NormalImageAndText:
        this.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        this._showColorUnderLine = false;
        this._showUnderLineImage = false;
        this._showUnderLineText = false;
        break;
      case ToolStripColorPickerDisplayType.UnderLineAndImage:
        this.DisplayStyle = ToolStripItemDisplayStyle.None;
        this._showColorUnderLine = true;
        this._showUnderLineImage = true;
        this._showUnderLineText = false;
        break;
      case ToolStripColorPickerDisplayType.UnderLineAndText:
        this.DisplayStyle = ToolStripItemDisplayStyle.None;
        this._showColorUnderLine = true;
        this._showUnderLineImage = false;
        this._showUnderLineText = true;
        break;
      case ToolStripColorPickerDisplayType.UnderLineTextAndImage:
        this.DisplayStyle = ToolStripItemDisplayStyle.None;
        this._showColorUnderLine = true;
        this._showUnderLineImage = true;
        this._showUnderLineText = true;
        break;
      case ToolStripColorPickerDisplayType.UnderLineOnly:
        this.DisplayStyle = ToolStripItemDisplayStyle.None;
        this._showColorUnderLine = true;
        this._showUnderLineImage = false;
        this._showUnderLineText = false;
        break;
      case ToolStripColorPickerDisplayType.None:
        this.DisplayStyle = ToolStripItemDisplayStyle.None;
        this._showColorUnderLine = false;
        this._showUnderLineImage = false;
        this._showUnderLineText = false;
        break;
      case ToolStripColorPickerDisplayType.Text:
        this.DisplayStyle = ToolStripItemDisplayStyle.Text;
        this._showColorUnderLine = false;
        this._showUnderLineImage = false;
        this._showUnderLineText = false;
        break;
    }
    this.Refresh();
  }

  /// <summary>When clicking on the button - opens the Color Picker</summary>
  /// <param name="e"></param>
  protected override void OnClick(EventArgs e) => this._colorPicker.Show(this.GetOpenPoint());

  /// <summary>Gets the button position by the parent ToolStrip</summary>
  /// <returns></returns>
  private Point GetOpenPoint()
  {
    if (this.Owner == null)
      return new Point(5, 5);
    int x = 0;
    foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.Parent.Items)
    {
      if (toolStripItem != this)
        x += toolStripItem.Width;
      else
        break;
    }
    return this.Owner.PointToScreen(new Point(x, -4));
  }

  /// <summary>Fires the SelectedColorChanged event.</summary>
  /// <param name="e"></param>
  public void OnSelectedColorChanged(EventArgs e)
  {
    if (this.SelectedColorChanged == null)
      return;
    this.SelectedColorChanged((object) this, e);
  }

  /// <summary>Repaint the button with the new color</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void HandleSelectedColorChanged(object sender, EventArgs e)
  {
    this.Refresh();
    this.OnSelectedColorChanged(EventArgs.Empty);
  }

  /// <summary>Repaint the parent tool strip and the button tool tip</summary>
  private void Refresh()
  {
    this.ToolTipText = this._originalToolTipText;
    if (this.Owner == null)
      return;
    this.Owner.Refresh();
  }

  /// <summary>Paints the underline rectangle.</summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  private void PaintUnderLine(Graphics g)
  {
    using (Brush brush = (Brush) new SolidBrush(this.Color))
    {
      this._colorRectangle = new Rectangle(2, this.Height - 6, this.Width - 16 /*0x10*/, 4);
      g.FillRectangle(brush, this._colorRectangle);
    }
  }

  /// <summary>Paints the under line image</summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <returns></returns>
  private Size PaintUnderLineImage(Graphics g)
  {
    if (!(this.Image is Bitmap image))
      return new Size(0, 0);
    image.MakeTransparent(this.ImageTransparentColor);
    g.DrawImage((Image) image, this._pictureRectangle);
    return image.Size;
  }

  /// <summary>Paints the underline text</summary>
  /// <param name="g"></param>
  /// <param name="imageSize"></param>
  /// <param name="bounds"></param>
  private void PaintUnderLineText(Graphics g, Size imageSize)
  {
    using (Brush brush = (Brush) new SolidBrush(this.ForeColor))
    {
      int x = imageSize.Width + 2;
      int y = 2;
      Rectangle layoutRectangle = new Rectangle(x, y, this.Width - x, this.Height - y);
      g.DrawString(this.Text, this.Font, brush, (RectangleF) layoutRectangle);
    }
  }

  /// <summary>
  /// Overrides, Paint the image in the specified scale and the color line if defined.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    if (this._showColorUnderLine)
    {
      base.OnPaint(e);
      Size imageSize = new Size(0, 0);
      this.PaintUnderLine(e.Graphics);
      if (this.Image != null && this._showUnderLineImage)
        imageSize = this.PaintUnderLineImage(e.Graphics);
      if (!this._showUnderLineText)
        return;
      this.PaintUnderLineText(e.Graphics, imageSize);
    }
    else
      base.OnPaint(e);
  }
}
