// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.AVSMeasureForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class AVSMeasureForm : MeasureForm
{
  private bool readOnlyCount;
  private bool showAllCheckBox = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox checkBoxAll;

  public AVSMeasureForm()
  {
    this.InitializeComponent();
    this.ShowAllCheckBox = true;
  }

  public bool ShowAllCheckBox
  {
    get => this.showAllCheckBox;
    set
    {
      this.showAllCheckBox = value;
      if (value)
      {
        this.checkBoxAll.Visible = true;
        this.Size = new Size(this.Width, 180);
        this.Height = 180;
        this.okBtn.Top = 97;
        this.cancelBtn.Top = 97;
      }
      else
      {
        this.checkBoxAll.Visible = false;
        this.Size = new Size(this.Width, 140);
        this.Height = 140;
        this.okBtn.Top = 65;
        this.cancelBtn.Top = 65;
      }
      this.MinimumSize = this.Size;
    }
  }

  public bool AllProducts => this.checkBoxAll.Checked;

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    if (!this.ReadOnlyCount)
      return;
    this.valueEdit.KeyPress += new KeyPressEventHandler(this.ValueEdit_KeyPress);
    this.valueEdit.KeyDown += new KeyEventHandler(this.ValueEdit_KeyDown);
    this.valueEdit.BackColor = Color.LightGray;
  }

  private void ValueEdit_KeyDown(object sender, KeyEventArgs e)
  {
    if (!this.ReadOnlyCount)
      return;
    e.Handled = true;
  }

  private void ValueEdit_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (!this.ReadOnlyCount)
      return;
    e.Handled = true;
  }

  public bool ReadOnlyCount
  {
    get => this.readOnlyCount;
    set => this.readOnlyCount = value;
  }

  private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
  {
  }

  private void checkBoxAll_CheckStateChanged(object sender, EventArgs e)
  {
  }

  private void checkBoxAll_VisibleChanged(object sender, EventArgs e)
  {
  }

  private void AVSMeasureForm_Load(object sender, EventArgs e)
  {
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
    this.checkBoxAll = new CheckBox();
    this.SuspendLayout();
    this.checkBoxAll.AutoSize = true;
    this.checkBoxAll.Location = new Point(12, 70);
    this.checkBoxAll.Name = "checkBoxAll";
    this.checkBoxAll.Size = new Size(193, 17);
    this.checkBoxAll.TabIndex = 5;
    this.checkBoxAll.Text = "Применить для всех исполнений";
    this.checkBoxAll.UseVisualStyleBackColor = true;
    this.checkBoxAll.CheckedChanged += new EventHandler(this.checkBoxAll_CheckedChanged);
    this.checkBoxAll.CheckStateChanged += new EventHandler(this.checkBoxAll_CheckStateChanged);
    this.checkBoxAll.VisibleChanged += new EventHandler(this.checkBoxAll_VisibleChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(342, 102);
    this.Controls.Add((Control) this.checkBoxAll);
    this.Name = nameof (AVSMeasureForm);
    this.Load += new EventHandler(this.AVSMeasureForm_Load);
    this.Controls.SetChildIndex((Control) this.checkBoxAll, 0);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
