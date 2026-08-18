// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleAttributesProcessingService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Необязательный сервис для обслуживания задачи синхронизации атрибутов изделий.
/// </summary>
public interface IArticleAttributesProcessingService
{
  /// <summary>
  /// Позволяет обработать значения атрибутов изделия непосредственно перед синхронизацией значений между файлом документа и объектом изделия в базе IPS.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="workingSet">Рабочий набор атрибутов изделия, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов изделия, прочитанный из базы данных</param>
  /// <exception cref="T:ArgumentNullException">articleItem || workingSet || databaseSet</exception>
  void PreprocessAttributes(SectionEntity articleItem, ValueBag workingSet, ValueBag databaseSet);

  /// <summary>
  /// Позволяет обработать значения атрибутов изделия непосредственно после синхронизации значений между файлом документа и объектом изделия в базе IPS.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="workingSet">Рабочий набор атрибутов изделия, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов изделия, прочитанный из базы данных</param>
  /// <exception cref="T:ArgumentNullException">articleItem || workingSet || databaseSet</exception>
  void PostprocessAttributes(SectionEntity articleItem, ValueBag workingSet, ValueBag databaseSet);
}
