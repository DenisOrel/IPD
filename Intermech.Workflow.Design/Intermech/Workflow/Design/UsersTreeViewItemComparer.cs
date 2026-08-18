// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.UsersTreeViewItemComparer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class UsersTreeViewItemComparer : IComparer
{
  public int Compare(object x, object y)
  {
    int num1 = 0;
    int num2 = 0;
    if (x is GroupNode)
      num1 = -1;
    if (y is GroupNode)
      num2 = -1;
    if (x is AllUsersNode)
      num1 = -100;
    if (y is AllUsersNode)
      num2 = -100;
    return num1 != num2 ? num1 - num2 : string.Compare((x as TreeNode).Text, (y as TreeNode).Text);
  }
}
