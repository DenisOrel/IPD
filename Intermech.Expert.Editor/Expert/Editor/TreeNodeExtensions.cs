// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TreeNodeExtensions
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Класс расширения TreeNode - перемещение элементов</summary>
public static class TreeNodeExtensions
{
  public static void MoveUp(this TreeNode node)
  {
    TreeNode parent = node.Parent;
    if (parent == null)
      return;
    int index = parent.Nodes.IndexOf(node);
    if (index <= 0)
      return;
    parent.Nodes.RemoveAt(index);
    parent.Nodes.Insert(index - 1, node);
    node.TreeView.SelectedNode = node;
  }

  public static void MoveDown(this TreeNode node)
  {
    TreeNode parent = node.Parent;
    if (parent == null)
      return;
    int index = parent.Nodes.IndexOf(node);
    if (index >= parent.Nodes.Count - 1)
      return;
    parent.Nodes.RemoveAt(index);
    parent.Nodes.Insert(index + 1, node);
    node.TreeView.SelectedNode = node;
  }

  public static void MoveFirst(this TreeNode node)
  {
    TreeNode parent = node.Parent;
    if (parent == null)
      return;
    int index = parent.Nodes.IndexOf(node);
    if (index <= 0)
      return;
    parent.Nodes.RemoveAt(index);
    parent.Nodes.Insert(0, node);
    node.TreeView.SelectedNode = node;
  }

  public static void MoveLast(this TreeNode node)
  {
    TreeNode parent = node.Parent;
    if (parent == null)
      return;
    int index = parent.Nodes.IndexOf(node);
    if (index >= parent.Nodes.Count - 1)
      return;
    parent.Nodes.RemoveAt(index);
    parent.Nodes.Add(node);
    node.TreeView.SelectedNode = node;
  }
}
