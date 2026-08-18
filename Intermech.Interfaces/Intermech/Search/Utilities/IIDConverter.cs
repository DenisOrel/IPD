
// Type: Intermech.Search.Utilities.IIDConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Utilities
{
    /// <summary>Конвертер идентификаторов</summary>
    public interface IIDConverter
    {
      /// <summary>
      /// Конвертировать глобальный идентификатор типа атрибута в идентификатор типа атрибута
      /// </summary>
      /// <param name="attributeTypeGuid">Глобальный идентификатор типа атрибута</param>
      /// <returns>Идентификтор типа атрибута</returns>
      int ConvertAttributeTypeGuidToAttributeTypeID(Guid attributeTypeGuid);

      /// <summary>
      /// Конвертирвать имя типа атрибута в идентификатор типа атрибута
      /// </summary>
      /// <param name="attributeTypeName">Имя типа атрибута</param>
      /// <returns>Идентификатор типа атрибута</returns>
      int ConvertAttributeTypeNameToAttributeTypeID(string attributeTypeName);

      int ConvertObjectTypeGuidToObjectTypeID(Guid objectTypeGuid);

      int ConvertRelationTypeGuidToRelationTypeID(Guid relationTypeGuid);

      int ConvertLifecycleLevelGuidToLifecycleLevelID(Guid lifecycleLevelGuid);

      int ConvertLifecycleStepGuidToLifecycleStepID(Guid lifecycleStepGuid);
    }
}
