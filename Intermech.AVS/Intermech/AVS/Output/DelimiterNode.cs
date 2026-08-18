// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.DelimiterNode
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

/// <summary>
/// Представляет разделитель в узле дерева схемы вывода атрибутов
/// </summary>
internal class DelimiterNode : TreeNode
{
  public string Delimiter { get; private set; }

  public DelimiterNode(string delimiter) => this.SetDelimiter(delimiter);

  public DelimiterNode(string delimiter, string name) => this.SetDelimiter(delimiter, name);

  public void SetDelimiter(string delimiter, string name = null)
  {
    this.Delimiter = delimiter;
    this.Name = name ?? DelimiterMapping.GetDefaultDescription(delimiter);
    this.Text = "Разделитель: " + this.Name;
  }

  public override string ToString() => this.Delimiter ?? "";

  public override object Clone()
  {
    DelimiterNode delimiterNode = new DelimiterNode(this.Delimiter, this.Name);
    delimiterNode.NodeFont = this.NodeFont;
    foreach (object node in this.Nodes)
      delimiterNode.Nodes.Add(((TreeNode) node).Clone() as TreeNode);
    return (object) delimiterNode;
  }
}
