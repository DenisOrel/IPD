// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterFactories.LayoutedPanel
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace GridViewExtensions.GridFilterFactories;

public class LayoutedPanel : Panel
{
  private Label[] _labels;
  private Control[] _controls;
  private int _horizontalSpacing;
  private int _verticalSpacing = 4;
  private int _controlsMinimumWidth = 40;
  private bool _rightAlignLabels;

  public LayoutedPanel()
  {
    this.AutoScroll = true;
    this.DoubleBuffered = true;
  }

  [Browsable(true)]
  [DefaultValue(40)]
  [Description("Gets and sets the minimum width for the controls. If the panel isn't big enough scrollbars will be created.")]
  public int ControlsMinimumWidth
  {
    get => this._controlsMinimumWidth;
    set
    {
      if (value < 1)
        throw new ArgumentException("Value must not be smaller 0", nameof (ControlsMinimumWidth));
      if (value == this._controlsMinimumWidth)
        return;
      this._controlsMinimumWidth = value;
      this.RefreshLayout();
    }
  }

  [Browsable(true)]
  [DefaultValue(0)]
  [Description("Gets and sets the horizontal space between the labels and controls.")]
  public int HorizontalSpacing
  {
    get => this._horizontalSpacing;
    set
    {
      if (value == this._horizontalSpacing)
        return;
      this._horizontalSpacing = value;
      this.RefreshLayout();
    }
  }

  [Browsable(true)]
  [DefaultValue(4)]
  [Description("Gets and sets the vertical space between the rows.")]
  public int VerticalSpacing
  {
    get => this._verticalSpacing;
    set
    {
      if (value == this._verticalSpacing)
        return;
      this._verticalSpacing = value;
      this.RefreshLayout();
    }
  }

  [Browsable(true)]
  [DefaultValue(false)]
  [Description("Gets and sets whether the labels are aligned to the right or to the left.")]
  public bool RightAlignLabels
  {
    get => this._rightAlignLabels;
    set
    {
      if (value == this._rightAlignLabels)
        return;
      this._rightAlignLabels = value;
      this.RefreshLayout();
    }
  }

  public void Clear()
  {
    this._labels = (Label[]) null;
    this._controls = (Control[]) null;
    List<Control> controlList = new List<Control>(this.Controls.Count);
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (!control.IsDisposed)
        control.Dispose();
    }
    this.Controls.Clear();
  }

  public void Fill(Label[] labels, Control[] controls)
  {
    if (labels.Length != controls.Length)
      throw new ArgumentException("Number of specified labels must match the number of specified controls.", nameof (labels));
    try
    {
      if (this.Parent != null)
        this.Parent.SuspendLayout();
      this.SuspendLayout();
      this.Clear();
      this._labels = new Label[labels.Length];
      labels.CopyTo((Array) this._labels, 0);
      this._controls = new Control[controls.Length];
      controls.CopyTo((Array) this._controls, 0);
      for (int index = 0; index < this._labels.Length; ++index)
        this.Controls.Add((Control) this._labels[index]);
      for (int index = 0; index < this._controls.Length; ++index)
        this.Controls.Add(this._controls[index]);
      this.RefreshLayout();
      Application.DoEvents();
    }
    finally
    {
      this.ResumeLayout();
      if (this.Parent != null)
        this.Parent.ResumeLayout();
    }
  }

  private void RefreshLayout()
  {
    if (this._labels == null || this._controls == null)
      return;
    int val1 = 0;
    for (int index = 0; index < this._labels.Length; ++index)
    {
      this._labels[index].AutoSize = true;
      val1 = Math.Max(val1, this._labels[index].Width);
    }
    int num1 = 0;
    for (int index = 0; index < this._labels.Length; ++index)
    {
      int num2 = Math.Max(this._controls[index].Height, this._labels[index].Height);
      this._controls[index].Location = new Point(val1 + this._horizontalSpacing, num1 + (num2 - this._controls[index].Height) / 2);
      this._controls[index].Width = Math.Max(this._controlsMinimumWidth, this.ClientSize.Width - this._controls[index].Left);
      if (this._rightAlignLabels)
        this._labels[index].Location = new Point(val1 - this._labels[index].Width, num1 + (num2 - this._labels[index].Height) / 2);
      else
        this._labels[index].Location = new Point(0, num1 + (num2 - this._labels[index].Height) / 2);
      num1 += num2 + this._verticalSpacing;
    }
    this.AutoScrollMinSize = new Size(val1 + this._horizontalSpacing + this._controlsMinimumWidth, 20);
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.RefreshLayout();
  }
}
