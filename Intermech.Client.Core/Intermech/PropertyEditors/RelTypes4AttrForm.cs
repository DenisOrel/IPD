
// Type: Intermech.PropertyEditors.RelTypes4AttrForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for RelTypes4AttrForm.</summary>
public class RelTypes4AttrForm : CustomTypes4AttrForm
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public RelTypes4AttrForm(Guid aInstGuid)
    : base(aInstGuid, 6)
  {
    this.InitializeComponent();
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RelTypes4AttrForm));
    this.SuspendLayout();
    this.Name = nameof (RelTypes4AttrForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "      ";
    this.ResumeLayout(false);
  }

  protected override BaseTabPage ActualTabPage
  {
    get => (BaseTabPage) TabPagesHolder.TabPages(this.instGuid).RelTypes4AttrTabPage;
  }
}
