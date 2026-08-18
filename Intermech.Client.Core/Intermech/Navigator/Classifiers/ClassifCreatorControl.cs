
// Type: Intermech.Navigator.Classifiers.ClassifCreatorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator.Classifiers;

internal sealed class ClassifCreatorControl : ObjectCreatorControl
{
  private CreatedObjectItem _createdObject;
  private CalcFormulaForm _editorForm;

  public ClassifCreatorControl(CreatedObjectItem createdObject)
  {
    this._createdObject = createdObject;
    this._editorForm = new CalcFormulaForm();
    this._editorForm.SetParent((Control) this);
    this._editorForm.ParentMode = 1;
    this._editorForm._dataSource = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._editorForm.parentClassifier = (ClassifierCalcFormula) null;
      if (this._createdObject.ObjectRelationArray != null && this._createdObject.ObjectRelationArray.Count > 0)
      {
        IDBRelationType relationType = sessionKeeper.Session.GetRelationType(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
        foreach (ObjectRelationLink objectRelation in this._createdObject.ObjectRelationArray)
        {
          if (objectRelation.RelationTypeID == relationType.RelationType)
          {
            this._editorForm.parentClassifier = new ClassifierCalcFormula(sessionKeeper.Session, objectRelation.ObjectID);
            break;
          }
        }
      }
      this._editorForm.CurrentClassifier = new ClassifierCalcFormula(sessionKeeper.Session, this._createdObject.ObjectID);
    }
    this._editorForm.LoadObjectData();
  }

  public override bool Save(PageSaveArgs args)
  {
    try
    {
      this._editorForm.SaveObjectData();
      return true;
    }
    catch (Exception ex)
    {
      args.Error = ex;
      return false;
    }
  }

  public override int HelpTopicID => 800;
}
