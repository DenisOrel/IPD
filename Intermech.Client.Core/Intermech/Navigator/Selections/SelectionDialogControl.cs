
// Type: Intermech.Navigator.Selections.SelectionDialogControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections;

public class SelectionDialogControl : ObjectCreatorControl
{
  private CreatedObjectItem selObject;
  private SelectionDialog sForm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public SelectionDialogControl(CreatedObjectItem createdObject)
  {
    this.InitializeComponent();
    this._SaveAfterCommitCreation = true;
    this.selObject = createdObject;
    this.sForm = new SelectionDialog();
    this.Width = this.sForm.Width;
    this.Height = this.sForm.Height;
    this.MinimumSize = new Size(this.Width, this.Height);
    this.sForm.SetParent((Control) this);
    this.sForm.IsTypeVisible = createdObject.ObjectRelationArray.Count <= 0;
    this.sForm.SelectionLoad(this.selObject.ObjectID, this.selObject.ObjectTypeID);
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    try
    {
      this.sForm.SelectionLoad(this.selObject.ObjectID, this.selObject.ObjectTypeID);
      return true;
    }
    catch (Exception ex)
    {
      args.Error = ex;
      return false;
    }
  }

  public override bool SaveAfterCommit(IUserSession session, long newObjectID)
  {
    return this.sForm.TabsSave(session, newObjectID);
  }

  public override bool Save(PageSaveArgs args)
  {
    try
    {
      this.sForm.SelectionSave(true);
      return true;
    }
    catch (Exception ex)
    {
      args.Error = ex;
      return false;
    }
  }

  public override int HelpTopicID
  {
    get
    {
      return !MetaDataHelper.IsObjectTypeChildOf(this.selObject.ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545")) ? 797 : 783;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectionDialogControl));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (SelectionDialogControl);
    this.ResumeLayout(false);
  }
}
