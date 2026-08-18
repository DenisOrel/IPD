// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.DocReport
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Client.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class DocReport : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button button1;
  private ListBox lb;

  public DocReport() => this.InitializeComponent();

  public void Launch(List<string> messageList)
  {
    this.lb.Items.Clear();
    foreach (object message in messageList)
      this.lb.Items.Add(message);
    int num = (int) this.ShowDialog();
  }

  private void DocReport_Load(object sender, EventArgs e) => FormStorage.LoadLayout((Control) this);

  private void DocReport_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocReport));
    this.panel1 = new Panel();
    this.button1 = new Button();
    this.lb = new ListBox();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.button1);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lb, "lb");
    this.lb.FormattingEnabled = true;
    this.lb.Name = "lb";
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lb);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (DocReport);
    this.FormClosed += new FormClosedEventHandler(this.DocReport_FormClosed);
    this.Load += new EventHandler(this.DocReport_Load);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
