// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EditingContextsForObjectsAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public sealed class EditingContextsForObjectsAnalyzer : ISearchGroupingObjectAnalyzer
{
  public string Name => "Поиск среди выделенных версий";

  public int Analyze(IUserSession session, SearchGroupingObjects searchObjects)
  {
    if (session == null || searchObjects == null || searchObjects.Count == 0 || !(session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsServerService customService))
      return 0;
    List<long> objectIds = searchObjects.GetObjectIDs();
    List<long> objectsContexts = customService.FindObjectsContexts((object) session, objectIds, false);
    foreach (long objectID in objectIds)
    {
      IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad0036b-306c-11d8-b4e9-00304f19f545")));
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_PROJ_ID
        }
      };
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersInVersion(paramSet, objectID).Rows)
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        objectsContexts.Add(int64Value);
      }
    }
    if (searchObjects.Count > 0 && objectsContexts.Count > 0)
    {
      for (int index = 0; index < objectsContexts.Count; ++index)
      {
        if (!searchObjects[0].GroupObjectIDs.ContainsKey(objectsContexts[index]))
          searchObjects[0].GroupObjectIDs.Add(objectsContexts[index], -1);
      }
    }
    return 0;
  }
}
