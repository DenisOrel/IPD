
// Type: Intermech.Diagnostics.MethodCallFormatter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Предоставляет базовую реализацию преобразования аргументов вызываемых методов в строковое представление.
    /// Данное преобразование используется при трассировке вызываемых методов.
    /// </summary>
    public class MethodCallFormatter : IMethodCallFormatter
    {
      private static readonly string nullString = "null";
      private static readonly string emptyListString = "[]";
      private static readonly string delegateString = "delegate";
      private static readonly string objectString = "object";

      /// <summary>
      /// Выполняет преобразование аргумента метода в текстовое представление.
      /// </summary>
      /// <param name="argument">Значение аргумента вызванного метода</param>
      /// <returns>Строковое представление аргумента</returns>
      public string FormatArgument(object argument) => this.DoFormatArgument(argument);

      /// <summary>
      /// Выполняет преобразование аргумента метода в текстовое представление.
      /// </summary>
      /// <param name="argument">Значение аргумента вызванного метода</param>
      /// <returns>Строковое представление аргумента</returns>
      protected virtual string DoFormatArgument(object argument)
      {
        switch (argument)
        {
          case null:
            return MethodCallFormatter.NullString;
          case string _:
            return this.FormatStringArgument((string) argument);
          case char ch:
            return this.FormatCharArgument(ch);
          case IList _:
            return this.FormatListArgument((IList) argument);
          case Enum _:
            return this.FormatEnumArgument(argument);
          default:
            return (object) (argument as Delegate) != null ? this.FormatDelegateArgument(argument) : this.FormatObjectArgument(argument);
        }
      }

      private string FormatStringArgument(string value)
      {
        if (value == string.Empty || object.Equals((object) value, (object) string.Empty))
          return "\"\"";
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(value.Length + 8))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(value);
          stringBuilder.Replace("\\", "\\\\");
          stringBuilder.Replace("\r", "\\r");
          stringBuilder.Replace("\n", "\\n");
          stringBuilder.Replace("\t", "\\t");
          stringBuilder.Replace("\"", "\\\"");
          stringBuilder.Insert(0, '"');
          stringBuilder.Append('"');
          return stringBuilder.ToString();
        }
      }

      private string FormatCharArgument(char value) => $"'{value.ToString()}'";

      private string FormatListArgument(IList list)
      {
        if (list.Count == 0)
          return MethodCallFormatter.EmptyListString;
        int num = list.Count;
        bool flag = false;
        if (num > 20)
        {
          num = 20;
          flag = true;
        }
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(24 * num))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append('[');
          stringBuilder.Append(this.DoFormatArgument(list[0]));
          for (int index = 1; index < num; ++index)
          {
            stringBuilder.Append(", ");
            stringBuilder.Append(this.DoFormatArgument(list[index]));
          }
          if (flag)
          {
            stringBuilder.Append(", ");
            stringBuilder.Append("...");
          }
          stringBuilder.Append(']');
          return stringBuilder.ToString();
        }
      }

      private string FormatEnumArgument(object enumValue)
      {
        Type type = enumValue.GetType();
        return $"{type.Name}.{Enum.GetName(type, enumValue)}";
      }

      private string FormatDelegateArgument(object value) => MethodCallFormatter.delegateString;

      private string FormatObjectArgument(object value)
      {
        Type type = value.GetType();
        if (type.IsCOMObject)
          return MethodCallFormatter.objectString;
        string objectString = value.ToString();
        if (objectString == type.ToString())
          objectString = MethodCallFormatter.objectString;
        return objectString;
      }

      /// <summary>Возвращает строковое представление null-значений.</summary>
      public static string NullString
      {
        [DebuggerStepThrough] get => MethodCallFormatter.nullString;
      }

      /// <summary>Возвращает строковое представление пустых списков.</summary>
      public static string EmptyListString
      {
        [DebuggerStepThrough] get => MethodCallFormatter.emptyListString;
      }
    }
}
