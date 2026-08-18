
// Type: Intermech.Search.Data.Repositories.IAttributeTypeRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    /// <summary>Репозиторий типов атрибутов</summary>
    public interface IAttributeTypeRepository
    {
      void AddOrUpdate(IMSAttributeType attributeType);

      /// <summary>Найти тип атрибута</summary>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <returns>Тип атрибута</returns>
      IMSAttributeType Find(int attributeTypeID);

      IMSAttributeType Find(Guid attributeTypeGuid);

      IMSAttributeType Find(string attributeTypeName);

      List<IMSAttributeType> FindAll();

      void Remove(int attributeTypeID);

      void RemoveAll();
    }
}
