
// Type: Intermech.IniFiles.IniFileBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.IniFiles
{
    /// <summary>
    /// Реализует основу для создания классов, читающих содержимое ini-файлов.
    /// </summary>
    public class IniFileBase
    {
      private Dictionary<string, string> values;
      private List<string> _sectionNames = new List<string>();
      private Dictionary<string, List<string>> _valueNames = new Dictionary<string, List<string>>();
      /// <summary>
      /// Массив выражений для деления текста на отдельные строки.
      /// </summary>
      private static readonly string[] TextSplitPatterns = new string[3]
      {
        "\n\r",
        "\n",
        "\r"
      };
      /// <summary>
      /// Массив выражений для деления текстовой строки на отдельные элементы.
      /// </summary>
      private static readonly char[] LineSplitPatterns = new char[1]
      {
        ' '
      };

      /// <summary>Создает объект.</summary>
      public IniFileBase() => this.values = new Dictionary<string, string>();

      /// <summary>
      /// Инициализирует объект содержимим ini-файла. Вызывается из конструкторов классов-потомков.
      /// </summary>
      /// <param name="content">Содержимое ini-файла</param>
      protected void Initialize(string content)
      {
        string[] strArray = !string.IsNullOrEmpty(content) ? content.Split(IniFileBase.TextSplitPatterns, StringSplitOptions.RemoveEmptyEntries) : throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces_568"), nameof (content));
        List<string> stringList = (List<string>) null;
        this.values.Clear();
        string key = (string) null;
        for (int index = 0; index < strArray.Length; ++index)
        {
          IniFileBase.Token token = this.ParseToken(strArray[index].Trim());
          switch (token.TokenType)
          {
            case IniFileBase.TokenType.Section:
              key = token.Items[0];
              if (!this._sectionNames.Contains(key))
                this._sectionNames.Add(key);
              if (this._valueNames.ContainsKey(key))
              {
                stringList = this._valueNames[key];
                break;
              }
              stringList = new List<string>();
              this._valueNames[key] = stringList;
              break;
            case IniFileBase.TokenType.Value:
              if (key == null)
                throw new InvalidOperationException(LocalizationHolder.rm.GetString("Interfaces_569"));
              string str = token.Items[0];
              this.values.Add(key + str, token.Items[1]);
              if (stringList != null && !stringList.Contains(str))
              {
                stringList.Add(str);
                break;
              }
              break;
          }
        }
      }

      /// <summary>Возвращает значение ключа из ini-файла.</summary>
      /// <param name="section">Имя секции</param>
      /// <param name="keyName">Имя кдюча</param>
      /// <param name="defaultValue">Значение по умолчанию, которое используется в случае отсутствия в файле указанного ключа</param>
      /// <returns>Значение ключа</returns>
      public string ReadString(string section, string keyName, string defaultValue)
      {
        if (string.IsNullOrEmpty(section))
          throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces_750"), nameof (section));
        if (string.IsNullOrEmpty(keyName))
          throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Interfaces_571"), (object) section), nameof (keyName));
        string str;
        if (!this.values.TryGetValue(section + keyName, out str))
          str = defaultValue;
        return str;
      }

      private IniFileBase.Token ParseToken(string line)
      {
        int length1 = line.Length;
        if (length1 > 2 && line[0] == '[' && line[length1 - 1] == ']')
          return new IniFileBase.Token(IniFileBase.TokenType.Section, new string[1]
          {
            line.Substring(1, length1 - 2)
          });
        if (length1 > 1)
        {
          int length2 = line.IndexOf('=');
          if (length2 >= 0)
          {
            string str1 = line.Substring(0, length2).Trim();
            string str2 = line.Substring(length2 + 1, length1 - length2 - 1).Trim();
            if (!string.IsNullOrEmpty(str1))
              return new IniFileBase.Token(IniFileBase.TokenType.Value, new string[2]
              {
                str1,
                str2
              });
          }
        }
        throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Interfaces_572"), (object) line));
      }

      /// <summary> Список имён секций </summary>
      public List<string> SectionNames => this._sectionNames;

      /// <summary> Списки имен значений у каждого раздела </summary>
      public Dictionary<string, List<string>> ValueNames => this._valueNames;

      private class Token
      {
        private IniFileBase.TokenType tokenType;
        private string[] items;

        public Token(IniFileBase.TokenType tokenType, params string[] items)
        {
          this.tokenType = tokenType;
          this.items = items;
        }

        public IniFileBase.TokenType TokenType => this.tokenType;

        public string[] Items => this.items;
      }

      private enum TokenType
      {
        Section,
        Value,
      }
    }
}
