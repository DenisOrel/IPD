// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.RelationWrapper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules;

/// <summary>Relation wrapper</summary>
internal class RelationWrapper
{
  /// <summary>
  /// 
  /// </summary>
  private int _reltypeID = -1;
  /// <summary>
  /// 
  /// </summary>
  private Guid _reltypeGuid;
  /// <summary>
  /// 
  /// </summary>
  private string _relTypeName = string.Empty;

  public RelationWrapper(Guid aGuid)
  {
    this._reltypeGuid = aGuid;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(aGuid);
    if (relationType == null)
      return;
    this._reltypeID = relationType.RelationTypeID;
    this._relTypeName = relationType.Description;
  }

  /// <summary>
  /// 
  /// </summary>
  public int RelTypeID => this._reltypeID;

  /// <summary>
  /// 
  /// </summary>
  public Guid RelTypeGuid => this._reltypeGuid;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this._relTypeName;
}
