
// Type: Intermech.Interfaces.CompareTypesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Класс со списком типов значений для сравнения</summary>
    [Serializable]
    public sealed class CompareTypesHelper
    {
      /// <summary>Тип значения для сравнения - CONST</summary>
      public const string ctConst = "CONST";
      /// <summary>Тип значения для сравнения - VARIABLE</summary>
      public const string ctVariable = "VARIABLE";
      /// <summary>Тип значения для сравнения - ATTRIBUTE</summary>
      public const string ctAttribute = "ATTRIBUTE";
      /// <summary>Список пар "Тип значения для сравнения" - "Индекс"</summary>
      public SortedList Types = new SortedList();
      /// <summary>
      /// Список пар "Тип значения для сравнения" = "Текстовое описание"
      /// </summary>
      public SortedList Names = new SortedList();

      /// <summary>
      /// Создать и инициализировать экземпляр класса с типами значений для сравнения
      /// </summary>
      public CompareTypesHelper()
      {
        this.Types[(object) "CONST"] = (object) 1;
        this.Types[(object) "VARIABLE"] = (object) 2;
        this.Types[(object) "ATTRIBUTE"] = (object) 3;
        this.Names[(object) "CONST"] = (object) LocalizationHolder.rm.GetString("Interfaces_541");
        this.Names[(object) "VARIABLE"] = (object) LocalizationHolder.rm.GetString("Interfaces_542");
        this.Names[(object) "ATTRIBUTE"] = (object) LocalizationHolder.rm.GetString("Interfaces_543");
      }

      /// <summary>
      /// Создать и инициализировать экземпляр класса с типами значений для сравнения
      /// (с учётом исключений)
      /// </summary>
      public CompareTypesHelper(params int[] excluded)
      {
        List<int> intList = new List<int>((IEnumerable<int>) excluded);
        if (!intList.Contains(1))
        {
          this.Types[(object) "CONST"] = (object) 1;
          this.Names[(object) "CONST"] = (object) LocalizationHolder.rm.GetString("Interfaces_541");
        }
        if (!intList.Contains(2))
        {
          this.Types[(object) "VARIABLE"] = (object) 2;
          this.Names[(object) "VARIABLE"] = (object) LocalizationHolder.rm.GetString("Interfaces_542");
        }
        if (intList.Contains(3))
          return;
        this.Types[(object) "ATTRIBUTE"] = (object) 3;
        this.Names[(object) "ATTRIBUTE"] = (object) LocalizationHolder.rm.GetString("Interfaces_543");
      }

      /// <summary>
      /// Метод возвращает массив со всеми типами для сравнения или их описаниями
      /// </summary>
      /// <param name="CopyNames">true - копировать в массив имена типов, false - их описания</param>
      /// <returns>Массив со всеми именами типов или их описаниями</returns>
      public object[] GetMembers(bool CopyNames)
      {
        if (this.Types.Count <= 0 || this.Names.Count <= 0)
          return (object[]) null;
        object[] members;
        if (CopyNames)
        {
          members = new object[this.Types.Count];
          this.Types.Keys.CopyTo((Array) members, 0);
        }
        else
        {
          members = new object[this.Names.Count];
          this.Names.Keys.CopyTo((Array) members, 0);
          for (int index = 0; index < members.Length; ++index)
            members[index] = this.Names[members[index]];
        }
        return members;
      }

      /// <summary>
      /// Проверить наличие значения в списке типов значений для сравнения
      /// </summary>
      /// <param name="value">Тип значения для сравнения</param>
      /// <returns>true, если значение принадлежит списку типов</returns>
      public bool IsMember(string value) => this.Types.ContainsKey((object) value);
    }
}
