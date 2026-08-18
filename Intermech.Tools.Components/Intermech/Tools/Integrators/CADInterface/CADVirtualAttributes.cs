// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADVirtualAttributes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Components.Properties;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Содержит имена виртуальных атрибутов, встречающиеся в документах CAD-системы.
/// </summary>
public static class CADVirtualAttributes
{
  /// <summary>
  /// Имя атрибута, содержащее название типа сборочного чертежа - "Сборочный чертеж", "Габаритный чертеж" и др.
  /// В штампе чертежа оно выводится второй строкой под наименованием.
  /// </summary>
  public static string DocumentDesignType => CADDocumentResources.EMB_DesignTypeAttribute;

  /// <summary>
  /// Имя атрибута, в котором хранится код документа, представляющий собой суффикс из обозначения документа.
  /// </summary>
  public static string DocumentCode => CADDocumentResources.EMB_DocumentCode;

  /// <summary>
  /// Имя атрибута, содержащее название раздела спецификации для изделия и одновременно название типа изделий.
  /// </summary>
  public static string ArticleSection => CADDocumentResources.EMB_ArticleTypeAttribute;

  /// <summary>
  /// Название раздела "Материалы". Данное значение является одним из возможных значений для виртуального атрибута <see cref="M:ArticleSection" />.
  /// </summary>
  public static string MaterialsSectionName => CADDocumentResources.EMB_MaterialsSection;

  /// <summary>
  /// Имя атрибута, где хранится признак, что документ или конфигурация является виртуальной, создается "на лету" и существует только в памяти приложения.
  /// </summary>
  public static string IsVirtualObject => CADDocumentResources.EMB_IsVirtualObject;
}
