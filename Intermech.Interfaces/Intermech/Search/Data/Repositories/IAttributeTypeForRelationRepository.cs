
// Type: Intermech.Search.Data.Repositories.IAttributeTypeForRelationRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface IAttributeTypeForRelationRepository
    {
      void AddOrUpdate(IMSAttribute4RelationType attributeType);

      List<IMSAttribute4RelationType> Find(int relationTypeID);

      IMSAttribute4RelationType Find(AttributeTypeForRelationKey key);

      List<IMSAttribute4RelationType> FindAll();

      void RemoveAll();
    }
}
