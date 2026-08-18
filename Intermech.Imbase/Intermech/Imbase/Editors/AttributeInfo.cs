// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.AttributeInfo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class AttributeInfo
{
  public int Id;
  internal string _caption;
  private int _imageId;
  private FieldTypes _fieldType;
  internal DataColumn _dataColumn;

  public AttributeInfo(int attId, DataColumn column, IUserSession session)
  {
    this.Id = attId;
    this._dataColumn = column;
    IDBAttributeType attributeType = session.GetAttributeType(this.Id);
    if (attributeType != null)
    {
      this._caption = !string.IsNullOrEmpty(attributeType.ShortName) ? $"{attributeType.Name} [{attributeType.ShortName}]" : attributeType.Name;
      this._fieldType = attributeType.AttributeType;
    }
    else
    {
      this._fieldType = FieldTypes.ftUnknown;
      this._caption = this.Id.ToString();
    }
    this._imageId = -2;
  }

  public int ImageId
  {
    get
    {
      if (this._imageId == -2)
        this._imageId = CopyRecords._iconService == null ? -1 : CopyRecords._iconService.IndexOf(3, -1, (object) this._fieldType);
      return this._imageId;
    }
  }
}
