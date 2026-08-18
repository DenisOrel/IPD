// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.MemoProxyReader
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Класс для "ленивого" чтения Memo-полей</summary>
[Serializable]
public class MemoProxyReader : ICloneable, IConvertible
{
  private bool _loaded;
  private bool _relation;
  private long _Id = -1;
  private int _attrId = -1;
  private string _value = "";

  /// <summary>
  /// Загружены ли данные из объекта? Если нет, это обрезанные данные
  /// </summary>
  public bool Loaded => this._loaded;

  /// <summary>Относится ли атрибут к объекту или к связи?</summary>
  public bool IsRelation => this._relation;

  /// <summary>Идентификатор объекта или связи</summary>
  public long Id => this._Id;

  /// <summary>Идентификатор атрибута</summary>
  public int AttrId => this._attrId;

  /// <summary>Значение атрибута (обрезанное или скачанное)</summary>
  public string Value
  {
    get => this._value;
    set => this._value = value;
  }

  /// <summary>Конструктор MemoProxyReader</summary>
  /// <param name="_id">ИД объекта или связи</param>
  /// <param name="_attr">ИД типа атрибуты</param>
  /// <param name="_rel">true, если ИД связи</param>
  public MemoProxyReader(long _id, int _attr, bool _rel = false)
  {
    this._Id = _id;
    this._attrId = _attr;
    this._relation = _rel;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_id">ИД объекта или связи</param>
  /// <param name="_attr">ИД типа атрибуты</param>
  /// <param name="_val">Исходное значение поля</param>
  /// <param name="_rel">true, если ИД связи</param>
  public MemoProxyReader(long _id, int _attr, string _val, bool _rel = false)
  {
    this._Id = _id;
    this._attrId = _attr;
    this._relation = _rel;
    this._value = _val;
  }

  /// <summary>Закрытый конструктор для клонирования</summary>
  private MemoProxyReader(long _id, int _attr, string _val, bool _loaded, bool _rel = false)
  {
    this._Id = _id;
    this._attrId = _attr;
    this._relation = _rel;
    this._value = _val;
    this._loaded = _loaded;
  }

  /// <summary>Загрузка мемо из объекта или связи</summary>
  /// <param name="ius">Пользовательская сессия</param>
  /// <returns>true, если данные были успешно загружены (в том числе раньше)</returns>
  public bool LoadData(IUserSession ius)
  {
    if (this._loaded)
      return true;
    IDBAttributable dbAttributable = !this._relation ? (IDBAttributable) ius.GetObject(this._Id, false) : (IDBAttributable) ius.GetRelation(this._Id, false);
    if (dbAttributable == null)
      return false;
    object[] valuesById = dbAttributable.GetValuesByID(this._attrId, false);
    if (valuesById == null || valuesById.Length == 0)
      return false;
    this._value = Convert.ToString(valuesById[0]);
    this._loaded = true;
    return true;
  }

  public override string ToString() => this._value;

  public override bool Equals(object obj)
  {
    return obj is MemoProxyReader memoProxyReader && this._Id == memoProxyReader._Id && this._attrId == memoProxyReader._attrId;
  }

  public override int GetHashCode() => this._attrId ^ (int) this._Id;

  public object Clone()
  {
    return (object) new MemoProxyReader(this._Id, this._attrId, this._value, this._loaded, this._relation);
  }

  public TypeCode GetTypeCode() => TypeCode.String;

  public bool ToBoolean(IFormatProvider provider) => this._loaded;

  public byte ToByte(IFormatProvider provider) => !this._loaded ? (byte) 0 : (byte) 1;

  public char ToChar(IFormatProvider provider) => !this._loaded ? 'N' : 'Y';

  public DateTime ToDateTime(IFormatProvider provider) => Convert.ToDateTime(this._value, provider);

  public Decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(this._value, provider);

  public double ToDouble(IFormatProvider provider) => Convert.ToDouble(this._value, provider);

  public short ToInt16(IFormatProvider provider) => Convert.ToInt16(this._value, provider);

  public int ToInt32(IFormatProvider provider) => Convert.ToInt32(this._value, provider);

  public long ToInt64(IFormatProvider provider) => Convert.ToInt64(this._value, provider);

  public sbyte ToSByte(IFormatProvider provider) => !this._loaded ? (sbyte) 0 : (sbyte) 1;

  public float ToSingle(IFormatProvider provider) => Convert.ToSingle(this._value, provider);

  public string ToString(IFormatProvider provider) => this._value;

  public object ToType(Type conversionType, IFormatProvider provider)
  {
    if (conversionType == typeof (string))
      return (object) this._value;
    throw new NotImplementedException();
  }

  public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(this._value, provider);

  public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(this._value, provider);

  public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(this._value, provider);
}
