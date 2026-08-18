
// Type: OfficePickers.ColorPicker.OfficeColorPicker
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using OfficePickers.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace OfficePickers.ColorPicker;

[DefaultEvent("SelectedColorChanged")]
[DefaultProperty("Color")]
[ToolboxItem(true)]
[ToolboxBitmap(typeof (OfficeColorPicker), "OfficeColorPicker")]
[Description("Provides color picker control that could be used in a model or non-model form.")]
public class OfficeColorPicker : UserControl
{
  /// <summary>The preferred height to span the control to</summary>
  public static readonly int PreferredHeight = 120;
  /// <summary>The preferred width to span the control to</summary>
  public static readonly int PreferredWidth = 146;
  private Color _color = Color.Black;
  /// <summary>
  /// Parent form when this control is inside a context menu form
  /// </summary>
  private ContextMenuForm _contextForm;
  /// <summary>
  /// Parent control, when on of the Show(Control parent ...) is called.
  /// </summary>
  private Control _parentControl;
  /// <summary>Known colors list that user may select from</summary>
  private SelectableColor[] colors = new SelectableColor[40];
  /// <summary>Buttons rectangle definitions.</summary>
  private Rectangle[] buttons = new Rectangle[41];
  /// <summary>
  /// Hot Track index to paint its button with HotTrack color
  /// </summary>
  private int _currentHotTrack = -1;
  /// <summary>
  /// Current selected index to paint its button with Selected color
  /// </summary>
  private int _currentSelected = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolTip colorToolTip;
  private ColorDialog colorDialog;

  /// <summary>Occurs when the value of the Color property changes.</summary>
  [Category("Behavior")]
  [Description("Occurs when the value of the Color property changes.")]
  public event EventHandler SelectedColorChanged;

  /// <summary>
  /// Gets or sets the selected color from the OfficeColorPicker
  /// </summary>
  [Category("Data")]
  [Description("The color selected in the dialog")]
  [DefaultValue(typeof (Color), "System.Drawing.Color.Black")]
  public Color Color
  {
    get => this._color;
    set
    {
      this._color = value;
      this.SetColor(value);
      this.OnSelectedColorChanged(EventArgs.Empty);
    }
  }

  /// <summary>
  /// Gets the selected color name, or 'Custom' if it is not one
  /// of the Selectable colors.
  /// </summary>
  [Browsable(false)]
  public string ColorName
  {
    get
    {
      string colorName = "Custom";
      if (this._currentSelected > -1 && this._currentSelected < CustomColors.SelectableColorsNames.Length)
        colorName = CustomColors.SelectableColorsNames[this._currentSelected];
      return colorName;
    }
  }

  /// <summary>
  /// Initialized a new instance of the OfficeColorPicker in order to provide
  /// color picker control that could be used in a model or non-model form.
  /// </summary>
  public OfficeColorPicker()
  {
    this.InitializeComponent();
    this.SetColorsObjects();
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.UserPaint, true);
  }

  /// <summary>
  /// Initialized a new instance of the OfficeColorPicker in order to provide
  /// color picker control that could be used in a model or non-model form.
  /// </summary>
  /// <param name="startingColor">Starting color to the OfficeColorPicker control</param>
  public OfficeColorPicker(Color startingColor)
    : this()
  {
    this.Color = startingColor;
  }

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// relative to the specified control.
  /// </summary>
  /// <param name="left">Parent control coordinates left location of the control</param>
  /// <param name="top">Parent control coordinates top location of the control</param>
  /// <param name="parent">Parent control to place the control at</param>
  public void Show(Control parent, int left, int top) => this.Show(parent, new Point(left, top));

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// </summary>
  /// <param name="left">Screen coordinates left location of the control</param>
  /// <param name="top">Screen coordinates top location of the control</param>
  public void Show(int left, int top) => this.Show(new Point(left, top));

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// </summary>
  /// <param name="startLocation">Screen coordinates location of the control</param>
  public void Show(Point startLocation)
  {
    this._contextForm = new ContextMenuForm();
    this._contextForm.SetContainingControl((Control) this);
    this._contextForm.Height = OfficeColorPicker.PreferredHeight;
    this._contextForm.Show((Control) this, startLocation, OfficeColorPicker.PreferredWidth);
  }

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// </summary>
  /// <param name="startLocation">Screen coordinates location of the control</param>
  /// <param name="parent">Parent control to place the control at</param>
  public void Show(Control parent, Point startLocation)
  {
    this._parentControl = parent;
    ContextMenuForm contextMenuForm = new ContextMenuForm();
    contextMenuForm.SetContainingControl((Control) this);
    contextMenuForm.Height = OfficeColorPicker.PreferredHeight;
    this._contextForm = contextMenuForm;
    contextMenuForm.Show(parent, startLocation, OfficeColorPicker.PreferredWidth);
  }

  /// <summary>
  /// Fires the OfficeColorPicker.SelectedColorChanged event
  /// </summary>
  /// <param name="e"></param>
  public void OnSelectedColorChanged(EventArgs e)
  {
    this.Refresh();
    if (this.SelectedColorChanged == null)
      return;
    this.SelectedColorChanged((object) this, e);
  }

  /// <summary>Creates the custom colors buttons</summary>
  private void SetColorsObjects()
  {
    for (int index = 0; index < this.colors.Length; ++index)
      this.colors[index] = new SelectableColor(CustomColors.SelectableColors[index]);
  }

  /// <summary>Set color to the specified one</summary>
  /// <param name="color"></param>
  private void SetColor(Color color)
  {
    this._currentHotTrack = -1;
    this._currentSelected = -1;
    for (int index = 0; index < CustomColors.SelectableColors.Length; ++index)
    {
      if (CustomColors.ColorEquals(CustomColors.SelectableColors[index], color))
      {
        this._currentSelected = index;
        this._currentHotTrack = -1;
      }
    }
    this.Refresh();
  }

  /// <summary>
  /// Overrides, when mouse move - allow the hot-track look-and-feel
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    for (int index = 0; index < this.buttons.Length; ++index)
    {
      if (this.buttons[index].Contains(e.Location))
      {
        this._currentHotTrack = index;
        this.colorToolTip.SetToolTip((Control) this, CustomColors.SelectableColorsNames[index]);
      }
    }
    this.Refresh();
  }

  /// <summary>Overrides, when click on, handles color selection.</summary>
  /// <param name="e"></param>
  protected override void OnMouseClick(MouseEventArgs e)
  {
    base.OnMouseClick(e);
    for (int index = 0; index < this.buttons.Length; ++index)
    {
      if (this.buttons[index].Contains(e.Location))
      {
        this._currentSelected = index;
        if (this._currentSelected == 40)
        {
          this.Color = this.OpenMoreColorsDialog();
        }
        else
        {
          this.Color = CustomColors.SelectableColors[index];
          this.colorToolTip.SetToolTip((Control) this, CustomColors.SelectableColorsNames[index]);
        }
        if (this._contextForm != null)
          this._contextForm.Hide();
        this._contextForm = (ContextMenuForm) null;
      }
    }
    this.Refresh();
  }

  /// <summary>
  /// Open the 'More Color' dialog, that is, a normal ColorDialog control.
  /// </summary>
  /// <returns></returns>
  private Color OpenMoreColorsDialog()
  {
    this.colorDialog.Color = this.Color;
    if (this.FindForm() is ContextMenuForm form)
    {
      form.Locked = true;
      int num = (int) this.colorDialog.ShowDialog((IWin32Window) form);
      if (this._parentControl != null)
        this._parentControl.FindForm().BringToFront();
      form.Locked = false;
    }
    else
    {
      int num1 = (int) this.colorDialog.ShowDialog((IWin32Window) this);
    }
    return this.colorDialog.Color;
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this._currentHotTrack = -1;
    this.Refresh();
  }

  /// <summary>Override, paint background to white</summary>
  /// <param name="e"></param>
  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    using (Brush brush = (Brush) new SolidBrush(CustomColors.ColorPickerBackgroundDocked))
      pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
  }

  /// <summary>Overrides, paint all buttons</summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    int x = 0;
    int y1 = 0;
    int num1 = 18;
    int num2 = 18;
    for (int index = 0; index < this.colors.Length; ++index)
    {
      bool hotTrack = index == this._currentHotTrack;
      bool selected = index == this._currentSelected;
      this.buttons[index] = this.PaintColor(e.Graphics, this.colors[index].Color, hotTrack, selected, x, y1);
      x += num1;
      if (x > 7 * num1)
      {
        x = 0;
        y1 += num2;
      }
    }
    int y2 = y1 + 4;
    this.PaintMoreColorsButton(e.Graphics, x, y2);
  }

  /// <summary>Paints the more colors button</summary>
  /// <param name="graphics"></param>
  /// <param name="x"></param>
  /// <param name="y"></param>
  protected void PaintMoreColorsButton(Graphics graphics, int x, int y)
  {
    Rectangle rectangle = new Rectangle(x, y, 143, 22);
    StringFormat format = new StringFormat();
    format.Alignment = StringAlignment.Center;
    format.LineAlignment = StringAlignment.Center;
    Font font = new Font("Arial", 8f);
    bool flag1 = this._currentSelected == 40;
    bool flag2 = this._currentHotTrack == 40;
    using (Brush brush1 = (Brush) new SolidBrush(CustomColors.ButtonHoverLight))
    {
      using (Brush brush2 = (Brush) new SolidBrush(CustomColors.ButtonHoverDark))
      {
        using (Pen pen = new Pen(CustomColors.SelectedBorder))
        {
          using (new Pen(CustomColors.ButtonBorder))
          {
            if (flag2)
            {
              graphics.FillRectangle(brush1, rectangle);
              graphics.DrawRectangle(pen, rectangle);
            }
            else if (flag1)
            {
              graphics.FillRectangle(brush2, rectangle);
              graphics.DrawRectangle(pen, rectangle);
            }
          }
        }
      }
    }
    graphics.DrawString("Другие цвета...", font, Brushes.Black, (RectangleF) rectangle, format);
    format.Dispose();
    font.Dispose();
    this.buttons[40] = rectangle;
  }

  /// <summary>Paints one color button</summary>
  /// <param name="graphics"></param>
  /// <param name="color"></param>
  /// <param name="hotTrack"></param>
  /// <param name="selected"></param>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <returns></returns>
  private Rectangle PaintColor(
    Graphics graphics,
    Color color,
    bool hotTrack,
    bool selected,
    int x,
    int y)
  {
    Rectangle rect1 = new Rectangle(x + 3, y + 3, 11, 11);
    Rectangle rect2 = new Rectangle(x, y, 17, 17);
    using (Brush brush1 = (Brush) new SolidBrush(color))
    {
      using (Brush brush2 = (Brush) new SolidBrush(CustomColors.ButtonHoverLight))
      {
        using (Brush brush3 = (Brush) new SolidBrush(CustomColors.ButtonHoverDark))
        {
          using (Brush brush4 = (Brush) new SolidBrush(CustomColors.SelectedAndHover))
          {
            using (Pen pen1 = new Pen(CustomColors.SelectedBorder))
            {
              using (Pen pen2 = new Pen(CustomColors.ButtonBorder))
              {
                if (selected & hotTrack)
                {
                  graphics.FillRectangle(brush4, rect2);
                  graphics.DrawRectangle(pen1, rect2);
                }
                else if (hotTrack)
                {
                  graphics.FillRectangle(brush2, rect2);
                  graphics.DrawRectangle(pen1, rect2);
                }
                else if (selected)
                {
                  graphics.FillRectangle(brush3, rect2);
                  graphics.DrawRectangle(pen1, rect2);
                }
                graphics.FillRectangle(brush1, rect1);
                graphics.DrawRectangle(pen2, rect1);
              }
            }
          }
        }
      }
    }
    return rect2;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OfficeColorPicker));
    this.colorToolTip = new ToolTip(this.components);
    this.colorDialog = new ColorDialog();
    this.SuspendLayout();
    this.colorToolTip.AutoPopDelay = 5000;
    this.colorToolTip.InitialDelay = 500;
    this.colorToolTip.ReshowDelay = 1000;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BorderStyle = BorderStyle.FixedSingle;
    this.Name = nameof (OfficeColorPicker);
    this.colorToolTip.SetToolTip((Control) this, componentResourceManager.GetString("$this.ToolTip"));
    this.ResumeLayout(false);
  }
}
