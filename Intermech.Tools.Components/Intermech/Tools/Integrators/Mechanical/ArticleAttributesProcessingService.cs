// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleAttributesProcessingService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует сервис для обслуживания задачи синхронизации атрибутов изделий.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
public class ArticleAttributesProcessingService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : 
  MechanicalDriverService(driver, driverContext),
  IArticleAttributesProcessingService
{
  /// <summary>
  /// Позволяет обработать значения атрибутов изделия непосредственно перед синхронизацией значений между файлом документа и объектом изделия в базе IPS.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="workingSet">Рабочий набор атрибутов изделия, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов изделия, прочитанный из базы данных</param>
  /// <exception cref="T:ArgumentNullException">articleItem || workingSet || databaseSet</exception>
  public void PreprocessAttributes(
    SectionEntity articleItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (workingSet == null)
      throw new ArgumentNullException(nameof (workingSet));
    if (databaseSet == null)
      throw new ArgumentNullException(nameof (databaseSet));
    this.DoPreprocessAttributes(articleItem, workingSet, databaseSet);
  }

  /// <summary>
  /// Позволяет обработать значения атрибутов изделия непосредственно перед синхронизацией значений между файлом документа и объектом изделия в базе IPS.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="workingSet">Рабочий набор атрибутов изделия, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов изделия, прочитанный из базы данных</param>
  protected virtual void DoPreprocessAttributes(
    SectionEntity articleItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
  }

  /// <summary>
  /// Позволяет обработать значения атрибутов изделия непосредственно после синхронизации значений между файлом документа и объектом изделия в базе IPS.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="workingSet">Рабочий набор атрибутов изделия, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов изделия, прочитанный из базы данных</param>
  /// <exception cref="T:ArgumentNullException">articleItem || workingSet || databaseSet</exception>
  public void PostprocessAttributes(
    SectionEntity articleItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (workingSet == null)
      throw new ArgumentNullException(nameof (workingSet));
    if (databaseSet == null)
      throw new ArgumentNullException(nameof (databaseSet));
    this.DoPostprocessAttributes(articleItem, workingSet, databaseSet);
  }

  /// <summary>
  /// Позволяет обработать значения атрибутов изделия непосредственно после синхронизации значений между файлом документа и объектом изделия в базе IPS.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="workingSet">Рабочий набор атрибутов изделия, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов изделия, прочитанный из базы данных</param>
  protected virtual void DoPostprocessAttributes(
    SectionEntity articleItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
  }
}
