// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.NavigatorSupport.NodeFactories.TechCompositionFromRelObjInfoItemFactory
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.NavigatorSupport.NodeFactories;

/// <summary>
/// Фабрика узлов для построения дерева на основании данных о составе из RelObjInfoItem
/// </summary>
internal class TechCompositionFromRelObjInfoItemFactory : INodesFactory
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IEnumerable<RelObjInfoItem> _relObjInfoItems;
  /// <summary>
  /// 
  /// </summary>
  private readonly bool _composition;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfoItems"></param>
  public TechCompositionFromRelObjInfoItemFactory(
    IEnumerable<RelObjInfoItem> relObjInfoItems,
    bool composition)
  {
    this._relObjInfoItems = relObjInfoItems ?? throw new ArgumentNullException(nameof (relObjInfoItems));
    this._composition = composition;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="categoryId"></param>
  /// <param name="typeId"></param>
  /// <returns></returns>
  public INode GetNode(int categoryId, int typeId)
  {
    return ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false).GetNode(categoryId, typeId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeId"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  public INode GetNode(INodeID nodeId, params object[] args)
  {
    long objectId = 0;
    long prjLinkId = 0;
    if (nodeId is NodeID nodeId1)
    {
      objectId = nodeId1.ObjectID;
      prjLinkId = nodeId1.PrjLinkID;
    }
    IEnumerable<ObjInfoItem> objInfoItems = this._composition ? (IEnumerable<ObjInfoItem>) this._relObjInfoItems.Where<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item =>
    {
      ObjInfoItem projInfo = item.ProjInfo;
      if (Math.Abs(projInfo != null ? projInfo.ObjectID : 0L) != Math.Abs(objectId))
        return false;
      return prjLinkId == 0L || prjLinkId != item.RelationID;
    })).Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.PartInfo)).ToList<ObjInfoItem>() : (IEnumerable<ObjInfoItem>) this._relObjInfoItems.Where<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item =>
    {
      ObjInfoItem partInfo = item.PartInfo;
      if (Math.Abs(partInfo != null ? partInfo.ObjectID : 0L) != Math.Abs(objectId))
        return false;
      return prjLinkId == 0L || prjLinkId != item.RelationID;
    })).Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)).ToList<ObjInfoItem>();
    if (objInfoItems.Any<ObjInfoItem>((Func<ObjInfoItem, bool>) (item => item.ObjTypeID == -1)))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ObjInfoHelper.UpdateUnknownTypes(objInfoItems, sessionKeeper.Session);
    }
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache(objInfoItems.Where<ObjInfoItem>((Func<ObjInfoItem, bool>) (item => item.ObjTypeID != -1)));
    if (objectTypeCache.Count == 0)
      return (INode) null;
    foreach (KeyValuePair<int, List<long>> keyValuePair in objectTypeCache)
      keyValuePair.Value.AddRange((IEnumerable<long>) keyValuePair.Value.Select<long, long>((Func<long, long>) (item => -item)).ToArray<long>());
    return (INode) new ObjectsDictNode(objectTypeCache, true);
  }
}
