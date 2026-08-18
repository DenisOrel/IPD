// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Nodes.TechObjectListVirtualNode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Parts;
using System;
using System.Collections;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Nodes;

/// <summary>Custom root node for TechObjectDescriptor</summary>
/// <summary>Constructor</summary>
/// <param name="descriptor"></param>
/// <param name="objectIDs"></param>
/// <param name="objectTypeId"></param>
/// <param name="expandNode"></param>
public class TechObjectListVirtualNode(
  IDescriptor descriptor,
  IList objectIDs,
  int objectTypeId,
  bool expandNode) : TechObjectListNode(descriptor, objectIDs, objectTypeId, expandNode)
{
  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-папки.
  /// </summary>
  /// <returns></returns>
  protected override ObjectsListPart GetObjectsListPart(
    IList objectVersionIds,
    IServiceProvider serviceProvider,
    int aObjectTypeID)
  {
    return (ObjectsListPart) new TechObjectListVirtualPart(objectVersionIds, (IConditionsProvider) null, serviceProvider, aObjectTypeID, this._expandNode);
  }
}
