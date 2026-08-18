// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ChangeRelInfo
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Информация об изменениях связей в структуре комплекта</summary>
[Serializable]
public class ChangeRelInfo : ChangeInfo
{
  /// <summary>Ид. версии родительского объкта</summary>
  protected long _projID;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <param name="operType"></param>
  public ChangeRelInfo(long id, DocOperType operType)
    : this(id, -1, 0L, operType)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <param name="typeID"></param>
  /// <param name="projID"></param>
  /// <param name="operType"></param>
  public ChangeRelInfo(long id, int typeID, long projID, DocOperType operType)
    : base(id, typeID, operType)
  {
    this._projID = projID;
    this._elemType = AttributableElements.Relation;
  }

  /// <summary>Ид. версии родительского объкта</summary>
  public virtual long ProjID => this._projID;
}
