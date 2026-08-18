
// Type: Intermech.Interfaces.ClearTrashProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary>Структура, описывающая расписание очистки мусора</summary>
    [Serializable]
    public struct ClearTrashProperties
    {
      /// <summary>
      /// Имя компьютера (сервера) который выполняет сей процесс
      /// </summary>
      public string ComputerName;
      /// <summary>Режим</summary>
      public ClearingMode ClearingMode;
      /// <summary>
      /// Таблица с TimeTableValue (формат -&gt; "Day=Time")
      /// 						 Day  : год (строка 4 символа) 2 символа месяц + 2 символа день;
      /// 								месяц (int) - число месяца
      /// 								неделя (int) - день недели
      /// 								день (int) в принципе ложить нечего, но будет просто счетчик
      /// 					     Time : время очистки;
      /// </summary>
      public ArrayList TimeTable;

      public ClearTrashProperties(string computerName)
      {
        this.ComputerName = computerName;
        this.ClearingMode = ClearingMode.SeveralPerWeek;
        this.TimeTable = new ArrayList();
      }

      public ClearTrashProperties(string computerName, ClearingMode clearingMode, ArrayList timeTable)
      {
        this.ComputerName = computerName;
        this.ClearingMode = clearingMode;
        this.TimeTable = timeTable;
      }

      public ClearTrashProperties(ClearingMode clearingMode, ArrayList timeTable)
      {
        this.ComputerName = string.Empty;
        this.ClearingMode = clearingMode;
        this.TimeTable = timeTable;
      }
    }
}
