
// Type: Intermech.Redline.Controls.UCRedlineLayerInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline.Controls;

public class UCRedlineLayerInfo : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel flpMain;
  private Panel panelUser;
  private Panel panelBizProc;
  private Panel panelTime;
  private Panel panelStep;
  private Label lbUser;
  private TextBox tBoxUser;
  private TextBox tBoxBusiness_process;
  private Label lbBizProcess;
  private Label lbTime;
  private TextBox tBoxTime;
  private TextBox tBoxStep;
  private Label lbStep;

  /// <summary>Очистить поля</summary>
  public void ClearTextBoxes()
  {
    this.tBoxUser.Text = "";
    this.tBoxTime.Text = "";
    this.tBoxBusiness_process.Text = "";
    this.tBoxStep.Text = "";
  }

  /// <summary>Заполнить текстовые поля данными из слоя</summary>
  public void UpdateInfoText(RedlineLayer redLayer)
  {
    if (redLayer == null)
    {
      this.ClearTextBoxes();
    }
    else
    {
      this.tBoxUser.Text = redLayer.UserID.Split('|')[0];
      this.tBoxTime.Text = redLayer.Time.ToString("dd.M.yyyy H.mm");
      this.tBoxBusiness_process.Text = redLayer.NameBusiness;
      this.tBoxStep.Text = redLayer.StepBusiness;
    }
  }

  /// <summary>Конструктор</summary>
  public UCRedlineLayerInfo() => this.InitializeComponent();

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
    this.flpMain = new Panel();
    this.panelUser = new Panel();
    this.lbUser = new Label();
    this.tBoxUser = new TextBox();
    this.panelBizProc = new Panel();
    this.tBoxBusiness_process = new TextBox();
    this.lbBizProcess = new Label();
    this.panelTime = new Panel();
    this.lbTime = new Label();
    this.tBoxTime = new TextBox();
    this.panelStep = new Panel();
    this.tBoxStep = new TextBox();
    this.lbStep = new Label();
    this.flpMain.SuspendLayout();
    this.panelUser.SuspendLayout();
    this.panelBizProc.SuspendLayout();
    this.panelTime.SuspendLayout();
    this.panelStep.SuspendLayout();
    this.SuspendLayout();
    this.flpMain.BorderStyle = BorderStyle.FixedSingle;
    this.flpMain.Controls.Add((Control) this.panelUser);
    this.flpMain.Controls.Add((Control) this.panelBizProc);
    this.flpMain.Controls.Add((Control) this.panelTime);
    this.flpMain.Controls.Add((Control) this.panelStep);
    this.flpMain.Dock = DockStyle.Fill;
    this.flpMain.Location = new Point(0, 0);
    this.flpMain.Name = "flpMain";
    this.flpMain.Size = new Size(200, 150);
    this.flpMain.TabIndex = 0;
    this.panelUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panelUser.Controls.Add((Control) this.lbUser);
    this.panelUser.Controls.Add((Control) this.tBoxUser);
    this.panelUser.Location = new Point(3, 3);
    this.panelUser.Name = "panelUser";
    this.panelUser.Padding = new Padding(50, 5, 0, 0);
    this.panelUser.Size = new Size(189, 32 /*0x20*/);
    this.panelUser.TabIndex = 0;
    this.lbUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbUser.AutoSize = true;
    this.lbUser.Location = new Point(6, 9);
    this.lbUser.Name = "lbUser";
    this.lbUser.Size = new Size(37, 13);
    this.lbUser.TabIndex = 10;
    this.lbUser.Text = "ФИО:";
    this.tBoxUser.Dock = DockStyle.Fill;
    this.tBoxUser.Location = new Point(50, 5);
    this.tBoxUser.Name = "tBoxUser";
    this.tBoxUser.ReadOnly = true;
    this.tBoxUser.Size = new Size(139, 20);
    this.tBoxUser.TabIndex = 11;
    this.panelBizProc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panelBizProc.Controls.Add((Control) this.tBoxBusiness_process);
    this.panelBizProc.Controls.Add((Control) this.lbBizProcess);
    this.panelBizProc.Location = new Point(3, 41);
    this.panelBizProc.Name = "panelBizProc";
    this.panelBizProc.Padding = new Padding(50, 5, 0, 0);
    this.panelBizProc.Size = new Size(189, 32 /*0x20*/);
    this.panelBizProc.TabIndex = 1;
    this.tBoxBusiness_process.Dock = DockStyle.Fill;
    this.tBoxBusiness_process.Location = new Point(50, 5);
    this.tBoxBusiness_process.Name = "tBoxBusiness_process";
    this.tBoxBusiness_process.ReadOnly = true;
    this.tBoxBusiness_process.Size = new Size(139, 20);
    this.tBoxBusiness_process.TabIndex = 13;
    this.lbBizProcess.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbBizProcess.AutoSize = true;
    this.lbBizProcess.Location = new Point(6, 9);
    this.lbBizProcess.Name = "lbBizProcess";
    this.lbBizProcess.Size = new Size(25, 13);
    this.lbBizProcess.TabIndex = 12;
    this.lbBizProcess.Text = "БП:";
    this.panelTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panelTime.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.panelTime.Controls.Add((Control) this.lbTime);
    this.panelTime.Controls.Add((Control) this.tBoxTime);
    this.panelTime.Location = new Point(3, 79);
    this.panelTime.Name = "panelTime";
    this.panelTime.Padding = new Padding(90, 5, 0, 0);
    this.panelTime.Size = new Size(189, 32 /*0x20*/);
    this.panelTime.TabIndex = 2;
    this.lbTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbTime.AutoSize = true;
    this.lbTime.Location = new Point(6, 9);
    this.lbTime.Name = "lbTime";
    this.lbTime.Size = new Size(80 /*0x50*/, 13);
    this.lbTime.TabIndex = 11;
    this.lbTime.Text = "Дата и время:";
    this.lbTime.TextAlign = ContentAlignment.MiddleRight;
    this.tBoxTime.Dock = DockStyle.Fill;
    this.tBoxTime.Location = new Point(90, 5);
    this.tBoxTime.Name = "tBoxTime";
    this.tBoxTime.ReadOnly = true;
    this.tBoxTime.Size = new Size(99, 20);
    this.tBoxTime.TabIndex = 12;
    this.panelStep.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panelStep.Controls.Add((Control) this.tBoxStep);
    this.panelStep.Controls.Add((Control) this.lbStep);
    this.panelStep.Location = new Point(3, 117);
    this.panelStep.Name = "panelStep";
    this.panelStep.Padding = new Padding(50, 5, 0, 0);
    this.panelStep.Size = new Size(189, 32 /*0x20*/);
    this.panelStep.TabIndex = 3;
    this.tBoxStep.Dock = DockStyle.Fill;
    this.tBoxStep.Location = new Point(50, 5);
    this.tBoxStep.Name = "tBoxStep";
    this.tBoxStep.ReadOnly = true;
    this.tBoxStep.Size = new Size(139, 20);
    this.tBoxStep.TabIndex = 14;
    this.lbStep.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbStep.AutoSize = true;
    this.lbStep.Location = new Point(6, 9);
    this.lbStep.Name = "lbStep";
    this.lbStep.Size = new Size(30, 13);
    this.lbStep.TabIndex = 13;
    this.lbStep.Text = "Шаг:";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.flpMain);
    this.MinimumSize = new Size(200, 150);
    this.Name = nameof (UCRedlineLayerInfo);
    this.Size = new Size(200, 150);
    this.flpMain.ResumeLayout(false);
    this.panelUser.ResumeLayout(false);
    this.panelUser.PerformLayout();
    this.panelBizProc.ResumeLayout(false);
    this.panelBizProc.PerformLayout();
    this.panelTime.ResumeLayout(false);
    this.panelTime.PerformLayout();
    this.panelStep.ResumeLayout(false);
    this.panelStep.PerformLayout();
    this.ResumeLayout(false);
  }
}
