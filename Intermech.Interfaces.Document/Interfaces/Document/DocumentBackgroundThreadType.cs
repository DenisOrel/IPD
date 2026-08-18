// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentBackgroundThreadType
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Тип фонового потока документа</summary>
[Flags]
public enum DocumentBackgroundThreadType
{
  /// <summary>Нет потоков</summary>
  None = 0,
  /// <summary>Поток фоновой загрузки документа</summary>
  LoadThread = 1,
  /// <summary>Поток разбивки документа по страницам</summary>
  DistributeThread = 2,
}
