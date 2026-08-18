
// Type: Intermech.Interfaces.NotifySamples.NSResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.NotifySamples
{
    /// <summary>Результат вызова обработчика уведомляющих выборок</summary>
    [Serializable]
    public class NSResult
    {
      /// <summary>Дата и время следующего опроса выборок</summary>
      public DateTime NextProcessTime = DateTime.MaxValue;
      /// <summary>Список выборок, в которых произошли изменения</summary>
      public List<NSDifferences> Samples = new List<NSDifferences>();
    }
}
