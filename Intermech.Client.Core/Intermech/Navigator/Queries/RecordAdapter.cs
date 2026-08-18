
// Type: Intermech.Navigator.Queries.RecordAdapter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Предназначен для преобразования результатов, полученных у источника данных
/// в результате выполнения запроса, к пригодному для использования виду.
/// </summary>
/// <remarks>
/// Количество полей данных, полученных в результате выполнения запроса, может отличаться
/// от количества виртуальных колонок, указанных в условии запроса:
///   1) если несколько виртуальных колонок навигатора отображаются в одно поле источника
///      данных, то количество полученных полей данных будет меньше количества запрошенных
///      виртуальных колонок;
///   2) для формирования унифицированных идентификаторов, присваиваемых элементам источника
///      данных, могут потребоваться значения полей, которые не были запрошены с помощью
///      виртуальных колонок. В этом случае количество полученных полей данных будет превышать
///      количество виртуальных колонок;
///   3) дополняет картину еще и то, что порядок полей у источника данных может отличаться от
///      порядка, указанного в коллекции виртуальных колонок.
/// Данных класс преобразует записи таким образом, чтобы значения полей следовали в порядке,
/// заданном коллекцией виртуальных колонок.
/// </remarks>
public class RecordAdapter
{
  private RecordMapping mapping;
  private int[] columnIndexes;
  private Dictionary<object, int> fieldIndexes;
  private static readonly Regex NewLineRegex = new Regex("[\n\r]", RegexOptions.Compiled);

  public RecordAdapter()
  {
  }

  /// <summary>Создает преобразователь записей.</summary>
  /// <param name="mapping">Схема соответствия виртуальных колонок и полей данных</param>
  /// <param name="fieldsOrder">Порядок следования полей источника данных</param>
  public RecordAdapter(RecordMapping mapping, object[] fieldsOrder)
  {
    if (mapping == null)
      throw new ArgumentNullException(sc_4307.ssp_imclient_4308(), LocalizationHolder.rm.GetString("Client.Core_386"));
    if (fieldsOrder == null)
      throw new ArgumentNullException(sc_4307.ssp_imclient_4309(), LocalizationHolder.rm.GetString("Client.Core_635"));
    this.mapping = mapping;
    this.columnIndexes = new int[this.mapping.Count];
    for (int index = 0; index < this.mapping.Count; ++index)
      this.columnIndexes[index] = Array.IndexOf<object>(fieldsOrder, this.mapping[index].Field);
    this.fieldIndexes = new Dictionary<object, int>();
    for (int index = 0; index < mapping.Fields.Length; ++index)
    {
      int num = Array.IndexOf<object>(fieldsOrder, mapping.Fields[index]);
      this.fieldIndexes.Add(mapping.Fields[index], num);
    }
  }

  /// <summary>
  /// Возвращает для указанного поля его индекс в записи,
  /// полученной у источника данных. Если поля в записи нет,
  /// то результатом будет -1.
  /// </summary>
  /// <param name="field">Идентификатор поля данных</param>
  /// <returns>Индекс поля данных в записи</returns>
  public virtual int GetFieldIndex(object field)
  {
    int fieldIndex;
    if (!this.fieldIndexes.TryGetValue(field, out fieldIndex))
      fieldIndex = -1;
    return fieldIndex;
  }

  /// <summary>
  /// Выполняет преобразование записи, полученной у источника данных,
  /// в пригодный для использования вид. При этом не учитываются
  /// индивидуальные преобразования значений полей.
  /// </summary>
  /// <param name="fieldValues">Исходная запись</param>
  /// <returns>Преобразованная запись</returns>
  public virtual object[] GetRawRecordValues(object[] fieldValues)
  {
    object[] rawRecordValues = new object[this.columnIndexes.Length];
    for (int index = 0; index < rawRecordValues.Length; ++index)
    {
      int columnIndex = this.columnIndexes[index];
      if (columnIndex >= 0)
        rawRecordValues[index] = fieldValues[columnIndex];
    }
    return rawRecordValues;
  }

  /// <summary>
  /// Выполняет преобразование записи, полученной у источника данных,
  /// в пригодный для использования вид.
  /// </summary>
  /// <param name="fieldValues">Исходная запись</param>
  /// <returns>Преобразованная запись</returns>
  public virtual object[] GetRecordValues(object[] fieldValues)
  {
    object[] recordValues = new object[this.columnIndexes.Length];
    for (int index = 0; index < recordValues.Length; ++index)
    {
      int columnIndex = this.columnIndexes[index];
      if (columnIndex >= 0)
      {
        INodeColumnTransform transform = this.mapping[index].Transform;
        object input = transform != null ? transform.Apply(fieldValues[columnIndex], this.mapping[index].Column, (object) this, fieldValues) : fieldValues[columnIndex];
        if (input is string)
          input = (object) RecordAdapter.NewLineRegex.Replace((string) input, string.Empty);
        recordValues[index] = input;
      }
    }
    return recordValues;
  }
}
