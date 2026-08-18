
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesRemovedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjectTypes.Implementation;
using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Анализатор, отрабатывающий при удалении типов объектов
/// </summary>
public class ObjectTypesRemovedAnalyser : IUpdateAnalyser
{
  /// <summary>Коллекция идентификаторов удалённых типов объектов</summary>
  private Hashtable _removedObjectTypeIDs;

  /// <summary>
  /// Создать новый анализатор, отрабатывающий при удалении типов объектов
  /// </summary>
  /// <param name="objTypeIDs">Коллекция идентификаторов удалённых типов объектов</param>
  public ObjectTypesRemovedAnalyser(IList<int> objTypeIDs)
  {
    this._removedObjectTypeIDs = new Hashtable();
    for (int index = 0; index < objTypeIDs.Count; ++index)
      this._removedObjectTypeIDs.Add((object) objTypeIDs[index], (object) null);
  }

  /// <summary>Предварительные действия</summary>
  /// <param name="plan">План (к "косякам" отношения не имеет)</param>
  public void Preprocess(IUpdatePlan plan)
  {
  }

  /// <summary>Основные действия</summary>
  /// <param name="nodeID">Узел</param>
  /// <param name="plan">План (к "косякам" отношения не имеет)</param>
  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID nodeId) || !this._removedObjectTypeIDs.Contains((object) nodeId.TypeID))
      return;
    plan.Remove();
  }

  /// <summary>Финальные действия</summary>
  /// <param name="plan">План (к "косякам" отношения не имеет)</param>
  public void Postprocess(IUpdatePlan plan)
  {
  }
}
