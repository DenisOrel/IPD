// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentViewMode
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Параметры просмотра документа</summary>
[Flags]
public enum DocumentViewMode
{
  Empty = 0,
  /// <summary>
  /// Записывать параметры в атрибуты документа (включает (отключает) передачу значений атрибутов при просмотре
  /// </summary>
  ShowDocumentReferences = 1,
  /// <summary>
  /// Записывать подписи в атрибуты документа (включает(отключает) передачу подписей в документ при просмотре)
  /// </summary>
  ShowSigns = 2,
  /// <summary>
  /// Оставить место в штампе для ручной подписи (оставляет пустыми подписи и даты. передается только фамилия подписавшего).
  /// </summary>
  ShowOnlySignName = 4,
  /// <summary>Запись контрольной суммы в файл</summary>
  ShowCRC = 8,
  Normal = ShowCRC | ShowSigns | ShowDocumentReferences, // 0x0000000B
}
