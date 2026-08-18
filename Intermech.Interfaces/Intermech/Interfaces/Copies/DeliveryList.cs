
// Type: Intermech.Interfaces.Copies.DeliveryList
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Copies
{
    /// <summary>Класс с информацией о листе рассылки</summary>
    [Serializable]
    public class DeliveryList
    {
      /// <summary>ID листа рассылки</summary>
      public long ID { get; set; }

      public string NameInMessages { get; set; }

      /// <summary>Подписчики</summary>
      public List<Subscriber> Subscribers { get; set; }
    }
}
