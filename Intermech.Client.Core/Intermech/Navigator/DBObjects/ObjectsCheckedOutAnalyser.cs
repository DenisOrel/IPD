
// Type: Intermech.Navigator.DBObjects.ObjectsCheckedOutAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjects;

public class ObjectsCheckedOutAnalyser : IUpdateAnalyser
{
  /// <summary>
  /// Коллекция пар значений [(Int64)ID старый] = [(Int64)ID новый]
  /// </summary>
  private IDictionary _checkedOutObjectIDs;

  /// <summary>
  /// Создать анализатор. Условие - (objIDs.Count == newObjIDs.Count)
  /// </summary>
  /// <param name="objIDs">Список старых идентификаторов объектов</param>
  /// <param name="newObjIDs">Список новых идентификаторов объектов</param>
  public ObjectsCheckedOutAnalyser(IList<long> objIDs, IList<long> newObjIDs)
  {
    this._checkedOutObjectIDs = (IDictionary) new HybridDictionary();
    for (int index = 0; index < objIDs.Count; ++index)
      this._checkedOutObjectIDs.Add((object) objIDs[index], (object) newObjIDs[index]);
  }

  /// <summary>Предварительная обработка плана</summary>
  /// <param name="plan">План обновления</param>
  public void Preprocess(IUpdatePlan plan)
  {
  }

  /// <summary>Обработать указанный узел согласно указанному плану</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="plan">План обновления</param>
  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID))
      return;
    NodeID nodeId = (NodeID) nodeID;
    if (!this._checkedOutObjectIDs.Contains((object) nodeId.ObjectID))
      return;
    plan.Replace((INodeID) new NodeID(new CreateObjectNodeParams(nodeId.TypeID, (long) this._checkedOutObjectIDs[(object) nodeId.ObjectID], nodeId.ID, nodeId.CheckedOutBy, nodeId.PrjLinkID, nodeId.LCStepID, nodeId.Caption, nodeId.RelationTypeID, nodeId.Owner, nodeId.Sorting, nodeId.State, nodeId.Version, nodeId.BaseVersion, nodeId.SiteID, nodeId.ProjID, nodeId.RelGuid, nodeId.ModificationID)));
  }

  /// <summary>Выполнить финальную обработку плана</summary>
  /// <param name="plan">План обновления</param>
  public void Postprocess(IUpdatePlan plan)
  {
  }
}
