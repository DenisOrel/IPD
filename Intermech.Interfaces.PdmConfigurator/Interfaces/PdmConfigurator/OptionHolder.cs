// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionHolder
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Класс, позволяющий сохранить содержимое ключевых полей объектов типа "Опции"
/// </summary>
[Serializable]
public sealed class OptionHolder : 
  ICloneable,
  IAssignable,
  IComparable,
  IComparable<OptionHolder>,
  IStoreable
{
  /// <summary>Объект для синхронизации</summary>
  private object syncRoot = new object();
  /// <summary>Идентификатор версии опции</summary>
  public long OptionObjectID;
  /// <summary>Категория опции</summary>
  public long OptionCategory;
  /// <summary>Guid опции</summary>
  public Guid OptionGuid = Guid.Empty;
  /// <summary>Заголовок опции</summary>
  public string OptionCaption = string.Empty;
  /// <summary>Код опции</summary>
  public string OptionCode = string.Empty;
  /// <summary>Примечание</summary>
  public string OptionDescription = string.Empty;
  /// <summary>
  /// Тип данных опции. Допускаются значения ftString, ftInteger, ftDouble, ftDateTime, ftBoolean
  /// </summary>
  public FieldTypes OptionDataType = FieldTypes.ftString;
  /// <summary>Флажки опции</summary>
  public OptionFlags OptionFlags;
  /// <summary>Коллекция значений опции</summary>
  public OptionValuesCollection OptionValues = new OptionValuesCollection();

  /// <summary>Создать пустой экземпляр класса</summary>
  public OptionHolder()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public OptionHolder(object source) => this.Assign(source);

  /// <summary>
  /// Получение списка колонок, необходимых для получения информации об опциях из коллекции объектов
  /// </summary>
  /// <returns>Список колонок, необходимых для получения информации об опциях из коллекции объектов</returns>
  internal static List<ColumnDescriptor> GetSelectColumns()
  {
    return new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Consts.attributeOptionCodeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cad00021-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Consts.attributeOptionDataTypeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Consts.attributeOptionFlagsID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Consts.attributeOptionValuesID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Consts.attributeCategoryLinkID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    };
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    lock (this.syncRoot)
    {
      this.OptionObjectID = 0L;
      this.OptionCategory = Consts.objectNoCategoryID;
      this.OptionGuid = Guid.Empty;
      this.OptionCaption = string.Empty;
      this.OptionCode = string.Empty;
      this.OptionDescription = string.Empty;
      this.OptionDataType = FieldTypes.ftString;
      this.OptionFlags = OptionFlags.None;
      this.OptionValues.Clear();
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case OptionHolder optionHolder:
        lock (this.syncRoot)
        {
          this.OptionObjectID = optionHolder.OptionObjectID;
          this.OptionCategory = optionHolder.OptionCategory;
          this.OptionGuid = optionHolder.OptionGuid;
          this.OptionCaption = optionHolder.OptionCaption;
          this.OptionCode = optionHolder.OptionCode;
          this.OptionDescription = optionHolder.OptionDescription;
          this.OptionDataType = optionHolder.OptionDataType;
          this.OptionFlags = optionHolder.OptionFlags;
          this.OptionValues.Assign((object) optionHolder.OptionValues);
          break;
        }
      case IDBConfiguratorOption configuratorOption1:
        lock (this.syncRoot)
        {
          this.OptionObjectID = configuratorOption1.ObjectID;
          this.OptionCategory = configuratorOption1.OptionCategory;
          this.OptionGuid = configuratorOption1.ObjectGUID;
          this.OptionCaption = configuratorOption1.Caption;
          this.OptionCode = configuratorOption1.OptionCode;
          this.OptionDescription = configuratorOption1.OptionDescription;
          this.OptionDataType = configuratorOption1.OptionDataType;
          this.OptionFlags = configuratorOption1.OptionFlags;
          this.OptionValues.Assign((object) configuratorOption1.OptionValues);
          break;
        }
      case DataRow row:
        lock (this.syncRoot)
        {
          this.OptionObjectID = DataSetProcessor.GetInt64Value(row, "cad00029-306c-11d8-b4e9-00304f19f545", 0L);
          this.OptionCategory = DataSetProcessor.GetInt64Value(row, "cad015a4-306c-11d8-b4e9-00304f19f545", Consts.objectNoCategoryID);
          this.OptionGuid = new Guid(DataSetProcessor.GetStringValue(row, "cad00130-306c-11d8-b4e9-00304f19f545", Guid.Empty.ToString()));
          this.OptionCaption = DataSetProcessor.GetStringValue(row, "cad00047-306c-11d8-b4e9-00304f19f545", string.Empty);
          this.OptionCode = DataSetProcessor.GetStringValue(row, "cad015a5-306c-11d8-b4e9-00304f19f545", string.Empty);
          this.OptionDescription = DataSetProcessor.GetStringValue(row, "cad00021-306c-11d8-b4e9-00304f19f545", string.Empty);
          this.OptionDataType = (FieldTypes) DataSetProcessor.GetInt64Value(row, "cad015aa-306c-11d8-b4e9-00304f19f545", 1L);
          this.OptionFlags = (OptionFlags) DataSetProcessor.GetInt64Value(row, "cad015ad-306c-11d8-b4e9-00304f19f545", 0L);
        }
        string stringValue = DataSetProcessor.GetStringValue(row, "cad015a2-306c-11d8-b4e9-00304f19f545", string.Empty);
        if (stringValue.IndexOf("0|") == 0)
        {
          lock (this.syncRoot)
          {
            this.OptionValues.Assign((object) stringValue);
            break;
          }
        }
        lock (this.syncRoot)
        {
          if (row.Table.ExtendedProperties[(object) "IUserSession"] is IUserSession extendedProperty)
          {
            if (extendedProperty.GetObject(this.OptionObjectID, false) is IDBConfiguratorOption configuratorOption)
            {
              this.OptionValues.Assign((object) configuratorOption.OptionValues);
              break;
            }
            this.OptionValues.Assign((object) stringValue);
            break;
          }
          this.OptionValues.Assign((object) stringValue);
          break;
        }
    }
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.OptionGuid.GetHashCode();

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты идентичны</returns>
  public override bool Equals(object obj)
  {
    OptionHolder optionHolder = obj as OptionHolder;
    lock (this.syncRoot)
      return optionHolder != null && this.OptionObjectID == optionHolder.OptionObjectID && this.OptionCategory == optionHolder.OptionCategory && this.OptionGuid.Equals(optionHolder.OptionGuid) && this.OptionCaption == optionHolder.OptionCaption && this.OptionCode == optionHolder.OptionCode && this.OptionDataType == optionHolder.OptionDataType && this.OptionFlags == optionHolder.OptionFlags && this.OptionDescription == optionHolder.OptionDescription && this.OptionValues.Equals((object) optionHolder.OptionValues);
  }

  /// <summary>Получить строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"[{this.OptionObjectID}] [{this.OptionCode}] {this.OptionCaption}";
  }

  /// <summary>Получить значение опции</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции</returns>
  private OptionValue GetOptionValue(string valueID)
  {
    lock (this.syncRoot)
      return this.OptionValues.FindValue(valueID) ?? throw new PdmConfiguratorTypeCastExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_60"), (object) valueID));
  }

  /// <summary>Получить значение опции в виде Int64-числа</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде Int64-числа или исключение,
  /// если тип данных значения опции не совместим с Int64 или не найдено значение опции</returns>
  public long GetAsInt64(string valueID)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    long result = 0;
    if (!long.TryParse(optionValue.Value, out result))
      throw new PdmConfiguratorTypeCastExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_61"), (object) optionValue.Value));
    return result;
  }

  /// <summary>Установить значение опции в виде Int64-числа</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде строки, которая будет преобразована в значение типа Int64</param>
  /// <returns>true - значение успешно преобразовано, false - ошибочная строка, исключение,
  /// если тип данных значения опции не совместим с Int64 или не найдено значение опции</returns>
  public bool SetAsInt64(string valueID, string value)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    if (optionValue == null)
      return false;
    long result = 0;
    if (!long.TryParse(value, out result))
      return false;
    optionValue.Value = result.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  /// <summary>Получить значение опции в виде Double-числа</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде Double-числа или исключение,
  /// если тип данных значения опции не совместим с Double или не найдено значение опции</returns>
  public double GetAsDouble(string valueID)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    double result = 0.0;
    if (!double.TryParse(optionValue.Value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result))
      throw new PdmConfiguratorTypeCastExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_62"), (object) optionValue.Value));
    return result;
  }

  /// <summary>Установить значение опции в виде Double-числа</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде строки, которая будет преобразована в значение типа Double</param>
  /// <returns>true - значение успешно преобразовано, false - ошибочная строка, исключение,
  /// если тип данных значения опции не совместим с Double или не найдено значение опции</returns>
  public bool SetAsDouble(string valueID, string value)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    if (optionValue == null)
      return false;
    double result = 0.0;
    if (!double.TryParse(value, out result))
      return false;
    optionValue.Value = result.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  /// <summary>Получить значение опции в виде DateTime</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде DateTime или исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public DateTime GetAsDateTime(string valueID)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    DateTime result = DateTime.MinValue;
    if (!DateTime.TryParse(optionValue.Value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
      throw new PdmConfiguratorTypeCastExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_63"), (object) optionValue.Value));
    return result;
  }

  /// <summary>Установить значение опции в виде DateTime-числа</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде строки, которая будет преобразована в значение типа DateTime</param>
  /// <returns>true - значение успешно преобразовано, false - ошибочная строка, исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public bool SetAsDateTime(string valueID, string value)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    if (optionValue == null)
      return false;
    DateTime result = DateTime.MinValue;
    if (!DateTime.TryParse(value, out result))
      return false;
    optionValue.Value = result.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  /// <summary>Установить значение опции в виде DateTime-числа</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде строки, которая будет преобразована в значение типа DateTime</param>
  /// <returns>true - значение успешно преобразовано, false - ошибочная строка, исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public bool CacheSetAsDateTime(string valueID, string value)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    if (optionValue == null)
      return false;
    DateTime result = DateTime.MinValue;
    if (!DateTime.TryParse(value, out result))
      return false;
    optionValue.Value = result.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  /// <summary>Получить значение опции в виде Boolean</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде Boolean или исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public bool GetAsBoolean(string valueID)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    bool result = false;
    if (!bool.TryParse(optionValue.Value, out result))
      throw new PdmConfiguratorTypeCastExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_64"), (object) optionValue.Value));
    return result;
  }

  /// <summary>Установить значение опции в виде Boolean</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде Boolean</param>
  /// <returns>true - значение успешно установлено, false - значение не найдено, исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public bool SetAsBoolean(string valueID, bool value)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    if (optionValue == null)
      return false;
    optionValue.Value = value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  /// <summary>Получить значение опции в виде String</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде String или исключение,
  /// если не найдено значение опции</returns>
  public string GetAsString(string valueID) => this.GetOptionValue(valueID).Value;

  /// <summary>Задать значение опции в виде String</summary>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде String</param>
  /// <returns>true - значение успешно установлено, false - ошибка, исключение,
  /// если не найдено значение опции</returns>
  public bool SetAsString(string valueID, string value)
  {
    OptionValue optionValue = this.GetOptionValue(valueID);
    if (optionValue == null)
      return false;
    optionValue.Value = value;
    return true;
  }

  /// <summary>
  /// Создать новое пустое значение опции (не добавляя его в коллекцию значений).
  /// Поля значения будут заполнены согласно текущему типу данных опции
  /// </summary>
  /// <returns>Новое пустое значение опции</returns>
  public OptionValue NewValue()
  {
    OptionValue optionValue = new OptionValue(string.Empty, string.Empty, string.Empty, string.Empty, Guid.Empty, OptionValueFlags.None, Guid.Empty, DateTime.UtcNow);
    switch (this.OptionDataType)
    {
      case FieldTypes.ftInteger:
      case FieldTypes.ftDouble:
        optionValue.Value = "0";
        break;
      case FieldTypes.ftDateTime:
        optionValue.Value = DateTime.Now.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case FieldTypes.ftBoolean:
        optionValue.Value = bool.TrueString;
        break;
    }
    return optionValue;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as OptionHolder);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(OptionHolder other)
  {
    if (other == null)
      return 1;
    lock (this.syncRoot)
      return this.OptionCaption.CompareTo(other.OptionCaption);
  }

  /// <summary>Загрузить информацию из объекта/связи базы данных</summary>
  /// <param name="obj">Источник</param>
  /// <returns>true - информация загружена успешно, false - были ошибки</returns>
  public bool LoadFromObject(IDBAttributable obj)
  {
    if (!(obj is IDBConfiguratorOption source))
      return false;
    this.Assign((object) source);
    return true;
  }

  /// <summary>Записать информацию в указанный элемент базы данных</summary>
  /// <param name="obj">Элемент-назначение</param>
  /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
  public bool SaveToObject(IDBAttributable obj)
  {
    if (!(obj is IDBConfiguratorOption configuratorOption))
      return false;
    this.BeforeSave();
    lock (this.syncRoot)
    {
      if (configuratorOption.OptionDataType != this.OptionDataType)
        configuratorOption.OptionValues = new OptionValuesCollection();
      configuratorOption.Caption = this.OptionCaption;
      configuratorOption.OptionCategory = this.OptionCategory;
      configuratorOption.OptionCode = this.OptionCode;
      configuratorOption.OptionDataType = this.OptionDataType;
      configuratorOption.OptionFlags = this.OptionFlags;
      configuratorOption.OptionDescription = this.OptionDescription;
      configuratorOption.OptionValues = this.OptionValues;
      PdmConfiguratorObjectOptionsCache.ResetOption(this.OptionObjectID);
    }
    return true;
  }

  /// <summary>
  /// Метод вызывается для проверки содержимого на наличие ошибок. В случае ошибки
  /// будет сгенерировано исключение
  /// </summary>
  public void BeforeSave() => this.OptionValues.BeforeSave();
}
