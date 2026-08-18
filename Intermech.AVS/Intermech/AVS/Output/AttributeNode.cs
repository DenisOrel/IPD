// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.AttributeNode
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

internal class AttributeNode : TreeNode
{
  public AttributeInfo AttributeInfo { get; }

  public AttributeNode(AttributeInfo attributeInfo, bool isDocRowAttribute = false)
  {
    this.AttributeInfo = attributeInfo;
    this.Name = attributeInfo.Name;
    this.Text = $"Атрибут: {this.AttributeInfo.Name} {(isDocRowAttribute ? "(запись)" : (attributeInfo.AttrSrc == FieldSource.Relation ? "(связь)" : (attributeInfo.AttrSrc == FieldSource.Object ? "(объект)" : "(графа)")))}";
  }

  public override string ToString() => $"[{this.AttributeInfo.Name}]";

  public override object Clone()
  {
    AttributeNode attributeNode = new AttributeNode(this.AttributeInfo);
    attributeNode.NodeFont = this.NodeFont;
    foreach (object node in this.Nodes)
      attributeNode.Nodes.Add(((TreeNode) node).Clone() as TreeNode);
    return (object) attributeNode;
  }
}
