
// Type: Intermech.Controls.CustomComboBoxDesigner
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.Windows.Forms.Design;


namespace Intermech.Controls;

internal class CustomComboBoxDesigner : ParentControlDesigner
{
  protected override void PreFilterProperties(IDictionary properties)
  {
    base.PreFilterProperties(properties);
    properties.Remove((object) "Items");
    properties.Remove((object) "ItemHeight");
    properties.Remove((object) "MaxDropDownItems");
    properties.Remove((object) "DisplayMember");
    properties.Remove((object) "ValueMember");
    properties.Remove((object) "DropDownWidth");
    properties.Remove((object) "DropDownHeight");
    properties.Remove((object) "IntegralHeight");
    properties.Remove((object) "Sorted");
  }
}
