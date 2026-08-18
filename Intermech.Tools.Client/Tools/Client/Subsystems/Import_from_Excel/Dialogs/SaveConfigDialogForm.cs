// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.Dialogs.SaveConfigDialogForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel.Dialogs;

public class SaveConfigDialogForm : Form
{
  private DialogConfigControl _dialogConfigControl;
  private IContainer components;

  public SaveConfigDialogForm(
    IEnumerable<Configuration> configurations,
    bool isAdmin = true,
    string configurationName = "",
    ConfigurationType configurationType = ConfigurationType.Personal)
  {
    this.InitializeComponent();
    DialogConfigControl dialogConfigControl = new DialogConfigControl(DialogConfigControlType.Save, configurations, isAdmin, configurationName, configurationType);
    dialogConfigControl.Dock = DockStyle.Fill;
    this._dialogConfigControl = dialogConfigControl;
    this._dialogConfigControl.OnAccept += new EventHandler(this._dialogConfigControl_OnAccept);
    this._dialogConfigControl.OnCancel += new EventHandler(this._dialogConfigControl_OnCancel);
    this.Controls.Add((Control) this._dialogConfigControl);
  }

  private void _dialogConfigControl_OnCancel(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
  }

  private void _dialogConfigControl_OnAccept(object sender, EventArgs e)
  {
    if (string.IsNullOrEmpty(this._dialogConfigControl.ConfigurationName))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_224"), LocalizationHolder.rm.GetString("Tools.Client_44"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
      this.DialogResult = DialogResult.OK;
  }

  public string ConfigurationName => this._dialogConfigControl?.ConfigurationName;

  public ConfigurationType ConfigurationType => this._dialogConfigControl.ConfigurationType;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(384, 361);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(150, 150);
    this.Name = nameof (SaveConfigDialogForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Сохранить конфигурацию";
    this.ResumeLayout(false);
  }
}
