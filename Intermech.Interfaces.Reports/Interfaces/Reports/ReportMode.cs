// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportMode
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Режим генерации комплекта документов</summary>
public enum ReportMode
{
  /// <summary>Создание нового КТД</summary>
  Create,
  /// <summary>Создание версии КТД</summary>
  CreateVersion,
  /// <summary>Обновление КТД</summary>
  Update,
  /// <summary>
  /// Обновление существующего, если найден или же создание нового
  /// </summary>
  CreateOrUpdate,
}
