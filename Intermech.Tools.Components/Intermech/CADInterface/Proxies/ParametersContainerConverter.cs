// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ParametersContainerConverter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Реализует конвертер формата данных для именованных параметров COM-объекта CAD-интерфейса.
/// </summary>
public class ParametersContainerConverter
{
  private static readonly ParametersContainerConverter defaultInstance = new ParametersContainerConverter();
  private const short CADTrueValue = 1;
  private const short CADFalseValue = 0;

  /// <summary>
  /// Собирает и возвращает список именованных параметров COM-объекта CAD-интерфейса. Сборка производится из отдельных массивов
  /// составных частей параметров, полученных от CAD-интерфейса.
  /// </summary>
  /// <param name="namesArray">Массив имен параметров</param>
  /// <param name="valuesArray">Массив значений параметров</param>
  /// <param name="readOnlyFlagsArray">Массив флагов read-only. Значение аргумента может быть не задано и равно null</param>
  /// <param name="skipNullValues">Признак, позволяющий исключить из результата работы метода параметры, чье значение равно null</param>
  /// <returns>Список именованных параметров COM-объекта CAD-интерфейса</returns>
  /// <exception cref="T:ArgumentNullException">namesArray || valuesArray</exception>
  /// <exception cref="T:ArgumentException">Для массива valuesArray или readOnlyFlagsArray не равна длине массива namesArray</exception>
  public List<ValueRecord> ToParameters(
    string[] namesArray,
    object[] valuesArray,
    short[] readOnlyFlagsArray,
    bool skipNullValues)
  {
    if (namesArray == null)
      throw new ArgumentNullException(nameof (namesArray));
    if (valuesArray == null)
      throw new ArgumentNullException(nameof (valuesArray));
    if (valuesArray.Length != namesArray.Length)
      throw new ArgumentException("Длина valuesArray должна совпадать с длиной namesArray.", nameof (valuesArray));
    if (readOnlyFlagsArray != null && readOnlyFlagsArray.Length != namesArray.Length)
      throw new ArgumentException("Длина readOnlyFlagsArray должна совпадать с длиной namesArray.", nameof (readOnlyFlagsArray));
    List<ValueRecord> parameters = new List<ValueRecord>(namesArray.Length);
    for (int index = 0; index < namesArray.Length; ++index)
    {
      string names = namesArray[index];
      if (!string.IsNullOrEmpty(names) && !string.IsNullOrEmpty(TextServices.Trim(names)))
      {
        object values = valuesArray[index];
        bool flag = values != null;
        if (flag || !skipNullValues)
        {
          ValueRecord valueRecord = new ValueRecord((StringKey) names, flag ? values : (object) TypedNull.String);
          if (readOnlyFlagsArray != null)
            valueRecord.Flags[NamedFlags.ReadOnly] = this.ConvertFlag(readOnlyFlagsArray[index]);
          parameters.Add(valueRecord);
        }
      }
    }
    return parameters;
  }

  /// <summary>
  /// Разбирает коллекцию именованных параметров COM-объекта CAD-интерфейса в отдельные массивы имен и значений.
  /// </summary>
  /// <param name="parameters">Коллекция именованных параметоров</param>
  /// <param name="namesArray">Массив имен параметров</param>
  /// <param name="valuesArray">Массив значений параметров</param>
  /// <exception cref="T:ArgumentNullException">parameters</exception>
  public void ToNamesAndValues(
    ICollection<ValueRecord> parameters,
    out string[] namesArray,
    out object[] valuesArray)
  {
    namesArray = parameters != null ? new string[parameters.Count] : throw new ArgumentNullException(nameof (parameters));
    valuesArray = new object[parameters.Count];
    int index = 0;
    foreach (ValueRecord parameter in (IEnumerable<ValueRecord>) parameters)
    {
      this.ValidateParameterBeforeWrite(parameter);
      namesArray[index] = (string) parameter.Key;
      valuesArray[index] = parameter.Value;
      ++index;
    }
  }

  /// <summary>
  /// Разбирает именованный параметр COM-объекта CAD-интерфейса в отдельные массивы имен и значений, состоящие из одного элемента.
  /// </summary>
  /// <param name="parameter">Именованный параметр</param>
  /// <param name="namesArray">Массив имен параметров из одного элемента</param>
  /// <param name="valuesArray">Массив значений параметров из одного элемента</param>
  /// <exception cref="T:ArgumentNullException">parameter</exception>
  public void ToNamesAndValues(
    ValueRecord parameter,
    out string[] namesArray,
    out object[] valuesArray)
  {
    if (parameter == null)
      throw new ArgumentNullException(nameof (parameter));
    this.ValidateParameterBeforeWrite(parameter);
    namesArray = new string[1];
    namesArray[0] = (string) parameter.Key;
    valuesArray = new object[1];
    valuesArray[0] = parameter.Value;
  }

  public void ValidateParameterBeforeWrite(ValueRecord parameter)
  {
    if (parameter == null)
      throw new ArgumentNullException(nameof (parameter));
    if (parameter.IsNull)
      throw new ApplicationProxyException($"Невозможно записать параметр '{parameter.Key}' в объект CAD-интерфейса, так как запись null-значений не поддерживается.");
  }

  public void ToValuesAndReadOnlyFlags(
    IList<string> names,
    IEnumerable<ValueRecord> parameters,
    out object[] valuesArray,
    out short[] readOnlyFlagsArray)
  {
    if (names == null)
      throw new ArgumentNullException(nameof (names));
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    valuesArray = new object[names.Count];
    readOnlyFlagsArray = new short[names.Count];
    for (int index = 0; index < names.Count; ++index)
    {
      StringKey nameKey = new StringKey(names[index]);
      ValueRecord valueRecord = CollectionUtils.Find<ValueRecord>(parameters, (Predicate<ValueRecord>) (item => item.Key == nameKey));
      if (valueRecord != null)
      {
        valuesArray[index] = valueRecord.Value;
        readOnlyFlagsArray[index] = this.ConvertFlag(valueRecord.Flags[NamedFlags.ReadOnly]);
      }
    }
  }

  /// <summary>
  /// Преобразует значение флага в формат на стороне CAD-интерфейса.
  /// </summary>
  /// <param name="flagValue">Значение флага</param>
  /// <returns>Значение флага в формате, понятном для CAD-интерфейса</returns>
  public short ConvertFlag(bool flagValue) => !flagValue ? (short) 0 : (short) 1;

  /// <summary>Преобразует значение флага в формат на стороне IPS.</summary>
  /// <param name="flagValue">Значение флага</param>
  /// <returns>Значение флага в формате, понятном IPS</returns>
  public bool ConvertFlag(short flagValue) => flagValue != (short) 0;

  /// <summary>
  /// Возвращает экземпляр объекта, используемый по умолчанию.
  /// Реализация этого экземпляра объекта является thread safe.
  /// </summary>
  public static ParametersContainerConverter Default
  {
    [DebuggerStepThrough] get => ParametersContainerConverter.defaultInstance;
  }
}
