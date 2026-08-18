// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticlePhysicalPropertiesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис для работы с физическими свойствами изделия.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
internal sealed class CIArticlePhysicalPropertiesService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : 
  MechanicalDriverService(driver, driverContext),
  IArticlePhysicalPropertiesService
{
  /// <summary>
  /// Вычисляет и возвращает массу изделия. Метод может вернуть null, если вычисление массы не поддерживается.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <returns>Масса изделия или null</returns>
  public MeasuredValue CalculateMass(SectionEntity articleItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    MeasuredValue mass = articleItem.Sections.Get<CIArticleData>().Configuration.Mass;
    if (mass == null)
      return mass;
    SectionEntity hiddenInitialDocument = this.Driver.MechanicalOperations.Articles.TryGetHiddenInitialDocument(articleItem);
    if (hiddenInitialDocument == null)
      return mass;
    AnalyzerChangesSection.Mark(hiddenInitialDocument);
    return mass;
  }
}
