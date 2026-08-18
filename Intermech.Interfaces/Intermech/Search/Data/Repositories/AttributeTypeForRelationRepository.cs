
// Type: Intermech.Search.Data.Repositories.AttributeTypeForRelationRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class AttributeTypeForRelationRepository : IAttributeTypeForRelationRepository
    {
      public void AddOrUpdate(IMSAttribute4RelationType attributeTypeForRelation)
      {
        throw new NotImplementedException();
      }

      public IMSAttribute4RelationType Find(AttributeTypeForRelationKey key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        return MetaDataHelper.GetAttribute4RelationType(key.RelationTypeID, key.AttributeTypeID);
      }

      public List<IMSAttribute4RelationType> Find(int relationTypeID)
      {
        return relationTypeID != -1 ? MetaDataHelper.GetAttribute4RelationTypeList(relationTypeID) : throw new ArgumentException();
      }

      public List<IMSAttribute4RelationType> FindAll() => throw new NotImplementedException();

      public void RemoveAll() => throw new NotImplementedException();
    }
}
