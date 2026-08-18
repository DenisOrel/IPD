
// Type: Intermech.PropertyEditors.EmptyForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

public class EmptyForm : TabPageForm
{
  private IContainer components;

  public EmptyForm(Guid aInstGuid)
    : base(aInstGuid)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EmptyForm));
    this.SuspendLayout();
    this.Name = nameof (EmptyForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "   ";
    this.ResumeLayout(false);
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).EmptyTabPage))
      return;
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).EmptyTabPage, true);
  }
}
