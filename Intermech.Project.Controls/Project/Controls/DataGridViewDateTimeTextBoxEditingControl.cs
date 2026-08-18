// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DataGridViewDateTimeTextBoxEditingControl
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class DataGridViewDateTimeTextBoxEditingControl : 
  EnhDataGridViewTextBoxEditingControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable,
  IPopupFormEditingControl
{
  [CanBeNull]
  protected DataGridView _DataGridView;
  [CanBeNull]
  private DataGridViewDateTimeTextBoxEditingControl.DateTimeForm _dateTimeForm;

  private void dateTimeForm_VisibleChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._dateTimeForm == null || this._dateTimeForm.Visible)
      return;
    this.BeginInvoke((Delegate) new EventHandler(this.DisposeForm));
  }

  private void DisposeForm([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._dateTimeForm != null)
      this._dateTimeForm.Dispose();
    this._dateTimeForm = (DataGridViewDateTimeTextBoxEditingControl.DateTimeForm) null;
    this._DataGridView?.EndEdit();
  }

  [CanBeNull]
  public override DataGridView EditingControlDataGridView
  {
    get => base.EditingControlDataGridView;
    set
    {
      this._DataGridView = value;
      base.EditingControlDataGridView = value;
    }
  }

  protected override void OnDoubleClick([NotNull] EventArgs e)
  {
    base.OnDoubleClick(e);
    this.ShowForm();
  }

  public void ShowForm()
  {
    DataGridViewDateTimeTextBoxEditingControl.DateTimeForm dateTimeForm = this._dateTimeForm;
    if ((dateTimeForm != null ? (!dateTimeForm.Visible ? 1 : 0) : 1) == 0)
      return;
    if (this._dateTimeForm == null)
    {
      this._dateTimeForm = new DataGridViewDateTimeTextBoxEditingControl.DateTimeForm(this);
      this._dateTimeForm.VisibleChanged += new EventHandler(this.dateTimeForm_VisibleChanged);
    }
    this._dateTimeForm.Show();
    this._dateTimeForm.Left -= 6;
    --this._dateTimeForm.Left;
    try
    {
      Point location = this._dateTimeForm.Location;
      Point point = location;
      point.Offset(this._dateTimeForm.Width, this._dateTimeForm.Height);
      Rectangle workingArea = Screen.GetWorkingArea((Control) this);
      if (location.X < workingArea.Left)
      {
        location.Offset(workingArea.Left - location.X, 0);
        this._dateTimeForm.Location = location;
      }
      else if (point.X > workingArea.Right)
      {
        location.Offset(workingArea.Right - point.X, 0);
        this._dateTimeForm.Location = location;
      }
      if (location.Y < workingArea.Top)
      {
        location.Offset(0, workingArea.Top - location.Y);
        this._dateTimeForm.Location = location;
      }
      else
      {
        if (point.Y <= workingArea.Bottom)
          return;
        location.Offset(0, workingArea.Bottom - point.Y);
        this._dateTimeForm.Location = location;
      }
    }
    catch (SecurityException ex)
    {
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this._dateTimeForm != null)
      this._dateTimeForm.Dispose();
    base.Dispose(disposing);
  }

  internal class DateTimeForm : Form
  {
    private readonly DataGridViewDateTimeTextBoxEditingControl _control;
    private readonly MonthCalendar _mc;

    public DateTimeForm([NotNull] DataGridViewDateTimeTextBoxEditingControl control)
    {
      this._control = control;
      this.ShowInTaskbar = false;
      this.TopMost = true;
      this.StartPosition = FormStartPosition.Manual;
      this.FormBorderStyle = FormBorderStyle.None;
      Point location = control.Location;
      location.Offset(0, control.Height);
      this.Location = control.PointToScreen(location);
      this.BackColor = SystemColors.ControlDark;
      this.Padding = new Padding(1);
      this._mc = new MonthCalendar();
      this._mc.SelectionStart = DateTime.MinValue;
      this._mc.DateChanged += new DateRangeEventHandler(this.mc_DateChanged);
      this._mc.DateSelected += new DateRangeEventHandler(this.mc_DateSelected);
      this._mc.KeyDown += new KeyEventHandler(this.mc_KeyDown);
      this._mc.MaxSelectionCount = 1;
      this.Size = this._mc.PreferredSize;
      this.Set();
      this.Controls.Add((Control) this._mc);
      this._mc.Dock = DockStyle.Fill;
    }

    private void HideForm() => this.Hide();

    [NotNull]
    private string FormatDateTime(DateTime dt)
    {
      return (this._control?._DataGridView is ProjectDataGridView dataGridView ? dataGridView.Project?.FormatDateTime(dt) : (string) null) ?? dt.ToShortDateString();
    }

    private DateTime ParseDateTime([NotNull] string s)
    {
      return this._control?._DataGridView is ProjectDataGridView dataGridView && dataGridView.Project != null ? dataGridView.Project.ParseDateTime(s) : DateTime.Parse(s);
    }

    private void mc_DateChanged([CanBeNull] object sender, [NotNull] DateRangeEventArgs e)
    {
      try
      {
        if (this._control == null || this._mc == null)
          return;
        this._control.Text = this.FormatDateTime(this._mc.SelectionStart);
      }
      catch (ObjectDisposedException ex)
      {
      }
    }

    private void mc_DateSelected([CanBeNull] object sender, [NotNull] DateRangeEventArgs e)
    {
      this.HideForm();
    }

    private void mc_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape && e.KeyCode != Keys.F4)
        return;
      this.HideForm();
    }

    protected override void OnDeactivate([NotNull] EventArgs e)
    {
      base.OnDeactivate(e);
      this.HideForm();
    }

    protected override void OnShown([NotNull] EventArgs e)
    {
      base.OnShown(e);
      if (this._mc != null)
      {
        int width1 = this._mc.Width;
        Padding padding = this.Padding;
        int left = padding.Left;
        int num1 = width1 + left;
        padding = this.Padding;
        int right = padding.Right;
        int width2 = num1 + right;
        int height1 = this._mc.Height;
        padding = this.Padding;
        int top = padding.Top;
        int num2 = height1 + top;
        padding = this.Padding;
        int bottom = padding.Bottom;
        int height2 = num2 + bottom;
        this.Size = new Size(width2, height2);
      }
      this.Set();
    }

    private void Set()
    {
      try
      {
        string s = this._control?.Text ?? string.Empty;
        if (s.EndsWith(Intermech.Project.IMProject.EstimationSymbol))
          s = s.Substring(0, s.Length - Intermech.Project.IMProject.EstimationSymbol.Length);
        if (this._mc == null)
          return;
        this._mc.SelectionStart = this.ParseDateTime(s);
      }
      catch (FormatException ex)
      {
        if (this._mc == null)
          return;
        this._mc.SelectionStart = DateTime.Today;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._mc?.Dispose();
      base.Dispose(disposing);
    }
  }
}
