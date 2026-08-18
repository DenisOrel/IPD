// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DatabaseConfiguratorNodeSorter
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.PropertyEditors;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class DatabaseConfiguratorNodeSorter : IComparer
{
  public int Compare(object x, object y)
  {
    if (!(x is TreeNode) || !(y is TreeNode))
      return 0;
    CustomFolder tag1 = ((TreeNode) x).Tag as CustomFolder;
    CustomFolder tag2 = ((TreeNode) y).Tag as CustomFolder;
    if (((TreeNode) x).Tag is AttributeGroupFolder && ((TreeNode) y).Tag is AttributeGroupFolder)
    {
      if (tag1 != null && tag2 != null)
      {
        if ((int) tag1.Id == -1)
          return -1;
        if ((int) tag2.Id == -1)
          return 1;
        if ((int) tag1.Id == -10)
          return -1;
        if ((int) tag2.Id == -10)
          return 1;
      }
      return string.Compare(((TreeNode) x).Text, ((TreeNode) y).Text);
    }
    if (!(((TreeNode) x).Tag is AllObjectTypesFolder) && !(((TreeNode) y).Tag is AllObjectTypesFolder))
      return string.Compare(((TreeNode) x).Text, ((TreeNode) y).Text);
    if (tag1 != null && tag2 != null)
    {
      if ((int) tag1.Id == -1)
        return -1;
      if ((int) tag2.Id == -1)
        return 1;
    }
    return string.Compare(((TreeNode) x).Text, ((TreeNode) y).Text);
  }
}
