// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.CompositionCopyingWizardForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.UI.Winforms.CodeBehaviors;
using Intermech.UI.Wpf.ViewModels;
using Intermech.UI.Wpf.WinformsInterop;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class CompositionCopyingWizardForm : Form
{
  private AutoCloseBehavior autoCloseBehavior;
  private IContainer components;
  private WpfElementHost wpfHost;
  private CompositionCopyingWizardControl compositionCopyingWizardControl1;

  public CompositionCopyingWizardForm()
  {
    this.InitializeComponent();
    Screen screen = Screen.FromControl((Control) this);
    this.Size = new System.Drawing.Size((int) ((double) screen.WorkingArea.Width * 0.8), (int) ((double) screen.WorkingArea.Height * 0.8));
    this.Text = DialogConsts.WizardCaption;
  }

  public WizardVM MainViewModel
  {
    get => (WizardVM) this.wpfHost.HostContainer.DataContext;
    set
    {
      this.wpfHost.HostContainer.DataContext = (object) value;
      this.UpdateCodeBehaviors();
    }
  }

  private void UpdateCodeBehaviors()
  {
    if (this.autoCloseBehavior != null)
    {
      this.autoCloseBehavior.Detach();
      this.autoCloseBehavior = (AutoCloseBehavior) null;
    }
    if (this.MainViewModel == null)
      return;
    this.autoCloseBehavior = new AutoCloseBehavior((Form) this, (INotifyPropertyChanged) this.MainViewModel);
  }

  private void CompositionCopyingWizardForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void CompositionCopyingWizardForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.wpfHost = new WpfElementHost();
    this.compositionCopyingWizardControl1 = new CompositionCopyingWizardControl();
    this.SuspendLayout();
    this.wpfHost.Dock = DockStyle.Fill;
    this.wpfHost.Location = new System.Drawing.Point(0, 0);
    this.wpfHost.Name = "wpfHost";
    this.wpfHost.Size = new System.Drawing.Size(784, 511 /*0x01FF*/);
    this.wpfHost.TabIndex = 0;
    this.wpfHost.Text = "wpfHost";
    this.wpfHost.Child = (UIElement) this.compositionCopyingWizardControl1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new System.Drawing.Size(784, 511 /*0x01FF*/);
    this.Controls.Add((Control) this.wpfHost);
    this.MinimumSize = new System.Drawing.Size(800, 550);
    this.Name = nameof (CompositionCopyingWizardForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "";
    this.FormClosed += new FormClosedEventHandler(this.CompositionCopyingWizardForm_FormClosed);
    this.Load += new EventHandler(this.CompositionCopyingWizardForm_Load);
    this.ResumeLayout(false);
  }
}
