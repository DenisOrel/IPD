
// Type: Intermech.Navigator.Queries.ObjectApplicabilityByClassifiersQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Data.Repositories;
using System.Data;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Запрос на получение применяемости объекта по классификатор и ручным выборкам
/// </summary>
/// <summary>Конструктор</summary>
/// <param name="objectVersionID">Идентификатор версии объекта</param>
/// <param name="support">Хелпер запроса</param>
public sealed class ObjectApplicabilityByClassifiersQuery(
  long objectVersionID,
  INodeQuerySupport support) : ObjectApplicabilityQueryBase(objectVersionID, support)
{
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      return (session.GetCustomService(typeof (IObjectRepositoryServerHandler)) as IObjectRepositoryServerHandler).FindApplicabilitiesInClassifiers(session.SessionGUID, this.ObjectVersionID, this.GetQueryParams(this.mapping, true));
    }
  }
}
