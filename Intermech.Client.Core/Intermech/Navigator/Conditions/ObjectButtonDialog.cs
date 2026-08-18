
// Type: Intermech.Navigator.Conditions.ObjectButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System.Collections;


namespace Intermech.Navigator.Conditions;

internal sealed class ObjectButtonDialog : ButtonDialog
{
  private readonly object _enabledObjectTypes;
  private readonly int[] _selection4Types;

  public ObjectButtonDialog(
    IConditionDataProvider dataProvider,
    int attributeID,
    int[] selection4Types,
    object enabledObjectTypes,
    object value)
    : base(dataProvider, attributeID, value)
  {
    this._selection4Types = selection4Types;
    this._enabledObjectTypes = enabledObjectTypes;
  }

  public override bool OnOpenDialog(bool multiselect)
  {
    object aObject = (object) null;
    if (!ValueRelationSelector.SelectObject(ref aObject, this.attributeID, this._selection4Types, this._enabledObjectTypes, multiselect))
      return false;
    this.Value = aObject;
    this.Text = aObject is IList list ? this.dataProvider.GetObjectCaption(list[list.Count - 1]) : this.dataProvider.GetObjectCaption(aObject);
    return true;
  }
}
