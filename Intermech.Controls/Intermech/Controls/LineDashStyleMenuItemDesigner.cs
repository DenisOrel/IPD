
// Type: Intermech.Controls.LineDashStyleMenuItemDesigner
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.Windows.Forms.Design;


namespace Intermech.Controls;

internal class LineDashStyleMenuItemDesigner : ParentControlDesigner
{
  protected override void PreFilterProperties(IDictionary properties)
  {
    base.PreFilterProperties(properties);
    properties.Remove((object) "Text");
    properties.Remove((object) "TextAlign");
  }
}
