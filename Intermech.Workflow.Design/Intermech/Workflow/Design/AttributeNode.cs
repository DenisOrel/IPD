// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AttributeNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class AttributeNode : TreeNode
{
  public readonly int AttributeID;

  public AttributeNode(string text, int attrID)
    : base(text)
  {
    this.AttributeID = attrID;
  }
}
