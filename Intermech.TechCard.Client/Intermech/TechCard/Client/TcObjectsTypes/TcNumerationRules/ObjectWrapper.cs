// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.ObjectWrapper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules;

/// <summary>Internal wrappers for TcNumeration</summary>
internal class ObjectWrapper
{
  private int _objTypeID;
  private Guid _objTypeGuid;
  private string _objTypeName;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aGuid"></param>
  public ObjectWrapper(Guid aGuid)
  {
    this._objTypeGuid = aGuid;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(aGuid);
    if (objectType != null)
    {
      this._objTypeID = objectType.ObjectTypeID;
      this._objTypeName = objectType.ObjectTypeName;
    }
    else
    {
      this._objTypeID = -1;
      this._objTypeName = string.Empty;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int ObjTypeID => this._objTypeID;

  /// <summary>
  /// 
  /// </summary>
  public Guid ObjTypeGuid => this._objTypeGuid;

  /// <summary>
  /// 
  /// </summary>
  public string ObjTypeName => this._objTypeName;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this._objTypeName;
}
