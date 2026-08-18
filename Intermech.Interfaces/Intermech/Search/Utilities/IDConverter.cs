
// Type: Intermech.Search.Utilities.IDConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Linq;


namespace Intermech.Search.Utilities
{
    /// <summary>Конвертер идентификаторов</summary>
    public sealed class IDConverter : IIDConverter
    {
      /// <summary>
      /// Конвертировать глобальный идентификатор типа атрибута в идентификатор типа атрибута
      /// </summary>
      /// <param name="attributeTypeGuid">Глобальный идентификатор типа атрибута</param>
      /// <returns>Идентификтор типа атрибута</returns>
      public int ConvertAttributeTypeGuidToAttributeTypeID(Guid attributeTypeGuid)
      {
        return MetaDataHelper.GetAttributeTypeID(attributeTypeGuid);
      }

      /// <summary>
      /// Конвертирвать имя типа атрибута в идентификатор типа атрибута
      /// </summary>
      /// <param name="attributeTypeName">Имя типа атрибута</param>
      /// <returns>Идентификатор типа атрибута</returns>
      /// <exception cref="T:System.ArgumentNullException"></exception>
      public int ConvertAttributeTypeNameToAttributeTypeID(string attributeTypeName)
      {
        if (attributeTypeName == null)
          throw new ArgumentNullException(attributeTypeName);
        IMSAttributeType imsAttributeType = MetaDataHelper.GetAttributeTypesList().FirstOrDefault<IMSAttributeType>((Func<IMSAttributeType, bool>) (o => o.Name == attributeTypeName));
        return imsAttributeType == null ? 0 : imsAttributeType.AttributeID;
      }

      public int ConvertObjectTypeGuidToObjectTypeID(Guid objectTypeGuid)
      {
        return MetaDataHelper.GetObjectTypeID(objectTypeGuid);
      }

      public int ConvertRelationTypeGuidToRelationTypeID(Guid relationTypeGuid)
      {
        return MetaDataHelper.GetRelationTypeID(relationTypeGuid);
      }

      public int ConvertLifecycleLevelGuidToLifecycleLevelID(Guid lifecycleLevelGuid)
      {
        return MetaDataHelper.GetLCLevelID(lifecycleLevelGuid);
      }

      public int ConvertLifecycleStepGuidToLifecycleStepID(Guid lifecycleStepGuid)
      {
        return MetaDataHelper.GetLCStepID(lifecycleStepGuid);
      }
    }
}
