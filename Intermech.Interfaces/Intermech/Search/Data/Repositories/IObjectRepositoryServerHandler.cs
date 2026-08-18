
// Type: Intermech.Search.Data.Repositories.IObjectRepositoryServerHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Search.Data.Repositories
{
    public interface IObjectRepositoryServerHandler
    {
      DataTable FindApplicabilitiesInLinks(
        Guid userSessionGuid,
        long objectVersionID,
        DBRecordSetParams @params);

      DataTable FindApplicabilitiesInClassifiers(
        Guid userSessionGuid,
        long objectVersionID,
        DBRecordSetParams @params);
    }
}
