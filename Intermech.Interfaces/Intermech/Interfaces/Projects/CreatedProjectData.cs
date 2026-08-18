
// Type: Intermech.Interfaces.Projects.CreatedProjectData
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Projects
{
    /// <summary>
    /// Вспомогательный класс для передачи созданных объектов в проекте, а так же атрибутов относящихся к этим объектам
    /// </summary>
    [Serializable]
    public sealed class CreatedProjectData
    {
      private long objectID;
      private List<Intermech.Interfaces.AttributeValues> attributeValues;

      public CreatedProjectData(long objectID, List<Intermech.Interfaces.AttributeValues> attributeValues)
      {
        if (Consts.IsUndefinedObjectId(objectID))
          throw new ArgumentException("Не задан идентификатор версии объекта", nameof (objectID));
        if (attributeValues == null)
          throw new ArgumentNullException(nameof (attributeValues));
        this.objectID = objectID;
        this.attributeValues = attributeValues;
      }

      public long ObjectID => this.objectID;

      public List<Intermech.Interfaces.AttributeValues> AttributeValues => this.attributeValues;
    }
}
