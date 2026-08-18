// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.VintaSoftScanner.ScanerSelectForm
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Vintasoft.Twain;

#nullable disable
namespace Intermech.TwainScanner.VintaSoftScanner;

public class ScanerSelectForm : Form
{
  private DeviceManager _deviceManager;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button bCancel;
  private Button bOk;
  private Panel panel2;
  private ListBox lbItems;

  public ScanerSelectForm()
  {
    this.InitializeComponent();
    this.bOk.Enabled = false;
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    List<string> stringList = new List<string>();
    for (int index = 0; index < ((ReadOnlyCollectionBase) this._deviceManager.Devices).Count; ++index)
      this.lbItems.Items.Add((object) this._deviceManager.Devices[index].Info.ProductName);
  }

  private void lbItems_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this.lbItems.SelectedItem == null)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  public static string Execute(DeviceManager deviceManager)
  {
    ScanerSelectForm scanerSelectForm = new ScanerSelectForm();
    scanerSelectForm._deviceManager = deviceManager;
    return scanerSelectForm.ShowDialog() == DialogResult.OK ? Convert.ToString(scanerSelectForm.lbItems.SelectedItem) : (string) null;
  }

  private void lbItems_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lbItems.SelectedIndex < 0)
      return;
    this.bOk.Enabled = true;
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
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.bOk = new Button();
    this.bCancel = new Button();
    this.lbItems = new ListBox();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOk);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 275);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(362, 36);
    this.panel1.TabIndex = 0;
    this.panel2.Controls.Add((Control) this.lbItems);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(362, 275);
    this.panel2.TabIndex = 1;
    this.bOk.DialogResult = DialogResult.OK;
    this.bOk.Location = new Point(194, 6);
    this.bOk.Name = "bOk";
    this.bOk.Size = new Size(75, 23);
    this.bOk.TabIndex = 0;
    this.bOk.Text = "OK";
    this.bOk.UseVisualStyleBackColor = true;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(275, 6);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 1;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.lbItems.Dock = DockStyle.Fill;
    this.lbItems.FormattingEnabled = true;
    this.lbItems.Location = new Point(0, 0);
    this.lbItems.Name = "lbItems";
    this.lbItems.Size = new Size(362, 275);
    this.lbItems.TabIndex = 0;
    this.lbItems.MouseDoubleClick += new MouseEventHandler(this.lbItems_MouseDoubleClick);
    this.lbItems.SelectedIndexChanged += new EventHandler(this.lbItems_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this.bOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(362, 311);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (ScanerSelectForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выбор сканера";
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
