
// Type: Intermech.Interfaces.CompositionTracking.ObjectTypeApplicabilityContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>
    /// Информация о типе объекта в контекте применяемости (родительском объекте / связи)
    /// </summary>
    [Serializable]
    public class ObjectTypeApplicabilityContext : IObjectTypeApplicabilityContext
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="objectTypeId"></param>
      /// <param name="inObjectTypeId"></param>
      /// <param name="relationTypeId"></param>
      public ObjectTypeApplicabilityContext(int objectTypeId, int inObjectTypeId = -1, int relationTypeId = -1)
      {
        this.ObjectTypeId = objectTypeId;
        this.InObjectTypeId = inObjectTypeId;
        this.RelationTypeId = relationTypeId;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objectTypeContext"></param>
      public ObjectTypeApplicabilityContext(IObjectTypeApplicabilityContext objectTypeContext)
      {
        this.ObjectTypeId = objectTypeContext != null ? objectTypeContext.ObjectTypeId : throw new ArgumentNullException(nameof (objectTypeContext));
        this.InObjectTypeId = objectTypeContext.InObjectTypeId;
        this.RelationTypeId = objectTypeContext.RelationTypeId;
      }

      /// <summary>Идентификатор типа объекта</summary>
      public int ObjectTypeId { get; set; }

      /// <summary>Идентификатор родительского типа объекта</summary>
      public int InObjectTypeId { get; set; }

      /// <summary>Идентификатор типа связи</summary>
      public int RelationTypeId { get; set; }
    }
}
