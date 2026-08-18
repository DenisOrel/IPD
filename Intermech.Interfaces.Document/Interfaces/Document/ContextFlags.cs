// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ContextFlags
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Флаги контекста сериализации</summary>
[Flags]
[Serializable]
public enum ContextFlags
{
  /// <summary>По умолчанию</summary>
  None = 0,
  /// <summary>Сериализовать дочерние узлы</summary>
  WithoutChilds = 1,
}
