// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttribute4RelationTypeInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

internal class CAttribute4RelationTypeInfoCollection(
  MetadataInfoParentContext serviceContext,
  int parentID,
  bool filtering) : CAttribute4TypeInfoCollection(serviceContext, parentID, filtering)
{
  protected override string DBKeyField => "F_RELATION_TYPE";

  protected override string DBTableName => "IMS_ATTR4RELATION_TYPES";

  protected override IDBAttributeTypeInfo4 CreateAttributeTypeInfo4(
    DataRow attr_row,
    DataRow attr4type_row)
  {
    return Convert.ToInt32(attr_row["F_ATTRIBUTE_TYPE"]) == 13 ? (IDBAttributeTypeInfo4) new CMeasuredAttributeTypeInfo4Relation(this.ServiceContext, attr_row, attr4type_row) : (IDBAttributeTypeInfo4) new CAttributeTypeInfo4Relation(this.ServiceContext, attr_row, attr4type_row);
  }

  protected override void ThrowNotFoundException(int attributeID)
  {
    throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_133"), (object) MetaDataHelper.GetRelationTypeName((int) this.ParentID), (object) MetaDataHelper.GetAttributeTypeName(Convert.ToInt32(attributeID))));
  }

  public override bool IsEnabledAttribute(int attributeID)
  {
    return AttributeCacheHelper.IsEnabledRelationTypeAttribute(attributeID, Convert.ToInt32(this.ParentID));
  }

  protected override DataRow FindRow(int attributeID)
  {
    return this.ServiceContext.ClientCache.GetTable(this.DBTableName).Rows.Find(new object[2]
    {
      (object) (int) this.ParentID,
      (object) attributeID
    });
  }
}
