
// Type: Intermech.Interfaces.AttachedSelObjectInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Краткая информация по объекту, к которому привязана выборка
    /// </summary>
    [Serializable]
    public class AttachedSelObjectInfo
    {
      /// <summary>Идентификатор версии объекта</summary>
      public long ObjectID;
      /// <summary>Тип объекта</summary>
      public int ObjectType;

      public AttachedSelObjectInfo(long objectID, int objectType)
      {
        this.ObjectID = objectID;
        this.ObjectType = objectType;
      }
    }
}
