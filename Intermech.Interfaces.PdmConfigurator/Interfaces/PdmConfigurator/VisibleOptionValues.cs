// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.VisibleOptionValues
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Класс хранит коллекцию видимых значений опций конфигуратора составов IPS
/// </summary>
[Serializable]
public sealed class VisibleOptionValues : ICloneable, IAssignable, IStoreable
{
  /// <summary>
  /// ID видимых значений опций конфигуратора составов IPS.
  /// [Guid опции] =&gt; [Список ID видимых значений опции]
  /// </summary>
  public Dictionary<Guid, List<string>> Items = new Dictionary<Guid, List<string>>();
  /// <summary>
  /// Значения опций по умолчанию
  /// [Guid опции] =&gt; [Значение по умолчанию]
  /// </summary>
  public Dictionary<Guid, string> DefaultValues = new Dictionary<Guid, string>();
  /// <summary>
  /// Обязательность опций к заполнению при применении в объекте
  /// [Guid опции] =&gt; [true/false]
  /// </summary>
  public Dictionary<Guid, bool> Obligatory = new Dictionary<Guid, bool>();

  /// <summary>
  /// Создать пустую коллекцию видимых значений опций конфигуратора составов IPS
  /// </summary>
  public VisibleOptionValues()
  {
  }

  /// <summary>
  /// Создать коллекцию видимых значений опций конфигуратора составов IPS на основе указанной кодированной строки
  /// </summary>
  /// <param name="codedValue">Значение в виде кодированной строки</param>
  public VisibleOptionValues(string codedValue) => this.Assign((object) codedValue);

  /// <summary>
  /// Создать коллекцию видимых значений опций конфигуратора составов IPS на основе указанного объекта
  /// </summary>
  /// <param name="source">Объект-источник (коллекция видимых значений опций, кодированная строка, опция)</param>
  public VisibleOptionValues(object source) => this.Assign(source);

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.Items.Clear();
    this.DefaultValues.Clear();
    this.Obligatory.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник (коллекция видимых значений опций, кодированная строка, опция)</param>
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
      case VisibleOptionValues visibleOptionValues:
        this.Items.Clear();
        this.DefaultValues = new Dictionary<Guid, string>((IDictionary<Guid, string>) visibleOptionValues.DefaultValues);
        this.Obligatory = new Dictionary<Guid, bool>((IDictionary<Guid, bool>) visibleOptionValues.Obligatory);
        using (Dictionary<Guid, List<string>>.Enumerator enumerator = visibleOptionValues.Items.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<Guid, List<string>> current = enumerator.Current;
            this.Items.Add(current.Key, new List<string>((IEnumerable<string>) current.Value));
          }
          break;
        }
      case IDBConfiguratorOption configuratorOption:
        Guid objectGuid = configuratorOption.ObjectGUID;
        OptionValuesCollection optionValues = configuratorOption.OptionValues;
        if (this.Items.ContainsKey(objectGuid))
          this.Items.Remove(objectGuid);
        if (optionValues == null || optionValues.Count == 0)
          break;
        List<string> stringList = new List<string>();
        this.Items[objectGuid] = stringList;
        for (int index = 0; index < optionValues.Count; ++index)
          stringList.Add(optionValues[index].ID);
        break;
    }
  }

  /// <summary>Загрузить информацию из объекта/связи базы данных</summary>
  /// <param name="obj">Источник</param>
  /// <returns>true - информация загружена успешно, false - были ошибки</returns>
  public bool LoadFromObject(IDBAttributable obj)
  {
    this.Clear();
    if (obj == null)
      return false;
    IDBAttribute attributeById = obj.GetAttributeByID(Consts.attributeVisibleOptionValuesID);
    if (attributeById == null)
      return false;
    StringBuilder stringBuilder = new StringBuilder();
    if (attributeById.ValuesCount == 1)
    {
      stringBuilder.Append(DataSetProcessor.GetStringValue(attributeById.Value, string.Empty));
    }
    else
    {
      object[] values = attributeById.Values;
      if (values != null)
      {
        for (int index = 0; index < values.Length; ++index)
          stringBuilder.Append(DataSetProcessor.GetStringValue(values[index], string.Empty));
      }
    }
    this.Assign((object) stringBuilder.ToString());
    return true;
  }

  /// <summary>Записать информацию в указанный элемент базы данных</summary>
  /// <param name="obj">Элемент-назначение</param>
  /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
  public bool SaveToObject(IDBAttributable obj)
  {
    if (obj == null || !(obj is IDBObject dbObject))
      return false;
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, Consts.attributeVisibleOptionValuesID);
    if (attribute4ObjectType == null)
      return false;
    if (this.Obligatory.Count > 0)
    {
      foreach (KeyValuePair<Guid, bool> keyValuePair in this.Obligatory)
      {
        if (!this.Items.ContainsKey(keyValuePair.Key) && keyValuePair.Value)
          this.Items[keyValuePair.Key] = new List<string>();
      }
    }
    string str = this.ToString(Consts.attributeVisibleOptionValuesID);
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Consts.attributeVisibleOptionValuesID);
    List<string> stringList = StringsHelper.SplitString(str.ToString(), (int) attributeType.SizeType);
    IDBAttribute dbAttribute = dbObject.GetAttributeByID(Consts.attributeVisibleOptionValuesID);
    if (dbAttribute != null)
    {
      if (this.Items.Count == 0 || stringList.Count == 0)
      {
        if (attribute4ObjectType.Required == RequiredModes.Manual)
          dbAttribute.Delete(0L);
        return true;
      }
    }
    else
    {
      if (this.Items.Count == 0 || stringList.Count == 0)
        return false;
      if (attribute4ObjectType.Required == RequiredModes.Manual)
        dbAttribute = dbObject.Attributes.AddAttribute(Consts.attributeVisibleOptionValuesID, false);
    }
    if (dbAttribute == null)
      return false;
    dbAttribute.Values = (object[]) stringList.ToArray();
    return true;
  }

  /// <summary>
  /// Сверить список видимых значений опции с контейнером опции,
  /// удалить из списка значения, которых уже нет в опции
  /// </summary>
  /// <param name="option">Опция</param>
  public void SyncWithOption(OptionHolder option)
  {
    if (option == null)
      return;
    if (!this.Items.ContainsKey(option.OptionGuid))
    {
      this.AddVisibleValues(option);
    }
    else
    {
      List<string> stringList = this.Items[option.OptionGuid];
      for (int index = stringList.Count - 1; index >= 0; --index)
      {
        if (option.OptionValues.FindValue(stringList[index]) == null)
          stringList.RemoveAt(index);
      }
    }
  }

  /// <summary>
  /// Добавить из опции все значения как видимые, если нет других настроек
  /// </summary>
  /// <param name="option">Опция</param>
  public void AddVisibleValues(OptionHolder option)
  {
    if (option == null)
      return;
    if (this.Items.ContainsKey(option.OptionGuid))
      this.Items.Remove(option.OptionGuid);
    this.Items[option.OptionGuid] = new List<string>();
    for (int index = 0; index < option.OptionValues.Count; ++index)
    {
      if ((option.OptionValues[index].Flags & OptionValueFlags.Obsolete) == OptionValueFlags.None)
        this.Items[option.OptionGuid].Add(option.OptionValues[index].ID);
    }
  }

  /// <summary>
  /// Удалить из списка видимых значений излишек информации, касающийся указанной опции
  /// (например, если все значения опции добавлены как видимые, список надо удалить полностью)
  /// </summary>
  /// <param name="option">Опции</param>
  public void RemoveVisibleValues(OptionHolder option)
  {
    if (option == null || !this.Items.ContainsKey(option.OptionGuid) || this.Items[option.OptionGuid].Count != 0)
      return;
    this.Items.Remove(option.OptionGuid);
  }

  /// <summary>
  /// Проверить, является ли указанное значение опции видимым
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <param name="id">Идентификатор значения опции</param>
  /// <returns>true - значение видимо</returns>
  public bool GetVisibleOptionValue(Guid option, string id)
  {
    return !this.Items.ContainsKey(option) || this.Items[option].Count == 0 || this.Items[option].IndexOf(id) >= 0;
  }

  /// <summary>
  /// Установить видимость значения опции.
  /// Метод генерирует исключение, если из списка удаляется последний видимый элемент
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <param name="id">Идентификатор значения опции</param>
  /// <param name="visible">Видимость значения опции</param>
  public void SetVisibleOptionValue(Guid option, string id, bool visible)
  {
    if (visible)
    {
      if (!this.Items.ContainsKey(option))
        this.Items[option] = new List<string>();
      if (this.Items[option].IndexOf(id) >= 0)
        return;
      this.Items[option].Add(id);
    }
    else
    {
      if (!this.Items.ContainsKey(option))
        return;
      if (this.GetDefaultOptionValue(option) == id)
        throw new PdmConfiguratorExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_68"));
      if (this.Items[option].Count == 1 && this.Items[option].IndexOf(id) == 0)
        throw new PdmConfiguratorExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_69"));
      this.Items[option].Remove(id);
    }
  }

  /// <summary>
  /// Проверить, является ли указанная опция обязательной для заполнения
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>true - опция обязательна для заполнения</returns>
  public bool GetObligatoryOption(Guid option)
  {
    return this.Obligatory.ContainsKey(option) && this.Obligatory[option];
  }

  /// <summary>
  /// Установить/отменить для указанной опции обязательность заполнения
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <param name="obligatory">Обязательность заполнения опции</param>
  public void SetObligatoryOption(Guid option, bool obligatory)
  {
    if (this.Obligatory.ContainsKey(option) && !obligatory)
    {
      this.Obligatory.Remove(option);
    }
    else
    {
      if (!obligatory)
        return;
      this.Obligatory[option] = obligatory;
    }
  }

  /// <summary>
  /// Получить значение опции по умолчанию, если оно было назначено
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>ID значения опции по умолчанию, или пустая строка</returns>
  public string GetDefaultOptionValue(Guid option)
  {
    return this.DefaultValues.ContainsKey(option) ? this.DefaultValues[option] : string.Empty;
  }

  /// <summary>
  /// Установить/отменить для указанной опции значение по умолчанию
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <param name="value">Значение по умолчанию (пустая строка - удалить значение по умолчанию)</param>
  public void SetDefaultOptionValue(Guid option, string value)
  {
    if (this.DefaultValues.ContainsKey(option) && string.IsNullOrEmpty(value))
    {
      this.DefaultValues.Remove(option);
    }
    else
    {
      if (string.IsNullOrEmpty(value))
        return;
      this.DefaultValues[option] = this.GetVisibleOptionValue(option, value) ? value : throw new PdmConfiguratorExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_70"));
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
    string[] strArray = val.Split(Helper.Splitter, StringSplitOptions.RemoveEmptyEntries);
    if (strArray == null || strArray.Length <= 1)
      return;
    Guid key = Guid.Empty;
    List<string> stringList = (List<string>) null;
    for (int index = 1; index < strArray.Length; ++index)
    {
      string str1 = strArray[index];
      if (!string.IsNullOrEmpty(str1))
      {
        if ((int) str1[0] == (int) Helper.AsteriskChar)
          str1 = strArray[index].Substring(Helper.AsteriskString.Length);
        if (GuidHelper.IsGuid(str1))
        {
          key = new Guid(str1);
          stringList = (List<string>) null;
          if (!(key == Guid.Empty) && str1.Length != strArray[index].Length)
            this.Obligatory[key] = true;
        }
        else if (!(key == Guid.Empty))
        {
          if (stringList == null)
          {
            stringList = new List<string>();
            this.Items[key] = stringList;
          }
          string str2 = strArray[index];
          if (!string.IsNullOrEmpty(str2))
          {
            if ((int) str2[0] == (int) Helper.AsteriskChar)
            {
              str2 = str2.Substring(Helper.AsteriskString.Length);
              this.DefaultValues[key] = str2;
            }
            stringList.Add(str2);
          }
        }
      }
    }
  }

  /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
  /// <returns>Значение экземпляра класса в виде строки</returns>
  public string ToString(int attributeTypeID)
  {
    if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
      throw new ArgumentException();
    if (this.Items.Count == 0 && this.DefaultValues.Count == 0 && this.Obligatory.Count == 0)
      return string.Empty;
    Dictionary<Guid, bool> dictionary = new Dictionary<Guid, bool>();
    StringBuilder stringBuilder = new StringBuilder();
    foreach (KeyValuePair<Guid, List<string>> keyValuePair in this.Items)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(Helper.SplitterChar);
      if (this.Obligatory.ContainsKey(keyValuePair.Key) && this.Obligatory[keyValuePair.Key])
        stringBuilder.Append(Helper.AsteriskChar);
      stringBuilder.Append(keyValuePair.Key.ToString());
      for (int index = 0; index < keyValuePair.Value.Count; ++index)
      {
        if (index == 0)
          stringBuilder.Append(Helper.SplitterChar);
        if (this.DefaultValues.ContainsKey(keyValuePair.Key) && this.DefaultValues[keyValuePair.Key] == keyValuePair.Value[index])
          stringBuilder.Append(Helper.AsteriskChar);
        stringBuilder.Append(keyValuePair.Value[index]);
        if (index < keyValuePair.Value.Count - 1)
          stringBuilder.Append(Helper.SplitterChar);
      }
      dictionary[keyValuePair.Key] = true;
    }
    stringBuilder.Insert(0, Helper.SplitterChar);
    int num = stringBuilder.Length + 1;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeID);
    stringBuilder.Insert(0, (long) num <= attributeType.SizeType ? "0" : "1");
    return stringBuilder.ToString();
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is VisibleOptionValues visibleOptionValues))
      return false;
    int num = Helper.CompareDictionaries((IDictionary) this.Items, (IDictionary) visibleOptionValues.Items) ? 1 : 0;
    if (num != 0)
      Helper.CompareDictionaries((IDictionary) this.DefaultValues, (IDictionary) visibleOptionValues.DefaultValues);
    if (num == 0)
      return num != 0;
    Helper.CompareDictionaries((IDictionary) this.Obligatory, (IDictionary) visibleOptionValues.Obligatory);
    return num != 0;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    int count = this.Items.Count;
    int num1 = count.GetHashCode() << 24;
    count = this.DefaultValues.Count;
    int num2 = count.GetHashCode() << 16 /*0x10*/;
    int num3 = num1 ^ num2;
    count = this.Obligatory.Count;
    int hashCode = count.GetHashCode();
    return num3 ^ hashCode;
  }
}
