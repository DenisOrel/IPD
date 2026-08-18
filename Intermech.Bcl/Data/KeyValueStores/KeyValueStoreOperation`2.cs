
// Type: Intermech.Data.KeyValueStores.KeyValueStoreOperation`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Data.KeyValueStores
{
    public class KeyValueStoreOperation<TKey, TValue> where TKey : IEquatable<TKey>
    {
      private KeyValueStoreOpCode opCode;
      private TKey key;
      private TValue value;
      private TValue previousValue;
      private bool hasPreviousValue;

      public KeyValueStoreOperation(KeyValueStoreOpCode opCode, TKey key, TValue value)
      {
        this.opCode = opCode;
        this.key = key;
        this.value = value;
        this.previousValue = default (TValue);
      }

      public KeyValueStoreOperation(
        KeyValueStoreOpCode opCode,
        TKey key,
        TValue value,
        TValue previousValue)
      {
        this.opCode = opCode;
        this.key = key;
        this.value = value;
        this.previousValue = previousValue;
        this.hasPreviousValue = true;
      }

      public KeyValueStoreOpCode OpCode
      {
        [DebuggerStepThrough] get => this.opCode;
      }

      public TKey Key
      {
        [DebuggerStepThrough] get => this.key;
      }

      public TValue Value
      {
        [DebuggerStepThrough] get => this.value;
      }

      public TValue PreviousValue
      {
        [DebuggerStepThrough] get => this.previousValue;
      }

      public bool HasPreviousValue
      {
        [DebuggerStepThrough] get => this.hasPreviousValue;
      }
    }
}
