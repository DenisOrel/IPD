
// Type: Intermech.Interfaces.WebPortal.ImportInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Информация о процессе запроса импорта</summary>
    [Serializable]
    public class ImportInfo
    {
      /// <summary>Процент выполнения</summary>
      public int Persent { get; set; }

      /// <summary>Статус</summary>
      public ImportTaskStatuses ImportTaskStatus { get; set; }

      /// <summary>Текст ошибки, если она есть</summary>
      public string ErrorMessage { get; set; }

      /// <summary>Стек ошибки, если она есть</summary>
      public string ErrorStack { get; set; }
    }
}
