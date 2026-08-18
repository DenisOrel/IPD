// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AdditionalContext
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Дополнительные параметры контекста сериализации</summary>
[Serializable]
public class AdditionalContext
{
  private ContextFlags flags;
  private DocumentTreeNode[] rootNodes;
  private object context;

  /// <summary>Конструктор</summary>
  /// <param name="flags">Флаги контекста</param>
  /// <param name="rootNodes">Стартовые узлы для сериализации</param>
  /// <param name="context">Дополнительные данные контекста</param>
  public AdditionalContext(ContextFlags flags, DocumentTreeNode[] rootNodes, object context)
  {
    this.flags = flags;
    this.rootNodes = rootNodes;
    this.context = context;
  }

  /// <summary>Конструктор</summary>
  /// <param name="flags">Флаги контекста</param>
  /// <param name="rootNodes">Корень сериализуемого дерева</param>
  public AdditionalContext(ContextFlags flags, DocumentTreeNode[] rootNodes)
  {
    this.flags = flags;
    this.rootNodes = rootNodes;
  }

  /// <summary>Флаги контекста</summary>
  public ContextFlags Flags
  {
    [DebuggerStepThrough] get => this.flags;
  }

  /// <summary>Корень сериализуемого дерева</summary>
  public DocumentTreeNode[] RootNodes
  {
    [DebuggerStepThrough] get => this.rootNodes;
  }

  /// <summary>Дополнительные данные контекста</summary>
  public object Context
  {
    [DebuggerStepThrough] get => this.context;
  }
}
