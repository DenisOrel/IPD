
// Type: Intermech.Client.Core.TreeNodeHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public static class TreeNodeHelper
{
  public static List<string> GetNodePath(this TreeNode node)
  {
    List<string> nodePath = new List<string>();
    if (node == null)
      return nodePath;
    for (; node != null; node = node.Parent)
    {
      if (node.Tag is FileItem tag)
      {
        if (tag.AttId == -1)
          nodePath.Insert(0, node.Text.GetRtfUnicodeEscapedString(true));
      }
      else
        nodePath.Insert(0, node.Text.GetRtfUnicodeEscapedString());
    }
    return nodePath;
  }
}
