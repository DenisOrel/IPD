// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.SystemSettingsEditorView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal class SystemSettingsEditorView : MvpUserControl, ISystemSettingsEditorView, IView
{
  private bool allowEditSettings;
  private IContainer components;
  private CheckBox cbEnableIntegration;
  private Label lbEnableIntegration;
  private Label lbIsRestartRequired;

  public SystemSettingsEditorView()
  {
    this.InitializeComponent();
    this.allowEditSettings = true;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool AllowEditSettings
  {
    [DebuggerStepThrough] get => this.allowEditSettings;
    set
    {
      if (this.allowEditSettings == value)
        return;
      this.allowEditSettings = value;
      this.OnAllowEditSettingsChanged();
    }
  }

  private void OnAllowEditSettingsChanged()
  {
    this.cbEnableIntegration.Enabled = this.allowEditSettings;
    this.lbEnableIntegration.Enabled = this.allowEditSettings;
    this.lbIsRestartRequired.Enabled = this.allowEditSettings;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool EnableIntegration
  {
    get => this.cbEnableIntegration.Checked;
    set => this.cbEnableIntegration.Checked = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ShowRestartRequiredWarning
  {
    get => this.lbIsRestartRequired.Visible;
    set => this.lbIsRestartRequired.Visible = value;
  }

  public event EventHandler EditableStateChanged;

  private void ControlValueChanged(object sender, EventArgs e)
  {
    if (this.EditableStateChanged == null)
      return;
    this.EditableStateChanged((object) this, EventArgs.Empty);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.cbEnableIntegration = new CheckBox();
    this.lbEnableIntegration = new Label();
    this.lbIsRestartRequired = new Label();
    this.SuspendLayout();
    this.cbEnableIntegration.AutoSize = true;
    this.cbEnableIntegration.Location = new Point(7, 7);
    this.cbEnableIntegration.Name = "cbEnableIntegration";
    this.cbEnableIntegration.Size = new Size(194, 17);
    this.cbEnableIntegration.TabIndex = 0;
    this.cbEnableIntegration.Text = "Включить интеграцию с IMViewer";
    this.cbEnableIntegration.UseVisualStyleBackColor = true;
    this.cbEnableIntegration.CheckedChanged += new EventHandler(this.ControlValueChanged);
    this.lbEnableIntegration.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbEnableIntegration.Location = new Point(23, 32 /*0x20*/);
    this.lbEnableIntegration.Name = "lbEnableIntegration";
    this.lbEnableIntegration.Size = new Size(556, 32 /*0x20*/);
    this.lbEnableIntegration.TabIndex = 1;
    this.lbEnableIntegration.Text = "Внимание! Изменение этого флага потребует перезапуска всех клиентов IPS. Без этого корректная работа интеграции не гарантируется.";
    this.lbIsRestartRequired.AutoSize = true;
    this.lbIsRestartRequired.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lbIsRestartRequired.Location = new Point(23, 66);
    this.lbIsRestartRequired.Name = "lbIsRestartRequired";
    this.lbIsRestartRequired.Size = new Size(460, 13);
    this.lbIsRestartRequired.TabIndex = 2;
    this.lbIsRestartRequired.Text = "Значение флага было изменено. Требуется перезапуск всех клиентов IPS.";
    this.lbIsRestartRequired.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lbIsRestartRequired);
    this.Controls.Add((Control) this.lbEnableIntegration);
    this.Controls.Add((Control) this.cbEnableIntegration);
    this.Margin = new Padding(8);
    this.Name = nameof (SystemSettingsEditorView);
    this.Padding = new Padding(4);
    this.Size = new Size(587, 169);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
