
// Type: Intermech.Controls.SetupLineUserControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Controls;

[DefaultEvent("OnLineChanged")]
public class SetupLineUserControl : 
  SimpleBaseUserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IDesignModeControlsContainer,
  IArrowKeysNavigationSupported,
  ILastFocusedControlTracker,
  IPopupMenu,
  IFocusFromDirection
{
  public static readonly Color DefaultLineColor = Color.Black;
  public const string DefaultLineColorName = "Black";
  public const int DefaultLineThickness = 100;
  public const DashStyle DefaultDashStyle = DashStyle.Solid;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ColorSelectionUserControl _colorSelectionUserControl;
  private SelectLineThicknessMenuItem _selectLineThicknessMenuItem;
  private SelectLineDashStyleMenuItem _selectLineDashStyleMenuItem;

  public SetupLineUserControl()
  {
    this.InitializeComponent();
    if (this._colorSelectionUserControl.Color != SetupLineUserControl.DefaultLineColor)
      this._colorSelectionUserControl.Color = SetupLineUserControl.DefaultLineColor;
    if (this._selectLineThicknessMenuItem.LineThickness != 100)
      this._selectLineThicknessMenuItem.LineThickness = 100;
    if (this._selectLineDashStyleMenuItem.DashStyle == DashStyle.Solid)
      return;
    this._selectLineDashStyleMenuItem.DashStyle = DashStyle.Solid;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Black")]
  public Color LineColor
  {
    get => this._colorSelectionUserControl.Color;
    set
    {
      if (value == Color.Empty)
        value = SetupLineUserControl.DefaultLineColor;
      if (!(this._colorSelectionUserControl.Color != value))
        return;
      this._colorSelectionUserControl.Color = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(100)]
  public int LineThickness
  {
    get => this._selectLineThicknessMenuItem.LineThickness;
    set
    {
      if (this._selectLineThicknessMenuItem.LineThickness == value)
        return;
      this._selectLineThicknessMenuItem.LineThickness = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(DashStyle.Solid)]
  public DashStyle DashStyle
  {
    get => this._selectLineDashStyleMenuItem.DashStyle;
    set
    {
      if (this._selectLineDashStyleMenuItem.DashStyle == value)
        return;
      this._selectLineDashStyleMenuItem.DashStyle = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Editor(typeof (StandartLineThicknessesEditor), typeof (UITypeEditor))]
  public Collection<int> StandartLineThicknesses
  {
    [DebuggerStepThrough] get => this._selectLineThicknessMenuItem.StandartLineThicknesses;
    set => this._selectLineThicknessMenuItem.StandartLineThicknesses = value;
  }

  public bool ShouldSerializeStandartLineThicknesses()
  {
    return this._selectLineThicknessMenuItem.ShouldSerializeStandartLineThicknesses();
  }

  public void ResetStandartLineThicknesses()
  {
    this._selectLineThicknessMenuItem.ResetStandartLineThicknesses();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  public string OperationName
  {
    [DebuggerStepThrough] get => this._colorSelectionUserControl.OperationName;
    set
    {
      if (!(this._colorSelectionUserControl.OperationName != value))
        return;
      this._colorSelectionUserControl.OperationName = value;
      this._selectLineDashStyleMenuItem.OperationName = value;
      this._selectLineThicknessMenuItem.OperationName = value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> MenuItems
  {
    get
    {
      yield return (Control) this._colorSelectionUserControl;
      yield return (Control) this._selectLineThicknessMenuItem;
      yield return (Control) this._selectLineDashStyleMenuItem;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> LeftMostControls
  {
    get
    {
      yield return (Control) this._colorSelectionUserControl;
      yield return (Control) this._selectLineThicknessMenuItem;
      yield return (Control) this._selectLineDashStyleMenuItem;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> TopMostControls
  {
    get
    {
      yield return (Control) this._colorSelectionUserControl;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> RightMostControls
  {
    get
    {
      yield return (Control) this._colorSelectionUserControl;
      yield return (Control) this._selectLineThicknessMenuItem;
      yield return (Control) this._selectLineDashStyleMenuItem;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> BottomMostControls
  {
    get
    {
      yield return (Control) this._selectLineDashStyleMenuItem;
    }
  }

  private void _colorSelectionUserControl_OnNavigateToUp(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToUp();
  }

  private void _colorSelectionUserControl_OnNavigateToRight(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToRight();
  }

  private void _selectLineDashStyleMenuItem_OnNavigateToDown(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToDown();
  }

  private void _colorSelectionUserControl_OnNavigateToLeft(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToLeft();
  }

  private void _colorSelectionUserControl_Resize(object sender, EventArgs e) => this.RecalcHeight();

  public virtual void RecalcHeight()
  {
    int height = this._colorSelectionUserControl.Height + this._selectLineThicknessMenuItem.Height + this._selectLineDashStyleMenuItem.Height;
    if (this.ClientSize.Height == height)
      return;
    this.ClientSize = new Size(this.ClientSize.Width, height);
    if (this.Parent == null || !(this.Parent is PopupDropDown))
      return;
    PopupDropDown parent = (PopupDropDown) this.Parent;
    if (parent.ClientSize.Height == this.Height)
      return;
    parent.ClientSize = new Size(parent.ClientSize.Width, this.Height);
  }

  public event SetupLineUserControl.SetupLineNotifyEvent OnLineColorSelected;

  protected virtual void FireOnLineColorSelected()
  {
    if (this.OnLineColorSelected == null)
      return;
    this.OnLineColorSelected(this);
  }

  public event SetupLineUserControl.SetupLineNotifyEvent OnLineThicknessesSelected;

  protected virtual void FireOnLineThicknessesSelected()
  {
    if (this.OnLineThicknessesSelected != null)
      this.OnLineThicknessesSelected(this);
    this.FireOnLineChanged();
  }

  public event SetupLineUserControl.SetupLineNotifyEvent OnLineDashStyleSelected;

  protected virtual void FireOnLineDashStyleSelected()
  {
    if (this.OnLineDashStyleSelected != null)
      this.OnLineDashStyleSelected(this);
    this.FireOnLineChanged();
  }

  public event SetupLineUserControl.SetupLineNotifyEvent OnLineChanged;

  protected virtual void FireOnLineChanged()
  {
    if (this.OnLineChanged == null)
      return;
    this.OnLineChanged(this);
  }

  private void _colorSelectionUserControl_ColorWasSelected(Color color)
  {
    this.FireOnLineColorSelected();
  }

  private void _selectLineThicknessMenuItem_OnLineThicknessSelected(
    SelectLineThicknessMenuItem sender,
    int selectedThickness)
  {
    this.FireOnLineThicknessesSelected();
  }

  private void _selectLineDashStyleMenuItem_OnLineDashStyleSelected(
    SelectLineDashStyleMenuItem sender,
    DashStyle selectedDashStyle)
  {
    this.FireOnLineDashStyleSelected();
  }

  protected override void OnEnter(EventArgs e)
  {
    base.OnEnter(e);
    this.Invalidate();
  }

  protected override void OnLeave(EventArgs e)
  {
    base.OnLeave(e);
    this.Invalidate();
  }

  public void AutoFocus() => this._colorSelectionUserControl.FocusCheckedButton();

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._colorSelectionUserControl = new ColorSelectionUserControl();
    this._selectLineThicknessMenuItem = new SelectLineThicknessMenuItem();
    this._selectLineDashStyleMenuItem = new SelectLineDashStyleMenuItem();
    this.SuspendLayout();
    this._colorSelectionUserControl.BackColor = Color.White;
    this._colorSelectionUserControl.Color = Color.Black;
    this._colorSelectionUserControl.Dock = DockStyle.Top;
    this._colorSelectionUserControl.DownControl = (Control) this._selectLineThicknessMenuItem;
    this._colorSelectionUserControl.Location = new Point(0, 0);
    this._colorSelectionUserControl.Name = "_colorSelectionUserControl";
    this._colorSelectionUserControl.Size = new Size(229, 199);
    this._colorSelectionUserControl.TabIndex = 0;
    this._colorSelectionUserControl.ColorWasSelected += new ColorSelectionUserControl.ColorWasSelectedDelegate(this._colorSelectionUserControl_ColorWasSelected);
    this._colorSelectionUserControl.OnNavigateToUp += new OnNavigateDelegate(this._colorSelectionUserControl_OnNavigateToUp);
    this._colorSelectionUserControl.OnNavigateToLeft += new OnNavigateDelegate(this._colorSelectionUserControl_OnNavigateToLeft);
    this._colorSelectionUserControl.OnNavigateToRight += new OnNavigateDelegate(this._colorSelectionUserControl_OnNavigateToRight);
    this._colorSelectionUserControl.Resize += new EventHandler(this._colorSelectionUserControl_Resize);
    this._selectLineThicknessMenuItem.BorderColor = Color.DarkGray;
    this._selectLineThicknessMenuItem.Borders = AnchorStyles.Top;
    this._selectLineThicknessMenuItem.Dock = DockStyle.Top;
    this._selectLineThicknessMenuItem.DownControl = (Control) this._selectLineDashStyleMenuItem;
    this._selectLineThicknessMenuItem.ImageIndex = 0;
    this._selectLineThicknessMenuItem.Location = new Point(0, 199);
    this._selectLineThicknessMenuItem.Name = "_selectLineThicknessMenuItem";
    this._selectLineThicknessMenuItem.RadioGroupName = "";
    this._selectLineThicknessMenuItem.Size = new Size(229, 30);
    this._selectLineThicknessMenuItem.TabIndex = 1;
    this._selectLineThicknessMenuItem.UpControl = (Control) this._colorSelectionUserControl;
    this._selectLineThicknessMenuItem.OnLineThicknessSelected += new SelectLineThicknessMenuItem.LineThicknessSelectedDelegate(this._selectLineThicknessMenuItem_OnLineThicknessSelected);
    this._selectLineThicknessMenuItem.OnNavigateToLeft += new OnNavigateDelegate(this._colorSelectionUserControl_OnNavigateToLeft);
    this._selectLineThicknessMenuItem.OnNavigateToRight += new OnNavigateDelegate(this._colorSelectionUserControl_OnNavigateToRight);
    this._selectLineDashStyleMenuItem.BorderColor = Color.Empty;
    this._selectLineDashStyleMenuItem.Borders = AnchorStyles.Top;
    this._selectLineDashStyleMenuItem.Dock = DockStyle.Top;
    this._selectLineDashStyleMenuItem.ImageIndex = 0;
    this._selectLineDashStyleMenuItem.Location = new Point(0, 229);
    this._selectLineDashStyleMenuItem.Name = "_selectLineDashStyleMenuItem";
    this._selectLineDashStyleMenuItem.RadioGroupName = "";
    this._selectLineDashStyleMenuItem.Size = new Size(229, 30);
    this._selectLineDashStyleMenuItem.TabIndex = 2;
    this._selectLineDashStyleMenuItem.UpControl = (Control) this._selectLineThicknessMenuItem;
    this._selectLineDashStyleMenuItem.OnLineDashStyleSelected += new SelectLineDashStyleMenuItem.LineDashStyleSelectedDelegate(this._selectLineDashStyleMenuItem_OnLineDashStyleSelected);
    this._selectLineDashStyleMenuItem.OnNavigateToDown += new OnNavigateDelegate(this._selectLineDashStyleMenuItem_OnNavigateToDown);
    this._selectLineDashStyleMenuItem.OnNavigateToLeft += new OnNavigateDelegate(this._colorSelectionUserControl_OnNavigateToLeft);
    this._selectLineDashStyleMenuItem.OnNavigateToRight += new OnNavigateDelegate(this._colorSelectionUserControl_OnNavigateToRight);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._selectLineDashStyleMenuItem);
    this.Controls.Add((Control) this._selectLineThicknessMenuItem);
    this.Controls.Add((Control) this._colorSelectionUserControl);
    this.Name = nameof (SetupLineUserControl);
    this.Size = new Size(229, 258);
    this.ResumeLayout(false);
  }

  public delegate void SetupLineNotifyEvent(SetupLineUserControl sender);
}
