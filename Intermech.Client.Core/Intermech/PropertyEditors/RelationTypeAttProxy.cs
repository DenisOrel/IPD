
// Type: Intermech.PropertyEditors.RelationTypeAttProxy
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class RelationTypeAttProxy
{
  private Guid _guid;
  private string _typeName;

  public RelationTypeAttProxy(Guid guid)
  {
    this._guid = guid;
    this._typeName = string.Empty;
  }

  public override string ToString()
  {
    if (this._guid == Guid.Empty)
      return LocalizationHolder.rm.GetString("Client.Core_929");
    if (this._typeName.Length == 0)
      this._typeName = MetaDataHelper.GetRelationTypeName(this._guid);
    return this._typeName;
  }

  public Guid Guid => this._guid;
}
