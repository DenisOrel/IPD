// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.CellNode
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

internal class CellNode : TreeNode
{
  public string Id { get; set; } = string.Empty;

  public bool IsOverriden { get; set; }

  public CellNode(string id, string name)
  {
    this.Id = id;
    this.Name = name;
    this.Text = "Графа: " + name;
  }

  public CellNode(string id)
  {
    this.Id = id;
    this.Name = id;
    this.Text = "Графа: " + id;
  }

  public override object Clone()
  {
    CellNode cellNode = new CellNode(this.Id, this.Name);
    cellNode.NodeFont = this.NodeFont;
    cellNode.IsOverriden = this.IsOverriden;
    foreach (object node in this.Nodes)
      cellNode.Nodes.Add(((TreeNode) node).Clone() as TreeNode);
    return (object) cellNode;
  }
}
