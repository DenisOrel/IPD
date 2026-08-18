
// Type: Intermech.Navigator.SelectionView.InputObjectAttributeSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces.SelectionService;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

internal sealed class InputObjectAttributeSelector : ValueSelector<InputObjectAttribute>
{
  public override InputObjectAttribute GetValue(InputObjectAttribute origValue)
  {
    Guid[] arrAttrGuid;
    if (origValue == null)
      arrAttrGuid = new Guid[1]{ Guid.Empty };
    else
      arrAttrGuid = new Guid[1]{ origValue.AttributeGUID };
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false, arrAttrGuid))
    {
      if (origValue != null && origValue.ObjectGUID != Guid.Empty)
        attributesSelectDlg.LoadAttrDialogForObjectsTypes(origValue.ObjectGUID);
      if (attributesSelectDlg.ShowDialog() == DialogResult.OK)
      {
        if (attributesSelectDlg.SelectedAttributesGuid.Count > 0)
          return new InputObjectAttribute()
          {
            ObjectGUID = attributesSelectDlg.SelectedObjectGuid,
            AttributeGUID = attributesSelectDlg.SelectedAttributesGuid[0]
          };
      }
    }
    return (InputObjectAttribute) null;
  }
}
