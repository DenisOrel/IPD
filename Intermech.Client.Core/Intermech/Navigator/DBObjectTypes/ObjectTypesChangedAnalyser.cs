
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesChangedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjectTypes.Implementation;
using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Анализатор, отрабатывающий при изменении типов объектов
/// </summary>
public class ObjectTypesChangedAnalyser : IUpdateAnalyser
{
  /// <summary>Коллекция идентификаторов изменённых типов объектов</summary>
  private IDictionary _changedObjectTypeIDs;

  /// <summary>
  /// Создать новый анализатор, отрабатывающий при изменении типов объектов
  /// </summary>
  /// <param name="objTypeIDs">Список идентификаторов изменённых типов объектов</param>
  public ObjectTypesChangedAnalyser(IList<int> objTypeIDs)
  {
    this._changedObjectTypeIDs = (IDictionary) new HybridDictionary();
    for (int index = 0; index < objTypeIDs.Count; ++index)
      this._changedObjectTypeIDs.Add((object) objTypeIDs[index], (object) null);
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
    if (!this._changedObjectTypeIDs.Contains((object) ((NodeID) nodeID).TypeID))
      return;
    plan.Update();
  }

  /// <summary>Финальные действия</summary>
  /// <param name="plan">План (к "косякам" отношения не имеет)</param>
  public void Postprocess(IUpdatePlan plan)
  {
  }
}
