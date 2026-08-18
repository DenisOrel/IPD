// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.UserPrompt
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Client.Core;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for UserPrompt.</summary>
public class UserPrompt : Form
{
  private Label label1;
  private TextBox textBox1;
  private Button button1;
  private Button button2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private bool allowEmpty = true;

  public UserPrompt() => this.InitializeComponent();

  public string Execute(string MainCaption, string LocCaption)
  {
    this.Text = MainCaption;
    this.label1.Text = LocCaption;
    return this.ShowDialog() == DialogResult.OK ? this.textBox1.Text : "";
  }

  public string Execute(string MainCaption, string LocCaption, bool allowEmpty)
  {
    this.allowEmpty = allowEmpty;
    return this.Execute(MainCaption, LocCaption);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserPrompt));
    this.label1 = new Label();
    this.textBox1 = new TextBox();
    this.button1 = new Button();
    this.button2 = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.Name = "textBox1";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.label1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (UserPrompt);
    this.ShowInTaskbar = false;
    this.Tag = (object) "";
    this.Load += new EventHandler(this.UserPrompt_Load);
    this.FormClosed += new FormClosedEventHandler(this.UserPrompt_FormClosed);
    this.Resize += new EventHandler(this.UserPrompt_Resize);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void button1_Click(object sender, EventArgs e)
  {
    if (this.allowEmpty || !(this.textBox1.Text == ""))
      return;
    this.DialogResult = DialogResult.None;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_584"), LocalizationHolder.rm.GetString("Expert.Editor_107"), MessageBoxButtons.OK);
  }

  private void UserPrompt_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void UserPrompt_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void UserPrompt_Resize(object sender, EventArgs e)
  {
    if (this.Height == 110)
      return;
    this.Height = 110;
  }
}
