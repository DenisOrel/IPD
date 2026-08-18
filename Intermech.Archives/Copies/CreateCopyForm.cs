// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CreateCopyForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// 
/// </summary>
public class CreateCopyForm : Form
{
  /// <summary>
  /// 
  /// </summary>
  internal int copyCount = 1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOK;
  private Button btnCancel;
  private Label label1;
  private NumericUpDown numericUpDown1;

  /// <summary>
  /// 
  /// </summary>
  public CreateCopyForm() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreateCopy_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreateCopy_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOK_Click(object sender, EventArgs e)
  {
    this.copyCount = Convert.ToInt32(this.numericUpDown1.Value);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CreateCopyForm));
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.label1 = new Label();
    this.numericUpDown1 = new NumericUpDown();
    this.numericUpDown1.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.numericUpDown1, "numericUpDown1");
    this.numericUpDown1.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDown1.Name = "numericUpDown1";
    this.numericUpDown1.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.numericUpDown1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CreateCopyForm);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.CreateCopy_FormClosed);
    this.Load += new EventHandler(this.CreateCopy_Load);
    this.numericUpDown1.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
