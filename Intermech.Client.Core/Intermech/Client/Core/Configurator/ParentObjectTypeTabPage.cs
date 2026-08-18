
// Type: Intermech.Client.Core.Configurator.ParentObjectTypeTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.Configurator;

/// <summary>
/// 
/// </summary>
public class ParentObjectTypeTabPage : BaseTabPage
{
  private ParentObjectTypeForm parentForm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ParentObjectTypeTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_1263"))
  {
    this.parentForm = PropertyFormsHolder.PropertyForms(this.instGuid).ParentTypeForm;
  }

  public override void DockToPanel(Panel panel) => this.parentForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this.parentForm;

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
