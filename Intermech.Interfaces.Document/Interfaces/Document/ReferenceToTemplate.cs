// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToTemplate
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
public class ReferenceToTemplate : ReferenceToNodeInTemplate
{
  /// <summary>Конструктор</summary>
  public ReferenceToTemplate()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToTemplate(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToTemplate(DocumentTreeNode ownerNode, string templateId)
    : base(ownerNode, templateId)
  {
  }

  /// <summary>База для ссылки на узел (для внутреннего использования)</summary>
  public override DocumentTreeNode NodeLinkBase
  {
    [DebuggerStepThrough] get
    {
      return this.OwnerNode != null ? this.OwnerNode.TemplateRoot : (DocumentTreeNode) null;
    }
  }

  /// <summary>Обновить связь</summary>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateLink(bool forceUpdate, bool updateUI, bool updateLayout)
  {
    DocumentTreeNode node = this.FindNodeByRef();
    if (this.OwnerNode != null && node != null && !this.OwnerNode.CanUseNodeAsTemplate(node))
      node = (DocumentTreeNode) null;
    this.AssignNodeLink(node);
  }
}
