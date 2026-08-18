// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ClipboardDataAdditionalInfo
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Дополнительные данные хранящиеся в буфере</summary>
[Serializable]
public class ClipboardDataAdditionalInfo
{
  /// <summary>Имя формата в буфере</summary>
  public static string ClipboardFormat = "NodesAdditionalInfo";
  /// <summary>Информация об узле</summary>
  public NodeClipboardInfo[] NodesInfo;
  private object tag;

  /// <summary>Конструктор</summary>
  /// <param name="nodesInfo">Информация об узле</param>
  public ClipboardDataAdditionalInfo(NodeClipboardInfo[] nodesInfo) => this.NodesInfo = nodesInfo;

  /// <summary>Доп информация</summary>
  public object Tag
  {
    get => this.tag;
    set => this.tag = value;
  }
}
