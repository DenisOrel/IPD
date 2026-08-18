
// Type: Intermech.PropertyEditors.AttributeValueClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for FileAttributeEditClasses.</summary>
public class AttributeValueClass
{
  public List<AttributeSingleValueClass> items = new List<AttributeSingleValueClass>();
  public int attributeID;
  public string attributeName = string.Empty;
  public Guid attributeGuid = Guid.Empty;
  public bool attributeReadOnly;
  public bool attributeDisableManualEdit;
  public FieldTypes attributeType;
  public AttributeValueClassList Owner;

  public AttributeValueClass(IDBAttribute iDBAttribute, IDBAttribute attrContentModifyDate)
  {
    this.attributeID = iDBAttribute.AttributeID;
    this.attributeName = iDBAttribute.Name;
    this.attributeGuid = (iDBAttribute as IDBGuid).GUID;
    this.attributeReadOnly = iDBAttribute.ReadOnly;
    this.attributeType = iDBAttribute.AttributeType.AttributeType;
    this.attributeDisableManualEdit = (iDBAttribute.AttributeType.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit;
    DateTime contentModifyDate = attrContentModifyDate == null || attrContentModifyDate.IsNull ? DateTime.MinValue : attrContentModifyDate.AsDateTime;
    for (int index = 0; index < iDBAttribute.ValuesCount; ++index)
    {
      iDBAttribute.Index = index;
      IBlobReader blobReader = iDBAttribute as IBlobReader;
      try
      {
        BlobInformation bi = blobReader.OpenBlob(-1);
        long boxId = -1;
        if (this.attributeType == FieldTypes.ftFile || this.attributeType == FieldTypes.ftBlob)
          boxId = Convert.ToInt64(iDBAttribute.AsDouble);
        AttributeSingleValueClass singleValueClass = new AttributeSingleValueClass(bi, boxId);
        if (this.attributeType == FieldTypes.ftFile)
          singleValueClass.InitializeColorText(bi.FileType, contentModifyDate);
        this.items.Add(singleValueClass);
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
    }
  }
}
