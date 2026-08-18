
// Type: Intermech.Navigator.Snapshots.CompositionCompareResultTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;


namespace Intermech.Navigator.Snapshots;

/// <summary>Преобразование значения флага CompositionCompareResult в человекочитабельный вид</summary>
public class CompositionCompareResultTransform
{
  [NotNull]
  private static readonly Lazy<INodeColumnTransform> _instance = new Lazy<INodeColumnTransform>((Func<INodeColumnTransform>) (() => (INodeColumnTransform) new CompositionCompareResultTransform.Implementation()), true);

  /// <summary>Singleton instance (thread safe)</summary>
  [NotNull]
  public static INodeColumnTransform Instance
  {
    [DebuggerStepThrough] get => CompositionCompareResultTransform._instance.Value;
  }

  /// <summary>Приватная реализация</summary>
  private class Implementation : INodeColumnTransform
  {
    [NotNull]
    private static readonly Type _dataType = typeof (string);
    [NotNull]
    [ItemNotNull]
    private static readonly string[] _values;

    /// <summary>Static constructor</summary>
    static Implementation()
    {
      Array values = Enum.GetValues(typeof (CompositionCompareResult));
      CompositionCompareResultTransform.Implementation._values = values.Cast<CompositionCompareResult>().Select<CompositionCompareResult, string>((Func<CompositionCompareResult, string>) (compareResult => EnumDescConverter.GetEnumDescription((Enum) compareResult))).ToArray<string>(values.Length);
    }

    /// <summary>Возвращает тип значения, образуемого при выполнении преобразования.</summary>
    [NotNull]
    public Type DataType => CompositionCompareResultTransform.Implementation._dataType;

    /// <summary>Метод выполняет преобразование исходного значения колонки в новое значение, если оно требуется какими-либо правилами. Если
    /// содержимое колонки column.Content отлично от значения по умолчанию Text, либо у колонки метод трансформации задан как
    /// CellTransformationMode.ConvertToCellValue, то преобразование вернёт значение в виде экземпляра класса CellValue, в
    /// котором хранятся одновременно два значения – оригинальное и новое значения.</summary>
    /// <param name="sourceValue">Исходное значение колонки</param>
    /// <param name="column">Описание колонки</param>
    /// <param name="adapter">Ссылка на объект типа Intermech.Navigator.Queries.RecordAdapter</param>
    /// <param name="allValues">Все допустимые значения в строке с данными</param>
    /// <returns>Преобразованное значение колонки</returns>
    [NotNull]
    public object Apply([CanBeNull] object sourceValue, [NotNull] NodeColumn column, [NotNull] object adapter, [NotNull, ItemNotNull] object[] allValues)
    {
      return sourceValue == null ? (object) string.Empty : (object) CompositionCompareResultTransform.Implementation._values[Convert.ToInt32(sourceValue)];
    }
  }
}
