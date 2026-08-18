
// Type: Intermech.Interfaces.RelationModificationEvent
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс для хранения инфы о связи</summary>
    [Serializable]
    public class RelationModificationEvent : CategoryValueGuid
    {
      /// <summary>Идентификатор версии родительского объекта</summary>
      public long ProjID;

      public RelationModificationEvent(
        int aCategoryType,
        long aCategoryID,
        ActionType anActionID,
        int metadataTypeID,
        Guid relationGUID,
        long projID)
        : base(aCategoryType, aCategoryID, anActionID, relationGUID, metadataTypeID)
      {
        this.ProjID = projID;
      }
    }
}
