
// Type: Intermech.Controls.BrushSetupComboBoxDesigner
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.Windows.Forms.Design;


namespace Intermech.Controls;

internal class BrushSetupComboBoxDesigner : ParentControlDesigner
{
  protected override void PreFilterProperties(IDictionary properties)
  {
    base.PreFilterProperties(properties);
    properties.Remove((object) "AllowResizeDropDown");
    properties.Remove((object) "ControlSize");
    properties.Remove((object) "DropDownSizeMode");
    properties.Remove((object) "DropSize");
  }
}
