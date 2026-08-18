// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Nodes.TechNodeID
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Nodes;

/// <summary>Define custom object node id</summary>
/// <summary>
/// 
/// </summary>
/// <param name="objTypeId"></param>
/// <param name="objId"></param>
/// <param name="id"></param>
/// <param name="checkedOutBy"></param>
/// <param name="prjLinkId"></param>
/// <param name="lcStepID"></param>
/// <param name="caption"></param>
/// <param name="relTypeID"></param>
/// <param name="owner">Владелец объекта</param>
/// <param name="projID"></param>
/// <param name="relGuid"></param>
/// <param name="modificationID">Номер группы изменений</param>
/// <param name="sorting">Значение атрибута "Сортировка"</param>
public class TechNodeID(
  int objTypeId,
  long objId,
  long id,
  long checkedOutBy,
  long prjLinkId,
  int lcStepID,
  string caption,
  int relTypeID,
  long owner,
  long sorting,
  long projID,
  Guid relGuid,
  long modificationID) : NodeID(objTypeId, objId, id, checkedOutBy, prjLinkId, lcStepID, caption, relTypeID, owner, sorting, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, projID, relGuid, modificationID)
{
  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => base.Equals(obj);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();
}
