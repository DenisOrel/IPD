
// Type: Intermech.Interfaces.CompositionTracking.IObjectTypeApplicabilityContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>
    /// Информация о типе объекта в контекте применяемости (родительском объекте / связи)
    /// </summary>
    public interface IObjectTypeApplicabilityContext
    {
      /// <summary>Идентификатор типа объекта</summary>
      int ObjectTypeId { get; set; }

      /// <summary>Идентификатор родительского типа объекта</summary>
      int InObjectTypeId { get; set; }

      /// <summary>Идентификатор типа связи</summary>
      int RelationTypeId { get; set; }
    }
}
