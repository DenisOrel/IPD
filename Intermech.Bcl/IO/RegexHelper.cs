
// Type: Intermech.IO.RegexHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.IO
{
    /// <summary>
    /// Содержит методы по созданию наиболее распространенных regex'ов.
    /// </summary>
    public static class RegexHelper
    {
      /// <summary>
      /// Список символов в файловой маске, которые должны употребляться со слэшем
      /// </summary>
      private static readonly string[] filemaskEscapeFrom = new string[10]
      {
        "\\",
        ".",
        "$",
        "^",
        "{",
        "[",
        "(",
        "|",
        ")",
        "+"
      };
      /// <summary>Список символов со слэшем</summary>
      private static readonly string[] filemaskEscapeTo = new string[10]
      {
        "\\\\",
        "\\.",
        "\\$",
        "\\^",
        "\\{",
        "\\[",
        "\\(",
        "\\|",
        "\\)",
        "\\+"
      };

      /// <summary>
      /// Создает regex из файловой маски, содержащей символы подстановки * и ?.
      /// </summary>
      /// <param name="fileMask">Файловая маска</param>
      /// <param name="ignoreCase">Нужно ли игнорировать регистр символов</param>
      /// <returns>Созданные regex</returns>
      public static Regex ToRegex(string fileMask, bool ignoreCase)
      {
        RegexOptions options = RegexOptions.Singleline;
        if (ignoreCase)
          options |= RegexOptions.IgnoreCase;
        return new Regex(RegexHelper.ToRegexString(fileMask), options);
      }

      /// <summary>Переводит текст файловой маски в текст regex.</summary>
      /// <param name="fileMask">Текст файловой маски</param>
      /// <returns>Текст regex</returns>
      public static string ToRegexString(string fileMask)
      {
        if (string.IsNullOrEmpty(fileMask))
          throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces_747"), nameof (fileMask));
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(fileMask.Length + 8))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(fileMask);
          if (stringBuilder[0] == '+')
            stringBuilder.Remove(0, 1);
          if (stringBuilder.Length == 0)
            throw new InvalidOperationException(LocalizationHolder.rm.GetString("Interfaces_748"));
          for (int index = 0; index < RegexHelper.filemaskEscapeFrom.Length; ++index)
            stringBuilder.Replace(RegexHelper.filemaskEscapeFrom[index], RegexHelper.filemaskEscapeTo[index]);
          stringBuilder.Replace("*", ".*");
          stringBuilder.Replace('?', '.');
          stringBuilder.Insert(0, '^');
          stringBuilder.Append('$');
          return stringBuilder.ToString();
        }
      }
    }
}
