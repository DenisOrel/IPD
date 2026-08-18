
// Type: Intermech.Navigator.Selections.SelectionCreatorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections;

public class SelectionCreatorControl : ObjectCreatorControl
{
  private CreatedObjectItem _createdObject;
  private List<long> _objIDList;
  private SelectionForm _editorForm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public SelectionCreatorControl(CreatedObjectItem createdObject)
  {
    this._createdObject = createdObject;
    this._editorForm = new SelectionForm()
    {
      ParentMode = SelectionFormMode.InObjectCreator
    };
    this._editorForm.SetParent((Control) this);
    this._objIDList = new List<long>()
    {
      createdObject.ObjectID
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int relationTypeId = MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
      List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(new Guid("cad00156-306c-11d8-b4e9-00304f19f545"));
      foreach (ObjectRelationLink objectRelation in createdObject.ObjectRelationArray)
      {
        if (objectRelation.RelationTypeID == relationTypeId)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectRelation.ObjectID);
          if (!objectInfo.Empty && objectTypeChildrenId.Contains(objectInfo.ObjectTypeID))
            this._objIDList.Add(objectInfo.ObjectID);
        }
      }
    }
    this._editorForm.SelectionLoad(this._createdObject.ObjectID, this._objIDList);
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._editorForm.ReloadObjTypes(sessionKeeper.Session, this._objIDList);
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._createdObject.ObjectID);
      this._editorForm.ReloadSelectionType(sessionKeeper.Session, dbObject);
    }
    return base.Refresh(args);
  }

  public override bool Save(PageSaveArgs args)
  {
    try
    {
      this._editorForm.SelectionSave();
      return true;
    }
    catch (Exception ex)
    {
      args.Error = ex;
      return false;
    }
  }

  public override int HelpTopicID => 785;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectionCreatorControl));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (SelectionCreatorControl);
    this.ResumeLayout(false);
  }
}
