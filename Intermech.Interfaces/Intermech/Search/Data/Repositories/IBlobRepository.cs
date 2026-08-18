
// Type: Intermech.Search.Data.Repositories.IBlobRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public interface IBlobRepository
    {
      void AddOrUpdate(Blob blob);

      Blob Find(BlobKey key);

      Blob Find(string fileName, bool withValue);

      List<Blob> FindForObject(long objectVersionID);

      List<Blob> FindForRelation(long relationID);

      void RemoveForObject(long objectVersionID);

      void RemoveForRelation(long relationVersionID);
    }
}
