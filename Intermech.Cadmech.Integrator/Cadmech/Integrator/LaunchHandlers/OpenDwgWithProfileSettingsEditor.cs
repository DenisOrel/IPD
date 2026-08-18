// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.LaunchHandlers.OpenDwgWithProfileSettingsEditor
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools;
using Intermech.Tools.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Cadmech.Integrator.LaunchHandlers;

internal class OpenDwgWithProfileSettingsEditor : DataEditorControl
{
  private OpenDwgWithProfileSettingCodec settingsCodec;
  private OpenDwgWithProfileSettingsValidator settingsValidator;
  private IContainer components;
  private Label lbProfileName;
  private TextBox tbProfileName;

  public OpenDwgWithProfileSettingsEditor()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.settingsCodec = new OpenDwgWithProfileSettingCodec();
    this.settingsValidator = new OpenDwgWithProfileSettingsValidator();
  }

  public override XmlDocument GetData()
  {
    OpenDwgWithProfileSettings newSettings = new OpenDwgWithProfileSettings();
    this.CaptureNewSettings(newSettings);
    this.settingsValidator.Validate((ISettingsObject) newSettings, SettingsValidatorContext.SettingsObjectOnly);
    return this.settingsCodec.Encode((ISettingsObject) newSettings);
  }

  private void CaptureNewSettings(OpenDwgWithProfileSettings newSettings)
  {
    string str = this.tbProfileName.Text;
    if (str != null)
      str = str.Trim();
    newSettings.ProfileName = str;
  }

  public override void SetData(XmlDocument data, bool readOnly)
  {
    base.SetData(data, readOnly);
    this.InitializeEditors((OpenDwgWithProfileSettings) this.settingsCodec.Decode(data));
  }

  private void InitializeEditors(OpenDwgWithProfileSettings settings)
  {
    this.tbProfileName.Text = settings.ProfileName;
  }

  private void OnValueChanged(object sender, EventArgs e) => this.RaiseDataChanged();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.lbProfileName = new Label();
    this.tbProfileName = new TextBox();
    this.SuspendLayout();
    this.lbProfileName.AutoSize = true;
    this.lbProfileName.Location = new Point(-2, 7);
    this.lbProfileName.Name = "lbProfileName";
    this.lbProfileName.Size = new Size(300, 13);
    this.lbProfileName.TabIndex = 0;
    this.lbProfileName.Text = "Имя профиля AutoCAD, под которым будет открыт файл: ";
    this.tbProfileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbProfileName.Location = new Point(304, 4);
    this.tbProfileName.Name = "tbProfileName";
    this.tbProfileName.Size = new Size(217, 20);
    this.tbProfileName.TabIndex = 1;
    this.tbProfileName.TextChanged += new EventHandler(this.OnValueChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tbProfileName);
    this.Controls.Add((Control) this.lbProfileName);
    this.Name = nameof (OpenDwgWithProfileSettingsEditor);
    this.Size = new Size(524, 33);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
