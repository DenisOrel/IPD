// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionValuesCollection
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Класс, хранящий коллекцию значений опции конфигуратора составов IPS
/// </summary>
[Serializable]
public sealed class OptionValuesCollection : 
  IEnumerable<OptionValue>,
  IEnumerable,
  ICloneable,
  IAssignable
{
  /// <summary>Уникальный счётчик для коллекции</summary>
  private long _counter;
  /// <summary>Значения опций конфигуратора составов IPS</summary>
  private List<OptionValue> _items = new List<OptionValue>();
  /// <summary>Словарик для быстрого поиска значений опций по их ID</summary>
  private Dictionary<string, OptionValue> _dict = new Dictionary<string, OptionValue>();

  /// <summary>Сгенерировать следующий уникальный идентификатор</summary>
  public string NextID
  {
    get
    {
      this.CorrectCounter();
      ++this._counter;
      return StringsHelper.IntToHex(this._counter);
    }
  }

  /// <summary>Количество значений опции</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this._items.Count;
  }

  /// <summary>
  /// Количество значений опции, которые не являются удалёнными
  /// </summary>
  public int EnabledCount
  {
    get
    {
      int enabledCount = 0;
      for (int index = 0; index < this._items.Count; ++index)
      {
        if ((this._items[index].Flags & OptionValueFlags.Obsolete) == OptionValueFlags.None)
          ++enabledCount;
      }
      return enabledCount;
    }
  }

  /// <summary>Управление элементами коллекции значений опции</summary>
  /// <param name="index">Индекс элемента коллекции</param>
  /// <returns>Значение опции с указанным индексом</returns>
  public OptionValue this[int index]
  {
    get => this._items[index];
    set => this.Replace(value, index);
  }

  /// <summary>
  /// Создать пустую коллекцию значений опций конфигуратора составов IPS
  /// </summary>
  public OptionValuesCollection()
  {
  }

  /// <summary>
  /// Создать коллекцию значений опций конфигуратора составов IPS на основе указанной кодированной строки
  /// </summary>
  /// <param name="codedValue">Значение в виде кодированной строки</param>
  public OptionValuesCollection(string codedValue) => this.Assign((object) codedValue);

  /// <summary>
  /// Создать коллекцию значений опций конфигуратора составов IPS на основе указанного объекта
  /// </summary>
  /// <param name="source">Объект-источник (коллекция значений опции, кодированная строка, опция)</param>
  public OptionValuesCollection(object source) => this.Assign(source);

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._items.Clear();
    this._dict.Clear();
    this._counter = 0L;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник (коллекция значений опции, кодированная строка, опция)</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case string _:
        this.FromString((string) source);
        break;
      case OptionValuesCollection valuesCollection:
        for (int index = 0; index < valuesCollection.Count; ++index)
        {
          OptionValue optionValue = valuesCollection[index].Clone() as OptionValue;
          this.Add(optionValue);
          this._dict[optionValue.ID] = optionValue;
        }
        this.CorrectCounter();
        break;
      case IDBConfiguratorOption configuratorOption:
        this.Assign((object) configuratorOption.OptionValues);
        break;
    }
  }

  /// <summary>Скорректировать значение счётчика</summary>
  private void CorrectCounter()
  {
    long val1 = 0;
    for (int index = 0; index < this.Count; ++index)
    {
      long int64 = StringsHelper.HexToInt64(this[index].ID);
      val1 = Math.Max(val1, int64);
    }
    this._counter = val1;
  }

  /// <summary>Добавить значение опции в коллекцию</summary>
  /// <param name="item">Значение опции</param>
  public void Add(OptionValue item)
  {
    if (this.IndexOf(item) >= 0)
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_65"), (object) item.ID));
    if (this.IndexOf(item.Code) >= 0)
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_66"), (object) item.Code));
    if (string.IsNullOrEmpty(item.ID))
      item.ID = this.NextID;
    this._items.Add(item);
    this._dict[item.ID] = item;
  }

  /// <summary>
  /// Вернуть индекс указанного значения (проверяется только ID значения, остальные поля игнорируются)
  /// </summary>
  /// <param name="item">Искомое значение</param>
  /// <returns>-1 или индекс найденного значения</returns>
  public int IndexOf(OptionValue item) => this._items.IndexOf(item);

  /// <summary>Найти в коллекции значение с указанным кодом</summary>
  /// <param name="valueCode">Код значения опции</param>
  /// <returns>-1 или индекс найденного значения</returns>
  public int IndexOf(string valueCode)
  {
    if (string.IsNullOrEmpty(valueCode))
      return -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].Code == valueCode)
        return index;
    }
    return -1;
  }

  /// <summary>Заменить элемент с указанным индексом</summary>
  /// <param name="item">Новое значение</param>
  /// <param name="index">Индекс заменяемого значения</param>
  public void Replace(OptionValue item, int index)
  {
    int num1 = this.IndexOf(item);
    if (num1 >= 0 && num1 != index)
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_65"), (object) item.ID));
    int num2 = this.IndexOf(item.Code);
    if (num2 >= 0 && num2 != index)
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_66"), (object) item.Code));
    if (string.IsNullOrEmpty(item.ID))
      item.ID = this.NextID;
    this._items[index] = item;
    this._dict[item.ID] = item;
  }

  /// <summary>Удалить из коллекции указанный элемент</summary>
  /// <param name="item">Удаляемый элемент</param>
  public void Remove(OptionValue item)
  {
    this._items.Remove(item);
    this._dict.Remove(item.ID);
  }

  /// <summary>Удалить из коллекции элемент с указанным индексом</summary>
  /// <param name="index">Индекс удаляемого элемента</param>
  public void RemoveAt(int index)
  {
    OptionValue optionValue = this[index];
    this._items.RemoveAt(index);
    this._dict.Remove(optionValue.ID);
  }

  /// <summary>Выполнить сортировку элементов списка</summary>
  public void Sort() => this._items.Sort();

  /// <summary>Выполнить сортировку элементов списка</summary>
  /// <param name="comparison">Способ сортировки</param>
  public void Sort(Comparison<OptionValue> comparison) => this._items.Sort(comparison);

  /// <summary>Получить массив значений опции</summary>
  /// <returns>Массив значений опции</returns>
  public OptionValue[] ToArray() => this._items.ToArray();

  /// <summary>Найти значение опции с указанным ID</summary>
  /// <param name="value">Guid значения</param>
  /// <returns>Значение опции или null</returns>
  public OptionValue FindValue(string value)
  {
    if (value == null)
      return (OptionValue) null;
    return !this._dict.ContainsKey(value) ? (OptionValue) null : this._dict[value];
  }

  public void Move(int index, int newIndex)
  {
    if (index < 0 || index >= this.Count)
      throw new ArgumentException();
    if (newIndex < 0 || newIndex >= this.Count)
      throw new ArgumentException();
    OptionValue optionValue = this._items[index];
    if (index < newIndex)
    {
      if (newIndex < this._items.Count - 1)
        this._items.Insert(newIndex + 1, optionValue);
      else
        this._items.Add(optionValue);
      this._items.RemoveAt(index);
    }
    else
    {
      this._items.RemoveAt(index);
      this._items.Insert(newIndex, optionValue);
    }
  }

  /// <summary>
  /// Заполнить экземпляр класса информацией из кодированной строки
  /// </summary>
  /// <param name="val">Кодированная строка</param>
  private void FromString(string val)
  {
    this.Clear();
    if (string.IsNullOrEmpty(val))
      return;
    string[] strArray = val.Split(Helper.Splitter, StringSplitOptions.None);
    if (strArray == null || strArray.Length < 2)
      return;
    StringsHelper.HexToInt32(strArray[0]);
    if (StringsHelper.HexToInt32(strArray[1]) <= 0)
      return;
    val = val.Substring(strArray[0].Length + strArray[1].Length + 2 * Helper.Splitter.Length);
    for (int length = val.Length; length > 0; length = val.Length)
    {
      OptionValue optionValue = new OptionValue();
      int startIndex = optionValue.FromString(val);
      this.Add(optionValue);
      val = startIndex <= 0 || startIndex >= val.Length ? string.Empty : val.Substring(startIndex);
    }
    this.CorrectCounter();
  }

  public string ToString(int attributeTypeID)
  {
    if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
      throw new ArgumentException();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(StringsHelper.IntToHex(this.Count));
    stringBuilder.Append(Helper.SplitterChar);
    int count = this.Count;
    for (int index = 0; index < count; ++index)
    {
      stringBuilder.Append(this[index].ToString());
      if (index < count - 1)
        stringBuilder.Append(Helper.SplitterChar);
    }
    stringBuilder.Insert(0, Helper.SplitterChar);
    int num = stringBuilder.Length + 1;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeID);
    stringBuilder.Insert(0, (long) num <= attributeType.SizeType ? "0" : "1");
    return stringBuilder.ToString();
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты идентичны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is OptionValuesCollection valuesCollection) || this.Count != valuesCollection.Count)
      return false;
    for (int index = 0; index < this.Count; ++index)
    {
      if (!this[index].Equals((object) valuesCollection[index]))
        return false;
    }
    return true;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.Count.GetHashCode();

  /// <summary>
  /// Метод вызывается для проверки содержимого на наличие ошибок. В случае ошибки
  /// будет сгенерировано исключение
  /// </summary>
  public void BeforeSave()
  {
    for (int index = 0; index < this.Count; ++index)
    {
      OptionValue optionValue = this[index];
      if (string.IsNullOrEmpty(optionValue.ID))
        optionValue.ID = this.NextID;
      if (string.IsNullOrEmpty(optionValue.Value) && (optionValue.Flags & OptionValueFlags.Obsolete) != OptionValueFlags.Obsolete)
        throw new PdmConfiguratorExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_67"));
    }
  }

  public IEnumerator<OptionValue> GetEnumerator()
  {
    return (IEnumerator<OptionValue>) this._items.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
