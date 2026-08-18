// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.PDMLinkedObjectsHandler
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Pdm.Server;

internal class PDMLinkedObjectsHandler : LinkedObjectsHandler, ILinkedObjectsHandler
{
  private List<int> _docObjTypes;
  private List<int> _artObjTypes;

  protected override void OnReloadTypes()
  {
    this._docObjTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    this._artObjTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
  }

  public List<int> HandleTypes
  {
    get => this._docObjTypes.Union<int>((IEnumerable<int>) this._artObjTypes).ToList<int>();
  }

  public List<int> OutputTypes
  {
    get => this._docObjTypes.Union<int>((IEnumerable<int>) this._artObjTypes).ToList<int>();
  }

  public List<LinkedObject> Handle(
    IUserSession session,
    long objectID,
    int objectType,
    string filtrationOwnerID)
  {
    List<LinkedObject> collection = new List<LinkedObject>();
    IArticleService customService = session.GetCustomService(typeof (IArticleService)) as IArticleService;
    if (this._docObjTypes.Contains(objectType))
      PDMLinkedObjectsHandler.AddToCollection(collection, (customService as ArticleSrvService).FindArticlesAndRelationsWithoutFiltration(objectID, filtrationOwnerID, (object) session));
    else if (this._artObjTypes.Contains(objectType))
    {
      PDMLinkedObjectsHandler.AddToCollection(collection, customService.FindArticlesByGroupIDWithoutFiltration(objectID, (object) session));
      DataTable dataTable = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID).ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(0, RelationalOperators.ObjectTypeFilter, (object) MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545"), LogicalOperators.NONE, 0, false)
      }, new object[2]{ (object) -2, (object) -20 }), objectID);
      if (dataTable.Rows.Count > 0)
      {
        long spObjectID = Convert.ToInt64(dataTable.Rows[0][0]);
        if (!collection.Exists((Predicate<LinkedObject>) (x => x.ObjectID.Equals(spObjectID))))
          collection.Add(new LinkedObject(spObjectID, Convert.ToInt64(dataTable.Rows[0][1])));
      }
    }
    return collection.Count <= 0 ? (List<LinkedObject>) null : collection;
  }

  private static void AddToCollection(List<LinkedObject> collection, long[] values)
  {
    if (values == null || values.Length == 0)
      return;
    foreach (long num in values)
    {
      long objID = num;
      if (!collection.Exists((Predicate<LinkedObject>) (x => x.ObjectID.Equals(objID))))
        collection.Add(new LinkedObject(objID));
    }
  }

  private static void AddToCollection(List<LinkedObject> collection, List<LinkedObject> values)
  {
    if (values == null || values.Count <= 0)
      return;
    foreach (LinkedObject linkedObject in values)
    {
      LinkedObject obj = linkedObject;
      if (!collection.Exists((Predicate<LinkedObject>) (x => x.ObjectID.Equals(obj.ObjectID) && x.RelationID.Equals(obj.RelationID))))
        collection.Add(obj);
    }
  }

  public string Name => "Модуль PDM";

  bool ILinkedObjectsHandler.IsTypesChanged(IUserSession session) => this.IsTypesChanged(session);

  void ILinkedObjectsHandler.UpdateHandleAndOutputTypes(IUserSession session, bool force)
  {
    this.UpdateHandleAndOutputTypes(session, force);
  }
}
