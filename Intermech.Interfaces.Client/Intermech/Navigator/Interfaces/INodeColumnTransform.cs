// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeColumnTransform
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Данный интерфейс предназначен для преобразования значений колонок в значения для отображения на экране,
/// например, для расшифровки идентификаторов пользователей в их имена и т.п.
/// ВНИМАНИЕ! Класс, реализующий этот интерфейс должен быть потокобезопасным.
/// </summary>
public interface INodeColumnTransform
{
  /// <summary>
  /// Возвращает тип значения, образуемого при выполнении преобразования.
  /// </summary>
  Type DataType { get; }

  /// <summary>
  /// Метод выполняет преобразование исходного значения колонки в новое значение, если оно требуется какими-либо
  /// правилами. Если содержимое колонки column.Contetns отлично от значения по умолчанию Text, либо у колонки метод
  /// трансформации задан как CellTransformationMode.ConvertToCellValue, то преобразование вернёт значение в виде
  /// экземпляра класса CellValue, в котором хранятся одновременно два значения – оригинальное и новое значения.
  /// </summary>
  /// <param name="sourceValue">Исходное значение колонки</param>
  /// <param name="column">Описание колонки</param>
  /// <param name="adapter">Ссылка на объект типа Intermech.Navigator.Queries.RecordAdapter</param>
  /// <param name="allValues">Все допустимые значения в строке с данными</param>
  /// <returns>Преобразованное значение колонки</returns>
  object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues);
}
