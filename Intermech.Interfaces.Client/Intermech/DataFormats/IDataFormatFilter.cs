// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDataFormatFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс фильтра, позволяющий определить, удовлетворяют ли данные
/// определенного формата заданным в фильтре условиям. Основное применение
/// этого интерфейса - фильтрация иерархии дерева навигатора при обработке
/// событий обновления.
/// Примечание! Объекты, реализующие этот интерфейс должны также
/// реализовывать ICloneable. В определенных ситуациях навигатор создает
/// несколько копий одного и того же фильтра, которые должны изменяться
/// методами Join и Disjoin независимо друг от друга.
/// </summary>
public interface IDataFormatFilter
{
  /// <summary>
  /// Объединяет условия данного фильтра с условиями фильтра, переданного
  /// в качестве параметра.
  /// </summary>
  /// <param name="filter">Фильтр, условия которого должны быть объединены с условиями данного фильтра/// </param>
  /// <returns>Возращает true, если условия удалось объединить, инача - false</returns>
  bool Join(IDataFormatFilter filter);

  /// <summary>
  /// Исключает из условий данного фильтра условия фильтра, переданного в
  /// качестве параметра.
  /// </summary>
  /// <param name="filter">Фильтр, условия которого должны быть исключены</param>
  /// <returns>Возращает true, если условия удалось исключить, инача - false</returns>
  bool Disjoin(IDataFormatFilter filter);

  /// <summary>Проверяет, удовлетворяет ли объект условиям фильтра.</summary>
  /// <param name="data"></param>
  /// <returns></returns>
  bool CanPassData(object data);
}
