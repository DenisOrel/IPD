// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToNodeInTemplate
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ссылка на шаблон узла</summary>
[Serializable]
public class ReferenceToNodeInTemplate : ReferenceToNodeId
{
  /// <summary>Конструктор</summary>
  public ReferenceToNodeInTemplate()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToNodeInTemplate(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToNodeInTemplate(DocumentTreeNode ownerNode, string templateId)
    : base(ownerNode)
  {
    this.AssignNodeId(templateId);
  }

  /// <summary>База для ссылки на узел (для внутреннего использования)</summary>
  public override DocumentTreeNode NodeLinkBase
  {
    [DebuggerStepThrough] get
    {
      DocumentTreeNode nodeLinkBase = (DocumentTreeNode) null;
      if (this.OwnerNode != null && !this.OwnerNode.IsTemplate)
        nodeLinkBase = this.OwnerNode.TemplateRoot;
      if (nodeLinkBase == null)
        nodeLinkBase = this.OwnerNode;
      return nodeLinkBase;
    }
  }

  /// <summary>Найти элемент по ссылке</summary>
  /// <returns></returns>
  protected DocumentTreeNode FindNodeByRef()
  {
    DocumentTreeNode nodeLinkBase = this.NodeLinkBase;
    if (string.IsNullOrEmpty(this.NodeId) || nodeLinkBase == null)
      return (DocumentTreeNode) null;
    return this.nodeLink != null && this.nodeLink.Id == this.NodeId && nodeLinkBase.IdService == this.nodeLink.IdService ? this.nodeLink : nodeLinkBase.FindNode(this.NodeId);
  }

  /// <summary>Обновить связь</summary>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateLink(bool forceUpdate, bool updateUI, bool updateLayout)
  {
    this.AssignNodeLink(this.FindNodeByRef());
  }
}
