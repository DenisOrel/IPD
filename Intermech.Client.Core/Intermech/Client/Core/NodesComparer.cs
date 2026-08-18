
// Type: Intermech.Client.Core.NodesComparer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Windows.Forms;


namespace Intermech.Client.Core;

internal sealed class NodesComparer : IComparer
{
  public int Compare(object x, object y)
  {
    return string.Compare((x as TreeNode).Text, (y as TreeNode).Text);
  }
}
