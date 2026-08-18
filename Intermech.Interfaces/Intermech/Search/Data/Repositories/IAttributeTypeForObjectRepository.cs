
// Type: Intermech.Search.Data.Repositories.IAttributeTypeForObjectRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface IAttributeTypeForObjectRepository
    {
      void AddOrUpdate(IMSAttribute4ObjectType attributeType);

      IMSAttribute4ObjectType Find(AttributeTypeForObjectKey key);

      List<IMSAttribute4ObjectType> Find(int objectTypeID);

      List<IMSAttribute4ObjectType> FindAll();

      void RemoveAll();
    }
}
