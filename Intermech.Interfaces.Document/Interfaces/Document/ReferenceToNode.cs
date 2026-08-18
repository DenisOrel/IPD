// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToNode
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ссылка на узел документа</summary>
[Serializable]
public abstract class ReferenceToNode : ReferenceBase
{
  [NonSerialized]
  protected DocumentTreeNode nodeLink;

  /// <summary>Конструктор</summary>
  public ReferenceToNode()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToNode(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Связь подключена. Есть ссылки на объекты</summary>
  public override bool IsConnected => this.nodeLink != null;

  /// <summary>Узел с которым связана ссылка</summary>
  [Browsable(false)]
  public virtual DocumentTreeNode NodeLink
  {
    [DebuggerStepThrough] get => this.nodeLink;
  }

  /// <summary>База для ссылки на узел (для внутреннего использования)</summary>
  [Browsable(false)]
  public abstract DocumentTreeNode NodeLinkBase { get; }

  /// <summary>Разорвать связь</summary>
  public override void DisconnectLink() => this.AssignNodeLink((DocumentTreeNode) null);

  /// <summary>Назначить значение свойству NodeLink</summary>
  /// <param name="value">Значение</param>
  public virtual void AssignNodeLink(DocumentTreeNode value)
  {
    if (this.nodeLink == value)
      return;
    if (this.nodeLink != null)
      this.nodeLink.RemoveConnection(this);
    this.nodeLink = value;
    if (this.nodeLink == null)
      return;
    this.nodeLink.AddConnection(this);
  }
}
