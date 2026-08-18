// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.NavigatorSupport.NodeFactories.TechCompositionFromDataTableNodesFactory
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.NavigatorSupport.NodeFactories;

/// <summary>
/// Фабрика узлов для построения дерева на основании данных о составе из DataTable
/// </summary>
internal class TechCompositionFromDataTableNodesFactory : INodesFactory
{
  /// <summary>
  /// 
  /// </summary>
  private readonly DataTable _dataTable;
  /// <summary>Индекс поля с ид. родительского объекта</summary>
  private readonly int _idxFldProjObjId;
  /// <summary>Индекс поля с ид. дочернего объекта</summary>
  private readonly int _idxFldPartObjId;
  /// <summary>Индекс поля с ид. типом дочернего объекта</summary>
  private readonly int _idxFldPartObjType;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataTable"></param>
  public TechCompositionFromDataTableNodesFactory(DataTable dataTable)
  {
    this._dataTable = dataTable ?? throw new ArgumentNullException(nameof (dataTable));
    this._idxFldProjObjId = dataTable.Columns.IndexOf("F_PROJ_ID");
    this._idxFldPartObjId = dataTable.Columns.IndexOf(DataHelper.Consts.cnt_fld_PartObjID);
    this._idxFldPartObjType = dataTable.Columns.IndexOf("F_OBJECT_TYPE");
    if (this._idxFldPartObjId != -1)
      return;
    this._idxFldPartObjId = dataTable.Columns.IndexOf("F_OBJECT_ID");
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
    if (this._idxFldPartObjId == -1 || this._idxFldProjObjId == -1)
      return (INode) null;
    long objectId = 0;
    if (nodeId is NodeID nodeId1)
      objectId = nodeId1.ObjectID;
    List<ObjInfoItem> list = this._dataTable.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToInt64(row[this._idxFldProjObjId]) == objectId && row[this._idxFldProjObjId] != row[this._idxFldPartObjId])).Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (row => new ObjInfoItem(Convert.ToInt64(row[this._idxFldPartObjId]), this._idxFldPartObjType != -1 ? Convert.ToInt32(row[this._idxFldPartObjType]) : -1))).ToList<ObjInfoItem>();
    if (list.Any<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => item.ObjTypeID == -1)))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) list, sessionKeeper.Session);
    }
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache(list.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => item.ObjTypeID != -1)));
    return objectTypeCache.Count == 0 ? (INode) null : (INode) new ObjectsDictNode(objectTypeCache, true);
  }
}
