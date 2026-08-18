
// Type: Intermech.Data.SqlUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Data
{
    public static class SqlUtils
    {
      private static readonly string[,] sqlQuotes = new string[1, 2]
      {
        {
          "'",
          "''"
        }
      };
      private static readonly string[,] likePatterns = new string[3, 2]
      {
        {
          "$",
          "$$"
        },
        {
          "%",
          "$%"
        },
        {
          "_",
          "$_"
        }
      };

      public static IDbDataParameter MakeParameter(
        IDbCommand dbCommand,
        string paramName,
        DbType paramType)
      {
        IDbDataParameter dbDataParameter = dbCommand != null ? dbCommand.CreateParameter() : throw new ArgumentNullException(nameof (dbCommand));
        dbCommand.Parameters.Add((object) dbDataParameter);
        dbDataParameter.ParameterName = paramName;
        dbDataParameter.DbType = paramType;
        return dbDataParameter;
      }

      public static IDbDataParameter CopyParameter(IDbCommand dbCommand, IDbDataParameter sourceParam)
      {
        if (sourceParam == null)
          throw new ArgumentNullException(nameof (sourceParam));
        IDbDataParameter dbDataParameter = SqlUtils.MakeParameter(dbCommand, sourceParam.ParameterName, sourceParam.DbType);
        dbDataParameter.Value = sourceParam.Value;
        return dbDataParameter;
      }

      public static string StringLiteral(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(text.Length * 2 + 2))
        {
          StringBuilder sb = objectPoolScope.Object;
          sb.Append(text);
          SqlUtils.GuardEscapeSymbols(sb, SqlUtils.sqlQuotes, false);
          SqlUtils.ApplyLiteralQuotes(sb);
          return sb.ToString();
        }
      }

      public static string LikeLiteral(string text, bool anyStart, bool anyStop)
      {
        if (string.IsNullOrEmpty(text))
          throw new ArgumentException(LocalizationHolder.rm.GetString("SR_1676"), nameof (text));
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(text.Length * 2 + 3))
        {
          StringBuilder sb = objectPoolScope.Object;
          sb.Append(text);
          SqlUtils.GuardEscapeSymbols(sb, SqlUtils.sqlQuotes, false);
          SqlUtils.GuardEscapeSymbols(sb, SqlUtils.likePatterns, false);
          if (anyStart)
            sb.Insert(0, '%');
          if (anyStop)
            sb.Append('%');
          SqlUtils.ApplyLiteralQuotes(sb);
          return sb.ToString();
        }
      }

      public static string WildcardLiteral(string text)
      {
        if (string.IsNullOrEmpty(text))
          throw new ArgumentException(LocalizationHolder.rm.GetString("SR_1676"), nameof (text));
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(text.Length * 2 + 2))
        {
          StringBuilder sb = objectPoolScope.Object;
          sb.Append(text);
          SqlUtils.GuardEscapeSymbols(sb, SqlUtils.sqlQuotes, false);
          SqlUtils.GuardEscapeSymbols(sb, SqlUtils.likePatterns, false);
          sb.Replace("*", "%");
          sb.Replace("?", "_");
          SqlUtils.ApplyLiteralQuotes(sb);
          return sb.ToString();
        }
      }

      private static void ApplyLiteralQuotes(StringBuilder sb)
      {
        sb.Insert(0, '\'');
        sb.Append('\'');
      }

      private static void GuardEscapeSymbols(
        StringBuilder sb,
        string[,] replaceTable,
        bool ignoreQuotes)
      {
        int startIndex = ignoreQuotes ? 1 : 0;
        for (int index = 0; index < replaceTable.GetLength(0); ++index)
        {
          int count = ignoreQuotes ? sb.Length - 2 : sb.Length;
          sb.Replace(replaceTable[index, 0], replaceTable[index, 1], startIndex, count);
        }
      }

      public static string ListToString<T>(
        IList<T> list,
        Converter<T, string> generator,
        string listSeparator)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (list.Count == 0)
          throw new ArgumentOutOfRangeException(nameof (list), LocalizationHolder.rm.GetString("SR_1677"));
        if (string.IsNullOrEmpty(listSeparator))
          throw new ArgumentException(LocalizationHolder.rm.GetString("SR_1676"), nameof (listSeparator));
        string[] strArray = new string[list.Count];
        int num = 0;
        for (int index = 0; index < list.Count; ++index)
        {
          strArray[index] = generator(list[index]);
          if (strArray[index].Length > num)
            num = strArray[index].Length;
        }
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(list.Count * (num + listSeparator.Length + 2)))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(strArray[0]);
          for (int index = 1; index < list.Count; ++index)
          {
            if (listSeparator.Length > 1)
              stringBuilder.Append(' ');
            stringBuilder.Append(listSeparator);
            stringBuilder.Append(' ');
            stringBuilder.Append(strArray[index]);
          }
          return stringBuilder.ToString();
        }
      }

      public static string ListToString<T>(IList<T> list, Converter<T, string> generator)
      {
        return SqlUtils.ListToString<T>(list, generator, ",");
      }

      public static string ListToString(IList<int> list)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (list.Count == 0)
          throw new ArgumentOutOfRangeException(nameof (list), LocalizationHolder.rm.GetString("SR_1677"));
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(24 * list.Count))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(list[0]);
          for (int index = 1; index < list.Count; ++index)
          {
            stringBuilder.Append(',');
            stringBuilder.Append(' ');
            stringBuilder.Append(list[index]);
          }
          return stringBuilder.ToString();
        }
      }
    }
}
