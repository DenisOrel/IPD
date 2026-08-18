
// Type: Intermech.Text.ImStringBuilder
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Text
{
    /// <summary>
    /// Специализированная реализация построителя строк, избавленная от проблем системного <see cref="!:StringBuilder" /> - объекты этого типа
    /// эффективно используют Capacity и внутренний буфер символов, могут переиспользоваться произвольное количество раз,
    /// могут быть очищены без утечки памяти. <see cref="T:Intermech.Text.ImStringBuilder" /> не является равноценной заменой для
    /// системного <see cref="!:StringBuilder" />, это тип предназначен для использования в высоконагруженных сервисах,
    /// генерирующих большое количество разнообразных текстов.
    /// </summary>
    public sealed class ImStringBuilder
    {
      private char[] buffer;
      private int capacity;
      private int insertIndex;

      public ImStringBuilder()
        : this(16 /*0x10*/)
      {
      }

      public ImStringBuilder(int capacity)
      {
        if (capacity < 0)
          throw new ArgumentOutOfRangeException(nameof (capacity));
        this.capacity = capacity != 0 ? capacity : 16 /*0x10*/;
        this.buffer = new char[this.capacity];
      }

      public int Length
      {
        [DebuggerStepThrough] get => this.insertIndex;
      }

      public int Capacity
      {
        [DebuggerStepThrough] get => this.capacity;
      }

      [IndexerName("Chars")]
      public char this[int index]
      {
        [DebuggerStepThrough] get
        {
          return index >= 0 && index < this.insertIndex ? this.buffer[index] : throw new IndexOutOfRangeException();
        }
        set
        {
          if (index < 0 || index >= this.insertIndex)
            throw new ArgumentOutOfRangeException(nameof (index));
          this.buffer[index] = value;
        }
      }

      public void Clear()
      {
        if (this.insertIndex == 0)
          return;
        this.insertIndex = 0;
      }

      public override unsafe string ToString()
      {
        if (this.insertIndex == 0)
          return string.Empty;
        fixed (char* chPtr = &this.buffer[0])
          return new string(chPtr, 0, this.insertIndex);
      }

      public ImStringBuilder Append(char value)
      {
        if (this.insertIndex >= this.capacity)
          return this.Append(value, 1);
        this.buffer[this.insertIndex++] = value;
        return this;
      }

      public ImStringBuilder Append(char value, int repeatCount)
      {
        if (repeatCount < 0)
          throw new ArgumentOutOfRangeException(nameof (repeatCount));
        if (repeatCount == 0)
          return this;
        int insertIndex = this.insertIndex;
        while (repeatCount > 0)
        {
          if (insertIndex < this.capacity)
          {
            this.buffer[insertIndex++] = value;
            --repeatCount;
          }
          else
          {
            int newSize = this.capacity + repeatCount + 16 /*0x10*/;
            Array.Resize(ref this.buffer, newSize);
            this.capacity = newSize;
          }
        }
        this.insertIndex = insertIndex;
        return this;
      }

      public unsafe ImStringBuilder Append(string value)
      {
        if (value != null && value.Length != 0)
        {
          int length = value.Length;
          int num = this.insertIndex + length;
          if (num > this.capacity)
          {
            int newSize = num + 16 /*0x10*/;
            Array.Resize(ref this.buffer, newSize);
            this.capacity = newSize;
          }
          fixed (char* src = value)
            fixed (char* dst = &this.buffer[this.insertIndex])
              this.ByteCopy((byte*) src, (byte*) dst, length << 1);
          this.insertIndex = num;
        }
        return this;
      }

      public ImStringBuilder AppendFormat(string format, object arg0)
      {
        return this.Append(string.Format(format, arg0));
      }

      public ImStringBuilder AppendFormat(string format, object arg0, object arg1)
      {
        return this.Append(string.Format(format, arg0, arg1));
      }

      public ImStringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
      {
        return this.Append(string.Format(format, arg0, arg1, arg2));
      }

      public ImStringBuilder AppendFormat(string format, params object[] args)
      {
        return this.Append(string.Format(format, args));
      }

      public ImStringBuilder AppendLine() => this.Append(Environment.NewLine);

      public unsafe ImStringBuilder Insert(int index, char value)
      {
        if (index < 0 || index > this.insertIndex)
          throw new ArgumentOutOfRangeException(nameof (index));
        this.InsertInternal(index, &value, 1);
        return this;
      }

      public unsafe ImStringBuilder Insert(int index, string value)
      {
        if (index < 0 || index > this.insertIndex)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (value != null && value.Length != 0)
        {
          int length = value.Length;
          fixed (char* valuePtr = value)
            this.InsertInternal(index, valuePtr, length);
        }
        return this;
      }

      private unsafe void InsertInternal(int index, char* valuePtr, int charCount)
      {
        int num = this.insertIndex + charCount;
        if (num > this.capacity)
        {
          int newSize = num + 16 /*0x10*/;
          Array.Resize(ref this.buffer, newSize);
          this.capacity = newSize;
        }
        int charCount1 = this.insertIndex - index;
        if (charCount1 != 0)
        {
          fixed (char* src = &this.buffer[this.insertIndex - 1])
          {
            char* dst = src + charCount;
            this.TailCharCopy(src, dst, charCount1);
          }
        }
        fixed (char* dst = &this.buffer[index])
          this.ByteCopy((byte*) valuePtr, (byte*) dst, charCount << 1);
        this.insertIndex = num;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private unsafe void ByteCopy(byte* src, byte* dst, int len)
      {
        for (; len >= 8; len -= 8)
        {
          *(long*) dst = *(long*) src;
          dst += 8;
          src += 8;
        }
        if (len >= 4)
        {
          *(int*) dst = (int) *(uint*) src;
          dst += 4;
          src += 4;
          len -= 4;
        }
        if (len >= 2)
        {
          *(short*) dst = (short) *(ushort*) src;
          dst += 2;
          src += 2;
          len -= 2;
        }
        if (len < 1)
          return;
        *dst = *src;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private unsafe void TailCharCopy(char* src, char* dst, int charCount)
      {
        for (; charCount != 0; --charCount)
        {
          *dst = *src;
          --dst;
          --src;
        }
      }
    }
}
