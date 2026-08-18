// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.VirtualGroupNode
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual;

internal class VirtualGroupNode : RectangleElement
{
  private readonly DocumentTreeNode _ownerTreeNode;

  public VirtualGroupNode([NotNull] DocumentTreeNode ownerTreeNode)
    : this()
  {
    this.parent = ownerTreeNode;
  }

  private VirtualGroupNode()
  {
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
    this.isVirtualNode = true;
    this.name = "Вложенные элементы";
  }

  public object Data { get; set; }

  public override DocumentTreeNode TemplateRoot => (DocumentTreeNode) null;

  public override bool IsTemplate => false;

  public override DocumentTreeNode FindTemplate(string templateId) => (DocumentTreeNode) null;

  public override void ConvertToHeader(bool removeData)
  {
  }

  public override string GetDefautCaption() => this.GetName();
}
