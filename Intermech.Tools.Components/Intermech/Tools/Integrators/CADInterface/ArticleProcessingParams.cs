// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ArticleProcessingParams
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Описывает сведения о конфигурации документа, доступные для выбора способа обработки изделия.
/// </summary>
public class ArticleProcessingParams
{
  private readonly string configurationName;
  private readonly ValueBag configurationAttributes;
  private int? documentType;

  /// <summary>Создает объект.</summary>
  /// <param name="configurationName">Имя конфигурации</param>
  /// <param name="configurationAttributes">Атрибуты конфигурации</param>
  public ArticleProcessingParams(string configurationName, ValueBag configurationAttributes)
  {
    if (string.IsNullOrEmpty(configurationName))
      throw new ArgumentException("Не задано имя конфигурации документа.", nameof (configurationName));
    if (configurationAttributes == null)
      throw new ArgumentNullException(nameof (configurationAttributes));
    this.configurationName = configurationName;
    this.configurationAttributes = configurationAttributes;
  }

  /// <summary>Добавляет в текущий объект информацию о документе.</summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  public void SetDocumentInfo(int documentType)
  {
    this.documentType = documentType != -1 ? new int?(documentType) : throw new ArgumentException("Не задан идентификатор типа документа.", nameof (documentType));
  }

  /// <summary>Возвращает иия конфигурации документа.</summary>
  public string ConfigurationName
  {
    [DebuggerStepThrough] get => this.configurationName;
  }

  /// <summary>
  /// Возвращает контейнер с атрибутами конфигурации документа.
  /// </summary>
  public ValueBag ConfigurationAttributes
  {
    [DebuggerStepThrough] get => this.configurationAttributes;
  }

  /// <summary>
  /// Возвращает идентификатор типа документа. Значение свойства может быть неопределено.
  /// </summary>
  public int? DocumentType
  {
    [DebuggerStepThrough] get => this.documentType;
  }
}
