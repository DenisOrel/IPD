// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportTaskParams
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Параметры задачи генерации КТД</summary>
[Serializable]
public class ReportTaskParams : IReportTaskParams
{
  /// <summary>Конструктор</summary>
  /// <param name="objectId"></param>
  /// <param name="scriptId"></param>
  /// <param name="packageObjId"></param>
  public ReportTaskParams(long objectId, long scriptId, long packageObjId = 0)
  {
    this.ObjectId = objectId;
    this.ScriptObjId = scriptId;
    this.PackageObjId = packageObjId;
  }

  /// <summary>
  /// Идентификатор версии объекта для которого генерируется КТД
  /// </summary>
  public long ObjectId { get; }

  /// <summary>Идентификатор версии скрипта ЭС</summary>
  public long ScriptObjId { get; }

  /// <summary>Идентификатор версии комплекта документов</summary>
  public long PackageObjId { get; set; }

  /// <summary>Ид. архива для документов</summary>
  public long ArchiveId { get; set; }

  /// <summary>Дополнительные параметры (атрибуты) задачи</summary>
  public AttributeValues[] Attributes { get; set; }

  /// <summary>Режим задачи генерации КТД</summary>
  public ReportTaskMode TaskMode { get; set; }
}
