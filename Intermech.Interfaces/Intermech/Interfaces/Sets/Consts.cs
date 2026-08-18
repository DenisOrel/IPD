
// Type: Intermech.Interfaces.Sets.Consts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Sets
{
    /// <summary>Вспомогательный класс с константами для множеств</summary>
    public static class Consts
    {
      /// <summary>"Минус бесконечность" - '-∞'</summary>
      internal const string minusInfinity = "-∞";
      /// <summary>"Плюс бесконечность" - '∞'</summary>
      internal const string plusInfinity = "∞";
      /// <summary>Диапазон - '..'</summary>
      internal const string Range = "..";
      /// <summary>
      /// Разделитель между элементами кодированной строки - ','
      /// </summary>
      internal const char SplitterChar = ',';
      /// <summary>
      /// Разделитель между элементами кодированной строки - ':'
      /// </summary>
      internal const char RangeSplitterChar = ':';
      /// <summary>
      /// Разделитель между элементами кодированной строки - '|'
      /// </summary>
      internal const char SplitterMainChar = '|';
      /// <summary>
      /// Разделитель между элементами кодированной строки - '#'
      /// </summary>
      internal const char SplitterPartsChar = '#';
      /// <summary>
      /// Разделитель между элементами кодированной строки - ','
      /// </summary>
      internal static readonly string[] Splitter = new string[1]
      {
        ","
      };
      /// <summary>
      /// Разделитель между элементами кодированной строки - ':'
      /// </summary>
      internal static readonly string[] RangeSplitter = new string[1]
      {
        ":"
      };
      /// <summary>
      /// Разделитель между элементами кодированной строки - '|'
      /// </summary>
      internal static readonly string[] SplitterMain = new string[1]
      {
        "|"
      };
      /// <summary>
      /// Разделитель между элементами кодированной строки - '#'
      /// </summary>
      internal static readonly string[] SplitterParts = new string[1]
      {
        "#"
      };
      /// <summary>Разделитель между элементами даты - '.'</summary>
      internal static readonly string[] RangeSplitterDot = new string[1]
      {
        "."
      };
      /// <summary>Разделитель между элементами диапазона - '..'</summary>
      internal static readonly string[] RangeSplitterDots = new string[1]
      {
        ".."
      };
      /// <summary>Пустое множество - '{}'</summary>
      internal const string EmtpyRange = "{}";
      /// <summary>Левая граница диапазона - '{'</summary>
      internal const char rangeLeft = '{';
      /// <summary>Правая граница диапазона - '}'</summary>
      internal const char rangeRight = '}';
      /// <summary>Пустое множество чисел - '[]'</summary>
      internal const string EmtpyRangeInt = "[]";
      /// <summary>Левая граница диапазона - '['</summary>
      internal const char rangeIntLeft = '[';
      /// <summary>Правая граница диапазона - ']'</summary>
      internal const char rangeIntRight = ']';
      /// <summary>
      /// Значение, принимаемое за "минус бесконечность" - DateTime.MinValue.Date
      /// </summary>
      public static readonly DateTime dateMinusInfinity = DateTime.MinValue.Date;
      /// <summary>
      /// Значение, принимаемое за "плюс бесконечность" - DateTime.MaxValue.Date
      /// </summary>
      public static readonly DateTime datePlusInfinity = DateTime.MaxValue.Date;
      /// <summary>
      /// Значение, принимаемое за "минус бесконечность" - Int32.MinValue
      /// </summary>
      public const int intMinusInfinity = -2147483648 /*0x80000000*/;
      /// <summary>
      /// Значение, принимаемое за "плюс бесконечность" - Int32.MaxValue
      /// </summary>
      public const int intPlusInfinity = 2147483647 /*0x7FFFFFFF*/;
      /// <summary>Начальный номер изделий в сериях - 1</summary>
      public const int intStartIndex = 1;
    }
}
