
// Type: Intermech.Controls.LineDashStylesUserControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Controls;

[DefaultEvent("OnDashStyleSelected")]
public class LineDashStylesUserControl : 
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
  IFocusFromDirection
{
  private Color _lineColor = LineMenuItem.DefaultLineColor;
  private int _lineThickness = 300;
  [NotNull]
  private string _operationName = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private LineDashStyleMenuItem _menuItem1;
  private LineDashStyleMenuItem _menuItem2;
  private LineDashStyleMenuItem _menuItem3;
  private LineDashStyleMenuItem _menuItem4;
  private LineDashStyleMenuItem _menuItem5;

  public LineDashStylesUserControl() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public IEnumerable<LineDashStyleMenuItem> MenuItems
  {
    get
    {
      yield return this._menuItem1;
      yield return this._menuItem2;
      yield return this._menuItem3;
      yield return this._menuItem4;
      yield return this._menuItem5;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public LineDashStyleMenuItem SelectedMenuItem
  {
    get
    {
      return this.MenuItems.FirstOrDefault<LineDashStyleMenuItem>((Func<LineDashStyleMenuItem, bool>) (menuItem => menuItem.Checked));
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(DashStyle.Solid)]
  public DashStyle SelectedDashStyle
  {
    get
    {
      LineDashStyleMenuItem selectedMenuItem = this.SelectedMenuItem;
      return selectedMenuItem == null ? DashStyle.Solid : selectedMenuItem.DashStyle;
    }
    set
    {
      if (value == this.SelectedDashStyle)
        return;
      this.MenuItems.FirstOrDefault<LineDashStyleMenuItem>((Func<LineDashStyleMenuItem, bool>) (menuItem => menuItem.DashStyle == value)).InvokeIfNotNull<LineDashStyleMenuItem>((Action<LineDashStyleMenuItem>) (menuItem =>
      {
        if (menuItem.Checked)
          return;
        menuItem.Checked = true;
      }));
    }
  }

  public void FocusSelectedDashStyle()
  {
    LineDashStyleMenuItem selectedMenuItem = this.SelectedMenuItem;
    if (selectedMenuItem == null)
      return;
    selectedMenuItem.FocusIfCan();
  }

  private void _menuItem1_OnNavigateToLeft(
    [NotNull] IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToLeft();
  }

  private void _menuItem1_OnNavigateToRight(
    [NotNull] IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToRight();
  }

  private void _menuItem1_OnNavigateToUp(
    [NotNull] IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToUp();
  }

  private void _menuItem5_OnNavigateToDown(
    [NotNull] IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToDown();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> LeftMostControls
  {
    get
    {
      yield return (Control) this._menuItem1;
      yield return (Control) this._menuItem2;
      yield return (Control) this._menuItem3;
      yield return (Control) this._menuItem4;
      yield return (Control) this._menuItem5;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> TopMostControls
  {
    get
    {
      yield return (Control) this._menuItem1;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> RightMostControls
  {
    get
    {
      yield return (Control) this._menuItem1;
      yield return (Control) this._menuItem2;
      yield return (Control) this._menuItem3;
      yield return (Control) this._menuItem4;
      yield return (Control) this._menuItem5;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> BottomMostControls
  {
    get
    {
      yield return (Control) this._menuItem5;
    }
  }

  public event LineDashStylesUserControl.OnDashStyleSelectedDelegate OnDashStyleSelected;

  protected virtual void FireOnDashStyleSelected()
  {
    if (this.OnDashStyleSelected == null)
      return;
    this.OnDashStyleSelected(this, this.SelectedDashStyle);
  }

  private void _menuItem1_Click([NotNull] object sender, [NotNull] EventArgs e)
  {
    ((ContextMenuItemSurrogate) sender).Checked = true;
    this.FireOnDashStyleSelected();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Black")]
  public Color LineColor
  {
    [DebuggerStepThrough] get => this._lineColor;
    set
    {
      if (!(this._lineColor != value))
        return;
      this._lineColor = value;
      foreach (LineMenuItem menuItem in this.MenuItems)
        menuItem.LineColor = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(300)]
  public int LineThickness
  {
    [DebuggerStepThrough] get => this._lineThickness;
    set
    {
      if (this._lineThickness == value)
        return;
      this._lineThickness = value;
      foreach (LineMenuItem menuItem in this.MenuItems)
        menuItem.LineThickness = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  [NotNull]
  public string OperationName
  {
    [DebuggerStepThrough] get => this._operationName;
    set
    {
      if (!(this._operationName != value))
        return;
      this._operationName = value;
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._menuItem1 = new LineDashStyleMenuItem();
    this._menuItem2 = new LineDashStyleMenuItem();
    this._menuItem3 = new LineDashStyleMenuItem();
    this._menuItem4 = new LineDashStyleMenuItem();
    this._menuItem5 = new LineDashStyleMenuItem();
    this.SuspendLayout();
    this._menuItem1.BackColor = Color.White;
    this._menuItem1.BorderColor = Color.Empty;
    this._menuItem1.Checked = true;
    this._menuItem1.Dock = DockStyle.Top;
    this._menuItem1.DownControl = (Control) this._menuItem2;
    this._menuItem1.ImageIndex = 0;
    this._menuItem1.LineColor = Color.Black;
    this._menuItem1.Location = new Point(0, 0);
    this._menuItem1.Name = "_menuItem1";
    this._menuItem1.RadioGroupName = "DashStyle";
    this._menuItem1.Size = new Size(217, 22);
    this._menuItem1.TabIndex = 0;
    this._menuItem1.OnNavigateToUp += new OnNavigateDelegate(this._menuItem1_OnNavigateToUp);
    this._menuItem1.OnNavigateToLeft += new OnNavigateDelegate(this._menuItem1_OnNavigateToLeft);
    this._menuItem1.OnNavigateToRight += new OnNavigateDelegate(this._menuItem1_OnNavigateToRight);
    this._menuItem1.Click += new EventHandler(this._menuItem1_Click);
    this._menuItem2.BackColor = Color.White;
    this._menuItem2.BorderColor = Color.Empty;
    this._menuItem2.DashStyle = DashStyle.Dash;
    this._menuItem2.Dock = DockStyle.Top;
    this._menuItem2.DownControl = (Control) this._menuItem3;
    this._menuItem2.ImageIndex = 0;
    this._menuItem2.LineColor = Color.Black;
    this._menuItem2.Location = new Point(0, 22);
    this._menuItem2.Name = "_menuItem2";
    this._menuItem2.RadioGroupName = "DashStyle";
    this._menuItem2.Size = new Size(217, 22);
    this._menuItem2.TabIndex = 1;
    this._menuItem2.UpControl = (Control) this._menuItem1;
    this._menuItem2.OnNavigateToLeft += new OnNavigateDelegate(this._menuItem1_OnNavigateToLeft);
    this._menuItem2.OnNavigateToRight += new OnNavigateDelegate(this._menuItem1_OnNavigateToRight);
    this._menuItem2.Click += new EventHandler(this._menuItem1_Click);
    this._menuItem3.BackColor = Color.White;
    this._menuItem3.BorderColor = Color.Empty;
    this._menuItem3.DashStyle = DashStyle.Dot;
    this._menuItem3.Dock = DockStyle.Top;
    this._menuItem3.DownControl = (Control) this._menuItem4;
    this._menuItem3.ImageIndex = 0;
    this._menuItem3.LineColor = Color.Black;
    this._menuItem3.Location = new Point(0, 44);
    this._menuItem3.Name = "_menuItem3";
    this._menuItem3.RadioGroupName = "DashStyle";
    this._menuItem3.Size = new Size(217, 22);
    this._menuItem3.TabIndex = 2;
    this._menuItem3.UpControl = (Control) this._menuItem2;
    this._menuItem3.OnNavigateToLeft += new OnNavigateDelegate(this._menuItem1_OnNavigateToLeft);
    this._menuItem3.OnNavigateToRight += new OnNavigateDelegate(this._menuItem1_OnNavigateToRight);
    this._menuItem3.Click += new EventHandler(this._menuItem1_Click);
    this._menuItem4.BackColor = Color.White;
    this._menuItem4.BorderColor = Color.Empty;
    this._menuItem4.DashStyle = DashStyle.DashDot;
    this._menuItem4.Dock = DockStyle.Top;
    this._menuItem4.DownControl = (Control) this._menuItem5;
    this._menuItem4.ImageIndex = 0;
    this._menuItem4.LineColor = Color.Black;
    this._menuItem4.Location = new Point(0, 66);
    this._menuItem4.Name = "_menuItem4";
    this._menuItem4.RadioGroupName = "DashStyle";
    this._menuItem4.Size = new Size(217, 22);
    this._menuItem4.TabIndex = 3;
    this._menuItem4.UpControl = (Control) this._menuItem3;
    this._menuItem4.OnNavigateToLeft += new OnNavigateDelegate(this._menuItem1_OnNavigateToLeft);
    this._menuItem4.OnNavigateToRight += new OnNavigateDelegate(this._menuItem1_OnNavigateToRight);
    this._menuItem4.Click += new EventHandler(this._menuItem1_Click);
    this._menuItem5.BackColor = Color.White;
    this._menuItem5.BorderColor = Color.Empty;
    this._menuItem5.DashStyle = DashStyle.DashDotDot;
    this._menuItem5.Dock = DockStyle.Top;
    this._menuItem5.ImageIndex = 0;
    this._menuItem5.LineColor = Color.Black;
    this._menuItem5.Location = new Point(0, 88);
    this._menuItem5.Name = "_menuItem5";
    this._menuItem5.RadioGroupName = "DashStyle";
    this._menuItem5.Size = new Size(217, 22);
    this._menuItem5.TabIndex = 4;
    this._menuItem5.UpControl = (Control) this._menuItem4;
    this._menuItem5.OnNavigateToDown += new OnNavigateDelegate(this._menuItem5_OnNavigateToDown);
    this._menuItem5.OnNavigateToLeft += new OnNavigateDelegate(this._menuItem1_OnNavigateToLeft);
    this._menuItem5.OnNavigateToRight += new OnNavigateDelegate(this._menuItem1_OnNavigateToRight);
    this._menuItem5.Click += new EventHandler(this._menuItem1_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._menuItem5);
    this.Controls.Add((Control) this._menuItem4);
    this.Controls.Add((Control) this._menuItem3);
    this.Controls.Add((Control) this._menuItem2);
    this.Controls.Add((Control) this._menuItem1);
    this.Name = "LineDashStyleUserControl";
    this.Size = new Size(217, 109);
    this.ResumeLayout(false);
  }

  public delegate void OnDashStyleSelectedDelegate(
    [NotNull] LineDashStylesUserControl sender,
    DashStyle dashStyle);
}
