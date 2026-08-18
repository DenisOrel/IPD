
// Type: Intermech.Navigator.Conditions.SelectAttributeHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

internal class SelectAttributeHelper
{
  public static ConditionAttributeInfo Select(
    IConditionDataProvider dataProvider,
    int[] objectTypeIDs,
    object selectedAttribute)
  {
    return SelectAttributeHelper.Select(dataProvider, objectTypeIDs, AttributeSourceTypes.Auto, selectedAttribute);
  }

  public static ConditionAttributeInfo Select(
    IConditionDataProvider dataProvider,
    int[] objectTypeIDs,
    AttributeSourceTypes attributeSource,
    object selectedAttribute)
  {
    if (dataProvider.AnyAttributes(attributeSource, objectTypeIDs))
    {
      AttributesSelectDlg attributesSelectDlg1;
      if (selectedAttribute == null)
        attributesSelectDlg1 = new AttributesSelectDlg(false);
      else if (!(selectedAttribute is int num1))
        attributesSelectDlg1 = new AttributesSelectDlg(false, new Guid[1]
        {
          (Guid) selectedAttribute
        });
      else
        attributesSelectDlg1 = new AttributesSelectDlg(false, new int[1]
        {
          num1
        });
      AttributesSelectDlg attributesSelectDlg2 = attributesSelectDlg1;
      attributesSelectDlg2.SelectorFilter = (ISelectorFilter) new WithoutObligatoryFilter(new AttributeSourceTypes[2]
      {
        AttributeSourceTypes.Object,
        AttributeSourceTypes.Relation
      });
      if (objectTypeIDs != null && objectTypeIDs.Length == 1)
        attributesSelectDlg2.LoadAttrDialogForObjectsTypes(MetaDataHelper.GetObjectTypeGuid(objectTypeIDs[0]));
      attributesSelectDlg2.ForbiddenAttrsTypesFilter.Add(FieldTypes.ftPassword);
      if (attributesSelectDlg2.ShowDialog() == DialogResult.OK && attributesSelectDlg2.SelectedAttributesID.Count > 0)
      {
        int num2 = attributesSelectDlg2.SelectedAttributesID[0];
        return new ConditionAttributeInfo((object) num2, dataProvider.GetAttributeName((object) num2), dataProvider.GetFieldType((object) num2));
      }
    }
    else
    {
      List<ConditionAttributeInfo> listAttributes = dataProvider.GetListAttributes(attributeSource, objectTypeIDs);
      using (ListAttributesForm listAttributesForm = new ListAttributesForm())
      {
        listAttributesForm.InitializeData(listAttributes);
        if (listAttributesForm.ShowDialog() == DialogResult.OK)
        {
          if (listAttributesForm.SelectedAttribute != null)
            return listAttributesForm.SelectedAttribute;
        }
      }
    }
    return (ConditionAttributeInfo) null;
  }
}
