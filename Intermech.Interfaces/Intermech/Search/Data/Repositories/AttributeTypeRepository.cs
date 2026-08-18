
// Type: Intermech.Search.Data.Repositories.AttributeTypeRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    /// <summary>Стандартный репозиторий типов атрибутов</summary>
    public sealed class AttributeTypeRepository : IAttributeTypeRepository
    {
      public void AddOrUpdate(IMSAttributeType attributeType) => throw new NotSupportedException();

      /// <summary>Найти тип атрибута</summary>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <returns>Тип атрибута</returns>
      public IMSAttributeType Find(int attributeTypeID)
      {
        return MetaDataHelper.GetAttributeType(attributeTypeID);
      }

      public IMSAttributeType Find(Guid attributeTypeGuid)
      {
        return MetaDataHelper.GetAttributeType(attributeTypeGuid);
      }

      public IMSAttributeType Find(string attributeTypeName)
      {
        return this.Find(MetaDataHelper.GetAttributeID((object) attributeTypeName));
      }

      public List<IMSAttributeType> FindAll() => MetaDataHelper.GetAttributeTypesList();

      public void Remove(int attributeTypeID) => throw new NotSupportedException();

      public void RemoveAll() => throw new NotSupportedException();
    }
}
