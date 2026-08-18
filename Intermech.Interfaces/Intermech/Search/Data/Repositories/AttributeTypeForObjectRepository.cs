
// Type: Intermech.Search.Data.Repositories.AttributeTypeForObjectRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class AttributeTypeForObjectRepository : IAttributeTypeForObjectRepository
    {
      public void AddOrUpdate(IMSAttribute4ObjectType attributeType)
      {
        throw new NotImplementedException();
      }

      public IMSAttribute4ObjectType Find(AttributeTypeForObjectKey key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        return MetaDataHelper.GetAttribute4ObjectType(key.ObjectTypeID, key.AttributeTypeID);
      }

      public List<IMSAttribute4ObjectType> Find(int objectTypeID)
      {
        return objectTypeID != -1 ? MetaDataHelper.GetAttribute4ObjectTypeList(objectTypeID) : throw new ArgumentException();
      }

      public List<IMSAttribute4ObjectType> FindAll() => throw new NotImplementedException();

      public void RemoveAll() => throw new NotImplementedException();
    }
}
