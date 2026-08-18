
// Type: Intermech.Interfaces.WebPortal.PublishComposition
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Публикуемый состав</summary>
    [Serializable]
    public class PublishComposition
    {
      /// <summary>Публикуемые объекты</summary>
      public List<PublishCompositionObject> Objects;
      /// <summary>
      /// Публикуемые связи, в качестве значения в коллекции указан глобальный идентификатор версии дочернего объекта
      /// </summary>
      public List<PublishCompositionRelation> Relations;

      /// <summary>Конструктор</summary>
      public PublishComposition()
      {
        this.Objects = new List<PublishCompositionObject>();
        this.Relations = new List<PublishCompositionRelation>();
      }

      /// <summary>Конструктор</summary>
      /// <param name="objects">Публикуемые объекты</param>
      /// <param name="relations">Публикуемые связи</param>
      [Obsolete("Конструктор устарел. Будет удален в IPS 7.0")]
      public PublishComposition(
        List<PublishCompositionObject> objects,
        List<PublishCompositionRelation> relations)
      {
        this.Objects = objects;
        this.Relations = relations;
      }
    }
}
