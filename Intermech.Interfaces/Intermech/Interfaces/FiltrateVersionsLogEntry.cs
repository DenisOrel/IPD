
// Type: Intermech.Interfaces.FiltrateVersionsLogEntry
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс представляет собой одну запись в протоколе подбора версий
    /// </summary>
    [Serializable]
    public sealed class FiltrateVersionsLogEntry : 
      IAssignable,
      ICloneable,
      IComparable,
      IComparable<FiltrateVersionsLogEntry>
    {
      /// <summary>Разделитель между значениями в строке</summary>
      private static char Separator = '|';
      /// <summary>Должно быть FieldsCount полей в закодированной строке</summary>
      private static int FieldsCount = 6;
      /// <summary>Идентификатор связи</summary>
      public long PrjLinkID;
      /// <summary>Идентификатор версии объекта</summary>
      public long ObjectID;
      /// <summary>Статус подобранной версии</summary>
      public ObjectFiltrationState State;
      /// <summary>
      /// "Вес", с которым подобралась или была отбракована указанная версия
      /// </summary>
      public int Weight;
      /// <summary>
      /// Идентификатор атрибута, по значению которого была подобрана данная версия по
      /// основным критериям подбора версий.
      /// Значение Intermech.Consts.UnknownAttributeId означает, что версия не была
      /// подобрана по основным критериям подбора версий.
      /// </summary>
      public int MainAttribute;
      /// <summary>
      /// Номер основного критерия, по которому была подобрана данная версия.
      /// Значение -1 означает, что версия не была подобрана по основным критериям
      /// подбора версий.
      /// </summary>
      public int Criterion = -1;

      /// <summary>Создать экземпляр класса</summary>
      public FiltrateVersionsLogEntry()
      {
      }

      /// <summary>Создать экземпляр класса на основе указанного объекта</summary>
      /// <param name="source">Объект-прототип (FiltrateVersionsLogEntry) или кодированная строка (string)</param>
      public FiltrateVersionsLogEntry(object source) => this.Assign(source);

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this.PrjLinkID = 0L;
        this.ObjectID = 0L;
        this.State = ObjectFiltrationState.fsNotRequired;
        this.Weight = 0;
        this.MainAttribute = 0;
        this.Criterion = -1;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник (допускается передавать строку или FiltrateVersionsLogEntry)</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        switch (source)
        {
          case string _:
            string[] strArray = ((string) source).Split(new char[1]
            {
              FiltrateVersionsLogEntry.Separator
            }, StringSplitOptions.None);
            if (strArray == null || strArray.Length != FiltrateVersionsLogEntry.FieldsCount)
              break;
            if (!long.TryParse(strArray[0], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out this.PrjLinkID))
              this.PrjLinkID = 0L;
            if (!long.TryParse(strArray[1], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out this.ObjectID))
              this.ObjectID = 0L;
            int result;
            if (!int.TryParse(strArray[2], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result))
              result = 0;
            this.State = (ObjectFiltrationState) result;
            if (!int.TryParse(strArray[3], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out this.Weight))
              this.Weight = 0;
            if (!int.TryParse(strArray[4], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out this.MainAttribute))
              this.MainAttribute = 0;
            if (int.TryParse(strArray[5], NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out this.Criterion))
              break;
            this.Criterion = -1;
            break;
          case FiltrateVersionsLogEntry versionsLogEntry:
            this.PrjLinkID = versionsLogEntry.PrjLinkID;
            this.ObjectID = versionsLogEntry.ObjectID;
            this.State = versionsLogEntry.State;
            this.Weight = versionsLogEntry.Weight;
            this.MainAttribute = versionsLogEntry.MainAttribute;
            this.Criterion = versionsLogEntry.Criterion;
            break;
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new FiltrateVersionsLogEntry((object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as FiltrateVersionsLogEntry);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(FiltrateVersionsLogEntry other)
      {
        if (other == null)
          return 1;
        int num = this.PrjLinkID.CompareTo(other.PrjLinkID);
        return num != 0 ? num : this.ObjectID.CompareTo(other.ObjectID);
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is FiltrateVersionsLogEntry versionsLogEntry && this.PrjLinkID == versionsLogEntry.PrjLinkID && this.ObjectID == versionsLogEntry.ObjectID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this.PrjLinkID.GetHashCode() << 16 /*0x10*/ ^ this.ObjectID.GetHashCode();
      }

      /// <summary>Вернуть значение в виде кодированной строки</summary>
      /// <returns>Значение в виде строки</returns>
      public override string ToString()
      {
        StringBuilder sb = new StringBuilder();
        this.ToString(sb);
        return sb.ToString();
      }

      /// <summary>
      /// Поместить значение в виде кодированной строки в билдер
      /// </summary>
      /// <param name="strBuilder"></param>
      public void ToString(StringBuilder sb)
      {
        sb.Append(this.PrjLinkID.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
        sb.Append(FiltrateVersionsLogEntry.Separator);
        sb.Append(this.ObjectID.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
        sb.Append(FiltrateVersionsLogEntry.Separator);
        sb.Append(((int) this.State).ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
        sb.Append(FiltrateVersionsLogEntry.Separator);
        sb.Append(this.Weight.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
        sb.Append(FiltrateVersionsLogEntry.Separator);
        sb.Append(this.MainAttribute.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
        sb.Append(FiltrateVersionsLogEntry.Separator);
        sb.Append(this.Criterion.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture));
      }

      /// <summary>
      /// Значение true указывает на то, что ключевые поля класса не заполнены
      /// </summary>
      public bool IsEmpty
      {
        [DebuggerStepThrough] get => this.PrjLinkID == 0L || this.ObjectID == 0L;
      }
    }
}
