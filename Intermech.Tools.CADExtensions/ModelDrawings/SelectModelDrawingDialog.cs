// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.ModelDrawings.SelectModelDrawingDialog
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.CADExtensions.ModelDrawings;

internal class SelectModelDrawingDialog : Form
{
  private readonly List<string> _chartsLis;
  public string SelectedModelDrawingFile = string.Empty;
  private IContainer components;
  private Button btnOK;
  private Button btnCancel;
  private ListBox clbModelDrawing;

  public SelectModelDrawingDialog(List<string> chartsList)
  {
    this._chartsLis = chartsList;
    this.InitializeComponent();
  }

  private void CheckChartsForOpen_Load(object sender, EventArgs e)
  {
    this.clbModelDrawing.BeginUpdate();
    foreach (object chartsLi in this._chartsLis)
      this.clbModelDrawing.Items.Add(chartsLi);
    this.clbModelDrawing.EndUpdate();
    this.clbModelDrawing.SelectedIndex = 0;
    this.clbModelDrawing.Focus();
  }

  private void button1_Click(object sender, EventArgs e)
  {
    this.SelectedModelDrawingFile = this.clbModelDrawing.SelectedItem.ToString();
    this.Close();
  }

  private void button2_Click(object sender, EventArgs e)
  {
    this.SelectedModelDrawingFile = string.Empty;
    this.Close();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.clbModelDrawing = new ListBox();
    this.SuspendLayout();
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.Location = new Point(174, 232);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.button1_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point((int) byte.MaxValue, 232);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.button2_Click);
    this.clbModelDrawing.FormattingEnabled = true;
    this.clbModelDrawing.Location = new Point(12, 12);
    this.clbModelDrawing.Name = "clbModelDrawing";
    this.clbModelDrawing.Size = new Size(318, 212);
    this.clbModelDrawing.TabIndex = 2;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(342, 262);
    this.Controls.Add((Control) this.clbModelDrawing);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(195, 300);
    this.Name = "CheckModelDrawingForOpen";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Load += new EventHandler(this.CheckChartsForOpen_Load);
    this.ResumeLayout(false);
  }
}
