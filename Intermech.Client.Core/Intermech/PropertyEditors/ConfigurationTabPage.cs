
// Type: Intermech.PropertyEditors.ConfigurationTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class ConfigurationTabPage : BaseTabPage
{
  private ConfigurationForm _ConfigurationForm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ConfigurationTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_83"))
  {
    this._ConfigurationForm = PropertyFormsHolder.PropertyForms(this.instGuid).ConfigurationFormType;
  }

  public override void DockToPanel(Panel panel) => this._ConfigurationForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._ConfigurationForm;

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
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
