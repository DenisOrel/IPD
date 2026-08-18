// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentNodesClipboardData
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Узлы и данные для передачи через буфер</summary>
[Serializable]
public class DocumentNodesClipboardData
{
  /// <summary>Имя формата в буфере</summary>
  public static string ClipboardFormat = "ImDocumentNodes";
  /// <summary>Узлы которые копируются в буфер</summary>
  public DocumentTreeNode[] Nodes;
  /// <summary>Хэш коды родителей узлов</summary>
  public int[] ParentHashCodes;

  /// <summary>Конструктор</summary>
  /// <param name="nodes">Узлы, которые копируются в буфер</param>
  /// <param name="from">Откуда скопировано</param>
  public DocumentNodesClipboardData(DocumentTreeNode[] nodes)
  {
    this.Nodes = nodes != null ? nodes : throw new ArgumentNullException(nameof (nodes));
    this.ParentHashCodes = new int[nodes.Length];
    for (int index = 0; index < nodes.Length; ++index)
    {
      if (nodes[index] != null && nodes[index].Parent != null)
        this.ParentHashCodes[index] = nodes[index].Parent.GetHashCode();
    }
  }
}
