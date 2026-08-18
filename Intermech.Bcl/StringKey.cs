
// Type: Intermech.StringKey
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech
{
    /// <summary>
    /// Реализует класс строковых ключей, не зависящих от регистра.
    /// </summary>
    [DebuggerDisplay("{value}")]
    public sealed class StringKey : 
      IEquatable<StringKey>,
      IEquatable<string>,
      IComparable<StringKey>,
      IComparable<string>,
      IComparable
    {
      /// <summary>Значение ключа</summary>
      private readonly string value;
      /// <summary>Хэш-код</summary>
      private int? hashCode;
      public static readonly StringComparer Comparer = StringComparer.CurrentCultureIgnoreCase;

      /// <summary>Создает новый ключ.</summary>
      /// <param name="value">Значение ключа</param>
      public StringKey(string value)
      {
        this.value = value != null ? value : throw new ArgumentNullException(nameof (value));
      }

      /// <summary>
      /// Возвращает true, если этот ключ эквивалентен указанном.
      /// </summary>
      /// <param name="other">Другой ключ</param>
      /// <returns>true, если ключи эквивалентны</returns>
      public bool Equals(StringKey other)
      {
        return other != (StringKey) null && StringKey.Comparer.Compare(this.value, other.value) == 0;
      }

      public bool Equals(string other) => StringKey.Comparer.Compare(this.value, other) == 0;

      /// <summary>Возвращает хэш-код ключа.</summary>
      /// <returns>Значение хэш-кода</returns>
      public override int GetHashCode()
      {
        if (!this.hashCode.HasValue)
        {
          lock (this)
            this.hashCode = new int?(StringKey.Comparer.GetHashCode(this.value));
        }
        return this.hashCode.Value;
      }

      /// <summary>
      /// Возвращает true, если этот ключ эквивалентен указанному объекту.
      /// </summary>
      /// <param name="obj">Другой объект</param>
      /// <returns>true, если объекты эквивалентны</returns>
      public override bool Equals(object obj)
      {
        StringKey other = obj as StringKey;
        return !(other != (StringKey) null) ? base.Equals(obj) : this.Equals(other);
      }

      /// <summary>Возвращает текстовое представление ключа.</summary>
      /// <returns></returns>
      public override string ToString() => this.value;

      public int CompareTo(StringKey other) => StringKey.Comparer.Compare(this.value, other.value);

      public int CompareTo(string other) => StringKey.Comparer.Compare(this.value, other);

      public int CompareTo(object obj)
      {
        StringKey other = obj as StringKey;
        return other != (StringKey) null ? this.CompareTo(other) : this.CompareTo((string) obj);
      }

      public static bool operator ==(StringKey x, StringKey y) => object.Equals((object) x, (object) y);

      public static bool operator !=(StringKey x, StringKey y)
      {
        return !object.Equals((object) x, (object) y);
      }

      public static implicit operator string(StringKey obj)
      {
        return !(obj != (StringKey) null) ? (string) null : obj.value;
      }

      public static implicit operator StringKey(string obj)
      {
        return obj == null ? (StringKey) null : new StringKey(obj);
      }
    }
}
