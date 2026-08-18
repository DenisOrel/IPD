
// Type: Intermech.Controls.SelectLineThicknessUserControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Extensions;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Controls;

[DefaultEvent("OnDashStyleSelected")]
public class SelectLineThicknessUserControl : 
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
  ISupportInitialize,
  IStandartLineThicknessesOwner,
  IFocusFromDirection
{
  private const int DefaultSelectedLineThickness = 100;
  private int _selectedLineThickness = 100;
  public static readonly IList<int> DefaultStandartLineThicknesses = (IList<int>) new List<int>(7)
  {
    50,
    100,
    150,
    200,
    300,
    400,
    500
  };
  private Collection<int> _standartLineThicknesses;
  private bool _isInInitMode;
  private bool _itemsWasBuild;
  private Dictionary<int, LineThicknessMenuItem> _standartMenuItems = new Dictionary<int, LineThicknessMenuItem>();
  private LineThicknessMenuItem _customThicknessMenuItem;
  private string _operationName = string.Empty;
  internal static readonly Color DefaultLineColor = Color.Black;
  internal const string DefaultLineColorName = "Black";
  private Color _lineColor = SelectLineThicknessUserControl.DefaultLineColor;
  public const DashStyle DefaultDashStyle = DashStyle.Solid;
  private DashStyle _dashStyle;
  private IButtonControl _ownerFormAcceptButton;
  public const Decimal DefaultCustomThicknessIncrement = 0.2M;
  public const string DefaultCustomThicknessIncrementText = "0.2";
  public const Decimal DefaultCustomThicknessMinimum = 0.2M;
  public const string DefaultCustomThicknessMinimumText = "0.2";
  public const Decimal DefaultCustomThicknessMaximum = 15M;
  public const string DefaultCustomThicknessMaximumText = "15";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Bevel _bevel;
  private Panel _panelCustomLine;
  private FlatNumericUpDown _editCustomLine;
  private Label _labelCustomWidth;
  private ContextMenuItemSurrogate _customLineApply;

  public SelectLineThicknessUserControl()
  {
    this.InitializeComponent();
    if (this._editCustomLine.Increment != 0.2M)
      this._editCustomLine.Increment = 0.2M;
    if (this._editCustomLine.Minimum != 0.2M)
      this._editCustomLine.Minimum = 0.2M;
    if (this._editCustomLine.Maximum != 15M)
      this._editCustomLine.Maximum = 15M;
    this._standartLineThicknesses = (Collection<int>) new SelectLineThicknessUserControl.StandartLineThicknessesCollection((IStandartLineThicknessesOwner) this, (IList<int>) SelectLineThicknessUserControl.DefaultStandartLineThicknesses.ToList<int>());
    this.RecalcHeight();
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
  [DefaultValue(100)]
  public int SelectedLineThickness
  {
    [DebuggerStepThrough] get => this._selectedLineThickness;
    set
    {
      if (this._selectedLineThickness == value)
        return;
      this._selectedLineThickness = value;
      this.UpdateSelected();
    }
  }

  protected IEnumerable<LineThicknessMenuItem> LineThicknessItems
  {
    get => this.Controls.OfType<LineThicknessMenuItem>().Reverse<LineThicknessMenuItem>();
  }

  protected LineThicknessMenuItem SelectedLineThicknessMenuItem
  {
    get
    {
      return this.LineThicknessItems.FirstOrDefault<LineThicknessMenuItem>((Func<LineThicknessMenuItem, bool>) (menuItem => menuItem.Checked && menuItem.LineThickness == this._selectedLineThickness));
    }
  }

  public void FocusSelectedLineThicknessMenuItem()
  {
    this.SelectedLineThicknessMenuItem.FocusIfCan();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Editor(typeof (StandartLineThicknessesEditor), typeof (UITypeEditor))]
  public Collection<int> StandartLineThicknesses
  {
    [DebuggerStepThrough] get => this._standartLineThicknesses;
    set
    {
      if (value == null)
        throw new Exception("null not supported");
      if (value.Count < 2)
        throw new Exception("must contains at least 2 values");
      if (value.Any<int>((Func<int, bool>) (Thickness => Thickness < 5 || Thickness > 10000)))
        throw new Exception("values must be between 5 and 10000");
      if (value.ContainsDuplicates<int>())
        throw new Exception("duplicates not allowed");
      if (this._standartLineThicknesses != value && !this._standartLineThicknesses.SequenceEqual<int>((IEnumerable<int>) value))
        this.SetStandartLineThicknesses((IEnumerable<int>) value);
      if (this._isInInitMode)
        return;
      this.RebuildItems();
    }
  }

  public bool ShouldSerializeStandartLineThicknesses()
  {
    return !this._standartLineThicknesses.SequenceEqual<int>((IEnumerable<int>) SelectLineThicknessUserControl.DefaultStandartLineThicknesses);
  }

  public void ResetStandartLineThicknesses()
  {
    this._standartLineThicknesses.Clear();
    this._standartLineThicknesses.AddRange<int>((IEnumerable<int>) SelectLineThicknessUserControl.DefaultStandartLineThicknesses);
    this.RebuildItems();
  }

  private void SetStandartLineThicknesses(IEnumerable<int> value, bool checkValueIsOrdered = true)
  {
    if (checkValueIsOrdered && !value.IsOrdered<int>())
      value = (IEnumerable<int>) value.OrderBy<int, int>((Func<int, int>) (Thickness => Thickness));
    this._standartLineThicknesses.Clear();
    this._standartLineThicknesses.AddRange<int>(value);
  }

  public virtual void AfterStandartLineThicknessesChanged()
  {
    if (!this._standartLineThicknesses.IsOrdered<int>())
      this.SetStandartLineThicknesses((IEnumerable<int>) this._standartLineThicknesses.OrderBy<int, int>((Func<int, int>) (Thickness => Thickness)).ToArray<int>(), false);
    if (this._isInInitMode)
      return;
    this.RebuildItems();
  }

  public void BeginInit() => this._isInInitMode = true;

  public void EndInit()
  {
    this._isInInitMode = false;
    this.RebuildItems();
  }

  protected override void FireFirstPaint()
  {
    if (!this._itemsWasBuild)
      this.RebuildItems();
    base.FireFirstPaint();
  }

  public void RebuildItems()
  {
    if (this._isInInitMode)
      return;
    this._itemsWasBuild = true;
    int num1 = -9999;
    List<int> list;
    if (this._standartLineThicknesses.Contains(this._selectedLineThickness))
    {
      list = this._standartLineThicknesses.Reverse<int>().ToList<int>(this._standartLineThicknesses.Count);
    }
    else
    {
      num1 = this._selectedLineThickness;
      list = this._standartLineThicknesses.Append<int>(this._selectedLineThickness).OrderByDescending<int, int>((Func<int, int>) (Thickness => Thickness)).ToList<int>(this._standartLineThicknesses.Count + 1);
    }
    int oldFocusedThickness = this.ActiveControl is LineThicknessMenuItem ? ((LineMenuItem) this.ActiveControl).LineThickness : -1;
    this.SuspendLayout();
    try
    {
      this._standartMenuItems.Clear();
      this._customThicknessMenuItem = (LineThicknessMenuItem) null;
      for (int index = this.Controls.Count - 1; index >= 0; --index)
      {
        Control control = this.Controls[index];
        if (control is LineThicknessMenuItem)
        {
          control.Click -= new EventHandler(this.menuItem_Click);
          ((ContextMenuItemSurrogate) control).OnNavigateToUp -= new OnNavigateDelegate(this.TopLineThicknessItem_NavigateToUp);
          ((ContextMenuItemSurrogate) control).OnNavigateToLeft -= new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToLeft);
          ((ContextMenuItemSurrogate) control).OnNavigateToRight -= new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToRight);
          this.Controls.RemoveAt(index);
          control.Dispose();
        }
      }
      int num2 = list.Count - 1;
      foreach (int key in list)
      {
        LineThicknessMenuItem thicknessMenuItem1 = new LineThicknessMenuItem();
        thicknessMenuItem1.BackColor = this._panelCustomLine.BackColor;
        thicknessMenuItem1.Dock = DockStyle.Top;
        thicknessMenuItem1.LineColor = Color.Black;
        thicknessMenuItem1.LineThickness = key;
        thicknessMenuItem1.DashStyle = this._dashStyle;
        thicknessMenuItem1.Name = "_item" + num2.ToString();
        thicknessMenuItem1.RadioGroupName = "LineThickness";
        thicknessMenuItem1.Size = new Size(100, 22);
        LineThicknessMenuItem thicknessMenuItem2 = thicknessMenuItem1;
        int num3 = num2;
        int num4 = num3 - 1;
        thicknessMenuItem2.TabIndex = num3;
        thicknessMenuItem1.LineColor = this._lineColor;
        thicknessMenuItem1.Click += new EventHandler(this.menuItem_Click);
        this.Controls.Add((Control) thicknessMenuItem1);
        thicknessMenuItem1.Checked = key == this._selectedLineThickness;
        num2 = num4 + 1;
        if (key != num1)
          this._standartMenuItems[key] = thicknessMenuItem1;
        else
          this._customThicknessMenuItem = thicknessMenuItem1;
      }
      this._panelCustomLine.TabIndex = list.Count;
      this.UpdateNavigation();
    }
    finally
    {
      this.RecalcHeight();
      this.ResumeLayout(true);
    }
    if (oldFocusedThickness == 0)
      return;
    if (!list.Contains(oldFocusedThickness))
      oldFocusedThickness = this.LineThicknessItems.FindMin<LineThicknessMenuItem, int, int>((Func<LineThicknessMenuItem, int>) (menuItem => Math.Abs(menuItem.LineThickness - oldFocusedThickness)), (Func<LineThicknessMenuItem, int>) (menuItem => menuItem.LineThickness));
    if (oldFocusedThickness == 0 || !list.Contains(oldFocusedThickness))
      return;
    this.LineThicknessItems.FirstOrDefault<LineThicknessMenuItem>((Func<LineThicknessMenuItem, bool>) (menuItem => menuItem.LineThickness == oldFocusedThickness)).FocusIfCan();
  }

  private void menuItem_Click(object sender, EventArgs e)
  {
    LineThicknessMenuItem thicknessMenuItem = (LineThicknessMenuItem) sender;
    if (thicknessMenuItem.Checked)
      return;
    this._selectedLineThickness = thicknessMenuItem.LineThickness;
    thicknessMenuItem.Checked = true;
    this.FireLineThicknessSelected();
  }

  private void RecalcHeight()
  {
    int height1 = this.LineThicknessItems.Aggregate<LineThicknessMenuItem, int>(0, (Func<int, LineThicknessMenuItem, int>) ((height, menuItem) => height += menuItem.Height)) + this._bevel.Height + this._panelCustomLine.Height;
    if (this.ClientSize.Height == height1)
      return;
    this.ClientSize = new Size(this.ClientSize.Width, height1);
    if (this.Parent == null || !(this.Parent is PopupDropDown))
      return;
    PopupDropDown parent = (PopupDropDown) this.Parent;
    Size clientSize = parent.ClientSize;
    if (clientSize.Height == this.Height)
      return;
    PopupDropDown popupDropDown = parent;
    clientSize = parent.ClientSize;
    Size size = new Size(clientSize.Width, this.Height);
    popupDropDown.ClientSize = size;
  }

  private void UpdateSelected()
  {
    if (!this._itemsWasBuild)
      return;
    bool performLayout = false;
    this.SuspendLayout();
    try
    {
      LineThicknessMenuItem control1 = this._standartMenuItems.Values.FirstOrDefault<LineThicknessMenuItem>((Func<LineThicknessMenuItem, bool>) (menuItem => menuItem.LineThickness == this._selectedLineThickness));
      if (control1 != null)
      {
        if (!control1.Checked)
          control1.Checked = true;
        if (this._customThicknessMenuItem == null)
          return;
        int num = this._customThicknessMenuItem.Focused ? 1 : 0;
        this.Controls.Remove((Control) this._customThicknessMenuItem);
        this._customThicknessMenuItem.Click -= new EventHandler(this.menuItem_Click);
        this._customThicknessMenuItem.OnNavigateToLeft -= new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToLeft);
        this._customThicknessMenuItem.OnNavigateToRight -= new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToRight);
        this._customThicknessMenuItem.OnNavigateToUp -= new OnNavigateDelegate(this.TopLineThicknessItem_NavigateToUp);
        this._customThicknessMenuItem.Dispose();
        this._customThicknessMenuItem = (LineThicknessMenuItem) null;
        performLayout = true;
        if (num == 0)
          return;
        control1.FocusIfCan();
      }
      else
      {
        bool flag = false;
        if (this._customThicknessMenuItem == null)
        {
          this._customThicknessMenuItem = new LineThicknessMenuItem();
          this._customThicknessMenuItem.BackColor = this._panelCustomLine.BackColor;
          this._customThicknessMenuItem.Dock = DockStyle.Top;
          this._customThicknessMenuItem.LineColor = Color.Black;
          this._customThicknessMenuItem.LineThickness = this._selectedLineThickness;
          this._customThicknessMenuItem.DashStyle = this._dashStyle;
          this._customThicknessMenuItem.RadioGroupName = "LineThickness";
          this._customThicknessMenuItem.Size = new Size(100, 22);
          this._customThicknessMenuItem.LineColor = this._lineColor;
          this._customThicknessMenuItem.Click += new EventHandler(this.menuItem_Click);
          flag = true;
          this.Controls.Add((Control) this._customThicknessMenuItem);
          this._customThicknessMenuItem.Checked = true;
          performLayout = true;
        }
        else
          this._customThicknessMenuItem.LineThickness = this._selectedLineThickness;
        if (!this._customThicknessMenuItem.Checked)
          this._customThicknessMenuItem.Checked = true;
        int num1 = this.Controls.IndexOf((Control) this._customThicknessMenuItem);
        LineThicknessMenuItem control2 = num1 < this.Controls.Count - 1 ? this.Controls[num1 + 1] as LineThicknessMenuItem : (LineThicknessMenuItem) null;
        LineThicknessMenuItem control3 = num1 > 0 ? this.Controls[num1 - 1] as LineThicknessMenuItem : (LineThicknessMenuItem) null;
        if (control2 != null && control2.LineThickness > this._customThicknessMenuItem.LineThickness || control3 != null && control3.LineThickness < this._customThicknessMenuItem.LineThickness)
        {
          IList<LineThicknessMenuItem> list = (IList<LineThicknessMenuItem>) this.LineThicknessItems.Except<LineThicknessMenuItem>(this._customThicknessMenuItem).ToList<LineThicknessMenuItem>();
          int newIndex = list.Last<LineThicknessMenuItem>().LineThickness > this._customThicknessMenuItem.LineThickness ? this.Controls.Count - list.IndexOfFirst<LineThicknessMenuItem>((Predicate<LineThicknessMenuItem>) (menuItem => menuItem.LineThickness > this._selectedLineThickness)) - 1 : 2;
          if (newIndex != num1)
          {
            this.Controls.SetChildIndex((Control) this._customThicknessMenuItem, newIndex);
            performLayout = true;
            flag = true;
          }
        }
        if (!flag)
          return;
        int num2 = 0;
        foreach (LineThicknessMenuItem lineThicknessItem in this.LineThicknessItems)
        {
          string str = "_item" + num2.ToString();
          if (lineThicknessItem.Name != str)
            lineThicknessItem.Name = str;
          if (lineThicknessItem.TabIndex != num2)
            lineThicknessItem.TabIndex = num2;
          ++num2;
        }
        this._panelCustomLine.TabIndex = num2;
        this.UpdateNavigation();
      }
    }
    finally
    {
      if (performLayout)
        this.RecalcHeight();
      this.ResumeLayout(performLayout);
    }
  }

  private void UpdateNavigation()
  {
    if (!this.LineThicknessItems.Any<LineThicknessMenuItem>())
      return;
    this.LineThicknessItems.First<LineThicknessMenuItem>().OnNavigateToUp += new OnNavigateDelegate(this.TopLineThicknessItem_NavigateToUp);
    LineThicknessMenuItem thicknessMenuItem1 = this.LineThicknessItems.First<LineThicknessMenuItem>();
    thicknessMenuItem1.OnNavigateToLeft += new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToLeft);
    thicknessMenuItem1.OnNavigateToRight += new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToRight);
    foreach (LineThicknessMenuItem thicknessMenuItem2 in this.LineThicknessItems.Skip<LineThicknessMenuItem>(1))
    {
      thicknessMenuItem2.UpControl = (Control) thicknessMenuItem1;
      thicknessMenuItem2.OnNavigateToLeft += new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToLeft);
      thicknessMenuItem2.OnNavigateToRight += new OnNavigateDelegate(this.lineThicknessMenuItem_OnNavigateToRight);
      thicknessMenuItem1 = thicknessMenuItem2;
    }
    this.LineThicknessItems.Last<LineThicknessMenuItem>().DownControl = (Control) this._editCustomLine;
    this._customLineApply.UpControl = (Control) this.LineThicknessItems.Last<LineThicknessMenuItem>();
  }

  private void lineThicknessMenuItem_OnNavigateToLeft(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToLeft();
    blockDefaultNavigation = true;
  }

  private void lineThicknessMenuItem_OnNavigateToRight(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToRight();
    blockDefaultNavigation = true;
  }

  private void TopLineThicknessItem_NavigateToUp(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToUp();
    blockDefaultNavigation = true;
  }

  public event SelectLineThicknessUserControl.LineThicknessSelectedDelegate LineThicknessSelected;

  protected virtual void FireLineThicknessSelected()
  {
    if (this.LineThicknessSelected == null)
      return;
    this.LineThicknessSelected(this, this.SelectedLineThickness);
  }

  private void ApplyCutomLineThickness()
  {
    int num = (int) (this._editCustomLine.Value * 100M);
    if (num == this.SelectedLineThickness)
      return;
    this.SelectedLineThickness = num;
    this.FireLineThicknessSelected();
  }

  private void _customLineApply_Click(object sender, EventArgs e) => this.ApplyCutomLineThickness();

  private void _editCustomLine_KeyDown(object sender, KeyEventArgs e)
  {
    switch (e.KeyCode)
    {
      case Keys.Return:
        this.ApplyCutomLineThickness();
        break;
      case Keys.Up:
        Control control = (Control) this.LineThicknessItems.LastOrDefault<LineThicknessMenuItem>();
        if (control != null)
        {
          control.FocusIfCan();
          break;
        }
        this.NavigateToLeft();
        break;
      case Keys.Down:
        this.NavigateToDown();
        break;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
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

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Black")]
  public Color LineColor
  {
    [DebuggerStepThrough] get => this._lineColor;
    set
    {
      if (!(value != this._lineColor))
        return;
      this._lineColor = value;
      this.LineThicknessItems.InvokeForAll<LineThicknessMenuItem>((Action<LineThicknessMenuItem>) (menuItem => menuItem.LineColor = value));
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(DashStyle.Solid)]
  public DashStyle DashStyle
  {
    [DebuggerStepThrough] get => this._dashStyle;
    set
    {
      if (value == DashStyle.Custom)
        value = DashStyle.Solid;
      if (value == this._dashStyle)
        return;
      this._dashStyle = value;
      this.LineThicknessItems.InvokeForAll<LineThicknessMenuItem>((Action<LineThicknessMenuItem>) (menuItem => menuItem.DashStyle = value));
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> LeftMostControls
  {
    get
    {
      return ((IEnumerable<Control>) this.LineThicknessItems).Append<Control>((Control) this._editCustomLine);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> TopMostControls
  {
    get
    {
      yield return (Control) this.LineThicknessItems.FirstOrDefault<LineThicknessMenuItem>() ?? (Control) this._editCustomLine;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> RightMostControls
  {
    get
    {
      return ((IEnumerable<Control>) this.LineThicknessItems).Append<Control>((Control) this._customLineApply);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<Control> BottomMostControls
  {
    get
    {
      yield return (Control) this._editCustomLine;
    }
  }

  private void _editCustomLine_Enter(object sender, EventArgs e)
  {
    this._ownerFormAcceptButton = this.ParentForm?.AcceptButton;
    if (this._ownerFormAcceptButton == null)
      return;
    this.ParentForm.AcceptButton = (IButtonControl) null;
  }

  private void _editCustomLine_Leave(object sender, EventArgs e)
  {
    if (this._ownerFormAcceptButton == null)
      return;
    this.ParentForm.AcceptButton = this._ownerFormAcceptButton;
    this._ownerFormAcceptButton = (IButtonControl) null;
  }

  private void _customLineApply_OnNavigateToDown(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.NavigateToDown();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Decimal), "0.2")]
  public Decimal CustomThicknessIncrement
  {
    get => this._editCustomLine.Increment;
    set
    {
      if (!(this._editCustomLine.Increment != value))
        return;
      this._editCustomLine.Increment = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Decimal), "0.2")]
  public Decimal CustomThicknessMinimum
  {
    get => this._editCustomLine.Minimum;
    set
    {
      if (!(this._editCustomLine.Minimum != value))
        return;
      this._editCustomLine.Minimum = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Decimal), "15")]
  public Decimal CustomThicknessMaximum
  {
    get => this._editCustomLine.Maximum;
    set
    {
      if (!(this._editCustomLine.Maximum != value))
        return;
      this._editCustomLine.Maximum = value;
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._panelCustomLine = new Panel();
    this._customLineApply = new ContextMenuItemSurrogate();
    this._editCustomLine = new FlatNumericUpDown();
    this._labelCustomWidth = new Label();
    this._bevel = new Bevel();
    this._panelCustomLine.SuspendLayout();
    this._editCustomLine.BeginInit();
    this.SuspendLayout();
    this._panelCustomLine.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this._panelCustomLine.BackColor = Color.White;
    this._panelCustomLine.Controls.Add((Control) this._customLineApply);
    this._panelCustomLine.Controls.Add((Control) this._editCustomLine);
    this._panelCustomLine.Controls.Add((Control) this._labelCustomWidth);
    this._panelCustomLine.Dock = DockStyle.Top;
    this._panelCustomLine.Location = new Point(0, 2);
    this._panelCustomLine.Name = "_panelCustomLine";
    this._panelCustomLine.Size = new Size(220, 29);
    this._panelCustomLine.TabIndex = 0;
    this._customLineApply.Anchor = AnchorStyles.Right;
    this._customLineApply.BackColor = Color.White;
    this._customLineApply.BorderColor = Color.DarkGray;
    this._customLineApply.Borders = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
    this._customLineApply.ImageIndex = 0;
    this._customLineApply.Location = new Point(135, 3);
    this._customLineApply.Name = "_customLineApply";
    this._customLineApply.RadioGroupName = "";
    this._customLineApply.Size = new Size(74, 23);
    this._customLineApply.TabIndex = 1;
    this._customLineApply.Text = "Выбрать";
    this._customLineApply.OnNavigateToDown += new OnNavigateDelegate(this._customLineApply_OnNavigateToDown);
    this._customLineApply.Click += new EventHandler(this._customLineApply_Click);
    this._editCustomLine.Anchor = AnchorStyles.Left | AnchorStyles.Right;
    this._editCustomLine.DecimalPlaces = 2;
    this._editCustomLine.Font = new Font("Microsoft Sans Serif", 10f);
    this._editCustomLine.Increment = new Decimal(new int[4]
    {
      2,
      0,
      0,
      65536 /*0x010000*/
    });
    this._editCustomLine.InterceptArrowKeys = false;
    this._editCustomLine.Location = new Point(55, 3);
    this._editCustomLine.Margin = new Padding(5);
    this._editCustomLine.Minimum = new Decimal(new int[4]
    {
      2,
      0,
      0,
      65536 /*0x010000*/
    });
    this._editCustomLine.Name = "_editCustomLine";
    this._editCustomLine.Size = new Size(80 /*0x50*/, 23);
    this._editCustomLine.TabIndex = 0;
    this._editCustomLine.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editCustomLine.Enter += new EventHandler(this._editCustomLine_Enter);
    this._editCustomLine.KeyDown += new KeyEventHandler(this._editCustomLine_KeyDown);
    this._editCustomLine.Leave += new EventHandler(this._editCustomLine_Leave);
    this._labelCustomWidth.Anchor = AnchorStyles.Left;
    this._labelCustomWidth.AutoSize = true;
    this._labelCustomWidth.Location = new Point(6, 7);
    this._labelCustomWidth.Name = "_labelCustomWidth";
    this._labelCustomWidth.Size = new Size(47, 13);
    this._labelCustomWidth.TabIndex = 0;
    this._labelCustomWidth.Text = "Другая:";
    this._bevel.Dock = DockStyle.Top;
    this._bevel.Location = new Point(0, 0);
    this._bevel.Name = "_bevel";
    this._bevel.Size = new Size(220, 2);
    this._bevel.TabIndex = 9;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.Silver;
    this.Controls.Add((Control) this._panelCustomLine);
    this.Controls.Add((Control) this._bevel);
    this.Name = nameof (SelectLineThicknessUserControl);
    this.Size = new Size(220, 244);
    this._panelCustomLine.ResumeLayout(false);
    this._panelCustomLine.PerformLayout();
    this._editCustomLine.EndInit();
    this.ResumeLayout(false);
  }

  [Serializable]
  internal class StandartLineThicknessesCollection : Collection<int>
  {
    [NonSerialized]
    public readonly IStandartLineThicknessesOwner Owner;

    public StandartLineThicknessesCollection(
      IStandartLineThicknessesOwner owner,
      IList<int> collection)
      : base(collection)
    {
      this.Owner = owner;
    }
  }

  public delegate void LineThicknessSelectedDelegate(
    SelectLineThicknessUserControl sender,
    int selectedThickness);
}
