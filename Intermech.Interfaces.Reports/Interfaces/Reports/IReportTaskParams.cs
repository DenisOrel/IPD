// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.IReportTaskParams
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Параметры задачи генерации КТД</summary>
public interface IReportTaskParams
{
  /// <summary>
  /// Идентификатор версии объекта для которого генерируется КТД
  /// </summary>
  long ObjectId { get; }

  /// <summary>Идентификатор версии скрипта ЭС</summary>
  long ScriptObjId { get; }

  /// <summary>Идентификатор версии комплекта документов</summary>
  long PackageObjId { get; set; }

  /// <summary>Ид. архива для документов</summary>
  long ArchiveId { get; set; }

  /// <summary>Параметры задачи</summary>
  AttributeValues[] Attributes { get; set; }

  /// <summary>Режимы задачи генерации КТД</summary>
  ReportTaskMode TaskMode { get; set; }
}
