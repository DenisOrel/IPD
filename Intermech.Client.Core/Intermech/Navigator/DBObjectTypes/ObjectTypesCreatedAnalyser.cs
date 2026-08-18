
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesCreatedAnalyser
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
/// Анализатор, отрабатывающий при создании новых типов объектов
/// </summary>
public class ObjectTypesCreatedAnalyser : IUpdateAnalyser
{
  /// <summary>Список идентификаторов новых типов объектов</summary>
  private IDictionary _newObjectTypeIDs;

  /// <summary>
  /// Создать новый анализатор, отрабатывающий при создании новых типов объектов
  /// </summary>
  /// <param name="objTypeIDs">Список идентификаторов новых типов объектов</param>
  public ObjectTypesCreatedAnalyser(IList<int> objTypeIDs)
  {
    this._newObjectTypeIDs = (IDictionary) new HybridDictionary();
    for (int index = 0; index < objTypeIDs.Count; ++index)
      this._newObjectTypeIDs.Add((object) objTypeIDs[index], (object) null);
  }

  /// <summary>Предварительные действия</summary>
  /// <param name="plan">План (к "косякам" отношения не имеет)</param>
  public void Preprocess(IUpdatePlan plan)
  {
  }

  /// <summary>Основные действия</summary>
  /// <param name="nodeID">Узел, с которым выполняются основные действия</param>
  /// <param name="plan">План (к "косякам" отношения не имеет)</param>
  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    this._newObjectTypeIDs.Remove((object) ((NodeID) nodeID).TypeID);
  }

  /// <summary>Финальные действия</summary>
  /// <param name="plan">План (к "косякам" отношения не имеет)</param>
  public void Postprocess(IUpdatePlan plan)
  {
    foreach (DictionaryEntry newObjectTypeId in this._newObjectTypeIDs)
      plan.Append((INodeID) new NodeID((int) newObjectTypeId.Key, AccessRights.NotDefined));
  }
}
