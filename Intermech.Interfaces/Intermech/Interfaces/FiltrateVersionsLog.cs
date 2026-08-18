
// Type: Intermech.Interfaces.FiltrateVersionsLog
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>В классе хранится протокол подбора версий объектов</summary>
    [Serializable]
    public sealed class FiltrateVersionsLog : IAssignable, ICloneable
    {
      /// <summary>
      /// Словарик
      /// [(Int32) Тип связи] =&gt; словарик с протоколом подбора версий
      /// </summary>
      private Dictionary<int, Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>> _log = new Dictionary<int, Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>>();
      /// <summary>
      /// Разделитель между значениями в строке протокола по одному типу связи
      /// </summary>
      private static char Separator = '/';
      /// <summary>
      /// Разделитель между значениями в строке между протоколами разных типов связей
      /// </summary>
      private static char SeparatorLogs = '\\';
      /// <summary>
      /// Ключ, по которому можно найти строку протокола в расширенных свойствах таблицы с составом
      /// </summary>
      public static string Key = nameof (FiltrateVersionsLog);
      /// <summary>
      /// Ключ, по которому можно найти строку с типом связи в расширенных свойствах таблицы с составом
      /// </summary>
      public static string RelTypeKey = "FiltrateVersionsLogRelType";

      /// <summary>Создать экземпляр класса</summary>
      public FiltrateVersionsLog()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его полями из прототипа
      /// </summary>
      /// <param name="template">Объект-прототип</param>
      public FiltrateVersionsLog(FiltrateVersionsLog template) => this.Assign((object) template);

      /// <summary>Очистить поля класса</summary>
      public void Clear() => this._log.Clear();

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник (допускается передавать строку или FiltrateVersionsLog)</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        switch (source)
        {
          case string _:
            string[] strArray1 = ((string) source).Split(new char[1]
            {
              FiltrateVersionsLog.SeparatorLogs
            }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray1 == null || strArray1.Length == 0)
              break;
            for (int index1 = 0; index1 < strArray1.Length; ++index1)
            {
              string[] strArray2 = strArray1[index1].Split(new char[1]
              {
                FiltrateVersionsLog.Separator
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray2 != null && strArray2.Length >= 2)
              {
                int result = -1;
                if (int.TryParse(strArray2[0], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result))
                {
                  Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = new Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>(strArray2.Length - 1);
                  FiltrateVersionsLogEntry versionsLogEntry = new FiltrateVersionsLogEntry();
                  for (int index2 = 1; index2 < strArray2.Length; ++index2)
                  {
                    versionsLogEntry.Assign((object) strArray2[index2]);
                    if (!versionsLogEntry.IsEmpty)
                      dictionary.Add(new FiltrateVersionsLogEntryKey(versionsLogEntry.PrjLinkID, versionsLogEntry.ObjectID), versionsLogEntry.Clone() as FiltrateVersionsLogEntry);
                  }
                  if (dictionary.Count > 0)
                  {
                    if (this._log.ContainsKey(result))
                      this._log.Remove(result);
                    this._log.Add(result, dictionary);
                  }
                }
              }
            }
            break;
          case FiltrateVersionsLog filtrateVersionsLog:
            using (Dictionary<int, Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>>.Enumerator enumerator = filtrateVersionsLog._log.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                KeyValuePair<int, Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>> current = enumerator.Current;
                if (current.Value.Count != 0)
                {
                  Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = new Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>(current.Value.Count);
                  foreach (KeyValuePair<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> keyValuePair in current.Value)
                  {
                    if (!keyValuePair.Value.IsEmpty)
                    {
                      FiltrateVersionsLogEntryKey key = new FiltrateVersionsLogEntryKey(keyValuePair.Value.PrjLinkID, keyValuePair.Value.ObjectID);
                      dictionary[key] = keyValuePair.Value.Clone() as FiltrateVersionsLogEntry;
                    }
                  }
                  if (dictionary.Count > 0)
                  {
                    if (this._log.ContainsKey(current.Key))
                      this._log.Remove(current.Key);
                    this._log.Add(current.Key, dictionary);
                  }
                }
              }
              break;
            }
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new FiltrateVersionsLog(this);

      /// <summary>Вернуть значение в виде кодированной строки</summary>
      /// <returns>Значение в виде строки</returns>
      public override string ToString()
      {
        StringBuilder sb = new StringBuilder();
        int num1 = 0;
        int count1 = this._log.Count;
        foreach (KeyValuePair<int, Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>> keyValuePair1 in this._log)
        {
          if (keyValuePair1.Value.Count != 0 && MetaDataHelper.ExistsRelationType(keyValuePair1.Key))
          {
            sb.Append(keyValuePair1.Key.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
            sb.Append(FiltrateVersionsLog.Separator);
            int count2 = keyValuePair1.Value.Count;
            int num2 = 0;
            foreach (KeyValuePair<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> keyValuePair2 in keyValuePair1.Value)
            {
              keyValuePair2.Value.ToString(sb);
              if (num2 < count2 - 1)
                sb.Append(FiltrateVersionsLog.Separator);
              ++num2;
            }
            if (num1 < count1 - 1)
              sb.Append(FiltrateVersionsLog.SeparatorLogs);
            ++num1;
          }
        }
        return sb.ToString();
      }

      /// <summary>
      /// Вернуть значение для указанного типа связи в виде кодированной строки
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Значение в виде строки</returns>
      public string ToString(int relTypeID)
      {
        if (!MetaDataHelper.ExistsRelationType(relTypeID))
          return string.Empty;
        Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = this[relTypeID];
        if (dictionary == null || dictionary.Count == 0)
          return string.Empty;
        StringBuilder sb = new StringBuilder();
        sb.Append(relTypeID.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
        sb.Append(FiltrateVersionsLog.Separator);
        int count = dictionary.Count;
        int num = 0;
        foreach (KeyValuePair<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> keyValuePair in dictionary)
        {
          keyValuePair.Value.ToString(sb);
          if (num < count - 1)
            sb.Append(FiltrateVersionsLog.Separator);
          ++num;
        }
        return sb.ToString();
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник (строка после метода ToString(Int32))</param>
      public void AssignRelTypeLog(object source)
      {
        if (!(source is string))
          return;
        string[] strArray = ((string) source).Split(new char[1]
        {
          FiltrateVersionsLog.Separator
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray == null || strArray.Length < 2)
          return;
        int result = -1;
        if (!int.TryParse(strArray[0], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          return;
        if (this._log.ContainsKey(result))
          this._log.Remove(result);
        Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = new Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>(strArray.Length - 1);
        FiltrateVersionsLogEntry versionsLogEntry = new FiltrateVersionsLogEntry();
        for (int index = 1; index < strArray.Length; ++index)
        {
          versionsLogEntry.Assign((object) strArray[index]);
          if (!versionsLogEntry.IsEmpty)
          {
            FiltrateVersionsLogEntryKey key = new FiltrateVersionsLogEntryKey(versionsLogEntry.PrjLinkID, versionsLogEntry.ObjectID);
            if (dictionary.ContainsKey(key))
              dictionary.Remove(key);
            dictionary.Add(key, versionsLogEntry.Clone() as FiltrateVersionsLogEntry);
          }
        }
        if (dictionary.Count <= 0)
          return;
        if (this._log.Count == 0)
          this._log.Clear();
        if (this._log.ContainsKey(result))
          this._log.Remove(result);
        this._log.Add(result, dictionary);
      }

      /// <summary>Получить протокол для указанного типа связи</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Протокол для указанного типа связи или null, если протокола нет в словарике</returns>
      public Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> this[int relTypeID]
      {
        get
        {
          return this._log.ContainsKey(relTypeID) ? this._log[relTypeID] : (Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>) null;
        }
        set
        {
          if (this._log.ContainsKey(relTypeID))
            this._log.Remove(relTypeID);
          if (value == null)
            return;
          this._log.Add(relTypeID, value);
        }
      }

      /// <summary>Найти запись для указанной связи и версии объекта</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="PrjLinkID">Идентификатор связи</param>
      /// <param name="ObjectID">Идентификатор версии объекта</param>
      /// <returns>Pапись для указанной связи и версии объекта</returns>
      public FiltrateVersionsLogEntry this[int relTypeID, long PrjLinkID, long ObjectID]
      {
        get
        {
          Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = this[relTypeID];
          if (dictionary == null)
            return (FiltrateVersionsLogEntry) null;
          FiltrateVersionsLogEntryKey key = new FiltrateVersionsLogEntryKey(PrjLinkID, ObjectID);
          return dictionary.ContainsKey(key) ? dictionary[key] : (FiltrateVersionsLogEntry) null;
        }
      }

      /// <summary>
      /// Количество элементов в протоколе для указанного типа связи
      /// </summary>
      public int Count(int relTypeID)
      {
        Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = this[relTypeID];
        return dictionary == null ? 0 : dictionary.Count;
      }

      /// <summary>Добавить в словарик запись для указанного типа связи</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="entry">Запись протокола</param>
      public void Add(int relTypeID, FiltrateVersionsLogEntry entry)
      {
        if (!MetaDataHelper.ExistsRelationType(relTypeID) || entry == null || entry.IsEmpty)
          return;
        Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = this[relTypeID];
        if (dictionary == null)
        {
          dictionary = new Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>();
          this._log.Add(relTypeID, dictionary);
        }
        dictionary[new FiltrateVersionsLogEntryKey(entry.PrjLinkID, entry.ObjectID)] = entry;
      }

      /// <summary>
      /// Удалить из протокола заданного типа связи указанную запись
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="entry">Удаляемая запись протокола</param>
      public void Remove(int relTypeID, FiltrateVersionsLogEntry entry)
      {
        Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> dictionary = this[relTypeID];
        if (dictionary == null)
          return;
        FiltrateVersionsLogEntryKey key = new FiltrateVersionsLogEntryKey(entry.PrjLinkID, entry.ObjectID);
        if (!dictionary.ContainsKey(key))
          return;
        dictionary.Remove(key);
      }

      /// <summary>
      /// Добавить информацию из указанного протокола в текущий протокол
      /// </summary>
      /// <param name="log">Протокол-источник</param>
      public void Append(FiltrateVersionsLog log)
      {
        if (log == null || log._log.Count == 0)
          return;
        foreach (KeyValuePair<int, Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>> keyValuePair1 in log._log)
        {
          foreach (KeyValuePair<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry> keyValuePair2 in keyValuePair1.Value)
            this.Add(keyValuePair1.Key, keyValuePair2.Value);
        }
      }
    }
}
