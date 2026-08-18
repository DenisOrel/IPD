
// Type: Intermech.Navigator.Conditions.RelationTypeButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal sealed class RelationTypeButtonDialog(IConditionDataProvider dataProvider, object value) : 
  ButtonDialog(dataProvider, 0, value)
{
  public override bool OnOpenDialog(bool multiselect)
  {
    object relationType = (object) -1;
    if (!this.dataProvider.ChoiseRelationType(ref relationType) || relationType.Equals(this.Value))
      return false;
    this.Value = relationType;
    this.Text = this.dataProvider.GetRelationTypeCaption(relationType);
    return true;
  }
}
