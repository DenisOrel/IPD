// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ChildNodesPositionExchanged_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события ChildNodesPositionExchanged</summary>
public class ChildNodesPositionExchanged_EventArgs : EventArgs
{
  /// <summary>Индек первого узла</summary>
  public int Index1;
  /// <summary>Индек второго узла</summary>
  public int Index2;

  /// <summary>Конструктор</summary>
  /// <param name="index1">Индек первого узла</param>
  /// <param name="index2">Индек второго узла</param>
  public ChildNodesPositionExchanged_EventArgs(int index1, int index2)
  {
    this.Index1 = index1;
    this.Index2 = index2;
  }
}
