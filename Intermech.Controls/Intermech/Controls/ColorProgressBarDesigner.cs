
// Type: Intermech.Controls.ColorProgressBarDesigner
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections;
using System.Windows.Forms.Design;


namespace Intermech.Controls;

[Serializable]
internal class ColorProgressBarDesigner : ControlDesigner
{
  protected override void PostFilterProperties(IDictionary Properties)
  {
    Properties.Remove((object) "AllowDrop");
    Properties.Remove((object) "BackgroundImage");
    Properties.Remove((object) "ContextMenu");
    Properties.Remove((object) "FlatStyle");
    Properties.Remove((object) "Image");
    Properties.Remove((object) "ImageAlign");
    Properties.Remove((object) "ImageIndex");
    Properties.Remove((object) "ImageList");
    Properties.Remove((object) "Text");
    Properties.Remove((object) "TextAlign");
  }
}
