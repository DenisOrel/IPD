// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.RelationAlreadyExists
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal class RelationAlreadyExists : Exception
{
  private string _parentObjName;
  private string _childObjName;
  private string _relationName;

  public RelationAlreadyExists(
    long parentObjId,
    long childObjId,
    int relTypeId,
    string parentObjName,
    string childObjName,
    string relationName)
  {
    this.ParentObjId = parentObjId;
    this.ChildObjId = childObjId;
    this.RelTypeId = relTypeId;
    this._parentObjName = parentObjName;
    this._childObjName = childObjName;
    this._relationName = relationName;
  }

  public override string Message
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Tools.Client_271"), (object) this._parentObjName, (object) this._childObjName, (object) this._relationName);
    }
  }

  public long ParentObjId { get; }

  public long ChildObjId { get; }

  public int RelTypeId { get; }
}
