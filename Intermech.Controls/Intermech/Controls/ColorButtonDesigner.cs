
// Type: Intermech.Controls.ColorButtonDesigner
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.Windows.Forms.Design;


namespace Intermech.Controls;

internal class ColorButtonDesigner : ControlDesigner
{
  protected override void PreFilterProperties(IDictionary properties)
  {
    base.PreFilterProperties(properties);
    properties.Remove((object) "BackColor");
    properties.Remove((object) "FlatStyle");
    properties.Remove((object) "FlatAppearance");
    properties.Remove((object) "BackgroundImageLayout");
  }
}
