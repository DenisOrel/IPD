// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToNodeId
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ссылка на узел через идентификатор</summary>
[Serializable]
public abstract class ReferenceToNodeId : ReferenceToNode
{
  private string nodeId;

  /// <summary>Конструктор</summary>
  public ReferenceToNodeId()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToNodeId(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Пустая ссылка</summary>
  public override bool IsEmpty => this.nodeId != null && this.nodeId != "";

  /// <summary>Идентификатор узла на который ссылается ссылка</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_411")]
  [CustomDescription("Attribute.Interfaces.Document_412")]
  public virtual string NodeId
  {
    [DebuggerStepThrough] get => this.nodeId;
  }

  /// <summary>Нанзначить значение свойству NodeId</summary>
  /// <param name="value">Значение</param>
  protected virtual void AssignNodeId(string value)
  {
    if (value == "")
      value = (string) null;
    this.nodeId = value;
  }

  /// <summary>Установить связь с узлом</summary>
  /// <param name="nodeId">Идентификатор узла</param>
  /// <param name="updateLink">Обновить ссылку</param>
  public virtual void SetReference(string nodeId, bool updateLink)
  {
    this.AssignNodeLink((DocumentTreeNode) null);
    this.AssignNodeId(nodeId);
    if (!updateLink)
      return;
    this.UpdateLink(false, false);
  }

  /// <summary>Установить связь с узлом</summary>
  /// <param name="nodeLink">Узел</param>
  public virtual void SetReference(DocumentTreeNode nodeLink)
  {
    this.AssignNodeLink(nodeLink);
    if (nodeLink != null)
      this.AssignNodeId(nodeLink.Id);
    else
      this.AssignNodeId((string) null);
  }

  /// <summary>Установить значение NodeId согласно текущей связи</summary>
  public virtual void SetNodeIdFromLink()
  {
    if (this.NodeLink == null)
      return;
    this.AssignNodeId(this.NodeLink.Id);
  }

  /// <summary>Обновить связь</summary>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateLink(bool forceUpdate, bool updateUI, bool updateLayout)
  {
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
    DocumentTreeNode nodeLinkBase = this.NodeLinkBase;
    if (this.nodeId != null && nodeLinkBase != null)
    {
      if (this.nodeLink != null && this.nodeLink.Id == this.nodeId && nodeLinkBase.IdService == this.nodeLink.IdService)
        return;
      documentTreeNode = nodeLinkBase.FindNode(this.nodeId);
    }
    this.AssignNodeLink(documentTreeNode);
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    if (this.nodeId == null)
      return;
    xw.WriteAttributeString("nodeId", this.nodeId);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if ("nodeId" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.nodeId = readArgs.Reader.Value;
      return true;
    }
    return base.ReadFieldFromXml(readArgs);
  }

  /// <summary>Копировать данные</summary>
  /// <param name="src">Источник данных</param>
  /// <param name="saveText">Сохранять данные</param>
  public override void CopyData(ReferenceBase src, bool copyText = true)
  {
    base.CopyData(src, copyText);
    if (!(src is ReferenceToNodeId referenceToNodeId))
      return;
    this.nodeId = referenceToNodeId.nodeId;
  }
}
