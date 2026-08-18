
// Type: Intermech.Search.Data.Repositories.IRelationRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface IRelationRepository
    {
      long AddOrUpdate(Relation relation);

      Relation Find(long relationID);

      List<Relation> Find(List<ConditionStructure> conditions);

      List<Relation> Find(int relationTypeID);

      List<Relation> Find(int relationTypeID, List<ConditionStructure> conditions);

      List<Relation> Find(FindRelationsParams @params);

      void Remove(long relationID);

      void Remove(int relationTypeID);

      void Remove(List<ConditionStructure> conditions);
    }
}
