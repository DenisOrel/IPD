
// Type: Intermech.Navigator.Conditions.SubjectAreaButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;


namespace Intermech.Navigator.Conditions;

internal sealed class SubjectAreaButtonDialog(
  IConditionDataProvider dataProvider,
  int attributeID,
  object value) : ButtonDialog(dataProvider, attributeID, value)
{
  public override bool OnOpenDialog(bool multiselect)
  {
    object aObject = (object) null;
    if (!ValueRelationSelector.SelectSubjectArea(ref aObject))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBSubjectAreaType subjectAreaType = sessionKeeper.Session.GetSubjectAreaType((char) aObject);
      this.Value = (object) subjectAreaType.AreaID;
      this.Text = subjectAreaType.AreaName;
      return true;
    }
  }
}
