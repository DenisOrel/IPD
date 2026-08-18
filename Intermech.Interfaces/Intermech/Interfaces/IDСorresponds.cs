
// Type: Intermech.Interfaces.IDСorresponds
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Информация о соответсвиях идентификаторов из источника идентификаторам в приемнике
    /// в результате импорта объектов
    /// </summary>
    [Serializable]
    public sealed class IDСorresponds
    {
      /// <summary>Идентификатор версии объекта в базе-источнике</summary>
      public long SourceObjectID { get; private set; }

      /// <summary>Идентификатор объекта в базе-источнике</summary>
      public long SourceID { get; private set; }

      /// <summary>Идентификатор объекта в базе-приемнике</summary>
      public long HostID { get; private set; }

      /// <summary>Идентификатор версии объекта в базе-приемнике</summary>
      public long HostObjectID { get; private set; }

      /// <summary>Признак того, что объект новый для базы-приемника</summary>
      public bool IsNew { get; set; }

      public IDСorresponds(
        long sourceObjectID,
        long sourceID,
        long hostObjectID,
        long hostID,
        bool isNew)
      {
        this.SourceObjectID = sourceObjectID;
        this.SourceID = sourceID;
        this.HostObjectID = hostObjectID;
        this.HostID = hostID;
        this.IsNew = isNew;
      }
    }
}
