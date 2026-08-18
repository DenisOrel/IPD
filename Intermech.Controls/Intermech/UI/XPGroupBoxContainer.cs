
// Type: Intermech.UI.XPGroupBoxContainer
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.UI;

/// <summary>Summary description for XPGroupBoxContainer.</summary>
[Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof (IDesigner))]
public class XPGroupBoxContainer : UserControl
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private Color paneTopLeftColor = Color.White;
  private Color paneBottomRightColor = SystemColors.Control;
  private Color paneOutlineColor = Color.White;
  private LinearGradientMode fillMode = LinearGradientMode.Vertical;
  private int _groupBoxSpacing = 4;

  [Description("Determines the starting (light) color of the pane gradient fill.")]
  [Category("Appearance")]
  public Color PaneTopLeftColor
  {
    get => this.paneTopLeftColor;
    set
    {
      this.paneTopLeftColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the ending (dark) color of the pane gradient fill.")]
  [Category("Appearance")]
  public Color PaneBottomRightColor
  {
    get => this.paneBottomRightColor;
    set
    {
      this.paneBottomRightColor = value;
      this.Invalidate();
    }
  }

  [Description("Determines the color of the pane outline.")]
  [Category("Appearance")]
  public Color PaneOutlineColor
  {
    get => this.paneOutlineColor;
    set
    {
      this.paneOutlineColor = value;
      this.Invalidate();
    }
  }

  [DefaultValue(2)]
  [Description("Determines the border size.")]
  [Category("Appearance")]
  public int Spacing
  {
    get => this._groupBoxSpacing / 2;
    set
    {
      if (this._groupBoxSpacing / 2 == value)
        return;
      this._groupBoxSpacing = value * 2;
      this.RepositionControls();
    }
  }

  [DefaultValue(LinearGradientMode.Vertical)]
  [Description("Background fill mode")]
  [Category("Appearance")]
  public LinearGradientMode PanelFillMode
  {
    get => this.fillMode;
    set
    {
      if (this.fillMode == value)
        return;
      this.fillMode = value;
      this.Invalidate(false);
    }
  }

  public XPGroupBoxContainer()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.AutoScroll = true;
  }

  /// <summary>Clean up any resources being used.</summary>
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
    this.Name = nameof (XPGroupBoxContainer);
    this.Resize += new EventHandler(this.XPGroupBoxContainer_Resize);
    this.Load += new EventHandler(this.XPGroupBoxContainer_Load);
  }

  protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);

  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    Rectangle displayRectangle = this.DisplayRectangle;
    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(displayRectangle, this.paneTopLeftColor, this.paneBottomRightColor, this.fillMode);
    pevent.Graphics.FillRectangle((Brush) linearGradientBrush, displayRectangle);
  }

  protected override void OnControlAdded(ControlEventArgs e)
  {
    base.OnControlAdded(e);
    if (!(e.Control is XPGroupBox))
      throw new InvalidOperationException("Can only add XPGroupBoxControls");
    this.RepositionControls();
    ((XPGroupBox) e.Control).SizeChanging += new SizeChangingHandler(this.XPGroupBoxContainer_SizeChanging);
  }

  public void RepositionControls()
  {
    int y = this.AutoScrollPosition.Y;
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control is XPGroupBox xpGroupBox)
      {
        xpGroupBox.Left = this._groupBoxSpacing;
        xpGroupBox.Top = y + this._groupBoxSpacing;
        y += xpGroupBox.Height + this._groupBoxSpacing;
        xpGroupBox.Width = this.Width - 2 * this._groupBoxSpacing - 16 /*0x10*/;
      }
    }
  }

  private void XPGroupBoxContainer_Load(object sender, EventArgs e)
  {
    this.RepositionControls();
    this.Refresh();
  }

  private void XPGroupBoxContainer_SizeChanging(object sender, EventArgs e)
  {
    this.RepositionControls();
    this.Refresh();
  }

  private void XPGroupBoxContainer_Resize(object sender, EventArgs e)
  {
    this.RepositionControls();
    this.Refresh();
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.RepositionControls();
    this.Refresh();
  }
}
