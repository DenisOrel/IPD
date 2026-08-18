
// Type: Intermech.PropertyEditors.SecurityForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Security;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Форма "Безопасность"</summary>
public class SecurityForm : TabPageForm
{
  private SecurityControl securityControl;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public SecurityForm(Guid aInstGuid)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SecurityForm));
    this.securityControl = new SecurityControl();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.securityControl, "securityControl");
    this.securityControl.FocusedUserId = (object) null;
    this.securityControl.Name = "securityControl";
    this.securityControl.Readonly = false;
    this.securityControl.Tag = (object) "   ";
    this.securityControl.SecurityChanged += new SecurityControl.SecurityChangedEventHandler(this.securityControl_SecurityChanged);
    this.Controls.Add((Control) this.securityControl);
    this.Name = nameof (SecurityForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "          ";
    this.ResumeLayout(false);
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage))
      return;
    try
    {
      this.securityControl.LoadSecurity(new object[1]
      {
        folder.Id
      }, folder as ISecurityCallback);
      StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, true);
    }
    catch (AccessDeniedException ex)
    {
      AccessDeniedExceptionForm.OnExceptionHandler((object) null, new ExceptionEventArgs((Exception) ex));
    }
  }

  public override bool SaveForm(IFolder folder)
  {
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage))
    {
      if (!this.securityControl.SaveSecurity())
        return false;
      StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, false);
    }
    return true;
  }

  private void securityControl_SecurityChanged(object sender, EventArgs e)
  {
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, true);
    EventsHolder.FireWasChanged(sender, this.instGuid, e);
  }

  /// <summary>id темы в справке</summary>
  /// <returns></returns>
  public override string HelpTopicID
  {
    get
    {
      if (this._folder == null)
        return "1005";
      if (this._folder is AttributeFolder || this._folder is AttributesFolder)
        return "1010";
      if (this._folder is AttributeGroupFolder)
        return "1017";
      if (this._folder is ObjectTypeFolder || this._folder is ObjectTypesFolder)
        return "1028";
      if (this._folder is RelationTypeFolder || this._folder is RelationTypesFolder)
        return "1034";
      if (this._folder is LevelFolder || this._folder is LevelsFolder)
        return "1040";
      if (this._folder is LCSchemaFolder || this._folder is LCSchemasFolder)
        return "1046";
      if (this._folder is AreasFolder)
        return "1051";
      return this._folder is LanguagesFolder ? "1056" : "1059";
    }
  }
}
