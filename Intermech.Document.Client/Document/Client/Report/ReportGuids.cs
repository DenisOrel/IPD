// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.ReportGuids
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>GUIDы для табличных отчетов</summary>
internal class ReportGuids
{
  /// <summary>Guid типа объектов "Табличные отчеты"</summary>
  public const string tableReportGuid = "cad00288-306c-11d8-b4e9-00304f19f545";
  /// <summary>Guid типа объектов "Общие табличные отчеты"</summary>
  public const string tableReportOwnGuid = "cad00289-306c-11d8-b4e9-00304f19f545";
  /// <summary>Guid типа объектов "Персональные табличные отчеты"</summary>
  public const string tableReportUserGuid = "cad0028a-306c-11d8-b4e9-00304f19f545";
  /// <summary>Guid типа объектов "Шаблоны табличных отчетов"</summary>
  public const string templateTableReportGuid = "cad00287-306c-11d8-b4e9-00304f19f545";
  /// <summary>атрибут "Шаблон табличного отчета"</summary>
  public const string attributeTemplateGuid = "cad0028b-306c-11d8-b4e9-00304f19f545";
  /// <summary>атрибут "Колонки табличного отчета"</summary>
  public const string attributeColumnsGuid = "cad0028c-306c-11d8-b4e9-00304f19f545";
  /// <summary>атрибут "Заголовок отчета"</summary>
  public const string attributeReportCaptionGuid = "cad0062d-306c-11d8-b4e9-00304f19f545";
  /// <summary>атрибут "Параметры табличного отчета"</summary>
  public const string attributeParametersGuid = "cad0062e-306c-11d8-b4e9-00304f19f545";
  /// <summary>атрибут "Тип генерируемого документа"</summary>
  public const string attributeGeneratedDocTypeGuid = "cad00116-306c-11d8-b4e9-00304f19f545";

  /// <summary>атрибут "Шаблон табличного отчета"</summary>
  public static int AttrTemplateId
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad0028b-306c-11d8-b4e9-00304f19f545");
    }
  }

  /// <summary>атрибут "Колонки табличного отчета"</summary>
  public static int AttributeColumnsId
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad0028c-306c-11d8-b4e9-00304f19f545");
    }
  }

  /// <summary>атрибут "Заголовок отчета"</summary>
  public static int AttributeReportCaptionId
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad0062d-306c-11d8-b4e9-00304f19f545");
    }
  }

  /// <summary>атрибут "Параметры табличного отчета"</summary>
  public static int AttributeParametersId
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad0062e-306c-11d8-b4e9-00304f19f545");
    }
  }

  /// <summary>атрибут "Тип генерируемого документа"</summary>
  public static int AttributeGeneratedDocTypeId
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad00116-306c-11d8-b4e9-00304f19f545");
    }
  }
}
