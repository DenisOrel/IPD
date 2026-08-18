// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportDocBaseEvent
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using Intermech.Interfaces.Expert;
using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Базовый класс параметров</summary>
[Serializable]
public class ReportDocBaseEvent : EventArgs
{
  /// <summary>Конструктор</summary>
  /// <param name="documentRecord"></param>
  public ReportDocBaseEvent(DocRecord documentRecord) => this.DocumentRecord = documentRecord;

  /// <summary>Информация по текущему документу вместе со статусом</summary>
  public DocRecord DocumentRecord { get; }
}
