// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.StatisticsReportForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Statistics;

public class StatisticsReportForm : Form
{
  private IContainer components;
  private Button closeButton;
  private TextBox textBox;

  public StatisticsReportForm() => this.InitializeComponent();

  public void ShowReport(List<string> report)
  {
    this.textBox.Lines = report.ToArray();
    int num = (int) this.ShowDialog();
  }

  private void StatisticsReportForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void StatisticsReportForm_FormClosed(object sender, FormClosedEventArgs e)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StatisticsReportForm));
    this.closeButton = new Button();
    this.textBox = new TextBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.closeButton, "closeButton");
    this.closeButton.DialogResult = DialogResult.OK;
    this.closeButton.Name = "closeButton";
    this.closeButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.textBox, "textBox");
    this.textBox.Name = "textBox";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.textBox);
    this.Controls.Add((Control) this.closeButton);
    this.Name = nameof (StatisticsReportForm);
    this.Tag = (object) " ";
    this.Load += new EventHandler(this.StatisticsReportForm_Load);
    this.FormClosed += new FormClosedEventHandler(this.StatisticsReportForm_FormClosed);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
