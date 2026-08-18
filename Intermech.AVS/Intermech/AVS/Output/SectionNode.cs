// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.SectionNode
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

internal class SectionNode : TreeNode
{
  public SpecificationSectionInfo Section { get; private set; }

  public string SectionGuid => this.Section?.SectionGuid.ToString() ?? Guid.Empty.ToString();

  public bool IsOverriden { get; internal set; }

  public SectionNode(SpecificationSectionInfo section)
  {
    this.Section = section;
    this.Name = section?.Caption ?? string.Empty;
    this.Text = "Раздел: " + this.Name;
  }

  public SectionNode(string name)
  {
    this.Name = name;
    this.Text = name;
  }

  public override object Clone()
  {
    SectionNode sectionNode = new SectionNode(this.Section);
    sectionNode.NodeFont = this.NodeFont;
    sectionNode.IsOverriden = this.IsOverriden;
    foreach (object node in this.Nodes)
      sectionNode.Nodes.Add(((TreeNode) node).Clone() as TreeNode);
    return (object) sectionNode;
  }
}
