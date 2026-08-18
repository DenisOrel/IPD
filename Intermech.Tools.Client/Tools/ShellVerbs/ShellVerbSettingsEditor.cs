// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.ShellVerbs.ShellVerbSettingsEditor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.LaunchActions;
using Intermech.Tools.Settings;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Tools.ShellVerbs;

internal sealed class ShellVerbSettingsEditor : DataEditorControl
{
  private readonly ShellVerbSettingsValidator validator;
  private readonly ShellVerbSettingsCodec codec;
  private IContainer components;
  private Label lbVerb;
  private TextBox tbVerb;

  public ShellVerbSettingsEditor()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.validator = new ShellVerbSettingsValidator();
    this.codec = new ShellVerbSettingsCodec();
  }

  public override void SetData(XmlDocument data, bool readOnly)
  {
    base.SetData(data, readOnly);
    this.tbVerb.Text = ((ShellVerbSettings) this.codec.Decode(data)).Verb;
    this.tbVerb.ReadOnly = readOnly;
  }

  public override XmlDocument GetData()
  {
    ShellVerbSettings shellVerbSettings = new ShellVerbSettings();
    shellVerbSettings.Verb = this.tbVerb.Text;
    this.validator.Validate((ISettingsObject) shellVerbSettings, SettingsValidatorContext.SettingsObjectOnly);
    return this.codec.Encode((ISettingsObject) shellVerbSettings);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShellVerbSettingsEditor));
    this.lbVerb = new Label();
    this.tbVerb = new TextBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbVerb, "lbVerb");
    this.lbVerb.Name = "lbVerb";
    componentResourceManager.ApplyResources((object) this.tbVerb, "tbVerb");
    this.tbVerb.Name = "tbVerb";
    this.tbVerb.TextChanged += new EventHandler(this.OnValueChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tbVerb);
    this.Controls.Add((Control) this.lbVerb);
    this.Name = nameof (ShellVerbSettingsEditor);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
