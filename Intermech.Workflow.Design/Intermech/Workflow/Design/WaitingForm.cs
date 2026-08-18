// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WaitingForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class WaitingForm : FormEx
{
  public static WaitingForm Form;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label textLabel;
  private ProgressBar progressBar;

  public WaitingForm() => this.InitializeComponent();

  public static void StartProgress(string Text, int stepsCount)
  {
    if (WaitingForm.Form == null)
      WaitingForm.Form = new WaitingForm();
    WaitingForm.Form.textLabel.Text = Text;
    WaitingForm.Form.progressBar.Minimum = 0;
    WaitingForm.Form.progressBar.Step = 1;
    WaitingForm.Form.progressBar.Maximum = stepsCount;
    WaitingForm.Form.Show();
    Application.DoEvents();
  }

  /// <summary>returns False if progress form aborted (closed)</summary>
  /// <returns></returns>
  public static bool IncProgress()
  {
    if (WaitingForm.Form != null)
    {
      WaitingForm.Form.progressBar.PerformStep();
      if (WaitingForm.Form.progressBar.Value == WaitingForm.Form.progressBar.Maximum)
        WaitingForm.CloseForm();
    }
    return WaitingForm.Form != null;
  }

  /// <summary>
  /// Use it only if error occured, otherwise form will be closed automatically when stepsCount reached
  /// </summary>
  public static void CloseForm()
  {
    if (WaitingForm.Form == null)
      return;
    WaitingForm.Form.Close();
    if (WaitingForm.Form == null)
      return;
    WaitingForm.Form.Dispose(true);
    WaitingForm.Form = (WaitingForm) null;
  }

  private void WaitingForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    WaitingForm.Form = (WaitingForm) null;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WaitingForm));
    this.textLabel = new Label();
    this.progressBar = new ProgressBar();
    this.SuspendLayout();
    this.textLabel.AccessibleDescription = (string) null;
    this.textLabel.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textLabel, "textLabel");
    this.textLabel.Font = (Font) null;
    this.textLabel.Name = "textLabel";
    this.progressBar.AccessibleDescription = (string) null;
    this.progressBar.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.progressBar, "progressBar");
    this.progressBar.BackgroundImage = (Image) null;
    this.progressBar.Font = (Font) null;
    this.progressBar.Name = "progressBar";
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.textLabel);
    this.Controls.Add((Control) this.progressBar);
    this.Font = (Font) null;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Icon = (Icon) null;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (WaitingForm);
    this.ShowInTaskbar = false;
    this.TopMost = true;
    this.FormClosed += new FormClosedEventHandler(this.WaitingForm_FormClosed);
    this.ResumeLayout(false);
  }
}
