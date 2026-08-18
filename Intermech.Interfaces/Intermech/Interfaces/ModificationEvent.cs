
// Type: Intermech.Interfaces.ModificationEvent
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый класс для хранения и передачи инфы о событиях модификации данных, которые происходят на сервере приложений IPS
    /// </summary>
    [Serializable]
    public class ModificationEvent : CategoryValue
    {
      /// <summary>
      /// Идентификатор метаданного, к которому относится событие (ид. типа объектов, связей и пр.)
      /// </summary>
      public int MetadataTypeID;

      public ModificationEvent(
        int aCategoryType,
        long aCategoryID,
        ActionType anActionID,
        int metadataTypeID)
        : base(aCategoryType, aCategoryID, anActionID)
      {
        this.MetadataTypeID = metadataTypeID;
      }
    }
}
