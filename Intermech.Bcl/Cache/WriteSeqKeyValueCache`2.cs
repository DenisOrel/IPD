
// Type: Intermech.Cache.WriteSeqKeyValueCache`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Cache
{
    /// <summary>
    /// Реализует кэш для значений, идентифицируемых по ключам и хранящихся в постоянном хранилище.
    /// Для проверки валидности значений в кэше используется счетчик изменений в хранилище, изменяющийся при каждой записи в хранилище.
    /// </summary>
    /// <typeparam name="TKey">Тип ключей</typeparam>
    /// <typeparam name="TValue">Тип кэшируемых значений</typeparam>
    public sealed class WriteSeqKeyValueCache<TKey, TValue>
    {
      private TimeSpan storageCheckPeriod;
      private Func<TKey, TValue> valueFunction;
      private Func<long> writeSeqFunction;
      private Dictionary<TKey, TValue> cacheItems;
      private bool cacheIsEmpty;
      private long cacheWriteSeq;
      private DateTime storageCheckTime;

      /// <summary>Создает объект.</summary>
      /// <param name="storageCheckPeriod">Время жизни значений в кэше</param>
      /// <param name="valueFunction">Функция получения значения из хранилища</param>
      /// <param name="writeSeqFunction">Функция получения счетчика изменений хранилища</param>
      /// <exception cref="T:ArgumentOutOfRangeException">storageCheckPeriod</exception>
      /// <exception cref="T:ArgumentNullException">valueFunction, writeSeqFunction</exception>
      public WriteSeqKeyValueCache(
        TimeSpan storageCheckPeriod,
        Func<TKey, TValue> valueFunction,
        Func<long> writeSeqFunction)
      {
        if (storageCheckPeriod.Ticks == 0L)
          throw new ArgumentOutOfRangeException(nameof (storageCheckPeriod));
        if (valueFunction == null)
          throw new ArgumentNullException(nameof (valueFunction));
        if (writeSeqFunction == null)
          throw new ArgumentNullException(nameof (writeSeqFunction));
        this.storageCheckPeriod = storageCheckPeriod;
        this.valueFunction = valueFunction;
        this.writeSeqFunction = writeSeqFunction;
        this.cacheItems = new Dictionary<TKey, TValue>();
        this.cacheIsEmpty = true;
        this.cacheWriteSeq = -1L;
      }

      /// <summary>
      /// Возвращает значение для указанного ключа. Если значение отсутствует в кэше, то оно будет получено из хранилища.
      /// </summary>
      /// <param name="key">Ключ</param>
      /// <returns>Значение</returns>
      public TValue GetValue(TKey key)
      {
        DateTime now = DateTime.Now;
        if (!this.cacheIsEmpty && now - this.storageCheckTime > this.storageCheckPeriod)
          this.ValidateCache(this.writeSeqFunction(), now);
        TValue obj;
        if (!this.cacheIsEmpty && this.cacheItems.TryGetValue(key, out obj))
          return obj;
            SignedValue signedStorageValue = this.GetSignedStorageValue(key);
        this.ValidateCache(signedStorageValue.WriteSeq, now);
        this.cacheItems[key] = signedStorageValue.Value;
        this.cacheIsEmpty = false;
        return signedStorageValue.Value;
      }

      private void ValidateCache(long actualWriteSeq, DateTime actualTime)
      {
        if (actualWriteSeq != this.cacheWriteSeq)
        {
          this.Clear();
          this.cacheWriteSeq = actualWriteSeq;
        }
        this.storageCheckTime = actualTime;
      }

      private SignedValue GetSignedStorageValue(TKey key)
      {
        long writeSeq = this.writeSeqFunction();
        int num = 10;
        TValue obj;
        bool flag;
        do
        {
          obj = this.valueFunction(key);
          flag = this.writeSeqFunction() != writeSeq;
          if (flag)
          {
            --num;
            if (num > 0)
              Thread.Yield();
          }
        }
        while (flag && num > 0);
        if (flag)
          throw new InvalidOperationException($"Unable to get a value of type '{typeof (TValue)}' from a storage. Too many concurrent writes to a storage detected.");
        return new SignedValue(obj, writeSeq);
      }

      /// <summary>Очищает кэш.</summary>
      public void Clear()
      {
        this.cacheItems.Clear();
        this.cacheIsEmpty = true;
      }

      /// <summary>
      /// Проверяет, есть ли в кэше значение для указанного ключа.
      /// </summary>
      /// <param name="key">Ключ</param>
      /// <returns>Признак наличия значения в кэше</returns>
      public bool HasCachedValue(TKey key) => this.cacheItems.ContainsKey(key);

      /// <summary>
      /// Возвращает кэшированное значение для указанного ключа.
      /// </summary>
      /// <param name="key">Ключ</param>
      /// <param name="defaultValue">Значение, возвращаемое при отсутствии значения в кэше</param>
      /// <returns>Кэшированное значение или значение по умолчанию</returns>
      public TValue TryGetCachedValue(TKey key, TValue defaultValue)
      {
        TValue obj;
        return this.cacheItems.TryGetValue(key, out obj) ? obj : defaultValue;
      }

      private struct SignedValue(TValue value, long writeSeq)
      {
        public readonly TValue Value = value;
        public readonly long WriteSeq = writeSeq;
      }
    }
}
