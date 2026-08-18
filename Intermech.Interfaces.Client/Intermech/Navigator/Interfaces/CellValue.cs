// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.CellValue
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using ImSSP;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Данный класс предназначен для хранения пары значений ячейки – оригинального значения, полученного у
/// источника данных, и преобразованного значения для отображения на экране.
/// Экземпляры данного класса создаются интерфейсом INodeColumnTransform. Экземпляры данного класса реализуют
/// все операторы для явного (explicit) и неявного (implicit) преобразования типов своих значений в
/// стандартные типы .NET Framework, а также реализуют интерфейсы ICloneable, IConvertible и IComparable.
/// </summary>
[Serializable]
public sealed class CellValue : ICloneable, IConvertible, IComparable
{
  /// <summary>Оригинальное значение элемента</summary>
  private object _value;
  /// <summary>
  /// Текстовое значение элемента для отображения на экране.
  /// Если значение равно null, то ToString() попытается вернуть _value.ToString()
  /// </summary>
  private object _caption = (object) string.Empty;

  /// <summary>
  /// Оригинальное значение ячейки "Навигатора", полученное из источника данных
  /// </summary>
  public object Value
  {
    get => this._value;
    set => this._value = value;
  }

  /// <summary>
  /// Текстовое значение элемента для отображения на экране.
  /// Если значение равно null, то ToString() попытается вернуть Value.ToString()
  /// </summary>
  public object Caption
  {
    get => this._caption;
    set => this._caption = value;
  }

  /// <summary>Создать пустой экземпляр класса CellValue</summary>
  public CellValue()
  {
  }

  /// <summary>Создать заполненный экземпляр класса CellValue</summary>
  /// <param name="value">Значение элемента</param>
  /// <param name="caption">Текстовое описание элемента</param>
  public CellValue(object value, object caption)
  {
    this._value = value;
    this._caption = caption;
  }

  /// <summary>
  /// Метод анализирует значение column.Contents, если оно равно Text,
  /// возвращает значение newValue, иначе возвращает экземпляр класса CellValue(sourceValue, newValue).
  /// </summary>
  /// <param name="sourceValue">Исходное значение</param>
  /// <param name="column">Колонка</param>
  /// <param name="newValue">Новое значение</param>
  /// <returns>Значение</returns>
  public static object GetValue(object sourceValue, NodeColumn column, object newValue)
  {
    if (column != null && column.TransformationMode == CellTransformationMode.WithoutTransformation)
      return sourceValue;
    return column != null && (column.Contents != ColumnContents.Text || column.TransformationMode == CellTransformationMode.ConvertToCellValue) ? (object) new CellValue(sourceValue, newValue) : newValue;
  }

  /// <summary>Перекрытый метод для возвращения заголовка</summary>
  /// <returns></returns>
  public override string ToString()
  {
    if (this._caption != null)
      return this._caption.ToString();
    return this._value != null ? this._value.ToString() : string.Empty;
  }

  /// <summary>Сделать клон объекта</summary>
  /// <returns>Вернёт 100% копию объекта</returns>
  public object Clone() => (object) new CellValue(this._value, this._caption);

  public TypeCode GetTypeCode()
  {
    return this.Value == null ? TypeCode.Empty : Convert.GetTypeCode(this.Value);
  }

  public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(this.Value, provider);

  public byte ToByte(IFormatProvider provider) => Convert.ToByte(this.Value, provider);

  public char ToChar(IFormatProvider provider) => Convert.ToChar(this.Value, provider);

  public DateTime ToDateTime(IFormatProvider provider) => Convert.ToDateTime(this.Value, provider);

  public Decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(this.Value, provider);

  public double ToDouble(IFormatProvider provider) => Convert.ToDouble(this.Value, provider);

  public short ToInt16(IFormatProvider provider) => Convert.ToInt16(this.Value, provider);

  public int ToInt32(IFormatProvider provider) => Convert.ToInt32(this.Value, provider);

  public long ToInt64(IFormatProvider provider) => Convert.ToInt64(this.Value, provider);

  public sbyte ToSByte(IFormatProvider provider) => Convert.ToSByte(this.Value, provider);

  public float ToSingle(IFormatProvider provider) => Convert.ToSingle(this.Value, provider);

  public string ToString(IFormatProvider provider) => Convert.ToString(this.Value, provider);

  public object ToType(Type conversionType, IFormatProvider provider)
  {
    throw new Exception(LocalizationHolder.rm.GetString(sc_10728.ssp_imclient_10729()) + conversionType.ToString() + LocalizationHolder.rm.GetString("Interfaces.Client_68"));
  }

  public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(this.Value, provider);

  public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(this.Value, provider);

  public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(this.Value, provider);

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(bool value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(byte value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(char value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(DateTime value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(Decimal value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(double value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(short value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(int value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(long value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(sbyte value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(float value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(string value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(ushort value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(uint value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Создать и вернуть экземпляр класса</summary>
  /// <param name="value">Значение</param>
  public static implicit operator CellValue(ulong value)
  {
    return new CellValue((object) value, (object) null);
  }

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator bool(CellValue value) => value.ToBoolean((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator byte(CellValue value) => value.ToByte((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator char(CellValue value) => value.ToChar((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator DateTime(CellValue value)
  {
    return value.ToDateTime((IFormatProvider) null);
  }

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator Decimal(CellValue value)
  {
    return value.ToDecimal((IFormatProvider) null);
  }

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator double(CellValue value) => value.ToDouble((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator short(CellValue value) => value.ToInt16((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator int(CellValue value) => value.ToInt32((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator long(CellValue value) => value.ToInt64((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator sbyte(CellValue value) => value.ToSByte((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator float(CellValue value) => value.ToSingle((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator string(CellValue value) => value.ToString((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator ushort(CellValue value) => value.ToUInt16((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator uint(CellValue value) => value.ToUInt32((IFormatProvider) null);

  /// <summary>Выполнить явное преобразование типов</summary>
  /// <param name="value">Значение</param>
  /// <returns>Преобразованное значение</returns>
  public static explicit operator ulong(CellValue value) => value.ToUInt64((IFormatProvider) null);

  /// <summary>Выполнить сравнение с объектом</summary>
  /// <param name="obj">Объект</param>
  /// <returns></returns>
  int IComparable.CompareTo(object obj)
  {
    if (obj == null || obj.GetType() != this.GetType())
      return 0;
    Type type1 = this.Value.GetType();
    object obj1 = obj;
    Type type2 = typeof (CellValue);
    if (type1 == type2)
      obj1 = (obj as CellValue).Caption;
    return obj1 is IComparable comparable ? -comparable.CompareTo(this.Caption) : 0;
  }
}
