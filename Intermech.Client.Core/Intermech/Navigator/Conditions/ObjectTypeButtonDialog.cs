
// Type: Intermech.Navigator.Conditions.ObjectTypeButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal sealed class ObjectTypeButtonDialog(
  IConditionDataProvider dataProvider,
  int attributeID,
  object value) : ButtonDialog(dataProvider, attributeID, value)
{
  public override bool OnOpenDialog(bool multiselect)
  {
    object objectType = (object) (this.Value != null ? (int) this.Value : -1);
    int num = (int) objectType;
    if (!this.dataProvider.ChoiseObjectType(ref objectType, SelectionType.ObjectTypes))
      return false;
    switch (objectType)
    {
      case int _:
        this.Value = (object) (int) objectType;
        this.Text = MetaDataHelper.GetObjectTypeName((int) objectType);
        break;
      case PublishTypeAttProxy _:
        this.Value = (object) ((PublishTypeAttProxy) objectType).ID;
        this.Text = ((PublishTypeAttProxy) objectType).Name;
        break;
    }
    return num != (int) this.Value;
  }
}
