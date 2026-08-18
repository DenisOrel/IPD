
// Type: Intermech.Interfaces.WebPortal.ImportOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class ImportOptions
    {
      /// <summary>Количество уровней, полный состав -1</summary>
      public SelectCompositionType CompositionType { get; set; }

      /// <summary>Получить права владения</summary>
      public bool SetOwner { get; set; }

      /// <summary>Типы разрешенных объектов</summary>
      public List<int> FilteredTypes { get; set; }

      /// <summary>Запуск задачи импорта незамедлительно</summary>
      public bool StartImmediately { get; set; }

      /// <summary>Получать обновления</summary>
      public bool AutoUpdate { get; set; }

      public ImportOptions()
      {
      }

      public ImportOptions(
        SelectCompositionType compositionType,
        bool setOwner,
        List<int> filteredTypes,
        bool startImmediately,
        bool autoUpdate)
      {
        this.CompositionType = compositionType;
        this.SetOwner = setOwner;
        this.FilteredTypes = filteredTypes;
        this.StartImmediately = startImmediately;
        this.AutoUpdate = autoUpdate;
      }
    }
}
