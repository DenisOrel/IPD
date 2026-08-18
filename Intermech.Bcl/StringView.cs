
// Type: Intermech.StringView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech
{
    /// <summary>Структура для описания фрагмента строки.</summary>
    public struct StringView
    {
      private int startIndex;
      private int length;

      /// <summary>Создает объект.</summary>
      /// <param name="startIndex">Индекс символа в строке, с которого начинается фрагмент</param>
      /// <param name="length">Длина фрагмента в символах, может быть равна 0</param>
      public StringView(int startIndex, int length)
      {
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (startIndex));
        if (length < 0)
          throw new ArgumentOutOfRangeException(nameof (length));
        this.startIndex = startIndex;
        this.length = length;
      }

      /// <summary>
      /// Возвращает индекс символа в строке, с которого начинается фрагмент.
      /// </summary>
      public int StartIndex
      {
        [DebuggerStepThrough] get => this.startIndex;
      }

      /// <summary>Возвращает длину фрагмента строки в символах</summary>
      public int Length
      {
        [DebuggerStepThrough] get => this.length;
      }

      /// <summary>
      /// Возвращает признак, что это пустой фрагмент строки. Его длина равна 0.
      /// </summary>
      public bool IsEmpty => this.Length == 0;
    }
}
