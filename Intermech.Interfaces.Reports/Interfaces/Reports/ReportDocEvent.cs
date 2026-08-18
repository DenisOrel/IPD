// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportDocEvent
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using Intermech.Interfaces.Document;
using Intermech.Interfaces.Expert;
using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Некий класс параметров</summary>
[Serializable]
public class ReportDocEvent : ReportDocBaseEvent
{
  /// <summary>Конструктор</summary>
  /// <param name="documentRecord">Информация по текущему документу вместе со статусом</param>
  /// <param name="traceInfo">Информация о трассировке</param>
  public ReportDocEvent(DocRecord documentRecord, byte[] traceInfo)
    : this(documentRecord, (ImDocumentData) null, traceInfo)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentRecord">Информация по текущему документу вместе со статусом</param>
  /// <param name="documentData">Данные документа</param>
  /// <param name="traceInfo">Информация о трассировке</param>
  public ReportDocEvent(DocRecord documentRecord, ImDocumentData documentData, byte[] traceInfo = null)
    : base(documentRecord)
  {
    this.DocumentData = documentData;
    this.TraceInfo = traceInfo;
  }

  /// <summary>Данные документа</summary>
  public ImDocumentData DocumentData { get; }

  /// <summary>Информация о трассировке</summary>
  public byte[] TraceInfo { get; }
}
