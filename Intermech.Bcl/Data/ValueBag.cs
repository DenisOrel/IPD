
// Type: Intermech.Data.ValueBag
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Intermech.Data
{
    /// <summary>Контейнер значений атрибутов</summary>
    public sealed class ValueBag : 
      ICollection<ValueRecord>,
      IEnumerable<ValueRecord>,
      IEnumerable,
      ICollection,
      ICloneable
    {
      private readonly List<ValueRecord> items;
      private readonly List<StringKey> keys;
      private readonly ReadOnlyCollection<StringKey> readOnlyKeysWrapper;
      private Dictionary<StringKey, ValueRecordState> changes;
      private List<ValueRecord> restoreData;

      /// <summary>Создать пустой контейнер значений атрибутов</summary>
      public ValueBag()
        : this(32 /*0x20*/)
      {
      }

      /// <summary>
      /// Создать пустой контейнер значений атрибутов указанной начальной емкости
      /// </summary>
      /// <param name="capacity">Начальная емкость контейнера значений</param>
      public ValueBag(int capacity)
      {
        if (capacity < 8)
          capacity = 8;
        this.items = new List<ValueRecord>(capacity);
        this.keys = new List<StringKey>(capacity);
        this.readOnlyKeysWrapper = new ReadOnlyCollection<StringKey>((IList<StringKey>) this.keys);
      }

      /// <summary>
      /// Создать контейнер значений атрибутов, заполнить его элементами из указанной коллекции
      /// </summary>
      /// <param name="initialItems">Коллекция значений атрибутов</param>
      public ValueBag(ICollection<ValueRecord> initialItems)
      {
        if (initialItems == null)
          throw new ArgumentNullException(nameof (initialItems));
        foreach (ValueRecord initialItem in (IEnumerable<ValueRecord>) initialItems)
        {
          if (initialItem.Bag != null)
            throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_798"));
        }
        try
        {
          this.items = new List<ValueRecord>(initialItems.Count);
          this.keys = new List<StringKey>(initialItems.Count);
          foreach (ValueRecord initialItem in (IEnumerable<ValueRecord>) initialItems)
          {
            int num = this.keys.BinarySearch(initialItem.Key);
            if (num >= 0)
              throw new ArgumentException();
            int index = ~num;
            this.items.Insert(index, initialItem);
            this.keys.Insert(index, initialItem.Key);
            initialItem.Bag = this;
          }
          this.readOnlyKeysWrapper = new ReadOnlyCollection<StringKey>((IList<StringKey>) this.keys);
        }
        catch
        {
          foreach (ValueRecord initialItem in (IEnumerable<ValueRecord>) initialItems)
            initialItem.Bag = (ValueBag) null;
          throw;
        }
      }

      /// <summary>Реализует конструктор клонирования.</summary>
      /// <param name="source">Клонируемый контейнер</param>
      private ValueBag(ValueBag source)
      {
        this.items = new List<ValueRecord>(source.items.Count);
        this.keys = new List<StringKey>(source.items.Count);
        foreach (ValueRecord valueRecord1 in source.items)
        {
          ValueRecord valueRecord2 = valueRecord1.Clone();
          valueRecord2.Bag = this;
          this.items.Add(valueRecord2);
          this.keys.Add(valueRecord2.Key);
        }
        this.readOnlyKeysWrapper = new ReadOnlyCollection<StringKey>((IList<StringKey>) this.keys);
        if (!source.HasChanges)
          return;
        this.changes = new Dictionary<StringKey, ValueRecordState>(source.changes.Count);
        this.restoreData = new List<ValueRecord>(source.restoreData.Capacity);
        foreach (KeyValuePair<StringKey, ValueRecordState> change in source.changes)
          this.changes.Add(change.Key, change.Value);
        foreach (ValueRecord valueRecord in source.restoreData)
          this.restoreData.Add(valueRecord.Clone());
      }

      /// <summary>Ключи параметров, находящихся в контейнере</summary>
      public IList<StringKey> Keys => (IList<StringKey>) this.readOnlyKeysWrapper;

      /// <summary>Возвращает количество значений в контейнере.</summary>
      public int Count => this.items.Count;

      /// <summary>
      /// Возвращает признак неизменности содержимого контейнера.
      /// </summary>
      public bool IsReadOnly => false;

      /// <summary>
      /// Возвращает true, если доступ к коллекции синхронизирован.
      /// </summary>
      public bool IsSynchronized => false;

      /// <summary>
      /// Возвращает объект для синхронизации доступа к это коллекции.
      /// </summary>
      public object SyncRoot => (object) this;

      /// <summary>Добавить новое значение атрибута в контейнер</summary>
      /// <param name="item">Значение, добавляемое в контейнер</param>
      public void Add(ValueRecord item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        if (item.Bag != null)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_798"));
        this.CheckNewItem(item.Key);
        this.AddCore(item);
      }

      /// <summary>Добавить новое значение атрибута в контейнер</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="value">Значение</param>
      /// <returns>Новое значение, добавленное в контейнер</returns>
      public ValueRecord Add(StringKey key, object value)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        this.CheckNewItem(key);
        ValueRecord valueRecord = new ValueRecord(key, value);
        this.AddCore(valueRecord);
        return valueRecord;
      }

      /// <summary>
      /// Добавить новое значение атрибута в контейнер с явным указанием типа значения.
      /// </summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="value">Значение</param>
      /// <param name="dataType">Тип добавляемого значения</param>
      /// <returns>Новое значение, добавленное в контейнер</returns>
      public ValueRecord Add(StringKey key, object value, Type dataType)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        if (dataType == (Type) null)
          throw new ArgumentNullException(nameof (dataType));
        if (ValueRecord.IsUntypedNullValue(value))
          value = (object) TypedNull.Instance(dataType);
        else if (ValueRecord.GetTypedValueType(value) != dataType)
          throw new ArgumentException("Тип значения не соответствует заявленному типу.", nameof (value));
        this.CheckNewItem(key);
        ValueRecord valueRecord = new ValueRecord(key, value);
        this.AddCore(valueRecord);
        return valueRecord;
      }

      /// <summary>
      /// Добавить новое значение атрибута в контейнер и установить у него указанный флаг
      /// </summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="value">Значение</param>
      /// <param name="flag">Флаг для установки</param>
      /// <param name="flagValue">Значение флага для установки</param>
      /// <returns>Новое значение, добавленное в контейнер</returns>
      public ValueRecord AddWithFlag(StringKey key, object value, StringKey flag, bool flagValue = true)
      {
        if (flag == (StringKey) null)
          throw new ArgumentNullException(nameof (flag));
        ValueRecord valueRecord = this.Add(key, value);
        valueRecord.Flags.Set(flag, flagValue);
        return valueRecord;
      }

      /// <summary>Добавляет несколько значений в контейнер.</summary>
      /// <param name="items">Коллекция добавляемых значений</param>
      public void AddRange(ICollection<ValueRecord> items)
      {
        if (items == null)
          throw new ArgumentNullException(nameof (items));
        foreach (ValueRecord valueRecord in (IEnumerable<ValueRecord>) items)
        {
          if (valueRecord.Bag != null)
            throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_799"));
          this.CheckNewItem(valueRecord.Key);
          this.AddCore(valueRecord);
        }
      }

      /// <summary>
      /// Добавляет в контейнер копию только значения для указанного атрибута. Никакие флаги и другая метаинформация не копируется.
      /// </summary>
      /// <param name="record">Значение атрибута, которое будет скопировано и добавлено в контейнер</param>
      /// <returns>Значение, добавленное в контейнер</returns>
      public ValueRecord Import(ValueRecord record)
      {
        if (record == null)
          throw new ArgumentNullException(nameof (record));
        this.CheckNewItem(record.Key);
        ValueRecord valueRecord = record.Copy();
        this.AddCore(valueRecord);
        return valueRecord;
      }

      /// <summary>
      /// Добавляет в контейнер копию только значения для указанных атрибутов. Никакие флаги и другая метаинформация не копируется.
      /// </summary>
      /// <param name="records">Значения атрибута, которое будуь скопированы и добавлены в контейнер</param>
      public void ImportRange(IEnumerable<ValueRecord> records)
      {
        if (records == null)
          throw new ArgumentNullException(nameof (records));
        foreach (ValueRecord record in records)
          this.Import(record);
      }

      /// <summary>Добавить новое значение атрибута в контейнер</summary>
      /// <param name="item">Значение, добавляемое в контейнер</param>
      private void AddCore(ValueRecord item)
      {
        int index = ~this.keys.BinarySearch(item.Key);
        this.TrackChanges(item, ValueBag.ValueBagOperation.Add);
        this.items.Insert(index, item);
        this.keys.Insert(index, item.Key);
        item.Bag = this;
      }

      /// <summary>
      /// Проверить на корректность добавляемое значение атрибута
      /// </summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      private void CheckNewItem(StringKey key)
      {
        if (this.Find(key) != null)
          throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("SR_800"), (object) key));
      }

      /// <summary>Выполняет очистку контейнера.</summary>
      public void Clear()
      {
        if (this.items.Count == 0)
          return;
        foreach (ValueRecord valueRecord in this.items)
          this.TrackChanges(valueRecord, ValueBag.ValueBagOperation.Remove);
        this.ClearCore();
      }

      private void ClearCore()
      {
        foreach (ValueRecord valueRecord in this.items)
          valueRecord.Bag = (ValueBag) null;
        this.items.Clear();
        this.keys.Clear();
      }

      /// <summary>Удалить значение указанного атрибута из контейнера</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <returns>Признак успешного удаления элемента</returns>
      public bool Remove(StringKey key)
      {
        int index = !(key == (StringKey) null) ? this.keys.BinarySearch(key) : throw new ArgumentNullException(nameof (key));
        if (index < 0)
          return false;
        ValueRecord valueRecord = this.items[index];
        this.TrackChanges(valueRecord, ValueBag.ValueBagOperation.Remove);
        this.items.RemoveAt(index);
        this.keys.RemoveAt(index);
        valueRecord.Bag = (ValueBag) null;
        return true;
      }

      /// <summary>Удаляет указанное значение из контейнера.</summary>
      /// <param name="item">Удаляемое значение</param>
      /// <returns>Признак успешного удаления элемента</returns>
      public bool Remove(ValueRecord item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        return item.Bag == this ? this.Remove(item.Key) : throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_799"));
      }

      /// <summary>
      /// Проверить, можно ли обновить указанное значение атрибута
      /// </summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="dataType">Тип данных</param>
      /// <param name="allowAppendItem">Можно ли дополнить элемент</param>
      /// <returns>true - элемент можно изменять</returns>
      public bool CanUpdate(StringKey key, Type dataType, bool allowAppendItem)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        if (dataType == (Type) null)
          throw new ArgumentNullException(nameof (dataType));
        ValueRecord valueRecord = this.Find(key);
        if (valueRecord == null)
        {
          if (!allowAppendItem)
            return false;
        }
        else if (valueRecord.Flags[NamedFlags.ReadOnly] || valueRecord.DataType != dataType)
          return false;
        return true;
      }

      /// <summary>Обновить значение атрибута</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="newValue">Новое значение</param>
      /// <returns>Обновленное значение атрибута</returns>
      public ValueRecord Update(StringKey key, object newValue) => this.Update(key, newValue, true);

      /// <summary>Обновить значение атрибута</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="newValue">Новое значение</param>
      /// <param name="allowAppendItem">Разрешено ли добавлять новые значения в контейнер</param>
      /// <returns>Обновленное значение атрибута</returns>
      public ValueRecord Update(StringKey key, object newValue, bool allowAppendItem)
      {
        return this.TryUpdate(key, newValue, allowAppendItem) ?? throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("SR_801"), (object) key, newValue));
      }

      /// <summary>Попытаться обновить значение атрибута</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="newValue">Новое значение</param>
      /// <returns>Обновленное значение атрибута или null</returns>
      public ValueRecord TryUpdate(StringKey key, object newValue)
      {
        return this.TryUpdate(key, newValue, false);
      }

      /// <summary>Попытаться обновить значение атрибута</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="newValue">Новое значение</param>
      /// <param name="allowAppendItem">Разрешено ли добавлять новые значения в контейнер</param>
      /// <returns>Обновленное значение атрибута или null</returns>
      public ValueRecord TryUpdate(StringKey key, object newValue, bool allowAppendItem)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        newValue = ValueRecord.CoerceToTypedValue(newValue);
        return this.CanUpdate(key, ValueRecord.GetTypedValueType(newValue), allowAppendItem) ? this.UpdateCore(key, newValue) : (ValueRecord) null;
      }

      /// <summary>Обновить значение атрибута</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="newValue">Новое значение</param>
      /// <returns>Изменённое значение атрибута</returns>
      private ValueRecord UpdateCore(StringKey key, object newValue)
      {
        ValueRecord valueRecord1 = this.Find(key);
        if (valueRecord1 == null)
        {
          ValueRecord valueRecord2 = new ValueRecord(key, newValue);
          this.AddCore(valueRecord2);
          return valueRecord2;
        }
        valueRecord1.Value = newValue;
        return valueRecord1;
      }

      /// <summary>Изменяет значение флага для указанного атрибута.</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="flag">Имя флага</param>
      /// <param name="flagValue">Новое значение для флага</param>
      public void SetFlag(StringKey key, StringKey flag, bool flagValue = true)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        if (flag == (StringKey) null)
          throw new ArgumentNullException(nameof (flag));
        this.Find(key)?.Flags.Set(flag, flagValue);
      }

      /// <summary>Изменяет значение флага у всех значений в контейнере.</summary>
      /// <param name="flag">Имя флага</param>
      /// <param name="flagValue">Новое значение для флага</param>
      public void SetFlagForAll(StringKey flag, bool flagValue = true)
      {
        if (flag == (StringKey) null)
          throw new ArgumentNullException(nameof (flag));
        foreach (ValueRecord valueRecord in this.items)
          valueRecord.Flags.Set(flag, flagValue);
      }

      /// <summary>
      /// Копирует и изменяет значение флага для указанного атрибута.
      /// </summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="source">Источник значения флага</param>
      /// <param name="flag">Имя флага</param>
      public void CopyFlag(StringKey key, NamedFlagCollection source, StringKey flag)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        if (flag == (StringKey) null)
          throw new ArgumentNullException(nameof (flag));
        this.Find(key)?.Flags.Copy(source, flag);
      }

      internal void NotifyChange(ValueRecord item, object newValue)
      {
        this.TrackChanges(item, ValueBag.ValueBagOperation.Update);
      }

      /// <summary>Клонирует контейнер значений атрибутов</summary>
      /// <returns>Клон контейнера атрибутов</returns>
      public ValueBag Clone() => new ValueBag(this);

      /// <summary>Клонирует контейнер значений атрибутов</summary>
      /// <returns>Клон контейнера атрибутов</returns>
      object ICloneable.Clone() => (object) this.Clone();

      /// <summary>Получить копию текущего контейнера.</summary>
      /// <returns>Копия текущего контейнера</returns>
      public ValueBag Copy()
      {
        ValueBag valueBag = new ValueBag(this.Count);
        valueBag.ImportRange((IEnumerable<ValueRecord>) this);
        valueBag.AcceptChanges();
        return valueBag;
      }

      /// <summary>Копирует содержащиеся значения в массив.</summary>
      /// <param name="array">Массив-приемник</param>
      /// <param name="arrayIndex">Индекс в массиве, с которого начинать копирование</param>
      public void CopyTo(ValueRecord[] array, int arrayIndex)
      {
        this.CloneItems().CopyTo(array, arrayIndex);
      }

      /// <summary>Копирует содержащиеся значения в массив.</summary>
      /// <param name="array">Массив-приемник</param>
      /// <param name="arrayIndex">Индекс в массиве, с которого начинать копирование</param>
      void ICollection.CopyTo(Array array, int arrayIndex)
      {
        ((ICollection) this.CloneItems()).CopyTo(array, arrayIndex);
      }

      private List<ValueRecord> CloneItems()
      {
        List<ValueRecord> valueRecordList = new List<ValueRecord>(this.items.Count);
        foreach (ValueRecord valueRecord in this.items)
          valueRecordList.Add(valueRecord.Clone());
        return valueRecordList;
      }

      /// <summary>
      /// Возвращает true, если указанный элемент находится в контейнере.
      /// </summary>
      /// <param name="item">Проверяемый элемент</param>
      /// <returns>Признак наличия указанного элемента в контейнере</returns>
      public bool Contains(ValueRecord item)
      {
        return item != null && (item.Bag == this || this.keys.BinarySearch(item.Key) >= 0);
      }

      /// <summary>Отыскать первое подходящее значение атрибута</summary>
      /// <param name="match">Метод, осуществляющий проверку значений атрибутов по требуемым критериям поиска</param>
      /// <returns>Первое подходящее значение атрибута или null</returns>
      public ValueRecord Find(Predicate<ValueRecord> match) => this.items.Find(match);

      /// <summary>Найти все подходящие значения атрибутов</summary>
      /// <param name="match">Метод, осуществляющий проверку значений атрибутов по требуемым критериям поиска</param>
      /// <returns>Все подходящие значения атрибутов</returns>
      public List<ValueRecord> FindAll(Predicate<ValueRecord> match) => this.items.FindAll(match);

      /// <summary>Отыскать указанное подходящее значение атрибута</summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <returns>Найденное значение атрибута или null</returns>
      public ValueRecord Find(StringKey key)
      {
        int index = !(key == (StringKey) null) ? this.keys.BinarySearch(key) : throw new ArgumentNullException(nameof (key));
        return index < 0 ? (ValueRecord) null : this.items[index];
      }

      /// <summary>
      /// Проверяет существование в контейнере указанного атрибута
      /// </summary>
      /// <param name="key">Ключ атрибута</param>
      /// <returns>true, если указанный атрибут присутствует в контейнера, false - если отсутствует</returns>
      public bool Exists(StringKey key)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        return this.keys.BinarySearch(key) >= 0;
      }

      /// <summary>Возвращает все значения атрибутов в виде списка.</summary>
      /// <returns>Список с значениями атрибутов</returns>
      public List<ValueRecord> GetItemsList()
      {
        return new List<ValueRecord>((IEnumerable<ValueRecord>) this.items);
      }

      /// <summary>Возвращает все ключи атрибутов в виде списка.</summary>
      /// <returns>Список с ключами атрибутов</returns>
      public List<StringKey> GetItemsKeys() => new List<StringKey>((IEnumerable<StringKey>) this.keys);

      /// <summary>Выполняет преобразование значений атрибутов</summary>
      /// <typeparam name="TOutput">Тип объектов после преобразования</typeparam>
      /// <param name="converter">Метод для выполнения преобразования</param>
      /// <returns>Список объектов после преобразования</returns>
      public List<TOutput> ConvertAll<TOutput>(Converter<ValueRecord, TOutput> converter)
      {
        return converter != null ? this.items.ConvertAll(converter) : throw new ArgumentNullException(nameof (converter));
      }

      /// <summary>Возвращает перечислитель элементов в коллекции.</summary>
      /// <returns>Объект перечислителя</returns>
      public IEnumerator<ValueRecord> GetEnumerator()
      {
        return (IEnumerator<ValueRecord>) this.items.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.items.GetEnumerator();

      /// <summary>
      /// Прочитать значение атрибута в виде указанного типа данных
      /// </summary>
      /// <typeparam name="TValue">Требуемый тип данных</typeparam>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <param name="defaultValue">Значение по умолчанию (возвращается, если значение атрибута пустое)</param>
      /// <returns>Значение атрибута в виде указанного типа данных</returns>
      public TValue Read<TValue>(StringKey key, TValue defaultValue)
      {
        ValueRecord valueRecord = this.Find(key);
        return valueRecord == null ? defaultValue : valueRecord.Read(defaultValue);
      }

      /// <summary>
      /// Возвращает true, если в содержимое контейнера были внесены какие-либо изменения.
      /// </summary>
      public bool HasChanges => this.changes != null && this.changes.Count != 0;

      public ValueRecordState GetChangeState(StringKey key)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        ValueRecordState changeState;
        if (this.changes != null && this.changes.TryGetValue(key, out changeState))
          return changeState;
        if (this.keys.Contains(key))
          return ValueRecordState.Unmodified;
        throw new InvalidOperationException($"В таблице отсутствует атрибут '{key}'.");
      }

      public List<Tuple<StringKey, ValueRecordState>> GetChanges()
      {
        if (!this.HasChanges)
          return new List<Tuple<StringKey, ValueRecordState>>(0);
        List<Tuple<StringKey, ValueRecordState>> changes = new List<Tuple<StringKey, ValueRecordState>>(this.changes.Count);
        foreach (KeyValuePair<StringKey, ValueRecordState> change in this.changes)
          changes.Add(Tuple.Create(change.Key, change.Value));
        return changes;
      }

      /// <summary>
      /// Возвращает список добавленных и измененных элементов контейнера.
      /// </summary>
      /// <returns>Список элементов контейнера</returns>
      public List<ValueRecord> GetChangedItems()
      {
        return this.HasChanges ? this.items.FindAll((Predicate<ValueRecord>) (item => this.changes.ContainsKey(item.Key))) : new List<ValueRecord>(0);
      }

      /// <summary>
      /// Возвращает список ключей добавленных и измененных элементов контейнера.
      /// </summary>
      /// <returns>Список с ключами атрибутов</returns>
      public List<StringKey> GetChangedItemsKeys()
      {
        return this.HasChanges ? this.keys.FindAll(new Predicate<StringKey>(this.changes.ContainsKey)) : new List<StringKey>(0);
      }

      private void TrackChanges(ValueRecord item, ValueBagOperation operation)
      {
        if (this.changes == null)
          this.changes = new Dictionary<StringKey, ValueRecordState>(this.items.Capacity);
        if (this.restoreData == null)
          this.restoreData = new List<ValueRecord>(this.items.Capacity);
        ValueRecordState valueRecordState;
        if (!this.changes.TryGetValue(item.Key, out valueRecordState))
          valueRecordState = ValueRecordState.Unmodified;
        switch (operation)
        {
          case ValueBag.ValueBagOperation.Add:
            if (valueRecordState == ValueRecordState.Unmodified)
            {
              this.changes.Add(item.Key, ValueRecordState.Added);
              break;
            }
            this.changes[item.Key] = ValueRecordState.Modified;
            break;
          case ValueBag.ValueBagOperation.Update:
            if (valueRecordState != ValueRecordState.Unmodified)
              break;
            this.restoreData.Add(item.Clone());
            this.changes.Add(item.Key, ValueRecordState.Modified);
            break;
          case ValueBag.ValueBagOperation.Remove:
            if (valueRecordState == ValueRecordState.Unmodified)
            {
              this.restoreData.Add(item.Clone());
              this.changes.Add(item.Key, ValueRecordState.Removed);
              break;
            }
            if (valueRecordState == ValueRecordState.Added)
            {
              this.changes.Remove(item.Key);
              break;
            }
            this.changes[item.Key] = ValueRecordState.Removed;
            break;
        }
      }

      /// <summary>
      /// Фиксирует все сделанные изменения и очищает журнал изменений.
      /// </summary>
      public void AcceptChanges()
      {
        if (!this.HasChanges)
          return;
        this.changes.Clear();
        this.restoreData = (List<ValueRecord>) null;
      }

      /// <summary>
      /// Фиксирует сделанные изменения для указанного атрибута.
      /// </summary>
      /// <param name="key">Уникальный ключ для идентификации атрибута</param>
      /// <exception cref="T:System.ArgumentNullException">Ключ атрибута не указан</exception>
      public void AcceptChanges(StringKey key)
      {
        if (key == (StringKey) null)
          throw new ArgumentNullException(nameof (key));
        if (!this.changes.ContainsKey(key))
          return;
        switch (this.GetChangeState(key))
        {
          case ValueRecordState.Unmodified:
            break;
          case ValueRecordState.Added:
            this.changes.Remove(key);
            break;
          default:
            this.restoreData.RemoveAt(this.restoreData.FindIndex((Predicate<ValueRecord>) (item => item.Key == key)));
            goto case ValueRecordState.Added;
        }
      }

      /// <summary>
      /// Отменяет все сделанные изменения и очищает журнал изменений.
      /// </summary>
      public void RejectChanges()
      {
        if (!this.HasChanges)
          return;
        foreach (Tuple<StringKey, ValueRecordState> change in this.GetChanges())
        {
          if (change.Item2 == ValueRecordState.Added)
            this.Remove(change.Item1);
        }
        foreach (ValueRecord valueRecord1 in this.restoreData)
        {
          ValueRecord valueRecord2 = this.Find(valueRecord1.Key);
          if (valueRecord2 != null && valueRecord2.DataType == valueRecord1.DataType)
          {
            valueRecord2.Value = valueRecord1.Value;
            valueRecord2.Flags.ResetAll();
            valueRecord2.Flags.CopyAll(valueRecord1.Flags);
          }
          else
          {
            if (valueRecord2 != null)
              this.Remove(valueRecord2);
            this.Add(valueRecord1);
          }
        }
        foreach (KeyValuePair<StringKey, ValueRecordState> change in this.changes)
          ;
        this.AcceptChanges();
      }

      private enum ValueBagOperation
      {
        Add,
        Update,
        Remove,
      }
    }
}
