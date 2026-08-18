
// Type: Intermech.Data.ValueRecord
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Diagnostics;


namespace Intermech.Data
{
    /// <summary>Значение атрибута объекта/связи</summary>
    [DebuggerDisplay("{Key}:{DataType} = {Value}, flags: {Flags}")]
    public sealed class ValueRecord : ICloneable
    {
      /// <summary>Ключ, идентифицирующий атрибут объекта/связи</summary>
      private readonly StringKey key;
      /// <summary>Тип данных</summary>
      private readonly Type dataType;
      /// <summary>Значение</summary>
      private object value;
      /// <summary>Контейнер-владелец</summary>
      private ValueBag bag;
      /// <summary>Коллекция именованных логических флагов</summary>
      private readonly NamedFlagCollection flags;
      private static readonly BooleanSwitch TypedNullStrictMode = new BooleanSwitch(nameof (TypedNullStrictMode), "", "1");

      /// <summary>Создать значение атрибута объекта/связи</summary>
      /// <param name="key">Ключ, идентифицирующий атрибут объекта/связи</param>
      /// <param name="value">Значение</param>
      public ValueRecord(StringKey key, object value)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        value = ValueRecord.CoerceToTypedValue(value);
        this.key = key;
        this.dataType = ValueRecord.GetTypedValueType(value);
        this.value = ValueRecord.IsTypedNullValue(value) ? (object) null : value;
        this.flags = new NamedFlagCollection();
      }

      internal static object CoerceToTypedValue(object value)
      {
        if (ValueRecord.IsUntypedNullValue(value))
        {
          if (ValueRecord.TypedNullStrictMode.Enabled)
            throw ValueRecord.UntypedNullException();
          value = (object) TypedNull.String;
        }
        return value;
      }

      internal static Type GetTypedValueType(object value)
      {
        if (ValueRecord.IsUntypedNullValue(value))
          throw ValueRecord.UntypedNullException();
        return value is TypedNull typedNull ? typedNull.ValueType : value.GetType();
      }

      private static ArgumentException UntypedNullException()
      {
        return new ArgumentException("Нетипизированные null-значения недопустимы. Воспользуйтесь значениями типа TypedNull.", "value");
      }

      /// <summary>Ключ, идентифицирующий атрибут объекта/связи</summary>
      public StringKey Key
      {
        [DebuggerStepThrough] get => this.key;
      }

      /// <summary>Тип данных</summary>
      public Type DataType
      {
        [DebuggerStepThrough] get => this.dataType;
      }

      /// <summary>Проверить, незаполнено ли значение атрибута</summary>
      public bool IsNull
      {
        [DebuggerStepThrough] get => this.value == null;
      }

      /// <summary>Проверить, незаполнено ли значение атрибута.</summary>
      /// <returns>true, если значение атрибута не заполнено</returns>
      public static bool IsNullValue(object value)
      {
        return ValueRecord.IsUntypedNullValue(value) || ValueRecord.IsTypedNullValue(value);
      }

      /// <summary>Значение</summary>
      public object Value
      {
        [DebuggerStepThrough] get => this.value;
        [DebuggerStepThrough] set => this.UpdateValue(value);
      }

      /// <summary>Контейнер-владелец</summary>
      public ValueBag Bag
      {
        [DebuggerStepThrough] get => this.bag;
        [DebuggerStepThrough] internal set => this.bag = value;
      }

      /// <summary>Коллекция именованных логических флагов.</summary>
      public NamedFlagCollection Flags => this.flags;

      /// <summary>
      /// Создать точную копию значения атрибута без копирования флагов и другой метаинформации.
      /// </summary>
      /// <returns>Копия значения атрибута</returns>
      public ValueRecord Copy() => new ValueRecord(this.key, this.ReadValueOrTypedNull());

      /// <summary>Создать точную копию значения атрибута</summary>
      /// <returns>Точная копия значения</returns>
      public ValueRecord Clone()
      {
        ValueRecord valueRecord = this.Copy();
        valueRecord.Flags.CopyAll(this.Flags);
        return valueRecord;
      }

      /// <summary>Создать точную копию значения атрибута</summary>
      /// <returns>Точная копия значения</returns>
      object ICloneable.Clone() => (object) this.Clone();

      /// <summary>Является ли значение пустым (null или DBNull.Value)</summary>
      /// <param name="value">Проверяемое значение</param>
      /// <returns>true - значение является пустым (null или DBNull.Value)</returns>
      internal static bool IsUntypedNullValue(object value)
      {
        return value == null || object.Equals(value, (object) DBNull.Value);
      }

      internal static bool IsTypedNullValue(object value) => value is TypedNull;

      /// <summary>
      /// Получить значение атрибута в виде указанного типа данных
      /// </summary>
      /// <typeparam name="TValue">Требуемый тип значения</typeparam>
      /// <param name="defaultValue">Значение по умолчанию (применяется, если значение атрибута является пустым)</param>
      /// <returns>Значение атрибута в виде указанного типа данных</returns>
      public TValue Read<TValue>(TValue defaultValue)
      {
        Type type = typeof (TValue);
        if (this.dataType != type)
          throw new ArgumentException($"Тип значения атрибута отличается от требуемого типа '{type}'.");
        return !this.IsNull ? (TValue) this.value : defaultValue;
      }

      /// <summary>
      /// Возвращает значение атрибута или объект типа TypedNull, если значение атрибута не задано.
      /// </summary>
      /// <returns>Значение атрибута или объект типа TypedNull</returns>
      public object ReadValueOrTypedNull()
      {
        return !this.IsNull ? this.value : (object) TypedNull.Instance(this.dataType);
      }

      /// <summary>Добавляет элемент в контейнер.</summary>
      /// <param name="newBag">Контейнер</param>
      public void AddTo(ValueBag newBag)
      {
        if (newBag == null)
          throw new ArgumentNullException(nameof (newBag));
        newBag.Add(this);
      }

      /// <summary>Удалить значение атрибута из контейнера</summary>
      public void Remove()
      {
        if (this.bag == null)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_802"));
        this.bag.Remove(this);
      }

      /// <summary>Заменить текущее значение атрибута новым</summary>
      /// <param name="newValue">Новое значение атрибута</param>
      private void UpdateValue(object newValue)
      {
        if (this.Flags[NamedFlags.ReadOnly])
          throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("SR_803"), (object) this.key));
        object obj = ValueRecord.IsUntypedNullValue(newValue) ? (object) TypedNull.Instance(this.dataType) : newValue;
        Type typedValueType = ValueRecord.GetTypedValueType(obj);
        if (typedValueType != this.dataType)
          throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("SR_804"), (object) this.key, obj, (object) this.dataType, (object) typedValueType));
        if (ValueRecord.IsNullValue(newValue))
          newValue = (object) null;
        if (object.Equals(this.value, newValue))
          return;
        if (this.bag != null)
          this.bag.NotifyChange(this, newValue);
        this.value = newValue;
      }
    }
}
