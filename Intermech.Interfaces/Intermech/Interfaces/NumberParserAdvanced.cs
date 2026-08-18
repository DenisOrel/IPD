
// Type: Intermech.Interfaces.NumberParserAdvanced
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;


namespace Intermech.Interfaces
{
    /// <summary>Парсер чисел</summary>
    public class NumberParserAdvanced
    {
      /// <summary>Максимальное колечество цифр в числе</summary>
      public static int NUMBER_MAXDIGITS = 31 /*0x1F*/;
      /// <summary>Точность Int32</summary>
      public static int INT32_PRECISION = 10;
      /// <summary>Точность UInt32</summary>
      public static int UINT32_PRECISION = NumberParserAdvanced.INT32_PRECISION;
      /// <summary>Точность Int64</summary>
      public static int INT64_PRECISION = 19;
      /// <summary>Точность UInt64</summary>
      public static int UINT64_PRECISION = 20;
      /// <summary>Точность float</summary>
      public static int FLOAT_PRECISION = 7;
      /// <summary>Точность double</summary>
      public static int DOUBLE_PRECISION = 15;
      /// <summary>Точность Decimal</summary>
      public static int DECIMAL_PRECISION = 29;
      /// <summary>Максимальный размер буфера</summary>
      public static int LARGE_BUFFER_SIZE = 600;
      /// <summary>Минимальный размер буфера</summary>
      public static int MIN_BUFFER_SIZE = 105;
      /// <summary>Precomputed tables with powers of 10. These allows us to do at most
      /// two Mul64 during the conversion. This is important not only
      /// for speed, but also for precision because of Mul64 computes with 1 bit error.
      /// </summary>
      private static ulong[] rgval64Power10 = new ulong[30]
      {
        11529215046068469760UL /*0xA000000000000000*/,
        14411518807585587200UL /*0xC800000000000000*/,
        18014398509481984000UL /*0xFA00000000000000*/,
        11258999068426240000UL /*0x9C40000000000000*/,
        14073748835532800000UL /*0xC350000000000000*/,
        17592186044416000000UL /*0xF424000000000000*/,
        10995116277760000000UL /*0x9896800000000000*/,
        13743895347200000000UL /*0xBEBC200000000000*/,
        17179869184000000000UL /*0xEE6B280000000000*/,
        10737418240000000000UL /*0x9502F90000000000*/,
        13421772800000000000UL /*0xBA43B74000000000*/,
        16777216000000000000UL /*0xE8D4A51000000000*/,
        10485760000000000000UL,
        13107200000000000000UL,
        16384000000000000000UL,
        14757395258967641293UL,
        11805916207174113035UL,
        9444732965739290428UL,
        15111572745182864686UL,
        12089258196146291749UL,
        9671406556917033399UL,
        15474250491067253438UL,
        12379400392853802751UL,
        9903520314283042201UL,
        15845632502852867522UL,
        12676506002282294018UL,
        10141204801825835215UL,
        16225927682921336344UL,
        12980742146337069075UL,
        10384593717069655260UL
      };
      /// <summary>exponents for both powers of 10 and 0.1</summary>
      private static byte[] rgexp64Power10 = new byte[15]
      {
        (byte) 4,
        (byte) 7,
        (byte) 10,
        (byte) 14,
        (byte) 17,
        (byte) 20,
        (byte) 24,
        (byte) 27,
        (byte) 30,
        (byte) 34,
        (byte) 37,
        (byte) 40,
        (byte) 44,
        (byte) 47,
        (byte) 50
      };
      /// <summary>exponents for both powers of 10^16 and 0.1^16</summary>
      private static short[] rgexp64Power10By16 = new short[21]
      {
        (short) 54,
        (short) 107,
        (short) 160 /*0xA0*/,
        (short) 213,
        (short) 266,
        (short) 319,
        (short) 373,
        (short) 426,
        (short) 479,
        (short) 532,
        (short) 585,
        (short) 638,
        (short) 691,
        (short) 745,
        (short) 798,
        (short) 851,
        (short) 904,
        (short) 957,
        (short) 1010,
        (short) 1064,
        (short) 1117
      };
      /// <summary>powers of 10^16</summary>
      private static ulong[] rgval64Power10By16 = new ulong[42]
      {
        10240000000000000000UL,
        11368683772161602974UL,
        12621774483536188886UL,
        14012984643248170708UL,
        15557538194652854266UL,
        17272337110188889248UL,
        9588073174409622172UL,
        10644899600020376798UL,
        11818212630765741798UL,
        13120851772591970216UL,
        14567071740625403792UL,
        16172698447808779622UL,
        17955302187076837696UL,
        9967194951097567532UL,
        11065809325636130658UL,
        12285516299433008778UL,
        13639663065038175358UL,
        15143067982934716296UL,
        16812182738118149112UL,
        9332636185032188787UL,
        10361307573072618722UL,
        16615349947311448416UL,
        14965776766268445891UL,
        13479973333575319909UL,
        12141680576410806707UL,
        10936253623915059637UL,
        9850501549098619819UL,
        17745086042373215136UL,
        15983352577617880260UL,
        14396524142538228461UL,
        12967236152753103031UL,
        11679847981112819795UL,
        10520271803096747049UL,
        9475818434452569218UL,
        17070116948172427008UL,
        15375394465392026135UL,
        13848924157002783096UL,
        12474001934591998882UL,
        11235582092889474480UL,
        10120112665365530972UL,
        18230774251475056952UL,
        16420821625123739930UL
      };

      /// <summary>Helper method to multiply two 32-bit uints</summary>
      private static ulong Mul32x32To64(uint a, uint b) => (ulong) a * (ulong) b;

      /// <summary>Multiply two numbers in the internal integer representation</summary>
      private static ulong Mul64Lossy(ulong a, ulong b, ref int pexp)
      {
        ulong num = NumberParserAdvanced.Mul32x32To64((uint) (a >> 32 /*0x20*/), (uint) (b >> 32 /*0x20*/)) + (NumberParserAdvanced.Mul32x32To64((uint) (a >> 32 /*0x20*/), (uint) b) >> 32 /*0x20*/) + (NumberParserAdvanced.Mul32x32To64((uint) a, (uint) (b >> 32 /*0x20*/)) >> 32 /*0x20*/);
        if (((long) num & long.MinValue) == 0L)
        {
          num <<= 1;
          --pexp;
        }
        return num;
      }

      private static bool ISWHITE(char ch)
      {
        int num = (int) ch;
        if (num == 32 /*0x20*/)
          return true;
        return num >= 9 && num <= 13;
      }

      /// <summary>Попытаться преобразовать текст в число Int32</summary>
      /// <param name="str">Строка</param>
      /// <param name="intValue">Число</param>
      /// <returns>true, если успешно</returns>
      public static bool TryParseInt32(string str, out int intValue)
      {
        intValue = 0;
        ParsedNumberData number = new ParsedNumberData();
        NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
        ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.LEADINGSIGN;
        int numberBegin = 0;
        int numberLength = 0;
        return NumberParserAdvanced.ParseNumber(str, 0, options, number, currentInfo, out numberBegin, out numberLength) && NumberParserAdvanced.NumberToInt32(number, out intValue);
      }

      /// <summary>Попытаться преобразовать текст в число Int32 из любого текста.
      /// Впереди и позади числа могут любые символы. Извлекает только первое число.</summary>
      /// <param name="str">Строка</param>
      /// <param name="intValue">Число</param>
      /// <returns>true, если успешно</returns>
      public static bool TryParseInt32FromAnyText(string str, out int intValue)
      {
        intValue = 0;
        ParsedNumberData number = new ParsedNumberData();
        NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
        ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.LEADINGSIGN | ParserOptions.DECIMAL | ParserOptions.THOUSANDS | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
        int numberBegin = 0;
        int numberLength = 0;
        return NumberParserAdvanced.ParseNumber(str, 0, options, number, currentInfo, out numberBegin, out numberLength) && NumberParserAdvanced.NumberToInt32(number, out intValue);
      }

      /// <summary>Попытаться преобразовать текст в число Double. Текст до и после числа игнорируется</summary>
      /// <param name="str">Строка</param>
      /// <param name="doubleValue">Число</param>
      /// <returns>true, если успешно</returns>
      public static bool TryParseDouble(string str, out double doubleValue)
      {
        doubleValue = 0.0;
        ParsedNumberData number = new ParsedNumberData();
        NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
        ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.LEADINGSIGN | ParserOptions.DECIMAL | ParserOptions.SCIENTIFIC | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
        int numberBegin = 0;
        int numberLength = 0;
        return NumberParserAdvanced.ParseNumber(str, 0, options, number, currentInfo, out numberBegin, out numberLength) && NumberParserAdvanced.NumberToDouble(number, out doubleValue);
      }

      /// <summary>Возвращает символ с индексом.
      /// Если индекс = длине, то возвращает 0 как будто это массив char</summary>
      /// <param name="str">Строка</param>
      /// <param name="index">Индекс</param>
      /// <returns>Символ</returns>
      private static char _char(string str, int index)
      {
        return index == str.Length ? char.MinValue : str[index];
      }

      private static int MatchChars(string str, string subString, int start)
      {
        if (str == null || str == "")
          return 0;
        int index;
        for (index = 0; index < subString.Length; ++index)
        {
          if (index + start >= str.Length || (int) str[start + index] != (int) subString[index] && (subString[index] != ' ' || str[index] != ' '))
            return 0;
        }
        return index + start;
      }

      private static bool IsDigit(char ch, bool hex)
      {
        if (ch >= '0' && ch <= '9')
          return true;
        if (!hex)
          return false;
        if (ch >= 'a' && ch <= 'f')
          return true;
        return ch >= 'A' && ch <= 'F';
      }

      /// <summary>Разобрать строковое представление числа</summary>
      /// <param name="text">Строковое представление числа</param>
      /// <param name="startIndex">Индекс начала подстроки в которой нужно искать число</param>
      /// <param name="options">Опции разбора</param>
      /// <param name="number">Класс в который будут помещены результаты разбора</param>
      /// <param name="numfmt">Информация о формате числа</param>
      /// <param name="numberBegin">Возвращает начало подстроки найденного числа</param>
      /// <param name="numberLength">Возвращает длину подстроки найденного числа</param>
      /// <returns>true, если найдено число согласно опциям и формату</returns>
      public static bool ParseNumber(
        string text,
        int startIndex,
        ParserOptions options,
        ParsedNumberData number,
        NumberFormatInfo numfmt,
        out int numberBegin,
        out int numberLength)
      {
        numberBegin = -1;
        numberLength = 0;
        int num1 = startIndex - 1;
        if (text == null || text == "")
          return false;
        number.scale = 0;
        number.sign = 0;
        bool flag1 = (options & ParserOptions.UseRusAndEnDecimalSeparators) != 0;
        string subString1 = (string) null;
        string subString2 = (string) null;
        string subString3 = (string) null;
        string subString4 = (string) null;
        bool hex = (options & ParserOptions.HEX) > (ParserOptions) 0;
        int num2 = (options & ParserOptions.CURRENCY) > (ParserOptions) 0 ? 1 : 0;
        bool flag2 = (options & ParserOptions.DECIMAL) > (ParserOptions) 0;
        bool flag3 = (options & ParserOptions.SkipLeadingText) > (ParserOptions) 0;
        string subString5;
        string subString6;
        if (num2 != 0)
        {
          subString1 = numfmt.CurrencySymbol;
          subString3 = numfmt.NumberDecimalSeparator;
          subString4 = numfmt.NumberGroupSeparator;
          subString5 = numfmt.CurrencyGroupSeparator;
          subString6 = numfmt.CurrencyDecimalSeparator;
        }
        else
        {
          subString5 = numfmt.NumberDecimalSeparator;
          subString6 = numfmt.NumberGroupSeparator;
        }
        string str1 = subString1;
        string str2 = subString2;
        State state = (State) 0;
        int num3 = startIndex;
        char ch = NumberParserAdvanced._char(text, num3);
        NumberParserAdvanced.MatchChars(text, numfmt.PositiveSign, num3);
        for (; ch != char.MinValue; ch = NumberParserAdvanced._char(text, ++num3))
        {
          if (NumberParserAdvanced.ISWHITE(ch) && (options & ParserOptions.LEADINGWHITE) > (ParserOptions) 0 && ((state & State.SIGN) <= (State) 0 || (state & State.SIGN) > (State) 0 && ((state & State.CURRENCY) > (State) 0 || numfmt.NumberNegativePattern == 2)))
          {
            if (num3 - num1 == 1)
              num1 = num3;
          }
          else
          {
            bool flag4;
            int num4;
            if ((flag4 = (options & ParserOptions.LEADINGSIGN) != (ParserOptions) 0 && (state & State.SIGN) == (State) 0) && (num4 = NumberParserAdvanced.MatchChars(text, numfmt.PositiveSign, num3)) != 0)
            {
              state |= State.SIGN;
              num3 = num4 - 1;
            }
            else
            {
              int num5;
              if (flag4 && (num5 = NumberParserAdvanced.MatchChars(text, numfmt.NegativeSign, num3)) != 0)
              {
                state |= State.SIGN;
                number.sign = 1;
                num3 = num5 - 1;
              }
              else if (ch == '(' && (options & ParserOptions.PARENS) > (ParserOptions) 0 && (state & State.SIGN) <= (State) 0)
              {
                state |= State.SIGN | State.PARENS;
                number.sign = 1;
              }
              else
              {
                int num6;
                if (subString1 != null && (num6 = NumberParserAdvanced.MatchChars(text, subString1, num3)) != 0 || subString2 != null && (num6 = NumberParserAdvanced.MatchChars(text, subString2, num3)) != 0)
                {
                  state |= State.CURRENCY;
                  subString1 = (string) null;
                  subString2 = (string) null;
                  num3 = num6 - 1;
                }
                else
                {
                  int index;
                  if (flag3 && !NumberParserAdvanced.IsDigit(ch, hex) && (!flag2 || (index = NumberParserAdvanced.MatchChars(text, subString5, num3)) == 0 && ((state & State.CURRENCY) == (State) 0 || (index = NumberParserAdvanced.MatchChars(text, subString3, num3)) == 0) && (!flag1 || (index = NumberParserAdvanced.MatchChars(text, ".", num3)) == 0) && (!flag1 || (index = NumberParserAdvanced.MatchChars(text, ",", num3)) == 0) || !NumberParserAdvanced.IsDigit(NumberParserAdvanced._char(text, index), hex)))
                  {
                    num1 = num3;
                    if ((state & State.CURRENCY) > (State) 0)
                    {
                      state &= ~State.CURRENCY;
                      subString1 = str1;
                      subString2 = str2;
                    }
                    if ((state & State.SIGN) > (State) 0)
                    {
                      state &= ~State.SIGN;
                      number.sign = 0;
                    }
                  }
                  else
                    break;
                }
              }
            }
          }
        }
        int num7 = 0;
        int index1 = 0;
        for (; ch != char.MinValue; ch = NumberParserAdvanced._char(text, ++num3))
        {
          if (NumberParserAdvanced.IsDigit(ch, hex))
          {
            state |= State.DIGITS;
            if (ch != '0' || (state & State.NONZERO) > (State) 0)
            {
              if (num7 < NumberParserAdvanced.NUMBER_MAXDIGITS)
              {
                number.digits[num7++] = ch;
                if (ch != '0')
                  index1 = num7;
              }
              if ((state & State.DECIMAL) <= (State) 0)
                ++number.scale;
              state |= State.NONZERO;
            }
            else if ((state & State.DECIMAL) > (State) 0)
              --number.scale;
          }
          else
          {
            int num8;
            if (flag2 && (state & State.DECIMAL) <= (State) 0 && ((num8 = NumberParserAdvanced.MatchChars(text, subString5, num3)) != 0 || (state & State.CURRENCY) != (State) 0 && (num8 = NumberParserAdvanced.MatchChars(text, subString3, num3)) != 0 || flag1 && (num8 = NumberParserAdvanced.MatchChars(text, ".", num3)) != 0 || flag1 && (num8 = NumberParserAdvanced.MatchChars(text, ",", num3)) != 0))
            {
              state |= State.DECIMAL;
              num3 = num8 - 1;
            }
            else
            {
              int num9;
              if ((options & ParserOptions.THOUSANDS) > (ParserOptions) 0 && (state & State.DIGITS) > (State) 0 && (state & State.DECIMAL) <= (State) 0 && ((num9 = NumberParserAdvanced.MatchChars(text, subString6, num3)) != 0 || (state & State.CURRENCY) != (State) 0 && (num9 = NumberParserAdvanced.MatchChars(text, subString4, num3)) != 0))
                num3 = num9 - 1;
              else
                break;
            }
          }
        }
        int num10 = 0;
        number.precision = index1;
        number.digits[index1] = char.MinValue;
        if ((state & State.DIGITS) > (State) 0)
        {
          if ((ch == 'E' || ch == 'e') && (options & ParserOptions.SCIENTIFIC) > (ParserOptions) 0)
          {
            int num11 = num3;
            ch = NumberParserAdvanced._char(text, ++num3);
            int num12;
            if ((num12 = NumberParserAdvanced.MatchChars(text, numfmt.PositiveSign, num3)) != 0)
            {
              num3 = num12;
              ch = NumberParserAdvanced._char(text, num3);
            }
            else
            {
              int num13;
              if ((num13 = NumberParserAdvanced.MatchChars(text, numfmt.NegativeSign, num3)) != 0)
              {
                num3 = num13;
                ch = NumberParserAdvanced._char(text, num3);
                num10 = 1;
              }
            }
            if (ch >= '0' && ch <= '9')
            {
              int num14 = 0;
              do
              {
                num14 = num14 * 10 + ((int) ch - 48 /*0x30*/);
                ch = NumberParserAdvanced._char(text, ++num3);
                if (num14 > 1000)
                {
                  num14 = 9999;
                  while (ch >= '0' && ch <= '9')
                    ch = NumberParserAdvanced._char(text, ++num3);
                }
              }
              while (ch >= '0' && ch <= '9');
              if (num10 > 0)
                num14 = -num14;
              number.scale += num14;
            }
            else
            {
              int num15 = num11;
              ch = NumberParserAdvanced._char(text, num3 = num15 + 1);
            }
          }
          int num16 = 0;
          while (true)
          {
            if (NumberParserAdvanced.ISWHITE(ch) && (options & ParserOptions.TRAILINGWHITE) > (ParserOptions) 0)
            {
              ++num16;
            }
            else
            {
              bool flag5;
              int num17;
              if (!(flag5 = (options & ParserOptions.TRAILINGSIGN) > (ParserOptions) 0 && (state & State.SIGN) <= (State) 0) && (num17 = NumberParserAdvanced.MatchChars(text, numfmt.PositiveSign, num3)) != 0)
              {
                state |= State.SIGN;
                num3 = num17 - 1;
                num16 = 0;
              }
              else
              {
                int num18;
                if (flag5 && (num18 = NumberParserAdvanced.MatchChars(text, numfmt.NegativeSign, num3)) != 0)
                {
                  state |= State.SIGN;
                  number.sign = 1;
                  num3 = num18 - 1;
                  num16 = 0;
                }
                else if (ch == ')' && (state & State.PARENS) > (State) 0)
                {
                  state &= ~State.PARENS;
                  num16 = 0;
                }
                else
                {
                  int num19;
                  if (subString1 != null && (num19 = NumberParserAdvanced.MatchChars(text, subString1, num3)) != 0 || subString2 != null && (num19 = NumberParserAdvanced.MatchChars(text, subString2, num3)) != 0)
                  {
                    subString1 = (string) null;
                    subString2 = (string) null;
                    num3 = num19 - 1;
                    num16 = 0;
                  }
                  else
                    break;
                }
              }
            }
            ch = NumberParserAdvanced._char(text, ++num3);
          }
          if ((state & State.PARENS) <= (State) 0)
          {
            if ((state & State.NONZERO) <= (State) 0)
            {
              number.scale = 0;
              if ((state & State.DECIMAL) <= (State) 0)
                number.sign = 0;
            }
            numberBegin = num1 + 1;
            numberLength = num3 - numberBegin - num16;
            return num3 == text.Length || (options & ParserOptions.IgnoreTrailingText) > (ParserOptions) 0 || (options & ParserOptions.SkipLeadingText) > (ParserOptions) 0;
          }
        }
        numberBegin = -1;
        numberLength = 0;
        return false;
      }

      /// <summary>Найти число в тексте</summary>
      /// <param name="text">Текст</param>
      /// <param name="rusAndEngDecimalSeparator">Использовать и точку и запятую как десятичный разделитель</param>
      /// <param name="textBeforeNumber">Текст перед числом</param>
      /// <param name="textNumber">Текст с числом</param>
      /// <param name="textAfterNumber">Текст после числа</param>
      /// <returns>true, если найдено число</returns>
      public static bool ParseNumber(
        string text,
        bool rusAndEngDecimalSeparator,
        out string textBeforeNumber,
        out string textNumber,
        out string textAfterNumber)
      {
        textBeforeNumber = "";
        textNumber = "";
        textAfterNumber = "";
        if (text == null || text == "")
          return false;
        ParsedNumberData number = new ParsedNumberData();
        NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
        ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.LEADINGSIGN | ParserOptions.DECIMAL | ParserOptions.SCIENTIFIC | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
        if (rusAndEngDecimalSeparator)
          options |= ParserOptions.UseRusAndEnDecimalSeparators;
        int numberBegin = 0;
        int numberLength = 0;
        if (!NumberParserAdvanced.ParseNumber(text, 0, options, number, currentInfo, out numberBegin, out numberLength))
          return false;
        if (numberBegin > 0)
          textBeforeNumber = text.Substring(0, numberBegin);
        if (numberLength > 0)
          textNumber = text.Substring(numberBegin, numberLength);
        if (numberBegin + numberLength < text.Length)
          textAfterNumber = text.Substring(numberBegin + numberLength);
        return true;
      }

      /// <summary>Найти и преобразовать число в тексте</summary>
      /// <param name="text">Текст</param>
      /// <param name="rusAndEngDecimalSeparator">Использовать и точку и запятую как десятичный разделитель</param>
      /// <param name="number">Число</param>
      /// <param name="textBeforeNumber">Текст перед числом</param>
      /// <param name="textAfterNumber">Текст после числа</param>
      /// <returns>true, если найдено число</returns>
      public static bool ParseNumber(
        string text,
        bool rusAndEngDecimalSeparator,
        out double number,
        out string textBeforeNumber,
        out string textAfterNumber)
      {
        number = 0.0;
        textBeforeNumber = "";
        textAfterNumber = "";
        if (text == null || text == "")
          return false;
        ParsedNumberData number1 = new ParsedNumberData();
        NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
        ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.LEADINGSIGN | ParserOptions.DECIMAL | ParserOptions.SCIENTIFIC | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
        if (rusAndEngDecimalSeparator)
          options |= ParserOptions.UseRusAndEnDecimalSeparators;
        int numberBegin = 0;
        int numberLength = 0;
        if (!NumberParserAdvanced.ParseNumber(text, 0, options, number1, currentInfo, out numberBegin, out numberLength))
          return false;
        if (numberBegin > 0)
          textBeforeNumber = text.Substring(0, numberBegin);
        if (numberBegin + numberLength < text.Length)
          textAfterNumber = text.Substring(numberBegin + numberLength);
        return NumberParserAdvanced.NumberToDouble(number1, out number);
      }

      /// <summary>Найти в тексте и преобразовать целое число без знака</summary>
      /// <param name="text">Текст</param>
      /// <param name="number">Число</param>
      /// <param name="textBeforeNumber">Текст перед числом</param>
      /// <param name="textAfterNumber">Текст после числа</param>
      /// <returns>true, если найдено число</returns>
      public static bool ParseUnsignedInteger(
        string text,
        out long number,
        out string textBeforeNumber,
        out string textAfterNumber)
      {
        number = 0L;
        textBeforeNumber = "";
        textAfterNumber = "";
        if (string.IsNullOrEmpty(text))
          return false;
        ParsedNumberData number1 = new ParsedNumberData();
        NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
        ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
        int numberBegin = 0;
        int numberLength = 0;
        if (!NumberParserAdvanced.ParseNumber(text, 0, options, number1, currentInfo, out numberBegin, out numberLength))
          return false;
        if (numberBegin > 0)
          textBeforeNumber = text.Substring(0, numberBegin);
        if (numberBegin + numberLength < text.Length)
          textAfterNumber = text.Substring(numberBegin + numberLength);
        return long.TryParse(text.Substring(numberBegin, numberLength), out number);
      }

      /// <summary>Преобразовать результат разбора числа в число Int32</summary>
      /// <param name="number">Результат разбора числа</param>
      /// <param name="intValue">Число</param>
      /// <returns>true, если преобразование прошло успешно</returns>
      public static bool NumberToInt32(ParsedNumberData number, out int intValue)
      {
        intValue = 0;
        int scale = number.scale;
        if (scale > NumberParserAdvanced.INT32_PRECISION || scale < number.precision)
          return false;
        int index = 0;
        int num = 0;
        while (--scale >= 0)
        {
          if ((uint) num > 214748364U /*0x0CCCCCCC*/)
            return false;
          num *= 10;
          if (number.digits[index] > char.MinValue)
            num += (int) number.digits[index++] - 48 /*0x30*/;
        }
        if (number.sign > 0)
        {
          num = -num;
          if (num > 0)
            return false;
        }
        else if (num < 0)
          return false;
        intValue = num;
        return true;
      }

      /// <summary>Преобразовать результат разбора числа в число double</summary>
      /// <param name="number">Результат разбора числа</param>
      /// <param name="value">Число</param>
      /// <returns>true, если преобразование прошло успешно</returns>
      public static bool NumberToDouble(ParsedNumberData number, out double value)
      {
        value = 0.0;
        ulong num1 = 0;
        int length = 0;
        while (length < number.digits.Length && number.digits[length] != char.MinValue)
          ++length;
        string str = new string(number.digits, 0, length);
        int num2 = 0;
        int val1_1 = length;
        for (; num2 < str.Length && str[num2] == '0'; ++num2)
          --val1_1;
        if (val1_1 == 0)
        {
          value = 0.0;
        }
        else
        {
          int count1 = Math.Min(val1_1, 9);
          int val1_2 = val1_1 - count1;
          ulong a = (ulong) NumberParserAdvanced.DigitsToInt(str, num2, count1);
          if (val1_2 > 0)
          {
            int count2 = Math.Min(val1_2, 9);
            val1_2 -= count2;
            uint b = (uint) (NumberParserAdvanced.rgval64Power10[count2 - 1] >> 64 /*0x40*/ - (int) NumberParserAdvanced.rgexp64Power10[count2 - 1]);
            a = NumberParserAdvanced.Mul32x32To64((uint) a, b) + (ulong) NumberParserAdvanced.DigitsToInt(str, num2 + 9, count2);
          }
          int num3 = number.scale - (length - val1_2);
          int num4 = Math.Abs(num3);
          if (num4 >= 352)
          {
            num1 = num3 > 0 ? 9218868437227405312UL /*0x7FF0000000000000*/ : 0UL;
          }
          else
          {
            int pexp = 64 /*0x40*/;
            if (((long) a & -4294967296L) == 0L)
            {
              a <<= 32 /*0x20*/;
              pexp -= 32 /*0x20*/;
            }
            if (((long) a & -281474976710656L /*0xFFFF000000000000*/) == 0L)
            {
              a <<= 16 /*0x10*/;
              pexp -= 16 /*0x10*/;
            }
            if (((long) a & -72057594037927936L /*0xFF00000000000000*/) == 0L)
            {
              a <<= 8;
              pexp -= 8;
            }
            if (((long) a & -1152921504606846976L /*0xF000000000000000*/) == 0L)
            {
              a <<= 4;
              pexp -= 4;
            }
            if (((long) a & -4611686018427387904L /*0xC000000000000000*/) == 0L)
            {
              a <<= 2;
              pexp -= 2;
            }
            if (((long) a & long.MinValue) == 0L)
            {
              a <<= 1;
              --pexp;
            }
            int num5 = num4 & 15;
            if (num5 > 0)
            {
              int num6 = (int) NumberParserAdvanced.rgexp64Power10[num5 - 1];
              pexp += num3 < 0 ? -num6 + 1 : num6;
              ulong b = NumberParserAdvanced.rgval64Power10[num5 + (num3 < 0 ? 15 : 0) - 1];
              a = NumberParserAdvanced.Mul64Lossy(a, b, ref pexp);
            }
            int num7 = num4 >> 4;
            if (num7 > 0)
            {
              int num8 = (int) NumberParserAdvanced.rgexp64Power10By16[num7 - 1];
              pexp += num3 < 0 ? -num8 + 1 : num8;
              ulong b = NumberParserAdvanced.rgval64Power10By16[num7 + (num3 < 0 ? 21 : 0) - 1];
              a = NumberParserAdvanced.Mul64Lossy(a, b, ref pexp);
            }
            ulong num9;
            if (((uint) a & 1024U /*0x0400*/) > 0U)
            {
              num9 = a + (ulong) (uint) (1023 /*0x03FF*/ + ((int) ((uint) a >> 11) & 1)) >> 11;
              if (num9 == 0UL)
                ++pexp;
            }
            else
              num9 = a >> 11;
            pexp += 1022;
            num1 = pexp > 0 ? (pexp < 2047 /*0x07FF*/ ? (ulong) (((long) pexp << 52) + ((long) num9 & 4503599627370495L /*0x0FFFFFFFFFFFFF*/)) : 9218868437227405312UL /*0x7FF0000000000000*/) : (pexp > -52 ? num9 >> -pexp + 1 : 0UL);
          }
        }
        if (number.sign > 0)
          num1 |= 9223372036854775808UL /*0x8000000000000000*/;
        long int64 = BitConverter.ToInt64(BitConverter.GetBytes(num1), 0);
        value = BitConverter.Int64BitsToDouble(int64);
        return true;
      }

      private static uint DigitsToInt(string str, int p, int count)
      {
        int num1 = p + count;
        uint num2 = (uint) str[p] - 48U /*0x30*/;
        for (++p; p < num1; ++p)
          num2 = (uint) (10 * (int) num2 + (int) str[p] - 48 /*0x30*/);
        return num2;
      }

      private static double DigitsToDouble(string str, int p, int count)
      {
        int num1 = p + count;
        double num2 = (double) ((uint) str[p] - 48U /*0x30*/);
        for (++p; p < num1; ++p)
          num2 = 10.0 * num2 + (double) str[p] - 48.0;
        return num2;
      }
    }
}
