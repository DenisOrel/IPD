// Decompiled with JetBrains decompiler
// Type: IMLauncher.ExceptionForm
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace IMLauncher;

public class ExceptionForm : Form
{
  private Button button1;
  private Button button2;
  private Label labelHint;
  private TextBox exceptionText;
  private RichTextBox exceptionStack;
  private Button button3;
  private bool _collapsed;
  private int _delta;
  private int _fullHeight;
  private SaveFileDialog sd;
  private Exception exc;
  private System.ComponentModel.Container components;

  public ExceptionForm()
  {
    this.InitializeComponent();
    this._collapsed = true;
    this._delta = this.Height - this.ClientRectangle.Height - 4;
    this._fullHeight = this.Height;
    this.Height = this.exceptionStack.Top + this._delta;
    this.button3.Select();
    Label labelHint = this.labelHint;
    DateTime now = DateTime.Now;
    string longDateString = now.ToLongDateString();
    now = DateTime.Now;
    string longTimeString = now.ToLongTimeString();
    string str = $"В системе возникла исключительная ситуация ({longDateString} в {longTimeString}).";
    labelHint.Text = str;
  }

  public DialogResult ShowException(Exception e)
  {
    this.exc = e;
    this.exceptionText.Text = e.Message;
    this.exceptionStack.Text = ExceptionServices.GetExtendedStackTrace(e);
    return this.ShowDialog();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExceptionForm));
    this.exceptionText = new TextBox();
    this.button1 = new Button();
    this.button2 = new Button();
    this.labelHint = new Label();
    this.exceptionStack = new RichTextBox();
    this.button3 = new Button();
    this.sd = new SaveFileDialog();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.exceptionText, "exceptionText");
    this.exceptionText.Name = "exceptionText";
    this.exceptionText.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Ignore;
    this.button1.Name = "button1";
    this.button2.DialogResult = DialogResult.Abort;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.labelHint, "labelHint");
    this.labelHint.Name = "labelHint";
    componentResourceManager.ApplyResources((object) this.exceptionStack, "exceptionStack");
    this.exceptionStack.Name = "exceptionStack";
    this.exceptionStack.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.button3_Click);
    this.sd.CheckPathExists = false;
    this.sd.DefaultExt = "xml";
    componentResourceManager.ApplyResources((object) this.sd, "sd");
    this.sd.RestoreDirectory = true;
    this.sd.SupportMultiDottedExtensions = true;
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button1;
    this.Controls.Add((Control) this.button3);
    this.Controls.Add((Control) this.exceptionStack);
    this.Controls.Add((Control) this.labelHint);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.exceptionText);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExceptionForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void button3_Click(object sender, EventArgs e)
  {
    if (this._collapsed)
    {
      this.Height = this._fullHeight;
    }
    else
    {
      this._fullHeight = this.Height;
      this.Height = this.exceptionStack.Top + this._delta;
    }
    this._collapsed = !this._collapsed;
  }
}
