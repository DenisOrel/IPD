// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProgressForm
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public class ProgressForm : Form
{
  private bool _canClose;
  private IContainer components;
  private ProgressBar _progressBar;
  private Label _captionLabel;
  private Panel _spacerPanel;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal ProgressBar ProgressBar
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._progressBar.CheckInitializedIn<ProgressBar>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Label CaptionLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._captionLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Panel SpacerPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._spacerPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  public ProgressForm()
  {
    this.InitializeComponent();
    Form openForm = Application.OpenForms[0];
    if (openForm != null)
      this.Owner = openForm;
    this._canClose = false;
    using (Graphics graphics = Graphics.FromHwnd(this.CaptionLabel.Handle))
    {
      SizeF sizeF = graphics.MeasureString("W\r\nW", this.CaptionLabel.Font, this.CaptionLabel.Width);
      int num = this.CaptionLabel.Padding.Top + this.CaptionLabel.Padding.Bottom + 3;
      this.CaptionLabel.ClientSize = new Size(this.CaptionLabel.ClientSize.Width, Convert.ToInt32(sizeF.Height) + num);
    }
    this.ClientSize = this.ClientSize with
    {
      Height = this.CaptionLabel.Height + this.ProgressBar.Height + this.SpacerPanel.Height + this.Padding.Top + this.Padding.Bottom
    };
  }

  private void ProgressForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
    e.Cancel = !this._canClose;
  }

  public new void Close()
  {
    this._canClose = true;
    base.Close();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProgressForm));
    this._progressBar = new ProgressBar();
    this._captionLabel = new Label();
    this._spacerPanel = new Panel();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._progressBar, "_progressBar");
    this._progressBar.Name = "_progressBar";
    this._progressBar.Step = 1;
    componentResourceManager.ApplyResources((object) this._captionLabel, "_captionLabel");
    this._captionLabel.AutoEllipsis = true;
    this._captionLabel.Name = "_captionLabel";
    componentResourceManager.ApplyResources((object) this._spacerPanel, "_spacerPanel");
    this._spacerPanel.Name = "_spacerPanel";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ControlBox = false;
    this.Controls.Add((Control) this._progressBar);
    this.Controls.Add((Control) this._spacerPanel);
    this.Controls.Add((Control) this._captionLabel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProgressForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.ProgressForm_FormClosing);
    this.ResumeLayout(false);
  }
}
