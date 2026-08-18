
// Type: Intermech.Interfaces.CompareOperatorsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary>Класс со списком логических функций</summary>
    [Serializable]
    public sealed class CompareOperatorsHelper
    {
      /// <summary>Отсутствие операции - NOP</summary>
      public const string ctBoolNOP = "NOP";
      /// <summary>Логическая операция "ИЛИ" - OR</summary>
      public const string ctBoolOR = "OR";
      /// <summary>Логическая операция "И" - AND</summary>
      public const string ctBoolAND = "AND";
      /// <summary>Логическая операция по умолчанию - "ИЛИ"</summary>
      public static string ctDefaultFunction = "OR";
      /// <summary>Список пар "Операция" - "Индекс"</summary>
      public SortedList Operations = new SortedList();
      /// <summary>Список пар "Операция" = "Текстовое описание"</summary>
      public SortedList Names = new SortedList();

      /// <summary>
      /// Создать и инициализировать экземпляр класса с типами значений для сравнения
      /// </summary>
      public CompareOperatorsHelper()
      {
        this.Operations[(object) "OR"] = (object) 1;
        this.Operations[(object) "AND"] = (object) 2;
        this.Names[(object) "OR"] = (object) LocalizationHolder.rm.GetString("Interfaces_544");
        this.Names[(object) "AND"] = (object) LocalizationHolder.rm.GetString("Interfaces_545");
      }

      /// <summary>
      /// Метод возвращает массив со операциями или их описаниями
      /// </summary>
      /// <param name="CopyNames">true - копировать в массив операции, false - их описания</param>
      /// <returns>Массив со всеми операциями или их описаниями</returns>
      public object[] GetMembers(bool CopyNames)
      {
        if (this.Operations.Count <= 0 || this.Names.Count <= 0)
          return (object[]) null;
        object[] members;
        if (CopyNames)
        {
          members = new object[this.Operations.Count];
          this.Operations.Keys.CopyTo((Array) members, 0);
        }
        else
        {
          members = new object[this.Names.Count];
          SortedList sortedList = new SortedList();
          for (int index = 0; index < this.Names.Count; ++index)
            sortedList.Add(this.Names.GetByIndex(index), (object) index);
          sortedList.Keys.CopyTo((Array) members, 0);
        }
        return members;
      }

      /// <summary>Проверить наличие значения в списке операций</summary>
      /// <param name="value">Операция</param>
      /// <returns>true, если значение принадлежит списку операций</returns>
      public bool IsMember(string value) => this.Operations.ContainsKey((object) value);
    }
}
