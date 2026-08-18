// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.NodeClipboardInfo
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Информация об узле хранящемся в буфере</summary>
[Serializable]
public class NodeClipboardInfo
{
  /// <summary>Тип узла</summary>
  public Type NodeType;

  /// <summary>Конструктор</summary>
  /// <param name="node">Узел</param>
  public NodeClipboardInfo(DocumentTreeNode node)
  {
    this.NodeType = node != null ? node.GetType() : throw new ArgumentNullException(nameof (node));
  }
}
