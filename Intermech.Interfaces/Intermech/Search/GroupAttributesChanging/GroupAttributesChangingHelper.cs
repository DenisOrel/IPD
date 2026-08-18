
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.GroupAttributesChanging
{
    public static class GroupAttributesChangingHelper
    {
      public static bool IsEditableAttribute(int attributeTypeID)
      {
        IMSAttributeType imsAttributeType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? MetaDataHelper.GetAttributeType(attributeTypeID) : throw new ArgumentException();
        return (attributeTypeID == -50 || imsAttributeType != null && (imsAttributeType.FieldType == FieldTypes.ftString || imsAttributeType.FieldType == FieldTypes.ftMemo)) && imsAttributeType.MultiValueMode == MultiValueModes.SingleValue && imsAttributeType.Computed == ComputeValueModes.NotComputableValue;
      }

      public static bool IsEditableAttribute(int objectTypeId, int attributeTypeId)
      {
        if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeId))
          throw new ArgumentException();
        IMSAttributeType imsAttributeType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeId) ? MetaDataHelper.GetAttributeType(attributeTypeId) : throw new ArgumentException();
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objectTypeId, attributeTypeId);
        if (attributeTypeId != -50 && (imsAttributeType == null || imsAttributeType.FieldType != FieldTypes.ftString && imsAttributeType.FieldType != FieldTypes.ftMemo) || imsAttributeType.MultiValueMode != MultiValueModes.SingleValue)
          return false;
        return attribute4ObjectType == null ? imsAttributeType.Computed == ComputeValueModes.NotComputableValue : attribute4ObjectType.Computed == ComputeValueModes.NotComputableValue;
      }
    }
}
